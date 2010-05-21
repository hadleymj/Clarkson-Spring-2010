using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Text;

namespace Movement.Database
{
    /// <summary>
    /// A patient that can take tests.
    /// </summary>
    public class Patient : DBObject
    {
        #region Private Attributes
        private int _PatientID;
        private DateTime _Created;

        private string _Name;
        private string _Address;
        private string _ContactInfo;

        private char _Sex;
        private DateTime _DOB;
        private char _Handedness;

        private string _SSN4; 
        #endregion

        /// <summary>
        /// The patient's database ID.
        /// </summary>
        public int PatientID { get { return _PatientID; } }

        /// <summary>
        /// The timestamp when the patient was created.
        /// </summary>
        public DateTime Created { get { return _Created; } }

        /// <summary>
        /// The name of the patient.
        /// </summary>
        public string Name
        {
            get { return _Name; }
			set
			{
				_Name = value;
				Execute(GetConnection(),
					"SET_PATIENT_FULL_NAME",
					"@patientID", PatientID,
					"@fullName", value);
			}
        }

        /// <summary>
        /// The address of the patient.
        /// </summary>
        public string Address
        {
            get { return _Address; }	//TODO: implement set address
            set
			{
				_Address = value;
				Execute(GetConnection(),
					"SET_PATIENT_ADDRESS",
					"@patientID", PatientID,
					"@address", value);
			}
        }

        /// <summary>
        /// The contact info of the patient.
        /// </summary>
        public string ContactInfo
        {
            get { return _ContactInfo; }
			set
			{
				_ContactInfo = value;
				Execute(GetConnection(),
					"SET_PATIENT_INFO",
					"@patientID", PatientID,
					"@info", value);
			}
        }

        /// <summary>
        /// The patient's sex.
        /// </summary>
        /// <remarks>'M' = Male, 'F' = Female.</remarks>
        public char Sex { get { return _Sex; } }

        /// <summary>
        /// The patient's date of birth.
        /// </summary>
        public DateTime DOB { get { return _DOB; } }

        /// <summary>
        /// The patient's dominant hand.
        /// </summary>
        /// <remarks>'L' = Left Hand, 'R' = Right Hand, 'A' = Ambidextrous.</remarks>
        public char Handedness { get { return _Handedness; } }

        /// <summary>
        /// The last four digits of the patient's social security number.
        /// </summary>
        public string SSN4 { get { return _SSN4; } }

        /// <summary>
        /// Notes recorded about this patient.
        /// </summary>
		public ReadOnlyCollection<PatientNote> Notes
		{
			get
			{
				List<PatientNote> Notes = new List<PatientNote>();

				using(IDataReader Reader = ExecuteReader(GetConnection(),
					"GET_PATIENT_NOTES",
					CommandBehavior.CloseConnection,
					"@patientID", PatientID))
				{
					while(Reader.Read())
						Notes.Add(new PatientNote(this, (DateTime)Reader["timestamp"], SafeRead(Reader, "user_id", -1), (string)Reader["note"]));
				}

				return new ReadOnlyCollection<PatientNote>(Notes);
			}
		}


		/// <summary>
		/// Constructs a new patient instance given the patient's database ID
		/// and retrieves the patient information from the database.
		/// </summary>
		/// <param name="patientID">The patient's database ID.</param>
        public Patient(int patientID)
        {
			using(IDataReader Reader = ExecuteReader(GetConnection(),
				"GET_PATIENT",
				CommandBehavior.CloseConnection | CommandBehavior.SingleRow,
				"@patientID", patientID))
			{
				if(!Reader.Read())
					throw new RecordNotFoundException();

				_PatientID = (int)Reader["patient_id"];
				_Name = (string)Reader["name"];
				_Address = SafeRead(Reader, "address", "");
				_ContactInfo = SafeRead(Reader, "contact_info", "");
				_SSN4 = (string)Reader["ssn4"];

				_Created = (DateTime)Reader["created"];
				_Sex = ((string)Reader["sex"])[0];
				_DOB = (DateTime)Reader["dob"];
				_Handedness = ((string)Reader["handedness"])[0];

			}
        }

        /// <summary>
        /// Stores a new note regarding this patient.
        /// </summary>
        /// <param name="author">The author of the note.</param>
        /// <param name="data">The content of the note.</param>
        /// <returns>The created note.</returns>
        public PatientNote RecordNote(
            User author,
            string data)
        {
			Execute(GetConnection(),
				"STORE_PATIENT_NOTE",
				"@patientID", PatientID,
				"@userID", author.UserID,
				"@note", data);

			return new PatientNote(this, DateTime.UtcNow, author.UserID, data);
        }

		public override bool Equals(object obj)
		{
			Patient other = (Patient)obj;
			return PatientID == other.PatientID;
		}

		public override int GetHashCode()
		{
			return PatientID.GetHashCode();
		}

		/// <summary>
		/// Gets all the tests that this patient has taken.
		/// </summary>
		/// <returns>A collection containing every test taken by this user.</returns>
		public ReadOnlyCollection<Test> GetAllTests()
		{
			List<int> Result = new List<int>();

			using(IDataReader Reader = ExecuteReader(GetConnection(),
				"GET_ALL_TESTS",
				CommandBehavior.CloseConnection,
				"@patientID", PatientID))
			{
				while(Reader.Read())
					Result.Add((int)Reader["test_id"]);
			}

			//convert and return all the ids as instances of Test
			return new ReadOnlyCollection<Test>(Result.ConvertAll<Test>(
				new Converter<int, Test>(
					delegate(int testID)
					{
						return new Test(testID);
					})));
		}

		/// <summary>
		/// Gets the last n tests that this patient has taken.
		/// </summary>
		/// <param name="n">The number of tests to get, n > 0.</param>
		/// <returns>A collection containing the last n tests taken by this user.</returns>
		public ReadOnlyCollection<Test> GetLastTests(int n)
		{
			if(n <= 0)
				throw new ArgumentOutOfRangeException("int n", n, "n must be > 0.");

			List<int> Result = new List<int>();

			using(IDataReader Reader = ExecuteReader(GetConnection(),
				"GET_LAST_TEST",
				CommandBehavior.CloseConnection,
				"@patientID", PatientID,
				"@numberOfTests", n))
			{
				while(Reader.Read())
					Result.Add((int)Reader["test_id"]);
			}

			//convert and return all the ids as instances of Test
			return new ReadOnlyCollection<Test>(Result.ConvertAll<Test>(
				new Converter<int, Test>(
					delegate(int testID)
					{
						return new Test(testID);
					})));
		}

        /// <summary>
        /// Creates and stores a new patient.
        /// </summary>
        /// <param name="name">The patient's name.</param>
        /// <param name="address">The patient's address.</param>
        /// <param name="contactInfo">The patient's contact information.</param>
        /// <param name="sex">The patient's sex.</param>
        /// <param name="dob">The patient's date of birth.</param>
        /// <param name="handedness">The patient's dominant hand.</param>
        /// <param name="ssn4">The last four digits of the patient's social security number.</param>
        /// <returns>The created patient.</returns>
        public static Patient CreatePatient(
            string name,
            string address,
            string contactInfo,
            char sex,
            DateTime dob,
            char handedness,
            string ssn4)
        {
			dob = dob.Date;

			return new Patient(Execute(GetConnection(),
				"CREATE_PATIENT",
				"@name", name,
				"@ssn", ssn4,
				"@address", address,
				"@contact", contactInfo,
				"@sex", sex,
				"@dob", dob,
				"@handedness", handedness));
        }

		private static ReadOnlyCollection<Patient> EndFindPatient(IDataReader Reader)
		{
			List<int> ResultSet = new List<int>();

			using(Reader)
			{
				while(Reader.Read())
					ResultSet.Add(Reader.GetInt32(0));
			}
			
			//convert and return all the ids as instances of Patient
			return new ReadOnlyCollection<Patient>(ResultSet.ConvertAll<Patient>(
				new Converter<int, Patient>(
					delegate(int patientID)
					{
						return new Patient(patientID);
					})));
		}

        /// <summary>
        /// Finds a patient given name and date of birth.
        /// </summary>
        /// <param name="name">The patient's name.</param>
        /// <param name="dob">The patient's date of birth.</param>
        /// <returns>A collection of matching patients.</returns>
		public static ReadOnlyCollection<Patient> FindPatient(
            string name,
            DateTime dob)
        {
			dob = dob.Date;

			IDataReader Reader = ExecuteReader(GetConnection(),
				"FIND_PATIENT_BY_DOB",
				CommandBehavior.CloseConnection,
				"@name", name,
				"@dob", dob);

			return EndFindPatient(Reader);
        }

        /// <summary>
        /// Finds a patient given name and last 4 digits of ssn.
        /// </summary>
        /// <param name="name">The patient's name.</param>
        /// <param name="ssn4">The last 4 digits of the patient's ssn.</param>
        /// <returns>A collection of matching patients.</returns>
        public static ReadOnlyCollection<Patient> FindPatient(
            string name,
            string ssn4)
        {
			IDataReader Reader = ExecuteReader(GetConnection(),
				"FIND_PATIENT_BY_SSN",
				CommandBehavior.CloseConnection,
				"@name", name,
				"@ssn", ssn4);
			
			return EndFindPatient(Reader);
        }

		/// <summary>
		/// Finds a patient given their name.
		/// </summary>
		/// <param name="name">The patient's name.</param>
		/// <returns>A collection of matching patients.</returns>
		public static ReadOnlyCollection<Patient> FindPatient(
			string name)
		{
			IDataReader Reader = ExecuteReader(GetConnection(),
				"FIND_PATIENT_BY_NAME",
				CommandBehavior.CloseConnection,
				"@name", name);

			return EndFindPatient(Reader);
		}

		/// <summary>
		/// Finds a patient given date of birth and last 4 digits of ssn.
		/// </summary>
		/// <param name="dob">The patient's date of birth.</param>
		/// <param name="ssn4">The last 4 digits of the patient's ssn.</param>
		/// <returns>A collection of matching patients.</returns>
		public static ReadOnlyCollection<Patient> FindPatient(
			DateTime dob,
			string ssn4)			
		{
			dob = dob.Date;
			IDataReader Reader = ExecuteReader(GetConnection(),
				"FIND_PATIENT_BY_SSN_DOB",
				CommandBehavior.CloseConnection,
				"@ssn", ssn4,
				"@dob", dob);

			return EndFindPatient(Reader);
		}

		/// <summary>
		/// Finds a patient given date of birth and last 4 digits of ssn.
		/// </summary>
		/// <param name="name">The patient's name.</param>
		/// <param name="dob">The patient's date of birth.</param>
		/// <param name="ssn4">The last 4 digits of the patient's ssn.</param>
		/// <returns>A collection of matching patients.</returns>
		public static ReadOnlyCollection<Patient> FindPatient(
			string name,
			DateTime dob,
			string ssn4)
		{
			dob = dob.Date;
			IDataReader Reader = ExecuteReader(GetConnection(),
							"FIND_PATIENT_BY_NAME_SSN_DOB",
							CommandBehavior.CloseConnection,
							"@name", name,
							"@ssn", ssn4,
							"@dob", dob);

			return EndFindPatient(Reader);
		}


        /// <summary>
        /// Removes a patient from the system.
        /// </summary>
        /// <param name="patientID">The patient's database ID.</param>
        /// <remarks>Test data collected about this patient may not be removed.</remarks>
        public static void RemovePatient(
            int patientID)
        {
			Execute(GetConnection(),
				"REMOVE_PATIENT",
				"@patientID", patientID);
        }
    }
}
