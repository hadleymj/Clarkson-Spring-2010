<?php
/*
 * Created on Nov 3, 2009
 *
 * To change the template for this generated file go to
 * Window - Preferences - PHPeclipse - PHP - Code Templates
 */
 
 
 require_once("./daos/db_include.php");

//THIS SECTION IS TO TEST THE TASKS DAOS!
/////////////////////////////////////////
/*
//List Daily tasks for a patient.
$d = new WalkingTaskDAO;
$u = new PatientDAO;
$u->Username = "bh673";

//THIS WAS REVISED. No longer need a WalkingTaskDAO to get
//a list of daily tasks for a patient.
$ls = $u->ListDailyTasks();
print_r($ls);

//Insert a new walking task.
$e = new WalkingTaskDAO;
$e->Active = 1;
$e->Date = "20091010";
$e->Steps = 1500;
$e->DailyTaskClass = "WalkingTask";
$e->idPatient = 4;
$e->Insert();

//Update an existing wakling task.
$f = new WalkingTaskDAO;
$f->idDailyTask = $e->idDailyTask;
$f->Find();
print_r($f);
$f->Active = 0;
$f->Steps = 2000;
$f->Update();

//Remove a walking task.
$g = new WalkingTaskDAO;
$g->idDailyTask = $f->idDailyTask;
$g->Remove();


//THIS SECTION IS TO TEST THE USER DAOS!
/////////////////////////////////////////

//Find a user and check its password.
$u = new UserDAO;

$u->Username = "ryan";

print $u->Find() . "<br/>\n";
print $u->Password . "<br/>\n";


//Insert a new user.
$p = new UserDAO;

$p->Username = "sam";
$p->Password = "pass";
$p->UserClass = "Clinician";
print $p->Insert() . "<br/>\n";


//Update an existing user.
$q = new UserDAO;

$q->Username = "sam";
$q->Find();

$q->FirstName = "Sam";
$q->MiddleName = "Phil";
$q->LastName = "Smith";

print $q->Update() . "<br/>\n";


//List Patients for a Clinician
$c = new UserDAO;
$c->Username = "ryan";
$c->Find();

///THIS WAS REVISED. 
//No longer need a PatientDAO to get a list of patients.
print_r($c->ListPatients());


//Get the data fora  patient.
$w = new PatientDAO;
$w->Username = "bh673";

print "Patient Found: " . $w->Find() . "<br/>\n";

print_r($w);
print "<br/>\n";


//Insert a patient
$d = new PatientDAO;
$d->Username = "n p";
$d->Height = 105;
$d->ClinicianId = 1;
$d->Gender = "male";
$d->UserClass = "Patient";

print "Patient Insert: " . $d->Insert() . "<br/>\n";


//Update a patient
$f = new PatientDAO;
$f->Username = "n p";
$f->Gender = "female";
print "Patient Update: " . $f->Update() . "<br/>\n";;
*/

//THIS SECTION IS TO TEST THE STATISTICS DAOS!
/////////////////////////////////////////

$pat = new PatientDAO;
$pat->Username = "bh673";

$dt_list = $pat->ListDailyTasks();

print("Patient List Obtained\n");

$st = $dt_list[0]->GetStatistics("2009-10-31 22:00");

print_r($st);

$st_list = $dt_list[1]->ListStatistics("2008-11-01 12:00","2010-11-01 12:00","2008-11-01 12:00","2010-11-01 12:00");

print_r($st_list);



?>
