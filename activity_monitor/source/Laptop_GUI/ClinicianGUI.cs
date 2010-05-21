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
    public partial class ClinicianGUI : Form
    {
        public ClinicianGUI()
        {
            InitializeComponent();
        }

        public void setStuff(MainMenu inMainPointer)//, WiiRemoteConnection inConn)
        {


            mainMenuPointer = inMainPointer;


        }

        MainMenu mainMenuPointer;

        private void save_setup_button_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (writeConfigFile(null, this.patient_name_textbox.Text, this.hostname_textbox.Text, this.username_textbox.Text, this.password_textbox.Text))
                {
                    AlertGUI temp = new AlertGUI();
                    temp.setMessage("File Written", "whatever");
                    Cursor.Current = Cursors.WaitCursor;
                    temp.ShowDialog();
                    Cursor.Current = Cursors.Arrow;
                    temp.Dispose();
                }
            }
            catch
            {

               
                try
                {
                    if (writeConfigFile(null, this.patient_name_textbox.Text, this.hostname_textbox.Text, this.username_textbox.Text, this.password_textbox.Text))
                    {
                        AlertGUI temp2 = new AlertGUI();
                        temp2.setMessage("File Written","whatever");
                        Cursor.Current = Cursors.WaitCursor;
                        temp2.ShowDialog();
                        Cursor.Current = Cursors.Arrow;
                        temp2.Dispose();
                    }
                }
                catch
                {

                    AlertGUI temp2 = new AlertGUI();
                    temp2.setMessage("File Not Written");
                    Cursor.Current = Cursors.WaitCursor;
                    temp2.ShowDialog();
                    Cursor.Current = Cursors.Arrow;

                }
            }
        }

        private void connection_test_button_Click(object sender, EventArgs e)
        {
            AlertGUI temp3 = new AlertGUI();
            temp3.setMessage("Attempting to connect to server....", "");
            temp3.Show();

            try
            {
                Laptop_GUI.ServerConnection conn = new Laptop_GUI.ServerConnection();
                conn.connect();

                temp3.Close();
                temp3.Dispose();
                AlertGUI temp5 = new AlertGUI();
                conn.disconnect();
                temp5.setMessage("Server Connected Successfully!","");
                Cursor.Current = Cursors.WaitCursor;
                temp5.ShowDialog();
                Cursor.Current = Cursors.Arrow;


            }
            catch
            {
                AlertGUI temp2 = new AlertGUI();
                temp2.setMessage("Could not find server");
                Cursor.Current = Cursors.WaitCursor;
                temp2.ShowDialog();
                Cursor.Current = Cursors.Arrow;
                temp3.Close();
                temp3.Dispose();


            }
        }

        //File I/O for Clinican Config file
        public void setConfigFile()
        {
            //Config file testing
            if (configFileExists())
            {

                StreamReader readConfigFile = new StreamReader(configFilePath);
                string lastKnownTasks = readConfigFile.ReadLine();
                string patientName = readConfigFile.ReadLine();
                string hostname = readConfigFile.ReadLine();
                string username = readConfigFile.ReadLine();
                string password = readConfigFile.ReadLine();
                string startTime = readConfigFile.ReadLine();
                string endTime = readConfigFile.ReadLine();
                string date = readConfigFile.ReadLine();

                readConfigFile.Close();

            }

            //else create new config file
            else
            {

                createConfigFile();

            }

        }
        public void exrtactConfigFile()
        {
            //Config file testing
            if (configFileExists())
            {

                StreamReader readConfigFile = new StreamReader(configFilePath);
                string lastKnownTasks = killBrackets(readConfigFile.ReadLine());
                string patientName = killBrackets(readConfigFile.ReadLine());

                string hostname = killBrackets(readConfigFile.ReadLine());
                string username = killBrackets(readConfigFile.ReadLine());
                string password = killBrackets(readConfigFile.ReadLine());
                string startTime = killBrackets(readConfigFile.ReadLine());
                string endTime = killBrackets(readConfigFile.ReadLine());
                string date = killBrackets(readConfigFile.ReadLine());



                readConfigFile.Close();

                mainMenuPointer.myConfig.lastKnownTasks = lastKnownTasks;
                mainMenuPointer.myConfig.patientName = patientName;
                mainMenuPointer.myConfig.hostname = hostname;
                mainMenuPointer.myConfig.username = username;
                mainMenuPointer.myConfig.password = password;
                mainMenuPointer.myConfig.startTime = startTime;
                mainMenuPointer.myConfig.endTime = endTime;
                mainMenuPointer.myConfig.date = date;



            }

            //else create new config file
            else
            {

                AlertGUI temp = new AlertGUI();
                temp.setMessage("Config File doesnt exist");
                Cursor.Current = Cursors.WaitCursor;
                temp.ShowDialog();
                Cursor.Current = Cursors.Arrow;

                createConfigFile();

            }

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
        //Check to see if config file exists

        public bool configFileExists()
        {
            //Config file testing
            if (Directory.Exists(activityMonitorDirectory) && File.Exists(configFilePath))
            {
                return true;
            }

            else
            {
                return false;
            }

        }

        //Create a config file
        public bool writeConfigFile(string[] tasks, string patient_name, string hostname, string username, string password)
        {
            string tasks_string = "";

            if (tasks == null)
            {
            }

            else
            {
                for (int i = 0; i < tasks.Length; ++i)
                {
                    if ((i = tasks.Length - 1) > 0)
                    {
                        tasks_string += tasks[i];
                    }

                    else
                    {

                        tasks_string += tasks[i] + ", ";
                    }

                }
            }


            if (!Directory.Exists(activityMonitorDirectory))
            {
                Directory.CreateDirectory(activityMonitorDirectory);
            }
            

            if (username.Contains('"'))
            {
                AlertGUI temp = new AlertGUI();
                temp.setMessage("Do not add \"");
                Cursor.Current = Cursors.WaitCursor;
                
                temp.ShowDialog();

                Cursor.Current = Cursors.Arrow;
                return false;
            }
            if (hostname.Contains('"'))
            {
                AlertGUI temp = new AlertGUI();
                temp.setMessage("Do not add \"");
                Cursor.Current = Cursors.WaitCursor;

                temp.ShowDialog();
      
                Cursor.Current = Cursors.Arrow;
                return false;
            }
            if (password.Contains('"'))
            {
                AlertGUI temp = new AlertGUI();
                temp.setMessage("Do not add \"");
                Cursor.Current = Cursors.WaitCursor;
         
                temp.ShowDialog();

                Cursor.Current = Cursors.Arrow;
                return false;
            }
            if (patient_name.Contains('"'))
            {
                AlertGUI temp = new AlertGUI();
                temp.setMessage("Do not add \"");
                Cursor.Current = Cursors.WaitCursor;
 
                temp.ShowDialog();
       
                Cursor.Current = Cursors.Arrow;
                return false;
            }

            //if config file exists, delete and create a new one
            if (configFileExists())
            {
                deleteConfigFile();
            }
            
            StreamWriter writeConfigFile = new StreamWriter(configFilePath);
            
            writeConfigFile.WriteLine("Last Known Tasks:\t {" + tasks_string + "}");
            writeConfigFile.WriteLine("Patient Name:\t {" + patient_name + "}");
            writeConfigFile.WriteLine("Hostname:\t {" + hostname + "}");
            writeConfigFile.WriteLine("Username:\t {" + username + "}");
            writeConfigFile.WriteLine("Password:\t {" + password + "}");
            writeConfigFile.WriteLine("Starttime:\t {00:00:00}");
            writeConfigFile.WriteLine("Endtime:\t {00:00:00}");
            writeConfigFile.WriteLine("Date:\t {00/00/00}");

            writeConfigFile.Close();
            return true;
        }

        public void createConfigFile()
        {
            //if file exists, return
            if (File.Exists(configFilePath))
            {
                return;
            }

            //if directory exists, create file
            if (Directory.Exists(activityMonitorDirectory))
            {
                FileStream createConfigFile = File.Create(configFilePath);
                createConfigFile.Close();
                return;
            }

            //if directory doesnt exist, create file and directory
            if (!Directory.Exists(activityMonitorDirectory))
            {
                Directory.CreateDirectory(activityMonitorDirectory);

                FileStream createConfigFile = File.Create(configFilePath);
                createConfigFile.Close();
                return;

            }

        }

        public void deleteConfigFile()
        {
            if (configFileExists())
            {
                File.Delete(configFilePath);
                Directory.Delete(activityMonitorDirectory);
            }
        }

        private void clinican_GUI_close_button_Click(object sender, EventArgs e)
        {
            this.Hide();
        }





    }
}



