using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Laptop_GUI
{
    public partial class DailyTasks : Form
    {
        public DailyTasks()
        {
            InitializeComponent();
        }

        private void tasks_main_menu_button_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            
        }
        public void setStuff(MainMenu inMainPointer)//, WiiRemoteConnection inConn)
        {


            mainMenuPointer = inMainPointer;
            this.task_item.Text = mainMenuPointer.current_task;
            //uses the main menu to get/set everything

        }

        public MainMenu mainMenuPointer;
        
    }
}
