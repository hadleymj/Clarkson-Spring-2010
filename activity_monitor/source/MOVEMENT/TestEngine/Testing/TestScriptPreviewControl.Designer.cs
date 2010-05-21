namespace Movement.TestEngine.Testing
{
    partial class TestScriptPreviewControl
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
            this.mPanelDirect = new System.Windows.Forms.Panel();
            this.mPanelCognitive = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // mPanelDirect
            // 
            this.mPanelDirect.BackColor = System.Drawing.Color.Red;
            this.mPanelDirect.Location = new System.Drawing.Point(8, 8);
            this.mPanelDirect.Name = "mPanelDirect";
            this.mPanelDirect.Size = new System.Drawing.Size(16, 16);
            this.mPanelDirect.TabIndex = 0;
            this.mPanelDirect.Paint += new System.Windows.Forms.PaintEventHandler(this.mPanelDirect_Paint);
            // 
            // mPanelCognitive
            // 
            this.mPanelCognitive.BackColor = System.Drawing.Color.Blue;
            this.mPanelCognitive.Location = new System.Drawing.Point(8, 30);
            this.mPanelCognitive.Name = "mPanelCognitive";
            this.mPanelCognitive.Size = new System.Drawing.Size(16, 16);
            this.mPanelCognitive.TabIndex = 1;
            this.mPanelCognitive.Paint += new System.Windows.Forms.PaintEventHandler(this.mPanelCognitive_Paint);
            // 
            // TestScriptPreviewControl
            // 
            this.Controls.Add(this.mPanelCognitive);
            this.Controls.Add(this.mPanelDirect);
            this.Name = "TestScriptPreviewControl";
            this.Size = new System.Drawing.Size(128, 128);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TestScriptPreviewControl_Paint);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel mPanelDirect;
        private System.Windows.Forms.Panel mPanelCognitive;
    }
}
