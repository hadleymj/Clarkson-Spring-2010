namespace Movement.TestEngine.Testing
{
    partial class TestBatchCaptureControl
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mLabelStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mLabelStatus
            // 
            this.mLabelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.mLabelStatus.AutoSize = true;
            this.mLabelStatus.ForeColor = System.Drawing.SystemColors.HotTrack;
            this.mLabelStatus.Location = new System.Drawing.Point(1, 126);
            this.mLabelStatus.Name = "mLabelStatus";
            this.mLabelStatus.Size = new System.Drawing.Size(16, 13);
            this.mLabelStatus.TabIndex = 1;
            this.mLabelStatus.Text = "...";
            // 
            // TestBatchCaptureControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.mLabelStatus);
            this.Name = "TestBatchCaptureControl";
            this.PostTestComplete += new Movement.TestEngine.Testing.PostTestCompleteHandler(this.TestBatchCaptureControl_PostTestComplete);
            this.TestComplete += new Movement.TestEngine.Testing.TestCompleteHandler(this.TestBatchCaptureControl_TestComplete);
            this.Controls.SetChildIndex(this.mLabelStatus, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mLabelStatus;
    }
}
