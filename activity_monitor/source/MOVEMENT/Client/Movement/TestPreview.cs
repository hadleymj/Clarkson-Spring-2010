using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Movement.UserInterface.movement_web_service;

namespace Movement.UserInterface
{
    public partial class TestPreview : Form
    {
        public TestPreview(ListBox.SelectedObjectCollection tests)
        {
            InitializeComponent();
             
            Script temp;
            Test t = new Test();
            TestEngine.Testing.TestScript p = new global::Movement.TestEngine.Testing.TestScript();
            TestEngine.Testing.TestScriptPreviewControl script_preview;
            const int MAX_PER_ROW = 4;
            const int MAX_PER_COL = 4;

            for (int x = 0; x < tests.Count; x++)
            {
                temp = t.GetScript((ScriptInfo)tests[x]);
                if (temp == null)
                {
                    MessageBox.Show(t.Error, "Error Creating Preview", MessageBoxButtons.OK, t.ErrorType);
                    return;
                }

                p.ScriptBody = temp.scriptData;
                p.TestScriptID = temp.scriptID;

                script_preview = new global::Movement.TestEngine.Testing.TestScriptPreviewControl();
                script_preview.Script = p;
                script_preview.Location = new Point((x % MAX_PER_ROW) * 135, ((x / MAX_PER_COL) * 135) + 8);

                Label script_label = new Label();
                script_label.Text = ((ScriptInfo)tests[x]).name;
                script_label.Location = new Point(script_preview.Location.X, script_preview.Location.Y - 8);
                script_label.AutoSize = true;

                this.Controls.Add(script_label);
                this.Controls.Add(script_preview);
            }
        }
    }
}