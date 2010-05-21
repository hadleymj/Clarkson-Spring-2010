<?php
/*
 * Created on Nov 3, 2009
 *
 * To change the template for this generated file go to
 * Window - Preferences - PHPeclipse - PHP - Code Templates
 */
 
//require_once("DAO.php");
require_once("db_include.php");
 
 class WalkingStatsDAO extends StatsDAO
 {
 	
 	public $Steps = 0;
 	
 	
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
 		if ( !parent::Find() )
			return false;	

		if ( $this->StatsClass != "WalkingStats" )
			return false;
 		
 		$conn = parent::GetConnection();
		
		$query = $this->SetupQuery(Query::FIND_WALKING_STATS);
		
		$result = odbc_exec($conn, $query);
		
		if ( !$result )
			return $this->HandleError();
			
		$row = array();
		if ( !odbc_fetch_into($result, $row) )
			return $this->HandleError();

		$this->Steps = $row[0];
		
		return true; 
 	}
 
 }
?>