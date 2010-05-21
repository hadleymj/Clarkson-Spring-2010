using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;

namespace Movement.Analysis
{
	/// <summary>
	/// Encapsulates data analysis structures and functionality.
	/// </summary>
	/// <remarks>Right now we are iterating over data stored in the database.
	/// This data, however, could just as easily be sourced from the test engine
	/// or from a file.</remarks>
	public partial class Analyzer
	{
		private double MeanX, MeanY;

		private int LastTime = 0;

		private RunningCount[] Counters;

		private List<PointF[]> Paths;

		private RunningCount this[AnalysisMetric m] { get { return Counters[(int)m]; } }
		public Analysis CurrentAnalysis { get { return new Analysis(Counters); } }

		public Analyzer(double meanX, double meanY, GraphicsPath path)
		{
			Counters = new RunningCount[Enum.GetValues(typeof(AnalysisMetric)).Length];

			foreach(AnalysisMetric metric in Enum.GetValues(typeof(AnalysisMetric)))
				Counters[(int)metric] = new RunningCount(metric);


			MeanX = meanX;
			MeanY = meanY;


			path.Flatten();

			using(GraphicsPathIterator PathIterator = new GraphicsPathIterator(path))
			{
				using(GraphicsPath Subpath = new GraphicsPath())
				{
					Paths = new List<PointF[]>();

					bool Closed;

					while(PathIterator.NextSubpath(Subpath, out Closed) > 0)
					{
						Paths.Add(Subpath.PathPoints);
						Subpath.Reset();
					}
				}
			}
		}

		/// <summary>
		/// Analyzes a sample and adds the results to the current analysis.
		/// </summary>
		/// <param name="t">The time of the sample.</param>
		/// <param name="x">The x-coord of the sample.</param>
		/// <param name="y">The y-coord of the sample.</param>
		/// <param name="p">The pressure value of the sample.</param>
		public void AnalyzeSample(int t, short x, short y, short p)
		{
            if (t < 0 || p < 0) return;  //don't analyze invalid samples

			int DT = t - LastTime;

			Cartesian(t, DT, x, y, p);
			Polar(t, DT, x, y, p);
			Trace(t, DT, x, y, p);

			LastTime = t;
		}

		/// <summary>
		/// Performs deviation analysis on a test's data.
		/// </summary>
		private void Trace(int t, int dt, short x, short y, short p)
		{
			/*
			 * Assumptions:
			 * TestData origin matches Path origin.
			 * Axis follows standard .NET drawing conventions:
			 *	X increases from left to right
			 *  Y increases from top to bottom
			 */

			double MinDeviation = double.PositiveInfinity;
			Vector2 SamplePoint = new Vector2(x, y),
				MinDistance = new Vector2(double.PositiveInfinity, double.PositiveInfinity);


			foreach(PointF[] subpath in Paths)
			{
				for(int k = 1; k < subpath.Length; k++)
				{
					//compute the distance vector to the current line segment
					Vector2 Distance = PointLineDistance(subpath[k - 1], subpath[k], SamplePoint);
					double DistanceNorm = Distance.Norm();

					if(DistanceNorm < MinDeviation)
					{
						MinDeviation = DistanceNorm;
						MinDistance = Distance;
					}
				}
			}

			

			//we consider the intended point as the point nearest the sample
			this[AnalysisMetric.Deviation].Add(MinDeviation);

			//compute the sign of the cos(theta)
			//here theta is the angle between distance vector and mean-origin vector
			switch(Math.Sign(MinDistance.Dot(-1 * SamplePoint)))
			{
				case -1:	//pi/2 < theta < pi; distance vector points away from origin
					this[AnalysisMetric.InnerDeviation].Add(MinDeviation);
					break;
				case 1:		//0 < theta < pi/2; distance vector points towards origin
					this[AnalysisMetric.OuterDeviation].Add(MinDeviation);
					break;
			}
		}




		/// <summary>
		/// Performs cartesian summary analysis on a test's data.
		/// </summary>
		private void Cartesian(int t, int dt, short x, short y, short p)
		{
			/*
			 * t(i) := time of sample i
			 * 
			 * p(i) := pressure of sample i
			 * j(i) := jerk of sample i (change in pressure)
			 * 
			 * CARTESIAN:
			 * x(i), y(i) := position of sample i
			 * v(i), vx(i), vy(i) := velocity of sample i
			 * ax(i), ay(i) := acceleration of sample i
			 * 
			 * j(i) = ( p(i) - p(i-1) ) / ( t(i) - t(i-1) )
			 * v(i) = ( x(i) - x(i-1) ) / ( t(i) - t(i-1) )
			 * a(i) = ( v(i) - v(i-1) ) / ( t(i) - t(i-1) )
			 * 
			 */

			//0th deriv data
			this[AnalysisMetric.X].Add(x);
			this[AnalysisMetric.Y].Add(y);
			this[AnalysisMetric.Pressure].Add(p);
			this[AnalysisMetric.Time].Add(t);

			this[AnalysisMetric.Distance].Add(new Vector2(this[AnalysisMetric.X].Deriv(1), this[AnalysisMetric.Y].Deriv(1)).Norm());
			this[AnalysisMetric.TimeDelta].Add(dt);

			//1st deriv data (backward-difference derivatives on 0th deriv data)
			this[AnalysisMetric.VelocityX].Add(this[AnalysisMetric.X].Deriv(dt));
			this[AnalysisMetric.VelocityY].Add(this[AnalysisMetric.Y].Deriv(dt));
			this[AnalysisMetric.MassFlux].Add(this[AnalysisMetric.Pressure].Deriv(dt));

			//2nd deriv data (backward-difference derivatives on 1st deriv data)
			this[AnalysisMetric.AccelerationX].Add(this[AnalysisMetric.VelocityX].Deriv(dt));
			this[AnalysisMetric.AccelerationY].Add(this[AnalysisMetric.VelocityY].Deriv(dt));
		}

		/// <summary>
		/// Performs polar summary analysis on a test's data.
		/// </summary>
		/// <param name="test">The test to analyze.</param>
		/// <param name="a">Any analysis done up to this point.  The X and Y analysis must be populated.</param>
		/// <returns>The polar analysis.</returns>
		private void Polar(int t, int dt, short x, short y, short p)
		{
			/*
			 * t(i) := time of sample i
			 * 
			 * p(i) := pressure of sample i
			 * j(i) := jerk of sample i (change in pressure)
			 * 
			 * POLAR:
			 * X := average x
			 * Y := average y
			 * 
			 * theta(i) := polar angle of sample i, mean-centered
			 * rho(i) := polar radius of sample i, mean-centered
			 * tangent(i) := tangential velocity of sample i
			 * 
			 * theta(i) = arctan( (y(i) - Y) / (x(i) - X) )
			 * rho(i) = sqrt( (y(i) - Y)^2 + (x(i) - X)^2 )
			 * tangent(i) = rho * vtheta
			 * 
			 */
			//per Charlie's matlab code, the origin for polar is the meanX, meanY
			double CenteredX = x - MeanX;
			double CenteredY = y - MeanY;

			//0th deriv data
			this[AnalysisMetric.Theta].Add(Math.Atan(CenteredY / CenteredX));
			this[AnalysisMetric.Rho].Add(new Vector2(CenteredX, CenteredY).Norm());

			//1st deriv data (backward-difference derivatives on 0th deriv data)
			this[AnalysisMetric.VelocityTheta].Add(this[AnalysisMetric.Theta].Deriv(dt));
			this[AnalysisMetric.VelocityRho].Add(this[AnalysisMetric.Rho].Deriv(dt));

			//2nd deriv data (backward-difference derivatives on 0th deriv data)
			this[AnalysisMetric.AccelerationTheta].Add(this[AnalysisMetric.VelocityTheta].Deriv(dt));
			this[AnalysisMetric.AccelerationRho].Add(this[AnalysisMetric.VelocityRho].Deriv(dt));

			//tangential data
			this[AnalysisMetric.VelocityTangent].Add(this[AnalysisMetric.Rho].Last * this[AnalysisMetric.VelocityTheta].Last);
			this[AnalysisMetric.AccelerationTangent].Add(this[AnalysisMetric.VelocityTangent].Deriv(dt));
		}



	}
}
