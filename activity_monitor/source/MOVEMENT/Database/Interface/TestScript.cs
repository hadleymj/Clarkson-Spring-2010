using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Movement.Database
{
	/// <summary>
	/// A test script type describes a test.
	/// Many tests may have the same test script type
	/// but different versions.
	/// </summary>
	public class TestScriptType : DBObject
	{
		#region Private Attributes

		private int _TypeID;
		private string _Name;
		private string _Description;

		#endregion

		/// <summary>
		/// The database ID for this test script type.
		/// </summary>
		public int TypeID { get { return _TypeID; } }

		/// <summary>
		/// The name of this test type.
		/// </summary>
		public string Name { get { return _Name; } }

		/// <summary>
		/// The description for this test type.
		/// </summary>
		public string Description { get { return _Description; } }

		/// <summary>
		/// Constructs a new test script type instance given a type
		/// and retrieves the type information from the database.
		/// </summary>
		/// <param name="typeID"></param>
		public TestScriptType(
			int typeID)
		{
			using(IDataReader Reader = ExecuteReader(GetConnection(),
				"GET_TEST_SCRIPT_TYPE",
				CommandBehavior.CloseConnection | CommandBehavior.SingleRow,
				"@typeID", typeID))
			{
				if(!Reader.Read())
					throw new RecordNotFoundException();

				_TypeID = (int)Reader["type_id"];
				_Name = (string)Reader["name"];
				_Description = (string)Reader["description"];
			}
		}

		/// <summary>
		/// Gets the latest (most recently defined - highest version number) 
		/// test script that is of this test script type.
		/// </summary>
		/// <returns>The test script instance.</returns>
		public TestScript GetLatestScript()
		{
			return new TestScript((int)ExecuteScalar(GetConnection(), 
				"GET_CURRENT_TEST_SCRIPT", 
				"@typeID", TypeID));
		}

		/// <summary>
		/// Creates a new test script type and stores it in the database.
		/// </summary>
		/// <param name="name">The name of the test script type.</param>
		/// <param name="description">The description.</param>
		/// <returns>The created test script type.</returns>
		public static TestScriptType CreateTestScriptType(
			string name,
			string description)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets a list of all available test script types in the database.
		/// </summary>
		/// <returns>The list of test script types.</returns>
		public static ReadOnlyCollection<TestScriptType> GetTestScriptTypes()
		{
			List<int> Result = new List<int>();

			//read the ids of all the test script types

			using(IDataReader Reader = ExecuteReader(GetConnection(),
				"GET_ALL_TEST_SCRIPT_TYPES",
				CommandBehavior.CloseConnection))
			{
				while(Reader.Read())
					Result.Add((int)Reader["type_id"]);
			}

			//convert and return all the ids as instances of TestScriptType
			return new ReadOnlyCollection<TestScriptType>(Result.ConvertAll<TestScriptType>(
				new Converter<int,TestScriptType>(
					delegate(int typeID)
					{
						return new TestScriptType(typeID);
					})));
		}
	}

    /// <summary>
    /// A test script that can be used to drive the test engine.
    /// </summary>
    public class TestScript : DBObject
    {
        #region Private Attributes
		private int _ScriptID;
		private int _TypeID;
		private int _Version;

		private DateTime _Created;

        private string _ScriptData; 
        #endregion

		/// <summary>
		/// The database ID for this test script.
		/// </summary>
		public int ScriptID { get { return _ScriptID; } }

		/// <summary>
		/// The test script type.
		/// </summary>
		public TestScriptType ScriptType { get { return new TestScriptType(_TypeID); } } 

        /// <summary>
        /// The script version.
        /// </summary>
        public int Version{get{return _Version;}}

        /// <summary>
        /// The body of the script.
        /// </summary>
        public string ScriptData{get{return _ScriptData;}}

		/// <summary>
		/// The analysis results expected for this test script.
		/// </summary>
		public NormativeAnalysis NormalAnalysis { get { return new NormativeAnalysis(ScriptID); } }


		/// <summary>
		/// Constructs a new test instance given the test script's id
		/// and retrieves the test script information from the database.
		/// </summary>
		/// <param name="scriptID">The database ID for the script.</param>
        public TestScript(int scriptID)
        {
			using(IDataReader Reader = ExecuteReader(GetConnection(),
				"GET_TEST_SCRIPT",
				CommandBehavior.CloseConnection | CommandBehavior.SingleRow,
				"@scriptID", scriptID))
			{
				if(!Reader.Read())
					throw new RecordNotFoundException();

				_ScriptID = (int)Reader["script_id"];
				_TypeID = (int)Reader["type_id"];
				_Version = (int)Reader["version"];
				_Created = (DateTime)Reader["timestamp"];
				_ScriptData = (string)Reader["body"];
			}
        }

        /// <summary>
        /// Creates a new script.
        /// </summary>
        /// <param name="type">The type of script to create.</param>
        /// <param name="scriptData">The body of the script.</param>
        /// <returns>The created script.</returns>
        public static TestScript CreateTestScript(
            int type,
            string scriptData)
        {
            throw new NotImplementedException();
        }
    }
}
