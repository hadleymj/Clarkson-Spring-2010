using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

using Microsoft.Ink;
using Microsoft.StylusInput;

namespace Movement.TestEngine.Capture.Calibration
{
    /// <summary>
    /// Supertype of all calibration types.  Calibration is done in a "pipeline" manner where calibrations may be composed.
    /// A global calibration instance is maintained here.
    /// </summary>
    public class InkCalibration
    {
        #region Global "Current" InkCalibration Instance
        private static InkCalibration _Current = new InkCalibration();
        public static InkCalibration Current
        {
            get
            {
                return _Current;
            }
            set
            {
                Interlocked.Exchange<InkCalibration>(ref _Current, value);
            }
        }

        public static void Debug()
        {
            Console.WriteLine("Current Calibration:");
            Console.WriteLine("Maximum Raw Pressure: {0}", Current.MaximumRawPressure);
            Console.WriteLine("Maxium Calibrated Pressure: {0}", Current.MaximumCalibratedPressure);
            Console.WriteLine();
            Console.WriteLine();

            foreach(Tablet t in new Tablets())
            {
                Console.WriteLine("Tablet Information:");
                Console.WriteLine("Tablet Name: {0}", t.Name);
                Console.WriteLine("Tablet PPID: {0}", t.PlugAndPlayId);
                if (new Tablets().DefaultTablet == t)
                    Console.WriteLine("DEFAULT");

                Console.WriteLine("Supported Metrics:");

                foreach (System.Reflection.FieldInfo fi in typeof(PacketProperty).GetFields())
                {
                    Guid PProp = (Guid)fi.GetValue(null);

                    Console.WriteLine("{0}: {1}",
                        fi.Name,
                        t.IsPacketPropertySupported(PProp) ? "Supported" : "Not Supported");

                    if (t.IsPacketPropertySupported(PProp))
                    {
                        TabletPropertyMetrics Metrics = t.GetPropertyMetrics(PProp);
                        Console.WriteLine("[{0}, {1}], d{2} as {3}", Metrics.Minimum, Metrics.Maximum, Metrics.Resolution, Metrics.Units);
                    }                       
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }
        #endregion

        #region Private Attributes

        private InkCalibration _BaseCalibration;
        private Renderer _InkRenderer = new Renderer();

        #endregion 

        /// <summary>
        /// The renderer used to draw ink.  Used for ink - pixel space conversions.
        /// </summary>
        public Renderer InkRenderer { get { return _InkRenderer; } }

        #region Maximum Pressure

        public int MaximumRawPressure
        {
            get
            {
                Tablets TabletCollection = new Tablets();

                if (TabletCollection.Count > 0
                    && TabletCollection.DefaultTablet != null
                    && TabletCollection.DefaultTablet.IsPacketPropertySupported(PacketProperty.NormalPressure))
                {
                    //then we have a default tablet that supports pressure!
                    return TabletCollection.DefaultTablet.GetPropertyMetrics(PacketProperty.NormalPressure).Maximum;
                }
                else
                {
                    return 0;
                }
            }
        }

        public virtual int MaximumCalibratedPressure
        {
            get
            {
                return MaximumRawPressure;
            }
        }

        #endregion

        public InkCalibration()
            : this(null)
        {
        }

        public InkCalibration(InkCalibration baseCalibration)
        {
            _BaseCalibration = baseCalibration;
        }

        /// <summary>
        /// Converts a CalibratedInkSample to a RawInkSample.
        /// </summary>
        /// <param name="g">The capture- or display- surface graphics object.</param>
        /// <param name="sample">The calibrated sample to convert.</param>
        /// <param name="clientArea">The capture- or display- surface size in pixels.</param>
        /// <returns>The RawInkSample.</returns>
        public RawInkSample CalibratedToRaw(Graphics g, CalibratedInkSample sample, Size clientArea)
        {
            //don't bother calibrating invalid samples
            if (sample.IsInvalid()) return new RawInkSample(sample.Time, sample.X, sample.Y, sample.Pressure);

            //next apply this instance's calibration
            sample = PipelinedCalibratedToRawConversion(g, sample, clientArea);

            //return a raw sample
            return new RawInkSample(sample.Time, sample.X, sample.Y, sample.Pressure);
        }

        /// <summary>
        /// Converts a RawInkSample to a CalibratedInkSample.
        /// </summary>
        /// <param name="g">The capture- or display- surface graphics object.</param>
        /// <param name="sample">The raw sample to convert.</param>
        /// <param name="clientArea">The capture- or display- surface size in pixels.</param>
        /// <returns>The CalibratedInkSample.</returns>
        public CalibratedInkSample RawToCalibrated(Graphics g, RawInkSample sample, Size clientArea)
        {
            //don't bother calibrating invalid samples
            if (sample.IsInvalid()) return new CalibratedInkSample(sample.Time, sample.X, sample.Y, sample.Pressure);

            //next apply this instance's calibration
            sample = PipelinedRawToCalibratedConversion(g, sample, clientArea);

            //return a calibrated sample
            return new CalibratedInkSample(sample.Time, sample.X, sample.Y, sample.Pressure);
        }
           


        protected virtual CalibratedInkSample PipelinedCalibratedToRawConversion(Graphics g, CalibratedInkSample sample, Size clientArea)
        {
            //invoke the base calibration
            if (_BaseCalibration != null)
                sample = _BaseCalibration.PipelinedCalibratedToRawConversion(g, sample, clientArea);

            //default implementation does no calibration
            return sample;
        }

        protected virtual RawInkSample PipelinedRawToCalibratedConversion(Graphics g, RawInkSample sample, Size clientArea)
        {
            //invoke the base calibration
            if (_BaseCalibration != null)
                sample = _BaseCalibration.PipelinedRawToCalibratedConversion(g, sample, clientArea);

            //default implementation does no calibration
            return sample;
        }

    }
}
