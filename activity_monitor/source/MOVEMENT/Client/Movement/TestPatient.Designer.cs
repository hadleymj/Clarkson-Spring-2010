namespace Movement.UserInterface
{
    partial class TestPatient
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
            this.tbcc_test_patient = new global::Movement.TestEngine.Testing.TestBatchCaptureControl();
            this.SuspendLayout();
            // 
            // tbcc_test_patient
            // 
            this.tbcc_test_patient.BackColor = System.Drawing.Color.Ivory;
            this.tbcc_test_patient.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tbcc_test_patient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcc_test_patient.DrawInk = true;
            this.tbcc_test_patient.DrawPressureMode = global::Movement.Scripting.InkPressureFeedbackMode.None;
            this.tbcc_test_patient.Location = new System.Drawing.Point(0, 0);
            this.tbcc_test_patient.Name = "tbcc_test_patient";
            this.tbcc_test_patient.Size = new System.Drawing.Size(292, 273);
            this.tbcc_test_patient.TabIndex = 0;
            // 
            // TestPatient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 273);
            this.Controls.Add(this.tbcc_test_patient);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "TestPatient";
            this.Text = "TestPatient";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.TestPatient_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private global::Movement.TestEngine.Testing.TestBatchCaptureControl tbcc_test_patient;
    }
}