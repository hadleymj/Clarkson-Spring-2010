namespace Movement.TestEngine.Testing
{
    partial class CognitiveMapForm
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
            
            //make sure to dispose the cognitive map control now so that it detatches its input
            mCognitiveMapControl.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mCognitiveMapControl = new Movement.TestEngine.Testing.CognitiveMapControl();
            this.SuspendLayout();
            // 
            // mCognitiveMapControl
            // 
            this.mCognitiveMapControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mCognitiveMapControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mCognitiveMapControl.DrawInk = true;
            this.mCognitiveMapControl.DrawPressureMode = Movement.Scripting.InkPressureFeedbackMode.None;
            this.mCognitiveMapControl.InputControl = null;
            this.mCognitiveMapControl.Location = new System.Drawing.Point(0, 0);
            this.mCognitiveMapControl.Name = "mCognitiveMapControl";
            this.mCognitiveMapControl.Size = new System.Drawing.Size(292, 266);
            this.mCognitiveMapControl.TabIndex = 0;
            // 
            // CognitiveMapForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.Controls.Add(this.mCognitiveMapControl);
            this.Name = "CognitiveMapForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "CognitiveMapForm";
            this.ResumeLayout(false);

        }

        #endregion

        private CognitiveMapControl mCognitiveMapControl;
    }
}