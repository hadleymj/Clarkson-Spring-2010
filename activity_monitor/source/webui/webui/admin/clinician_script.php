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

if($Username == "" || $Password == "" || $FirstName == "" || $MiddleName == "" 
	|| $LastName == "" || $Address == "" || $PhoneNumber == "")
{header("Location: add_clinician.php?error=102");}


$u = new UserDAO;
$u->Username = $Username;
if($u->Find())
{header("Location: add_clinician.php?error=103");}
$u->Password = $Password;
$u->FirstName = $FirstName;
$u->MiddleName = $MiddleName;
$u->LastName = $LastName;
$u->Address = $Address;
$u->PhoneNumber = $PhoneNumber;
$u->UserClass = $UserClass;

$u->Insert();

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