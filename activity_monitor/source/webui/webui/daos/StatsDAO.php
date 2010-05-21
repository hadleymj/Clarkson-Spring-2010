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
 * StatsDAO extends DAO
 * 		GetConnection()
 * 
 * Members:
 * 	public $idStatistics = -1;
 	public $idDailyTask = -1;
 	public $StatsClass = "";
 	public $StartDate = "19000101";
 	public $EndDate = "19000101"; 
 * 
 * Methods (public):
 * 		Do not call or instantiate this object directly. 
 * 		Instead use the DailyTaskDAO.GetStatistics or DailyTaskDAO.ListStatistics functions.
 * 
 */
 
 class StatsDAO extends DAO
 {
 	
 	public $idStatistics = -1;
 	public $idDailyTask = -1;
 	public $StatsClass = "";
 	public $StartDate = "19000101";
 	public $EndDate = "19000101";
 	
 	
	//Given a query replace the appropriate field identifiers with their values from this object.
	// return the query with all the field identifiers completed.
 	private function SetupQuery($query)
 	{
 		$needles = array("%Key", "%idStatistics");
		$replace = array(Config::DB_KEY, $this->idStatistics);
		
		$query = str_replace($needles, $replace, $query);
		
		if ( Config::DAO_DEBUG_MODE == 1 ) print $query;
		
		return $query;
 		
 	}
 	
 	public function Find()
 	{
 		$conn = parent::GetConnection();
		
		$query = $this->SetupQuery(Query::FIND_STATISTICS);
		
		$result = odbc_exec($conn, $query);
		
		if ( !$result )
			return $this->HandleError();
			
		$row = array();
		if ( !odbc_fetch_into($result, $row) )
			return $this->HandleError();

		$this->idDailyTask = $row[0];
		$this->StatsClass = $row[1];
		$this->StartDate = $row[2];
		$this->EndDate = $row[3];
		
		return true; 
 	}
 
 }
?>