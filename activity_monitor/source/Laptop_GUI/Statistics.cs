using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Laptop_GUI
{
    public partial class Statistics : Form
    {
        public Statistics()
        {

            InitializeComponent();
            Past=new string[7];
            PreviousStats();

            //pull off previous stats


        }
        private void PreviousStats()
        {
            try
            {
                string statsFilePath = "C:\\ActivityMonitor\\PreviousStats.stats";
                string activityMonitorDirectory = "C:\\ActivityMonitor";

                if (File.Exists(statsFilePath))
                {

                    StreamReader ReadStatsFile = new StreamReader(statsFilePath);
                    Queue<string> previous = new Queue<string>();
                    int a = 7;
                    while (a > 0)
                    {
                        a--;
                        if (ReadStatsFile.EndOfStream == false)
                            previous.Enqueue(ReadStatsFile.ReadLine());
                        else
                            previous.Enqueue("No stats saved,No stats saved");
                    }
                    //string TodaysStats = STEPS.ToString() + " Steps, " + statsDialog.steps_min_value.Text + " Steps/Min";
                    //previous.Push(TodaysStats);
                    ReadStatsFile.Close();

                    Past[0] = previous.Dequeue();
                    Past[1] = previous.Dequeue();
                    Past[2] = previous.Dequeue();
                    Past[3] = previous.Dequeue();
                    Past[4] = previous.Dequeue();
                    Past[5] = previous.Dequeue();
                    Past[6] = previous.Dequeue();


                }
                else
                {
                    Past[0] = "No stats saved,No stats saved";
                    Past[1] = "No stats saved,No stats saved";
                    Past[2] = "No stats saved,No stats saved";
                    Past[3] = "No stats saved,No stats saved";
                    Past[4] = "No stats saved,No stats saved";
                    Past[5] = "No stats saved,No stats saved";
                    Past[6] = "No stats saved,No stats saved";

                }


            }
            catch
            {
                Past[0] = "No stats saved,No stats saved";
                Past[1] = "No stats saved,No stats saved";
                Past[2] = "No stats saved,No stats saved";
                Past[3] = "No stats saved,No stats saved";
                Past[4] = "No stats saved,No stats saved";
                Past[5] = "No stats saved,No stats saved";
                Past[6] = "No stats saved,No stats saved";

                AlertGUI temp2 = new AlertGUI();
                temp2.setMessage("Could not find previous stats");
                Cursor.Current = Cursors.WaitCursor;
                temp2.ShowDialog();
                Cursor.Current = Cursors.Arrow;
            }
        }

        private void stats_main_menu_button_Click(object sender, EventArgs e)
        {

            this.Visible = false;
            this.steps_value.Text = mainMenuPointer.getSteps().ToString();
            mainMenuPointer.OFF();
            
        }
       public void stepsMin()
        {
           //uses wierd casting to round off to 2 decimals
            try
            {
                  int cur_steps = mainMenuPointer.getSteps();
                  double cur_min = (hours*60) + (minutes) + (double)seconds/60.00;
                  int way_moreTemp = (int)(100.00*(double)(cur_steps / cur_min));
                  double stepsmin = (double)way_moreTemp/100.00;
                  this.steps_min_value.Text = (stepsmin).ToString();
            }
            catch
            {
                  this.steps_min_value.Text ="0";
            }
        }


        public void setStuff(MainMenu inMainPointer)
        {


            mainMenuPointer = inMainPointer;
            //uses the main menu to get/set everything

        }



        private void new_timer_Tick(object sender, EventArgs e)
        {
            
            
            mainMenuPointer.ProcessSteps();
            this.Refresh();
            this.steps_value.Text = mainMenuPointer.getSteps().ToString();
            this.Refresh();
            
            if (mainMenuPointer.isON)
            {
                this.Show();
            }

            dis_counter++;
            seconds++;

            stepsMin();//calculates and sets the steps per minuite field

            this.distance_value.Text = (mainMenuPointer.getSteps() * .003).ToString()+ " mi" ;


            if (seconds == 60)
            {
                seconds = 0;
                sec = "00";
                minutes++;
            }

            if (seconds < 10)
            {
                sec = "0" + seconds.ToString();

            }


            if (seconds >= 10)
            {
                sec = seconds.ToString();
            }

            if (minutes == 60)
            {
                minutes = 0;
                min = "00";
                hours++;
            }

            if (minutes < 10)
            {
                min = "0" + minutes.ToString();
            }

            if (minutes >= 10)
            {
                min = minutes.ToString();
            }

            if (hours < 10)
            {
                hr = "0" + hours.ToString();
            }

            if (hours >= 10)
            {
                hr = hours.ToString();
            }


            this.time_walking_value.Text = hr + ":" + min + ":" + sec;

        }


        public MainMenu mainMenuPointer;
        public int seconds = 0;
        public string sec = "00";
        public int minutes = 0;
        public string min = "00";
        public int hours = 0;
        public string hr = "00";
        public Timer time;
        public int step_tick_stub = 0;
        public int cur_step = 0;
        public int dis_counter = 0;
        public int dis_total = 0;
        public string[] Past;
        private void past_stats_comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = past_stats_comboBox.SelectedIndex;
            int comma=Past[index].IndexOf(',');
            try
            {
                string Psteps = Past[index].Substring(0, comma);
                string PstepsM = Past[index].Substring(comma + 1);
                this.past_steps_value.Text = Psteps;
                this.past_steps_min_value.Text = PstepsM;
            }
            catch
            {
                this.past_steps_value.Text = "No stats";
                this.past_steps_min_value.Text = "No Stats";
            }
            
            
        }
    }


    
}
