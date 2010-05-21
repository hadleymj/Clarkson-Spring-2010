namespace PDA_GUI
{
    partial class ClinicianGUI
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
            this.clinician_setup_panel = new System.Windows.Forms.Panel();
            this.clinician_GUI_close_button = new System.Windows.Forms.Button();
            this.connection_test_button = new System.Windows.Forms.Button();
            this.save_setup_button = new System.Windows.Forms.Button();
            this.hostname_label = new System.Windows.Forms.Label();
            this.password_label = new System.Windows.Forms.Label();
            this.username_label = new System.Windows.Forms.Label();
            this.hostname_textbox = new System.Windows.Forms.TextBox();
            this.password_textbox = new System.Windows.Forms.TextBox();
            this.username_textbox = new System.Windows.Forms.TextBox();
            this.patient_name_textbox = new System.Windows.Forms.TextBox();
            this.patient_name_label = new System.Windows.Forms.Label();
            this.patient_setup_label = new System.Windows.Forms.Label();
            this.clinician_setup_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // clinician_setup_panel
            // 
            this.clinician_setup_panel.BackColor = System.Drawing.SystemColors.Info;
            this.clinician_setup_panel.Controls.Add(this.clinician_GUI_close_button);
            this.clinician_setup_panel.Controls.Add(this.connection_test_button);
            this.clinician_setup_panel.Controls.Add(this.save_setup_button);
            this.clinician_setup_panel.Controls.Add(this.hostname_label);
            this.clinician_setup_panel.Controls.Add(this.password_label);
            this.clinician_setup_panel.Controls.Add(this.username_label);
            this.clinician_setup_panel.Controls.Add(this.hostname_textbox);
            this.clinician_setup_panel.Controls.Add(this.password_textbox);
            this.clinician_setup_panel.Controls.Add(this.username_textbox);
            this.clinician_setup_panel.Controls.Add(this.patient_name_textbox);
            this.clinician_setup_panel.Controls.Add(this.patient_name_label);
            this.clinician_setup_panel.Controls.Add(this.patient_setup_label);
            this.clinician_setup_panel.Location = new System.Drawing.Point(0, 0);
            this.clinician_setup_panel.Name = "clinician_setup_panel";
            this.clinician_setup_panel.Size = new System.Drawing.Size(225, 289);
            // 
            // clinician_GUI_close_button
            // 
            this.clinician_GUI_close_button.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.clinician_GUI_close_button.Location = new System.Drawing.Point(61, 236);
            this.clinician_GUI_close_button.Name = "clinician_GUI_close_button";
            this.clinician_GUI_close_button.Size = new System.Drawing.Size(102, 48);
            this.clinician_GUI_close_button.TabIndex = 7;
            this.clinician_GUI_close_button.Text = "Close";
            this.clinician_GUI_close_button.Click += new System.EventHandler(this.clinician_GUI_close_button_Click);
            // 
            // connection_test_button
            // 
            this.connection_test_button.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.connection_test_button.Location = new System.Drawing.Point(117, 182);
            this.connection_test_button.Name = "connection_test_button";
            this.connection_test_button.Size = new System.Drawing.Size(102, 48);
            this.connection_test_button.TabIndex = 6;
            this.connection_test_button.Text = "Test";
            this.connection_test_button.Click += new System.EventHandler(this.connection_test_button_Click);
            // 
            // save_setup_button
            // 
            this.save_setup_button.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.save_setup_button.Location = new System.Drawing.Point(0, 182);
            this.save_setup_button.Name = "save_setup_button";
            this.save_setup_button.Size = new System.Drawing.Size(102, 48);
            this.save_setup_button.TabIndex = 5;
            this.save_setup_button.Text = "Save";
            this.save_setup_button.Click += new System.EventHandler(this.save_setup_button_Click);
            // 
            // hostname_label
            // 
            this.hostname_label.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.hostname_label.Location = new System.Drawing.Point(3, 130);
            this.hostname_label.Name = "hostname_label";
            this.hostname_label.Size = new System.Drawing.Size(85, 26);
            this.hostname_label.Text = "Hostname:";
            this.hostname_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // password_label
            // 
            this.password_label.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.password_label.Location = new System.Drawing.Point(3, 94);
            this.password_label.Name = "password_label";
            this.password_label.Size = new System.Drawing.Size(77, 26);
            this.password_label.Text = "Password:";
            this.password_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // username_label
            // 
            this.username_label.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.username_label.Location = new System.Drawing.Point(3, 56);
            this.username_label.Name = "username_label";
            this.username_label.Size = new System.Drawing.Size(85, 26);
            this.username_label.Text = "Username:";
            this.username_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // hostname_textbox
            // 
            this.hostname_textbox.Location = new System.Drawing.Point(88, 135);
            this.hostname_textbox.Name = "hostname_textbox";
            this.hostname_textbox.Size = new System.Drawing.Size(131, 21);
            this.hostname_textbox.TabIndex = 4;
            // 
            // password_textbox
            // 
            this.password_textbox.Location = new System.Drawing.Point(88, 99);
            this.password_textbox.Name = "password_textbox";
            this.password_textbox.PasswordChar = '*';
            this.password_textbox.Size = new System.Drawing.Size(131, 21);
            this.password_textbox.TabIndex = 3;
            this.password_textbox.TextChanged += new System.EventHandler(this.password_textbox_TextChanged);
            // 
            // username_textbox
            // 
            this.username_textbox.Location = new System.Drawing.Point(88, 61);
            this.username_textbox.Name = "username_textbox";
            this.username_textbox.Size = new System.Drawing.Size(131, 21);
            this.username_textbox.TabIndex = 2;
            // 
            // patient_name_textbox
            // 
            this.patient_name_textbox.Location = new System.Drawing.Point(88, 27);
            this.patient_name_textbox.Name = "patient_name_textbox";
            this.patient_name_textbox.Size = new System.Drawing.Size(131, 21);
            this.patient_name_textbox.TabIndex = 1;
            // 
            // patient_name_label
            // 
            this.patient_name_label.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.patient_name_label.Location = new System.Drawing.Point(3, 24);
            this.patient_name_label.Name = "patient_name_label";
            this.patient_name_label.Size = new System.Drawing.Size(60, 21);
            this.patient_name_label.Text = "Name:";
            this.patient_name_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // patient_setup_label
            // 
            this.patient_setup_label.BackColor = System.Drawing.Color.Silver;
            this.patient_setup_label.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.patient_setup_label.Location = new System.Drawing.Point(0, 0);
            this.patient_setup_label.Name = "patient_setup_label";
            this.patient_setup_label.Size = new System.Drawing.Size(240, 24);
            this.patient_setup_label.Text = "Patient Setup";
            this.patient_setup_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // ClinicianGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.clinician_setup_panel);
            this.KeyPreview = true;
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "ClinicianGUI";
            this.Text = "ClinicianGUI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ClinicianGUI_KeyDown);
            this.clinician_setup_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel clinician_setup_panel;
        private System.Windows.Forms.Label patient_name_label;
        private System.Windows.Forms.Label patient_setup_label;
        private System.Windows.Forms.Label hostname_label;
        private System.Windows.Forms.Label password_label;
        private System.Windows.Forms.Label username_label;
        public System.Windows.Forms.TextBox hostname_textbox;
        public System.Windows.Forms.TextBox password_textbox;
        public System.Windows.Forms.TextBox username_textbox;
        public System.Windows.Forms.TextBox patient_name_textbox;
        private System.Windows.Forms.Button save_setup_button;
        private System.Windows.Forms.Button clinician_GUI_close_button;
        private System.Windows.Forms.Button connection_test_button;
    }
}