using System;

using System.Text;
using System.Data;

using System.Collections;
using System.Data.Odbc;


namespace Server
{
    class UserDAO : DAO
    {

        protected string m_Username = string.Empty;
        protected string m_Password = string.Empty;
        protected string m_UserClass = string.Empty;
        protected int m_idNumber = -1;

        public string Username
        {
            get
            {
                return m_Username;
            }
            set
            {
                m_Username = value;
            }
        }

        

        public string Password
        {
            get
            {
                return m_Password;
            }
        }

        

        public string UserClass
        {
            get
            {
                return m_UserClass;
            }
        }

        

        public int idNumber
        {
            get
            {
                return m_idNumber;
            }
        }

        /// <summary>
        /// Use the Username property to fill in the remainder of the fields if the User exists in the database.
        /// </summary>
        /// <returns>True, if the record was found. Else the return value will be false.</returns>
        public bool Find()
        {

            string mySelectQuery = "select AES_DECRYPT(Password, 'key'), idNumber, UserClass from User where Username = '" + m_Username + "'";


            OdbcConnection myConnection = GetConnection();
            OdbcCommand myCommand = new OdbcCommand(mySelectQuery, myConnection);

            myConnection.Open();

            bool recordFound = false;
            OdbcDataReader myReader;
            myReader = myCommand.ExecuteReader(CommandBehavior.SingleRow);
            try
            {
                if (myReader.Read())
                {
                    m_Password = myReader.GetString(0);
                    m_idNumber = myReader.GetInt32(1);
                    m_UserClass = myReader.GetString(2);
                    recordFound = true;
                }
            }
            finally
            {
                myReader.Close();
                myConnection.Close();
            }

            return recordFound;
        }



    }
}
