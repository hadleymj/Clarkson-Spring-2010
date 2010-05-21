using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Movement.UserInterface.movement_web_service;

namespace Movement.UserInterface
{
    public partial class TestPatient : Form
    {
        List<TestEngine.Testing.TestScript> test_scripts;
        char sender;

        PatientObject patient;
        UserObject user;
        Test t;

        public TestPatient(List<Script> tests, String batch_name, UserObject user, PatientObject patient, char sender)
        {
            InitializeComponent();
            this.patient = patient;
            this.user = user;
            this.t = new Test();
            this.sender = sender;

            TestEngine.Testing.TestScript s;
            test_scripts = new List<global::Movement.TestEngine.Testing.TestScript>();

            //Display the name of the batch as the form's title
            this.Text = "Running " + batch_name;

            //put the tests in a list in the form used by the test batch capture controle
            for (int x = 0; x < tests.Count; x++)
                test_scripts.Add(s = new global::Movement.TestEngine.Testing.TestScript(tests[x].scriptID, tests[x].scriptData));

            //callback functions for test completion and batch completion
            tbcc_test_patient.TestBatchComplete += new global::Movement.TestEngine.Testing.TestBatchCompleteHandler(testBatchCaptureControl1_TestBatchComplete);
            tbcc_test_patient.TestComplete += new global::Movement.TestEngine.Testing.TestCompleteHandler(testBatchCaptureControl1_TestComplete);
        }

        void testBatchCaptureControl1_TestComplete(object state, int testScriptID, List<global::Movement.TestEngine.Capture.CalibratedInkSample> samples)
        {
            //Send data to server
            bool success = true;
            if((char)state != 'p' )
                success = t.SendTestData(patient, user, samples, testScriptID);

            if(!success)
                MessageBox.Show("An error occurred while trying to communicate with the server.  Please check your Internet connection and try again.","Communication Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void testBatchCaptureControl1_TestBatchComplete()
        {
            this.Close();
        }

        private void TestPatient_Shown(object sender, EventArgs e)
        {
            //start the batch
            tbcc_test_patient.RunBatch(this.sender, test_scripts);
        }
    }
}