namespace Movement.TestEngine.Display
{
    partial class ReplayInkOutputControl
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
            this.mAnimationTimer = new System.Windows.Forms.Timer(this.components);
            this.mPlaybackControl = new Movement.TestEngine.Display.PlaybackControl();
            this.SuspendLayout();
            // 
            // mAnimationTimer
            // 
            this.mAnimationTimer.Enabled = true;
            this.mAnimationTimer.Interval = 30;
            this.mAnimationTimer.Tick += new System.EventHandler(this.mAnimationTimer_Tick);
            // 
            // mPlaybackControl
            // 
            this.mPlaybackControl.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.mPlaybackControl.Location = new System.Drawing.Point(0, 117);
            this.mPlaybackControl.Name = "mPlaybackControl";
            this.mPlaybackControl.Size = new System.Drawing.Size(142, 25);
            this.mPlaybackControl.TabIndex = 0;
            this.mPlaybackControl.PlaybackPause += new Movement.TestEngine.Display.PlaybackPauseHandler(this.mPlaybackControl_PlaybackPause);
            this.mPlaybackControl.PlaybackResume += new Movement.TestEngine.Display.PlaybackResumeHandler(this.mPlaybackControl_PlaybackResume);
            this.mPlaybackControl.PlaybackSeek += new Movement.TestEngine.Display.PlaybackSeekHandler(this.mPlaybackControl_PlaybackSeek);
            // 
            // ReplayInkOutputControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.mPlaybackControl);
            this.Name = "ReplayInkOutputControl";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer mAnimationTimer;
        private PlaybackControl mPlaybackControl;
    }
}
