using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Microsoft.Ink;

using Movement.Scripting;
using Movement.TestEngine.Capture;
using Movement.TestEngine.Capture.Calibration;

namespace Movement.TestEngine.Display
{
	public partial class InkOutputControl : UserControl
    {
        #region Private Attributes

        private PressurePen _InkPen;
        private bool _DrawInk = true;
        private InkPressureFeedbackMode _DrawPressureMode = InkPressureFeedbackMode.None;

        private List<CalibratedInkSample> _Ink = new List<CalibratedInkSample>();

        #endregion

        public bool DrawInk { get { return _DrawInk; } set { _DrawInk = value; } }
        public InkPressureFeedbackMode DrawPressureMode { get { return _DrawPressureMode; } set { _DrawPressureMode = value; } }

        public InkOutputControl()
		{
            InitializeComponent();

            _InkPen = new PressurePen(this, InkCalibration.Current.MaximumCalibratedPressure);
		}

        public void Clear()
        {
            _Ink.Clear();
            Invalidate();
        }

        protected void DrawSample(CalibratedInkSample cal)
        {
            if (!DrawInk)
                return;

            using (Graphics g = CreateGraphics())
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                DrawSample(g, Size, cal);
            }
        }

        private void DrawSample(Graphics g, Size clientArea, CalibratedInkSample sample)
        {
            CalibratedInkSample lastSample;
            
            if(_Ink.Count == 0)  //no previous ink samples
                lastSample = new CalibratedInkSample(0,0,0,-1);
            else
                lastSample = _Ink[_Ink.Count - 1];

            RawInkSample raw = InkCalibration.Current.CalibratedToRaw(g, sample, clientArea);  //the current raw sample
            RawInkSample lastRaw = (lastSample.IsInvalid()
                                    ? raw : InkCalibration.Current.CalibratedToRaw(g, lastSample, clientArea));

            _InkPen.Draw(
                g,
                DrawPressureMode,
                lastSample,
                sample,
                lastRaw,
                raw);

            _Ink.Add(sample);
        }

        private void InkOutputControl_Paint(object sender, PaintEventArgs e)
        {
            if (DrawPressureMode == InkPressureFeedbackMode.Size)   //TODO: just draw the last sample
                return;

            Size ClientArea = Size;
            List<CalibratedInkSample> InkCopy = _Ink;
            _Ink = new List<CalibratedInkSample>(_Ink.Count);

            foreach (CalibratedInkSample cal in InkCopy)
            {
                DrawSample(e.Graphics, ClientArea, cal);
            }            
        }

        private void InkOutputControl_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }
	}
}
