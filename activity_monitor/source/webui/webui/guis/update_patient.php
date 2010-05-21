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
				<h2 class="welcome">Update Patient</h2>
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
				$p->Find();	
				?>
					
				<br />
				<form name="update patient" action="patient_update_script.php" method="post">
   				First name: <input type="text" name="firstname" value="<?php echo $p->FirstName;?>"/><br />
   				Middle name: <input type="text" name="middlename" value="<?php echo $p->MiddleName;?>"/><br />
   				Last name: <input type="text" name="lastname" value="<?php echo $p->LastName;?>"/><br />
   				Username: <input type="text" name="username" readonly value="<?php echo $p->Username;?>"/><br />
   				Password: <input type="text" name="password" value="<?php echo $p->Password;?>"/><br />
   				Address: <input type="text" name="address" value="<?php echo $p->Address;?>"/><br />
   				Phone Number: <input type="text" name="phone" value="<?php echo $p->PhoneNumber;?>"/><br />
   				Height: <input type="text" name="height" value="<?php echo $p->Height;?>"/><br />
   				Weight: <input type="text" name="weight" value="<?php echo $p->Weight;?>"/><br />
   				Gender: <select name="gender">
   					<option value="Male">Male</option>
   					<option value="Female">Female</option>
   				</select><br />
   				SSN: <input type="text" name="SSN" value="<?php echo $p->SSN;?>"/><br />
   				DOB: <input type="text" name="DOB" value="<?php echo $p->DOB;?>"/><br />
   				Medical Conditions: <textarea name="MedicalConditions" rows="5" cols="50"><?php echo $p->MedicalConditions;?></textarea>
   				<br /><br /><br />
   				   				
   				
   				<input type="submit" value="Submit">
   				</form>
   			
			</div>

		</div> <!-- End columnwrapper -->  
	</div> <!-- End bottomwrapper -->

	<div id="footer">   
       This is the Footer		
	</div>				 
</div> <!-- End fullwrapper -->
  
</body>
</html>