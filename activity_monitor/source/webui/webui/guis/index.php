<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
<title>Login!</title>
<link rel="stylesheet" type="text/css" href="index.css" />
</head>

<body>


<div id="loginpage">
           
	<div id="loginform">	 
		<h1>Activity Monitor Login</h1>			 

			<?php
				if($_GET['error'] == 101)
				{
			?>
				<table class="error" bgcolor="red">
				<tr>
				<td size=>Username and/or Password is incorrect. Please try again.</td>
				</tr>
				</table>
			<?php
				}
			?>
			
			<?php
				if($_GET["error"] == 102)
				{
			?>
				<table class="error" bgcolor="red">
				<tr>
				<td size=>You have been logged out due to inactivity.</td>
				</tr>
				</table>
			<?php
				}
			?>
			
			<?php
				if($_GET["error"] == 103)
				{
			?>
				<table class="error" bgcolor="red">
				<tr>
				<td size=>You must be logged in to do that!</td>
				</tr>
				</table>
			<?php
				}
			?>
			
			<form name="login" action="login.php" method="post">
			Username: <input type="text" name="username" /><br />
			Password: <input type="password" name="password" /><br />
			<input type="submit" name="submit" />
			</form>   
   			
 			</div>
	
	</div>
			 
</div> 
  
</body>
</html>