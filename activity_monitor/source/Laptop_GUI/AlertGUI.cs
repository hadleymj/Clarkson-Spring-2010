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
    public partial class AlertGUI : Form
    {
        public AlertGUI()
        {
            InitializeComponent();
        }

        public void setMessage(string inMessage)
        {
            this.current_alert_panel.Text = "System Alert!";
            this.alert_item.ForeColor = Color.Red;
            //this.ForeColor = Color.Red;
            this.alert_item.Text = inMessage;
        }
        public void setMessage(string inMessage, string color)
        {
            this.current_alert_panel.Text = "Great Success";
            this.alert_item.ForeColor = Color.Green;
            //this.ForeColor = Color.Green;
            this.alert_item.Text = inMessage;
        }

        private void alert_close_button_Click(object sender, EventArgs e)
        {

            this.Hide();
        }

        private void current_alert_panel_Enter(object sender, EventArgs e)
        {

        }

        private void alert_item_Click(object sender, EventArgs e)
        {

        }
    }
}
