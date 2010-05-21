using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Laptop_GUI
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        public void setSteps(int inSteps)
        {

            STEPS = inSteps;
        }

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




        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

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


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.tasks_button = new System.Windows.Forms.Button();
            this.stats_button = new System.Windows.Forms.Button();
            this.quit_button = new System.Windows.Forms.Button();
            this.welcome_label = new System.Windows.Forms.Label();
            this.patient_name_value = new System.Windows.Forms.Label();
            this.clinican_GUI_button = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.clinican_GUI_button)).BeginInit();
            this.SuspendLayout();
            // 
            // tasks_button
            // 
            this.tasks_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tasks_button.Location = new System.Drawing.Point(129, 131);
            this.tasks_button.Name = "tasks_button";
            this.tasks_button.Size = new System.Drawing.Size(315, 141);
            this.tasks_button.TabIndex = 0;
            this.tasks_button.Text = "Tasks";
            this.tasks_button.UseVisualStyleBackColor = true;
            this.tasks_button.Click += new System.EventHandler(this.tasks_button_Click);
            // 
            // stats_button
            // 
            this.stats_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stats_button.Location = new System.Drawing.Point(129, 301);
            this.stats_button.Name = "stats_button";
            this.stats_button.Size = new System.Drawing.Size(315, 141);
            this.stats_button.TabIndex = 1;
            this.stats_button.Text = "Progress";
            this.stats_button.UseVisualStyleBackColor = true;
            this.stats_button.Click += new System.EventHandler(this.stats_button_Click);
            // 
            // quit_button
            // 
            this.quit_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quit_button.Location = new System.Drawing.Point(353, 474);
            this.quit_button.Name = "quit_button";
            this.quit_button.Size = new System.Drawing.Size(315, 141);
            this.quit_button.TabIndex = 2;
            this.quit_button.Text = "Quit";
            this.quit_button.UseVisualStyleBackColor = true;
            this.quit_button.Click += new System.EventHandler(this.quit_button_Click);
            // 
            // welcome_label
            // 
            this.welcome_label.AutoSize = true;
            this.welcome_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.welcome_label.Location = new System.Drawing.Point(186, 9);
            this.welcome_label.Name = "welcome_label";
            this.welcome_label.Size = new System.Drawing.Size(199, 46);
            this.welcome_label.TabIndex = 3;
            this.welcome_label.Text = "Welcome,";
            // 
            // patient_name_value
            // 
            this.patient_name_value.AutoSize = true;
            this.patient_name_value.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.patient_name_value.Location = new System.Drawing.Point(157, 55);
            this.patient_name_value.Name = "patient_name_value";
            this.patient_name_value.Size = new System.Drawing.Size(261, 46);
            this.patient_name_value.TabIndex = 4;
            this.patient_name_value.Text = "Patient Name";
            // 
            // clinican_GUI_button
            // 
            this.clinican_GUI_button.BackColor = System.Drawing.Color.Transparent;
            this.clinican_GUI_button.ErrorImage = global::Laptop_GUI.Properties.Resources.DrBearington1;
            this.clinican_GUI_button.ImageLocation = "DrBearington.jpg";
            this.clinican_GUI_button.InitialImage = global::Laptop_GUI.Properties.Resources.DrBearington1;
            this.clinican_GUI_button.Location = new System.Drawing.Point(1, 472);
            this.clinican_GUI_button.Name = "clinican_GUI_button";
            this.clinican_GUI_button.Size = new System.Drawing.Size(97, 141);
            this.clinican_GUI_button.TabIndex = 5;
            this.clinican_GUI_button.TabStop = false;
            this.clinican_GUI_button.Click += new System.EventHandler(this.clinican_GUI_button_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 615);
            this.ControlBox = false;
            this.Controls.Add(this.clinican_GUI_button);
            this.Controls.Add(this.patient_name_value);
            this.Controls.Add(this.welcome_label);
            this.Controls.Add(this.quit_button);
            this.Controls.Add(this.stats_button);
            this.Controls.Add(this.tasks_button);
            this.Menu = this.mainMenu1;
            this.Name = "MainMenu";
            this.Text = "Main Menu";
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.MainMenu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.clinican_GUI_button)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
            this.CenterToScreen();

        }

        #endregion

        private System.Windows.Forms.Button tasks_button;
        private System.Windows.Forms.Button stats_button;
        private System.Windows.Forms.Button quit_button;
        private System.Windows.Forms.Label welcome_label;
        private System.Windows.Forms.Label patient_name_value;
        private PictureBox clinican_GUI_button;


    }
}

