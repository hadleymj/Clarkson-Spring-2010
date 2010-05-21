using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Movement.Analysis
{
	public partial class Analyzer
	{
		/// <summary>
		/// Computes the minimum distance vector from a point to a line segment.
		/// </summary>
		/// <param name="lStart">The start of the line segment.</param>
		/// <param name="lEnd">The end of the line segment.</param>
		/// <param name="p">The point.</param>
		/// <returns>The distance vector for the shortest distance to the line segment.</returns>
		private static Vector2 PointLineDistance(PointF lStart, PointF lEnd, Vector2 p)
		{
			Vector2 B = new Vector2(lStart.X, lStart.Y),
				M = new Vector2(lEnd.X - lStart.X, lEnd.Y - lStart.Y);

			//line segment = B + tM, t in [0, 1]

			double t = M.Dot(p - B) / M.Dot(M);
			t = Math.Max(0, t);	//clamp t between 0 and 1
			t = Math.Min(1, t);

			return (p - (B + t * M));
		}

		/// <summary>
		/// Basic 2D vector implementation.
		/// </summary>
		public struct Vector2
		{
			public double x, y;

			public Vector2(double x, double y)
			{
				this.x = x;
				this.y = y;
			}

			public static Vector2 operator +(Vector2 a, Vector2 b)
			{
				return new Vector2(a.x + b.x, a.y + b.y);
			}

			public static Vector2 operator -(Vector2 a, Vector2 b)
			{
				return new Vector2(a.x - b.x, a.y - b.y);
			}

			public static Vector2 operator *(Vector2 a, double x)
			{
				return new Vector2(a.x * x, a.y * x);
			}

			public static Vector2 operator *(double x, Vector2 a)
			{
				return new Vector2(a.x * x, a.y * x);
			}

			public double Dot(Vector2 b)
			{
				return x * b.x + y * b.y;
			}

			public double Norm()
			{
				return Math.Sqrt(x * x + y * y);
			}
		}


		/// <summary>
		/// Manages statistics as we iterate through a sequence of samples using partial sums.
		/// </summary>
		public class RunningCount
		{
			public AnalysisMetric Metric;

			public int Count;

			public double Min = double.PositiveInfinity, Max = double.NegativeInfinity;
			public double Sum;
			public double SumAbs;
			public double SumSquares;
			public double Last;
			public double SecondLast;

			/// <summary>
			/// The average of all the samples collected.
			/// </summary>
			public double Mean
			{
				get
				{
					//TODO: should this use the signed or absolute sum?
					return (Count == 0) ? 0 : (Sum / Count);
				}
			}

			/// <summary>
			/// The standard deviation of all the samples collected.
			/// </summary>
			public double StdDev
			{
				get
				{
					//TODO: should this use the signed or absolute sum?
					return (Count < 2) ? 0 : Math.Sqrt(Math.Abs(Count * SumSquares - (SumAbs * SumAbs)) / (Count * (Count - 1)));
				}
			}

			/// <summary>
			/// The 2-norm of the samples collected.
			/// </summary>
			public double Norm
			{
				get
				{
					return Math.Sqrt(SumSquares);
				}
			}

			/// <summary>
			/// The current numerical derivative of the last two samples.
			/// </summary>
			/// <param name="dt">The time difference between the last two samples.</param>
			/// <returns>A numerical approximation to the derivative, or double.NaN if less than two samples have been added.</returns>
			public double Deriv(double dt)
			{
				return (Count < 2) ? double.NaN : (Last - SecondLast) / dt;
			}

			public RunningCount(AnalysisMetric m)
			{
				Metric = m;
				Clear();
			}

			/// <summary>
			/// Adds a sample to the running count.
			/// </summary>
			/// <param name="sample">The sampe to add.</param>
			public void Add(double sample)
			{
				if(Double.IsNaN(sample))
					return;

				Count++;
				Min = Math.Min(Min, sample);
				Max = Math.Max(Max, sample);

				Sum += sample;
				SumAbs += Math.Abs(sample);
				SumSquares += sample * sample;

				SecondLast = Last;
				Last = sample;
			}

			/// <summary>
			/// Resets the running counters.
			/// </summary>
			public void Clear()
			{
				Count = 0;
				Sum = 0;
				SumAbs = 0;
				SumSquares = 0;
				Last = 0;
				SecondLast = 0;
			}
		}
	}
}
