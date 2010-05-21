using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Movement.Scripting;

namespace Movement.TestEngine.Testing
{
    public partial class TestReplayControl : Movement.TestEngine.Display.ReplayInkOutputControl
    {
        #region Private Attributes

        private TestScriptRenderer _ScriptRenderer = null;

        #endregion

        public TestReplayControl()
        {
            InitializeComponent();
        }

        public void ReplayTest(TestScript script, List<Capture.CalibratedInkSample> samples)
        {
            Clear();

            if (_ScriptRenderer != null)    //clean up any existing script renderer
                _ScriptRenderer.Dispose();

            ScriptEngine ScriptEngine = new ScriptEngine(script.ScriptBody);

            _ScriptRenderer = new TestScriptRenderer(this, ScriptEngine);

            DrawPressureMode = ScriptEngine.PressureFeedback;
            if (DrawPressureMode == InkPressureFeedbackMode.None) DrawPressureMode = InkPressureFeedbackMode.Color;          

            base.Play(samples);
        }
    }
}

