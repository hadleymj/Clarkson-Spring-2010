using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using Movement.Scripting;
using Movement.TestEngine.Capture;

namespace Movement.TestEngine.Display
{
    public delegate void ReplayStartHandler(List<CalibratedInkSample> samples);
    public delegate void ReplayTickHandler(int elapsedUS);

    public partial class ReplayInkOutputControl : Movement.TestEngine.Display.InkOutputControl
    {

        private InkAnimator _Animator = null;
        private bool _AnimatorClearNext = false;


        public event ReplayStartHandler ReplayStart;
        public event ReplayTickHandler ReplayTick;

        public ReplayInkOutputControl()
        {
            InitializeComponent();

            //we probably want to display pressure by default
            DrawPressureMode = InkPressureFeedbackMode.Color;

            //wire up the playback control
            ReplayStart += new ReplayStartHandler(mPlaybackControl.PlaybackStart);
            ReplayTick += new ReplayTickHandler(mPlaybackControl.PlaybackTick);
        }

        public void Play(List<CalibratedInkSample> samples)
        {
            _Animator = new InkAnimator(samples);
            _Animator.Play();

            //notify the start event (synchronously so that the list of samples can't be changed on us)
            Invoke(new ReplayStartHandler(OnReplayStart), samples);
        }

        private void ResumePlayback()
        {
            if (_Animator == null)
                return;

            _Animator.Resume();
        }

        private void PausePlayback()
        {
            if (_Animator == null)
                return;

            _Animator.Pause();
        }

        private void SeekPlayback(ulong seekTime)
        {
            if (_Animator == null)
                return;

            _Animator.Seek(seekTime);
            _AnimatorClearNext = true;
        }
        
        private void mAnimationTimer_Tick(object sender, EventArgs e)
        {
            if (_Animator == null)
                return;

            lock (_Animator.SyncRoot)
            {   //make sure the ticks are serialized

                if (_AnimatorClearNext)
                {
                    _AnimatorClearNext = false; //we don't need to clear again
                    Clear();    //clear the drawing surface (otherwise ink samples will pile up in InkOutputControl's sample buffer!)
                    _Animator.Resume(); //resume the animator
                    return;
                }

                List<CalibratedInkSample> Samples = new List<CalibratedInkSample>();

                if ( (_AnimatorClearNext = _Animator.GetSamples(Samples)) == true )
                    _Animator.Pause();

                foreach (CalibratedInkSample s in Samples)
                    DrawSample(s);

                //notify the tick event
                Invoke(new ReplayTickHandler(OnReplayTick), _Animator.ElapsedMicroseconds);
            }
        }


        private void OnReplayStart(List<CalibratedInkSample> samples)
        {
            if (ReplayStart != null)
                ReplayStart(samples);
        }

        private void OnReplayTick(int elapsedUS)
        {
            if (ReplayTick != null)
                ReplayTick(elapsedUS);
        }

        private void mPlaybackControl_PlaybackPause()
        {
            PausePlayback();
        }

        private void mPlaybackControl_PlaybackResume()
        {
            ResumePlayback();
        }

        private void mPlaybackControl_PlaybackSeek(ulong seekTime)
        {
            SeekPlayback(seekTime);
        }
    }
}

