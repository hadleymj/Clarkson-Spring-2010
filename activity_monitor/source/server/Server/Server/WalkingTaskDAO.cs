using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Odbc;
using System.Data;

namespace Server
{
    class WalkingTaskDAO : DailyTaskDAO
    {

        protected int m_Steps;

        public int Steps
        {
            get
            {
                return m_Steps;
            }
        }

        override public bool Find()
        {
            if (!base.Find())
                return false;

            string mySelectQuery = "select Steps from WalkingTask where idDailyTask = '" + m_idDailyTask + "'";

            OdbcConnection myConnection = GetConnection();
            OdbcCommand myCommand = new OdbcCommand(mySelectQuery, myConnection);

            myConnection.Open();

            OdbcDataReader myReader;
            myReader = myCommand.ExecuteReader(CommandBehavior.SingleRow);
            try
            {
                while (myReader.Read())
                {
                    m_Steps = myReader.GetInt32(0);
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
