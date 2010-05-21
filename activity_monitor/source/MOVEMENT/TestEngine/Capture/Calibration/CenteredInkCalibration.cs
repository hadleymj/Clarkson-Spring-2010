using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Movement.TestEngine.Capture.Calibration
{
    public class CenteredInkCalibration : InkCalibration
    {
        public CenteredInkCalibration()
        {
        }

        public CenteredInkCalibration(InkCalibration baseCalibration)
            : base(baseCalibration)
        {
        }

        protected override CalibratedInkSample PipelinedCalibratedToRawConversion(Graphics g, CalibratedInkSample sample, Size clientArea)
        {
            sample = base.PipelinedCalibratedToRawConversion(g, sample, clientArea);

            Point areaVector = new Point(clientArea.Width, clientArea.Height);
            InkRenderer.PixelToInkSpace(g, ref areaVector);

            sample.X += areaVector.X >> 1;
            sample.Y += areaVector.Y >> 1;

            return sample;
        }
        
        protected override RawInkSample PipelinedRawToCalibratedConversion(Graphics g, RawInkSample sample, Size clientArea)
        {
            sample = base.PipelinedRawToCalibratedConversion(g, sample, clientArea);

            Point areaVector = new Point(clientArea.Width, clientArea.Height);
            InkRenderer.PixelToInkSpace(g, ref areaVector);

            sample.X -= areaVector.X >> 1;
            sample.Y -= areaVector.Y >> 1;

            return sample;
        }
    }
}
