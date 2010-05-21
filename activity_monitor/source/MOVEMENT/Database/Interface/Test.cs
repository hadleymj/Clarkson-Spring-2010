using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Text;

using Movement.Analysis;
using Movement.Scripting;

namespace Movement.Database
{
    /// <summary>
    /// A test taken by a patient.
    /// </summary>
    public class Test : DBObject
    {
        #region Private Attributes
        private int _TestID;
        private int _UserID;
        private int _PatientID;
        private int _ScriptID;

        private DateTime _Timestamp;
		private char _Hand;
		private char _Mode;
		private short _Rotation;
        #endregion

        /// <summary>
        /// The test's database ID.
        /// </summary>
        public int TestID { get { return _TestID; } }
        
        /// <summary>
        /// The user that administered the test.
        /// </summary>
        public User User { get { return new User(_UserID); } }

        /// <summary>
        /// The patient that took the test.
        /// </summary>
        public Patient Patient { get { return new Patient(_PatientID); } }

        /// <summary>
        /// The script that was used to run the test.
        /// </summary>
        public TestScript TestScript { get { return new TestScript(_ScriptID); } }

        /// <summary>
        /// The data collected during the test.
        /// </summary>
        public TestData Data { get { return new TestData(TestID); } }

        /// <summary>
        /// The timestamp for the test.
        /// </summary>
        public DateTime Timestamp { get { return _Timestamp; } }

		/// <summary>
		/// The hand (R or L) that the test was taken with.
		/// </summary>
		public char Hand { get { return _Hand; } }

		/// <summary>
		/// The mode that the test was run in.
		/// </summary>
		public char Mode { get { return _Mode; } }

		/// <summary>
		/// The rotation / orientation that the test was taken with, 
		/// in positive degress counter-clockwise from the cartesian
		/// x-axis.
		/// </summary>
		public short Rotation { get { return _Rotation; } }

		/// <summary>
		/// The analysis data available for this test.
		/// </summary>
		public TestAnalysis Analysis { get { return new TestAnalysis(TestID); } }

		/// <summary>
		/// Checks if the available analysis data is within normal limits or not.
		/// </summary>
		public bool AnalysisIsNormal{get{return new NormativeAnalysis(_ScriptID).IsNormal(Analysis);}}

        /// <summary>
        /// Notes recorded about this test.
        /// </summary>
		public ReadOnlyCollection<TestNote> Notes
		{
			get
			{
				List<TestNote> Notes = new List<TestNote>();

				using(IDataReader Reader = ExecuteReader(GetConnection(),
					"GET_TEST_NOTES",
					CommandBehavior.CloseConnection,
					"@testID", TestID))
				{
					while(Reader.Read())
						Notes.Add(new TestNote(this, (DateTime)Reader["timestamp"], SafeRead(Reader, "user_id", -1), (string)Reader["note"]));
				}

				return new ReadOnlyCollection<TestNote>(Notes);
			}
		}

		/// <summary>
		/// Constructs a new test instance given the test's database ID
		/// and retrieves the test information from the database.
		/// </summary>
		/// <param name="testID">The test's database ID.</param>
        public Test(int testID)
        {
			using(IDataReader Reader = ExecuteReader(GetConnection(),
				"GET_TEST",
				CommandBehavior.CloseConnection | CommandBehavior.SingleResult,
				"@testID", testID))
			{
				if(!Reader.Read())
					throw new RecordNotFoundException();

				_TestID = (int)Reader["test_id"];
				_UserID = SafeRead(Reader, "user_id", -1);
				_PatientID = (int)Reader["patient_id"];
				_ScriptID = (int)Reader["script_id"];
                _Timestamp = (DateTime)Reader["timestamp"];
				_Hand = SafeRead(Reader, "hand", 'R');
				_Mode = SafeRead(Reader, "mode", 'D');
				_Rotation = SafeRead(Reader, "rotation", (short)0);
			}
        }

        /// <summary>
        /// Stores a new note regarding this test.
        /// </summary>
        /// <param name="author">The author of the note.</param>
        /// <param name="data">The content of the note.</param>
        /// <returns>The created note.</returns>
        public TestNote RecordNote(
            User author,
            string data)
        {
			Execute(GetConnection(),
				"STORE_TEST_NOTE",
				"@testID", TestID,
				"@userID", author.UserID,
				"@note", data);

			return new TestNote(this, DateTime.UtcNow, author.UserID, data);
        }

		public override bool Equals(object obj)
		{
			Test other = (Test)obj;
			return TestID == other.TestID;
		}

		public override int GetHashCode()
		{
			return TestID.GetHashCode();
		}

		/// <summary>
		/// Creates and stores a new test.
		/// </summary>
		/// <param name="user">The user that administered the test.</param>
		/// <param name="patient">The patient that took the test.</param>
		/// <param name="hand">The hand that the test was taken with.</param>
		/// <param name="script">The script used to run the test.</param>
		/// <param name="mode">The mode that the test was run in.</param>
		/// <param name="data">The data collected during the test.</param>
		/// <param name="meanX">The average x-coordinate of all the data collected.</param>
		/// <param name="meanY">The average y-coordinate of all the data collected.</param>
		/// <param name="rotation">The number of degrees that the test was rotated (from the positive x-axis) when taken.</param>
		/// <returns>The created test.</returns>
		public static Test CreateTest(
			User user,
			Patient patient,
			char hand,
			TestScript script,
			char mode,
			IEnumerable<TestDataSample> data,
			double meanX,
			double meanY,
			short rotation)
        {
			int TestID = -1;

			//create an analyzer
			Analyzer DataAnalyzer = new Analyzer(
				meanX,
				meanY,
				new ScriptEngine(script.ScriptData).Path);

			using(DbConnection Connection = GetAsyncConnection())
			{
				Connection.Open();

				//begin a transaction so that creation is atomic
				DbTransaction Transaction = Connection.BeginTransaction();

				try
				{
					//create the test
					TestID = Execute(Connection, Transaction,
						"STORE_TEST_RESULTS",
						"@scriptID", script.ScriptID,
						"@patientID", patient.PatientID,
						"@userID", user.UserID,
						"@hand", hand,
						"@mode", mode,
						"@rotation", rotation);


					//prepare the add-sample command ahead of time for efficiency
					DbCommand AddSampleCommand = PrepareCommand(Connection, Transaction, "ADD_TEST_SAMPLE");
					AddSampleCommand.Parameters["@testID"].Value = TestID;
					int O_X = AddSampleCommand.Parameters.IndexOf("@x"),	//determine the parameter indices ahead of time for efficiency
						O_Y = AddSampleCommand.Parameters.IndexOf("@y"),
						O_P = AddSampleCommand.Parameters.IndexOf("@p"),
						O_T = AddSampleCommand.Parameters.IndexOf("@t");

					IAsyncResult AddSampleHandle = null;


					//store and analyze samples in parallel
					foreach(TestDataSample s in data)
					{
						//modify the prepared procedure arguments
						AddSampleCommand.Parameters[O_X].Value = s.X;
						AddSampleCommand.Parameters[O_Y].Value = s.Y;
						AddSampleCommand.Parameters[O_P].Value = s.Pressure;
						AddSampleCommand.Parameters[O_T].Value = s.Time;						

						//finish the last procedure execution
						EndExecute(AddSampleHandle);

						//start the next procedure execution
						AddSampleHandle = BeginExecute(AddSampleCommand);

						//perform analysis on the sample
						DataAnalyzer.AnalyzeSample(s.Time, s.X, s.Y, s.Pressure);
					}

					EndExecute(AddSampleHandle);

					//store the analysis
					TestAnalysis.StoreAnalysis(
						Connection,
						Transaction,
						TestID,
						DataAnalyzer.CurrentAnalysis);

					Transaction.Commit();
				}
				catch(Exception e)
				{
					Transaction.Rollback();
					throw e;
				}
			}

			

			return new Test(TestID);
        }
    }


    /// <summary>
    /// A collection of samples in a test.
    /// </summary>
    public class TestData : DBObject,
		IDisposable,
		IEnumerable<TestDataSample>,
		IEnumerator<TestDataSample>
    {
        #region Private Attributes
        private int _SampleCount; 
        #endregion

		private IDataReader Reader = null;
		
        /// <summary>
        /// The number of samples in this test data collection.
        /// </summary>
        public int SampleCount { get { return _SampleCount; } }

        internal TestData(int testID)
        {
			Reader = ExecuteReader(GetConnection(),
				"GET_TEST_SAMPLES",
				CommandBehavior.CloseConnection,
				"@testID", testID);

			//get the sample count
			if(!Reader.Read())
				throw new RecordNotFoundException();

			_SampleCount = (int)Reader[0];

			//move the reader to the next recordset
			if(!Reader.NextResult())
				throw new RecordNotFoundException();
        }

		#region IDisposable Members

		public void Dispose()
		{
			if(Reader != null)
			{
				Reader.Close();
				Reader = null;
			}
		}

		#endregion

		#region IEnumerable<TestDataSample> Members

		public IEnumerator<TestDataSample> GetEnumerator()
		{
			return this;
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return this;
		}

		#endregion

		#region IEnumerator<TestDataSample> Members

		public TestDataSample Current
		{
			get
			{
				return new TestDataSample(
					(int)Reader["time"],
					(short)Reader["x"],
					(short)Reader["y"],
					(short)Reader["pressure"]);
			}
		}

		#endregion

		#region IEnumerator Members

		object System.Collections.IEnumerator.Current
		{
			get { return Current; }
		}

		public bool MoveNext()
		{
			return Reader.Read();
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}

		#endregion
	}

    /// <summary>
    /// A single sample in a test.
    /// </summary>
    public struct TestDataSample
    {
        /// <summary>
        /// An invalid test sample.
        /// </summary>
        public static readonly TestDataSample INVALID = new TestDataSample(-1, -1, -1, -1);

        private int _Time;
        private short _X, _Y, _Pressure;

        /// <summary>
        /// The time in ms of the test sample since the start of the test.
        /// </summary>
        public int Time { get { return _Time; } }

        /// <summary>
        /// The x-coord of the sample in mm.
        /// </summary>
        public short X { get { return _X; } }

        /// <summary>
        /// The y-coord of the sample in mm.
        /// </summary>
        public short Y { get { return _Y; } }

        /// <summary>
        /// The pressure level of the sample in mN.
        /// </summary>
        public short Pressure { get { return _Pressure; } }

        public TestDataSample(
            int time,
            short x,
            short y,
            short pressure)
        {
            _Time = time;
            _X = x;
            _Y = y;
            _Pressure = pressure;
        }

        public override bool Equals(object obj)
        {
            return obj is TestDataSample
                && _Time == ((TestDataSample)obj)._Time
                && _X == ((TestDataSample)obj)._X
                && _Y == ((TestDataSample)obj)._Y
                && _Pressure == ((TestDataSample)obj)._Pressure;
        }

        public override int GetHashCode()
        {
            return Time ^ X ^ Y ^ Pressure;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2}, {3})", Time, X, Y, Pressure);
        }
    }
}
