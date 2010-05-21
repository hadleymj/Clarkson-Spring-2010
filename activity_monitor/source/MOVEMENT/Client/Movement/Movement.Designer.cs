namespace Movement.UserInterface
{
    partial class Movement
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
        /// 

        private void InitializeLogin(){
            this.btn_connect = new System.Windows.Forms.Button();
            this.cmb_server = new System.Windows.Forms.ComboBox();
            this.lbl_server_name = new System.Windows.Forms.Label();
            this.txt_password = new System.Windows.Forms.TextBox();
            this.txt_username = new System.Windows.Forms.TextBox();
            this.lbl_password = new System.Windows.Forms.Label();
            this.lbl_user = new System.Windows.Forms.Label();
            this.grp_login = new System.Windows.Forms.GroupBox();

            // 
            // grp_login
            // 
            this.grp_login.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_login.Controls.Add(this.txt_username);
            this.grp_login.Controls.Add(this.lbl_password);
            this.grp_login.Controls.Add(this.lbl_user);
            this.grp_login.Controls.Add(this.cmb_server);
            this.grp_login.Controls.Add(this.lbl_server_name);
            this.grp_login.Controls.Add(this.txt_password);            
            this.grp_login.Location = new System.Drawing.Point(52, 96);
            this.grp_login.Name = "grp_clinician_tasks";
            this.grp_login.Size = new System.Drawing.Size(354, 141);
            this.grp_login.TabIndex = 1;
            this.grp_login.TabStop = false;
            this.grp_login.Text = "Login Information";
            // 
            // cmb_server
            // 
            this.cmb_server.FormattingEnabled = true;
            this.cmb_server.Location = new System.Drawing.Point(97,40);
            this.cmb_server.Name = "cmb_server";
            this.cmb_server.Size = new System.Drawing.Size(220, 20);
            this.cmb_server.TabIndex = 2;
            // 
            // lbl_server_name
            //
            this.lbl_server_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_server_name.AutoSize = true;
            this.lbl_server_name.Location = new System.Drawing.Point(25,43);
            this.lbl_server_name.Name = "lbl_server_name";
            this.lbl_server_name.Size = new System.Drawing.Size(70, 13);
            this.lbl_server_name.TabIndex = 6;
            this.lbl_server_name.Text = "Server name:";
            // 
            // txt_username
            // 
            this.txt_username.Location = new System.Drawing.Point(97, 67);
            this.txt_username.Name = "txt_username";
            this.txt_username.Size = new System.Drawing.Size(200, 18);
            this.txt_username.TabIndex = 3;
            // 
            // lbl_user
            // 
            this.lbl_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_user.AutoSize = true;
            this.lbl_user.Location = new System.Drawing.Point(36, 70);
            this.lbl_user.Name = "lbl_user";
            this.lbl_user.Size = new System.Drawing.Size(32, 13);
            this.lbl_user.TabIndex = 6;
            this.lbl_user.Text = "Username:";

            // 
            // txt_password
            // 
            this.txt_password.Location = new System.Drawing.Point(97,93);
            this.txt_password.Name = "txt_password";
            this.txt_password.PasswordChar = '*';
            this.txt_password.Size = new System.Drawing.Size(200, 18);
            this.txt_password.TabIndex = 4;
            // 
            // lbl_password
            // 
            this.lbl_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_password.AutoSize = true;
            this.lbl_password.Location = new System.Drawing.Point(38, 96);
            this.lbl_password.Name = "lbl_password";
            this.lbl_password.Size = new System.Drawing.Size(56, 13);
            this.lbl_password.TabIndex = 7;
            this.lbl_password.Text = "Password:";

            //
            // btn_connect
            //
            this.btn_connect.Location = new System.Drawing.Point(385, 365);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(75, 23);
            this.btn_connect.TabIndex = 5;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            //this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
        }

        private void InitializeClinicianTasks()
        {
            this.grp_clinician_tasks = new System.Windows.Forms.GroupBox();
            this.opt_patient_history = new System.Windows.Forms.RadioButton();
            this.opt_test_patient = new System.Windows.Forms.RadioButton();
            this.opt_practice_test = new System.Windows.Forms.RadioButton();
            this.opt_patient_information = new System.Windows.Forms.RadioButton();
            // 
            // grp_clinician_tasks
            // 
            this.grp_clinician_tasks.Controls.Add(this.opt_test_patient);
            this.grp_clinician_tasks.Controls.Add(this.opt_practice_test);
            this.grp_clinician_tasks.Controls.Add(this.opt_patient_history);
            this.grp_clinician_tasks.Controls.Add(this.opt_patient_information);
            this.grp_clinician_tasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_clinician_tasks.Location = new System.Drawing.Point(50, 65);
            this.grp_clinician_tasks.Name = "grp_clinician_tasks";
            this.grp_clinician_tasks.Size = new System.Drawing.Size(275, 120);
            this.grp_clinician_tasks.TabIndex = 1;
            this.grp_clinician_tasks.TabStop = false;
            this.grp_clinician_tasks.Text = "Patient Tasks";
            // 
            // opt_test_patient
            // 
            this.opt_test_patient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt_test_patient.Location = new System.Drawing.Point(20, 35);
            this.opt_test_patient.Name = "opt_test_patient";
            this.opt_test_patient.AutoSize = true;
            this.opt_test_patient.TabIndex = 2;
            this.opt_test_patient.TabStop = true;
            this.opt_test_patient.Text = "Test Patient";
            this.opt_test_patient.UseVisualStyleBackColor = true;
            // 
            // opt_practice_test
            // 
            this.opt_practice_test.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt_practice_test.Location = new System.Drawing.Point(125, 35);
            this.opt_practice_test.Name = "opt_practice_test";
            this.opt_practice_test.AutoSize = true;
            this.opt_practice_test.TabIndex = 4;
            this.opt_practice_test.TabStop = true;
            this.opt_practice_test.Text = "Practice Test";
            this.opt_practice_test.UseVisualStyleBackColor = true;
            // 
            // opt_patient_history
            // 
            this.opt_patient_history.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt_patient_history.Location = new System.Drawing.Point(20, 70);
            this.opt_patient_history.Name = "opt_patient_history";
            this.opt_patient_history.AutoSize = true;
            this.opt_patient_history.TabIndex = 3;
            this.opt_patient_history.TabStop = true;
            this.opt_patient_history.Text = "Patient History";
            this.opt_patient_history.UseVisualStyleBackColor = true;
            // 
            // opt_patient_information
            // 
            this.opt_patient_information.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt_patient_information.Location = new System.Drawing.Point(125, 70);
            this.opt_patient_information.Name = "opt_patient_information";
            this.opt_patient_information.AutoSize = true;
            this.opt_patient_information.TabIndex = 5;
            this.opt_patient_information.TabStop = true;
            this.opt_patient_information.Text = "Patient Information";
            this.opt_patient_information.UseVisualStyleBackColor = true;

        }

        private void InitializePatientInformation()
        {
            this.grp_patient_information = new System.Windows.Forms.GroupBox();
            this.grp_patient_notes = new System.Windows.Forms.GroupBox();
            this.lbl_patient_sex = new System.Windows.Forms.Label();
            this.lbl_patient_notes = new System.Windows.Forms.Label();
            this.lbl_patient_dob = new System.Windows.Forms.Label();
            this.lbl_patient_handedness = new System.Windows.Forms.Label();
            this.lbl_patient_name = new System.Windows.Forms.Label();
            this.lbl_patient_contact = new System.Windows.Forms.Label();
            this.lbl_patient_address = new System.Windows.Forms.Label();
            this.btn_add_note = new System.Windows.Forms.Button();
            this.pnl_patient_notes = new System.Windows.Forms.Panel();

            // 
            // grp_patient_information
            // 
            this.grp_patient_information.Controls.Add(this.lbl_patient_contact);
            this.grp_patient_information.Controls.Add(this.lbl_patient_address);
            this.grp_patient_information.Controls.Add(this.lbl_patient_sex);
            this.grp_patient_information.Controls.Add(this.lbl_patient_dob);
            this.grp_patient_information.Controls.Add(this.lbl_patient_handedness);
            this.grp_patient_information.Controls.Add(this.lbl_patient_name);
            this.grp_patient_information.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_patient_information.Location = new System.Drawing.Point(20, 65);
            this.grp_patient_information.Name = "grp_patient_information";
            this.grp_patient_information.Size = new System.Drawing.Size(200, 161);
            this.grp_patient_information.AutoSize = true;
            this.grp_patient_information.TabIndex = 1;
            this.grp_patient_information.TabStop = false;
            this.grp_patient_information.Text = "Patient Information";
            // 
            // grp_patient_notes
            // 
            this.grp_patient_notes.Controls.Add(this.pnl_patient_notes);
            this.grp_patient_notes.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_patient_notes.Location = new System.Drawing.Point(250, 65);
            this.grp_patient_notes.Name = "grp_patient_notes";
            this.grp_patient_notes.Size = new System.Drawing.Size(200, 161);
            this.grp_patient_notes.AutoSize = true;
            this.grp_patient_notes.TabIndex = 1;
            this.grp_patient_notes.TabStop = false;
            this.grp_patient_notes.MaximumSize = new System.Drawing.Size(200, 225);
            this.grp_patient_notes.Text = "Patient Notes";
            // 
            // lbl_patient_sex
            // 
            this.lbl_patient_sex.AutoSize = true;
            this.lbl_patient_sex.Location = new System.Drawing.Point(10, 107);
            this.lbl_patient_sex.Name = "lbl_patient_sex";
            this.lbl_patient_sex.Size = new System.Drawing.Size(45, 15);
            this.lbl_patient_sex.TabIndex = 3;
            this.lbl_patient_sex.Text = "";
            // 
            // lbl_patient_dob
            // 
            this.lbl_patient_dob.AutoSize = true;
            this.lbl_patient_dob.Location = new System.Drawing.Point(10, 81);
            this.lbl_patient_dob.Name = "lbl_patient_dob";
            this.lbl_patient_dob.Size = new System.Drawing.Size(104, 15);
            this.lbl_patient_dob.TabIndex = 2;
            this.lbl_patient_dob.Text = "";
            // 
            // lbl_patient_handedness
            // 
            this.lbl_patient_handedness.AutoSize = true;
            this.lbl_patient_handedness.Location = new System.Drawing.Point(10, 56);
            this.lbl_patient_handedness.Name = "lbl_patient_handedness";
            this.lbl_patient_handedness.Size = new System.Drawing.Size(106, 15);
            this.lbl_patient_handedness.TabIndex = 1;
            this.lbl_patient_handedness.Text = "";
            // 
            // lbl_patient_name
            // 
            this.lbl_patient_name.AutoSize = true;
            this.lbl_patient_name.Location = new System.Drawing.Point(10, 29);
            this.lbl_patient_name.Name = "lbl_patient_name";
            this.lbl_patient_name.Size = new System.Drawing.Size(189, 15);
            this.lbl_patient_name.TabIndex = 0;
            this.lbl_patient_name.Text = "";
            // 
            // lbl_patient_contact
            // 
            this.lbl_patient_contact.AutoSize = true;
            this.lbl_patient_contact.Location = new System.Drawing.Point(10, 133);
            this.lbl_patient_contact.Name = "lbl_patient_contact";
            this.lbl_patient_contact.Size = new System.Drawing.Size(189, 15);
            this.lbl_patient_contact.TabIndex = 0;
            this.lbl_patient_contact.Text = "";
            // 
            // lbl_patient_address
            // 
            this.lbl_patient_address.AutoSize = true;
            this.lbl_patient_address.Location = new System.Drawing.Point(10, 133);
            this.lbl_patient_address.Name = "lbl_patient_address";
            this.lbl_patient_address.Size = new System.Drawing.Size(189, 15);
            this.lbl_patient_address.TabIndex = 0;
            this.lbl_patient_address.Text = "";
            // 
            // lbl_patient_notes
            // 
            this.lbl_patient_notes.AutoSize = true;
            this.lbl_patient_notes.Location = new System.Drawing.Point(5, 5);
            this.lbl_patient_notes.Name = "lbl_patient_notes";
            this.lbl_patient_notes.TabIndex = 0;
            this.lbl_patient_notes.Text = "";
            this.lbl_patient_notes.MaximumSize = new System.Drawing.Size(180, 2147483647);
            // 
            // pnl_patient_notes
            // 
            this.pnl_patient_notes.AutoSize = true;
            this.pnl_patient_notes.BackColor = System.Drawing.SystemColors.Control;
            this.pnl_patient_notes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.pnl_patient_notes.Controls.Add(this.lbl_patient_notes);
            this.pnl_patient_notes.Location = new System.Drawing.Point(250, 65);
            this.pnl_patient_notes.Name = "pnl_patient_notes";
            this.pnl_patient_notes.Size = new System.Drawing.Size(200, 225);
            this.pnl_patient_notes.MaximumSize = new System.Drawing.Size(200, 225);
            this.pnl_patient_notes.AutoScroll = true;
            this.pnl_patient_notes.TabIndex = 80;
            // 
            // btn_add_note
            // 
            this.btn_add_note.Location = new System.Drawing.Point(225, 365);
            this.btn_add_note.Name = "btn_add_note";
            this.btn_add_note.Size = new System.Drawing.Size(75, 23);
            this.btn_add_note.TabIndex = 13;
            this.btn_add_note.Text = "Add Note";
            this.btn_add_note.UseVisualStyleBackColor = true;
            this.btn_add_note.Click += new System.EventHandler(this.btn_add_note_Click);
        }

        private void InitializePostLogin()
        {
            this.opt_add_patient = new System.Windows.Forms.RadioButton();
            this.opt_retrieve_patient = new System.Windows.Forms.RadioButton();
            this.opt_create_batch = new System.Windows.Forms.RadioButton();
            this.opt_create_user = new System.Windows.Forms.RadioButton();
            this.grp_tasks = new System.Windows.Forms.GroupBox();
            this.grp_tasks.SuspendLayout();
            this.lbl_administrative_tasks = new System.Windows.Forms.Label();
            this.lbl_clinician_tasks = new System.Windows.Forms.Label();

            // 
            // grp_tasks
            // 
            this.grp_tasks.Controls.Add(this.opt_add_patient);
            this.grp_tasks.Controls.Add(this.lbl_clinician_tasks);
            this.grp_tasks.Controls.Add(this.opt_retrieve_patient);
            this.grp_tasks.Controls.Add(this.lbl_administrative_tasks);
            this.grp_tasks.Controls.Add(this.opt_create_user);
            this.grp_tasks.Controls.Add(this.opt_create_batch);

            this.grp_tasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_tasks.Location = new System.Drawing.Point(50, 95);
            this.grp_tasks.Name = "grp_tasks";
            this.grp_tasks.Size = new System.Drawing.Size(355, 220);
            this.grp_tasks.TabIndex = 12;
            this.grp_tasks.TabStop = false;
            this.grp_tasks.Text = "Tasks";
            // 
            // opt_add_patient
            // 
            this.opt_add_patient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt_add_patient.Location = new System.Drawing.Point(55, 65);
            this.opt_add_patient.Name = "opt_add_patient";
            this.opt_add_patient.AutoSize = true;
            this.opt_add_patient.TabIndex = 1;
            this.opt_add_patient.TabStop = true;
            this.opt_add_patient.Text = "Add Patient";
            this.opt_add_patient.UseVisualStyleBackColor = true;
            // 
            // opt_retrieve_patient
            // 
            this.opt_retrieve_patient.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt_retrieve_patient.Location = new System.Drawing.Point(opt_add_patient.Location.X+110, opt_add_patient.Location.Y);
            this.opt_retrieve_patient.Name = "opt_retrieve_patient";
            this.opt_retrieve_patient.AutoSize = true;
            this.opt_retrieve_patient.TabIndex = 2;
            this.opt_retrieve_patient.TabStop = true;
            this.opt_retrieve_patient.Text = "Patient Tasks";
            this.opt_retrieve_patient.UseVisualStyleBackColor = true;
            // 
            // opt_create_user
            // 
            this.opt_create_user.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt_create_user.Location = new System.Drawing.Point(opt_add_patient.Location.X, 160);
            this.opt_create_user.Name = "opt_create_user";
            this.opt_create_user.AutoSize = true;
            this.opt_create_user.TabIndex = 5;
            this.opt_create_user.TabStop = true;
            this.opt_create_user.Text = "Create User";
            this.opt_create_user.UseVisualStyleBackColor = true;
            // 
            // opt_create_batch
            // 
            this.opt_create_batch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.opt_create_batch.Location = new System.Drawing.Point(opt_retrieve_patient.Location.X, opt_create_user.Location.Y);
            this.opt_create_batch.Name = "opt_create_batch";
            this.opt_create_batch.AutoSize = true;
            this.opt_create_batch.TabIndex = 6;
            this.opt_create_batch.TabStop = true;
            this.opt_create_batch.Text = "Create Batch";
            this.opt_create_batch.UseVisualStyleBackColor = true;
            // 
            // lbl_administrative_tasks
            // 
            this.lbl_administrative_tasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_administrative_tasks.Image = Properties.Resources.settings32;
            this.lbl_administrative_tasks.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lbl_administrative_tasks.Location = new System.Drawing.Point(35, 120);
            this.lbl_administrative_tasks.Name = "lbl_administrative_tasks";
            this.lbl_administrative_tasks.Size = new System.Drawing.Size(115, 34);
            this.lbl_administrative_tasks.TabIndex = 10;
            this.lbl_administrative_tasks.Text = "Administrator";
            this.lbl_administrative_tasks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lbl_clinician_tasks
            // 
            this.lbl_clinician_tasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_clinician_tasks.Image = Properties.Resources.movement32;
            this.lbl_clinician_tasks.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lbl_clinician_tasks.Location = new System.Drawing.Point(35, 25);
            this.lbl_clinician_tasks.Name = "lbl_clinician_tasks";
            this.lbl_clinician_tasks.Size = new System.Drawing.Size(95, 34);
            this.lbl_clinician_tasks.TabIndex = 9;
            this.lbl_clinician_tasks.Text = "Clinician";
            this.lbl_clinician_tasks.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
        }

        private void InitializeAddPatient()
        {
            this.pnl_handedness = new System.Windows.Forms.Panel();
            this.pnl_sex = new System.Windows.Forms.Panel();
            this.rdbtn_female = new System.Windows.Forms.RadioButton();
            this.rdbtn_male = new System.Windows.Forms.RadioButton();
            this.rdbtn_left = new System.Windows.Forms.RadioButton();
            this.rdbtn_right = new System.Windows.Forms.RadioButton();
            this.lbl_handedness = new System.Windows.Forms.Label();
            this.lbl_sex = new System.Windows.Forms.Label();
            this.lbl_notes = new System.Windows.Forms.Label();
            this.lbl_contact = new System.Windows.Forms.Label();
            this.lbl_add_patient_address = new System.Windows.Forms.Label();
            this.txt_patient_contact = new System.Windows.Forms.TextBox();
            this.txt_add_patient_address = new System.Windows.Forms.TextBox();
            this.txt_patient_notes = new System.Windows.Forms.TextBox();
            this.msk_patient_ssn = new System.Windows.Forms.MaskedTextBox();
            this.msk_patient_dob = new System.Windows.Forms.MaskedTextBox();
            this.lbl_patient_ssn = new System.Windows.Forms.Label();
            this.lbl_add_patient_dob = new System.Windows.Forms.Label();
            this.lbl_add_patient_name = new System.Windows.Forms.Label();
            this.txt_patient_name = new System.Windows.Forms.TextBox();

            // 
            // pnl_handedness
            // 
            this.pnl_handedness.Controls.Add(this.rdbtn_left);
            this.pnl_handedness.Controls.Add(this.rdbtn_right);
            this.pnl_handedness.Location = new System.Drawing.Point(314, 56);
            this.pnl_handedness.Name = "pnl_handedness";
            this.pnl_handedness.Size = new System.Drawing.Size(103, 18);
            this.pnl_handedness.TabIndex = 4;
            // 
            // pnl_sex
            // 
            this.pnl_sex.Controls.Add(this.rdbtn_male);
            this.pnl_sex.Controls.Add(this.rdbtn_female);
            this.pnl_sex.Location = new System.Drawing.Point(314, 83);
            this.pnl_sex.Name = "pnl_sex";
            this.pnl_sex.Size = new System.Drawing.Size(112, 18);
            this.pnl_sex.TabIndex = 5;
            // 
            // rdbtn_left
            // 
            this.rdbtn_left.AutoSize = true;
            this.rdbtn_left.Location = new System.Drawing.Point(0, 0);
            this.rdbtn_left.Name = "rdbtn_left";
            this.rdbtn_left.TabIndex = 4;
            this.rdbtn_left.TabStop = true;
            this.rdbtn_left.Text = "Left";
            this.rdbtn_left.UseVisualStyleBackColor = true;
            this.rdbtn_left.Checked = true;
            // 
            // rdbtn_right
            // 
            this.rdbtn_right.AutoSize = true;
            this.rdbtn_right.Location = new System.Drawing.Point(50, 0);
            this.rdbtn_right.Name = "rdbtn_right";
            this.rdbtn_right.TabIndex = 5;
            this.rdbtn_right.TabStop = true;
            this.rdbtn_right.Text = "Right";
            this.rdbtn_right.UseVisualStyleBackColor = true;
            // 
            // lbl_handedness
            // 
            this.lbl_handedness.AutoSize = true;
            this.lbl_handedness.Location = new System.Drawing.Point(240, 61);
            this.lbl_handedness.Name = "lbl_handedness";
            this.lbl_handedness.TabIndex = 82;
            this.lbl_handedness.Text = "Handedness:";

            // 
            // rdbtn_male
            // 
            this.rdbtn_male.AutoSize = true;
            this.rdbtn_male.Location = new System.Drawing.Point(0, 0);
            this.rdbtn_male.Name = "rdbtn_male";
            this.rdbtn_male.TabIndex = 6;
            this.rdbtn_male.TabStop = true;
            this.rdbtn_male.Text = "Male";
            this.rdbtn_male.UseVisualStyleBackColor = true;
            this.rdbtn_male.Checked = true;
            // 
            // rdbtn_female
            // 
            this.rdbtn_female.AutoSize = true;
            this.rdbtn_female.Location = new System.Drawing.Point(50, 0);
            this.rdbtn_female.Name = "rdbtn_female";
            this.rdbtn_female.TabIndex = 7;
            this.rdbtn_female.TabStop = true;
            this.rdbtn_female.Text = "Female";
            this.rdbtn_female.UseVisualStyleBackColor = true;
            // 
            // lbl_sex
            // 
            this.lbl_sex.AutoSize = true;
            this.lbl_sex.Location = new System.Drawing.Point(240, 88);
            this.lbl_sex.Name = "lbl_sex";
            this.lbl_sex.TabIndex = 82;
            this.lbl_sex.Text = "Sex:";
            // 
            // lbl_contact
            // 
            this.lbl_contact.AutoSize = true;
            this.lbl_contact.Location = new System.Drawing.Point(12, 145);
            this.lbl_contact.Name = "lbl_contact";
            this.lbl_contact.Size = new System.Drawing.Size(105, 13);
            this.lbl_contact.TabIndex = 115;
            this.lbl_contact.Text = "Contact Information: ";
            // 
            // lbl_add_patient_address
            // 
            this.lbl_add_patient_address.AutoSize = true;
            this.lbl_add_patient_address.Location = new System.Drawing.Point(12, 208);
            this.lbl_add_patient_address.Name = "lbl_add_patient_address";
            this.lbl_add_patient_address.Size = new System.Drawing.Size(105, 13);
            this.lbl_add_patient_address.TabIndex = 115;
            this.lbl_add_patient_address.Text = "Address: ";
            // 
            // lbl_notes
            // 
            this.lbl_notes.AutoSize = true;
            this.lbl_notes.Location = new System.Drawing.Point(12, 272);
            this.lbl_notes.Name = "lbl_notes";
            this.lbl_notes.Size = new System.Drawing.Size(74, 13);
            this.lbl_notes.TabIndex = 95;
            this.lbl_notes.Text = "Patient Notes:";
            // 
            // txt_patient_contact
            // 
            this.txt_patient_contact.Location = new System.Drawing.Point(15, 160);
            this.txt_patient_contact.Multiline = true;
            this.txt_patient_contact.Name = "txt_patient_contact";
            this.txt_patient_contact.Size = new System.Drawing.Size(300, 40);
            this.txt_patient_contact.TabIndex = 8;
            // 
            // txt_add_patient_address
            // 
            this.txt_add_patient_address.Location = new System.Drawing.Point(15, 223);
            this.txt_add_patient_address.Multiline = true;
            this.txt_add_patient_address.Name = "txt_add_patient_address";
            this.txt_add_patient_address.Size = new System.Drawing.Size(300, 40);
            this.txt_add_patient_address.TabIndex = 9;
            // 
            // txt_patient_notes
            // 
            this.txt_patient_notes.Location = new System.Drawing.Point(15, 287);
            this.txt_patient_notes.Multiline = true;
            this.txt_patient_notes.Name = "txt_patient_notes";
            this.txt_patient_notes.Size = new System.Drawing.Size(300, 66);
            this.txt_patient_notes.TabIndex = 10;
            // 
            // txt_patient_name
            // 
            this.txt_patient_name.Location = new System.Drawing.Point(85, 60);
            this.txt_patient_name.MaxLength = 50;
            this.txt_patient_name.Name = "txt_patient_name";
            this.txt_patient_name.Size = new System.Drawing.Size(140, 20);
            this.txt_patient_name.TabIndex = 1;
            // 
            // lbl_add_patient_name
            // 
            this.lbl_add_patient_name.AutoSize = true;
            this.lbl_add_patient_name.Location = new System.Drawing.Point(12, 60);
            this.lbl_add_patient_name.Name = "lbl_add_patient_name";
            this.lbl_add_patient_name.TabIndex = 82;
            this.lbl_add_patient_name.Text = "Name:";

            // 
            // msk_patient_dob
            // 
            this.msk_patient_dob.Location = new System.Drawing.Point(85,85);
            this.msk_patient_dob.Mask = "00/00/0000";
            this.msk_patient_dob.Name = "msk_patient_dob";
            this.msk_patient_dob.Size = new System.Drawing.Size(75, 20);
            this.msk_patient_dob.TabIndex = 2;
            this.msk_patient_dob.ValidatingType = typeof(System.DateTime);
            // 
            // lbl_add_patient_dob
            // 
            this.lbl_add_patient_dob.AutoSize = true;
            this.lbl_add_patient_dob.Location = new System.Drawing.Point(12, 90);
            this.lbl_add_patient_dob.Name = "lbl_add_patient_dob";
            this.lbl_add_patient_dob.TabIndex = 83;
            this.lbl_add_patient_dob.Text = "DOB:";

            // 
            // msk_patient_ssn
            // 
            this.msk_patient_ssn.Location = new System.Drawing.Point(85,112);
            this.msk_patient_ssn.Mask = "0000";
            this.msk_patient_ssn.Name = "msk_patient_ssn";
            this.msk_patient_ssn.Size = new System.Drawing.Size(40, 20);
            this.msk_patient_ssn.TabIndex = 3;
            // 
            // lbl_patient_ssn
            // 
            this.lbl_patient_ssn.AutoSize = true;
            this.lbl_patient_ssn.Location = new System.Drawing.Point(12,115);
            this.lbl_patient_ssn.Name = "lbl_patient_ssn";
            this.lbl_patient_ssn.TabIndex = 92;
            this.lbl_patient_ssn.Text = "SSN: (last 4)";

        }

        private void InitializeRetrievePatient()
        {
            this.msk_ssn = new System.Windows.Forms.MaskedTextBox();
            this.msk_dob = new System.Windows.Forms.MaskedTextBox();
            this.lbl_ssn = new System.Windows.Forms.Label();
            this.lbl_dob = new System.Windows.Forms.Label();
            this.lbl_name = new System.Windows.Forms.Label();
            this.txt_name = new System.Windows.Forms.TextBox();
            this.lst_patients = new System.Windows.Forms.ListBox();
            this.btn_retrieve = new System.Windows.Forms.Button();
            
            // 
            // txt_name
            // 
            this.txt_name.Location = new System.Drawing.Point(90, 100);
            this.txt_name.MaxLength = 50;
            this.txt_name.Name = "txt_name";
            this.txt_name.Size = new System.Drawing.Size(150, 20);
            this.txt_name.TabIndex = 1;
            // 
            // lbl_name
            // 
            this.lbl_name.AutoSize = true;
            this.lbl_name.Location = new System.Drawing.Point(40, txt_name.Location.Y);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.TabIndex = 82;
            this.lbl_name.Text = "Name:";

            // 
            // msk_dob
            // 
            this.msk_dob.Location = new System.Drawing.Point(90, txt_name.Location.Y + 25);
            this.msk_dob.Mask = "00/00/0000";
            this.msk_dob.Name = "msk_dob";
            this.msk_dob.Size = new System.Drawing.Size(75, 20);
            this.msk_dob.TabIndex = 2;
            this.msk_dob.ValidatingType = typeof(System.DateTime);
            // 
            // lbl_dob
            // 
            this.lbl_dob.AutoSize = true;
            this.lbl_dob.Location = new System.Drawing.Point(36, msk_dob.Location.Y);
            this.lbl_dob.Name = "lbl_dob";
            this.lbl_dob.TabIndex = 83;
            this.lbl_dob.Text = "DOB:";

            // 
            // msk_ssn
            // 
            this.msk_ssn.Location = new System.Drawing.Point(90, msk_dob.Location.Y + 25);
            this.msk_ssn.Mask = "0000";
            this.msk_ssn.Name = "msk_ssn";
            this.msk_ssn.Size = new System.Drawing.Size(40, 20);
            this.msk_ssn.TabIndex = 3;
            // 
            // lbl_ssn
            // 
            this.lbl_ssn.AutoSize = true;
            this.lbl_ssn.Location = new System.Drawing.Point(6, msk_ssn.Location.Y);
            this.lbl_ssn.Name = "lbl_ssn";
            this.lbl_ssn.TabIndex = 92;
            this.lbl_ssn.Text = "SSN: (last 4)";

            // 
            // lst_patients
            // 
            this.lst_patients.FormattingEnabled = true;
            this.lst_patients.Location = new System.Drawing.Point(260, 100);
            this.lst_patients.Name = "lst_patients";
            this.lst_patients.Size = new System.Drawing.Size(185, 173);
            this.lst_patients.TabIndex = 5;
            this.lst_patients.ValueMember = "name";
            this.lst_patients.SelectedIndexChanged += new System.EventHandler(this.lst_patients_SelectedIndexChanged);

            //
            // btn_retrieve
            //
            this.btn_retrieve.Location = new System.Drawing.Point(10, msk_ssn.Location.Y+50);
            this.btn_retrieve.Name = "btn_retrieve";
            this.btn_retrieve.Size = new System.Drawing.Size(75, 23);
            this.btn_retrieve.TabIndex = 4;
            this.btn_retrieve.Text = "Retrieve";
            this.btn_retrieve.UseVisualStyleBackColor = true;
            this.btn_retrieve.Click += new System.EventHandler(this.btn_retrieve_Click);
        }

        private void InitializeBatchSelection()
        {
            this.cmb_batches = new System.Windows.Forms.ComboBox();
            this.lbl_description = new System.Windows.Forms.Label();
            this.lbl_batch_description = new System.Windows.Forms.Label();
            this.lbl_batch_label = new System.Windows.Forms.Label();

            // 
            // lbl_batch_label
            // 
            this.lbl_batch_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_batch_label.Location = new System.Drawing.Point(27, 65);
            this.lbl_batch_label.MinimumSize = new System.Drawing.Size(100, 0);
            this.lbl_batch_label.Name = "lbl_batch_label";
            this.lbl_batch_label.AutoSize = true;
            this.lbl_batch_label.TabIndex = 3;
            this.lbl_batch_label.Text = "Select a test batch:";
            // 
            // cmb_batches
            // 
            this.cmb_batches.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_batches.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmb_batches.FormattingEnabled = true;
            this.cmb_batches.Location = new System.Drawing.Point(30, 85);
            this.cmb_batches.Name = "cmb_batches";
            this.cmb_batches.TabIndex = 1;
            this.cmb_batches.Size = new System.Drawing.Size(200, 18);
            this.cmb_batches.Sorted = true;
            this.cmb_batches.ValueMember = "name";
            this.cmb_batches.SelectedIndexChanged += new System.EventHandler(this.cmb_batches_SelectedIndexChanged);
            // 
            // lbl_description
            // 
            this.lbl_description.AutoSize = true;
            this.lbl_description.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_description.Location = new System.Drawing.Point(27, 150);
            this.lbl_description.Name = "lbl_description";
            this.lbl_description.TabIndex = 2;
            this.lbl_description.Text = "Description:";
            // 
            // lbl_batch_description
            // 
            this.lbl_batch_description.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_batch_description.Location = new System.Drawing.Point(30, 170);
            this.lbl_batch_description.MinimumSize = new System.Drawing.Size(100, 0);
            this.lbl_batch_description.Name = "lbl_batch_description";
            this.lbl_batch_description.Size = new System.Drawing.Size(288, 74);
            this.lbl_batch_description.TabIndex = 3;

        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Movement));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_connect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_disconnect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu_help = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnu_about_movement = new System.Windows.Forms.ToolStripMenuItem();
            this.pnl_content = new System.Windows.Forms.Panel();
            this.lbl_instructions = new System.Windows.Forms.Label();
            this.pnl_header = new System.Windows.Forms.Panel();
            this.pct_header_image = new System.Windows.Forms.PictureBox();
            this.lbl_header_text = new System.Windows.Forms.Label();
            this.pnl_steps = new System.Windows.Forms.Panel();
            this.btn_back = new System.Windows.Forms.Button();
            this.btn_next = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.pnl_content.SuspendLayout();
            this.pnl_header.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pct_header_image)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(648, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_connect,
            this.mnu_disconnect,
            this.toolStripSeparator1,
            this.mnu_exit});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // mnu_connect
            // 
            this.mnu_connect.Name = "mnu_connect";
            this.mnu_connect.Size = new System.Drawing.Size(137, 22);
            this.mnu_connect.Text = "Connect";
            // 
            // mnu_disconnect
            // 
            this.mnu_disconnect.Enabled = false;
            this.mnu_disconnect.Name = "mnu_disconnect";
            this.mnu_disconnect.Size = new System.Drawing.Size(137, 22);
            this.mnu_disconnect.Text = "Disconnect";
            this.mnu_disconnect.Click += new System.EventHandler(this.mnu_disconnect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(134, 6);
            // 
            // mnu_exit
            // 
            this.mnu_exit.Name = "mnu_exit";
            this.mnu_exit.Size = new System.Drawing.Size(137, 22);
            this.mnu_exit.Text = "Exit";
            this.mnu_exit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu_help,
            this.toolStripSeparator2,
            this.mnu_about_movement});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.toolsToolStripMenuItem.Text = "Help";
            // 
            // mnu_help
            // 
            this.mnu_help.Name = "mnu_help";
            this.mnu_help.Size = new System.Drawing.Size(167, 22);
            this.mnu_help.Text = "Help";
            this.mnu_help.Click += new System.EventHandler(this.mnu_help_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(164, 6);
            // 
            // mnu_about_movement
            // 
            this.mnu_about_movement.Name = "mnu_about_movement";
            this.mnu_about_movement.Size = new System.Drawing.Size(167, 22);
            this.mnu_about_movement.Text = "About Movement";
            this.mnu_about_movement.Click += new System.EventHandler(this.mnu_about_movement_Click);
            // 
            // pnl_content
            // 
            this.pnl_content.AutoSize = true;
            this.pnl_content.BackColor = System.Drawing.SystemColors.Control;
            this.pnl_content.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_content.Controls.Add(this.lbl_instructions);
            this.pnl_content.Location = new System.Drawing.Point(179, 99);
            this.pnl_content.Name = "pnl_content";
            this.pnl_content.Size = new System.Drawing.Size(468, 399);
            this.pnl_content.TabIndex = 80;
            // 
            // lbl_instructions
            // 
            this.lbl_instructions.AutoSize = true;
            this.lbl_instructions.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_instructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_instructions.Location = new System.Drawing.Point(5, 10);
            this.lbl_instructions.MaximumSize = new System.Drawing.Size(450, 100);
            this.lbl_instructions.Name = "lbl_instructions";
            this.lbl_instructions.Size = new System.Drawing.Size(0, 17);
            this.lbl_instructions.TabIndex = 20;
            // 
            // pnl_header
            // 
            this.pnl_header.BackColor = System.Drawing.Color.Transparent;
            this.pnl_header.Controls.Add(this.pct_header_image);
            this.pnl_header.Controls.Add(this.lbl_header_text);
            this.pnl_header.Location = new System.Drawing.Point(0, 24);
            this.pnl_header.Name = "pnl_header";
            this.pnl_header.Size = new System.Drawing.Size(647, 75);
            this.pnl_header.TabIndex = 84;
            // 
            // pct_header_image
            // 
            this.pct_header_image.Location = new System.Drawing.Point(8, 3);
            this.pct_header_image.Name = "pct_header_image";
            this.pct_header_image.Size = new System.Drawing.Size(64, 64);
            this.pct_header_image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pct_header_image.TabIndex = 19;
            this.pct_header_image.TabStop = false;
            // 
            // lbl_header_text
            // 
            this.lbl_header_text.AutoSize = true;
            this.lbl_header_text.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_header_text.ForeColor = System.Drawing.Color.Black;
            this.lbl_header_text.Location = new System.Drawing.Point(84, 17);
            this.lbl_header_text.Name = "lbl_header_text";
            this.lbl_header_text.Size = new System.Drawing.Size(0, 25);
            this.lbl_header_text.TabIndex = 20;
            // 
            // pnl_steps
            // 
            this.pnl_steps.BackColor = System.Drawing.SystemColors.Control;
            this.pnl_steps.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_steps.Location = new System.Drawing.Point(3, 99);
            this.pnl_steps.Name = "pnl_steps";
            this.pnl_steps.Size = new System.Drawing.Size(172, 399);
            this.pnl_steps.TabIndex = 85;
            // 
            // btn_back
            // 
            this.btn_back.Location = new System.Drawing.Point(305, 365);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(75, 23);
            this.btn_back.TabIndex = 12;
            this.btn_back.Text = "< Back";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // btn_next
            // 
            this.btn_next.Location = new System.Drawing.Point(385, 365);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(75, 23);
            this.btn_next.TabIndex = 13;
            this.btn_next.Text = "Next >";
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // Movement
            // 
            this.AcceptButton = this.btn_next;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(648, 500);
            this.Controls.Add(this.pnl_steps);
            this.Controls.Add(this.pnl_header);
            this.Controls.Add(this.pnl_content);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Movement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Movement";
            this.Load += new System.EventHandler(this.Movement_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.pnl_content.ResumeLayout(false);
            this.pnl_content.PerformLayout();
            this.pnl_header.ResumeLayout(false);
            this.pnl_header.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pct_header_image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void InitializePracticeTest()
        {
            this.lbl_practice_label = new System.Windows.Forms.Label();
            this.script_preview = new global::Movement.TestEngine.Testing.TestScriptPreviewControl();

            // 
            // lbl_batch_label
            // 
            this.lbl_practice_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_practice_label.Location = new System.Drawing.Point(27, 65);
            this.lbl_practice_label.MinimumSize = new System.Drawing.Size(100, 0);
            this.lbl_practice_label.Name = "lbl_batch_label";
            this.lbl_practice_label.AutoSize = true;
            this.lbl_practice_label.TabIndex = 3;
            this.lbl_practice_label.Text = "Select a test:";

            //
            //script_preview
            //
            this.script_preview.Location = new System.Drawing.Point(275, 45);
            this.script_preview.Name = "script_preview";
        }

        private void InitializeCreateBatch()
        {
            this.grp_create_batch = new System.Windows.Forms.GroupBox();
            this.lbl_test_batch = new System.Windows.Forms.Label();
            this.lbl_available_tests = new System.Windows.Forms.Label();
            this.lst_available_tests = new System.Windows.Forms.ListBox();
            this.btn_move_test_down = new System.Windows.Forms.Button();
            this.btn_move_test_up = new System.Windows.Forms.Button();
            this.btn_remove_test = new System.Windows.Forms.Button();
            this.btn_add_test = new System.Windows.Forms.Button();
            this.lst_test_batch = new System.Windows.Forms.ListBox();
            this.grp_create_batch_information = new System.Windows.Forms.GroupBox();
            this.lbl_batch_information_descr = new System.Windows.Forms.Label();
            this.txt_description = new System.Windows.Forms.TextBox();
            this.lbl_batch_information_name = new System.Windows.Forms.Label();
            this.txt_batch_name = new System.Windows.Forms.TextBox();
            this.lnk_test_preview = new System.Windows.Forms.LinkLabel();

            // 
            // grp_create_batch
            // 
            this.grp_create_batch.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_create_batch.Controls.Add(this.lbl_test_batch);
            this.grp_create_batch.Controls.Add(this.lbl_available_tests);
            this.grp_create_batch.Controls.Add(this.lst_available_tests);
            this.grp_create_batch.Controls.Add(this.btn_move_test_down);
            this.grp_create_batch.Controls.Add(this.btn_move_test_up);
            this.grp_create_batch.Controls.Add(this.btn_remove_test);
            this.grp_create_batch.Controls.Add(this.btn_add_test);
            this.grp_create_batch.Controls.Add(this.lst_test_batch);
            this.grp_create_batch.Controls.Add(this.lnk_test_preview);
            this.grp_create_batch.Location = new System.Drawing.Point(12, 82);
            this.grp_create_batch.Name = "grp_create_batch";
            this.grp_create_batch.Size = new System.Drawing.Size(444, 195);
            this.grp_create_batch.TabIndex = 54;
            this.grp_create_batch.TabStop = false;
            this.grp_create_batch.Text = "Configure Test Batch";
            //
            //lnk_test_preview
            //
            this.lnk_test_preview.AutoSize = true;
            this.lnk_test_preview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lnk_test_preview.Location = new System.Drawing.Point(15, 175);
            this.lnk_test_preview.Name = "lnk_test_preview";
            this.lnk_test_preview.Text = "Preview Tests";
            this.lnk_test_preview.TabIndex = 7;
            this.lnk_test_preview.Click += new System.EventHandler(this.lnk_test_preview_Click);
            // 
            // lbl_test_batch
            // 
            this.lbl_test_batch.AutoSize = true;
            this.lbl_test_batch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_test_batch.Location = new System.Drawing.Point(238, 26);
            this.lbl_test_batch.Name = "lbl_test_batch";
            this.lbl_test_batch.Size = new System.Drawing.Size(62, 13);
            this.lbl_test_batch.TabIndex = 59;
            this.lbl_test_batch.Text = "Test Batch:";
            // 
            // lbl_available_tests
            // 
            this.lbl_available_tests.AutoSize = true;
            this.lbl_available_tests.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_available_tests.Location = new System.Drawing.Point(18, 26);
            this.lbl_available_tests.Name = "lbl_available_tests";
            this.lbl_available_tests.Size = new System.Drawing.Size(82, 13);
            this.lbl_available_tests.TabIndex = 58;
            this.lbl_available_tests.Text = "Available Tests:";
            // 
            // lst_available_tests
            // 
            this.lst_available_tests.FormattingEnabled = true;
            this.lst_available_tests.Location = new System.Drawing.Point(18, 44);
            this.lst_available_tests.Name = "lst_available_tests";
            this.lst_available_tests.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lst_available_tests.Size = new System.Drawing.Size(150, 134);
            this.lst_available_tests.TabIndex = 1;
            this.lst_available_tests.DisplayMember = "name";
            this.lst_available_tests.Sorted = true;
            // 
            // btn_move_test_down
            // 
            this.btn_move_test_down.FlatAppearance.BorderSize = 0;
            this.btn_move_test_down.Image = Properties.Resources.test_down;
            this.btn_move_test_down.Location = new System.Drawing.Point(395, 104);
            this.btn_move_test_down.Name = "btn_move_test_down";
            this.btn_move_test_down.Size = new System.Drawing.Size(33, 51);
            this.btn_move_test_down.TabIndex = 6;
            this.btn_move_test_down.UseVisualStyleBackColor = true;
            this.btn_move_test_down.Click += new System.EventHandler(this.move_test_down_Click);
            // 
            // btn_move_test_up
            // 
            this.btn_move_test_up.FlatAppearance.BorderSize = 0;
            this.btn_move_test_up.Image = Properties.Resources.test_up;
            this.btn_move_test_up.Location = new System.Drawing.Point(395, 53);
            this.btn_move_test_up.Name = "btn_move_test_up";
            this.btn_move_test_up.Size = new System.Drawing.Size(33, 51);
            this.btn_move_test_up.TabIndex = 5;
            this.btn_move_test_up.UseVisualStyleBackColor = true;
            this.btn_move_test_up.Click += new System.EventHandler(this.move_test_up_Click);
            // 
            // btn_remove_test
            // 
            this.btn_remove_test.FlatAppearance.BorderSize = 0;
            this.btn_remove_test.Image = Properties.Resources.remove_test;
            this.btn_remove_test.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_remove_test.Location = new System.Drawing.Point(174, 105);
            this.btn_remove_test.Name = "btn_remove_test";
            this.btn_remove_test.Size = new System.Drawing.Size(60, 45);
            this.btn_remove_test.TabIndex = 3;
            this.btn_remove_test.UseVisualStyleBackColor = true;
            this.btn_remove_test.Click += new System.EventHandler(this.remove_test_Click);
            // 
            // btn_add_test
            // 
            this.btn_add_test.FlatAppearance.BorderSize = 0;
            this.btn_add_test.Image = Properties.Resources.add_test;
            this.btn_add_test.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btn_add_test.Location = new System.Drawing.Point(174, 60);
            this.btn_add_test.Name = "btn_add_test";
            this.btn_add_test.Size = new System.Drawing.Size(60, 45);
            this.btn_add_test.TabIndex = 2;
            this.btn_add_test.UseVisualStyleBackColor = true;
            this.btn_add_test.Click += new System.EventHandler(this.add_test_Click);
            // 
            // lst_test_batch
            // 
            this.lst_test_batch.AllowDrop = true;
            this.lst_test_batch.FormattingEnabled = true;
            this.lst_test_batch.Location = new System.Drawing.Point(239, 44);
            this.lst_test_batch.Name = "lst_test_batch";
            this.lst_test_batch.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lst_test_batch.Size = new System.Drawing.Size(150, 134);
            this.lst_test_batch.TabIndex = 4;
            this.lst_test_batch.DisplayMember = "name";
            this.lst_test_batch.Sorted = true; 
            // 
            // grp_create_batch_information
            // 
            this.grp_create_batch_information.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_create_batch_information.Controls.Add(this.lbl_batch_information_descr);
            this.grp_create_batch_information.Controls.Add(this.txt_description);
            this.grp_create_batch_information.Controls.Add(this.lbl_batch_information_name);
            this.grp_create_batch_information.Controls.Add(this.txt_batch_name);
            this.grp_create_batch_information.Location = new System.Drawing.Point(12, 294);
            this.grp_create_batch_information.Name = "grp_create_batch_information";
            this.grp_create_batch_information.Size = new System.Drawing.Size(444, 188);
            this.grp_create_batch_information.TabIndex = 55;
            this.grp_create_batch_information.TabStop = false;
            this.grp_create_batch_information.Text = "Test Batch Information";
            // 
            // lbl_batch_information_descr
            // 
            this.lbl_batch_information_descr.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_batch_information_descr.AutoSize = true;
            this.lbl_batch_information_descr.Location = new System.Drawing.Point(17, 84);
            this.lbl_batch_information_descr.Name = "lbl_batch_information_descr";
            this.lbl_batch_information_descr.Size = new System.Drawing.Size(63, 13);
            this.lbl_batch_information_descr.TabIndex = 58;
            this.lbl_batch_information_descr.Text = "Description:";
            // 
            // txt_description
            // 
            this.txt_description.Location = new System.Drawing.Point(20, 100);
            this.txt_description.MaxLength = 255;
            this.txt_description.Multiline = true;
            this.txt_description.Name = "txt_description";
            this.txt_description.Size = new System.Drawing.Size(405, 70);
            this.txt_description.TabIndex = 8;
            // 
            // lbl_batch_information_name
            // 
            this.lbl_batch_information_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_batch_information_name.AutoSize = true;
            this.lbl_batch_information_name.Location = new System.Drawing.Point(15, 29);
            this.lbl_batch_information_name.Name = "lbl_batch_information_name";
            this.lbl_batch_information_name.Size = new System.Drawing.Size(38, 13);
            this.lbl_batch_information_name.TabIndex = 55;
            this.lbl_batch_information_name.Text = "Name:";
            // 
            // txt_batch_name
            // 
            this.txt_batch_name.Location = new System.Drawing.Point(20, 45);
            this.txt_batch_name.MaxLength = 50;
            this.txt_batch_name.Name = "txt_name";
            this.txt_batch_name.Size = new System.Drawing.Size(405, 18);
            this.txt_batch_name.TabIndex = 7;

        }

        private void InitializeCreateUser()
        {
            this.grp_personal_information = new System.Windows.Forms.GroupBox();
            this.lbl_contact_information = new System.Windows.Forms.Label();
            this.lbl_new_name = new System.Windows.Forms.Label();
            this.txt_contact_info = new System.Windows.Forms.TextBox();
            this.txt_new_name = new System.Windows.Forms.TextBox();
            this.grp_user_details = new System.Windows.Forms.GroupBox();
            this.lbl_role = new System.Windows.Forms.Label();
            this.cmb_role = new System.Windows.Forms.ComboBox();
            this.lbl_new_password_confirm = new System.Windows.Forms.Label();
            this.txt_new_password_confirm = new System.Windows.Forms.TextBox();
            this.lbl_new_password = new System.Windows.Forms.Label();
            this.lbl_new_username = new System.Windows.Forms.Label();
            this.txt_new_password = new System.Windows.Forms.TextBox();
            this.txt_new_username = new System.Windows.Forms.TextBox();

            // 
            // grp_personal_information
            // 
            this.grp_personal_information.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_personal_information.Controls.Add(this.lbl_contact_information);
            this.grp_personal_information.Controls.Add(this.lbl_new_name);
            this.grp_personal_information.Controls.Add(this.txt_contact_info);
            this.grp_personal_information.Controls.Add(this.txt_new_name);
            this.grp_personal_information.Location = new System.Drawing.Point(16, 40);
            this.grp_personal_information.Name = "grp_personal_information";
            this.grp_personal_information.Size = new System.Drawing.Size(350, 155);
            this.grp_personal_information.TabIndex = 15;
            this.grp_personal_information.TabStop = false;
            this.grp_personal_information.Text = "Personal Information";
            // 
            // lbl_contact_information
            // 
            this.lbl_contact_information.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_contact_information.AutoSize = true;
            this.lbl_contact_information.Location = new System.Drawing.Point(6, 69);
            this.lbl_contact_information.Name = "lbl_contact_information";
            this.lbl_contact_information.TabIndex = 22;
            this.lbl_contact_information.Text = "Contact Information: ";
            // 
            // lbl_new_name
            // 
            this.lbl_new_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_new_name.AutoSize = true;
            this.lbl_new_name.Location = new System.Drawing.Point(11, 38);
            this.lbl_new_name.Name = "lbl_new_name";
            this.lbl_new_name.TabIndex = 21;
            this.lbl_new_name.Text = "Name:";
            // 
            // txt_contact_info
            // 
            this.txt_contact_info.Location = new System.Drawing.Point(9, 85);
            this.txt_contact_info.Multiline = true;
            this.txt_contact_info.Name = "txt_contact_info";
            this.txt_contact_info.Size = new System.Drawing.Size(300, 59);
            this.txt_contact_info.TabIndex = 3;
            // 
            // txt_new_name
            // 
            this.txt_new_name.Location = new System.Drawing.Point(58, 35);
            this.txt_new_name.MaxLength = 50;
            this.txt_new_name.Name = "txt_new_name";
            this.txt_new_name.Size = new System.Drawing.Size(250, 18);
            this.txt_new_name.TabIndex = 1;
            // 
            // grp_user_details
            // 
            this.grp_user_details.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grp_user_details.Controls.Add(this.lbl_role);
            this.grp_user_details.Controls.Add(this.cmb_role);
            this.grp_user_details.Controls.Add(this.lbl_new_password_confirm);
            this.grp_user_details.Controls.Add(this.txt_new_password_confirm);
            this.grp_user_details.Controls.Add(this.lbl_new_password);
            this.grp_user_details.Controls.Add(this.lbl_new_username);
            this.grp_user_details.Controls.Add(this.txt_new_password);
            this.grp_user_details.Controls.Add(this.txt_new_username);
            this.grp_user_details.Location = new System.Drawing.Point(16, 205);
            this.grp_user_details.Name = "grp_user_details";
            this.grp_user_details.Size = new System.Drawing.Size(350, 153);
            this.grp_user_details.TabIndex = 60;
            this.grp_user_details.TabStop = false;
            this.grp_user_details.Text = "User Details";
            // 
            // lbl_role
            // 
            this.lbl_role.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_role.AutoSize = true;
            this.lbl_role.Location = new System.Drawing.Point(11, 31);
            this.lbl_role.Name = "lbl_role";
            this.lbl_role.TabIndex = 32;
            this.lbl_role.Text = "Role:";
            // 
            // cmb_role
            // 
            this.cmb_role.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_role.FormattingEnabled = true;
            this.cmb_role.Items.AddRange(new object[] {
            "Administrator",
            "Clinician"});
            this.cmb_role.SelectedIndex = 1;
            this.cmb_role.Location = new System.Drawing.Point(120, 28);
            this.cmb_role.Name = "cmb_role";
            this.cmb_role.Size = new System.Drawing.Size(119, 18);
            this.cmb_role.TabIndex = 4;
            // 
            // lbl_new_password_confirm
            // 
            this.lbl_new_password_confirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_new_password_confirm.AutoSize = true;
            this.lbl_new_password_confirm.Location = new System.Drawing.Point(11, 121);
            this.lbl_new_password_confirm.Name = "lbl_new_password_confirm";
            this.lbl_new_password_confirm.TabIndex = 30;
            this.lbl_new_password_confirm.Text = "Retype Password:";
            // 
            // txt_new_password_confirm
            // 
            this.txt_new_password_confirm.Location = new System.Drawing.Point(120, 118);
            this.txt_new_password_confirm.Name = "txt_new_password_confirm";
            this.txt_new_password_confirm.PasswordChar = '*';
            this.txt_new_password_confirm.Size = new System.Drawing.Size(178, 18);
            this.txt_new_password_confirm.TabIndex = 7;
            // 
            // lbl_new_password
            // 
            this.lbl_new_password.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_new_password.AutoSize = true;
            this.lbl_new_password.Location = new System.Drawing.Point(11, 91);
            this.lbl_new_password.Name = "lbl_new_password";
            this.lbl_new_password.TabIndex = 29;
            this.lbl_new_password.Text = "Password:";
            // 
            // lbl_new_username
            // 
            this.lbl_new_username.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_new_username.AutoSize = true;
            this.lbl_new_username.Location = new System.Drawing.Point(11, 60);
            this.lbl_new_username.Name = "lbl_new_username";
            this.lbl_new_username.TabIndex = 28;
            this.lbl_new_username.Text = "User Name:";
            // 
            // txt_new_password
            // 
            this.txt_new_password.Location = new System.Drawing.Point(120, 91);
            this.txt_new_password.Name = "txt_new_password";
            this.txt_new_password.PasswordChar = '*';
            this.txt_new_password.Size = new System.Drawing.Size(178, 18);
            this.txt_new_password.TabIndex = 6;
            // 
            // txt_new_username
            // 
            this.txt_new_username.Location = new System.Drawing.Point(120, 60);
            this.txt_new_username.MaxLength = 50;
            this.txt_new_username.Name = "txt_new_username";
            this.txt_new_username.Size = new System.Drawing.Size(178, 18);
            this.txt_new_username.TabIndex = 5;


        }

        private void InitializePatientHistory()
        {
            this.dataGrid = new System.Windows.Forms.DataGridView();          
            this.normal_limits = new System.Windows.Forms.GroupBox();
            this.normal = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.rotation = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.mode = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.test_name = new System.Windows.Forms.Label();
            this.handness = new System.Windows.Forms.GroupBox();
            this.hand = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lst_tests = new System.Windows.Forms.ListBox();
            this.tabs_history = new System.Windows.Forms.TabControl();
            this.tab_metrics = new System.Windows.Forms.TabPage();
            this.tab_graphs = new System.Windows.Forms.TabPage();
            this.tab_replay = new System.Windows.Forms.TabPage();
            this.analysis_control = new global::Movement.Analysis.Graphing.AnalysisDisplayControl();
            this.replay_control = new global::Movement.TestEngine.Testing.TestReplayControl();
            //
            // list box
            //
            this.lst_tests.FormattingEnabled = true;
            this.lst_tests.Location = new System.Drawing.Point(562,50);
            this.lst_tests.Name = "lst_patients";
            this.lst_tests.Size = new System.Drawing.Size(150, 362);
            this.lst_tests.TabIndex = 5;            
            this.lst_tests.SelectedIndexChanged += new System.EventHandler(this.lst_tests_SelectedIndexChanged);
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;            
            this.dataGrid.Location = new System.Drawing.Point(0,0);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.ReadOnly = true;
            //this.dataGrid.Size = new System.Drawing.Size(543, 410);
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGrid.TabIndex = 0;                   
            // 
            // normal_limits
            // 
            this.normal_limits.Controls.Add(this.normal);
            this.normal_limits.Location = new System.Drawing.Point(483, 475);
            this.normal_limits.Name = "normal_limits";
            this.normal_limits.Size = new System.Drawing.Size(125, 37);
            this.normal_limits.TabIndex = 11;
            this.normal_limits.TabStop = false;
            this.normal_limits.Text = "Within Normal Limits:";           
            // 
            // normal
            // 
            this.normal.AutoSize = true;
            this.normal.Location = new System.Drawing.Point(19, 16);
            this.normal.Name = "normal";
            this.normal.Size = new System.Drawing.Size(38, 13);
            this.normal.TabIndex = 0;
            //this.normal.Text = "normal";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.rotation);
            this.groupBox5.Location = new System.Drawing.Point(224, 475);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(125, 37);
            this.groupBox5.TabIndex = 10;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Rotation:";            
            // 
            // rotation
            // 
            this.rotation.AutoSize = true;
            this.rotation.Location = new System.Drawing.Point(19, 16);
            this.rotation.Name = "rotation";
            this.rotation.Size = new System.Drawing.Size(47, 13);
            this.rotation.TabIndex = 9;
            //this.rotation.Text = "Rotation";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.mode);
            this.groupBox4.Location = new System.Drawing.Point(354, 475);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(124, 37);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Mode Test Taken:";          
            // 
            // mode
            // 
            this.mode.AutoSize = true;
            this.mode.Location = new System.Drawing.Point(19, 16);
            this.mode.Name = "mode";
            this.mode.Size = new System.Drawing.Size(70, 13);
            this.mode.TabIndex = 5;
            //this.mode.Text = "Uncongnitive";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.test_name);
            this.groupBox3.Location = new System.Drawing.Point(95, 475);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(124, 37);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Test Name:";            
            // 
            // test
            // 
            this.test_name.AutoSize = true;
            this.test_name.Location = new System.Drawing.Point(19, 16);
            this.test_name.Name = "test_name";
            this.test_name.Size = new System.Drawing.Size(28, 13);
            this.test_name.TabIndex = 6;
            //this.test_name.Text = "Test";
            // 
            // handness
            // 
            this.handness.Controls.Add(this.hand);
            this.handness.Location = new System.Drawing.Point(9, 475);
            this.handness.Name = "handness";
            this.handness.Size = new System.Drawing.Size(83, 37);
            this.handness.TabIndex = 4;
            this.handness.TabStop = false;
            this.handness.Text = "Hand Used:";            
            // 
            // hand
            // 
            this.hand.AutoSize = true;
            this.hand.Location = new System.Drawing.Point(19, 16);
            this.hand.Name = "hand";
            this.hand.Size = new System.Drawing.Size(32, 13);
            this.hand.TabIndex = 3;
            //this.hand.Text = "Right";     
            
            //this.tabs_history.SuspendLayout();
            //this.SuspendLayout();
            // 
            // tabs_history
            // 
            this.tabs_history.Controls.Add(this.tab_metrics);
            this.tabs_history.Controls.Add(this.tab_graphs);
            this.tabs_history.Controls.Add(this.tab_replay);
            this.tabs_history.Location = new System.Drawing.Point(9, 50);
            this.tabs_history.Name = "tabs_history";
            this.tabs_history.SelectedIndex = 0;
            this.tabs_history.Size = new System.Drawing.Size(548, 365);
            //this.tabs_history.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabs_history.TabIndex = 0;
            // 
            // tab_metrics
            // 
            this.tab_metrics.Controls.Add(this.dataGrid);
            //this.tab_metrics.Location = new System.Drawing.Point(4, 22);
            this.tab_metrics.Name = "tab_metrics";
            this.tab_metrics.Padding = new System.Windows.Forms.Padding(3);
            //this.tab_metrics.Size = new System.Drawing.Size(571, 405);
            this.tab_metrics.TabIndex = 0;
            this.tab_metrics.Text = "Metrics";
            this.tab_metrics.UseVisualStyleBackColor = true;
            // 
            // tab_graphs
            // 
            //this.tab_graphs.Location = new System.Drawing.Point(4, 22);
            this.tab_graphs.Name = "tab_graphs";
            this.tab_graphs.Padding = new System.Windows.Forms.Padding(3);
            //this.tab_graphs.Size = new System.Drawing.Size(571, 405);
            this.tab_graphs.TabIndex = 1;
            this.tab_graphs.Text = "Graphs";
            this.tab_graphs.UseVisualStyleBackColor = true;
            // 
            // tab_replay
            // 
            //this.tab_replay.Location = new System.Drawing.Point(4, 22);
            this.tab_replay.Name = "tab_replay";
            //this.tab_replay.Size = new System.Drawing.Size(571, 405);
            this.tab_replay.TabIndex = 2;
            this.tab_replay.Text = "Replay";
            this.tab_replay.UseVisualStyleBackColor = true;

            this.tab_graphs.Controls.Add(this.analysis_control);
            this.analysis_control.Dock = System.Windows.Forms.DockStyle.Fill;

            this.tab_replay.Controls.Add(this.replay_control);
            this.replay_control.Dock = System.Windows.Forms.DockStyle.Fill;
        }

        #endregion

        //Basic Movement Layout
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnu_connect;
        private System.Windows.Forms.ToolStripMenuItem mnu_disconnect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnu_exit;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnu_help;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem mnu_about_movement;
        private System.Windows.Forms.Panel pnl_content;
        private System.Windows.Forms.Panel pnl_header;
        private System.Windows.Forms.Panel pnl_steps;
        private System.Windows.Forms.Label lbl_header_text;
        private System.Windows.Forms.PictureBox pct_header_image;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Button btn_next;

        //Login View
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.Label lbl_instructions;
        private System.Windows.Forms.ComboBox cmb_server;
        private System.Windows.Forms.Label lbl_server_name;
        private System.Windows.Forms.TextBox txt_password;
        private System.Windows.Forms.TextBox txt_username;
        private System.Windows.Forms.Label lbl_password;
        private System.Windows.Forms.Label lbl_user;
        private System.Windows.Forms.GroupBox grp_login;

        //Post-login View       
        private System.Windows.Forms.RadioButton opt_add_patient;
        private System.Windows.Forms.RadioButton opt_retrieve_patient;
        private System.Windows.Forms.RadioButton opt_create_user;
        private System.Windows.Forms.RadioButton opt_create_batch;
        private System.Windows.Forms.GroupBox grp_tasks;
        private System.Windows.Forms.Label lbl_administrative_tasks;
        private System.Windows.Forms.Label lbl_clinician_tasks;

        //Clinician Tasks
        private System.Windows.Forms.RadioButton opt_test_patient;
        private System.Windows.Forms.RadioButton opt_patient_history;
        private System.Windows.Forms.RadioButton opt_practice_test;
        private System.Windows.Forms.RadioButton opt_patient_information;
        private System.Windows.Forms.GroupBox grp_clinician_tasks;

        //Patient Information
        private System.Windows.Forms.GroupBox grp_patient_information;
        private System.Windows.Forms.GroupBox grp_patient_notes;
        private System.Windows.Forms.Panel pnl_patient_notes;
        private System.Windows.Forms.Label lbl_patient_notes;
        private System.Windows.Forms.Label lbl_patient_sex;
        private System.Windows.Forms.Label lbl_patient_dob;
        private System.Windows.Forms.Label lbl_patient_handedness;
        private System.Windows.Forms.Label lbl_patient_name;
        private System.Windows.Forms.Label lbl_patient_contact;
        private System.Windows.Forms.Label lbl_patient_address;
        private System.Windows.Forms.Button btn_add_note;

        //Retrieve Patient
        private System.Windows.Forms.MaskedTextBox msk_ssn;
        private System.Windows.Forms.Label lbl_ssn;
        private System.Windows.Forms.Label lbl_dob;
        private System.Windows.Forms.Label lbl_name;
        private System.Windows.Forms.TextBox txt_name;
        private System.Windows.Forms.MaskedTextBox msk_dob;
        private System.Windows.Forms.ListBox lst_patients;
        private System.Windows.Forms.Button btn_retrieve;

        //Add Patient
        private System.Windows.Forms.Panel pnl_sex;
        private System.Windows.Forms.Panel pnl_handedness;
        private System.Windows.Forms.RadioButton rdbtn_right;
        private System.Windows.Forms.RadioButton rdbtn_left;
        private System.Windows.Forms.RadioButton rdbtn_male;
        private System.Windows.Forms.RadioButton rdbtn_female;
        private System.Windows.Forms.Label lbl_handedness;
        private System.Windows.Forms.Label lbl_sex;
        private System.Windows.Forms.Label lbl_contact;
        private System.Windows.Forms.Label lbl_notes;
        private System.Windows.Forms.Label lbl_add_patient_address;
        private System.Windows.Forms.TextBox txt_add_patient_address;
        private System.Windows.Forms.TextBox txt_patient_notes;
        private System.Windows.Forms.TextBox txt_patient_contact;
        private System.Windows.Forms.MaskedTextBox msk_patient_ssn;
        private System.Windows.Forms.Label lbl_patient_ssn;
        private System.Windows.Forms.Label lbl_add_patient_dob;
        private System.Windows.Forms.Label lbl_add_patient_name;
        private System.Windows.Forms.TextBox txt_patient_name;
        private System.Windows.Forms.MaskedTextBox msk_patient_dob;

        //Test Batch Selection
        private System.Windows.Forms.Label lbl_description;
        private System.Windows.Forms.Label lbl_batch_description;
        private System.Windows.Forms.Label lbl_batch_label;
        private System.Windows.Forms.ComboBox cmb_batches;

        //Practice Test
        private System.Windows.Forms.Label lbl_practice_label;
        private TestEngine.Testing.TestScriptPreviewControl script_preview;

        //Create Test Batch
        private System.Windows.Forms.GroupBox grp_create_batch;
        private System.Windows.Forms.Label lbl_test_batch;
        private System.Windows.Forms.Label lbl_available_tests;
        private System.Windows.Forms.ListBox lst_available_tests;
        private System.Windows.Forms.Button btn_move_test_down;
        private System.Windows.Forms.Button btn_move_test_up;
        private System.Windows.Forms.Button btn_remove_test;
        private System.Windows.Forms.Button btn_add_test;
        private System.Windows.Forms.ListBox lst_test_batch;
        private System.Windows.Forms.GroupBox grp_create_batch_information;
        private System.Windows.Forms.Label lbl_batch_information_descr;
        private System.Windows.Forms.TextBox txt_description;
        private System.Windows.Forms.Label lbl_batch_information_name;
        private System.Windows.Forms.TextBox txt_batch_name;
        private System.Windows.Forms.LinkLabel lnk_test_preview;

        //Create New User
        private System.Windows.Forms.GroupBox grp_personal_information;
        private System.Windows.Forms.Label lbl_contact_information;
        private System.Windows.Forms.Label lbl_new_name;
        public System.Windows.Forms.TextBox txt_contact_info;
        public System.Windows.Forms.TextBox txt_new_name;
        private System.Windows.Forms.GroupBox grp_user_details;
        private System.Windows.Forms.Label lbl_role;
        public System.Windows.Forms.ComboBox cmb_role;
        private System.Windows.Forms.Label lbl_new_password_confirm;
        public System.Windows.Forms.TextBox txt_new_password_confirm;
        private System.Windows.Forms.Label lbl_new_password;
        private System.Windows.Forms.Label lbl_new_username;
        public System.Windows.Forms.TextBox txt_new_password;
        public System.Windows.Forms.TextBox txt_new_username;

        //Patient History
        private System.Windows.Forms.ListBox lst_tests;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Label label1;                
        private System.Windows.Forms.Label hand;
        private System.Windows.Forms.Label mode;
        private System.Windows.Forms.GroupBox handness;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label test_name;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label rotation;
        private System.Windows.Forms.GroupBox normal_limits;
        private System.Windows.Forms.Label normal;
        private System.Windows.Forms.TabControl tabs_history;
        private System.Windows.Forms.TabPage tab_metrics;
        private System.Windows.Forms.TabPage tab_graphs;
        private System.Windows.Forms.TabPage tab_replay;
        private global::Movement.TestEngine.Testing.TestReplayControl replay_control;
        private global::Movement.Analysis.Graphing.AnalysisDisplayControl analysis_control;        
    }
}

