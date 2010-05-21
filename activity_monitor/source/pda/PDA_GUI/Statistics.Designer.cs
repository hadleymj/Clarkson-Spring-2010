namespace PDA_GUI
{
    partial class Statistics
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.live_stats_panel = new System.Windows.Forms.Panel();
            this.distance_value = new System.Windows.Forms.Label();
            this.step_min_value = new System.Windows.Forms.Label();
            this.time_walking_value = new System.Windows.Forms.Label();
            this.steps_value = new System.Windows.Forms.Label();
            this.distance_label = new System.Windows.Forms.Label();
            this.time_walking_label = new System.Windows.Forms.Label();
            this.steps_min_label = new System.Windows.Forms.Label();
            this.steps_label = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.past_steps_value = new System.Windows.Forms.Label();
            this.past_steps_min_value = new System.Windows.Forms.Label();
            this.past_stats_combobox = new System.Windows.Forms.ComboBox();
            this.past_stats_min_label = new System.Windows.Forms.Label();
            this.past_steps_label = new System.Windows.Forms.Label();
            this.past_stats_label = new System.Windows.Forms.Label();
            this.stats_main_menu_button = new System.Windows.Forms.Button();
            this.new_timer = new System.Windows.Forms.Timer();
            this.live_stats_label = new System.Windows.Forms.Label();
            this.live_stats_panel.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // live_stats_panel
            // 
            this.live_stats_panel.BackColor = System.Drawing.SystemColors.Info;
            this.live_stats_panel.Controls.Add(this.live_stats_label);
            this.live_stats_panel.Controls.Add(this.distance_value);
            this.live_stats_panel.Controls.Add(this.step_min_value);
            this.live_stats_panel.Controls.Add(this.time_walking_value);
            this.live_stats_panel.Controls.Add(this.steps_value);
            this.live_stats_panel.Controls.Add(this.distance_label);
            this.live_stats_panel.Controls.Add(this.time_walking_label);
            this.live_stats_panel.Controls.Add(this.steps_min_label);
            this.live_stats_panel.Controls.Add(this.steps_label);
            this.live_stats_panel.Location = new System.Drawing.Point(0, 0);
            this.live_stats_panel.Name = "live_stats_panel";
            this.live_stats_panel.Size = new System.Drawing.Size(225, 109);
            // 
            // distance_value
            // 
            this.distance_value.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.distance_value.Location = new System.Drawing.Point(140, 84);
            this.distance_value.Name = "distance_value";
            this.distance_value.Size = new System.Drawing.Size(77, 20);
            this.distance_value.Text = "0 mi.";
            this.distance_value.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // step_min_value
            // 
            this.step_min_value.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.step_min_value.Location = new System.Drawing.Point(140, 44);
            this.step_min_value.Name = "step_min_value";
            this.step_min_value.Size = new System.Drawing.Size(91, 20);
            this.step_min_value.Text = "0";
            this.step_min_value.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // time_walking_value
            // 
            this.time_walking_value.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.time_walking_value.Location = new System.Drawing.Point(126, 64);
            this.time_walking_value.Name = "time_walking_value";
            this.time_walking_value.Size = new System.Drawing.Size(91, 20);
            this.time_walking_value.Text = "00:00:00";
            this.time_walking_value.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // steps_value
            // 
            this.steps_value.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.steps_value.Location = new System.Drawing.Point(140, 24);
            this.steps_value.Name = "steps_value";
            this.steps_value.Size = new System.Drawing.Size(91, 20);
            this.steps_value.Text = "0";
            this.steps_value.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // distance_label
            // 
            this.distance_label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.distance_label.Location = new System.Drawing.Point(3, 83);
            this.distance_label.Name = "distance_label";
            this.distance_label.Size = new System.Drawing.Size(97, 20);
            this.distance_label.Text = "Distance:";
            // 
            // time_walking_label
            // 
            this.time_walking_label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.time_walking_label.Location = new System.Drawing.Point(3, 64);
            this.time_walking_label.Name = "time_walking_label";
            this.time_walking_label.Size = new System.Drawing.Size(117, 20);
            this.time_walking_label.Text = "Time Walking:";
            // 
            // steps_min_label
            // 
            this.steps_min_label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.steps_min_label.Location = new System.Drawing.Point(3, 44);
            this.steps_min_label.Name = "steps_min_label";
            this.steps_min_label.Size = new System.Drawing.Size(89, 20);
            this.steps_min_label.Text = "Steps/Min:";
            // 
            // steps_label
            // 
            this.steps_label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.steps_label.Location = new System.Drawing.Point(3, 24);
            this.steps_label.Name = "steps_label";
            this.steps_label.Size = new System.Drawing.Size(89, 20);
            this.steps_label.Text = "Steps: ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Info;
            this.panel1.Controls.Add(this.past_steps_value);
            this.panel1.Controls.Add(this.past_steps_min_value);
            this.panel1.Controls.Add(this.past_stats_combobox);
            this.panel1.Controls.Add(this.past_stats_min_label);
            this.panel1.Controls.Add(this.past_steps_label);
            this.panel1.Controls.Add(this.past_stats_label);
            this.panel1.Location = new System.Drawing.Point(0, 115);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(225, 178);
            // 
            // past_steps_value
            // 
            this.past_steps_value.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.past_steps_value.Location = new System.Drawing.Point(140, 66);
            this.past_steps_value.Name = "past_steps_value";
            this.past_steps_value.Size = new System.Drawing.Size(77, 20);
            this.past_steps_value.Text = "0";
            this.past_steps_value.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // past_steps_min_value
            // 
            this.past_steps_min_value.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.past_steps_min_value.Location = new System.Drawing.Point(140, 86);
            this.past_steps_min_value.Name = "past_steps_min_value";
            this.past_steps_min_value.Size = new System.Drawing.Size(77, 20);
            this.past_steps_min_value.Text = "0";
            this.past_steps_min_value.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // past_stats_combobox
            // 
            this.past_stats_combobox.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.past_stats_combobox.Items.Add("1 day ago");
            this.past_stats_combobox.Items.Add("2 days ago");
            this.past_stats_combobox.Items.Add("3 days ago");
            this.past_stats_combobox.Items.Add("4 days ago");
            this.past_stats_combobox.Items.Add("5 days ago");
            this.past_stats_combobox.Items.Add("6 days ago");
            this.past_stats_combobox.Items.Add("7 days ago");
            this.past_stats_combobox.Location = new System.Drawing.Point(45, 27);
            this.past_stats_combobox.Name = "past_stats_combobox";
            this.past_stats_combobox.Size = new System.Drawing.Size(148, 27);
            this.past_stats_combobox.TabIndex = 2;
            this.past_stats_combobox.TabStop = false;
            // 
            // past_stats_min_label
            // 
            this.past_stats_min_label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.past_stats_min_label.Location = new System.Drawing.Point(3, 86);
            this.past_stats_min_label.Name = "past_stats_min_label";
            this.past_stats_min_label.Size = new System.Drawing.Size(89, 20);
            this.past_stats_min_label.Text = "Steps/Min:";
            // 
            // past_steps_label
            // 
            this.past_steps_label.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.past_steps_label.Location = new System.Drawing.Point(3, 66);
            this.past_steps_label.Name = "past_steps_label";
            this.past_steps_label.Size = new System.Drawing.Size(58, 20);
            this.past_steps_label.Text = "Steps:";
            // 
            // past_stats_label
            // 
            this.past_stats_label.BackColor = System.Drawing.Color.Silver;
            this.past_stats_label.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.past_stats_label.Location = new System.Drawing.Point(0, 0);
            this.past_stats_label.Name = "past_stats_label";
            this.past_stats_label.Size = new System.Drawing.Size(225, 24);
            this.past_stats_label.Text = "Past Stats";
            this.past_stats_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // stats_main_menu_button
            // 
            this.stats_main_menu_button.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.stats_main_menu_button.Location = new System.Drawing.Point(0, 233);
            this.stats_main_menu_button.Name = "stats_main_menu_button";
            this.stats_main_menu_button.Size = new System.Drawing.Size(120, 48);
            this.stats_main_menu_button.TabIndex = 10;
            this.stats_main_menu_button.TabStop = false;
            this.stats_main_menu_button.Text = "Main Menu";
            this.stats_main_menu_button.Click += new System.EventHandler(this.stats_main_menu_button_Click_1);
            // 
            // new_timer
            // 
            this.new_timer.Interval = 1000;
            // 
            // live_stats_label
            // 
            this.live_stats_label.BackColor = System.Drawing.Color.Silver;
            this.live_stats_label.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.live_stats_label.Location = new System.Drawing.Point(0, 0);
            this.live_stats_label.Name = "live_stats_label";
            this.live_stats_label.Size = new System.Drawing.Size(225, 24);
            this.live_stats_label.Text = "Live Stats";
            this.live_stats_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // Statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.stats_main_menu_button);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.live_stats_panel);
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "Statistics";
            this.Text = "Statistics";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.live_stats_panel.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel live_stats_panel;
        private System.Windows.Forms.Label distance_label;
        private System.Windows.Forms.Label time_walking_label;
        private System.Windows.Forms.Label steps_min_label;
        private System.Windows.Forms.Label steps_label;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox past_stats_combobox;
        private System.Windows.Forms.Label past_stats_min_label;
        private System.Windows.Forms.Label past_steps_label;
        private System.Windows.Forms.Label past_stats_label;
        private System.Windows.Forms.Button stats_main_menu_button;
        public System.Windows.Forms.Label distance_value;
        public System.Windows.Forms.Label step_min_value;
        public System.Windows.Forms.Label time_walking_value;
        private System.Windows.Forms.Label steps_value;
        private System.Windows.Forms.Label past_steps_value;
        private System.Windows.Forms.Label past_steps_min_value;
        public System.Windows.Forms.Timer new_timer;
        private System.Windows.Forms.Label live_stats_label;
    }
}