using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Microsoft.Ink;
using Microsoft.StylusInput;

namespace Movement.TestEngine.Capture
{
	public partial class InkInputControl : Movement.TestEngine.Display.InkOutputControl
	{		
        #region Private Attributes

		private InkDataCapture _DataCapture;

		#endregion

        /// <summary>
        /// The InkDataCapture instance used for this control to capture Ink input.
        /// </summary>
        public InkDataCapture DataCapture { get { return _DataCapture; } }

        /// <summary>
        /// Occurs for every new ink sample that is captured.
        /// </summary>
		public event NewInkSampleHandler NewInkSample;

		public InkInputControl()
		{
			InitializeComponent();

			//create a new capture component and add it to our list of components
			_DataCapture = new InkDataCapture(this);
			_DataCapture.NewInkSample += new NewInkSampleHandler(DataCapture_NewInkSample);

            if (components == null)
                components = new Container();
            
            components.Add(_DataCapture);
		}

		/// <summary>
		/// Begins capturing ink packets.
		/// </summary>
		public void StartCapture()
		{
			_DataCapture.StartCapture();
		}

		/// <summary>
		/// Stops capturing ink packets.
		/// </summary>
		public void StopCapture()
		{
			_DataCapture.StopCapture();
		}

		private void DataCapture_NewInkSample(CalibratedInkSample cal)
		{
            DrawSample(cal);

			if(NewInkSample != null)
				NewInkSample(cal);
		}
	}
}
