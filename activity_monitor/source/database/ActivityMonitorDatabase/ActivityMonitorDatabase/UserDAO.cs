using System;

using System.Text;
using System.Data;

using System.Collections;
using System.Data.Odbc;
using System.Collections.Generic;

namespace Server
{

    public class UserDAO : DAO
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

        /// <summary>
        /// If this is a Patient class record, find and return all the Active daily tasks associated with this record.
        /// </summary>
        /// <returns>A list of DailyTaskDAOs which represent the patient's active daily tasks. If the patient was not found, then an empty list is returned. If there was an error, null is returned. Note, the objects in the list will be derived classes of DailyTaskDAO and not DailyTaskDAO objects.</returns>
        public List<DailyTaskDAO> GetActiveTasks()
        {
            List<DailyTaskDAO> results = new List<DailyTaskDAO>();

            string myQueryStr = "select DailyTaskClass, idDailyTask from DailyTask where active=true and idPatient=" + m_idNumber;

            OdbcConnection myConnection = GetConnection();
            OdbcCommand myCommand = new OdbcCommand(myQueryStr, myConnection);

            myConnection.Open();

            OdbcDataReader myReader;
            myReader = myCommand.ExecuteReader();
            try
            {
                while (myReader.Read())
                {
                    DailyTaskDAO temp = null;

                    WalkingTaskDAO wtd = new WalkingTaskDAO();
                    Type typeofTask = Type.GetType("Server."+myReader.GetString(0)+"DAO", true);
                    temp = (DailyTaskDAO)(Activator.CreateInstance(typeofTask));

                    temp.idDailyTask = myReader.GetInt32(1);

                    if (temp == null)
                        return null;

                    temp.Find();

                    results.Add(temp);
                }
            }
            finally
            {
                myReader.Close();
                myConnection.Close();
            }

            return results;
        }

    }
}
