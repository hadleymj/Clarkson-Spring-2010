using System;
using System.Collections.Generic;
using System.Text;

namespace Movement.TestEngine.Capture
{
	/// <summary>
	/// A single raw sample of ink data.
	/// </summary>
	public struct RawInkSample
	{
		//public static readonly RawInkSample INVALID = new RawInkSample(-1, -1, -1, -1);

		/// <summary>
		/// The timestamp of the sample in microseconds.
		/// </summary>
		public int Time;

		/// <summary>
		/// The x-coord of the sample in HIMETRIC.
		/// </summary>
		public int X;

		/// <summary>
		/// The y-coord of the sample in HIMETRIC.
		/// </summary>
		public int Y;

		/// <summary>
		/// The un-normalized pressure value of the sample.
		/// </summary>
		public int Pressure;

        public RawInkSample(int t)
            : this(t, 0, 0, -1)
        {
        }

		public RawInkSample(int t, int x, int y, int p)
		{
			Time = t;
			X = x;
			Y = y;
			Pressure = p;
		}

        public bool IsInvalid() { return Pressure < 0; }

		public override bool Equals(object obj)
		{
            RawInkSample raw = (RawInkSample)obj;

			return raw.Time == Time
				&& raw.X == X
				&& raw.Y == Y
				&& raw.Pressure == Pressure;
		}

		public override int GetHashCode()
		{
			return Time.GetHashCode() ^ X ^ Y ^ Pressure;
		}

		public override string ToString()
		{
            if (IsInvalid())
                return "sR(INVALID)";
            else
			    return string.Format(@"sR({0}, {1}, {2}, {3})", Time, X, Y, Pressure);
		}
	}
}
