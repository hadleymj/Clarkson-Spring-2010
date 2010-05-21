<?php
/*
 * Created on Nov 3, 2009
 *
 * To change the template for this generated file go to
 * Window - Preferences - PHPeclipse - PHP - Code Templates
 */
 
require_once("db_include.php");

/*
 * UserDAO extends DAO
 * 		GetConnection()
 * 
 * Members:
 * 	public $Username = "";
	public $Password = "";
	public $FirstName = "";
	public $MiddleName = "";
	public $LastName = "";
	public $idNumber = -1;
	public $Address = "";
	public $PhoneNumber = "";
	public $UserClass = ""; 
 * 
 * Methods (public):
 * 		Insert()
 * 				//Set all the fields and call the insert function.
				//Will return false if the username is already taken.
				 * 
 * 		Find()
 * 				//Set the Username.
 				//Will return false if the user does not exist.
 				//If the user does exist the rest of the fields will be filled and true will be returned.
 				 *  		
 * 		RetrieveUserId()
 * 				//Get the idNumber for the object provided with only the Username field set.
				//This will change the idNumber field but not alter any other fields.
				 * 
 * 		Update()
 * 			 	//Set the username, and change the other fields accordingly.
 				//Call update and the values of the fields in the DAO will be pushed on to the database.
 				 * 
 * 		ListPatients()
 * 				//ListPatients provides an array of PatientDAO objects. Where each cell is associated with
				//a patient that belongs to the Clinician. This must be called from a Clinician class UserDAO object.
 * 
 */

 
 class UserDAO extends DAO
 {

	public $Username = "";
	public $Password = "";
	public $FirstName = "";
	public $MiddleName = "";
	public $LastName = "";
	public $idNumber = -1;
	public $Address = "";
	public $PhoneNumber = "";
	public $UserClass = ""; 

	//Given a query replace the appropriate field identifiers with their values from this object.
	// return the query with all the field identifiers completed.
	private function SetupQuery($query)
	{
		$needles = array("%Key", "%idNumber", "%Username", "%Password", "%FirstName", "%MiddleName", "%LastName", "%Address", "%PhoneNumber", "%UserClass");
		$replace = array(Config::DB_KEY, $this->idNumber, $this->Username, $this->Password, $this->FirstName, $this->MiddleName, $this->LastName, $this->Address, $this->PhoneNumber, $this->UserClass);

		$query = str_replace($needles, $replace, $query);
		
		if ( Config::DAO_DEBUG_MODE == 1 ) print $query;
		
		return $query;	
	}

	//Set all the fields and call the insert function.
	//Will return false if the username is already taken.
 	public function Insert()
 	{
		$conn = parent::GetConnection();
		
		//Use the query defined in the Query object.	
		$query = $this->SetupQuery(Query::INSERT_USER);

		if ( !odbc_exec($conn, $query) )
			return HandleError($conn);
		
		if ( !($result = odbc_exec($conn, Query::LAST_INSERT_ID)) )
			return HandleError($conn);
			
		$row = array();
		if ( !odbc_fetch_into($result, $row) )
			return $this->HandleError($conn);
			
		$this->idNumber = $row[0];

		return true;	
 	}
 	
 	//Set the Username.
 	//Will return false if the user does not exist.
 	//If the user does exist the rest of the fields will be filled and true will be returned.
 	public function Find()
 	{
		$conn = parent::GetConnection();
		
		$query = $this->SetupQuery(Query::FIND_USER);
		
		$result = odbc_exec($conn, $query);
		
		if ( !$result )
			return $this->HandleError($conn);
		
		$row = array();	
		odbc_fetch_into($result, $row);

		$this->idNumber = $row[0];
		$this->UserClass = $row[1];
		$this->Password = $row[2];
		$this->FirstName = $row[3];
		$this->MiddleName = $row[4];
		$this->LastName = $row[5];
		$this->Address = $row[6];
		$this->PhoneNumber = $row[7];
		
		return true; 		
 	}
 	
 	//Get the idNumber for the object provided with only the Username field set.
 	//This will change the idNumber field but not alter any other fields.
 	public function RetrieveUserId()
 	{
 		$conn = parent::GetConnection();
		
		$query = $this->SetupQuery(Query::FIND_USER);
		
		$result = odbc_exec($conn, $query);
		
		if ( !$result )
			return $this->HandleError($conn);
		
		$row = array();	
		odbc_fetch_into($result, $row);

		$this->idNumber = $row[0];	
 		
 	}
 	
 	//Set the username, and change the other fields accordingly.
 	//Call update and the values of the fields in the DAO will be pushed on to the database.
 	public function Update()
 	{
 		$conn = parent::GetConnection();

 		$query = $this->SetupQuery(Query::UPDATE_USER);

		if ( !odbc_exec($conn, $query) )
			return $this->HandleError($conn);
 				
 		return true; 		
 	}
 	
	//ListPatients provides an array of PatientDAO objects. Where each cell is associated with
	//a patient that belongs to the Clinician. This must be called from a Clinician class UserDAO object.
	public function ListPatients()
 	{
 		$this->Find();
 		
 		if ( $this->UserClass != "Clinician" )
 			return array();
 		
 		$conn = parent::GetConnection();
 		
 		$query = $this->SetupQuery(Query::LIST_PATIENTS);
 		
 		$patientArray = array();
 		
 		$result = odbc_exec($conn, $query);
 		
 		if ( !$result )
 		{
 			$this->HandleError($conn);
 			return array();
 		}
 		
 		$row = array();
 		while ( odbc_fetch_into($result, $row) )
 		{ 			
 			$className = $row[1]."DAO";
 			$patientArray[] = new $className();
 			
 			$i = count($patientArray) - 1;
 			
 			$patientArray[$i]->Username = $row[0];
 			$patientArray[$i]->Find();
 			
 		}
 		
 		return $patientArray;
 	}
 }
 
?>
