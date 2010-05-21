<?php
/*
 * Created on Nov 11, 2009
 *
 * To change the template for this generated file go to
 * Window - Preferences - PHPeclipse - PHP - Code Templates
 */
 
class Config
{
	const DB_KEY = "key";
	
	const DB_USERNAME = "root";
	
	const DB_PASSWORD = "bnbdbpw";

	const DB_DRIVER = "{MySQL ODBC 5.1 Driver}";
	
	const DB_SERVER = "localhost";

	const DB_DATABASE = "Activity_Monitor";

	const DB_USE_SSL = 0;
	
	const DB_SSL_CACERT = "c:/newcerts/ca-cert.pem";
	
	const DB_SSL_CERT = "c:/newcerts/client-cert.pem";
	
	const DB_SSL_KEY = "c:/newcerts/client-key.pem";
	
	const DB_CONN_STRING = "Driver=%Driver;Server=%Server;Database=%Database;sslca=%Cacert;sslcert=%Sslcert;sslkey=%Sslkey;sslverify=%Usessl;Option=3;";
	
	const DAO_DEBUG_MODE = 0;
}
?>
