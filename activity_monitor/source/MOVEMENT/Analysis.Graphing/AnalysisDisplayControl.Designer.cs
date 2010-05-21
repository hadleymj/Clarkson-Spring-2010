namespace Movement.Analysis.Graphing
{
    partial class AnalysisDisplayControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mComboBox = new System.Windows.Forms.ComboBox();
            this.mAnalysisPlot = new Movement.Analysis.Graphing.AnalysisPlot();
            this.SuspendLayout();
            // 
            // mComboBox
            // 
            this.mComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mComboBox.FormattingEnabled = true;
            this.mComboBox.Location = new System.Drawing.Point(3, 257);
            this.mComboBox.Name = "mComboBox";
            this.mComboBox.Size = new System.Drawing.Size(214, 21);
            this.mComboBox.TabIndex = 1;
            this.mComboBox.SelectedValueChanged += new System.EventHandler(this.mComboBox_SelectedValueChanged);
            // 
            // mAnalysisPlot
            // 
            this.mAnalysisPlot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mAnalysisPlot.Location = new System.Drawing.Point(0, 0);
            this.mAnalysisPlot.Name = "mAnalysisPlot";
            this.mAnalysisPlot.Size = new System.Drawing.Size(399, 281);
            this.mAnalysisPlot.TabIndex = 0;
            this.mAnalysisPlot.Title = "Title";
            this.mAnalysisPlot.XLabel = "Time us";
            this.mAnalysisPlot.YLabel = "Y Axis";
            // 
            // AnalysisDisplayControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mComboBox);
            this.Controls.Add(this.mAnalysisPlot);
            this.Name = "AnalysisDisplayControl";
            this.Size = new System.Drawing.Size(399, 281);
            this.ResumeLayout(false);

        }

        #endregion

        private AnalysisPlot mAnalysisPlot;
        private System.Windows.Forms.ComboBox mComboBox;
    }
}
