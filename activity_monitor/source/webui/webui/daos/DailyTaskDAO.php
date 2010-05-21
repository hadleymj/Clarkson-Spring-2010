<?php
/*
 * Created on Nov 3, 2009
 *
 * To change the template for this generated file go to
 * Window - Preferences - PHPeclipse - PHP - Code Templates
 */
 
//require_once("DAO.php");
require_once("db_include.php");


/*
 * DailyTaskDAO extends UserDAO
 * 		GetConnection()
 * 		ListPatients() //Invalid because the calling object is not a Clinician Class.
 * 
 * Members:
 * 	public $idDailyTask = -1;
 	public $idPatient = -1;
 	public $DailyTaskClass = "";
 	public $Date = "19000101";
 	public $Active = 0; 
 * 
 * Methods (public):
 * 		Insert()
 * 				//Insert a new record into the database where the values of the record are the values of this object.
 				// returns true on success.
 				//* This will update the idDailyTask field with the id of the record once it has been inserted.
				 * 
 * 		Find()
 * 				//If the record exists in the database set the fields of this object equal to the values from the db.
 				// returns true on success.
 				 *  		
 * 		Update()
 * 			 	//Update the record in the database to reflect the current values in this object.
				// returns true on success.
 				 * 
 *		Remove()
				//Remove the record represented by this object from the database.
			 	//returns true on success.
			 	 * 
 * 		GetStatistics($Date)
 *			 	//Get the Statistics object associated with this Daily task at the Date specified.
			 	//This will return a single Statistics object or null if the function failed. 
			 	//Note: The DailyTaskDAO this is being
			 	//called from must have the DailyTaskClass and idDailyTask fields populated.
			 	 *  
 * 		ListStatistics($StartDateMin, $StartDateMax, $EndDateMin, $EndDateMax)
 * 				//Get the Statistics objects associated with this Daily task over the range specified.
 				//This will return an array of Statistics objects or null if the function failed.
 				//Note: The DailyTaskDAO this is being
 				//called from must have the DailyTaskClass and idDailyTask fields populated.
 				 * 
 * 
 */ 

 class DailyTaskDAO extends DAO
 {
 	
 	public $idDailyTask = -1;
 	public $idPatient = -1;
 	public $DailyTaskClass = "";
 	public $Date = "19000101";
 	public $Active = 0;
 	
 	
	//Given a query replace the appropriate field identifiers with their values from this object.
	// return the query with all the field identifiers completed.
 	private function SetupQuery($query)
 	{
 		$needles = array("%Key", "%idDailyTask", "%idPatient", "%DailyTaskClass", "%Date", "%Active");
		$replace = array(Config::DB_KEY, $this->idDailyTask, $this->idPatient, $this->DailyTaskClass, $this->Date, $this->Active);
		
		$query = str_replace($needles, $replace, $query);
		
		if ( Config::DAO_DEBUG_MODE == 1 ) print $query;
		
		return $query;
 		
 	}
 	
 	//Insert a new record into the database where the values of the record are the values of this object.
 	// returns true on success.
 	//* This will update the idDailyTask field with the id of the record once it has been inserted.
 	public function Insert()
 	{
		$conn = parent::GetConnection();
		
		$query = $this->SetupQuery(Query::INSERT_DAILY_TASK);
				
		if ( !odbc_exec($conn, $query) )
			return $this->HandleError();
		
		//Obtain the ID of the record that was insert last.
		$query = Query::LAST_INSERT_ID;
		if ( !($result = odbc_exec($conn, $query)) )
			return $this->HandleError();
			
		$row = array();
		if ( !odbc_fetch_into($result, $row) )
			return $this->HandleError();
			
		$this->idDailyTask = $row[0];

		return true; 		
 	}
 	
 	//If the record exists in the database set the fields of this object equal to the values from the db.
 	// returns true on success.
 	public function Find()
 	{
		$conn = parent::GetConnection();
		
		$query = $this->SetupQuery(Query::FIND_DAILY_TASK);
		
		$result = odbc_exec($conn, $query);
		
		if ( !$result )
			return $this->HandleError();
			
		$row = array();
		if ( !odbc_fetch_into($result, $row) )
			return $this->HandleError();

		$this->idPatient = $row[0];
		$this->DailyTaskClass = $row[1];
		$this->Date = $row[2];
		$this->Active = $row[3];
		
		return true; 	 		
 	}
 	
 	//Update the record in the database to reflect the current values in this object.
	// returns true on success.
 	public function Update()
 	{
 		$conn = parent::GetConnection();
 		
 		$query = $this->SetupQuery(Query::UPDATE_DAILY_TASK);
 			
		if ( !odbc_exec($conn, $query) )
			return $this->HandleError();
 				
 		return true;  		
 		
 	}
 	
 	//Remove the record represented by this object from the database.
 	//returns true on success.
 	public function Remove()
 	{
 		$conn = parent::GetConnection();
 		
 		$query = $this->SetupQuery(Query::REMOVE_DAILY_TASK);
 		
		if ( !odbc_exec($conn, $query) )
			return $this->HandleError();
 				
 		return true;  		
 	}
 	
 	//Get the Statistics object associated with this Daily task at the Date specified.
 	//This will return a single Statistics object or null if the function failed. 
 	//Note: The DailyTaskDAO this is being
 	//called from must have the DailyTaskClass and idDailyTask fields populated.
 	public function GetStatistics($Date)
 	{

		$StatsClass = substr($this->DailyTaskClass, 0, strlen($this->DailyTaskClass)-4)."Stats";

		print("\nStatsClass=".$StatsClass."\n");

		$conn = parent::GetConnection();
		
		$query = Query::FIND_STATS_EXACT;
		
		$needles = array("%Key", "%idDailyTask", "%StatsClass", "%Date");
		$replace = array(Config::DB_KEY, $this->idDailyTask, $StatsClass, $Date);
		
		$query = str_replace($needles, $replace, $query);
		
		if ( Config::DAO_DEBUG_MODE == 1 ) print $query;
		
		$result = odbc_exec($conn, $query);
		
		if ( !$result )
		{
			$this->HandleError($conn);
			return null;
		}
			
		$row = array();
		if ( !odbc_fetch_into($result, $row) )
		{
			$this->HandleError($conn);
			return null;
		}
			
		$className = $StatsClass."DAO";
 		$stats = new $className();
 			
 		$stats->idStatistics = $row[0];
 		$stats->Find();
	
		return $stats;
 		
 	}
 	
 	//Get the Statistics objects associated with this Daily task over the range specified.
 	//This will return an array of Statistics objects or null if the function failed.
 	//Note: The DailyTaskDAO this is being
 	//called from must have the DailyTaskClass and idDailyTask fields populated.
 	public function ListStatistics($StartDateMin, $StartDateMax, $EndDateMin, $EndDateMax)
 	{
		$StatsClass = substr($this->DailyTaskClass, 0, strlen($this->DailyTaskClass)-4)."Stats";

		$conn = parent::GetConnection();
		
		$query = Query::FIND_STATS_RANGE;
		
		$needles = array("%Key", "%idDailyTask", "%StatsClass", "%StartDateMin", "%StartDateMax", "%EndDateMin", "%EndDateMax");
		$replace = array(Config::DB_KEY, $this->idDailyTask, $StatsClass, $StartDateMin, $StartDateMax, $EndDateMin, $EndDateMax);
		
		$query = str_replace($needles, $replace, $query);
		
		if ( Config::DAO_DEBUG_MODE == 1 ) print $query;
		
		$result = odbc_exec($conn, $query);
		
		if ( !$result )
		{
			$this->HandleError();
			return null;
		}
			
		$row = array();
		$statsArray = array();
		$className = $StatsClass."DAO";
		
		while ( odbc_fetch_into($result, $row) )
		{
			
 			$statsArray[] = new $className();
 			
 			$i = count($statsArray) - 1;
 			
 			$statsArray[$i]->idStatistics = $row[0];
 			$statsArray[$i]->Find();
			
		}

		return $statsArray; 	 		
 	}

 }
?>
