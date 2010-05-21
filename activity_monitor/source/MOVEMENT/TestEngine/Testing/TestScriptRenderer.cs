using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

using Movement.Scripting;

namespace Movement.TestEngine.Testing
{
    /// <summary>
    /// Renders a test script to a control.
    /// </summary>
    public class TestScriptRenderer : IDisposable
    {
        #region Private Attributes
        private bool _StartingPointOnly;
        private Control _AttachedControl;

        private GraphicsPath _ScriptPath;
        private Point _ScriptStartingPoint;
        #endregion

        public TestScriptRenderer(Control c, ScriptEngine scriptEngine)
            : this(c, scriptEngine, false)
        {
        }

        /// <summary>
        /// Creates a new test script renderer.
        /// </summary>
        /// <param name="c">The control that the script should be rendered to.</param>
        /// <param name="scriptEngine">The script engine that defines rendering behavior.</param>
        public TestScriptRenderer(Control c, ScriptEngine scriptEngine, bool startingOnly)
        {
            _AttachedControl = c;

            _ScriptPath = scriptEngine.Path;
            _ScriptStartingPoint = scriptEngine.StartingPoint;
            _StartingPointOnly = startingOnly;

            _AttachedControl.Resize += new EventHandler(_AttachedControl_Resize);
            _AttachedControl.Paint += new PaintEventHandler(_AttachedControl_Paint);
            _AttachedControl.Invalidate();
        }

        private void _AttachedControl_Paint(object sender, PaintEventArgs e)
        {
            DrawScriptPath(e.Graphics);
        }

        private void _AttachedControl_Resize(object sender, EventArgs e)
        {
            _AttachedControl.Invalidate();
        }

        public void DrawScriptPath(Graphics g)
        {
            GraphicsState OriginalGraphicsState = g.Save();
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //transform the path so that it is centered and scaled
            const int ScaleDistance = 10000;

            Point ScaleVector = new Point(ScaleDistance, ScaleDistance),
                  Origin = new Point(_AttachedControl.Width >> 1, _AttachedControl.Height >> 1);

            //compute the scale vector
            Movement.TestEngine.Capture.Calibration.InkCalibration.Current.InkRenderer.InkSpaceToPixel(g, ref ScaleVector);

            g.TranslateTransform(Origin.X, Origin.Y);
            g.ScaleTransform(
                ScaleVector.X / (float)ScaleDistance,
                -ScaleVector.Y / (float)ScaleDistance);

            if(!_StartingPointOnly)
                g.DrawPath(Pens.Black, _ScriptPath);

            DrawStartingPoint(g);

            g.Restore(OriginalGraphicsState);
        }

        private void DrawStartingPoint(Graphics g)
        {
            if (_ScriptStartingPoint.X == int.MaxValue
                || _ScriptStartingPoint.Y == int.MaxValue) return;

            g.FillEllipse(Brushes.Black, new Rectangle(_ScriptStartingPoint.X - 125, _ScriptStartingPoint.Y - 125, 250, 250));
        }

        #region IDisposable Members

        public void Dispose()
        {
            _AttachedControl.Resize -= new EventHandler(_AttachedControl_Resize);
            _AttachedControl.Paint -= new PaintEventHandler(_AttachedControl_Paint);

            _AttachedControl.Invalidate();
        }

        #endregion
    }
}
