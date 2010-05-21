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
<title>Update Clinician</title>
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
	  			<a href="clinicians.php">List Clinicians</a><br />
	  			<a href="add_clinician.php">Add Clinician</a><br />
	  			<a href="logout.php">Logout</a>
    		</div>
        </div>
        
        <div id="columnwrapper">
 	   
 		  	<div id="breadcrumbs">
 		  	
  			</div>
  
   			<div id="content">
				<h2 class="welcome">Update Clinician</h2>
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
				
				$u = new UserDAO();
				$u->Username = $_GET["clinician"];
				$u->Find();	
				?>
					
				<br />
				<form name="update clinician" action="clinician_update_script.php" method="post">
   				First name: <input type="text" name="firstname" value="<?php echo $p->FirstName;?>"/><br />
   				Middle name: <input type="text" name="middlename" value="<?php echo $p->MiddleName;?>"/><br />
   				Last name: <input type="text" name="lastname" value="<?php echo $p->LastName;?>"/><br />
   				Username: <input type="text" name="username" readonly value="<?php echo $p->Username;?>"/><br />
   				Password: <input type="text" name="password" value="<?php echo $p->Password;?>"/><br />
   				Address: <input type="text" name="address" value="<?php echo $p->Address;?>"/><br />
   				Phone Number: <input type="text" name="phone" value="<?php echo $p->PhoneNumber;?>"/><br /><br />
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