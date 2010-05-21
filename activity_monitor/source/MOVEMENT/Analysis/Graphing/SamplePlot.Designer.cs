namespace Movement.Analysis.Graphing
{
    partial class SamplePlot
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
            this.components = new System.ComponentModel.Container();
            this.mZedGraphControl = new ZedGraph.ZedGraphControl();
            this.SuspendLayout();
            // 
            // mZedGraphControl
            // 
            this.mZedGraphControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mZedGraphControl.EditButtons = System.Windows.Forms.MouseButtons.None;
            this.mZedGraphControl.IsAntiAlias = true;
            this.mZedGraphControl.IsAutoScrollRange = true;
            this.mZedGraphControl.IsEnableHPan = false;
            this.mZedGraphControl.IsEnableHZoom = false;
            this.mZedGraphControl.IsEnableVPan = false;
            this.mZedGraphControl.IsEnableVZoom = false;
            this.mZedGraphControl.IsPrintScaleAll = false;
            this.mZedGraphControl.LinkButtons = System.Windows.Forms.MouseButtons.None;
            this.mZedGraphControl.Location = new System.Drawing.Point(0, 0);
            this.mZedGraphControl.Name = "mZedGraphControl";
            this.mZedGraphControl.ScrollGrace = 0;
            this.mZedGraphControl.ScrollMaxX = 0;
            this.mZedGraphControl.ScrollMaxY = 0;
            this.mZedGraphControl.ScrollMaxY2 = 0;
            this.mZedGraphControl.ScrollMinX = 0;
            this.mZedGraphControl.ScrollMinY = 0;
            this.mZedGraphControl.ScrollMinY2 = 0;
            this.mZedGraphControl.Size = new System.Drawing.Size(645, 379);
            this.mZedGraphControl.TabIndex = 0;
            // 
            // SamplePlot
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.mZedGraphControl);
            this.Name = "SamplePlot";
            this.Size = new System.Drawing.Size(645, 379);
            this.ResumeLayout(false);

        }

        #endregion

        private ZedGraph.ZedGraphControl mZedGraphControl;
    }
}
