using System;
using System.Collections.Generic;

using System.Text;
using System.Data.Odbc;


namespace Server
{
    class DAO
    {
        /// <summary>
        /// Get a new database connection. The connection will be over an SSL socket to the Activity Monitor Database. These options
        /// are currently hardcoded into the software.
        /// </summary>
        /// <returns>A new database connection over SSL to the localhost MySQL server.</returns>
        public OdbcConnection GetConnection()
        {
            //string connStr = "Driver={MySQL ODBC 5.1 Driver};Server=localhost;Database=mydb;User=ssluser;Password=goodsecret;Option=3;";
            string connStr = "Driver={MySQL ODBC 5.1 Driver};Server=localhost;Database=Activity_Monitor;User=ssluser;Password=goodsecret;sslca=c:/newcerts/ca-cert.pem;sslcert=c:/newcerts/client-cert.pem;sslkey=c:/newcerts/client-key.pem;sslverify=1;Option=3;";

            OdbcConnection newConn = new OdbcConnection(connStr);

            return newConn;
        }



    }
}
