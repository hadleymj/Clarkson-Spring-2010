using System;
using System.Runtime.InteropServices;

namespace Movement.TestEngine.Capture
{
	internal class HighResTimer
	{
		// Construction

		public HighResTimer()
		{
#if (!NOTIMER)
			StartTicks = StopTicks = 0UL;
			QueryPerformanceFrequency(out TickFrequency);
#endif
		}

		// Properties

        public bool Paused
        {
            get
            {
                return _Paused;
            }
        }

		public ulong ElapsedTicks
		{
#if (!NOTIMER)
			get
			{
                return (StopTicks - StartTicks); 
            }
#else
            get
            {
                return 0UL;
            }
#endif
		}

		public ulong ElapsedMicroseconds
		{
#if (!NOTIMER)
			get
			{
                ulong d = ElapsedTicks;
				return (d < 0x10c6f7a0b5edUL) ? (d * 1000000UL) / TickFrequency : (d / TickFrequency) * 1000000UL;
                            // 2^64 / 1e6
			}
#else
            get
            {
                return 0UL;
            }
#endif
		}

		public TimeSpan ElapsedTimeSpan
		{
#if (!NOTIMER)
			get
			{
				ulong t = 10UL * ElapsedMicroseconds;
                return ((t & 0x8000000000000000UL) == 0UL) ? new TimeSpan((long)t) : TimeSpan.MaxValue;
			}
#else
            get
            {
                return TimeSpan.Zero;
            }
#endif
		}

        public ulong Frequency
        {
#if (!NOTIMER)
            get
            {
                return TickFrequency;
            }
#else
            get
            {
                return 1UL;
            }
#endif
        }

		// Methods

        /// <summary>
        /// Starts the timer.
        /// </summary>
		public void Start()
		{
#if (!NOTIMER)
			QueryPerformanceCounter(out StartTicks);
            StopTicks = StartTicks;
#endif
		}

        /// <summary>
        /// Updates the timer.
        /// </summary>
        /// <returns>The elapsed microseconds since start, accounting for pauses.</returns>
		public int Now()
		{
#if (!NOTIMER)
			QueryPerformanceCounter(out StopTicks);
            return (int)ElapsedMicroseconds;
#else
            return 0UL;
#endif
		}

        /// <summary>
        /// Pauses the timer.
        /// </summary>
        /// <remarks>If resume is never called, pausing has no effect other than updating the timer.</remarks>
        public void Pause()
        {
            if (_Paused)
                return;

            _Paused = true;
            Now();
        }

        /// <summary>
        /// Resumes the timer.
        /// </summary>
        public void Resume()
        {
            if (!_Paused)
                return;

            ulong d = StopTicks;    //get the ticks at pause time
            Now(); //update the ticks now

            d = StopTicks - d;  //find the difference between pause time and now
            StartTicks += d;    //advance start ticks by as much
            _Paused = false;
        }

        public void SetMicroseconds(ulong elapsed)
        {
            //elapsed = 1000000UL * (d / TickFrequency)
            // ==> d = TickFrequency * elapsed / 1000000UL
            Now();
            StartTicks = StopTicks - (TickFrequency * elapsed / 1000000UL);
        }

		// Implementation

#if (!NOTIMER)
		[System.Security.SuppressUnmanagedCodeSecurity()]
		[DllImport("kernel32.dll")]
		protected static extern int QueryPerformanceFrequency(out ulong x);

		[System.Security.SuppressUnmanagedCodeSecurity()]
		[DllImport("kernel32.dll")]
		protected static extern int QueryPerformanceCounter(out ulong x);

        private bool _Paused = false;
		protected ulong StartTicks, StopTicks, TickFrequency;
#endif
	}
}

