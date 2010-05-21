using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace Movement.Analysis.Graphing
{
    public partial class SamplePlot : UserControl
    {
        /// <summary>
        /// The title of the plot.
        /// </summary>
        public string Title
        {
            get { return mZedGraphControl.GraphPane.Title.Text; }
            set { mZedGraphControl.GraphPane.Title.Text = value; }
        }

        /// <summary>
        /// The X-Axis label of the plot.
        /// </summary>
        public string XLabel
        {
            get { return mZedGraphControl.GraphPane.XAxis.Title.Text; }
            set { mZedGraphControl.GraphPane.XAxis.Title.Text = value; }
        }

        /// <summary>
        /// The Y-Axis label of the plot.
        /// </summary>
        public string YLabel
        {
            get { return mZedGraphControl.GraphPane.YAxis.Title.Text; }
            set { mZedGraphControl.GraphPane.YAxis.Title.Text = value; }
        }
        
        public SamplePlot()
        {
            InitializeComponent();

            XLabel = string.Format("{0} {1}", "Time", StandardUnits.Time.Label);
            mZedGraphControl.GraphPane.IsPenWidthScaled = false;
        }

        public void ShowData(string label, Color c, double[] t, double[] s)
        {
            Analyzer.RunningCount Counter = new Analyzer.RunningCount(AnalysisMetric.Invalid);
            for (int i = 0; i < s.Length; i++)
                Counter.Add(s[i]);

            mZedGraphControl.GraphPane.AddCurve(label, t, s, c, SymbolType.None);

            using (Graphics g = mZedGraphControl.CreateGraphics())
            {   //scale the XY axis appropriately
                mZedGraphControl.GraphPane.XAxis.Scale.PickScale(mZedGraphControl.GraphPane, g, mZedGraphControl.GraphPane.CalcScaleFactor());
                mZedGraphControl.GraphPane.YAxis.Scale.PickScale(mZedGraphControl.GraphPane, g, mZedGraphControl.GraphPane.CalcScaleFactor());
            }
            
            //refresh the scaling so we can use it later...
            mZedGraphControl.AxisChange();
            mZedGraphControl.Refresh();

            //draw a box for the first standard deviation
            BoxObj StdBox = new BoxObj(
                mZedGraphControl.GraphPane.XAxis.Scale.Min,
                Counter.Mean + Counter.StdDev,
                mZedGraphControl.GraphPane.XAxis.Scale.Max - mZedGraphControl.GraphPane.XAxis.Scale.Min,
                2*Counter.StdDev,
                Color.Empty,
                Color.FromArgb(150, Color.LightGreen));
            StdBox.IsClippedToChartRect = true;
            StdBox.ZOrder = ZOrder.E_BehindCurves;

            mZedGraphControl.GraphPane.GraphObjList.Add(StdBox);    //add the box

            //draw a box for the second standard deviation
            StdBox = new BoxObj(
                mZedGraphControl.GraphPane.XAxis.Scale.Min,
                Counter.Mean + 2*Counter.StdDev,
                mZedGraphControl.GraphPane.XAxis.Scale.Max - mZedGraphControl.GraphPane.XAxis.Scale.Min,
                Counter.StdDev,
                Color.Empty,
                Color.FromArgb(150, Color.LightBlue));
            StdBox.IsClippedToChartRect = true;
            StdBox.ZOrder = ZOrder.E_BehindCurves;

            mZedGraphControl.GraphPane.GraphObjList.Add(StdBox);    //add the box

            //draw a box for the second standard deviation
            StdBox = new BoxObj(
                mZedGraphControl.GraphPane.XAxis.Scale.Min,
                Counter.Mean - Counter.StdDev,
                mZedGraphControl.GraphPane.XAxis.Scale.Max - mZedGraphControl.GraphPane.XAxis.Scale.Min,
                Counter.StdDev,
                Color.Empty,
                Color.FromArgb(150, Color.LightBlue));
            StdBox.IsClippedToChartRect = true;
            StdBox.ZOrder = ZOrder.E_BehindCurves;

            mZedGraphControl.GraphPane.GraphObjList.Add(StdBox);    //add the box

            //draw a line for the mean value
            LineObj MeanLine = new LineObj(
                mZedGraphControl.GraphPane.XAxis.Scale.Min,
                Counter.Mean,
                mZedGraphControl.GraphPane.XAxis.Scale.Max,
                Counter.Mean);
            MeanLine.Line.Style = System.Drawing.Drawing2D.DashStyle.Dash;
            MeanLine.IsClippedToChartRect = true;

            mZedGraphControl.GraphPane.GraphObjList.Add(MeanLine);  //add the line

            //draw a label for the mean value
            TextObj MeanLabel = new TextObj(
                "Mean",
                mZedGraphControl.GraphPane.XAxis.Scale.Min,
                Counter.Mean,
                CoordType.AxisXYScale,
                AlignH.Left,
                AlignV.Bottom);
            MeanLabel.FontSpec.Fill.IsVisible = false;
            MeanLabel.FontSpec.Border.IsVisible = false;
            MeanLabel.FontSpec.IsBold = true;
            MeanLabel.FontSpec.IsItalic = true;

            mZedGraphControl.GraphPane.GraphObjList.Add(MeanLabel); //add the label


            mZedGraphControl.AxisChange();
            mZedGraphControl.Refresh();

        }

        public void ShowData(string label, Color c, double[] t, double[] s, int deriv)
        {
            if (deriv < 1) ShowData(label, c, t, s);
            else
            {
                for (int i = 1; i < t.Length && i < s.Length; i++)
                {
                    if (t[i] == t[i - 1])
                        s[i - 1] = double.NaN;
                    else
                        s[i - 1] = (s[i] - s[i - 1]) / (t[i] - t[i - 1]); //compute derivative
                }

                s[s.Length - 1] = double.NaN;

                ShowData(label, c, t, s, deriv - 1);
            }
        }

        public void ShowBreaks(Color c, double[] b)
        {
            for (int i = 0; i < b.Length; i++)
            {
                LineObj BreakLine = new LineObj(
                    b[i],
                    mZedGraphControl.GraphPane.YAxis.Scale.Min,
                    b[i],
                    mZedGraphControl.GraphPane.YAxis.Scale.Max);
                BreakLine.Line.Style = System.Drawing.Drawing2D.DashStyle.Dot;
                BreakLine.ZOrder = ZOrder.A_InFront;

                mZedGraphControl.GraphPane.GraphObjList.Add(BreakLine);
            }

            mZedGraphControl.AxisChange();
            mZedGraphControl.Refresh();
        }

        public void Clear()
        {
            mZedGraphControl.GraphPane.CurveList.Clear();
            mZedGraphControl.GraphPane.GraphObjList.Clear();

            mZedGraphControl.AxisChange();
            mZedGraphControl.Refresh();
        }
    }
}
