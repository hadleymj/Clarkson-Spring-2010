namespace PDA_GUI
{
    partial class AlertGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.alert_item = new System.Windows.Forms.Label();
            this.current_alert_label = new System.Windows.Forms.Label();
            this.alert_close_button = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // alert_item
            // 
            this.alert_item.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.alert_item.ForeColor = System.Drawing.Color.Red;
            this.alert_item.Location = new System.Drawing.Point(12, 24);
            this.alert_item.Name = "alert_item";
            this.alert_item.Size = new System.Drawing.Size(218, 98);
            this.alert_item.Text = "Put alert messages here.....";
            this.alert_item.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // current_alert_label
            // 
            this.current_alert_label.BackColor = System.Drawing.Color.Silver;
            this.current_alert_label.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.current_alert_label.Location = new System.Drawing.Point(0, 0);
            this.current_alert_label.Name = "current_alert_label";
            this.current_alert_label.Size = new System.Drawing.Size(237, 24);
            this.current_alert_label.Text = "System Alert";
            this.current_alert_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // alert_close_button
            // 
            this.alert_close_button.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.alert_close_button.Location = new System.Drawing.Point(56, 125);
            this.alert_close_button.Name = "alert_close_button";
            this.alert_close_button.Size = new System.Drawing.Size(120, 48);
            this.alert_close_button.TabIndex = 4;
            this.alert_close_button.Text = "OK";
            this.alert_close_button.Click += new System.EventHandler(this.alert_close_button_Click);
            // 
            // AlertGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.ControlBox = false;
            this.Controls.Add(this.current_alert_label);
            this.Controls.Add(this.alert_item);
            this.Controls.Add(this.alert_close_button);
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "AlertGUI";
            this.Text = "AlertGUI";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label alert_item;
        private System.Windows.Forms.Label current_alert_label;
        private System.Windows.Forms.Button alert_close_button;
    }
}