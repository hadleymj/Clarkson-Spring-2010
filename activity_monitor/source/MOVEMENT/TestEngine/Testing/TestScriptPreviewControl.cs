using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Data;
using System.Text;
using System.Windows.Forms;

using Movement.Scripting;

namespace Movement.TestEngine.Testing
{
    public partial class TestScriptPreviewControl : UserControl
    {
        #region Private Attributes
        private Bitmap _Background = null;
        private Rectangle _DirectRegion = Rectangle.Empty,
                          _CognitiveRegion = Rectangle.Empty;

        private TestScript _Script;
        private ScriptEngine _ScriptEngine;
        #endregion

        public TestScript Script
        {
            get
            {
                return _Script;
            }
            set
            {
                _Script = value;
                Configure();
            }
        }

        public TestScriptPreviewControl()
        {
            InitializeComponent();
        }

        public TestScriptPreviewControl(TestScript script)
            : this()
        {
            Script = script;
        }


        /// <summary>
        /// Configures the preview control given a change to the underlying script instance.
        /// </summary>
        private void Configure()
        {
            _ScriptEngine = new ScriptEngine(_Script.ScriptBody);

            if (_ScriptEngine.Cognitive)
                _Background = Properties.Icons.tablet_cognitive;
            else
                _Background = Properties.Icons.tablet_direct;

            _DirectRegion = GetColorRegion(_Background, Color.Red);
            _CognitiveRegion = GetColorRegion(_Background, Color.Blue);

            mPanelDirect.Location = _DirectRegion.Location;
            mPanelDirect.Size = _DirectRegion.Size;

            mPanelCognitive.Location = _CognitiveRegion.Location;
            mPanelCognitive.Size = _CognitiveRegion.Size;

            Invalidate(true);
        }

        private void TestScriptPreviewControl_Paint(object sender, PaintEventArgs e)
        {
            if (_Background == null) return;

            e.Graphics.DrawImage(_Background, 0, 0, _Background.Width, _Background.Height);
        }

        private void mPanelDirect_Paint(object sender, PaintEventArgs e)
        {
            if (_ScriptEngine == null) return;

            e.Graphics.Clear(Color.Ivory);
            
            if(!_ScriptEngine.Cognitive)
                DrawPath(_ScriptEngine.Path, mPanelDirect.Size, e.Graphics);

            if (_ScriptEngine.DirectFeedback)
                DrawInk(_ScriptEngine.Path, _ScriptEngine.PressureFeedback, mPanelDirect.Size, e.Graphics);
        }

        private void mPanelCognitive_Paint(object sender, PaintEventArgs e)
        {
            if (_ScriptEngine == null) return;

            e.Graphics.Clear(Color.Ivory);

            if (_ScriptEngine.Cognitive)
                DrawPath(_ScriptEngine.Path, mPanelCognitive.Size, e.Graphics);

            if (_ScriptEngine.CognitiveFeedback)
                DrawInk(_ScriptEngine.Path, _ScriptEngine.PressureFeedback, mPanelCognitive.Size, e.Graphics);
        }

        /// <summary>
        /// Scans a bitmap for the largest rectangular region of a specified color.
        /// </summary>
        /// <param name="b">The bitmap to scan.</param>
        /// <param name="c">The color.</param>
        /// <returns>The rectangle of the region, otherwise Rectangle.Empty if no region was found.</returns>
        private static Rectangle GetColorRegion(Bitmap b, Color c)
        {
            if (b.PixelFormat != PixelFormat.Format32bppArgb)
                return Rectangle.Empty; //we expect 32bppARGB format

            int MaskColor = c.ToArgb();
            int[] BmpData; //the BMP data in (int)A-R-G-B format

            #region BmpData Initialization
            //lock the bitmap bits to copy them to a managed array
            BitmapData Data = b.LockBits(new Rectangle(0, 0, b.Width, b.Height), System.Drawing.Imaging.ImageLockMode.ReadOnly, b.PixelFormat);

            //the managed array
            BmpData = new int[Data.Width * Data.Height];

            //do the copy
            System.Runtime.InteropServices.Marshal.Copy(Data.Scan0, BmpData, 0, BmpData.Length);

            //unlock the bitmap data
            b.UnlockBits(Data);
            #endregion

            int FirstMatch = 0,
                LastMatch = 0;

            //scan through each pixel in the bitmap data
            for (int i = 0; i < BmpData.Length; i++)
            {
                if (BmpData[i] == MaskColor)
                {   //if the pixel matches the mask color, set first/last match appropriately
                    if (FirstMatch == 0) FirstMatch = i;
                    LastMatch = i;
                }
            }

            //first, lastmatch are either both set or both 0

            return Rectangle.FromLTRB(
                FirstMatch % b.Width,
                FirstMatch / b.Width,
                LastMatch % b.Width + 1,
                LastMatch / b.Width + 1);
        }

        private static void DrawInk(GraphicsPath p, InkPressureFeedbackMode m, Size s, Graphics g)
        {
            RectangleF PathBounds = p.GetBounds();  //get the bounds of the path and compute the square scale transform          
            float ds = 0.8f * Math.Min(s.Width / PathBounds.Width, s.Height / PathBounds.Height);

            GraphicsState OriginalGraphicsState = g.Save();

            g.TranslateTransform(s.Width >> 1, s.Height >> 1);
            g.ScaleTransform(ds, -ds);

            switch (m)
            {
                case InkPressureFeedbackMode.Size:
                    g.FillEllipse(Brushes.Black, new Rectangle(-800, -800, 1600, 1600));
                    break;

                case InkPressureFeedbackMode.Width:
                    g.DrawLine(new Pen(Color.Black, 500f), new Point(-500, 0), new Point(500, 0));
                    break;

                case InkPressureFeedbackMode.None:
                case InkPressureFeedbackMode.Color:
                default:
                    GraphicsPath InkRegion = new GraphicsPath();    //upper-left triangular clip region to fake "ink"
                    InkRegion.AddPolygon(new Point[] { Point.Empty, new Point(s.Width, 0), new Point(0, s.Height) });
                    Matrix InverseTransform = g.Transform;
                    InverseTransform.Invert();
                    InkRegion.Transform(InverseTransform);

                    g.IntersectClip(new Region(InkRegion));
                    g.DrawPath(new Pen(Color.Blue, 4 / ds), p);
                    break;
            }

            g.Restore(OriginalGraphicsState); 
        }

        private static void DrawPath(GraphicsPath p, Size s, Graphics g)
        {
            RectangleF PathBounds = p.GetBounds();  //get the bounds of the path and compute the square scale transform
            float ds = 0.8f*Math.Min(s.Width / PathBounds.Width, s.Height / PathBounds.Height);

            GraphicsState OriginalGraphicsState = g.Save();
            
            g.TranslateTransform(s.Width >> 1, s.Height >> 1);
            g.ScaleTransform(ds, -ds);

            g.DrawPath(new Pen(Color.Black, 2), p);

            g.Restore(OriginalGraphicsState);           
        }
    }
}
