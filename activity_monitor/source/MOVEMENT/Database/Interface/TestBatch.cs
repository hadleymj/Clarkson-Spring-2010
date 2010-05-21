using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Movement.Database
{
	/// <summary>
	/// A collection of tests that can be run as a batch.
	/// </summary>
	public class TestBatch : DBObject
	{
		#region Private Attributes
		
		private int _TestBatchID;
		private string _Name;
		private string _Description;

		private ReadOnlyCollection<TestScript> _TestScripts;
		
		#endregion

		/// <summary>
		/// The database ID for this test batch.
		/// </summary>
		public int TestBatchID { get { return _TestBatchID; } }

		/// <summary>
		/// A name for this test batch.
		/// </summary>
		public string Name { get { return _Name; } }

		/// <summary>
		/// A description for this test batch.
		/// </summary>
		public string Description { get { return _Description; } }

		/// <summary>
		/// A collection of scripts, in order, that compose this test batch.
		/// </summary>
		public ReadOnlyCollection<TestScript> TestScripts { get { return _TestScripts; } }


		/// <summary>
		/// Creates a new instance of a TestBatch given the test batch database ID
		/// and retrieves the batch information from the database.
		/// </summary>
		/// <param name="testBatchID">The database ID for the test batch to retreive.</param>
		public TestBatch(int testBatchID)
		{
			_TestBatchID = testBatchID;

			Update();
		}

		private void Update()
		{
			List<int> TestScriptIDs = new List<int>();

			using(IDataReader Reader = ExecuteReader(GetConnection(),
				"GET_TEST_BATCH",
				CommandBehavior.CloseConnection,
				"@batchID", TestBatchID))
			{
				if(!Reader.Read())
					throw new RecordNotFoundException();

				_Name = (string)Reader["name"];
				_Description = (string)Reader["description"];

				if(!Reader.NextResult())
					throw new RecordNotFoundException();

				while(Reader.Read())
					TestScriptIDs.Add(Reader.GetInt32(0));
			}

			_TestScripts = new ReadOnlyCollection<TestScript>(TestScriptIDs.ConvertAll(
				new Converter<int, TestScript>(
					delegate(int scriptID)
					{
						return new TestScript(scriptID);
					})));

			_RetrievalTimestamp = DateTime.UtcNow;
		}


		/// <summary>
		/// Updates the tests associated with this test batch.
		/// </summary>
		/// <param name="newTestScriptIDs">A list of test script IDs, in order, that this batch should be composed of.</param>
		/// <remarks>This test batch instance will be updated with the new test scripts.</remarks>
		public void UpdateBatch(
			IEnumerable<int> newTestScriptIDs)
		{
			using(DbConnection Connection = GetConnection())
			{
				Connection.Open();
				DbTransaction Transaction = Connection.BeginTransaction();

				try
				{
					//clear the tests that are already specified for this test batch
					Execute(Connection, Transaction,
						"REMOVE_TEST_BATCH_TESTS",
						"@batchID", TestBatchID);

					//insert the new tests into this batch in order
					int Sequence = 0;
					foreach(int scriptID in newTestScriptIDs)
						Execute(Connection, Transaction,
							"ADD_TEST_BATCH_TEST",
							"@batchID", TestBatchID,
							"@sequence", Sequence++,
							"@scriptID", scriptID);

					Transaction.Commit();
				}
				catch(Exception e)
				{
					Transaction.Rollback();
					throw e;
				}
			}

			//refresh the tests in this test batch
			Update();
		}

		/// <summary>
		/// Gets a list of all available test script batches in the database.
		/// </summary>
		/// <returns>The list of test script batches.</returns>
		public static ReadOnlyCollection<TestBatch> GetTestBatches()
		{
			List<int> Result = new List<int>();

			//read the ids of all the test script types

			using(IDataReader Reader = ExecuteReader(GetConnection(),
				"GET_ALL_TEST_BATCHES",
				CommandBehavior.CloseConnection))
			{
				while(Reader.Read())
					Result.Add((int)Reader["batch_id"]);
			}

			//convert and return all the ids as instances of TestScriptType
			return new ReadOnlyCollection<TestBatch>(Result.ConvertAll<TestBatch>(
				new Converter<int, TestBatch>(
					delegate(int batchID)
					{
						return new TestBatch(batchID);
					})));
		}

		/// <summary>
		/// Creates a new test batch.
		/// </summary>
		/// <param name="name">The name of the batch.</param>
		/// <param name="description">A description for the batch.</param>
		/// <param name="testScriptIDs">A collection of script IDs, int order, that should compose this test batch.</param>
		/// <returns></returns>
		public static TestBatch CreateTestBatch(
			string name,
			string description,
			IEnumerable<int> testScriptIDs)
		{
			int TestBatchID = -1;

			using(DbConnection Connection = GetConnection())
			{
				Connection.Open();
				DbTransaction Transaction = Connection.BeginTransaction();

				try
				{
					//create the test batch
					TestBatchID = Execute(Connection, Transaction,
						"CREATE_TEST_BATCH",
						"@name", name,
						"@description", description);

					//insert the new tests into this batch in order
					int Sequence = 0;
					foreach(int scriptID in testScriptIDs)
						Execute(Connection, Transaction,
							"ADD_TEST_BATCH_TEST",
							"@batchID", TestBatchID,
							"@sequence", Sequence++,
							"@scriptID", scriptID);

					Transaction.Commit();
				}
				catch(Exception e)
				{
					Transaction.Rollback();
					throw e;
				}
			}

			return new TestBatch(TestBatchID);
		}


		/// <summary>
		/// Removes a test batch from the database.
		/// </summary>
		/// <param name="testBatchID">The database test batch ID to remove.</param>
		public static void RemoveTestBatch(
			int testBatchID)
		{
			Execute(GetConnection(),
				"REMOVE_TEST_BATCH",
				"@batchID", testBatchID);
		}
	}
}
