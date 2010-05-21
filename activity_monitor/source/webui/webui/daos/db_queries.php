<?php
/*
 * Created on Nov 11, 2009
 *
 * To change the template for this generated file go to
 * Window - Preferences - PHPeclipse - PHP - Code Templates
 */

class Query
{
	//////////////////////////////////////////
	//Queries for the UserDAO object.
	//+++++++++++++++
	
	//Replace: %Username, %Password, %FirstName, %MiddleName, %LastName, %Address, %PhoneNumber, %UserClass, %Key
	const INSERT_USER = "insert into User (Username, Password, FirstName, MiddleName, LastName, Address, PhoneNumber, UserClass) values ('%Username', AES_ENCRYPT('%Password', '%Key'), AES_ENCRYPT('%FirstName', '%Key'), AES_ENCRYPT('%MiddleName', '%Key'), AES_ENCRYPT('%LastName', '%Key'), AES_ENCRYPT('%Address', '%Key'), AES_ENCRYPT('%PhoneNumber', '%Key'), '%UserClass')";
	
	//Replace: %Username, %Key
	const FIND_USER = "select idNumber, UserClass, AES_DECRYPT(Password, '%Key'), AES_DECRYPT(FirstName, '%Key'), AES_DECRYPT(MiddleName, '%Key'), AES_DECRYPT(LastName, '%Key'), AES_DECRYPT(Address, '%Key'), AES_DECRYPT(PhoneNumber, '%Key') from User where Username='%Username'";	
	
	//Replace: %Username, %Password, %FirstName, %MiddleName, %LastName, %Address, %PhoneNumber, %UserClass, %Key
	const UPDATE_USER = "update User set Password=AES_ENCRYPT('%Password', '%Key'), UserClass='%UserClass', FirstName=AES_ENCRYPT('%FirstName', '%Key'), MiddleName=AES_ENCRYPT('%MiddleName', '%Key'), LastName=AES_ENCRYPT('%LastName', '%Key'), Address=AES_ENCRYPT('%Address', '%Key'), PhoneNumber=AES_ENCRYPT('%PhoneNumber', '%Key') where Username='%Username'";

	//Replace: %idNumber
	const LIST_PATIENTS = "select Username, UserClass from Patient, user where ClinicianId=%idNumber and User.idNumber = Patient.idNumber";	

	
	///////////////////////////////////////////
	
	
	
	///////////////////////////////////////////
	//Queries for the PatientDAO object.
	//+++++++++++++++++
	
	//Replace: %idNumber, %Height, %Weight, %Gender, %DOB, %SSN, %MedicalConditions, %ClinicianId, %Key
	const INSERT_PATIENT = "insert into Patient (idNumber, Height, Weight, Gender, DOB, SSN, MedicalConditions, ClinicianId ) values (%idNumber, %Height, %Weight, '%Gender', '%DOB', AES_ENCRYPT('%SSN', '%Key'), '%MedicalConditions', %ClinicianId)";
	
	//Replace: %idNumber, %Height, %Weight, %Gender, %DOB, %SSN, %MedicalConditions, %ClinicianId, %Key
	const UPDATE_PATIENT = "update Patient set Height=%Height, Weight=%Weight, Gender='%Gender', DOB='%DOB', MedicalConditions='%MedicalConditions', SSN=AES_ENCRYPT('%SSN', '%Key'), ClinicianId=%ClinicianId where idNumber=%idNumber";
	
	//Replace: %idNumber, %Key
	const FIND_PATIENT ="select Height, Weight, DOB, Gender, AES_DECRYPT(SSN, '%Key'), MedicalConditions, ClinicianId from Patient where idNumber=%idNumber";

	//Replace: %idNumber
	const LIST_DAILY_TASKS = "select DailyTask.idDailyTask, DailyTask.DailyTaskClass from DailyTask, WalkingTask where idPatient=%idNumber and DailyTask.idDailyTask = WalkingTask.idDailyTask and Active=true"; 


	//////////////////////////////////////////



	///////////////////////////////////////////
	//Queries for the DailyTaskDAO
	//++++++++++++++++++
	
	//Replace: %idPatient, %DailyTaskClass, %Date, %Active
	const INSERT_DAILY_TASK = "insert into DailyTask (idPatient, DailyTaskClass, Date, Active) values (%idPatient, '%DailyTaskClass', '%Date', %Active)";

	//Replace: %idDailyTask
	const FIND_DAILY_TASK = "select idPatient, DailyTaskClass, Date, Active from DailyTask where idDailyTask='%idDailyTask'";

	//Replace: %idPatient, %DailyTaskClass, %Date, %Active, %idDailyTask	
	const UPDATE_DAILY_TASK = "update DailyTask set idPatient=%idPatient, DailyTaskClass='%DailyTaskClass', Date='%Date', Active=%Active where idDailyTask=%idDailyTask";

	//Replace: %idDailyTask
	const REMOVE_DAILY_TASK = "delete from DailyTask where idDailyTask=%idDailyTask";

	//Replace: %idDailyTask, %StatsClass, %StartDateMin, %StartDateMax, %EndDateMin, %EndDateMax
	const FIND_STATS_RANGE = "select idStatistics from Statistics where StatsClass='%StatsClass' and idDailyTask=%idDailyTask and StartDateTime between CONVERT('%StartDateMin', DATETIME) and CONVERT('%StartDateMax', DATETIME) and EndDateTime between CONVERT('%EndDateMin', DATETIME) and CONVERT('%EndDateMax', DATETIME)";
	
	//Replace: %idDailyTask, %StatsClass, %Date
	const FIND_STATS_EXACT = "select idStatistics from Statistics where StatsClass='%StatsClass' and idDailyTask=%idDailyTask and StartDateTime <= CONVERT('%Date', DATETIME) and CONVERT('%Date', DATETIME) <= EndDateTime";


	
	///////////////////////////////////////////
	
	
	
	///////////////////////////////////////////
	//Queries for the WalkingTaskDAO
	//+++++++++++++++++++
	
	//Replace: %idDailyTask, %Steps
	const INSERT_WALKING_TASK = "insert into WalkingTask (idDailyTask, Steps) values (%idDailyTask, %Steps)";
	
	//Replace: %idDailyTask
	const FIND_WALKING_TASK = "select Steps from WalkingTask where idDailyTask=%idDailyTask";
	
	//Replace: %idDailyTask, %idSteps
	const UPDATE_WALKING_TASK = "update WalkingTask set Steps=%Steps where idDailyTask=%idDailyTask";

	//Replace: %idDailyTask
	const REMOVE_WALKING_TASK = "delete from WalkingTask where idDailyTask=%idDailyTask";

	////////////////////////////////////////////
		
	
	///////////////////////////////////////////
	//Queries for the StatsDAO
	//+++++++++++++++++++
	
	//Replace: %idStatistics
	const FIND_STATISTICS = "select idDailyTask, StatsClass, StartDateTime, EndDateTime from Statistics where idStatistics = %idStatistics";
	
	////////////////////////////////////////////
		
	
	///////////////////////////////////////////
	//Queries for the StatsDAO
	//+++++++++++++++++++	
	
	//Replace: %idStatistics
	const FIND_WALKING_STATS = "select Steps from WalkingStats where idStatistics = %idStatistics";
	
	////////////////////////////////////////////
	
		
	///////////////////////////////////////////
	//Misc Queries that may be used by all objects.
	//+++++++++++++++++
	
	
	const LAST_INSERT_ID = "select LAST_INSERT_ID()";
	
	////////////////////////////////////////////
					
}

?>
