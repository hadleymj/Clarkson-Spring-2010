<?php

session_start();
session_unset();
session_destroy();

if($_GET["error"] == 102)
{
	header("Location: index.php?error=102");
}
else if($_GET["error"] == 103)
{
	header("Location: index.php?error=103");
}
else
{
	header("Location: index.php");
}
?>
