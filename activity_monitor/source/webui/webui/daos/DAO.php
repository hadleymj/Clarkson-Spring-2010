<?php
/*
 * Created on Nov 3, 2009
 *
 * To change the template for this generated file go to
 * Window - Preferences - PHPeclipse - PHP - Code Templates
 */
 
 require_once("db_include.php");
 
 
/*
 * DAO
 * 
 * Methods(public):
 * 		GetConnection()
 * 					//Returns a valid connection to the database. 
					//It uses the connection string in db_config.php to establish the connection.
					 *
 * 		
 */ 
 
 class DAO
 {
 	public function GetConnection()
 	{

		$conn_string = Config::DB_CONN_STRING;
		
		$conn_string = str_replace("%Driver", Config::DB_DRIVER, $conn_string);
		$conn_string = str_replace("%Server", Config::DB_SERVER, $conn_string);
		$conn_string = str_replace("%Database", Config::DB_DATABASE, $conn_string);
		$conn_string = str_replace("%Usessl", Config::DB_USE_SSL, $conn_string);
		$conn_string = str_replace("%Cacert", Config::DB_SSL_CACERT, $conn_string);
		$conn_string = str_replace("%Sslcert", Config::DB_SSL_CERT, $conn_string);
		$conn_string = str_replace("%Sslkey", Config::DB_SSL_KEY, $conn_string);

		if ( Config::DAO_DEBUG_MODE == 2 ) print("Conn String: \"$conn_string\"");		

		$conn = odbc_connect($conn_string, Config::DB_USERNAME, Config::DB_PASSWORD);
		
		//Proof that it is actually using SSL.
		//$conn = odbc_connect("Driver={MySQL ODBC 5.1 Driver};Server=localhost;Database=Activity_Monitor;", "ssluser", "goodsecret");

		return $conn;
 	 }
 	 
 	  	//If an error occurred on the connection, process the error displaying the 
 	// message if necessary and return false.
	protected function HandleError($conn)
	{
		if ( Config::DAO_DEBUG_MODE == 1 ) odbc_errormsg($conn);
		return false;
	}
 	
 }
 
?>
