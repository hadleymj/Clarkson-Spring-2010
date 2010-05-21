using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;

namespace PDA_GUI
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            CreateControls();
            InitializeComponent();
 
            Setstuff();

            goodConnection =ConnectionStuff();
        }

        public bool wiimoteCon;
        public bool isON;
        public int STEPS;
        private Queue<Stats> TodaysStats;
        private WiiRemoteConnection myWiiRemoteConnection;
        private WiiRemoteData myWiiData;
        private Statistics statsDialog;
        private DailyTasks tasksDialog;
        private AlertGUI alertDialog;
        private ClinicianGUI clinicianDialog;
        private DailyTask todaysTask;
        public string current_task;
        private ServerConnection myServerConnection;
        public ConfigFile myConfig;
        public int DailyTaskID;
        public bool goodConnection;

        [DllImport("coredll.dll", EntryPoint = "SendMessage")]
        private static extern uint SendMessage(IntPtr hWnd, uint msg, uint wParam, int lParam);


        const int WM_VSCROLL = 0x115;

        const int SB_TOP = 0x6;



        public int getSteps()
        {

            return STEPS;
        }
        public void setTodaysTask(DailyTask inTask)
        {
            todaysTask = inTask;
        }
        public DailyTask getTodaysTask()
        {
            return todaysTask;
        }


        public WiiRemoteData GetWiiRemoteData()
        {
            return myWiiData;
        }
        public void SetWiiRemoteData(WiiRemoteData inWiiData)
        {
            myWiiData = inWiiData;
        }

        public WiiRemoteConnection GetWiiRemoteConnection()
        {
            return myWiiRemoteConnection;
        }
        public void SetWiiRemoteConnection(WiiRemoteConnection inWiiCon)
        {
            myWiiRemoteConnection = inWiiCon;
        }
        public MainMenu GetThis()
        {
            return this;
        }

        public Queue<Stats> GetStats()
        {
            //if (TodaysStats == null)
            return TodaysStats;
            //return TodaysStats.Last();

        }
        public Queue<Stats> GetTodaysStats()
        {
            return TodaysStats;
        }

        public void SetTodaysStats(Queue<Stats> inGetTodaysStats)
        {
            TodaysStats = inGetTodaysStats;
        }
        public void ProcessSteps()
        {
            //myWiiData.processData(myWiiRemoteConnection);

            Stats TEMP;
            try
            {
                TEMP = myWiiData.getStats();
            }
            catch
            {
                TEMP = null;
            }

            if (TEMP != null)
            {

                TodaysStats.Enqueue(TEMP);

                // STEPS = STEPS + TEMP.getStats();

                STEPS = myWiiData.getTotalSteps();
                //TEMP = myWiiData.getStats();


            }

        }

        public void Addtats(Stats inStats)
        {
            TodaysStats.Enqueue(inStats);
        }

        private void clinician_GUI_button_Click(object sender, EventArgs e)
        {
            clinicianDialog.hostname_textbox.Text = myConfig.hostname;
            clinicianDialog.Show();
        }

        private void quit_button_Click(object sender, EventArgs e)
        {
            
            myWiiRemoteConnection.disconnect();
            //save summary of days tasks
            try
            {
                string statsFilePath = "\\ActivityMonitor\\PreviousStats.stats";
                string activityMonitorDirectory = "\\ActivityMonitor";

                if (File.Exists(statsFilePath))
                {

                    StreamReader ReadStatsFile = new StreamReader(statsFilePath);
                    Queue<string> previous = new Queue<string>();
                    int a = 6;
                    while (a > 0)
                    {
                        a--;
                        previous.Enqueue(ReadStatsFile.ReadLine());
                    }
                    string TodaysStats = STEPS.ToString() + "," + statsDialog.step_min_value.Text;
                    //previous.Push(TodaysStats);
                    ReadStatsFile.Close();

                    StreamWriter writeStatsFile = new StreamWriter(statsFilePath);
                    writeStatsFile.WriteLine(TodaysStats);
                    while (previous.Count() > 0)
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
                

                temp2.ShowDialog();
                
            }

            //sending data to the server
            try
            {
                PDA_GUI.ServerConnection conn = new PDA_GUI.ServerConnection();
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

                

            }

            statsDialog.Close();

            tasksDialog.Close();

            this.Close();

            
           
            
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

        private void tasks_button_Click(object sender, EventArgs e)
        {
            Setstuff();
            
            //tasksDialog.ShowDialog();
            tasksDialog.Show();
           
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
           //this.main_menu_scroll.Handle
            
         
            
            

            statsDialog.Activate();

            statsDialog.new_timer.Enabled = true;
          
        }

        public bool ConnectionStuff()
        {
            try
            {
                PDA_GUI.ServerConnection conn = new PDA_GUI.ServerConnection();
                conn.connect();

                Server_Connection.DailyTask[] dts = conn.getTasks();
                DailyTaskID = dts[0].idDailyTask;
                todaysTask = new WalkingTask(((Server_Connection.WalkingTask)dts[0]).steps);
                conn.disconnect();
            }
            catch
            {
                todaysTask = new WalkingTask(500);

             //   temp2.ShowDialog();
                current_task = todaysTask.display();

                tasksDialog.setStuff(this);
                this.myWiiData = new WiiRemoteData();
                myWiiRemoteConnection = new WiiRemoteConnection();

                wiimoteCon = myWiiRemoteConnection.connect();

                this.myWiiData.processData(myWiiRemoteConnection);
                this.TodaysStats = new Queue<Stats>();

                return false;
            }

            current_task = todaysTask.display();

            tasksDialog.setStuff(this);
            this.myWiiData = new WiiRemoteData();
            myWiiRemoteConnection = new WiiRemoteConnection();

            wiimoteCon= myWiiRemoteConnection.connect();

            this.myWiiData.processData(myWiiRemoteConnection);
            this.TodaysStats = new Queue<Stats>();
            return true;
        }




    }
}