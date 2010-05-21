<?php
/*
 * Created on Nov 3, 2009
 *
 * To change the template for this generated file go to
 * Window - Preferences - PHPeclipse - PHP - Code Templates
 */
 
//require_once("UserDAO.php");
require_once("db_include.php"); 
 
 
/*
 * PatientDAO extends UserDAO
 * 		GetConnection()
 * 		ListPatients() //Invalid because the calling object is not a Clinician Class.
 * 		RetrieveUserId()
 * 				//Get the idNumber for the object provided with only the Username field set.
				//This will change the idNumber field but not alter any other fields.
				 * 
 * 
 * Members:
 * 	public $Height = 0;
	public $Weight = 0;
	public $DOB = "19000101";
	public $Gender = "";
	public $SSN = "";
	public $MedicalConditions = "";
	public $ClinicianId = 0; 
 * 
 * Methods (public):
 * 		Insert() : override
 * 				//Set all the fields and call the insert function.
				//Will return false if the username is already taken.
				 * 
 * 		Find() : override
 * 				//Set the Username.
 				//Will return false if the user does not exist.
 				//If the user does exist the rest of the fields will be filled and true will be returned.
 				 *  		
 * 		Update() : override
 * 			 	//Set the username, and change the other fields accordingly.
 				//Call update and the values of the fields in the DAO will be pushed on to the database.
 				 * 
 * 		ListDailyTasks()
 * 				//Retrive a list of DailyTaskDAO objects which could be any derived object of DailyTaskDAO
				//that represents this Patients ACTIVE daily task list.
				//The Username of the PatientDAO object must be set.
 * 
 */ 
 
 
 class PatientDAO extends UserDAO
 {
	public $Height = 0;
	public $Weight = 0;
	public $DOB = "19000101";
	public $Gender = "";
	public $SSN = "";
	public $MedicalConditions = "";
	public $ClinicianId = 0;
	
	//Given a query replace the appropriate field identifiers with their values from this object.
	// return the query with all the field identifiers completed.
	private function SetupQuery($query)
	{
		$needles = array("%Key", "%idNumber", "%Height", "%Weight", "%Gender", "%DOB", "%SSN", "%MedicalConditions", "%ClinicianId");
		$replace = array(Config::DB_KEY, $this->idNumber, $this->Height, $this->Weight, $this->Gender, $this->DOB, $this->SSN, $this->MedicalConditions, $this->ClinicianId);
		
		$query = str_replace($needles, $replace, $query);
		
		if ( Config::DAO_DEBUG_MODE == 1 ) print $query;
		
		return $query;		
	}
	
	// Insert the Patient's record into the database.
	// returns true on success.
	public function Insert()
	{
		if ( !parent::Insert() )
			return false;

		$conn = parent::GetConnection();

		$query = $this->SetupQuery(Query::INSERT_PATIENT);

		if ( !odbc_exec($conn, $query) )
			return $this->HandleError($conn);

		return true;		
	}
	
	//Update the record in the database to reflect the current values in this object.
	// returns true on success.
	public function Update()
	{
		if ( !parent::Update() )
			return false;
		
		$conn = parent::GetConnection();
 		
 		$this->RetrieveUserId();
 		
		$query = $this->SetupQuery(Query::UPDATE_PATIENT);

		if ( !odbc_exec($conn, $query) )
			return $this->HandleError($conn);
 				
 		return true;
	}
	
	//Read the current values from the database and update the this object accordingly.
	//The Username field of the class must be set in order for this to work.
	// returns true on success.
	public function Find()
	{
		
		if ( !parent::Find() )
			return false;	

		if ( $this->UserClass != "Patient" )
			return false;
		
		$conn = parent::GetConnection();
		
		$query = $this->SetupQuery(Query::FIND_PATIENT);
		
		$result = odbc_exec($conn, $query);
		
		if ( !$result )
			return $this->HandleError($conn);
			
		$row = array();
		if ( !odbc_fetch_into($result, $row) )
			return $this->HandleError($conn);

		//Copy the values from the single record returned.
		$this->Height = $row[0];
		$this->Weight = $row[1];
		$this->DOB = $row[2];
		$this->Gender = $row[3];
		$this->SSN = $row[4];
		$this->MedicalConditions = $row[5];
		$this->ClinicianId = $row[6];
		
		return true; 			
	}
	
 	
	//Retrive a list of DailyTaskDAO objects which could be any derived object of DailyTaskDAO
	//that represents this Patients ACTIVE daily task list.
	//The Username of the PatientDAO object must be set.
 	public function ListDailyTasks()
 	{
		$this->Find();

  		if ( $this->UserClass != "Patient" )
 			return array();
 		
 		$conn = parent::GetConnection();
 			
 		$query = $this->SetupQuery(Query::LIST_DAILY_TASKS);
 		
 		$taskArray = array();
 		
 		$result = odbc_exec($conn, $query);
 		
 		if ( !$result )
 		{
 			$this->HandleError();
 			return array();
 		}
 		
 		$row = array();
 		while ( odbc_fetch_into($result, $row) )
 		{
 			$className = $row[1]."DAO";
 			$taskArray[] = new $className();
 			
 			$i = count($taskArray) - 1;
 			
 			$taskArray[$i]->idDailyTask = $row[0];
 			$taskArray[$i]->Find();
 			
 		}
 		
 		return $taskArray;		
 	} 

 }
 
?>