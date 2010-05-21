using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Movement.Database
{
    /// <summary>
    /// A user that can log into the system and administer tests.
    /// </summary>
    public class User : DBObject
    {
        #region Private Attributes
        private int _UserID;
        private DateTime _Created;
        private DateTime _LastLogin;

        private string _Name;
        private string _ContactInfo;

        private string _Username;
        private char _Role; 
        #endregion

        /// <summary>
        /// The user's database ID.
        /// </summary>
        public int UserID { get { return _UserID; } }

        /// <summary>
        /// The timestamp when the user was created.
        /// </summary>
        public DateTime Created { get { return _Created; } }
        /// <summary>
        /// The timestamp when the user last logged in, or DateTime.MinValue if the user has never logged in.
        /// </summary>
        public DateTime LastLogin { get { return _LastLogin; } }

        /// <summary>
        /// The user's name.
        /// </summary>
        public string Name
        {
            get { return _Name; }
			set
			{
				_Name = value;

				Execute(GetConnection(),
					"SET_CLINICIAN_FULL_NAME",
					"@userID", UserID,
					"@fullName", value);
			}
        }

        /// <summary>
        /// The user's contact info.
        /// </summary>
        public string ContactInfo
        {
            get { return _ContactInfo; }
			set
			{
				_ContactInfo = value;

				Execute(GetConnection(),
					"SET_CLINICIAN_INFO",
					"@userID", UserID,
					"@contactInfo", value);
			}
        }

        /// <summary>
        /// The user's username.
        /// </summary>
        public string Username { get { return _Username; } }

        /// <summary>
        /// The user's role.
        /// </summary>
        public char Role { get { return _Role; } }

        /// <summary>
        /// The user's password (write-only).
        /// </summary>
		public string Password
		{
			set
			{
				Execute(GetConnection(),
					"CHANGE_PASSWORD",
					"@userID", UserID,
					"@newPassword", HashPassword(Username, value));
			}
		}

		/// <summary>
		/// Constructs a new user instance given the user's database ID
		/// and retrieves the user's information from the database.
		/// </summary>
		/// <param name="userID">The user's database ID.</param>
        public User(int userID)
        {
			using(IDataReader Reader = ExecuteReader(GetConnection(),
				"GET_USER",
				CommandBehavior.CloseConnection | CommandBehavior.SingleRow,
				"@userID", userID))
			{
				if(!Reader.Read())
					throw new RecordNotFoundException();

				_UserID = (int)Reader["user_id"];
				_Role = SafeRead(Reader, "role", '?');
				_Created = (DateTime)Reader["created"];
				_LastLogin = SafeRead(Reader, "last_login", DateTime.MinValue);
				_Username = (string)Reader["username"];
				_Name = (string)Reader["name"];
				_ContactInfo = SafeRead(Reader, "contact_info", "");
			}
        }

		public override bool Equals(object obj)
		{
			User other = (User)obj;
			return UserID == other.UserID;
		}

		public override int GetHashCode()
		{
			return UserID.GetHashCode();
		}

        /// <summary>
        /// Creates and stored a new user.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The user's password.</param>
        /// <param name="role">The user's role.</param>
        /// <param name="name">The name of the user.</param>
        /// <param name="contactInfo">The user's contact information.</param>
        /// <returns>The created user.</returns>
        public static User CreateUser(
            string username,
            string password,
            char role,
            string name,
            string contactInfo)
        {

			return new User(Execute(GetConnection(), 
				"CREATE_CLINICIAN", 
				"@uname", username,
				"@upass", HashPassword(username, password),
				"@realname", name,
				"@info", contactInfo,
				"@role", role));
        }

        /// <summary>
        /// Removes a user from the system.
        /// </summary>
        /// <param name="userID">The user's database id.</param>
        /// <remarks>Tests and patients administered by this user may not be removed.</remarks>
        public static void RemoveUser(
            int userID)
        {
			Execute(GetConnection(),
				"REMOVE_CLINICIAN",
				"@userID", userID);
        }

        /// <summary>
        /// Logs a user into the system.
        /// </summary>
        /// <param name="username">The user's username.</param>
        /// <param name="password">The user's password.</param>
        /// <returns>The logged in user, or null if login failed.</returns>
        public static User Login(
            string username,
            string password)
        {
			using(IDataReader Reader = ExecuteReader(GetConnection(),
				"GET_LOGIN",
				CommandBehavior.CloseConnection | CommandBehavior.SingleRow,
				"@uname", username))
			{
				if(!Reader.Read() || !CheckPassword(username, password, (byte[])Reader["password"]))
					return null;
				else
					return new User((int)Reader["user_id"]);
			}
        }

		private static byte[] HashPassword(string username, string password)
		{
			return new System.Security.Cryptography.SHA1Managed().ComputeHash(
				System.Text.UTF8Encoding.UTF8.GetBytes(username + ":" + password));
		}

		private static bool CheckPassword(string username, string password, byte[] storedHash)
		{
			byte[] SubmittedHash = HashPassword(username, password);

			//make sure the lengths match
			if(SubmittedHash.Length != storedHash.Length)
				return false;

			//now make sure the hashes match
			for(int i = 0; i < SubmittedHash.Length; i++)
				if(SubmittedHash[i] != storedHash[i])
					return false;

			return true;
		}
	}
}
