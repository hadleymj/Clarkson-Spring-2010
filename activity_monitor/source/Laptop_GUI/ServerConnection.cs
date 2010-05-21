using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ActivityMonitorService;
using System.Web;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace Laptop_GUI
{
    public class ServerConnection : Connection
    {
        public void setMainMenu(MainMenu IN)
        {
            BadIdea = IN;
        }
        MainMenu BadIdea;

        private Server_Connection.ActivityMonitor server_conn;
        
        public override void connect() 
        {
            // Access remote web services 
            server_conn = new Server_Connection.ActivityMonitor();
         
        }

        public override void disconnect() 
        { 
        
        }

        public string helloWorld()
        {
            return server_conn.HelloWorld();
        }

        private string killBrackets(string In)
        {
            try
            {
                int firstBracket = In.IndexOf('{') + 1;

                In = In.Remove(0, firstBracket);
                int secondBracket = In.LastIndexOf('}');
                //In.Remove(secondBracket);
                In = In.Substring(0, secondBracket);

                return In;
            }
            catch
            {
                return "";
            }
        }

        public Server_Connection.DailyTask [] getTasks()
        {

            string configFilePath = "C:\\ActivityMonitor\\ActivityMonitor.cfg";
            string activityMonitorDirectory = "C:\\ActivityMonitor";

            Server_Connection.User me = new Server_Connection.User();

            StreamReader readConfigFile = new StreamReader(configFilePath);

            string lastKnownTasks = killBrackets(readConfigFile.ReadLine());
            string patientName = killBrackets(readConfigFile.ReadLine());

            string hostname = killBrackets(readConfigFile.ReadLine());
            string username = killBrackets(readConfigFile.ReadLine());
            string password = killBrackets(readConfigFile.ReadLine());
            string startTime = killBrackets(readConfigFile.ReadLine());
            string endTime = killBrackets(readConfigFile.ReadLine());
            string date = killBrackets(readConfigFile.ReadLine());
            me.Username = username;
            me.Password = password;
           // me.Username = "bh673";
            //me.Password = "password";
            return server_conn.GetTasks(me);
        }

        public void sendStats()
        {
            Server_Connection.User me = new Server_Connection.User();
           // me.Username = "bh673";
            //me.Password = "password";


            

            string configFilePath = "C:\\ActivityMonitor\\ActivityMonitor.cfg";
            string activityMonitorDirectory = "C:\\ActivityMonitor";
            StreamReader readConfigFile = new StreamReader(configFilePath);

            string lastKnownTasks = killBrackets(readConfigFile.ReadLine());
            string patientName = killBrackets(readConfigFile.ReadLine());

            string hostname = killBrackets(readConfigFile.ReadLine());
            string username = killBrackets(readConfigFile.ReadLine());
            string password = killBrackets(readConfigFile.ReadLine());
            string startTime = killBrackets(readConfigFile.ReadLine());
            string endTime = killBrackets(readConfigFile.ReadLine());
            string date = killBrackets(readConfigFile.ReadLine());
            me.Username = username;
            me.Password = password;

            

            Queue<Stats> Todays = BadIdea.GetStats();
            //loop as long as there are still stats
            
            Todays.Enqueue(new WalkingStats(4000,(long)2, (long)3));

            while(Todays.Count>0)
            {

                Server_Connection.WalkingStats  mystats = new Server_Connection.WalkingStats();
                Stats Frog= Todays.Dequeue();
                
                mystats.idDailyTask = BadIdea.DailyTaskID;
                mystats.idStatistics=0;
                
                mystats.StartDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                mystats.StartDateTime = mystats.StartDateTime.AddMilliseconds(Frog.start_time);

                mystats.EndDateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                mystats.EndDateTime = mystats.StartDateTime.AddMilliseconds(Frog.end_time);

                mystats.StatsClass="WalkingStats";
                mystats.steps=Frog.getStats();
                server_conn.UploadStats(me, mystats);
            }



        }

        /*
         public string getUsername();
         public void setUsername(string Username);
         public string getPassword();
         public void setPassword(string Password);
         public string getServerName();
         public void setServerName(string ServerName);
         public DailyTask[] getTasks();
        

     }
     class ServerConnection : Connection
     {
         public override void connect() { }

         public override void disconnect() { }

        /* public void sendStats(Queue<Stats> inStats);

         public string getUsername();
         public void setUsername(string Username);
         public string getPassword();
         public void setPassword(string Password);
         public string getServerName();
         public void setServerName(string ServerName);
         public DailyTask[] getTasks();
         */

        
    }
}
