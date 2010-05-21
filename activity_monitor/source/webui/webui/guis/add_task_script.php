<?php session_start();

 // check to see if $_SESSION['dashboard_timeout'] is set
if(isset($_SESSION["timeout"]) ) {
        $session_life = time() - $_SESSION["timeout"];
        if($session_life > $_SESSION["inactive_time"])
        { session_destroy(); header("Location: logout.php?error=102"); }
}
$_SESSION["timeout"] = time();

if($_SESSION["loggedin"] == "")
{
 header("Location: logout.php?error=103");
}

/*
 * Created on Nov 12, 2009
 *
 * To change the template for this generated file go to
 * Window - Preferences - PHPeclipse - PHP - Code Templates
 */
require_once("../daos/db_include.php");

if($_POST["class"] == "WalkingTask")
{
	$Date = $_POST["date"];	
	$Steps = $_POST["steps"];
	
	$p = new PatientDAO();
	$p->Username = $_GET["patient"];
	$p->Find();
	
	$w = new WalkingTaskDAO();
	$w->Active = 1;
	$w->DailyTaskClass = "WalkingTask";
	$w->Date = $Date;
	$w->Steps = $Steps;
	$w->idPatient = $p->idNumber;
	
	$w->Insert();
}


$location = "Location: patient_display.php?patient=" . $_GET["patient"];
header($location);


?>
