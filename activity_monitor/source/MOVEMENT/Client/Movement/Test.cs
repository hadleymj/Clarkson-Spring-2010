using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Movement.UserInterface.movement_web_service;
using System.IO;

namespace Movement.UserInterface
{
    class Test
    {
        WebServicer Connection = new WebServicer();
        String error;
        MessageBoxIcon error_type;


        /// <summary>
        /// Returns a list of available batches
        /// </summary>
        /// <returns></returns>
        public Batch[] RetrieveBatches()
        {
            Batch[] temp;

            try
            {
                temp = Connection.Servicer.getBatchList();
                if (temp == null)
                {
                    error = "No batches are available at this time.";
                    error_type = MessageBoxIcon.Warning;
                }

            }
            catch (System.Net.WebException e)
            {
                error = e.Message;
                error_type = MessageBoxIcon.Error;
                return null;
            }

            return temp;
        }

        private List<ScriptInfo> test_list;
        private List<Script> script_list;

        

        public ScriptInfo[] LocalRetrieveTests()
        {

            test_list = new List<ScriptInfo>();
            script_list = new List<Script>();

            FileStream inFile = new FileStream("../../tests.dat", FileMode.Open);

            TextReader tr = new StreamReader(inFile);

            string curLine = null;
            ScriptInfo curInfo = new ScriptInfo();
            Script curScript = new Script();

            while ((curLine = tr.ReadLine()) != null)
            {
                if (curLine.StartsWith("####"))
                {
                    curInfo.scriptID = test_list.Count;
                    curScript.scriptID = script_list.Count;

                    test_list.Add(curInfo);
                    script_list.Add(curScript);
                    curInfo = new ScriptInfo();
                    curScript = new Script();
                }
                else if (curLine.StartsWith("Name: "))
                    curInfo.name = curLine.Substring(6);

                else if (curLine.StartsWith("Description: "))
                    curInfo.description = curLine.Substring(13);

                else if (curLine.StartsWith("Version: "))
                    curInfo.versionID = Int32.Parse(curLine.Substring(9));

                else if (curLine.StartsWith("TestData: <<--"))
                {
                    curLine = tr.ReadLine();
                    while (null != curLine && !curLine.StartsWith("-->>"))
                    {
                        curScript.scriptData += curLine + "\r\n";
                        curLine = tr.ReadLine();
                    }

                }
            }
            tr.Close();

            return test_list.ToArray();
        }
        /// <summary>
        /// Returns a list of available tests
        /// </summary>
        /// <returns></returns>
        public ScriptInfo[] RetrieveTests()
        {
            ScriptInfo[] temp;

            try
            {
                temp = Connection.Servicer.getScriptList();
                if(temp == null){
                    error = "No tests are available at this time.";
                    error_type = MessageBoxIcon.Warning;
                }

            }
            catch (System.Net.WebException e)
            {
                error = e.Message;
                error_type = MessageBoxIcon.Error;
                return null;
            }

            return temp;
        }

        /// <summary>
        /// Returns a test script
        /// </summary>
        /// <param name="test">ScriptInfo object</param>
        /// <returns></returns>
        public Script GetScript(ScriptInfo test)
        {

            if (test.versionID == 0)
            {
                return (script_list.ToArray())[test.scriptID];
            }


            try
            {
                return Connection.Servicer.getScript(test);
            }
            catch (System.Net.WebException e)
            {
                error = e.Message;
                error_type = MessageBoxIcon.Error;
                return null;
            }

        }

        public List <movement_web_service.Test> RetrieveHistory(PatientObject patient)
        {

            return new List<global::Movement.UserInterface.movement_web_service.Test>(Connection.Servicer.getTests(0, patient));

        }

        public List<Data> RetrieveTestData(int testID)
        {
            return new List<Data>(Connection.Servicer.getTestData(testID));
        }


        /// <summary>
        /// Create a new test batch
        /// </summary>
        /// <param name="name">Name of the test batch</param>
        /// <param name="description">Description of the test batch</param>
        /// <param name="tests">Array containing all of the tests to add to the batch</param>
        /// <returns></returns>
        public Boolean CreateBatch(String name, String description, ScriptInfo[] tests)
        {
            //set the batch's properties
            Batch temp = new Batch();
            temp.name = name;
            temp.description = description;
            temp.scripts = tests;
            bool result;

            //create the batch
            try
            {
                result= Connection.Servicer.makeBatch(temp);
            }
            catch (System.Net.WebException e)
            {
                error = e.Message;
                error_type = MessageBoxIcon.Error;
                return false;
            }

            //batch creation failed
            if (!result)
            {
                error = "An error occured while creating the batch.  Please try again.";
                error_type = MessageBoxIcon.Error;
                return false;
            }

            return true;
        }


        /// <summary>
        /// Sends a completed test taken by a patient to the sever
        /// </summary>
        /// <param name="patient">Patient taking the test</param>
        /// <param name="user">Current user logged into Movement</param>
        /// <param name="samples">Data captured during the test</param>
        /// <param name="id">Script ID</param>
        /// <returns></returns>
        public Boolean SendTestData(PatientObject patient, UserObject user, List<global::Movement.TestEngine.Capture.CalibratedInkSample> samples, int id)
        {
            movement_web_service.Test completed_test = new global::Movement.UserInterface.movement_web_service.Test();
            ScriptInfo tempScript = new ScriptInfo();
            tempScript.scriptID = id;
            Scripting.ScriptEngine script_engine = new global::Movement.Scripting.ScriptEngine(Connection.Servicer.getScript(tempScript).scriptData);
            Data[] test_data = new Data[samples.Count];
            short average_x =0, average_y=0;
            Data temp;

            //get the data in a form to send to the server
            for (int x = 0; x < samples.Count; x++)
            {
                temp = new Data();
                temp.pressure = (short)samples[x].Pressure;
                temp.time = samples[x].Time;
                temp.x = (short)samples[x].X;
                temp.y = (short)samples[x].Y;
               
                //we also need to compute the average x and y coordinates
                average_x += temp.x;
                average_y += temp.y;

                test_data[x] = temp;
            }

            //get the rest of the information about the test together to send to the sever
            completed_test.hand = patient.handedness;
            completed_test.patient = patient;
            completed_test.user = user;
            if(script_engine.Cognitive)
                completed_test.mode = 'C';
            else
                completed_test.mode = 'D';
            
            completed_test.rotation = 0;
            completed_test.avg_X = average_x/samples.Count;
            completed_test.avg_Y = average_y/samples.Count;
            completed_test.script = new Script();
            completed_test.script.scriptID = id;
            completed_test.data = test_data;

            //try to store the completed test
            if (Connection.Servicer.storeTest(completed_test))
                return true;

            return false;
        }

        /// <summary>
        /// Error property
        /// </summary>
        public String Error
        {
            get { return error; }
        }

        /// <summary>
        /// Error type property
        /// </summary>
        public MessageBoxIcon ErrorType
        {
            get { return error_type; }
        }
    }
}
