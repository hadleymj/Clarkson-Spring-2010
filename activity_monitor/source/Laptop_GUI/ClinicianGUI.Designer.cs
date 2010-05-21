namespace Laptop_GUI
{
    partial class ClinicianGUI
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

        public string configFilePath = "C:\\ActivityMonitor\\ActivityMonitor.cfg";
        public string activityMonitorDirectory = "C:\\ActivityMonitor";

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.clinician_setup_panel = new System.Windows.Forms.GroupBox();
            this.clinican_GUI_close_button = new System.Windows.Forms.Button();
            this.connection_test_button = new System.Windows.Forms.Button();
            this.save_setup_button = new System.Windows.Forms.Button();
            this.hostname_textbox = new System.Windows.Forms.TextBox();
            this.password_textbox = new System.Windows.Forms.TextBox();
            this.username_textbox = new System.Windows.Forms.TextBox();
            this.patient_name_textbox = new System.Windows.Forms.TextBox();
            this.patient_name_label = new System.Windows.Forms.Label();
            this.username_label = new System.Windows.Forms.Label();
            this.hostname_label = new System.Windows.Forms.Label();
            this.password_label = new System.Windows.Forms.Label();
            this.clinician_setup_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // clinician_setup_panel
            // 
            this.clinician_setup_panel.BackColor = System.Drawing.SystemColors.Info;
            this.clinician_setup_panel.Controls.Add(this.clinican_GUI_close_button);
            this.clinician_setup_panel.Controls.Add(this.connection_test_button);
            this.clinician_setup_panel.Controls.Add(this.save_setup_button);
            this.clinician_setup_panel.Controls.Add(this.hostname_textbox);
            this.clinician_setup_panel.Controls.Add(this.password_textbox);
            this.clinician_setup_panel.Controls.Add(this.username_textbox);
            this.clinician_setup_panel.Controls.Add(this.patient_name_textbox);
            this.clinician_setup_panel.Controls.Add(this.patient_name_label);
            this.clinician_setup_panel.Controls.Add(this.username_label);
            this.clinician_setup_panel.Controls.Add(this.hostname_label);
            this.clinician_setup_panel.Controls.Add(this.password_label);
            this.clinician_setup_panel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clinician_setup_panel.Location = new System.Drawing.Point(77, 92);
            this.clinician_setup_panel.Name = "clinician_setup_panel";
            this.clinician_setup_panel.Size = new System.Drawing.Size(495, 495);
            this.clinician_setup_panel.TabIndex = 4;
            this.clinician_setup_panel.TabStop = false;
            this.clinician_setup_panel.Text = "Patient Setup";
            // 
            // clinican_GUI_close_button
            // 
            this.clinican_GUI_close_button.Location = new System.Drawing.Point(141, 439);
            this.clinican_GUI_close_button.Name = "clinican_GUI_close_button";
            this.clinican_GUI_close_button.Size = new System.Drawing.Size(214, 54);
            this.clinican_GUI_close_button.TabIndex = 7;
            this.clinican_GUI_close_button.Text = "Close";
            this.clinican_GUI_close_button.UseVisualStyleBackColor = true;
            this.clinican_GUI_close_button.Click += new System.EventHandler(this.clinican_GUI_close_button_Click);
            // 
            // connection_test_button
            // 
            this.connection_test_button.Location = new System.Drawing.Point(275, 379);
            this.connection_test_button.Name = "connection_test_button";
            this.connection_test_button.Size = new System.Drawing.Size(214, 54);
            this.connection_test_button.TabIndex = 6;
            this.connection_test_button.Text = "Test";
            this.connection_test_button.UseVisualStyleBackColor = true;
            this.connection_test_button.Click += new System.EventHandler(this.connection_test_button_Click);
            // 
            // save_setup_button
            // 
            this.save_setup_button.Location = new System.Drawing.Point(6, 379);
            this.save_setup_button.Name = "save_setup_button";
            this.save_setup_button.Size = new System.Drawing.Size(214, 54);
            this.save_setup_button.TabIndex = 5;
            this.save_setup_button.Text = "Save";
            this.save_setup_button.UseVisualStyleBackColor = true;
            this.save_setup_button.Click += new System.EventHandler(this.save_setup_button_Click);
            // 
            // hostname_textbox
            // 
            this.hostname_textbox.Location = new System.Drawing.Point(239, 281);
            this.hostname_textbox.Name = "hostname_textbox";
            this.hostname_textbox.Size = new System.Drawing.Size(214, 38);
            this.hostname_textbox.TabIndex = 4;
            // 
            // password_textbox
            // 
            this.password_textbox.Location = new System.Drawing.Point(239, 210);
            this.password_textbox.MaxLength = 32;
            this.password_textbox.Name = "password_textbox";
            this.password_textbox.PasswordChar = '*';
            this.password_textbox.Size = new System.Drawing.Size(214, 38);
            this.password_textbox.TabIndex = 3;
            // 
            // username_textbox
            // 
            this.username_textbox.Location = new System.Drawing.Point(239, 145);
            this.username_textbox.MaxLength = 20;
            this.username_textbox.Name = "username_textbox";
            this.username_textbox.Size = new System.Drawing.Size(214, 38);
            this.username_textbox.TabIndex = 2;
            // 
            // patient_name_textbox
            // 
            this.patient_name_textbox.Location = new System.Drawing.Point(239, 72);
            this.patient_name_textbox.MaxLength = 40;
            this.patient_name_textbox.Name = "patient_name_textbox";
            this.patient_name_textbox.Size = new System.Drawing.Size(214, 38);
            this.patient_name_textbox.TabIndex = 1;
            // 
            // patient_name_label
            // 
            this.patient_name_label.AutoSize = true;
            this.patient_name_label.Location = new System.Drawing.Point(15, 75);
            this.patient_name_label.Name = "patient_name_label";
            this.patient_name_label.Size = new System.Drawing.Size(186, 31);
            this.patient_name_label.TabIndex = 3;
            this.patient_name_label.Text = "Patient Name:";
            // 
            // username_label
            // 
            this.username_label.AutoSize = true;
            this.username_label.Location = new System.Drawing.Point(15, 148);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(147, 31);
            this.username_label.TabIndex = 2;
            this.username_label.Text = "Username:";
            // 
            // hostname_label
            // 
            this.hostname_label.AutoSize = true;
            this.hostname_label.Location = new System.Drawing.Point(15, 284);
            this.hostname_label.Name = "hostname_label";
            this.hostname_label.Size = new System.Drawing.Size(146, 31);
            this.hostname_label.TabIndex = 1;
            this.hostname_label.Text = "Hostname:";
            // 
            // password_label
            // 
            this.password_label.AutoSize = true;
            this.password_label.Location = new System.Drawing.Point(15, 213);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(142, 31);
            this.password_label.TabIndex = 0;
            this.password_label.Text = "Password:";
            // 
            // ClinicianGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 615);
            this.ControlBox = false;
            this.Controls.Add(this.clinician_setup_panel);
            this.Name = "ClinicianGUI";
            this.Text = "Clinician Set Up";
            this.clinician_setup_panel.ResumeLayout(false);
            this.clinician_setup_panel.PerformLayout();
            this.ResumeLayout(false);
            this.CenterToScreen();

        }

        #endregion

        private System.Windows.Forms.GroupBox clinician_setup_panel;
        private System.Windows.Forms.Label patient_name_label;
        private System.Windows.Forms.Label username_label;
        private System.Windows.Forms.Label hostname_label;
        private System.Windows.Forms.Label password_label;
        public System.Windows.Forms.TextBox hostname_textbox;
        public System.Windows.Forms.TextBox password_textbox;
        public System.Windows.Forms.TextBox username_textbox;
        public System.Windows.Forms.TextBox patient_name_textbox;
        private System.Windows.Forms.Button save_setup_button;
        private System.Windows.Forms.Button connection_test_button;
        private System.Windows.Forms.Button clinican_GUI_close_button;

    }
}