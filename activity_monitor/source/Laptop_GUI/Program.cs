using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using ActivityMonitorService;

namespace Laptop_GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            /*
            Laptop_GUI.ServerConnection conn = new Laptop_GUI.ServerConnection();
            conn.connect();

            Server_Connection.DailyTask [] dts = conn.getTasks();
            conn.sendStats();
             */
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            MainMenu mainMenuDialog = new MainMenu();
            Application.Run(mainMenuDialog);
        }

    }
}
