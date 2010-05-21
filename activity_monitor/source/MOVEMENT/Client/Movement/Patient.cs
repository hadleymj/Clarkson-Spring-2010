using System;
using System.Collections.Generic;
using System.Text;
using Movement.UserInterface.movement_web_service;

namespace Movement.UserInterface
{
    public class Patient
    {
        WebServicer Connection = new WebServicer();
        PatientObject current_patient = new PatientObject();

        public Patient() { }

        public List<PatientObject> RetrievePatient(String name, String ssn, DateTime dob)
        {
            PatientObject patient = new PatientObject();
            patient.dob = dob;
            patient.ssn = ssn;
            patient.name = name;

            return new List<PatientObject>(Connection.Servicer.findPatient(patient));
        }

        public int AddPatient(UserObject user, String name, String ssn, DateTime dob, Char sex, Char hand, String contact, String address, String notes)
        {
            current_patient.name = name;
            current_patient.ssn = ssn;
            current_patient.dob = dob;
            current_patient.sex = sex;
            current_patient.handedness = hand;
            current_patient.ContactInfo = contact;
            current_patient.address = address;

            current_patient.ID = Connection.Servicer.addPatient(current_patient);
            this.AddNote(current_patient, user, notes);

            return current_patient.ID;
        }

        public Notes[] GetNotes(PatientObject patient)
        {
            return Connection.Servicer.getPatientNotes(patient);
        }

        public PatientObject ThisPatient
        {
            get { return current_patient; }
            set { current_patient = value; }
        }

        public Boolean AddNote(PatientObject patient, UserObject user, String data)
        {
            return Connection.Servicer.addPatientNote(patient, user, data);
        }
    }
}
