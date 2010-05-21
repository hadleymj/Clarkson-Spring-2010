using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

using Movement.Scripting;

namespace Movement.TestEngine.Display
{
    /// <summary>
    /// Provides a pen whose color is dependent on an Ink sample's pressure value.
    /// </summary>
    /// <remarks>All pressure values are in calibrated units.  All point values are in pixels.</remarks>
	internal class PressurePen : IDisposable
    {
        #region Static Pressure Gradient Map
        private const int MaximumRadius = 3500;
        private static ColorBlend Gradient;

		static PressurePen()
		{
			//define the color map
			Gradient = new ColorBlend(7);
            Gradient.Colors[0] = Color.Black;
			Gradient.Colors[1] = Color.Black;
			Gradient.Colors[2] = Color.Blue;
			Gradient.Colors[3] = Color.Green;
			Gradient.Colors[4] = Color.Orange;
			Gradient.Colors[5] = Color.Red;
			Gradient.Colors[6] = Color.Magenta;

			Gradient.Positions[0] = 0.0f;
            Gradient.Positions[1] = 0.0f;
			Gradient.Positions[2] = 0.20f;
			Gradient.Positions[3] = 0.40f;
			Gradient.Positions[4] = 0.60f;
			Gradient.Positions[5] = 0.80f;
			Gradient.Positions[6] = 1.0f;
        }

        #endregion

        #region Private Attributes

        private Control _AttachedControl;

        private int _MaxPressure;
        private Color[] _ColorCache;

        private GDIRasterInterop _Raster;


        #endregion

        public PressurePen(Control attachedControl, int maxPressure)
		{
            _AttachedControl = attachedControl;
            _AttachedControl.Resize += new EventHandler(_AttachedControl_Resize);

            _MaxPressure = maxPressure;
            _ColorCache = new Color[maxPressure+1];
            
            //initialize the color cache
            for (int i = 0; i < _ColorCache.Length; i++)
                _ColorCache[i] = Color.Empty;

            _AttachedControl_Resize(null, null);
		}

        private void _AttachedControl_Resize(object sender, EventArgs e)
        {
            using (Graphics g = _AttachedControl.CreateGraphics())
            {
                _Raster = new GDIRasterInterop(_AttachedControl.Size, GetRadius(g, _MaxPressure) << 1); ;
            }
        }

        public void Draw(
            Graphics g,
            InkPressureFeedbackMode pressureMode,
            Capture.CalibratedInkSample lastSample,
            Capture.CalibratedInkSample currentSample,
            Capture.RawInkSample lastRawSample,
            Capture.RawInkSample currentRawSample)
        {
            GraphicsState OriginalGraphicsState = g.Save();
            g.SmoothingMode = SmoothingMode.HighQuality;

            Point p1 = new Point(lastRawSample.X, lastRawSample.Y);
            Point p2 = new Point(currentRawSample.X, currentRawSample.Y);

            //ink space ==> pixel space transformation
            Capture.Calibration.InkCalibration.Current.InkRenderer.InkSpaceToPixel(g, ref p1);
            Capture.Calibration.InkCalibration.Current.InkRenderer.InkSpaceToPixel(g, ref p2);

            switch (pressureMode)
            {
                case InkPressureFeedbackMode.Size:
                    //drawing circles (uninverting then inverting)
                    _Raster.Begin(g);

                    if (!lastSample.IsInvalid())
                        _Raster.InvertCircle(p1, GetRadius(g, lastSample.Pressure));    //uninvert the last circle

                    if (!currentSample.IsInvalid())
                        _Raster.InvertCircle(p2, GetRadius(g, currentSample.Pressure)); //invert the next circle

                    _Raster.Commit(g);
                    break;

                default:
                    //drawing lines or similar (connecting two valid points)
                    if (!lastRawSample.IsInvalid()
                        && !currentRawSample.IsInvalid())
                    {
                        switch (pressureMode)
                        {
                            case InkPressureFeedbackMode.Color:
                                DrawPressureLine(g, p1, p2, currentSample.Pressure);
                                break;
                            case InkPressureFeedbackMode.Width:
                                DrawWidth(g, p1, p2, lastSample.Pressure, currentSample.Pressure);
                                break;
                            case InkPressureFeedbackMode.None:
                            default:
                                DrawLine(g, p1, p2);
                                break;
                        }
                    }
                    break;
            }

            g.Restore(OriginalGraphicsState);
        }

        #region Drawing Helpers

        private void DrawWidth(Graphics g, Point p1, Point p2, int n1, int n2)
        {
            //use some trig to compute a polygon that connects a circle at p1 and a circle at p2
            float theta = -1 * (float)Math.Atan(((double)(p2.Y - p1.Y)) / (p2.X - p1.X));

            float  cosTheta = (float)Math.Cos(theta),
                   sinTheta = (float)Math.Sin(theta);

            int r1 = GetRadius(g, n1) >> 1,
                r2 = GetRadius(g, n2) >> 1;

            //fill the circle for p2 (p1 was filled by previous call)
            g.FillEllipse(Brushes.Black, new Rectangle(p2.X - r2, p2.Y - r2, r2 << 1, r2 << 1));

            g.FillPolygon(Brushes.Black,
                new PointF[]
                {
                    new PointF(p1.X - r1 * cosTheta, p1.Y - r1 * sinTheta), //compute points on the perimiters of circles p1, p2
                    new PointF(p2.X - r2 * cosTheta, p2.Y - r2 * sinTheta),
                    new PointF(p2.X + r2 * cosTheta, p2.Y + r2 * sinTheta),
                    new PointF(p1.X + r1 * cosTheta, p1.Y + r1 * sinTheta),
                });
        }

        private void DrawLine(Graphics g, Point p1, Point p2)
        {
            g.DrawLine(new Pen(Color.Black, 2), p1, p2);
        }

        private void DrawPressureLine(Graphics g, Point p1, Point p2, int pressure)
		{
            g.DrawLine(new Pen(GetColorMapValue(pressure), 2), p1, p2);
		}

        #endregion

        #region Other Helpers

        /// <summary>
        /// Gets the radius that a pressure should be represented as.
        /// </summary>
        /// <param name="g">A graphics object used to translated ink ==> pixel space.</param>
        /// <param name="pressure">The calibrated pressure.</param>
        /// <returns>The radius of the circle.</returns>
        private int GetRadius(Graphics g, int pressure)
        {
            Point ScaleVector = new Point(MaximumRadius, MaximumRadius);
            Capture.Calibration.InkCalibration.Current.InkRenderer.InkSpaceToPixel(g, ref ScaleVector);

            //clamp the pressure value
            pressure = Math.Min(_MaxPressure, pressure);
            pressure = Math.Max(0, pressure);

            return Math.Max(3, (int)(ScaleVector.X * (pressure / (float)_MaxPressure)));
        }

        /// <summary>
        /// Computes (or gets a cached copy) of the color to represent a pressure as.
        /// </summary>
        /// <param name="pressure">The calibrated pressure.</param>
        /// <returns>The color corresponding to the pressure.</returns>
        private Color GetColorMapValue(int pressure)
        {
            //if max pressure is 0 then we can be done quickly
            if (_MaxPressure == 0)
                return Color.Black;

            //clamp the pressure value
            pressure = Math.Min(_MaxPressure, pressure);
            pressure = Math.Max(0, pressure);

            if (_ColorCache[pressure] == Color.Empty)
                _ColorCache[pressure] = ComputeColorMapValue(pressure);

            return _ColorCache[pressure];
        }

        /// <summary>
        /// Blends the two ARGB gradient values on either side of gradient-index corresponding to the supplied pressure.
        /// </summary>
        /// <param name="pressure">The calibrated pressure.</param>
        /// <returns>The color to draw the pressure sample in.</returns>
        private Color ComputeColorMapValue(int pressure)
		{
            float RelPressure = (float)pressure / _MaxPressure;

            int RIndex = GetColorIndex(RelPressure);
            int LIndex = RIndex - 1;

            Color RColor = Gradient.Colors[RIndex];
            Color LColor = Gradient.Colors[LIndex];

            float Delta = Gradient.Positions[RIndex] - Gradient.Positions[LIndex];
            float ColorDelta;   //0% ==> LColor, 100% ==> RColor

            ColorDelta = (Delta == 0 ? 0 : (RelPressure - Gradient.Positions[LIndex]) / Delta);

            //blend the two colors
            return Color.FromArgb(
                (int)(LColor.A + ColorDelta * ((short)RColor.A - LColor.A)),
                (int)(LColor.R + ColorDelta * ((short)RColor.R - LColor.R)),
                (int)(LColor.G + ColorDelta * ((short)RColor.G - LColor.G)),
                (int)(LColor.B + ColorDelta * ((short)RColor.B - LColor.B)));

		}

        /// <summary>
        /// Gets the gradient-index that corresponds to the relative pressure.  Relative pressure is in the interval
        /// [0, 1], where 1 means the pressure is equal to the maximum calibrated pressure.
        /// </summary>
        /// <param name="relPressure">The relative pressure, in the interval [0, 1].</param>
        /// <returns>The gradient-index.</returns>
        private int GetColorIndex(float relPressure)
        {
            for (int i = 1; i < Gradient.Positions.Length; i++)
                if (Gradient.Positions[i] > relPressure)
                    return i;

            //default: return max pressure color
            return Gradient.Positions.Length - 1;
        }

        #endregion

        #region IDisposable Members

        private bool _Disposed = false;
        public void Dispose()
        {
            if (_Disposed)
            {
                _Disposed = true;
                _AttachedControl.Resize -= new EventHandler(_AttachedControl_Resize);
            }
        }

        #endregion

        #region Composite Class - GDIRasterInterop

        private class GDIRasterInterop
        {
            #region GDI32 External Functions

            [DllImport("GDI32.DLL")]
            private static extern int BitBlt(
                IntPtr hdcDest,
                int nXDest,
                int nYDest,
                int nWidth,
                int nHeight,
                IntPtr hdcSrc,
                int nXSrc,
                int nYsrc,
                int swRop);

            [DllImport("GDI32.DLL")]
            private static extern IntPtr CreateCompatibleDC(
                IntPtr hdc);

            [DllImport("GDI32.DLL")]
            private static extern int DeleteDC(
                IntPtr hdc);

            [DllImport("GDI32.DLL")]
            private static extern IntPtr CreateCompatibleBitmap(
                IntPtr hdc,
                int nWidth,
                int nHeight);

            [DllImport("GDI32.DLL")]
            private static extern IntPtr SelectObject(
                IntPtr hdc,
                IntPtr handle);

            [DllImport("GDI32.DLL")]
            private static extern int DeleteObject(
                IntPtr handle);

            #endregion

            #region Private Attributes

            private Size _SurfaceSize;
            private int _MaxDiameter;

            private GDISurface _Buffer = null,
                _BackBuffer = null;

            private Rectangle _InvalidRegion = Rectangle.Empty;

            #endregion

            public GDIRasterInterop(Size surfaceSize, int maxDiameter)
            {
                _SurfaceSize = surfaceSize;
                _MaxDiameter = maxDiameter;
            }

            public void Begin(Graphics g)
            {
                IntPtr GraphicsHandle = g.GetHdc();
                try
                {
                    //TODO: make sure that the device is still compatible

                    //make sure we are properly setup for the graphics instance
                    if (_Buffer == null)
                        _Buffer = new GDISurface(GraphicsHandle, _SurfaceSize);

                    if (_BackBuffer == null)
                        _BackBuffer = new GDISurface(GraphicsHandle, new Size(_MaxDiameter, _MaxDiameter));

                    _Buffer.Clear();
                    _BackBuffer.Clear();

                    _InvalidRegion = Rectangle.Empty;
                }
                finally
                {
                    g.ReleaseHdc(GraphicsHandle);
                }
            }

            public void InvertCircle(Point p, int r)
            {
                p.Offset(-r, -r);
                int d = r << 1;
                Size bounds = new Size(d, d);

                _BackBuffer.FillCircle(r);
                _BackBuffer.Invert(_Buffer.Device, p, bounds, Point.Empty);
                _BackBuffer.Clear();

                if (_InvalidRegion == Rectangle.Empty)
                    _InvalidRegion = new Rectangle(p, bounds);
                else
                    _InvalidRegion = Rectangle.Union(_InvalidRegion, new Rectangle(p, bounds));
            }

            public void Commit(Graphics g)
            {
                IntPtr GraphicsHandle = g.GetHdc();
                try
                {
                    _Buffer.Invert(GraphicsHandle, _InvalidRegion.Location, _InvalidRegion.Size, _InvalidRegion.Location);
                }
                finally
                {
                    g.ReleaseHdc(GraphicsHandle);
                }
            }

            #region Composite Class - GDISurface

            private class GDISurface
            {
                public IntPtr Device = IntPtr.Zero;
                public IntPtr Bitmap = IntPtr.Zero;
                public Size SurfaceSize;

                public GDISurface(IntPtr compatibleDevice, Size surfaceSize)
                {
                    SurfaceSize = surfaceSize;

                    Device = CreateCompatibleDC(compatibleDevice);
                    if (Device == IntPtr.Zero)
                        throw new OutOfMemoryException();

                    Bitmap = CreateCompatibleBitmap(compatibleDevice, surfaceSize.Width, surfaceSize.Height);
                    if (Bitmap == IntPtr.Zero)
                        throw new OutOfMemoryException();

                    SelectObject(Device, Bitmap);
                }

                ~GDISurface()
                {
                    if (Bitmap != IntPtr.Zero)
                        DeleteObject(Bitmap);    //delete the bitmap

                    if (Device != IntPtr.Zero)
                        DeleteDC(Device);    //delete the created dc
                }

                public void Clear()
                {
                    using (Graphics g = Graphics.FromHdc(Device))
                    {
                        g.Clear(Color.Black);
                    }
                }

                public void FillCircle(int r)
                {
                    r <<= 1;

                    using(Graphics g = Graphics.FromHdc(Device))
                    {
                        g.FillEllipse(Brushes.White, 0, 0, r, r);
                    }
                }

                public void Invert(IntPtr target, Point dest, Size size, Point source)
                {
                    //perform the bitblt from our created surface to the existing drawing surface
                    BitBlt(
                        target, //destination
                        dest.X,
                        dest.Y,
                        size.Width,  //dimensions
                        size.Height,
                        Device,  //source
                        source.X,
                        source.Y,
                        0x00660046);    //source invert ROP
                }
            }
            #endregion

        }

        #endregion


    }
}
