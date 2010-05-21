/*
 * MOVEMENT
 * Clarkson University
 * 02/2007
 * 
 * */


using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using Movement.UserInterface.movement_web_service;
using Movement.TestEngine.Capture.Calibration;

namespace Movement.UserInterface
{
    public partial class Movement : Form
    {
        public Movement()
        {
            InitializeComponent();
            InitializeLogin();
            InitializePostLogin();
            InitializeRetrievePatient();
            InitializeAddPatient();
            InitializeBatchSelection();
            InitializePracticeTest();
            InitializeCreateBatch();
            InitializeCreateUser();
            InitializeClinicianTasks();
            InitializePatientInformation();
            InitializePatientHistory();
            tasks = new TaskList(new Point(15, 15));
            states = new Stack<int>(10);
            web_connection = new Connection();
            patient = new Patient();
            users = new User();
            test = new Test();

            InkCalibration.Current = new CenteredInkCalibration(new PressureInkCalibration());
        }


        /// <summary>
        /// Clears the contents of the panel displaying the content of each step,
        /// and panel displaying the tasks
        /// </summary>
        private void ClearPanels()
        {
            //remove contents of panels
            pnl_content.Controls.Clear();
            pnl_steps.Controls.Clear();
        }


        /// <summary>
        /// Prepares the form for a new step or task
        /// </summary>
        /// <param name="instructions">Instructions to display to the user</param>
        /// <param name="headerText">Text to display in the header</param>
        /// <param name="headerImage">Image to display to the left of the header text</param>
        private void SetupForm(String instructions, String headerText, Image headerImage)
        {
            //remove contents of panels
            ClearPanels();

            //Set header information and instructions
            lbl_instructions.Text = instructions;
            lbl_header_text.Text = headerText;
            pct_header_image.Image = headerImage;

            //Display header information and back,next buttons
            pnl_content.Controls.AddRange(new System.Windows.Forms.Control[] { btn_back, btn_next, lbl_instructions });

            //set the accept button
            this.AcceptButton = btn_next;
        }


        /// <summary>
        /// Displays the login content
        /// </summary>
        private void DisplayLogin()
        {
            //Load the list of servers previously connected to
            ArrayList temp = web_connection.LoadServers("servers");
            if (temp != null)
            {
                for (int x = 0; x < temp.Count; x++)
                    cmb_server.Items.Add(temp[x]);
            }

            states.Push((int)state.Login);

            //Setup the form
            String instr = "Welcome to Movement!  Before we can begin you must login.  Enter the name of the " +
                "server you want to connect to, and your username and password to begin.";
            SetupForm(instr, "User Login", Properties.Resources.user64);

            //Display login content
            btn_next.Text = "Connect";
            btn_back.Text = "Local Mode";
            pnl_content.Controls.Add(grp_login);
            //pnl_content.Controls.Remove(btn_back);

            //Display tasks
            tasks.AddTask(Properties.Resources.arrow, "Login");
            tasks.Display(pnl_steps);           

        }


        /// <summary>
        /// Displays tasks available after a successful login
        /// </summary>
        private void DisplayPostLogin()
        {
            states.Push((int)state.PostLogin);
            String instr = "Welcome " + _User.name +"!  Select from one of the tasks listed below to begin.";
            SetupForm(instr, "Select Task", Properties.Resources.action64);

            //Display actions
            pnl_content.Controls.Add(grp_tasks);
            pnl_content.Controls.Remove(btn_back);

            //if admin
            if (_User.role == 'A')
            {
                lbl_administrative_tasks.Visible = true;
                opt_create_user.Visible = true;
                opt_create_batch.Visible = true;
            }
            else
            {
                lbl_administrative_tasks.Visible = false;
                opt_create_user.Visible = false;
                opt_create_batch.Visible = false;

            }

            //Display tasks
            tasks.ModifyImage(states.Count - 2, Properties.Resources.arrow_gray);
            tasks.AddTask(Properties.Resources.arrow, "Select Task");
            tasks.Display(pnl_steps);
        }


        /// <summary>
        /// Displays the content for retrieving a patient
        /// </summary>
        private void DisplayRetrievePatient()
        {
            states.Push((int)state.RetrievePatient);

            String instr = "To retrieve a patient provide at least two of the three search criteria below " +
                "and click Retrieve.  Select the patient from the list of matching patients, then click Next to continue.";
            SetupForm(instr, "Retrieve Patient", Properties.Resources.retrieve_patient);

            //Display search options
            pnl_content.Controls.Add(msk_ssn);
            pnl_content.Controls.Add(msk_dob);
            pnl_content.Controls.Add(lbl_ssn);
            pnl_content.Controls.Add(lbl_dob);
            pnl_content.Controls.Add(lbl_name);
            pnl_content.Controls.Add(txt_name);
            pnl_content.Controls.Add(lst_patients);
            pnl_content.Controls.Add(btn_retrieve);

            //Display tasks
            tasks.ModifyImage(states.Count - 2, Properties.Resources.arrow_gray);
            tasks.AddTask(Properties.Resources.arrow, "Retrieve Patient", 15);
            tasks.Display(pnl_steps);
        }


        /// <summary>
        /// Displays content for selecting a batch
        /// </summary>
        private void DisplayBatchSelection()
        {
            Batch[] batch_list;
            states.Push((int)state.BatchSelection);

            String instr = "Select the desired test batch and click Start to continue.";
            SetupForm(instr, "Select Test Batch", Properties.Resources.movement64);

            //Display batch selection content
            //pnl_content.Controls.AddRange(new System.Windows.Forms.Control[] { btn_back, btn_next, lbl_instructions });
            btn_next.Text = "Start";
            pnl_content.Controls.AddRange(new System.Windows.Forms.Control[] { lbl_batch_description, lbl_description, cmb_batches,lbl_batch_label });

            //Display tasks
            tasks.ModifyImage(states.Count - 2, Properties.Resources.arrow_gray);
            tasks.AddTask(Properties.Resources.arrow, "Test Patient", 30);
            tasks.Display(pnl_steps);
            
            // Remove all batches.  We don't want duplicates!
            cmb_batches.Items.Clear();
            lbl_batch_description.Text = "";


            //add the batches
            batch_list = test.RetrieveBatches();
            
            //check to see if an error occurred
            if (batch_list == null)
                MessageBox.Show(test.Error, "Error Retrieving Batches", MessageBoxButtons.OK, test.ErrorType);
            else
            {
                for (int i = 0; i < batch_list.Length; i++)
                    cmb_batches.Items.Add(batch_list[i]);
            }
        }


        /// <summary>
        /// Displays content for patient history
        /// </summary>
        private void DisplayPatientHistory()
        {
            states.Push((int)state.PatientHistory);

            //form is too small for this, make it bigger
            /*btn_back.Left = 560;
            btn_next.Left = 635;*/
            btn_next.Visible = false;
            btn_back.Left = 635;

            btn_back.Top = 500;
            btn_next.Top = btn_back.Location.Y;
            pnl_steps.Height = 652;
            this.Width = 900;
            this.Height = 655;

            String instr = "Select a test from the list on the right side of the screen to view it's analysis.";
            SetupForm(instr, "Patient History", Properties.Resources.history64);

            //display data fields
            //pnl_content.Controls.Add(dataGrid);           
            pnl_content.Controls.Add(tabs_history);           
            pnl_content.Controls.Add(label1);

            pnl_content.Controls.Add(handness);
            pnl_content.Controls.Add(groupBox3);
            pnl_content.Controls.Add(groupBox4);
            pnl_content.Controls.Add(groupBox5);            
            pnl_content.Controls.Add(normal_limits);            
            pnl_content.Controls.Add(lst_tests);

            //Display tasks
            tasks.ModifyImage(states.Count - 2, Properties.Resources.arrow_gray);
            tasks.AddTask(Properties.Resources.arrow, "Patient History", 30);
            tasks.Display(pnl_steps);
     
            history_test_list = test.RetrieveHistory(patient.ThisPatient);
            for (int i=0; i < history_test_list.Count; i++)
            {
                lst_tests.Items.Add(history_test_list[i].timestamp.ToLocalTime());
            }
        }


        /// <summary>
        /// Displays content for adding a new patient
        /// </summary>
        private void DisplayAddPatient()
        {
            states.Push((int)state.AddPatient);

            String instr = "Enter all required information and click Add to continue.";
            SetupForm(instr, "Add Patient", Properties.Resources.add_patient);

            //Display search options
            btn_next.Text = "Add";
            pnl_content.Controls.Add(msk_patient_ssn);
            pnl_content.Controls.Add(msk_patient_dob);
            pnl_content.Controls.Add(lbl_patient_ssn);
            pnl_content.Controls.Add(lbl_add_patient_dob);
            pnl_content.Controls.Add(lbl_add_patient_name);
            pnl_content.Controls.Add(txt_patient_name);
            pnl_content.Controls.Add(pnl_sex);
            pnl_content.Controls.Add(lbl_sex);
            pnl_content.Controls.Add(pnl_handedness);
            pnl_content.Controls.Add(lbl_handedness);
            pnl_content.Controls.Add(lbl_notes);
            pnl_content.Controls.Add(txt_patient_notes);
            pnl_content.Controls.Add(lbl_contact);
            pnl_content.Controls.Add(txt_patient_contact);
            pnl_content.Controls.Add(txt_add_patient_address);
            pnl_content.Controls.Add(lbl_add_patient_address);

            //Display tasks
            tasks.ModifyImage(states.Count - 2, Properties.Resources.arrow_gray);
            tasks.AddTask(Properties.Resources.arrow, "Add Patient", 15);
            tasks.Display(pnl_steps);

        }

        /// <summary>
        /// Displays content for taking a practice test
        /// </summary>
        private void DisplayPracticeTest()
        {

            ScriptInfo[] available_tests;

            int previous = -1;
            if ( states.Count > 0 )
                previous = states.Peek();

            states.Push((int)state.PracticeTest);

            if (previous == (int)state.Login)
            {
                btn_back.Text = "< Back";
            }

            String instr = "Select a test and click Start to begin the practice test.";
            SetupForm(instr, "Practice Test", Properties.Resources.movement64);

            //Display batch selection content
            btn_next.Text = "Start";
            cmb_batches.Items.Clear();
            lbl_batch_description.Text = "";
            pnl_content.Controls.AddRange(new System.Windows.Forms.Control[] { lbl_batch_description, lbl_description, cmb_batches, lbl_practice_label });

            //Display tasks
            if (previous != (int)state.Login)
            {
                tasks.ModifyImage(states.Count - 2, Properties.Resources.arrow_gray);
                tasks.AddTask(Properties.Resources.arrow, "Practice Test", 30);
            }
            else
                tasks.AddTask(Properties.Resources.arrow, "Practice Test");

            tasks.Display(pnl_steps);

            //Display available tests
            if (previous != (int)state.Login)
                available_tests = test.RetrieveTests();
            else
                available_tests = test.LocalRetrieveTests();

            for (int x = 0; x < available_tests.Length; x++)
                cmb_batches.Items.Add(available_tests[x]);

        }

        /// <summary>
        /// Displays content for creating a new user
        /// </summary>
        private void DisplayCreateUser()
        {
            states.Push((int)state.CreateUser);

            String instr = "Fill out the fields below and click Create to continue.";
            SetupForm(instr, "Create New User", Properties.Resources.newUser64);

            //Display create user content
            btn_next.Text = "Create";
            pnl_content.Controls.Add(grp_personal_information);
            pnl_content.Controls.Add(grp_user_details);

            //Display tasks
            tasks.ModifyImage(states.Count - 2, Properties.Resources.arrow_gray);
            tasks.AddTask(Properties.Resources.arrow, "Create User", 15);
            tasks.Display(pnl_steps);

        }

        /// <summary>
        /// Displays content for creating a new test batch
        /// </summary>
        private void DisplayCreateBatch()
        {
            ScriptInfo[] available_tests;
            states.Push((int)state.CreateBatch);

            String instr = "Select one or more tests from the list of available tests, then click the right arrow button to add them to the test batch. " +
                "Give the test batch a name, an optional description, and click Create.";
            SetupForm(instr, "Create Test Batch", Properties.Resources.settings64);

            //the form is not big enough for this step so we have to resize it, and move the next, back buttons
            btn_back.Top = 500;
            btn_next.Top = btn_back.Location.Y;
            this.Height = 655;

            //Display content
            btn_next.Text = "Create";
            pnl_content.Controls.Add(grp_create_batch);
            pnl_content.Controls.Add(grp_create_batch_information);
            pnl_steps.Height = pnl_content.Height;

            //Display tasks
            tasks.ModifyImage(states.Count - 2, Properties.Resources.arrow_gray);
            tasks.AddTask(Properties.Resources.arrow, "Create Batch", 15);
            tasks.Display(pnl_steps);

            //add the test to the list of available tests
            lst_available_tests.Items.Clear();
            available_tests = test.RetrieveTests();
            
            //check to see if an error occurred
            if(available_tests == null)
                MessageBox.Show(test.Error, "Error", MessageBoxButtons.OK, test.ErrorType);
            else{
                for(int x=0; x< available_tests.Length; x++)
                    lst_available_tests.Items.Add(available_tests[x]);
            }

            System.Windows.Forms.ListBox.SelectedObjectCollection t = lst_available_tests.SelectedItems;
        }

        private void DisplayPatientInformation()
        {
            states.Push((int)state.PatientInformation);

            String instr = "Patient Information for " +patient.ThisPatient.name;
            SetupForm(instr, "Patient Information", Properties.Resources.Information64);

            //Display content
            pnl_content.Controls.Add(grp_patient_information);
            pnl_content.Controls.Add(pnl_patient_notes);
            pnl_content.Controls.Add(btn_add_note);
            btn_next.Enabled = false;

            //Display tasks
            tasks.ModifyImage(states.Count - 2, Properties.Resources.arrow_gray);
            tasks.AddTask(Properties.Resources.arrow, "Patient Information", 30);
            tasks.Display(pnl_steps);

            //display the current patient's information
            lbl_patient_dob.Text = "DOB: " + String.Format("{0:d}", patient.ThisPatient.dob);
            lbl_patient_handedness.Text = "Handedness: " + patient.ThisPatient.handedness;
            lbl_patient_name.Text = "Name: " + patient.ThisPatient.name;
            lbl_patient_sex.Text = "Sex: " + patient.ThisPatient.sex;
            lbl_patient_contact.Text = "Contact Information:\r\n" + patient.ThisPatient.ContactInfo;
            
            //set the location of the address to the bottom of the contact info.
            //we need to adjust it since some patients will have more contact info than others,
            //and the size of the textbox changes dynamically 
            lbl_patient_address.Location = new Point(lbl_patient_address.Location.X, lbl_patient_contact.Location.Y + lbl_patient_contact.Height + 15);
            lbl_patient_address.Text = "Address:\r\n" + patient.ThisPatient.address;

            //display patient notes
            Notes[] patient_notes = patient.GetNotes(patient.ThisPatient);
            lbl_patient_notes.Text = "";
            for (int x = 0; x < patient_notes.Length; x++)
            {
                lbl_patient_notes.Text += "Author: " + patient_notes[x].author + "\r\n";
                lbl_patient_notes.Text += "Date: " + String.Format("{0:d}",patient_notes[x].time) + "\r\n";
                lbl_patient_notes.Text += patient_notes[x].note + "\r\n\r\n";
            }
        }

        /// <summary>
        /// Displays the tasks a clinician will perform after adding/retrieving a patient
        /// </summary>
        private void DisplayClinicianTasks()
        {
            states.Push((int)state.ClinicianTasks);

            String instr = "Select a task.";
            SetupForm(instr, "Select A Patient Task", Properties.Resources.action64);

            //Display content
            pnl_content.Controls.Add(grp_clinician_tasks);

            //Display tasks
            tasks.ModifyImage(states.Count - 2, Properties.Resources.arrow_gray);
            tasks.AddTask(Properties.Resources.arrow, "Patient Tasks", 15);
            tasks.Display(pnl_steps);

            //display the current patient's information
            lbl_patient_dob.Text = "DOB: " + patient.ThisPatient.dob;
            lbl_patient_handedness.Text = "Handedness: " + patient.ThisPatient.handedness;
            lbl_patient_name.Text = "Name: " + patient.ThisPatient.name;
            lbl_patient_sex.Text = "Sex: " + patient.ThisPatient.sex;

        }

        private void Movement_Load(object sender, EventArgs e)
        {
            DisplayLogin();
        }

        private void StartTestBatch(char sender)
        {
            TestPatient t;
            List<Script> temp = new List<Script>();

            if (cmb_batches.SelectedItem == null)
            {
                MessageBox.Show("You must select a test batch before continuing.", "Error Retrieving Batches", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (sender == 'p')
            {
                temp.Add(test.GetScript((ScriptInfo)cmb_batches.SelectedItem));
                t = new TestPatient(temp, ((ScriptInfo)cmb_batches.SelectedItem).name, _User, patient.ThisPatient, sender);
            }
            else
            {
                for (int i = 0; i < ((Batch)cmb_batches.SelectedItem).scripts.Length; i++)
                    temp.Add(test.GetScript(((Batch)cmb_batches.SelectedItem).scripts[i]));

                t = new TestPatient(temp, ((Batch)cmb_batches.SelectedItem).name, _User, patient.ThisPatient, sender);
            }

            t.ShowDialog();

        }

        /// <summary>
        /// Advance to the next step based on the goal selected by the user
        /// </summary>
        private void PostLoginAction()
        {
            //Add Patient
            if (opt_add_patient.Checked)
            {
                DisplayAddPatient();
            }

            //Create Test Batch
            else if (opt_retrieve_patient.Checked)
            {
                DisplayRetrievePatient();
            }
 
            //Create Test Batch
            else if (opt_create_batch.Checked)
            {
                DisplayCreateBatch();
            }

            //Create New User
            else if (opt_create_user.Checked)
            {
                DisplayCreateUser();
            }
        
        }

        private void CreateBatch()
        {
            if (txt_batch_name.Text.Equals("") || lst_test_batch.Items.Count == 0)
            {
                MessageBox.Show("The batch must contain at least one test, and you must give the test batch a name.", "Required Data Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ScriptInfo[] selected_tests = new ScriptInfo[lst_test_batch.Items.Count];
            for (int x = 0; x < lst_test_batch.Items.Count; x++)
                selected_tests[x] = (ScriptInfo)lst_test_batch.Items[x];

            if (test.CreateBatch(txt_batch_name.Text, txt_description.Text, selected_tests))
            {
                lst_test_batch.Items.Clear();
                txt_description.Clear();
                txt_batch_name.Clear();
                MessageBox.Show("The batch was created successfully!", "Batch Created", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                MessageBox.Show(test.Error, "Error", MessageBoxButtons.OK, test.ErrorType);


        }

        private void AddPatient()
        {
            Char sex;
            Char hand;
            DateTime dob;
            if (txt_patient_name.Text.Equals("") || msk_patient_dob.Text.Equals("  /  /") || msk_patient_dob.Text.Length != 10 || msk_patient_ssn.Text.Equals("") || (rdbtn_female.Checked && rdbtn_male.Checked) || (rdbtn_left.Checked && rdbtn_right.Checked))
            {
                MessageBox.Show("Please fill out all of the required information.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //convert the entered DOB to a DateTime
            String[] date = msk_patient_dob.Text.Split('/');
            try { dob = new DateTime(int.Parse(date[2]), int.Parse(date[0]), int.Parse(date[1])); }
            catch (Exception) { dob = new DateTime(1900, 1, 1); }

            if (rdbtn_female.Checked) sex = 'F';
            else sex = 'M';

            if (rdbtn_right.Checked) hand = 'R';
            else hand = 'L';

            int patient_id = patient.AddPatient(_User, txt_patient_name.Text, msk_patient_ssn.Text, dob, sex, hand, txt_patient_contact.Text, txt_add_patient_address.Text, txt_patient_notes.Text);
            
            if (patient_id == -1)
                MessageBox.Show("The patient you are trying to add already exists.", "Add Patient Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if(patient_id == -5)
                MessageBox.Show("An error occured while adding the patient.  Please try again!.", "Add Patient Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                MessageBox.Show("The patient was added successfully!", "Patient Added", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisplayClinicianTasks();
            }
        }

        /// <summary>
        /// Load the next screen based on the selected radio button
        /// </summary>
        private void ClinicianTasks()
        {
            //Test Patient
            if (opt_test_patient.Checked)
                DisplayBatchSelection();

            //Practice Test
            else if (opt_practice_test.Checked)
                DisplayPracticeTest();

            //Patient History
            else if (opt_patient_history.Checked)
                DisplayPatientHistory();

            else if (opt_patient_information.Checked)
                DisplayPatientInformation();

        }

        /// <summary>
        /// Establish connection to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connect()
        {
            //disable the connect button
            btn_connect.Enabled = false;
            web_connection.Connect(cmb_server.Text, txt_username.Text, txt_password.Text);
            //attempt connection
            if (web_connection.Connect(cmb_server.Text, txt_username.Text, txt_password.Text))
            { 
                //get the returned user object
                _User = web_connection.User;
                //_User.userName = txt_username.Text.ToString();
                //_User.password = txt_password.Text.ToString();
                //valid username & password entered
                if (_User.userName != null)
                {
                    web_connection.SaveServer("servers");
                    DisplayPostLogin();
                    mnu_connect.Enabled = false;
                    mnu_disconnect.Enabled = true;
                    btn_next.Text = "Next >";
                    btn_back.Text = "< Back";
                    txt_password.Clear();
                    txt_username.Clear();
                }

                //Invalid username/password entered
                  else 
                {
                    MessageBox.Show("Login Failed. An invalid username or password was entered.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            //connection failed
            else
            {
                MessageBox.Show("Login Failed. A connection could not be established with the sever.", "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //enable the connectio button
            btn_connect.Enabled = true;

        }


        /// <summary>
        /// Clean up the forms so we don't have garbage data
        /// </summary>
        /// <param name="state"></param>
        private void CleanUp(int clean)
        {
            switch (clean)
            {
                case (int)state.CreateBatch:
                    lst_available_tests.Items.Clear();
                    lst_test_batch.Items.Clear();
                    txt_description.Clear();
                    txt_batch_name.Clear();
                    break;
                case (int)state.AddPatient:
                    txt_add_patient_address.Clear();
                    txt_patient_notes.Clear();
                    txt_patient_contact.Clear();
                    msk_patient_ssn.Clear();
                    txt_patient_name.Clear();
                    msk_patient_dob.Clear();
                    rdbtn_male.Checked = true;
                    rdbtn_left.Checked = true;
                    break;
                case (int)state.CreateUser:
                    txt_contact_info.Clear();
                    txt_new_name.Clear();
                    txt_new_password_confirm.Clear();
                    txt_new_password.Clear();
                    txt_new_username.Clear();
                    cmb_role.SelectedIndex = 1;
                    break;
                case (int)state.RetrievePatient:
                    msk_ssn.Clear();
                    txt_name.Clear();
                    msk_dob.Clear();
                    lst_patients.Items.Clear();
                    break;
            }
        }
 
        /// <summary>
        /// Go back to the previous state/task of the user's goal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_back_Click(object sender, EventArgs e)
        {
            int state_current = states.Peek();

            //If the state is Login then switch to demo mode.
            if (state_current == (int)state.Login)
            {
                tasks.RemoveLast();
                DisplayPracticeTest();
                return;
            }

            states.Pop();
            int state_last = states.Pop();

            //remove the current task from the list
            CleanUp(state_current);
            tasks.RemoveLast();

            if (state_current == (int)state.PracticeTest && state_last == (int)state.Login)
            {
                DisplayLogin();
                return;
            }

            //remove the next one so when we go back, the same taks doesn't get added twice
            tasks.RemoveLast();

            //we want to remove the state we are currently at, so it doesn't go back to itself
            //and we want to check if it was CreateBatch so we can fix the form size
            if (state_current == (int)state.CreateBatch || state_current == (int)state.PatientHistory)
            {
                this.Height = 525;
                this.Width = 650;
                btn_back.Top = 365;
                btn_next.Top = 365;
                
                pnl_steps.Height = 399;
                btn_next.Text = "Next >";
            }
            else if (state_current == (int)state.PatientInformation)
            {
                btn_next.Enabled = true;
            }
            else if (state_current == (int)state.CreateUser || state_current == (int)state.AddPatient || states.Peek() == (int)state.BatchSelection || state_current == (int)state.PracticeTest)
            {
                btn_next.Text = "Next >";
            }
             
            if (state_current == (int)state.PatientHistory)
            {
                btn_back.Left = 305;
                btn_next.Left = 385;
                btn_next.Visible=true;
                lst_tests.Items.Clear(); 
            }

            switch (state_last)
            {
                case (int)state.CreateBatch:
                    DisplayCreateBatch();
                    break;
                case (int)state.AddPatient:
                    DisplayAddPatient();
                    break;
                case (int)state.PatientHistory:
                    DisplayPatientHistory();
                    break;
                case (int)state.BatchSelection:
                    DisplayBatchSelection();
                    break;
                case (int)state.CreateUser:
                    DisplayCreateUser();
                    break;
                case (int)state.PracticeTest:
                    DisplayPracticeTest();
                    break; 
                case (int)state.PostLogin:
                    DisplayPostLogin();
                    break; 
                case (int)state.RetrievePatient:
                    DisplayRetrievePatient();
                    break; 
                case (int)state.ClinicianTasks:
                    DisplayClinicianTasks();
                    break;
            }              
        }

        /// <summary>
        /// Go the next state/task of the user's goal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_next_Click(object sender, EventArgs e)
        {
            switch (states.Peek())
            {
                case (int)state.Login:
                    Connect();
                    break;
                case (int)state.PostLogin:
                    PostLoginAction();
                    break;
                case (int)state.AddPatient:
                    AddPatient();
                    break;
                case (int)state.RetrievePatient:
                    if(lst_patients.SelectedIndex == -1)
                        MessageBox.Show("You need to select a patient before you can continue.", "Please Select A Patient", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    else
                        DisplayClinicianTasks();
                    break;
                case (int)state.ClinicianTasks:
                    ClinicianTasks();
                    break;
                case (int)state.CreateBatch:
                    CreateBatch();
                    break;
                case (int)state.CreateUser:
                    CreateUser();
                    break;
                case (int)state.BatchSelection:
                    StartTestBatch('b');
                    break;
                case (int)state.PracticeTest:
                    StartTestBatch('p');
                    break;
            }
        }

        private void CreateUser()
        {
            //try to create the user
            if (users.CreateUser(txt_new_name.Text, txt_contact_info.Text, cmb_role.Text, txt_new_username.Text, txt_new_password.Text, txt_new_password_confirm.Text))
            {
                MessageBox.Show("The user was created successfully!", "User Created", MessageBoxButtons.OK, MessageBoxIcon.Information);

                //clear the fields
                txt_new_username.Clear();
                txt_contact_info.Clear();
                txt_new_name.Clear();
                cmb_role.SelectedIndex = 0;
                txt_new_password.Clear();
                txt_new_password_confirm.Clear();
            }
            else
                MessageBox.Show(users.Error, "Create User Failed", MessageBoxButtons.OK, users.ErrorType);
        }

        /// <summary>
        /// Exit the application
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //try to disconnect before exiting
            if (web_connection.Connected)
            {
                try
                {
                    web_connection.Disconnect();
                }
                catch (Exception) { }
            }
            
            this.Close();
        }

        /// <summary>
        /// Disconnect from the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnu_disconnect_Click(object sender, EventArgs e)
        {
            
            if (web_connection.Disconnect())
            {
                //fix the form size of disconnected while creating a batch
                if (states.Peek() == (int)state.CreateBatch || states.Peek() == (int)state.PatientHistory)
                {
                    this.btn_next.Location = new System.Drawing.Point(385, 365);
                    this.btn_back.Location = new System.Drawing.Point(305, 365);
                    this.pnl_content.Size = new System.Drawing.Size(468, 399);
                    this.Size = new Size(654, 525);
                    btn_next.Visible = true;
                    btn_back.Visible = true;
                    pnl_steps.Height = 399;                    
                }

                //we need to do a little housekeeping
                CleanUp(states.Peek());
                CleanUp((int)state.RetrievePatient);
                states.Clear();
                tasks.Clear();
                mnu_connect.Enabled = true;
                mnu_disconnect.Enabled = false;
                cmb_server.Items.Clear();
                DisplayLogin();

                opt_add_patient.Checked = false;
                opt_create_batch.Checked = false;
                opt_create_user.Checked = false;
                opt_patient_history.Checked = false;
                opt_patient_information.Checked = false;
                opt_practice_test.Checked = false;
                opt_retrieve_patient.Checked = false;
                opt_test_patient.Checked = false;



            }
            else
                MessageBox.Show(web_connection.Error, "Disconnect Error", MessageBoxButtons.OK, web_connection.ErrorType);
        }

        /// <summary>
        /// Retrieve a patient with the given search criteria
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_retrieve_Click(object sender, EventArgs e)
        {
            lst_patients.Items.Clear();
            DateTime dob;

            //convert the entered DOB to a DateTime
            String[] date = msk_dob.Text.Split('/');            
            try{ dob = new DateTime(int.Parse(date[2]), int.Parse(date[0]), int.Parse(date[1])); }
            catch (Exception) { dob = new DateTime(1900, 1, 1); }

            //get the list of matching patients
            patient_list = patient.RetrievePatient(txt_name.Text, msk_ssn.Text, dob);
            
            //display the matches
            for (int x = 0; x < patient_list.Count; x++)
                lst_patients.Items.Add(patient_list[x]);

            //No patient found!
            if(patient_list.Count ==0)
            {
                MessageBox.Show("No patients matched your search criteria.", "No Patients Found", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Display the selected patient's name, ssn, and dob
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lst_patients_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get the index of the selected patient
            int index = lst_patients.SelectedIndex;

            //an actual patient name was selected, not just an empty area within the listbox
            if (index >= 0)
            {
                //set our patient to the one selected
                patient.ThisPatient = patient_list[index];

                //display the selected patient's name, ssn, and dob in the search criteria boxes
                txt_name.Text = patient.ThisPatient.name;
                msk_ssn.Text = patient.ThisPatient.ssn;

                //convert the date to use with our mask
                String month = patient.ThisPatient.dob.Month.ToString();
                String day = patient.ThisPatient.dob.Day.ToString();

                if (month.Length == 1)
                    month = month.PadLeft(2, '0');
                if (day.Length == 1)
                    day = day.PadLeft(2, '0');

                msk_dob.Text = month + day + patient.ThisPatient.dob.Year.ToString();
            }
        }

        /// <summary>
        /// Display the selected batch's description
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmb_batches_SelectedIndexChanged(object sender, EventArgs e)
        {
            String description = "";
            Script temp;
            TestEngine.Testing.TestScript p = new global::Movement.TestEngine.Testing.TestScript();

            //get the description of the selected item
            if (states.Peek() == (int)state.PracticeTest)
            {
                description = ((ScriptInfo)cmb_batches.SelectedItem).description;
                
                //display the script preview
                temp = test.GetScript((ScriptInfo)cmb_batches.SelectedItem);
                p.ScriptBody = temp.scriptData;
                p.TestScriptID = temp.scriptID;
                script_preview.Script = p;

                if (cmb_batches.SelectedIndex != -1 && !pnl_content.Controls.Contains(script_preview))
                    pnl_content.Controls.Add(script_preview);

            }
            else if (states.Peek() == (int)state.BatchSelection)
                description = ((Batch)cmb_batches.SelectedItem).description;

            
            //set the description if none is available
            if (description.Equals(""))
                description = "No description available.";

            //display description
            lbl_batch_description.Text = description;


        }

        /// <summary>
        /// Move the selected test up one position in the test batch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// BMN
        private void move_test_up_Click(object sender, EventArgs e)
        {
            //move all selected items up
            for (int x = 0; x < lst_test_batch.SelectedIndices.Count; x++)
            {
                //set the currently selected item       
                int i = lst_test_batch.SelectedIndices[x];

                //swap the selected item with the item above it
                if (i > 0)
                {
                    Object temp = lst_test_batch.Items[i - 1];
                    lst_test_batch.Items[i - 1] = lst_test_batch.Items[i];
                    lst_test_batch.Items[i] = temp;

                    //set the selected index to the item that is being moved
                    lst_test_batch.SelectedIndex = i - 1;

                    //remove the item as selected in the list
                    lst_test_batch.SetSelected(i, false);
                }
            }
        }

        /// <summary>
        /// Move the selected test down one position in the test batch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// BMN
        private void move_test_down_Click(object sender, EventArgs e)
        {
            //move all selected items down
            for (int x = lst_test_batch.SelectedIndices.Count - 1; x >= 0; x--)
            {
                //set the currently selected item       
                int i = lst_test_batch.SelectedIndices[x];

                //swap the selected item with the item below it
                if (i < lst_test_batch.Items.Count - 1 && i >= 0)
                {
                    Object temp = lst_test_batch.Items[i + 1];
                    lst_test_batch.Items[i + 1] = lst_test_batch.Items[i];
                    lst_test_batch.Items[i] = temp;

                    //set the selected index to the item that is being moved
                    lst_test_batch.SelectedIndex = i + 1;

                    //remove the item as selected in the list
                    lst_test_batch.SetSelected(i, false);
                }
            }
        }
        
        /// <summary>
        /// Adds the selected tests to the test batch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// BMN
        private void add_test_Click(object sender, EventArgs e)
        {
            //loop through selected tests and add to the test batch
            for (int x = 0; x < lst_available_tests.SelectedItems.Count; x++)
                lst_test_batch.Items.Add(lst_available_tests.SelectedItems[x]);
        }

        /// <summary>
        /// Remove the selected tests from the test batch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// BMN
        private void remove_test_Click(object sender, EventArgs e)
        {
            //loop through the selected tests and remove them
            for (int x = lst_test_batch.SelectedItems.Count; x > 0; x--)
            {
                int selected = lst_test_batch.SelectedIndex;
                lst_test_batch.Items.RemoveAt(selected);
            }
        }

        private void btn_add_note_Click(object sender, EventArgs e)
        {
            notes = new PatientNotes(_User, patient.ThisPatient, lbl_patient_notes);
            notes.ShowDialog(this);
        }


        //Patient History List Box
        private void lst_tests_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lst_tests.SelectedIndex >=0 )
            {
                DataTable grid_data = new DataTable();
                DataRow rows;
                grid_data.Columns.Add("Metric");
                grid_data.Columns.Add("Mean");
                grid_data.Columns.Add("Std Dev");
                grid_data.Columns.Add("Min");
                grid_data.Columns.Add("Max");

                for (int k = 0; k < history_test_list[lst_tests.SelectedIndex].anal.Length; k++)
                {
                    rows = grid_data.NewRow();
                    rows["Metric"] = history_test_list[lst_tests.SelectedIndex].anal[k].metric;
                    rows["Mean"] = history_test_list[lst_tests.SelectedIndex].anal[k].mean;
                    rows["Std Dev"] = history_test_list[lst_tests.SelectedIndex].anal[k].stdDev;
                    rows["Min"] = history_test_list[lst_tests.SelectedIndex].anal[k].min;
                    rows["Max"] = history_test_list[lst_tests.SelectedIndex].anal[k].max;
                    grid_data.Rows.Add(rows);
                }

                dataGrid.DataSource = grid_data;

                hand.Text = history_test_list[lst_tests.SelectedIndex].hand.ToString();
                mode.Text = history_test_list[lst_tests.SelectedIndex].mode.ToString();
                rotation.Text = history_test_list[lst_tests.SelectedIndex].rotation.ToString();
                test_name.Text = history_test_list[lst_tests.SelectedIndex].script.type.ToString();
                normal.Text = history_test_list[lst_tests.SelectedIndex].isNormal.ToString();

                List<Data> samples = test.RetrieveTestData(history_test_list[lst_tests.SelectedIndex].ID);
                List<global::Movement.TestEngine.Capture.CalibratedInkSample> testSamples = samples.ConvertAll<global::Movement.TestEngine.Capture.CalibratedInkSample>(
                    new Converter<Data, global::Movement.TestEngine.Capture.CalibratedInkSample>(
                    delegate(Data d)
                    {
                        global::Movement.TestEngine.Capture.CalibratedInkSample sample = new global::Movement.TestEngine.Capture.CalibratedInkSample();
                        sample.X = d.x;
                        sample.Y = d.y;
                        sample.Time = d.time;
                        sample.Pressure = d.pressure;
                        return sample;
                    }));

                analysis_control.ShowAnalysis(global::Movement.Analysis.AnalysisMetric.Pressure,
                    testSamples);

                ScriptInfo si = new ScriptInfo();
                si.scriptID = history_test_list[lst_tests.SelectedIndex].script.scriptID;
                Script s = test.GetScript(si);
                global::Movement.TestEngine.Testing.TestScript ts = new global::Movement.TestEngine.Testing.TestScript(si.scriptID, s.scriptData);

                replay_control.ReplayTest(ts, testSamples);
            }
        }

        private void lnk_test_preview_Click(object sender, EventArgs e)
        {
            if (lst_available_tests.SelectedItems.Count > 0)
            {
                TestPreview preview = new TestPreview(lst_available_tests.SelectedItems);
                preview.ShowDialog();
            }
        }



        /**********************/
        /*    Data Members    */
        /**********************/
        private TaskList tasks;
        private Stack<int> states;
        private UserObject _User;   //Current, logged in user
        private Connection web_connection;
        private Patient patient;
        private Test test;
        private User users;
        private PatientNotes notes;
        private List<PatientObject> patient_list;
        private List<global::Movement.UserInterface.movement_web_service.Test> history_test_list;
        
        private enum state
        {
            PostLogin,
            AddPatient,
            RetrievePatient,
            CreateUser,
            CreateBatch,
            Login,
            PatientHistory,
            BatchSelection,
            PracticeTest,
            ClinicianTasks,
            PatientInformation
        };

        private void mnu_about_movement_Click(object sender, EventArgs e)
        {
            About about_movement = new About();
            about_movement.ShowDialog(this);
        }

        /// <summary>
        /// Display the movment help content
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnu_help_Click(object sender, EventArgs e)
        {
            try { System.Diagnostics.Process.Start("Movement.hlp"); }
            catch (System.ComponentModel.Win32Exception exp)
            {
                MessageBox.Show(exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
