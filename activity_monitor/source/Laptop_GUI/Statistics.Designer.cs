namespace Laptop_GUI
{
    partial class Statistics
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.components = new System.ComponentModel.Container();
            this.past_stats_panel = new System.Windows.Forms.GroupBox();
            this.past_steps_min_value = new System.Windows.Forms.Label();
            this.past_steps_value = new System.Windows.Forms.Label();
            this.past_steps_min_label = new System.Windows.Forms.Label();
            this.past_steps_label = new System.Windows.Forms.Label();
            this.past_stats_comboBox = new System.Windows.Forms.ComboBox();
            this.live_stats_panel = new System.Windows.Forms.GroupBox();
            this.distance_value = new System.Windows.Forms.Label();
            this.time_walking_value = new System.Windows.Forms.Label();
            this.steps_min_value = new System.Windows.Forms.Label();
            this.steps_value = new System.Windows.Forms.Label();
            this.distance_label = new System.Windows.Forms.Label();
            this.time_walking_label = new System.Windows.Forms.Label();
            this.steps_min_label = new System.Windows.Forms.Label();
            this.steps_label = new System.Windows.Forms.Label();
            this.stats_main_menu_button = new System.Windows.Forms.Button();
            this.new_timer = new System.Windows.Forms.Timer(this.components);
            this.past_stats_panel.SuspendLayout();
            this.live_stats_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // past_stats_panel
            // 
            this.past_stats_panel.BackColor = System.Drawing.SystemColors.Info;
            this.past_stats_panel.Controls.Add(this.past_steps_min_value);
            this.past_stats_panel.Controls.Add(this.past_steps_value);
            this.past_stats_panel.Controls.Add(this.past_steps_min_label);
            this.past_stats_panel.Controls.Add(this.past_steps_label);
            this.past_stats_panel.Controls.Add(this.past_stats_comboBox);
            this.past_stats_panel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.past_stats_panel.Location = new System.Drawing.Point(119, 268);
            this.past_stats_panel.Name = "past_stats_panel";
            this.past_stats_panel.Size = new System.Drawing.Size(430, 202);
            this.past_stats_panel.TabIndex = 4;
            this.past_stats_panel.TabStop = false;
            this.past_stats_panel.Text = "Past Stats";
            // 
            // past_steps_min_value
            // 
            this.past_steps_min_value.AutoSize = true;
            this.past_steps_min_value.Location = new System.Drawing.Point(221, 149);
            this.past_steps_min_value.Name = "past_steps_min_value";
            this.past_steps_min_value.Size = new System.Drawing.Size(29, 31);
            this.past_steps_min_value.TabIndex = 9;
            this.past_steps_min_value.Text = "0";
            // 
            // past_steps_value
            // 
            this.past_steps_value.AutoSize = true;
            this.past_steps_value.Location = new System.Drawing.Point(221, 118);
            this.past_steps_value.Name = "past_steps_value";
            this.past_steps_value.Size = new System.Drawing.Size(29, 31);
            this.past_steps_value.TabIndex = 8;
            this.past_steps_value.Text = "0";
            // 
            // past_steps_min_label
            // 
            this.past_steps_min_label.AutoSize = true;
            this.past_steps_min_label.Location = new System.Drawing.Point(7, 149);
            this.past_steps_min_label.Name = "past_steps_min_label";
            this.past_steps_min_label.Size = new System.Drawing.Size(143, 31);
            this.past_steps_min_label.TabIndex = 7;
            this.past_steps_min_label.Text = "Steps/Min:";
            // 
            // past_steps_label
            // 
            this.past_steps_label.AutoSize = true;
            this.past_steps_label.Location = new System.Drawing.Point(7, 118);
            this.past_steps_label.Name = "past_steps_label";
            this.past_steps_label.Size = new System.Drawing.Size(92, 31);
            this.past_steps_label.TabIndex = 6;
            this.past_steps_label.Text = "Steps:";
            // 
            // past_stats_comboBox
            // 
            this.past_stats_comboBox.CausesValidation = false;
            this.past_stats_comboBox.FormattingEnabled = true;
            this.past_stats_comboBox.Items.AddRange(new object[] {
            "1 Day Ago",
            "2 Days Ago",
            "3 Days Ago",
            "4 Days Ago",
            "5 Days Ago",
            "6 Days Ago",
            "7 Days Ago"});
            this.past_stats_comboBox.Location = new System.Drawing.Point(66, 56);
            this.past_stats_comboBox.Name = "past_stats_comboBox";
            this.past_stats_comboBox.Size = new System.Drawing.Size(266, 39);
            this.past_stats_comboBox.TabIndex = 2;
            this.past_stats_comboBox.SelectedIndexChanged += new System.EventHandler(this.past_stats_comboBox_SelectedIndexChanged);
            // 
            // live_stats_panel
            // 
            this.live_stats_panel.BackColor = System.Drawing.SystemColors.Info;
            this.live_stats_panel.Controls.Add(this.distance_value);
            this.live_stats_panel.Controls.Add(this.time_walking_value);
            this.live_stats_panel.Controls.Add(this.steps_min_value);
            this.live_stats_panel.Controls.Add(this.steps_value);
            this.live_stats_panel.Controls.Add(this.distance_label);
            this.live_stats_panel.Controls.Add(this.time_walking_label);
            this.live_stats_panel.Controls.Add(this.steps_min_label);
            this.live_stats_panel.Controls.Add(this.steps_label);
            this.live_stats_panel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.live_stats_panel.Location = new System.Drawing.Point(119, 46);
            this.live_stats_panel.Name = "live_stats_panel";
            this.live_stats_panel.Size = new System.Drawing.Size(430, 199);
            this.live_stats_panel.TabIndex = 3;
            this.live_stats_panel.TabStop = false;
            this.live_stats_panel.Text = "Live Stats";
            // 
            // distance_value
            // 
            this.distance_value.AutoSize = true;
            this.distance_value.Location = new System.Drawing.Point(303, 144);
            this.distance_value.Name = "distance_value";
            this.distance_value.Size = new System.Drawing.Size(72, 31);
            this.distance_value.TabIndex = 7;
            this.distance_value.Text = "0 mi.";
            // 
            // time_walking_value
            // 
            this.time_walking_value.AutoSize = true;
            this.time_walking_value.Location = new System.Drawing.Point(285, 113);
            this.time_walking_value.Name = "time_walking_value";
            this.time_walking_value.Size = new System.Drawing.Size(120, 31);
            this.time_walking_value.TabIndex = 6;
            this.time_walking_value.Text = "00:00:00";
            // 
            // steps_min_value
            // 
            this.steps_min_value.AutoSize = true;
            this.steps_min_value.Location = new System.Drawing.Point(330, 82);
            this.steps_min_value.Name = "steps_min_value";
            this.steps_min_value.Size = new System.Drawing.Size(29, 31);
            this.steps_min_value.TabIndex = 5;
            this.steps_min_value.Text = "0";
            // 
            // steps_value
            // 
            this.steps_value.AutoSize = true;
            this.steps_value.Location = new System.Drawing.Point(330, 51);
            this.steps_value.Name = "steps_value";
            this.steps_value.Size = new System.Drawing.Size(29, 31);
            this.steps_value.TabIndex = 4;
            this.steps_value.Text = "0";
            // 
            // distance_label
            // 
            this.distance_label.AutoSize = true;
            this.distance_label.Location = new System.Drawing.Point(7, 144);
            this.distance_label.Name = "distance_label";
            this.distance_label.Size = new System.Drawing.Size(129, 31);
            this.distance_label.TabIndex = 3;
            this.distance_label.Text = "Distance:";
            // 
            // time_walking_label
            // 
            this.time_walking_label.AutoSize = true;
            this.time_walking_label.Location = new System.Drawing.Point(7, 113);
            this.time_walking_label.Name = "time_walking_label";
            this.time_walking_label.Size = new System.Drawing.Size(185, 31);
            this.time_walking_label.TabIndex = 2;
            this.time_walking_label.Text = "Time Walking:";
            // 
            // steps_min_label
            // 
            this.steps_min_label.AutoSize = true;
            this.steps_min_label.Location = new System.Drawing.Point(7, 82);
            this.steps_min_label.Name = "steps_min_label";
            this.steps_min_label.Size = new System.Drawing.Size(143, 31);
            this.steps_min_label.TabIndex = 1;
            this.steps_min_label.Text = "Steps/Min:";
            // 
            // steps_label
            // 
            this.steps_label.AutoSize = true;
            this.steps_label.Location = new System.Drawing.Point(7, 51);
            this.steps_label.Name = "steps_label";
            this.steps_label.Size = new System.Drawing.Size(92, 31);
            this.steps_label.TabIndex = 0;
            this.steps_label.Text = "Steps:";
            // 
            // stats_main_menu_button
            // 
            this.stats_main_menu_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stats_main_menu_button.Location = new System.Drawing.Point(-4, 476);
            this.stats_main_menu_button.Name = "stats_main_menu_button";
            this.stats_main_menu_button.Size = new System.Drawing.Size(315, 141);
            this.stats_main_menu_button.TabIndex = 5;
            this.stats_main_menu_button.Text = "Main Menu";
            this.stats_main_menu_button.UseVisualStyleBackColor = true;
            this.stats_main_menu_button.Click += new System.EventHandler(this.stats_main_menu_button_Click);
            // 
            // new_timer
            // 
            this.new_timer.Interval = 1000;
            this.new_timer.Tick += new System.EventHandler(this.new_timer_Tick);
            // 
            // Statistics
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 615);
            this.Controls.Add(this.stats_main_menu_button);
            this.Controls.Add(this.past_stats_panel);
            this.Controls.Add(this.live_stats_panel);
            this.Name = "Statistics";
            this.Text = "Statistics";
            this.past_stats_panel.ResumeLayout(false);
            this.past_stats_panel.PerformLayout();
            this.live_stats_panel.ResumeLayout(false);
            this.live_stats_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox past_stats_panel;
        private System.Windows.Forms.GroupBox live_stats_panel;
        private System.Windows.Forms.Label distance_label;
        private System.Windows.Forms.Label time_walking_label;
        private System.Windows.Forms.Label steps_min_label;
        private System.Windows.Forms.Label steps_label;
        private System.Windows.Forms.Label distance_value;
        private System.Windows.Forms.Label time_walking_value;
        public System.Windows.Forms.Label steps_min_value;
        private System.Windows.Forms.Label steps_value;
        private System.Windows.Forms.Label past_steps_min_value;
        private System.Windows.Forms.Label past_steps_value;
        private System.Windows.Forms.Label past_steps_min_label;
        private System.Windows.Forms.Label past_steps_label;
        private System.Windows.Forms.ComboBox past_stats_comboBox;
        private System.Windows.Forms.Button stats_main_menu_button;
        public System.Windows.Forms.Timer new_timer;
    }
}