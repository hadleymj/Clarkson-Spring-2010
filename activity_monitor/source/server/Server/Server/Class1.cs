using System;
using System.Collections.Generic;
using System.Text;

//Download Mysql .net connector 5.0, and install it.
//Project->Add Reference... : Mysql.DATA.
using MySql.Data.MySqlClient;



namespace Server
{
    class Class1
    {

        // Main begins program execution.
        static void Main()
        {
            // Write to console
            Console.WriteLine("Welcome to the C# Station Tutorial!");


            //Check to see if the user "bh673" exists in the database.
            UserDAO u = new UserDAO();
            u.Username = "bh673";
            if (u.Find())
                //The record was found.
                Console.WriteLine("User's record was found.");

            if (u.Password.Equals("password"))
                //Check to see if the user's password equals the provided password: "password"
                Console.WriteLine("Password was Correct!");

            
            //Obtain a list of daily tasks, for the user "bh673".
            DailyTaskDAO dtd = new DailyTaskDAO();
            List<DailyTaskDAO> res = dtd.GetActiveTasks(u);

            //List the number of steps for each of the walking tasks.
            List<DailyTaskDAO>.Enumerator t = res.GetEnumerator();
            while (t.MoveNext())
                Console.WriteLine("Steps: " + (t.Current as WalkingTaskDAO).Steps);

            //Add a Statistics record to the database.
            WalkingStatsDAO s = new WalkingStatsDAO();
            s.idDailyTask = 1;
            s.StartDateTime = new DateTime(2007, 12, 25);
            s.EndDateTime = new DateTime(2008, 1, 1);
            s.Steps = 550;
            Console.WriteLine("Insert: " + s.Insert());
            
            //Pause
            Console.WriteLine("I executed.");
        }

    }
}
