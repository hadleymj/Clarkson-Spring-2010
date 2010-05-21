using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Movement.TestEngine.Capture;

namespace Movement.TestEngine.Display
{
    internal partial class PipedInkOutputControl : InkOutputControl
    {
        #region Private Attributes
        private InkInputControl _InputControl;
        #endregion

        public InkInputControl InputControl
        {
            get
            {
                return _InputControl;
            }
            set
            {
                DetatchInput();
                _InputControl = value;
                AttachInput();
            }
        }

        public PipedInkOutputControl()
        {
            InitializeComponent();
        }

        private void AttachInput()
        {
            if (_InputControl == null)
                return;

            _InputControl.NewInkSample += new NewInkSampleHandler(InputControl_NewInkSample);
        }

        private void DetatchInput()
        {
            if (_InputControl == null)
                return;

            _InputControl.NewInkSample -= new NewInkSampleHandler(InputControl_NewInkSample);
            _InputControl = null;
        }

        private void InputControl_NewInkSample(CalibratedInkSample s)
        {
            DrawSample(s);
        }
    }
}

