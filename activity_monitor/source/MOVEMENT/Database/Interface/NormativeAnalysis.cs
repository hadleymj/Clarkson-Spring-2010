using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

using Movement.Analysis;

namespace Movement.Database
{
	/// <summary>
	/// Analysis results expected for a kind of test.
	/// </summary>
	public class NormativeAnalysis : DBObject
	{
		#region Private Attributes
		private Dictionary<AnalysisMetric, List<NormativeAnalysisComponent>> _Components = new Dictionary<AnalysisMetric, List<NormativeAnalysisComponent>>();

		#endregion

		/// <summary>
		/// The list of analysis components available in this instance.
		/// </summary>
		public Dictionary<AnalysisMetric, List<NormativeAnalysisComponent>> Components { get { return _Components; } }

		/// <summary>
		/// Accesses a specific analysis component in this instance.
		/// </summary>
		/// <param name="value">The metric to get the analysis for.</param>
		/// <returns>The analysis result for the given metric.</returns>
		public List<NormativeAnalysisComponent> this[AnalysisMetric value] { get { return Components[value]; } }

		/// <summary>
		/// Constructs a new instance of the NormativeAnalysis class and gets analysis
		/// data from the database for the specified script ID.
		/// </summary>
		/// <param name="testID">The database ID for the script for which to retrieve a normative analysis.</param>
		public NormativeAnalysis(int scriptID)
		{
			using(IDataReader Reader = ExecuteReader(GetConnection(),
				"GET_NORMATIVE_ANALYSIS",
				CommandBehavior.CloseConnection,
				"@script_id", scriptID))
			{
				while(Reader.Read())
				{
					AnalysisMetric Metric = (AnalysisMetric)Reader["metric_id"];
					List<NormativeAnalysisComponent> MetricList;
					
					if(!Components.TryGetValue(Metric, out MetricList))
					{
						MetricList = new List<NormativeAnalysisComponent>();
						Components.Add(Metric, MetricList);
					}

					MetricList.Add(new NormativeAnalysisComponent(
						Metric,
						(TestAnalysisComponentField)Reader["metric_field_id"],
						(double)Reader["min"],
						(double)Reader["max"]));
				}
			}
		}

		/// <summary>
		/// Determines if a test analysis is normal (within normal limits) based on this normative analysis.
		/// </summary>
		/// <param name="analysis">The analysis results to check.</param>
		/// <returns>True if the test analysis is within normal limits, otherwise false.</returns>
		public bool IsNormal(TestAnalysis analysis)
		{
			foreach(TestAnalysisComponent c in analysis.Components.Values)
				if(!IsNormal(c))
					return false;

			//no analysis was abnormal, so the result is normal
			return true;
		}

		/// <summary>
		/// Determines if a test analysis component is normal (within normal limits) based on this normative analysis.
		/// </summary>
		/// <param name="analysisComponent">The analysis component (a single result) to check.</param>
		/// <returns>True if the test analysis component is within normal limits, otherwise false.</returns>
		public bool IsNormal(TestAnalysisComponent analysisComponent)
		{
			List<NormativeAnalysisComponent> NormalAnalysis;
			if(Components.TryGetValue(analysisComponent.Metric, out NormalAnalysis))
			{
				foreach(NormativeAnalysisComponent c in NormalAnalysis)
				{
					double FieldValue;
					switch(c.Field)
					{	//deduce the value we are comparing

						case TestAnalysisComponentField.Count:
							FieldValue = (double)analysisComponent.Count;
							break;

						case TestAnalysisComponentField.Sum:
							FieldValue = analysisComponent.Sum;
							break;

						case TestAnalysisComponentField.Min:
							FieldValue = analysisComponent.Min;
							break;

						case TestAnalysisComponentField.Max:
							FieldValue = analysisComponent.Max;
							break;

						case TestAnalysisComponentField.StdDev:
							FieldValue = analysisComponent.StdDev;
							break;

						case TestAnalysisComponentField.Mean:
						default:
							FieldValue = analysisComponent.Mean;
							break;
					}


					//compare the field value with the normal analysis
					if(FieldValue < c.Min || FieldValue > c.Max)
						return false;
				}
			}

			//no analysis was abnormal, so the result is normal
			return true;
		}
	}

	/// <summary>
	/// Part of a normative analysis.
	/// </summary>
	public struct NormativeAnalysisComponent
	{
		/// <summary>
		/// The test analysis component that this checks.
		/// </summary>
		public AnalysisMetric Metric;

		/// <summary>
		/// The field of the test analysis component that this checks.
		/// </summary>
		public TestAnalysisComponentField Field;

		/// <summary>
		/// The minimum value expected.
		/// </summary>
		public double Min;

		/// <summary>
		/// The maximum value expected.
		/// </summary>
		public double Max;

		public NormativeAnalysisComponent(AnalysisMetric metric, TestAnalysisComponentField field, double min, double max)
		{
			Metric = metric;
			Field = field;
			Min = min;
			Max = max;
		}
	}
}
