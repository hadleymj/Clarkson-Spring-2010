namespace Movement.UserInterface
{
    partial class PatientNotes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PatientNotes));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_add_note = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.frm_add_note = new System.Windows.Forms.Label();
            this.txt_patient_note = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Movement.UserInterface.Properties.Resources.Information64;
            this.pictureBox1.Location = new System.Drawing.Point(7, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(71, 70);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // btn_add_note
            // 
            this.btn_add_note.Location = new System.Drawing.Point(359, 320);
            this.btn_add_note.Name = "btn_add_note";
            this.btn_add_note.Size = new System.Drawing.Size(75, 23);
            this.btn_add_note.TabIndex = 1;
            this.btn_add_note.Text = "Add Note";
            this.btn_add_note.UseVisualStyleBackColor = true;
            this.btn_add_note.Click += new System.EventHandler(this.btn_add_note_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_cancel.Location = new System.Drawing.Point(278, 320);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 23);
            this.btn_cancel.TabIndex = 2;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = true;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // frm_add_note
            // 
            this.frm_add_note.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.frm_add_note.Location = new System.Drawing.Point(90, 18);
            this.frm_add_note.Name = "frm_add_note";
            this.frm_add_note.Size = new System.Drawing.Size(343, 49);
            this.frm_add_note.TabIndex = 3;
            this.frm_add_note.Text = "Enter the patient note in the field below and click Add Note to continue.";
            // 
            // txt_patient_note
            // 
            this.txt_patient_note.AcceptsReturn = true;
            this.txt_patient_note.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txt_patient_note.Location = new System.Drawing.Point(7, 88);
            this.txt_patient_note.Multiline = true;
            this.txt_patient_note.Name = "txt_patient_note";
            this.txt_patient_note.Size = new System.Drawing.Size(427, 220);
            this.txt_patient_note.TabIndex = 4;
            // 
            // PatientNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_cancel;
            this.ClientSize = new System.Drawing.Size(445, 355);
            this.Controls.Add(this.txt_patient_note);
            this.Controls.Add(this.frm_add_note);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_add_note);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PatientNotes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Add Patient Note";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_add_note;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.Label frm_add_note;
        private System.Windows.Forms.TextBox txt_patient_note;

    }
}