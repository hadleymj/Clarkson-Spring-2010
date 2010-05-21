using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using WiimoteLib;

namespace PDA_GUI
{
    /// <summary>
    /// Class which will provide a connection to the WiiRemote. The 
    /// purpose is to store the accelerometer data until it is to 
    /// be processed.
    /// </summary>
    public class WiiRemoteConnection //: Connection
    {

        private Wiimote wm;
        private Queue<RawData> rawDataQueue;

        /// <summary>
        /// Instantiate the class and initialize the private members as needed.
        /// </summary>
        public WiiRemoteConnection()
        {
            rawDataQueue = new Queue<RawData>();

            wm = new Wiimote();
            wm.WiimoteChanged += wm_WiimoteChanged;

        }

        //override public void connect()
        public bool connect()
        {
            try
            {
                
                wm.Connect();
                wm.SetReportType(InputReport.IRAccel, true);
                wm.SetLEDs(false, true, true, false);
                
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

        //override public void disconnect()
        public void disconnect()
        {
            wm.Disconnect();
            return;
        }

        /// <summary>
        /// If the wii-remotes status changes it means there is a new
        /// accelerometer reading to put on the Queue.
        /// </summary>
        /// <param name="sender">Object initiating the function call.</param>
        /// <param name="args">The wii status message.</param>
        private void wm_WiimoteChanged(object sender, WiimoteChangedEventArgs args)
        {
            RawData sample = new RawData();
            sample.accel_x = args.WiimoteState.AccelState.RawValues.X - args.WiimoteState.AccelCalibrationInfo.X0;
            sample.accel_y = args.WiimoteState.AccelState.RawValues.Y - args.WiimoteState.AccelCalibrationInfo.Y0;
            sample.accel_z = args.WiimoteState.AccelState.RawValues.Z - args.WiimoteState.AccelCalibrationInfo.Z0;

            //get the unix timestamp.
            TimeSpan span = (DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime());
            sample.time = (long)span.TotalMilliseconds;

            //Lock the queue while the next sample is added.
            lock (rawDataQueue)
            {
                rawDataQueue.Enqueue(sample);
            }
        }

        /// <summary>
        /// Returns the next RawData object collected from the WiiRemote.
        /// The objects are in the order they were collected.
        /// </summary>
        /// <returns>The next RawData object representing the accelerometer readings.</returns>
        public RawData getData()
        {
            //Make sure no data is added to the rawDataQueue while 
            //the next object is being popped off of it.
            lock (rawDataQueue)
            {
                if (rawDataQueue.Count() > 0)
                {
                    return rawDataQueue.Dequeue();
                }
                else
                {
                    return null;
                }
            }
        }



    }
}
