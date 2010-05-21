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
$UserClass = "Patient";

//PatientDAO DB fields
$Height = $_POST["height"];
$Weight = $_POST["weight"];
$DOB = $_POST["DOB"];
$Gender = $_POST["gender"];
$SSN = $_POST["SSN"];
$MedicalConditions = $_POST["MedicalConditions"];

if($Username == "" || $Password == "" || $FirstName == "" || $MiddleName == "" 
	|| $LastName == "" || $Address == "" || $PhoneNumber == "" || $Height == "" 
	|| $Weight == "" || $DOB == "" || $Gender == "" || $SSN == ""
	|| $MedicalConditions == "")
{header("Location: add_patient.php?error=102");}


$u = new UserDAO;
$u->Username = $_SESSION["username"];
$u->Find();
$ClinicianId = $u->idNumber;

$p = new PatientDAO;
$p->Username = $Username;
if($p->Find())
{header("Location: add_patient.php?error=103");}
$p->Password = $Password;
$p->FirstName = $FirstName;
$p->MiddleName = $MiddleName;
$p->LastName = $LastName;
$p->Address = $Address;
$p->PhoneNumber = $PhoneNumber;
$p->UserClass = $UserClass;

$p->Height = $Height;
$p->Weight = $Weight;
$p->DOB = $DOB;
$p->Gender = $Gender;
$p->SSN = $SSN;
$p->MedicalConditions = $MedicalConditions;
$p->ClinicianId = $ClinicianId;

$p->Insert();

header("Location: patients.php");
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
