<?php
/*
 * Created on Nov 3, 2009
 *
 * To change the template for this generated file go to
 * Window - Preferences - PHPeclipse - PHP - Code Templates
 */
 
//require_once("DailyTaskDAO.php");
require_once("db_include.php");
 
 class WalkingTaskDAO extends DailyTaskDAO
 {
 	
 	public $Steps = 0;
 	
	//Given a query replace the appropriate field identifiers with their values from this object.
	// return the query with all the field identifiers completed.
 	private function SetupQuery($query)
 	{
 		$needles = array("%Steps", "%idDailyTask");
		$replace = array($this->Steps, $this->idDailyTask);
		
		$query = str_replace($needles, $replace, $query);
		
		if ( Config::DAO_DEBUG_MODE == 1 ) print $query;
		
		return $query;
 		
 	}
 	
 	//Insert the WalkingTaskDAO record into the database.
 	// returns true on success.
 	public function Insert()
 	{
		if ( !parent::Insert() )
			return false;

		$conn = parent::GetConnection();

		$query = $this->SetupQuery(Query::INSERT_WALKING_TASK);
				
		if ( !odbc_exec($conn, $query) )
			return false;

		return true; 		
 	}
 	
 	//Update the fields of this object with the values in the database if the record exists.
 	//The search key is the idDailyTask field which must not be null.
 	// returns true if the object was found.
 	public function Find()
 	{
		if ( !parent::Find() )
			return false;	

		if ( $this->DailyTaskClass != "WalkingTask" )
			return false;
		
		$conn = parent::GetConnection();
		
		$query = $this->SetupQuery(Query::FIND_WALKING_TASK);
		
		$result = odbc_exec($conn, $query);
		
		if ( !$result )
			return false;
		
		$row = array();	
		if ( !odbc_fetch_into($result, $row) )
			return false;

		$this->Steps = $row[0];
		
		return true; 		
 	}
 	
 	//Set the values in the database to the fields of this object.
 	// returns true on success.
 	public function Update()
 	{
		if ( !parent::Update() )
			return false;
		
		$conn = parent::GetConnection();
 		
 		$query = $this->SetupQuery(Query::UPDATE_WALKING_TASK);
 			
		if ( !odbc_exec($conn, $query) )
			return false;
 				
 		return true; 		
 	}
 	
 	//Remove the record from the database identified by this object.
 	//Returns true on success.
 	public function Remove()
 	{ 		
 		$conn = parent::GetConnection();
 		
 		$query = $this->SetupQuery(Query::REMOVE_WALKING_TASK);
 		
		if ( !odbc_exec($conn, $query) )
			return false;
 		
 		return parent::Remove();  		
 	}
 	
 }
 
?>
