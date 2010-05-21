<?php
/*
 * Created on Dec 3, 2009
 *
 * To change the template for this generated file go to
 * Window - Preferences - PHPeclipse - PHP - Code Templates
 */
 
 require_once("../daos/db_include.php");
 $t = new DailyTaskDAO;
 $t->idDailyTask = $_GET["task"];
 $t->Find();
 
 $t->Active = 0;
 $t->Update();
 
 $location = "Location: patient_display.php?patient=" . $_GET["patient"];
 header($location);
?>
