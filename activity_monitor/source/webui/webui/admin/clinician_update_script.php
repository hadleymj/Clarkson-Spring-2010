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
//UserDAO DB fields
$Username = $_POST["username"];
$Password = $_POST["password"];
$FirstName = $_POST["firstname"];
$MiddleName = $_POST["middlename"];
$LastName = $_POST["lastname"];
$Address = $_POST["address"];
$PhoneNumber = $_POST["phone"];
$UserClass = "Clinician";

$u = new UserDAO;
$u->Username = $Username;
$u->Password = $Password;
$u->FirstName = $FirstName;
$u->MiddleName = $MiddleName;
$u->LastName = $LastName;
$u->Address = $Address;
$u->PhoneNumber = $PhoneNumber;
$u->UserClass = $UserClass;

$u->Update();

header("Location: clinicians.php");
/*
$worked = $p->Insert();
if ($worked)
{
	header("Location: homepage.php?success=101");
}
else
{
	header("Location: add_patient.php?error=101");
}
*/
?>