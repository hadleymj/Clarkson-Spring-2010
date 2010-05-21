using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;

namespace Server
{
    class WalkingStatsDAO : StatisticsDAO
    {

        public WalkingStatsDAO()
        {
            m_StatsClass = "WalkingStats";
        }

        protected int m_Steps;

        public int Steps
        {
            get
            {
                return m_Steps;
            }
            set
            {
                m_Steps = value;
            }
        }

        
        override public bool Insert()
        {

            if (m_idStatistics == -1)
                return false;

            if (!base.Insert())
                return false;
            
            string myInsertQuery = "insert into WalkingStats (idStatistics, Steps) values (" + m_idStatistics + ", " + m_Steps + ")";

            OdbcConnection myConnection = GetConnection();
            OdbcCommand myCommand = new OdbcCommand(myInsertQuery, myConnection);

            myConnection.Open();

            int rowsAffected = myCommand.ExecuteNonQuery();

            myConnection.Close();

            return rowsAffected == 1;
        }

    }
}
