<?php 
if($_POST["dailyTaskClass"] == "")
{
	$location = "Location: add_task_choose.php?patient=" . $_GET["patient"];
	header($location);	
}
session_start();
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
<title>Add Task</title>
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
   			<h2 class="welcome">Edit Task</h2>
   			<div id="content1">
   			<?php
   			
   			
   			$p = new PatientDAO();
   			$p->Username = $_GET["patient"];
   			$p->Find();
   			
   			if($_POST["dailyTaskClass"] == "WalkingTask")
   			{
   				
   			?>
   			<h3 class="welcome">Walking Task</h3>
   			<form name="add_task" action="add_task_script.php?patient=<?php echo $_GET["patient"];?>" method="post">
   				Patient: <?php echo $p->FirstName . " " . $p->MiddleName . " " . $p->LastName;?><br />
   				Task: <input type="text" readonly name="class" value="<?php echo $_POST["dailyTaskClass"]?>"/><br />
   				Date: <input type="text" name="date"/><br />
   				Steps: <input type="text" name="steps"/><br />
   				<br /> 				   				
   				
   				<input type="submit" value="Submit">
   				</form>
   			
   			<?php 
   			}
   			?>
   			
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