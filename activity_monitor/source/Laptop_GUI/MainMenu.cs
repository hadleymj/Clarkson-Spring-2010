using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Laptop_GUI
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            CreateControls();
            InitializeComponent();
            Setstuff(); 
            
            ConnectionStuff();

         
        }
        public void ConnectionStuff()
        {
            try
            {
                Laptop_GUI.ServerConnection conn = new Laptop_GUI.ServerConnection();
                conn.connect();

                Server_Connection.DailyTask[] dts = conn.getTasks();
                DailyTaskID = dts[0].idDailyTask;
                todaysTask = new WalkingTask(((Server_Connection.WalkingTask)dts[0]).steps);
                conn.disconnect();
            }
            catch
            {
                AlertGUI temp2 = new AlertGUI();
                temp2.setMessage("Connection to server lost: Setting default task");
                Cursor.Current = Cursors.WaitCursor;
                
                temp2.ShowDialog();
                Cursor.Current = Cursors.Arrow;
                todaysTask = new WalkingTask(500);
            }

            current_task = todaysTask.display();

            tasksDialog.setStuff(this);
            this.myWiiData = new WiiRemoteData();
            myWiiRemoteConnection = new WiiRemoteConnection();

            myWiiRemoteConnection.connect();

            this.myWiiData.processData(myWiiRemoteConnection);
            this.TodaysStats = new Queue<Stats>();
        }
        private void CreateControls()
        {
            tasksDialog = new DailyTasks();
            statsDialog = new Statistics();
            clinicianDialog = new ClinicianGUI();
            alertDialog = new AlertGUI();
            clinicianDialog.ControlBox = false;
            tasksDialog.ControlBox = false;
            statsDialog.ControlBox = false;
            alertDialog.ControlBox = false;

            statsDialog.Activate();

            statsDialog.new_timer.Enabled = true;
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {

        }

        private void tasks_button_Click(object sender, EventArgs e)
        {
            Setstuff();
            Cursor.Current = Cursors.WaitCursor;
            tasksDialog.ShowDialog();
            Cursor.Current = Cursors.Arrow;

        }

        private void stats_button_Click(object sender, EventArgs e)
        {
            Setstuff();
            ON();
            
            
        }

        public void ON()//this is for the updating statistics menu
        {
             isON = true;
        }
        public void OFF()//this is for the updating statistics menu
        {
             isON = false;
        }
 
        public void Setstuff()
        {
            statsDialog.setStuff(this);

            tasksDialog.setStuff(this);
            clinicianDialog.setStuff(this);
            clinicianDialog.exrtactConfigFile();
            this.patient_name_value.Text = myConfig.patientName;

            clinicianDialog.username_textbox.Text = myConfig.username;
            clinicianDialog.hostname_textbox.Text = myConfig.hostname;
            clinicianDialog.patient_name_textbox.Text = myConfig.patientName;
            clinicianDialog.password_textbox.Text = myConfig.password;
            

        }

        private void quit_button_Click(object sender, EventArgs e)
        {
            //save summary of days tasks
            try
            {
                string statsFilePath = "C:\\ActivityMonitor\\PreviousStats.stats";
                string activityMonitorDirectory = "C:\\ActivityMonitor";

                if(File.Exists(statsFilePath))
                {
                 
                    StreamReader ReadStatsFile = new StreamReader(statsFilePath);
                    Queue<string> previous= new Queue<string>();
                    int a = 6;
                    while (a>0)
                    {
                        a--;
                        previous.Enqueue(ReadStatsFile.ReadLine());
                    }
                    string TodaysStats = STEPS.ToString() + "," + statsDialog.steps_min_value.Text ;
                    //previous.Push(TodaysStats);
                    ReadStatsFile.Close();

                    StreamWriter writeStatsFile = new StreamWriter(statsFilePath);
                    writeStatsFile.WriteLine(TodaysStats);
                    while(previous.Count()>0)
                    {
                        writeStatsFile.WriteLine(previous.Dequeue());
                       
                    }
                        writeStatsFile.Close();
                }
                else
                {
                                    

                    if (File.Exists(statsFilePath))
                        {
                         return;
                         }

                    //if directory exists, create file
                    if (Directory.Exists(activityMonitorDirectory))
                    {
                        FileStream createConfigFile = File.Create(statsFilePath);
                        createConfigFile.Close();
                        return;
                    }

                    //if directory doesnt exist, create file and directory
                    if (!Directory.Exists(activityMonitorDirectory))
                    {
                        Directory.CreateDirectory(activityMonitorDirectory);

                        FileStream createConfigFile = File.Create(statsFilePath);
                        createConfigFile.Close();
                        return;

                    }
                }

            }
            catch
            {
                AlertGUI temp2 = new AlertGUI();
                temp2.setMessage("Could not save daily tasks");
                Cursor.Current = Cursors.WaitCursor;
                
                temp2.ShowDialog();
                Cursor.Current = Cursors.Arrow;
            }

            //sending data to the server
            try
            {
                Laptop_GUI.ServerConnection conn = new Laptop_GUI.ServerConnection();
                conn.connect();
                conn.setMainMenu(this);
                conn.sendStats();

               
            }
            catch (Exception e5)
            {
                AlertGUI temp2 = new AlertGUI();
                temp2.setMessage("Could not find server");
                Cursor.Current = Cursors.WaitCursor;
                
                temp2.ShowDialog();

                Cursor.Current = Cursors.Arrow;
                
            }

            statsDialog.Close();
            
            tasksDialog.Close();
           
            Application.Exit();
            Environment.Exit(0);
        }

        private void clinican_GUI_button_Click(object sender, EventArgs e)
        {
            clinicianDialog.hostname_textbox.Text = myConfig.hostname;
            clinicianDialog.Show();
        }




    }
}
