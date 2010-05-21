namespace Movement.TestEngine.Display
{
    partial class PlaybackControl
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
            this.ButtonPlayPause = new System.Windows.Forms.Button();
            this.PanelProgress = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // ButtonPlayPause
            // 
            this.ButtonPlayPause.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.ButtonPlayPause.Location = new System.Drawing.Point(3, 0);
            this.ButtonPlayPause.Name = "ButtonPlayPause";
            this.ButtonPlayPause.Size = new System.Drawing.Size(75, 23);
            this.ButtonPlayPause.TabIndex = 0;
            this.ButtonPlayPause.Text = "Pause";
            this.ButtonPlayPause.UseVisualStyleBackColor = true;
            this.ButtonPlayPause.Click += new System.EventHandler(this.ButtonPlayPause_Click);
            // 
            // PanelProgress
            // 
            this.PanelProgress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.PanelProgress.BackColor = System.Drawing.SystemColors.Window;
            this.PanelProgress.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PanelProgress.Cursor = System.Windows.Forms.Cursors.PanEast;
            this.PanelProgress.Location = new System.Drawing.Point(84, 3);
            this.PanelProgress.Name = "PanelProgress";
            this.PanelProgress.Size = new System.Drawing.Size(313, 19);
            this.PanelProgress.TabIndex = 1;
            this.PanelProgress.Resize += new System.EventHandler(this.PanelProgress_Resize);
            this.PanelProgress.Paint += new System.Windows.Forms.PaintEventHandler(this.PanelProgress_Paint);
            this.PanelProgress.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelProgress_MouseUp);
            // 
            // PlaybackControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PanelProgress);
            this.Controls.Add(this.ButtonPlayPause);
            this.Name = "PlaybackControl";
            this.Size = new System.Drawing.Size(400, 25);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button ButtonPlayPause;
        private System.Windows.Forms.Panel PanelProgress;
    }
}
