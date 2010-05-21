<?php
require_once("../daos/db_include.php");
$username = $_POST["username"];
$password = $_POST["password"];

$u = new UserDAO;
$u->Username = $username;
$u->Find();

if($u->Password == $password && $u->UserClass == "Admin")
{
	session_start();
	$_SESSION["username"] = $username;
	$_SESSION["loggedin"] = TRUE;
	$_SESSION["inactive_time"] = 600;//Timeout Time in Seconds
	header("Location: homepage.php");
}
else//redirect back to login with error
{
	ob_end_clean();
	header("Location: index.php?error=101");
}
?>