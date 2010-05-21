namespace Movement.TestEngine.Testing
{
    partial class TestCaptureControl
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
            this.components = new System.ComponentModel.Container();
            this.mLabelInstructions = new System.Windows.Forms.Label();
            this.mTestTimer = new System.Windows.Forms.Timer(this.components);
            this.mLabelTimer = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mLabelInstructions
            // 
            this.mLabelInstructions.Dock = System.Windows.Forms.DockStyle.Top;
            this.mLabelInstructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mLabelInstructions.Location = new System.Drawing.Point(0, 0);
            this.mLabelInstructions.Name = "mLabelInstructions";
            this.mLabelInstructions.Size = new System.Drawing.Size(142, 20);
            this.mLabelInstructions.TabIndex = 0;
            this.mLabelInstructions.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // mTestTimer
            // 
            this.mTestTimer.Enabled = true;
            this.mTestTimer.Tick += new System.EventHandler(this.mTestTimer_Tick);
            // 
            // mLabelTimer
            // 
            this.mLabelTimer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.mLabelTimer.AutoSize = true;
            this.mLabelTimer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mLabelTimer.Location = new System.Drawing.Point(103, 126);
            this.mLabelTimer.Name = "mLabelTimer";
            this.mLabelTimer.Size = new System.Drawing.Size(36, 16);
            this.mLabelTimer.TabIndex = 3;
            this.mLabelTimer.Text = "0.00";
            // 
            // TestCaptureControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.mLabelTimer);
            this.Controls.Add(this.mLabelInstructions);
            this.Name = "TestCaptureControl";
            this.NewInkSample += new Movement.TestEngine.Capture.NewInkSampleHandler(this.TestCaptureControl_NewInkSample);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label mLabelInstructions;
        protected System.Windows.Forms.Timer mTestTimer;
        private System.Windows.Forms.Label mLabelTimer;
    }
}
