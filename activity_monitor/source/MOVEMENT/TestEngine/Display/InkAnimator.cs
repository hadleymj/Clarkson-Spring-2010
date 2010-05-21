using System;
using System.Collections.Generic;
using System.Text;

using Movement.TestEngine.Capture;

namespace Movement.TestEngine.Display
{
    internal class InkAnimator
    {
        #region Private Attributes

        private object _SyncRoot = new object();
        private HighResTimer _Timer = new HighResTimer();
        private List<CalibratedInkSample> _Samples;
        private int _SampleIndex = 0;

        #endregion

        public object SyncRoot { get { return _SyncRoot; } }

        public int ElapsedMicroseconds
        {
            get
            {
                return (int)_Timer.ElapsedMicroseconds;
            }
        }

        public InkAnimator(List<CalibratedInkSample> samples)
        {
            _Samples = samples;
        }

        public void Play()
        {
            lock (SyncRoot)
            {
                _Timer.Start();
            }
        }

        public void Pause()
        {
            lock (SyncRoot)
            {
                _Timer.Pause();
            }
        }

        public void Resume()
        {
            lock (SyncRoot)
            {
                _Timer.Resume();
            }
        }

        public void Seek(ulong seekTime)
        {
            lock (_SyncRoot)
            {
                _SampleIndex = 0;
                _Timer.SetMicroseconds(seekTime);
            }
        }

        /// <summary>
        /// Gets the next samples that should be drawn at the time of invocation.
        /// </summary>
        /// <param name="toDraw">The samples to draw.</param>
        /// <returns>True if the drawing surface should be cleared first.  Otherwise false.</returns>
        public bool GetSamples(List<CalibratedInkSample> toDraw)
        {
            lock (SyncRoot)
            {
                if (_Timer.Paused) return false;

                int ElapsedUS = _Timer.Now();

                while (_SampleIndex < _Samples.Count
                    && (_Samples[_SampleIndex].IsInvalid() || _Samples[_SampleIndex].Time < ElapsedUS))
                {
                    toDraw.Add(_Samples[_SampleIndex++]);
                }

                if (_SampleIndex == _Samples.Count)
                {   //we have hit the end of the animation
                    _SampleIndex = 0;
                    _Timer.Start();

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
