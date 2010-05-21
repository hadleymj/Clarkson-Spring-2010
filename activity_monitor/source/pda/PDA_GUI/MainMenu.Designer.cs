using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PDA_GUI
{
    partial class MainMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;


   

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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.imageList1 = new System.Windows.Forms.ImageList();
            this.welcome_label = new System.Windows.Forms.Label();
            this.patient_name_value = new System.Windows.Forms.Label();
            this.tasks_button = new System.Windows.Forms.Button();
            this.quit_button = new System.Windows.Forms.Button();
            this.stats_button = new System.Windows.Forms.Button();
            this.clinician_GUI_button = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            // 
            // welcome_label
            // 
            this.welcome_label.BackColor = System.Drawing.Color.Silver;
            this.welcome_label.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.welcome_label.Location = new System.Drawing.Point(0, 0);
            this.welcome_label.Name = "welcome_label";
            this.welcome_label.Size = new System.Drawing.Size(225, 27);
            this.welcome_label.Text = "Welcome,";
            this.welcome_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // patient_name_value
            // 
            this.patient_name_value.BackColor = System.Drawing.Color.Silver;
            this.patient_name_value.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.patient_name_value.Location = new System.Drawing.Point(0, 27);
            this.patient_name_value.Name = "patient_name_value";
            this.patient_name_value.Size = new System.Drawing.Size(225, 27);
            this.patient_name_value.Text = "Patient Name";
            this.patient_name_value.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tasks_button
            // 
            this.tasks_button.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.tasks_button.Location = new System.Drawing.Point(55, 57);
            this.tasks_button.Name = "tasks_button";
            this.tasks_button.Size = new System.Drawing.Size(120, 48);
            this.tasks_button.TabIndex = 1;
            this.tasks_button.Text = "Tasks";
            this.tasks_button.Click += new System.EventHandler(this.tasks_button_Click);
            // 
            // quit_button
            // 
            this.quit_button.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.quit_button.Location = new System.Drawing.Point(108, 202);
            this.quit_button.Name = "quit_button";
            this.quit_button.Size = new System.Drawing.Size(110, 48);
            this.quit_button.TabIndex = 4;
            this.quit_button.Text = "Quit";
            this.quit_button.Click += new System.EventHandler(this.quit_button_Click);
            // 
            // stats_button
            // 
            this.stats_button.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.stats_button.Location = new System.Drawing.Point(55, 111);
            this.stats_button.Name = "stats_button";
            this.stats_button.Size = new System.Drawing.Size(120, 48);
            this.stats_button.TabIndex = 2;
            this.stats_button.Text = "Progress";
            this.stats_button.Click += new System.EventHandler(this.stats_button_Click);
            // 
            // clinician_GUI_button
            // 
            this.clinician_GUI_button.Image = ((System.Drawing.Image)(resources.GetObject("clinician_GUI_button.Image")));
            this.clinician_GUI_button.Location = new System.Drawing.Point(3, 165);
            this.clinician_GUI_button.Name = "clinician_GUI_button";
            this.clinician_GUI_button.Size = new System.Drawing.Size(99, 132);
            this.clinician_GUI_button.Click += new System.EventHandler(this.clinician_GUI_button_Click);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.quit_button);
            this.Controls.Add(this.stats_button);
            this.Controls.Add(this.clinician_GUI_button);
            this.Controls.Add(this.tasks_button);
            this.Controls.Add(this.welcome_label);
            this.Controls.Add(this.patient_name_value);
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "MainMenu";
            this.Text = "MainMenu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private Label welcome_label;
        private Label patient_name_value;
        private Button tasks_button;
        private Button quit_button;
        private Button stats_button;
        private PictureBox clinician_GUI_button;
    }
}