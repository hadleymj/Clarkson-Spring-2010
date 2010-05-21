using System;
using System.Collections.Generic;
using System.Text;

namespace Movement.TestEngine.Capture.Calibration
{
    public class PressureInkCalibration : InkCalibration
    {
        public PressureInkCalibration()
        {
        }

        public PressureInkCalibration(InkCalibration baseCalibration)
            : base(baseCalibration)
        {
        }

        protected override CalibratedInkSample PipelinedCalibratedToRawConversion(System.Drawing.Graphics g, CalibratedInkSample sample, System.Drawing.Size clientArea)
        {
            //TODO: implement
            return base.PipelinedCalibratedToRawConversion(g, sample, clientArea);
        }

        protected override RawInkSample PipelinedRawToCalibratedConversion(System.Drawing.Graphics g, RawInkSample sample, System.Drawing.Size clientArea)
        {
            //TODO: implement
            return base.PipelinedRawToCalibratedConversion(g, sample, clientArea);
        }
    }
}
