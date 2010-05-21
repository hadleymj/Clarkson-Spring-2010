using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Movement.TestEngine.Capture;

namespace Movement.Analysis.Graphing
{
    public partial class AnalysisDisplayControl : UserControl
    {
        private List<CalibratedInkSample> _LastSamples;

        public AnalysisDisplayControl()
        {
            InitializeComponent();

            foreach (AnalysisMetric m in Enum.GetValues(typeof(AnalysisMetric)))
                if (m == AnalysisMetric.Invalid) continue;
                else mComboBox.Items.Add(m);
        }

        public void ShowAnalysis(AnalysisMetric metric, List<CalibratedInkSample> samples)
        {
            if (samples == null)
                return;
            else if (mComboBox.SelectedItem == null || (AnalysisMetric)mComboBox.SelectedItem != metric)
                mComboBox.SelectedItem = metric;

            _LastSamples = samples;
            mAnalysisPlot.ShowAnalysis(metric, samples);
        }

        private void mComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (mComboBox.SelectedItem != null)
                ShowAnalysis((AnalysisMetric)mComboBox.SelectedItem, _LastSamples);
        }
    }
}
