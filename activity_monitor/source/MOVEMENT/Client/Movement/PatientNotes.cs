using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Movement.UserInterface.movement_web_service;

namespace Movement.UserInterface
{
    public partial class PatientNotes : Form
    {
        private Patient patient;
        private UserObject user;
        private Label notes;
        

        public PatientNotes(UserObject user, PatientObject patient, Label patient_notes)
        {
            InitializeComponent();
            this.patient = new Patient();
            this.patient.ThisPatient = patient;
            this.user = user;
            this.notes = patient_notes;
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_patient_note.Clear();
            this.Dispose();
        }

        private void btn_add_note_Click(object sender, EventArgs e)
        {
            Boolean success;
            if (txt_patient_note.Text.Equals(""))
            {
                MessageBox.Show("A patient note cannot be empty.", "Required Information Missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            success = patient.AddNote(patient.ThisPatient, user, txt_patient_note.Text);

            if (!success)
                MessageBox.Show("The patient note could not be added at this time.  Please try again.", "Error Adding Note", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                String new_note = "Author: " + user.name + "\r\n" + "Date: " + String.Format("{0:d}",DateTime.UtcNow) + "\r\n" + txt_patient_note.Text + "\r\n\r\n";
                notes.Text = new_note + notes.Text;
                txt_patient_note.Clear();
                this.Dispose();
            }
        }
    }
}