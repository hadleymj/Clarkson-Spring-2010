using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

using Movement.Analysis;

namespace Movement.Database
{
	/// <summary>
	/// Analysis results computed for a single test.
	/// </summary>
	public class TestAnalysis : DBObject
	{
		#region Private Attributes
		private Dictionary<AnalysisMetric, TestAnalysisComponent> _Components = new Dictionary<AnalysisMetric, TestAnalysisComponent>();
		
		#endregion
		
		/// <summary>
		/// The list of analysis components available in this instance.
		/// </summary>
		public Dictionary<AnalysisMetric, TestAnalysisComponent> Components { get { return _Components; } }

		/// <summary>
		/// Accesses a specific analysis component in this instance.
		/// </summary>
		/// <param name="value">The metric to get the analysis for.</param>
		/// <returns>The analysis result for the given metric.</returns>
		public TestAnalysisComponent this[AnalysisMetric value] { get { return Components[value]; } }

		/// <summary>
		/// Constructs a new instance of the TestAnalysis class and gets analysis
		/// data from the database for the specified test ID.
		/// </summary>
		/// <param name="testID">The database ID for the test for which to retrieve an analysis.</param>
		public TestAnalysis(int testID)
		{
			using(IDataReader Reader = ExecuteReader(GetConnection(),
				"GET_TEST_ANALYSIS",
				CommandBehavior.CloseConnection,
				"@test_id", testID))
			{
				while(Reader.Read())
				{
					Components.Add(
						(AnalysisMetric)Reader["metric_id"],
						new TestAnalysisComponent(
							(AnalysisMetric)Reader["metric_id"],
							(int)Reader["count"],
							(double)Reader["sum"],
							(double)Reader["mean"],
							(double)Reader["stddev"],
							(double)Reader["min"],
							(double)Reader["max"]));
				}
			}
		}

		internal static void StoreAnalysis(
			DbConnection Connection,
			DbTransaction Transaction,
			int TestID,
			Movement.Analysis.Analysis Results)
		{
			foreach(AnalysisComponent c in Results)
			{
				Execute(Connection, Transaction,
					"ADD_TEST_ANALYSIS",
					"@test_id", TestID,
					"@metric_id", (int)c.Metric,
					"@count", c.Count,
					"@sum", c.Sum,
					"@mean", c.Mean,
					"@stddev", c.StdDev,
					"@min", c.Min,
					"@max", c.Max);
			}
		}
	}

	/// <summary>
	/// Identifies an attribute of the analysis component struct.
	/// </summary>
	/// <remarks>This enumeration must be synchronized with the metric_field table in the database.</remarks>
	public enum TestAnalysisComponentField
	{
		/// <summary>
		/// The average value.
		/// </summary>
		Mean = 0,

		/// <summary>
		/// The standard deviation value.
		/// </summary>
		StdDev = 1,

		/// <summary>
		/// The minimum value.
		/// </summary>
		Min = 2,

		/// <summary>
		/// The maximum value.
		/// </summary>
		Max = 3,

		/// <summary>
		/// The sum of all the values.
		/// </summary>
		Sum = 4,

		/// <summary>
		/// The number of values.
		/// </summary>
		Count = 5
	};

	/// <summary>
	/// Part of a test analysis.
	/// </summary>
	public struct TestAnalysisComponent
	{
		/// <summary>
		/// The part of the analysis represented.
		/// </summary>
		public AnalysisMetric Metric;

		/// <summary>
		/// The number of values.
		/// </summary>
		public int Count;

		/// <summary>
		/// The sum of the values.
		/// </summary>
		public double Sum;

		/// <summary>
		/// The average value.
		/// </summary>
		public double Mean;

		/// <summary>
		/// The standard deviation value.
		/// </summary>
		public double StdDev;

		/// <summary>
		/// The minimum value.
		/// </summary>
		public double Min;

		/// <summary>
		/// The maximum value.
		/// </summary>
		public double Max;

		public TestAnalysisComponent(AnalysisMetric metric, int count, double sum, double mean, double stddev, double min, double max)
		{
			Metric = metric;
			Count = count;
			Sum = sum;
			Mean = mean;
			StdDev = stddev;
			Min = min;
			Max = max;
		}
	}
}
