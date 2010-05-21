using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Text;

namespace Movement.Scripting
{
	/// <summary>
	/// Interprets text scripts as stored in the database.
	/// </summary>
	public class ScriptEngine
	{
		#region Private Attributes

        private bool _DirectFeedback = true;
        private bool _Cognitive = false;
        private bool _CognitiveFeedback = false;
        private InkPressureFeedbackMode _PressureFeedback = InkPressureFeedbackMode.None;
        private int _TimeLimit = -1;
        private Point _StartingPoint = new Point(int.MaxValue, int.MaxValue);

		private string _Instructions = "";
		private GraphicsPath _Path;
		
		#endregion

		/// <summary>
		/// The instruction meta data gathered from the script.
		/// </summary>
		public string Instructions { get { return _Instructions; } }

		/// <summary>
		/// The graphics path that must be drawn for this script.
		/// </summary>
		public GraphicsPath Path { get { return _Path; } }

        /// <summary>
        /// If true, the test should be run in cognitive mode.
        /// </summary>
        public bool Cognitive { get { return _Cognitive; } }

        /// <summary>
        /// If true, ink should be presented under the pen.
        /// </summary>
        public bool DirectFeedback { get { return _DirectFeedback; } }

        /// <summary>
        /// If true, ink should be presented on the cognitive mapping display.
        /// </summary>
        public bool CognitiveFeedback { get { return Cognitive && _CognitiveFeedback; } }

        /// <summary>
        /// The mode under which the pressure should be displayed.
        /// </summary>
        public InkPressureFeedbackMode PressureFeedback { get { return _PressureFeedback; } }

        /// <summary>
        /// The maximum time to run this test in seconds, or -1 if no time limit.
        /// </summary>
        public int TimeLimit { get { return _TimeLimit; } }

        /// <summary>
        /// The starting point of the script.
        /// </summary>
        public Point StartingPoint { get { return _StartingPoint; } }

		/// <summary>
		/// Creates a new instance of the script engine and interprets a given script.
		/// </summary>
		/// <param name="script">The script to interpret.</param>
		public ScriptEngine(string script)
		{
			ReadScript(script);
		}


		private void ReadScript(string script)
		{
			using(StringReader Reader = new StringReader(script))
			{
				ReadHeader(Reader);
				_Path = ReadGraphicsPath(Reader);
			}
		}

        private void ReadHeader(StringReader reader)
		{
			string Line;
            while((Line = reader.ReadLine()) != null
                && Line != "")
            {
                if(IsCommentLine(Line))
                    continue;

                InterpretHeaderLine(Line);
			}
		}

        private void InterpretHeaderLine(string line)
        {
            string[] Part = line.Split(new string[] { ": " }, 2, StringSplitOptions.None);
            if (Part.Length != 2) return;

            try
            {
                switch (Part[0])
                {
                    case "DirectFeedback":
                        _DirectFeedback = bool.Parse(Part[1]);
                        break;
                    case "Cognitive":
                        _Cognitive = bool.Parse(Part[1]);
                        break;
                    case "CognitiveFeedback":
                        _CognitiveFeedback = bool.Parse(Part[1]);
                        break;
                    case "PressureFeedback":
                        _PressureFeedback = (InkPressureFeedbackMode)Enum.Parse(typeof(InkPressureFeedbackMode), Part[1]);
                        break;
                    case "TimeLimit":
                        _TimeLimit = int.Parse(Part[1]);
                        break;
                    case "Instructions":
                        _Instructions = Part[1];
                        break;
                    case "StartingPoint":
                        string[] Subparts = Part[1].Replace(" ", "").Split(new char[] { ',' }, 2);
                        _StartingPoint = new Point(int.Parse(Subparts[0]), int.Parse(Subparts[1]));
                        break;
                }
            }
            catch
            {
            }
        }

		private GraphicsPath ReadGraphicsPath(StringReader reader)
		{
			GraphicsPath Result = new GraphicsPath();
			string Line;

			while((Line = reader.ReadLine()) != null)	//trim Draw / Fill from the beginning of the command
			{
                if(IsCommentLine(Line))
                    continue;

				if(Line.Length > 0)
					IssuePathCommand(reader, Result, Line.Substring(4).ToUpper());
			}

			return Result;
		}

		private void IssuePathCommand(StringReader reader, GraphicsPath path, string command)
		{
			List<float[]> ArgumentList = new List<float[]>();

			
			int ExpectedArguments;

			//figure out the number of floats that should be in each element of ArgumentList

			#region Deduce Argument Counts

			switch(command)
			{
				case "ARC":
					ExpectedArguments = 6;
					break;

				case "BEZIER":
					ExpectedArguments = 8;
					break;

				case "CURVE":
				case "POLYGON":
                    ExpectedArguments = 2;
                    break;

				case "LINE":
				case "LINES":
				case "ELLIPSE":
				case "RECTANGLE":
				case "RECTANGLES":
					ExpectedArguments = 4;
					break;

				default:
					throw new NotSupportedException(command);
			}
			#endregion

			//read all the arguments to the command
			string Line;
			while((Line = reader.ReadLine()) != null
				&& Line != "")	//keep reading while we haven't hit the end of the file or a blank line
			{
                if(IsCommentLine(Line))
                    continue;

				string[] Parts = Line.Split(',');
				AssertFormat(Parts.Length == ExpectedArguments,
					string.Format("Invalid number of arguments to {0}.  Expected {1}; found {2}.", command, ExpectedArguments, Parts.Length));
				
				//convert the string parts to floats
				float[] fParts = new float[ExpectedArguments];
				for(int i = 0; i < ExpectedArguments; i++)
					fParts[i] = float.Parse(Parts[i]);

				ArgumentList.Add(fParts);
			}

			//issue the command

			#region Command Issuance

            switch (command)
            {
                case "CURVE":   //curve command requires an array of all points
                    path.StartFigure();
                    path.AddCurve(ArgumentList.ConvertAll(new Converter<float[], PointF>(
                        delegate(float[] p)
                        {
                            return new PointF(p[0], p[1]);
                        })).ToArray());

                    break;

                case "POLYGON": //polygon command requires an array of all points
                    path.StartFigure();
                    path.AddPolygon(ArgumentList.ConvertAll(new Converter<float[], PointF>(
                        delegate(float[] p)
                        {
                            return new PointF(p[0], p[1]);
                        })).ToArray());
                    break;

                default:
                    
                    foreach (float[] a in ArgumentList)
                    {
                        path.StartFigure();

                        //process each line of coordinates in series
                        switch (command)
                        {
                            case "ARC":
                                path.AddArc(a[0], a[1], a[2], a[3], a[4], a[5]);
                                break;
                            case "BEZIER":
                            case "BEZIERS":
                                path.AddBezier(a[0], a[1], a[2], a[3], a[4], a[5], a[6], a[7]);
                                break;
                            case "LINE":
                            case "LINES":
                                path.AddLine(a[0], a[1], a[2], a[3]);
                                break;
                            case "ELLIPSE":
                                path.AddEllipse(a[0], a[1], a[2], a[3]);
                                break;
                            case "RECTANGLE":
                            case "RECTANGLES":
                                path.AddRectangle(new RectangleF(a[0], a[1], a[2], a[3]));
                                break;
                        }
                    }
                    break;
            }

			
			#endregion
		}

        private static bool IsCommentLine(string line)
        {
            for(int i=0; i<line.Length; i++)
                if(line[i] == '%') return true;
                else if(!char.IsWhiteSpace(line[i])) return false;

            return false;
        }

		private static void AssertFormat(bool condition, string message)
		{
			if(!condition)
				throw new FormatException("Invalid script format.",
					new FormatException(message));
		}
	}
}
