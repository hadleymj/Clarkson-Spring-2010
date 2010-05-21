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
<script src="../scripts/sorttable.js"></script>
<title>List Patients</title>
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

   			<p>
   			<h2 class="welcome">Patient List</h2>
   				
   			<!-- Table of patient results -->
   			
   			<?php
   			$u = new UserDAO();
   			//$p = new PatientDAO();
   			$u->Username = $_SESSION["username"];
   			$u->Find();
   			$patientArray = $u->ListPatients();
   			?>
   			<p>
   			<?php echo "Click a Column Heading to sort by that Field.";?>
   			</p>
   			
   				<table class="sortable" border="1">
   				<tr>
	   				<th>Last Name</th>
	   				<th>First Name</th>
	   				<th>Middle Name</th>
	   				<th>Address</th>
	   				<th>Phone Number</th>
	   				<th>ID Number</th>
	   				<th>Username</th>
				</tr>
   			<?php
   			
   			foreach($patientArray as $item)
   			{   				
   			?>
   					<tr>
   					<td><?php echo $item->LastName;?></td>
   					<td><?php echo $item->FirstName;?></td>
   					<td><?php echo $item->MiddleName;?></td>
   					<td><?php echo $item->Address;?></td>
   					<td><?php echo $item->PhoneNumber;?></td>
   					<td><?php echo $item->idNumber;?></td>
   					<td><a href="patient_display.php?patient=<?php echo $item->Username;?>"><?php echo $item->Username;?></a></td>
   					</tr>
   			<?php
   			}		
   			?>
   				</table>
			</div>

		</div> <!-- End columnwrapper -->  
	</div> <!-- End bottomwrapper -->

	<div id="footer">   
       This is the Footer		
	</div>				 
</div> <!-- End fullwrapper -->
  
</body>
</html>