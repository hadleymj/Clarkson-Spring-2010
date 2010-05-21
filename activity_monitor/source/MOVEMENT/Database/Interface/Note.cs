using System;
using System.Collections.Generic;
using System.Text;

namespace Movement.Database
{
    /// <summary>
    /// A general note.
    /// </summary>
    public abstract class Note : DBObject
    {
        #region Private Attributes
        protected DateTime _Timestamp;
        protected int _Author;
        protected string _Data; 
        #endregion

        /// <summary>
        /// The timestamp that the note was created.
        /// </summary>
        public DateTime Timestamp { get { return _Timestamp; } }

        /// <summary>
        /// The author of the note.
        /// </summary>
		public User Author
		{
			get
			{
				if(_Author > 0)
					return new User(_Author);
				else
					return null;
			}
		}

        /// <summary>
        /// The content of the note.
        /// </summary>
        public string Data { get { return _Data; } }

        protected Note(
            DateTime timestamp,
            int author,
            string data)
        {
            _Timestamp = timestamp;
            _Author = author;
            _Data = data;
        }

		public override bool Equals(object obj)
		{
			Note other = (Note)obj;
			return Timestamp == other.Timestamp && _Author == other._Author;
		}

		public override int GetHashCode()
		{
			return Timestamp.GetHashCode() ^ _Author.GetHashCode();
		}
    }

    /// <summary>
    /// A patient-specific note.
    /// </summary>
    public class PatientNote : Note
    {
        #region Private Attributes
        private Patient _Patient; 
        #endregion

        /// <summary>
        /// The patient relating to this note.
        /// </summary>
        public Patient Patient { get { return _Patient; } }

        internal PatientNote(
            Patient patient,
            DateTime timestamp,
            int author,
            string data) : base(timestamp, author, data)
        {
            _Patient = patient;
        }

		public override bool Equals(object obj)
		{
			PatientNote other = (PatientNote)obj;
			return base.Equals(obj) && Patient == other.Patient;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ Patient.GetHashCode();
		}
    }

    /// <summary>
    /// A test-specific note.
    /// </summary>
    public class TestNote : Note
    {
        #region Private Attributes
        private Test _Test; 
        #endregion

        /// <summary>
        /// The test relating to this note.
        /// </summary>
        public Test Test { get { return _Test; } }

        internal TestNote(
            Test test,
            DateTime timestamp,
            int author,
            string data) : base(timestamp, author, data)
        {
            _Test = test;
        }

		public override bool Equals(object obj)
		{
			TestNote other = (TestNote)obj;
			return base.Equals(obj) && Test == other.Test;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode() ^ Test.GetHashCode();
		}
    }
}
