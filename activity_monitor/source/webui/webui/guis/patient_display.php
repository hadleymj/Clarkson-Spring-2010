<?php session_start();
require_once("../daos/db_include.php");
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

?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Add Patient</title>
<link rel="stylesheet" type="text/css" href="styles.css" />
</head>

<body>
  
<div id="fullwrapper">
           
	<div id="header">		 
		<h1>Activity Monitor</h1>			 
	</div>
  
	<div id="bottomwrapper"> 
      
      	<div id="columnwrapper0">
    		<div id="leftminibar"></div>
    		<div id="leftcolumn"> 
    			<font size=5><u>Menu</u></font><br /><br />
    			<a href="homepage.php">Home</a><br />
	  			<a href="patients.php">List Patients</a><br />
	  			<a href="add_patient.php">Add Patient</a><br />
	  			<a href="charts.php">Charts</a><br />
				<a href="stats.php">Stats Tables</a><br />
	  			<a href="logout.php">Logout</a>
    		</div>
        </div>
        
        <div id="columnwrapper">
 	   
 		  	<div id="breadcrumbs">
			
  			</div>
  
   			<div id="content_container">
   			<h2 class="welcome">Patient Profile</h2>
   			<div id="content1">
   			
   			<?php 
   				$p = new PatientDAO();
   				$p->Username =  $_GET["patient"];
   				$p->Find();
   			?>
   			Name: <?php echo $p->FirstName . " " . $p->MiddleName . " " . $p->LastName . "<br />";  ?>
   			Username: <?php echo $p->Username . "<br />"; ?>
   			Address: <?php echo $p->Address . "<br />"; ?>
   			Phone Number: <?php echo $p->PhoneNumber . "<br />"; ?>
   			Height: <?php echo $p->Height . "<br />"; ?>
   			Weight: <?php echo $p->Weight . "<br />"; ?>
   			Date of Birth: <?php echo $p->DOB . "<br />"; ?>
   			SSN: <?php echo $p->SSN . "<br />"; ?>
   			Medical Conditions: <?php echo $p->MedicalConditions . "<br /><br />"; ?>
   			<a href="update_patient.php?patient=<?php echo $p->Username;?>">Update Patient Information</a>
			</div>
			
			<div id="content2">
			<h3>Daily Tasks</h3>
   			
   			<?php
   			$i = 1;
   			$tasks = $p->ListDailyTasks();
   			foreach($tasks as $item)
   			{
   				if($item->Active)
   				{
	   				if($item->DailyTaskClass == "WalkingTask")
	   				{
	   					echo $i . ". " . $item->Date . " Walk " . $item->Steps . " steps. ";
	   					?>&nbsp;<a href="edit_task.php?task=<?php echo $item->idDailyTask;?>&patient=<?php echo $_GET["patient"];?>">Edit</a>&nbsp;&nbsp;
	   					<a href="remove_task.php?task=<?php echo $item->idDailyTask;?>&patient=<?php echo $_GET["patient"];?>">Remove</a>
	   					<?php echo "<br /><br />";
	   				}
   				}
   				$i = $i + 1;
   			}
   			?>
			<a href="add_task_choose.php?patient=<?php echo $_GET["patient"];?>">Add New Task</a>   		
			</div>
			
			</div>

		</div> <!-- End columnwrapper -->  
	</div> <!-- End bottomwrapper -->

	<div id="footer">   
       This is the Footer		
	</div>				 
</div> <!-- End fullwrapper -->
  
</body>
</html>