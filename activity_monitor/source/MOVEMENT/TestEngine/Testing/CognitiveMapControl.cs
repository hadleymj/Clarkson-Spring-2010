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
    internal partial class CognitiveMapControl : Movement.TestEngine.Display.PipedInkOutputControl
    {
        #region Private Attributes

        private TestScriptRenderer _ScriptRenderer = null;

        #endregion

        public CognitiveMapControl()
        {
            InitializeComponent();
        }

        public void RunScript(ScriptEngine scriptEngine)
        {
            if (_ScriptRenderer != null)
                _ScriptRenderer.Dispose();

            _ScriptRenderer = new TestScriptRenderer(this, scriptEngine);

            DrawPressureMode = scriptEngine.PressureFeedback;
            DrawInk = scriptEngine.CognitiveFeedback;
        }
    }
}

