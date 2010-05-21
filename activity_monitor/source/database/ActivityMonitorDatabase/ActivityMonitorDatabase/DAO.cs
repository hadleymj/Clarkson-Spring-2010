using System;
using System.Collections.Generic;

using System.Text;
using System.Data.Odbc;
using System.Configuration;

namespace Server
{
    public abstract class DAO
    {
        /// <summary>
        /// Get a new database connection. The connection will be over an SSL socket to the Activity Monitor Database. These options
        /// are currently hardcoded into the software.
        /// </summary>
        /// <returns>A new database connection over SSL to the localhost MySQL server.</returns>
        public OdbcConnection GetConnection()
        {
            //string connStr = "Driver={MySQL ODBC 5.1 Driver};Server=localhost;Database=mydb;User=ssluser;Password=goodsecret;Option=3;";
            string connStr = "Driver={MySQL ODBC 5.1 Driver};Server=localhost;Database=Activity_Monitor;User=root;Password=bnbdbpw;sslca=c:/newcerts/ca-cert.pem;sslcert=c:/newcerts/client-cert.pem;sslkey=c:/newcerts/client-key.pem;sslverify=0;Option=3;";
            //string connStr = ConfigurationManager.ConnectionStrings["MySQL"]

            OdbcConnection newConn = new OdbcConnection(connStr);

            return newConn;
        }



    }
}
