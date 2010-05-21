using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ActivityMonitorService_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Server_Connection.ActivityMonitor am = new Server_Connection.ActivityMonitor();


            Server_Connection.User u = new Server_Connection.User();
            u.Username = "bh673";
            u.Password = "password";

            Server_Connection.DailyTask[] dt = am.GetTasks(u);

            for (int i = 0; i < dt.GetLength(0); i++)
                Console.WriteLine("{0}", dt[i].idDailyTask);

            Server_Connection.WalkingStats ws = new Server_Connection.WalkingStats();
            ws.idDailyTask = dt[0].idDailyTask;
            ws.steps = 259;
            ws.StartDateTime = DateTime.Now;
            ws.EndDateTime = DateTime.Now.AddHours(1);
            ws.StatsClass = "WalkingStats";

            am.UploadStats(u, ws);

        }
    }
}
