using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Data;

namespace Server
{

    /// <summary>
    /// A class to access the daily tasks stored within the database.
    /// </summary>
    public class DailyTaskDAO : DAO
    {
        protected int m_idDailyTask;
        protected int m_idPatient;
        protected string m_DailyTaskClass;
        protected bool m_Active;
        protected DateTime m_Date;

        /// <summary>
        /// The database key for DailyTask type records.
        /// </summary>
        public int idDailyTask
        {
            get
            {
                return m_idDailyTask;
            }
            set
            {
                m_idDailyTask = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return m_Date;
            }
        }

        public string DailyTaskClass
        {
            get
            {
                return m_DailyTaskClass;
            }

        }

        public bool Active
        {
            get
            {
                return m_Active;
            }
        }

        /// <summary>
        /// Use the value of the idDailyTask property to locate the object in the database and obtain the remainder of the DAO property values.
        /// </summary>
        /// <returns>True, if the object with the specified idDailyTask was found, else False.</returns>
        virtual public bool Find()
        {
            string mySelectQuery = "select idPatient, DailyTaskClass, Date, Active from DailyTask where idDailyTask = '" + m_idDailyTask + "'";

            OdbcConnection myConnection = GetConnection();
            OdbcCommand myCommand = new OdbcCommand(mySelectQuery, myConnection);

            myConnection.Open();

            OdbcDataReader myReader;
            myReader = myCommand.ExecuteReader(CommandBehavior.SingleRow);
            try
            {
                while (myReader.Read())
                {
                    m_idPatient = myReader.GetInt32(0);
                    m_DailyTaskClass = myReader.GetString(1);
                    m_Date = myReader.GetDateTime(2);
                    m_Active = myReader.GetBoolean(3);
                }
            }
            finally
            {
                myReader.Close();
                myConnection.Close();
            }


            return true;
        }

    }
}
