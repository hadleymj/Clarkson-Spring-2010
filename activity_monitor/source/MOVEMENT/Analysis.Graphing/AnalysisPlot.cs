using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Movement.Analysis;
using Movement.TestEngine.Capture;

namespace Movement.Analysis.Graphing
{
    public partial class AnalysisPlot : Movement.Analysis.Graphing.SamplePlot
    {
        public AnalysisPlot()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Shows an analysis.
        /// </summary>
        /// <param name="metric">The metric to show.</param>
        /// <param name="t">The time data.</param>
        /// <param name="b">The timestamps where there is a gap in the data.</param>
        /// <param name="x">The x-coord data.</param>
        /// <param name="y">The y-coord data.</param>
        /// <param name="p">The pressure data.</param>
        public void ShowAnalysis(
            AnalysisMetric metric,
            List<CalibratedInkSample> samples)
        {
            Clear();

            if (samples.Count == 0) return;

            List<double> t = new List<double>(samples.Count),
                        x = new List<double>(samples.Count),
                        y = new List<double>(samples.Count),
                        p = new List<double>(samples.Count),
                        b = new List<double>();

            foreach (CalibratedInkSample s in samples)
            {
                if (s.IsInvalid())
                {
                    b.Add(s.Time);
                }
                else
                {
                    t.Add(s.Time);
                    x.Add(s.X);
                    y.Add(s.Y);
                    p.Add(s.Pressure);
                }
            }

            switch (metric)
            {
                    //CARTESIAN PLOTS
                case AnalysisMetric.X:
                    ShowAnalysis(metric, t, b, x, 0, StandardUnits.Distance);
                    break;
                case AnalysisMetric.Y:
                    ShowAnalysis(metric, t, b, y, 0, StandardUnits.Distance);
                    break;
                case AnalysisMetric.Pressure:
                    ShowAnalysis(metric, t, b, p, 0, StandardUnits.Pressure);
                    break;
                case AnalysisMetric.VelocityX:
                    ShowAnalysis(metric, t, b, x, 1, StandardUnits.Distance.TimeDerivative(1));
                    break;
                case AnalysisMetric.VelocityY:
                    ShowAnalysis(metric, t, b, y, 1, StandardUnits.Distance.TimeDerivative(1));
                    break;
                case AnalysisMetric.MassFlux:
                    ShowAnalysis(metric, t, b, p, 1, StandardUnits.Pressure.TimeDerivative(1));
                    break;
                case AnalysisMetric.AccelerationX:
                    ShowAnalysis(metric, t, b, x, 2, StandardUnits.Distance.TimeDerivative(2));
                    break;
                case AnalysisMetric.AccelerationY:
                    ShowAnalysis(metric, t, b, y, 2, StandardUnits.Distance.TimeDerivative(2));
                    break;

                    //POLAR PLOTS
                case AnalysisMetric.Rho:
                case AnalysisMetric.Theta:
                case AnalysisMetric.VelocityRho:
                case AnalysisMetric.VelocityTheta:
                case AnalysisMetric.VelocityTangent:
                case AnalysisMetric.AccelerationRho:
                case AnalysisMetric.AccelerationTheta:
                case AnalysisMetric.AccelerationTangent:
                    ShowPolarAnalysis(metric, t, b, x, y, p);
                    break;

                case AnalysisMetric.Time:
                case AnalysisMetric.TimeDelta:
                case AnalysisMetric.Distance:
                    ShowOtherAnalysis(metric, t, b, x, y, p);
                    break;

                case AnalysisMetric.Deviation:
                case AnalysisMetric.InnerDeviation:
                case AnalysisMetric.OuterDeviation:                
                case AnalysisMetric.Invalid:
                    throw new NotSupportedException();
                default:
                    throw new NotImplementedException();
            }
        }

        private void ShowOtherAnalysis(
            AnalysisMetric metric,
            List<double> t,
            List<double> b,
            List<double> x,
            List<double> y,
            List<double> p)
        {
            List<double> Temp;

            switch (metric)
            {
                case AnalysisMetric.Time:
                    ShowAnalysis(metric, t, b, t, 0, StandardUnits.Time);
                    break;
                case AnalysisMetric.TimeDelta:
                    Temp = new List<double>(t.Count);
                    for (int i = 1; i < t.Count; i++)
                        Temp.Add(t[i] - t[i - 1]);
                    Temp.Add(0);

                    ShowAnalysis(metric, t, b, Temp, 0, StandardUnits.Time);
                    break;

                case AnalysisMetric.Distance:
                    Temp = new List<double>(t.Count);
                    Temp.Add(0);   //use the pressure array as a temp
                    for (int i = 1; i < t.Count; i++)
                        Temp.Add(Temp[Temp.Count - 1] + new Analyzer.Vector2(x[i] - x[i - 1], y[i] - y[i - 1]).Norm());

                    ShowAnalysis(metric, t, b, Temp, 0, StandardUnits.Distance);
                    break;
            }
        }

        private void ShowPolarAnalysis(
            AnalysisMetric metric,
            List<double> t,
            List<double> b,
            List<double> x,
            List<double> y,
            List<double> p)
        {
            Analyzer.RunningCount XCounter = new Analyzer.RunningCount(AnalysisMetric.Invalid),
                                  YCounter = new Analyzer.RunningCount(AnalysisMetric.Invalid);

            for (int i = 0; i < t.Count; i++)
            {
                XCounter.Add(x[i]);
                YCounter.Add(y[i]);
            }

            double XMean = XCounter.Mean,
                   YMean = YCounter.Mean;

            List<double> Rho = new List<double>(t.Count),
                         Theta = new List<double>(t.Count),
                         VelTan = new List<double>(t.Count);

            for (int i = 0; i < t.Count; i++)
            {
                double XCentered = x[i] - XMean,
                       YCentered = y[i] - YMean;

                Theta.Add(Math.Atan(YCentered / XCentered));
                Rho.Add(new Analyzer.Vector2(XCentered, YCentered).Norm());
                VelTan.Add(Theta[i] * Rho[i]);
            }

            switch (metric)
            {
                case AnalysisMetric.Rho:
                    ShowAnalysis(metric, t, b, Rho, 0, StandardUnits.Distance);
                    break;
                case AnalysisMetric.Theta:
                    ShowAnalysis(metric, t, b, Theta, 0, StandardUnits.Angle);
                    break;
                case AnalysisMetric.VelocityRho:
                    ShowAnalysis(metric, t, b, Rho, 1, StandardUnits.Distance.TimeDerivative(1));
                    break;
                case AnalysisMetric.VelocityTheta:
                    ShowAnalysis(metric, t, b, Theta, 1, StandardUnits.Angle.TimeDerivative(1));
                    break;
                case AnalysisMetric.VelocityTangent:
                    ShowAnalysis(metric, t, b, VelTan, 0, StandardUnits.Distance.TimeDerivative(1));
                    break;
                case AnalysisMetric.AccelerationRho:
                    ShowAnalysis(metric, t, b, Rho, 2, StandardUnits.Distance.TimeDerivative(2));
                    break;
                case AnalysisMetric.AccelerationTheta:
                    ShowAnalysis(metric, t, b, Theta, 2, StandardUnits.Angle.TimeDerivative(2));
                    break;
                case AnalysisMetric.AccelerationTangent:
                    ShowAnalysis(metric, t, b, VelTan, 1, StandardUnits.Distance.TimeDerivative(2));
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private void ShowAnalysis(
            AnalysisMetric metric,
            List<double> time,
            List<double> breaks,
            List<double> data,
            int derivative,
            Unit dataUnit)
        {
            Title = string.Format("{0} vs {1}", metric, "Time");
            YLabel = string.Format("{0} {1}", metric, dataUnit.Label);
            ShowData(metric.ToString(), Color.Black, time.ToArray(), data.ToArray(), derivative);
            ShowBreaks(Color.Black, breaks.ToArray());

        }
    }
}

