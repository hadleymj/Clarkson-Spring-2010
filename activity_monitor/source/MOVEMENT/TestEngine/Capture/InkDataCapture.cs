using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Microsoft.Ink;
using Microsoft.StylusInput;

using Movement.TestEngine.Capture.Calibration;

namespace Movement.TestEngine.Capture
{
    /// <summary>
    /// Handler for new ink samples (where the samples are already calibrated).
    /// </summary>
    /// <param name="calSample">The new calibrated ink sample.</param>
    public delegate void NewInkSampleHandler(CalibratedInkSample calSample);

    /// <summary>
    /// Handler for new raw ink samples.
    /// </summary>
    /// <param name="rawSample">The new raw ink sample.</param>
    public delegate void NewRawInkSampleHandler(RawInkSample rawSample);

    /// <summary>
    /// Provides capture services for ink given a windows forms control.
    /// </summary>
	public class InkDataCapture : IComponent
    {
        #region Private Attributes
        private InkOverlay _Overlay;

		private HighResTimer _Timer = new HighResTimer();

        private List<CalibratedInkSample> _CapturedPackets = new List<CalibratedInkSample>(2 * 1024);	//2K samples
		#endregion

		/// <summary>
		/// A list of packets captured so far.
		/// </summary>
		public List<CalibratedInkSample> CapturedPackets { get { return _CapturedPackets; } }

        /// <summary>
        /// True if capturing is enabled, otherwise false.
        /// </summary>
        public bool IsCapturing { get { return _Overlay.Enabled; } }

        /// <summary>
        /// Occurs once for every new calibrated ink sample that is captured.
        /// </summary>
		public event NewInkSampleHandler NewInkSample;

        /// <summary>
        /// Creates a new capture component and attaches it to the specified control.
        /// </summary>
        /// <param name="attachedControl"></param>
		public InkDataCapture(Control attachedControl)
		{
            _Overlay = new InkOverlay(attachedControl);

            _Overlay.AttachMode = InkOverlayAttachMode.Behind;
            _Overlay.CollectionMode = CollectionMode.InkOnly;
            _Overlay.EditingMode = InkOverlayEditingMode.Ink;	//draw ink

            _Overlay.DynamicRendering = false;	//don't draw ink as it is presented
            _Overlay.AutoRedraw = false; //don't redraw ink when necessary			

            //collect data as {X, Y, Pressure?} samples
            _Overlay.DesiredPacketDescription = new Guid[]{
                        PacketProperty.X,
						PacketProperty.Y,
                        PacketProperty.TimerTick,
						PacketProperty.NormalPressure};

            _Overlay.NewPackets += new InkCollectorNewPacketsEventHandler(Overlay_NewPackets);
		}

		/// <summary>
		/// Begins capturing ink packets.  Packets that have already been collected are cleared.
		/// </summary>
		public void StartCapture()
		{
            Clear();

            //enable pen capture and start the timer
			_Timer.Start();
			_Overlay.Enabled = true;
		}

		/// <summary>
		/// Stops capturing ink packets.
		/// </summary>
		public void StopCapture()
		{
			//disable pen capture and stop the timer
			_Overlay.Enabled = false;
			_Timer.Now();
		}

        /// <summary>
        /// Clears all of the captured packets.
        /// </summary>
        public void Clear()
        {
            _CapturedPackets.Clear();
        }

        private bool Overlay_NewPackets_PacketsHavePressure = false;
		private void Overlay_NewPackets(object sender, InkCollectorNewPacketsEventArgs e)
		{
			/*** THIS ROUTINE MUST BE EXTREMELY FAST ***/
			/*
			 * Since we are handling a flurry of packet
			 * data, this routine should not perform any
			 * significant processing, just data recording.
             * 
             * Asynchronous invocation on the windows forms
             * thread is used to delay as much sample
             * processing as possible.
			 */

            //go over the packet data and store InkSamples
			int[] PacketData = e.PacketData;

			if(PacketData[2] == 0) //new stroke since timer is 0; use the internal timer to compute the time of the first packet
			{
                //check if the packets have pressure data
                Tablet CurrentTablet = e.Cursor.Tablet;
                Overlay_NewPackets_PacketsHavePressure = CurrentTablet != null
                                                        && CurrentTablet.IsPacketPropertySupported(PacketProperty.NormalPressure);

				//raise the NewInkSample event with an invalid ink sample to signal a new stroke to listeners
                _Overlay.AttachedControl.BeginInvoke(new NewRawInkSampleHandler(OnNewRawInkSample), new RawInkSample(_Timer.Now()));
			}

			for(int i = 0; i < PacketData.Length; )
			{
				RawInkSample s = new RawInkSample();

                s.Time = _Timer.Now();

				s.X = PacketData[i++];
				s.Y = PacketData[i++];
                i++;    //skip the timer sample
				s.Pressure = Overlay_NewPackets_PacketsHavePressure ? PacketData[i++] : 0;


				//have the form thread raise the NewInkSample event at its convenience
                _Overlay.AttachedControl.BeginInvoke(new NewRawInkSampleHandler(OnNewRawInkSample), s);
			}
		}

		private void OnNewRawInkSample(RawInkSample raw)
		{
            //checking for duplicate times
            if (_CapturedPackets.Count > 0 && _CapturedPackets[_CapturedPackets.Count - 1].Time == raw.Time)
                return;

            //calibrate the ink sample
            CalibratedInkSample cal;

            using (Graphics g = _Overlay.AttachedControl.CreateGraphics())
            {
                cal = InkCalibration.Current.RawToCalibrated(g, raw, _Overlay.AttachedControl.Size);
            }

#if(DEBUG)
			Console.WriteLine(cal);
#endif
			//store the sample
			_CapturedPackets.Add(cal);

			if(NewInkSample != null)
				NewInkSample(cal);
		}

		#region IComponent Members

		public event EventHandler Disposed;

		public ISite Site
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		#endregion

		#region IDisposable Members

		private bool _Disposed = false;
		public void Dispose()
		{
			if(!_Disposed)
			{
				_Disposed = true;

				//do some cleanup
				_Overlay.NewPackets -= new InkCollectorNewPacketsEventHandler(Overlay_NewPackets);
				_Overlay.Dispose();

                if(Disposed != null)
				    Disposed(this, null);
			}
		}

		#endregion
	}
}
