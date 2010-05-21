--Insert some data to work with into the database for activity monitor.

use Activity_Monitor;

delete from WalkingStats;
delete from Statistics;
delete from WalkingTask;
delete from DailyTask;
delete from Patient;
delete from User;

insert into User (idNumber, Username, Password, FirstName, MiddleName, LastName, Address, PhoneNumber, UserClass) values
	(1, 'ryan', AES_ENCRYPT('password', 'key'), AES_ENCRYPT('Scott', 'key'), AES_ENCRYPT('Ryan', 'key'), AES_ENCRYPT('Edgar', 'key'), AES_ENCRYPT('72 W Willets Dr. Red Hook, NY 12571', 'key'), AES_ENCRYPT('8457501573', 'key'), 'Clinician'),
	(2, 'mike', AES_ENCRYPT('password', 'key'), AES_ENCRYPT('Mike', 'key'), AES_ENCRYPT('J', 'key'), AES_ENCRYPT('Hadley', 'key'), AES_ENCRYPT('123 Fake St. Springfield, MA 90210', 'key'), AES_ENCRYPT('5550001234', 'key'), 'Clinician'),
	(3, 'bh673', AES_ENCRYPT('password', 'key'), AES_ENCRYPT('Henry', 'key'), AES_ENCRYPT('I', 'key'), AES_ENCRYPT('Mitch', 'key'), AES_ENCRYPT('321 Evergreen Terace, Springfield, OH 13699', 'key'), AES_ENCRYPT('1235551234', 'key'), 'Patient'),
	(4, 'abc56', AES_ENCRYPT('password', 'key'), AES_ENCRYPT('Harry', 'key'), AES_ENCRYPT('N', 'key'), AES_ENCRYPT('Smith', 'key'), AES_ENCRYPT('666 Evil St. Hell, MI 66666', 'key'), AES_ENCRYPT('6661236660', 'key'), 'Patient');

insert into Patient (idNumber, DOB, Height, Weight, Gender, SSN, MedicalConditions, ClinicianId) values
	(3, '19500620', 68, 155, 'male', AES_ENCRYPT('123456789', 'key'), 'None', 2),
	(4, '19660601', 70, 140, 'male', AES_ENCRYPT('987654321', 'key'), 'Is pure evil!', 1);

insert into DailyTask (idDailyTask, idPatient, DailyTaskClass, Date, Active) values
	(1, 3, 'WalkingTask', '20091101', TRUE),
	(2, 3, 'WalkingTask', '20091031', FALSE),
	(3, 3, 'WalkingTask', '20091101', TRUE),
	(4, 4, 'WalkingTask', '20091101', TRUE);

insert into WalkingTask (idDailyTask, Steps) values
	(1, 500),
	(2, 1000),
	(3, 500),
	(4, 250);

insert into Statistics (idStatistics, StatsClass, idDailyTask, StartDateTime, EndDateTime) values
	(1, 'WalkingStats', 1, '2009-10-31 10:10:00', '2009-10-31 11:00:00'),
	(2, 'WalkingStats', 1, '2009-10-31 11:00:00', '2009-10-31 20:00:00'),
	(3, 'WalkingStats', 1, '2009-10-31 20:00:00', '2009-10-31 20:10:00'),
	(4, 'WalkingStats', 1, '2009-10-31 20:10:00', '2009-11-01 00:00:00'),
	(5, 'WalkingStats', 4, '2009-11-01 20:10:00', '2009-11-02 00:00:00'),
	(6, 'WalkingStats', 3, '2009-10-31 22:00:00', '2009-11-01 00:00:00');


insert into WalkingStats (idStatistics, Steps) values
	(1, 200),
	(2, 0),
	(3, 100),
	(4, 0),
	(5, 300),
	(6, 50);
