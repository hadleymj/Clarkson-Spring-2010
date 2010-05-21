using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Movement.Analysis
{
	/// <summary>
	/// An enumeration of each supported analysis metric.
	/// </summary>
	public enum AnalysisMetric
	{
		Invalid = 0,
		X,
		Y,
		Pressure,
		VelocityX,
		VelocityY,
		MassFlux,
		AccelerationX,
		AccelerationY,
		Theta,
		Rho,
		VelocityTheta,
		VelocityRho,
		AccelerationTheta,
		AccelerationRho,
		VelocityTangent,
		AccelerationTangent,
		Deviation,
		InnerDeviation,
		OuterDeviation,
		Distance,
		Time,
		TimeDelta,
	};

    /// <summary>
    /// The units used for each metric dimension.
    /// </summary>
    public struct StandardUnits
    {
        public static Unit Time = new Unit(-6, "s");
        public static Unit Distance = new Unit(-2, "mm");
        public static Unit Pressure = new Unit(0, "raw");
        public static Unit Angle = new Unit(0, "rad");
    }

    /// <summary>
    /// A unit.
    /// </summary>
    public struct Unit
    {
        public int Scale;
        public string Metric;
        public string Label
        {
            get
            {
                if (Scale == 0)
                    return Metric;
                else
                    return string.Format("10^{0} {1}", Scale, Metric);
            }
        }

        public Unit(int scale, string metric)
        {
            Scale = scale;
            Metric = metric;
        }

        public Unit TimeDerivative(int d)
        {
            return new Unit(Scale, string.Format("{0}/{1}^{2}", Metric, StandardUnits.Time.Metric, d+1));
        }
    }

	/// <summary>
	/// An analysis component, which contains summary data specific to a component of a test.
	/// </summary>
	public struct AnalysisComponent
	{
		public static readonly AnalysisComponent INVALID = new AnalysisComponent();

		public AnalysisMetric Metric;

		public int Count;
		public double Sum;
		public double Mean;
		public double StdDev;
		public double Min;
		public double Max;

		internal AnalysisComponent(Analyzer.RunningCount r)
		{
			Metric = r.Metric;

			Count = r.Count;
			Sum = r.Sum;

			Mean = r.Mean;
			StdDev = r.StdDev;
			Min = r.Min;
			Max = r.Max;

			if(!IsValid(Count))
				Count = 0;
			if(!IsValid(Sum))
				Sum = 0;

			if(!IsValid(Mean))
				Mean = 0;
			if(!IsValid(StdDev))
				StdDev = 0;
			
			if(!IsValid(Min))
				Min = 0;
			if(!IsValid(Max))
				Max = 0;
		}

		private static bool IsValid(double r)
		{
			return !double.IsInfinity(r) && !double.IsNaN(r);
		}

		public override string ToString()
		{
			return string.Format("{1:0.00}\t{2:0.00}\t{3:0.00}\t{4:0.00}\t\t{0}", Metric.ToString(), Mean, StdDev, Min, Max);
		}
	}

	/// <summary>
	/// A collection of analysis components.
	/// </summary>
	public class Analysis : IEnumerable<AnalysisComponent>
	{
		private static readonly int INITIAL_SIZE = Enum.GetValues(typeof(AnalysisMetric)).Length;

		private Dictionary<AnalysisMetric, AnalysisComponent> Metrics = new Dictionary<AnalysisMetric, AnalysisComponent>(INITIAL_SIZE);

		public AnalysisComponent this[AnalysisMetric m]
		{
			get
			{
				AnalysisComponent c;
				return Metrics.TryGetValue(m, out c) ? c : AnalysisComponent.INVALID;
			}
		}

		public Analysis()
		{
		}

		internal Analysis(Analyzer.RunningCount[] metrics)
		{
			foreach(Analyzer.RunningCount m in metrics)
				if(m.Metric != AnalysisMetric.Invalid)
					Metrics.Add(m.Metric, new AnalysisComponent(m));
		}

		/// <summary>
		/// Adds / updates this analysis result instance with a second set of analysis results.
		/// </summary>
		/// <param name="a">The analysis results to merge into this instance.</param>
		public void Union(Analysis a)
		{
			foreach(KeyValuePair<AnalysisMetric, AnalysisComponent> x in a.Metrics)
				Metrics[x.Key] = x.Value;
		}

		public override string ToString()
		{
			StringBuilder b = new StringBuilder();
			b.AppendLine("Analysis Results");
			b.AppendLine("Mean\tStdDev\tMin\tMax\t\tMetric");

			foreach(KeyValuePair<AnalysisMetric, AnalysisComponent> v in Metrics)
			{
				b.Append(v.Value.ToString());
				b.AppendLine();
			}

			return b.ToString();
		}

		#region IEnumerable<AnalysisComponent> Members

		public IEnumerator<AnalysisComponent> GetEnumerator()
		{
			return Metrics.Values.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return Metrics.Values.GetEnumerator();
		}

		#endregion
	}
}
