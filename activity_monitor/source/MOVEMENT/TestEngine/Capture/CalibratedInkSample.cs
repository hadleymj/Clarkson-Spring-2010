using System;
using System.Collections.Generic;
using System.Text;

namespace Movement.TestEngine.Capture
{
	/// <summary>
	/// A single calibrated sample of ink data.
	/// </summary>
	public struct CalibratedInkSample
	{
		//public static readonly CalibratedInkSample INVALID = new CalibratedInkSample(-1, -1, -1, -1);

		/// <summary>
		/// The timestamp of the sample in microseconds.
		/// </summary>
		public int Time;

		/// <summary>
		/// The normalized x-coord of the sample in HIMETRIC.
		/// </summary>
		public int X;

		/// <summary>
		/// The normalized y-coord of the sample in HIMETRIC.
		/// </summary>
		public int Y;

		/// <summary>
		/// The normalized pressure value of the sample.
		/// </summary>
		public int Pressure;

        public CalibratedInkSample(int t)
            : this(t, 0, 0, -1)
        {
        }

        public CalibratedInkSample(int t, int x, int y, int p)
		{
			Time = t;
			X = x;
			Y = y;
			Pressure = p;
		}

        public bool IsInvalid() { return Pressure < 0; }

		public override bool Equals(object obj)
		{
            CalibratedInkSample cal = (CalibratedInkSample)obj;

			return cal.Time == Time
                && cal.X == X
                && cal.Y == Y
                && cal.Pressure == Pressure;
		}

		public override int GetHashCode()
		{
			return Time.GetHashCode() ^ X ^ Y ^ Pressure;
		}

		public override string ToString()
		{
            if (IsInvalid())
                return "sC(INVALID)";
            else
			    return string.Format(@"sC({0}, {1}, {2}, {3})", Time, X, Y, Pressure);
		}
	}
}
