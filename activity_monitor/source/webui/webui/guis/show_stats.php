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
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Update Patient</title>
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
  
   			<div id="content">
				<h2 class="welcome"><?php echo $_GET["patient"] , ": ";?> Stats </h2>
				<br />
			<?php
				if($_GET["error"] == 101)
				{
			?>
				<table class="error" bgcolor="red">
				<tr>
				<td size=>User was not updated.</td>
				</tr>
				</table>
			<?php
				}
			?>
				
				<?php
				
				$p = new PatientDAO();
				$p->Username = $_GET["patient"];
				//$p->Find();	
				$dailytaskArray = $p->ListDailyTasks();
						
				
				?>
				<p>
				<?php echo "Click a Column Heading to sort by that Field.";?>
   			</p>
   			
				<table class="sortable" border="1">
   				<tr>
	   				<th>Date</th>
					<th>Steps</th>
	   			</tr>
				<?php
				foreach($dailytaskArray as $task)
				{
				?>
					<tr>
					<td><?php echo $task->Date; ?></td>
					<td><?php echo $task->Steps; ?></td>
					
				</tr>
				<?php
				}
				?>
				
				
				</table>
					
				<br />
				
   						
				
   			
			</div>

		</div> <!-- End columnwrapper -->  
	</div> <!-- End bottomwrapper -->

	<div id="footer">   
       This is the Footer		
	</div>				 
</div> <!-- End fullwrapper -->
  
</body>
</html>