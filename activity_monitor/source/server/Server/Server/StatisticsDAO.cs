using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;

namespace Server
{
    abstract class StatisticsDAO : DAO
    {
        protected string m_StatsClass;
        protected DateTime m_StartDateTime, m_EndDateTime;
        protected int m_idStatistics, m_idDailyTask;

        public DateTime StartDateTime
        {
            get
            {
                return m_StartDateTime;
            }
            set
            {
                m_StartDateTime = value;
            }
        }

        public DateTime EndDateTime
        {
            get
            {
                return m_EndDateTime;
            }
            set
            {
                m_EndDateTime = value;
            }
        }

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

        virtual public bool Insert()
        {
            string myInsertQuery = "insert into Statistics (idDailyTask, StatsClass, StartDateTime, EndDateTime) values (" + m_idDailyTask + ", '" + m_StatsClass + "', '" + m_StartDateTime.ToString("yyyyMMdd") + "', '" + m_EndDateTime.ToString("yyyyMMdd") + "')";

            OdbcConnection myConnection = GetConnection();
            OdbcCommand myCommand = new OdbcCommand(myInsertQuery, myConnection);

            myConnection.Open();

            int rowsAffected = myCommand.ExecuteNonQuery();

            if (rowsAffected != 1)
                return false;

            string mySelectQuery = "select LAST_INSERT_ID()";

            myCommand = new OdbcCommand(mySelectQuery, myConnection);

            Object obj = myCommand.ExecuteScalar();

            m_idStatistics = (int)(long)obj;

            myConnection.Close();


            return true;
        }
    }
}
