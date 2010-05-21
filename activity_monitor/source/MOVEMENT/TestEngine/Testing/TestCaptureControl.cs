using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

using Movement.Scripting;

namespace Movement.TestEngine.Testing
{
    public delegate void TestCompleteHandler(object state, int testScriptID, List<Capture.CalibratedInkSample> samples);
    public delegate void PostTestCompleteHandler(object state);

    public partial class TestCaptureControl : Movement.TestEngine.Capture.InkInputControl
    {
        #region Private Attributes
        private TestScriptRenderer _ScriptRenderer = null;
        private CognitiveMapForm _CognitiveDisplay = null;
        private int _CallbackScriptID = -1;
        private object _CallbackState = null;

        private const int _TestIdleTimeout = 5; // seconds
        private int _TestTimeout = -1; // seconds (-1 means infinite)
        private DateTime _TestStart = DateTime.MinValue;
        private DateTime _TestLastSample = DateTime.MinValue;
        #endregion

        /// <summary>
        /// Occurs as each test completes.
        /// </summary>
        public event TestCompleteHandler TestComplete;

        /// <summary>
        /// Occurs as each test completes, after TestComplete.
        /// </summary>
        public event PostTestCompleteHandler PostTestComplete;

        /// <summary>
        /// True if a test is running currently.  Otherwise false.
        /// </summary>
        public bool TestRunning { get { return DataCapture.IsCapturing; } }

        /// <summary>
        /// The timestamp that test was started at (UTC time).
        /// </summary>
        public DateTime TestStart { get { return _TestStart; } }

        public TestCaptureControl()
        {
            InitializeComponent();
        }

        public void RunTest(object state, TestScript script)
        {
            if (TestRunning)    //don't allow a new test to be run while one is already running
                throw new InvalidOperationException("A script is already running.");

            if (_ScriptRenderer != null)    //clean up any existing script renderer
                _ScriptRenderer.Dispose();

            ScriptEngine ScriptEngine = new ScriptEngine(script.ScriptBody);

            //create a new script renderer for this control
            _ScriptRenderer = new TestScriptRenderer(this, ScriptEngine, ScriptEngine.Cognitive);

            if (ScriptEngine.Cognitive)
            {
                _CognitiveDisplay = new CognitiveMapForm();
                _CognitiveDisplay.Show();
                _CognitiveDisplay.RunScript(this, ScriptEngine);

                DrawPressureMode = ScriptEngine.PressureFeedback;
                DrawInk = ScriptEngine.DirectFeedback;
            }
            else /*IsDirect*/
            {
                DrawPressureMode = ScriptEngine.PressureFeedback;
                DrawInk = ScriptEngine.DirectFeedback;
            }

            mLabelInstructions.Text = ScriptEngine.Instructions;

            _CallbackScriptID = script.TestScriptID;
            _CallbackState = state;
            
            //clear collected samples
            Clear();

            _TestTimeout = ScriptEngine.TimeLimit;
            _TestLastSample = _TestStart = DateTime.UtcNow;
            StartCapture();
        }

        public void StopTest()
        {
            if (!TestRunning)
                return;

            StopCapture();

#if(DEBUG)
            Console.WriteLine("Test Done");
#endif

            if (_CognitiveDisplay != null)  //close the cognitive display if it is open
            {
                _CognitiveDisplay.Close();
                _CognitiveDisplay.Dispose();
                _CognitiveDisplay = null;
            }

            //fire the test complete event
            if (TestComplete != null)
                TestComplete(_CallbackState, _CallbackScriptID, DataCapture.CapturedPackets);

            if (PostTestComplete != null)
                PostTestComplete(_CallbackState);
        }

        private void TestCaptureControl_NewInkSample(Movement.TestEngine.Capture.CalibratedInkSample calSample)
        {
            //if we have a new ink sample, record the time
            _TestLastSample = DateTime.UtcNow;
        }

        private void mTestTimer_Tick(object sender, EventArgs e)
        {
            if (TestRunning)
                mLabelTimer.Text = DateTime.UtcNow.Subtract(TestStart).TotalSeconds.ToString("0.00");

            if (TestRunning //if there is a test running and...
                && ((_TestStart != _TestLastSample && DateTime.UtcNow.Subtract(_TestLastSample).TotalSeconds > _TestIdleTimeout)   //we have actually started and exceeded idle timeout since last sample
                    || (_TestTimeout > 0 && DateTime.UtcNow.Subtract(_TestStart).TotalSeconds > _TestTimeout))) //we have exceeded test timeout since test start
                StopTest(); //stop the test!
        }


    }
}

