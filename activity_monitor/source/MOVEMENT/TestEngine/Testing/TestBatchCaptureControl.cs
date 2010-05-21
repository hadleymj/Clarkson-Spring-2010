using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Movement.TestEngine.Testing
{
    public delegate void TestBatchCompleteHandler();

    public partial class TestBatchCaptureControl : Movement.TestEngine.Testing.TestCaptureControl
    {
        #region Private Attributes

        private Queue<TestScript> _Scripts = new Queue<TestScript>();
        private object _CallbackState;

        #endregion

        public bool BatchRunning { get { return _Scripts.Count > 0; } }

        /// <summary>
        /// Occurs whenever the test batch (and all tests) have completed.
        /// </summary>
        public event TestBatchCompleteHandler TestBatchComplete;

        public TestBatchCaptureControl()
        {
            InitializeComponent();
        }

        public void RunBatch(object state, List<TestScript> scripts)
        {
            _CallbackState = state;

            //queue the scripts to run
            foreach (TestScript script in scripts)
                _Scripts.Enqueue(script);

            //run the first one
            RunNextTest();
        }

        public void StopBatch()
        {
            if (!BatchRunning)
                return;

            _Scripts.Clear();
            
            if(TestRunning)
                StopTest();
        }

        private void RunNextTest()
        {
            if (_Scripts.Count == 0)
            {
                mLabelStatus.Text = "Testing complete.";

                if (TestBatchComplete != null)
                    TestBatchComplete();

                return;
            }

            TestScript NextScript = _Scripts.Dequeue();

            mLabelStatus.Text = string.Format("Running test script ID {0}.  {1} test(s) remaining.",
                NextScript.TestScriptID,
                _Scripts.Count);

            RunTest(_CallbackState, NextScript);
        }

        private void TestBatchCaptureControl_TestComplete(object state, int testScriptID, List<Capture.CalibratedInkSample> samples)
        {
        }

        private void TestBatchCaptureControl_PostTestComplete(object state)
        {
            //run the next test
            RunNextTest();
        }
    }
}

