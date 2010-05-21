using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Movement.Scripting;

namespace Movement.TestEngine.Testing
{
    public partial class CognitiveMapForm : Form
    {
        public CognitiveMapForm()
        {
            InitializeComponent();

            //find and position to the second display, maximized
            Screen SecondScreen = null;
            foreach (Screen s in Screen.AllScreens)
            {
                if (s == Screen.PrimaryScreen) continue;
                else
                {
                    SecondScreen = s;
                    break;
                }
            }
            if (SecondScreen != null)
            {
                SetDesktopLocation(SecondScreen.Bounds.Left, SecondScreen.Bounds.Top);
                //this.Size = SecondScreen.Bounds.Size;                
                WindowState = FormWindowState.Maximized;
            }
        }

        public void RunScript(Capture.InkInputControl input, ScriptEngine scriptEngine)
        {
            mCognitiveMapControl.InputControl = input;
            mCognitiveMapControl.RunScript(scriptEngine);
        }
    }
}