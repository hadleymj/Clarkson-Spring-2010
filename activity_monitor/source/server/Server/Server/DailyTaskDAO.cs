using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Data;

namespace Server
{
    class DailyTaskDAO : DAO
    {
        protected int m_idDailyTask;
        protected int m_idPatient;
        protected string m_DailyTaskClass;
        protected bool m_Active;
        protected DateTime m_Date;

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


        public List<DailyTaskDAO> GetActiveTasks(UserDAO patient)
        {
            List<DailyTaskDAO> results = new List<DailyTaskDAO>();

            patient.Find();

            if (!patient.UserClass.Equals("Patient"))
                return null;

            string myQueryStr = "select DailyTaskClass, idDailyTask from DailyTask where active=true and idPatient=" + patient.idNumber;

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

                    if (myReader.GetString(0).Equals("WalkingTask"))
                    {
                        temp = new WalkingTaskDAO();
                        temp.m_idDailyTask = myReader.GetInt32(1);
                        
                    }

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
