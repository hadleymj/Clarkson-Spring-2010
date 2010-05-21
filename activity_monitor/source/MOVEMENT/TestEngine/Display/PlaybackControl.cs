using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Movement.TestEngine.Capture;

namespace Movement.TestEngine.Display
{
    public delegate void PlaybackResumeHandler();
    public delegate void PlaybackPauseHandler();
    public delegate void PlaybackSeekHandler(ulong seekTime);

    internal partial class PlaybackControl : UserControl
    {
        #region Private Attributes

        private List<CalibratedInkSample> _Samples = new List<CalibratedInkSample>();
        private int _MaximumTime = 0;
        
        #endregion

        public event PlaybackResumeHandler PlaybackResume;
        public event PlaybackPauseHandler PlaybackPause;
        public event PlaybackSeekHandler PlaybackSeek;

        public PlaybackControl()
        {
            InitializeComponent();
        }

        public void PlaybackStart(List<CalibratedInkSample> samples)
        {
            _Samples = samples;

            if (_Samples.Count > 0)
                _MaximumTime = _Samples[_Samples.Count - 1].Time;
            else
                _MaximumTime = 0;

            Invalidate(true);
        }

        private int lastElapsedUS = -1;
        
        public void PlaybackTick(int elapsedUS)
        {
            if (InvokeRequired)
            {
                Invoke(new ReplayTickHandler(PlaybackTick), elapsedUS);
            }
            else
            {   //invoke safe

                using(Graphics g = PanelProgress.CreateGraphics())
                {
                    PlaybackIndicatorInvert(g, lastElapsedUS);  //first invert (ie. un-invert) the last indicator line
                    PlaybackIndicatorInvert(g, elapsedUS);  //hten invert the new indicator line
                }               

                lastElapsedUS = elapsedUS;
            }
        }

        private void ButtonPlayPause_Click(object sender, EventArgs e)
        {
            if (ButtonPlayPause.Text.Equals("Pause"))
            {   //do pause; make play available
                ButtonPlayPause.Text = "Play";
                
                if(PlaybackPause != null)
                    PlaybackPause();
            }
            else// if (ButtonPlayPause.Text.Equals("Play"))
            {   //do play; make pause available
                ButtonPlayPause.Text = "Pause";

                if (PlaybackResume != null)
                    PlaybackResume();
            }
                
        }

        private void PanelProgress_MouseUp(object sender, MouseEventArgs e)
        {
            if (PlaybackSeek != null)
                PlaybackSeek(XToTime(e.X));
        }

        private void PanelProgress_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void PanelProgress_Paint(object sender, PaintEventArgs e)
        {
            Pen SamplePen = new Pen(Brushes.DarkBlue, 4);

            foreach (CalibratedInkSample s in _Samples)
            {
                if(s.IsInvalid())
                    continue;

                float x = TimeToX(s.Time);
                e.Graphics.DrawLine(SamplePen, x, 0, x, Height);
            }

            PlaybackIndicatorInvert(e.Graphics, lastElapsedUS);
        }

        private void PlaybackIndicatorInvert(Graphics g, int elapsedUS)
        {
            //get the upper left corner of the start of the invert operation
            Point PPOrigin = Point.Round(new PointF(TimeToX(elapsedUS), 0));

            if (!g.Clip.IsVisible(PPOrigin, g))  //dont bother inverting outside of the graphics clip region
                return; //this is important for partial re-paint events

            g.CopyFromScreen(   //do the invert operation
                PPOrigin,
                PPOrigin,
                new Size(4, PanelProgress.Height),
                CopyPixelOperation.DestinationInvert);
        }

        private float TimeToX(int time)
        {
            return ((float)time * PanelProgress.Width) / _MaximumTime;
        }

        private ulong XToTime(int x)
        {
            return (ulong)(((double)x / PanelProgress.Width) * _MaximumTime);
        }
    }
}
