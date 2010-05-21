namespace Laptop_GUI
{
    partial class AlertGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.current_alert_panel = new System.Windows.Forms.GroupBox();
            this.alert_close_button = new System.Windows.Forms.Button();
            this.alert_item = new System.Windows.Forms.Label();
            this.current_alert_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // current_alert_panel
            // 
            this.current_alert_panel.BackColor = System.Drawing.SystemColors.Info;
            this.current_alert_panel.Controls.Add(this.alert_close_button);
            this.current_alert_panel.Controls.Add(this.alert_item);
            this.current_alert_panel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current_alert_panel.Location = new System.Drawing.Point(12, 29);
            this.current_alert_panel.Name = "current_alert_panel";
            this.current_alert_panel.Size = new System.Drawing.Size(661, 321);
            this.current_alert_panel.TabIndex = 2;
            this.current_alert_panel.TabStop = false;
            this.current_alert_panel.Text = "System Alert";
            this.current_alert_panel.Enter += new System.EventHandler(this.current_alert_panel_Enter);
            this.CenterToScreen();
            // 
            // alert_close_button
            // 
            this.alert_close_button.Location = new System.Drawing.Point(234, 258);
            this.alert_close_button.Name = "alert_close_button";
            this.alert_close_button.Size = new System.Drawing.Size(214, 63);
            this.alert_close_button.TabIndex = 1;
            this.alert_close_button.Text = "OK";
            this.alert_close_button.UseVisualStyleBackColor = true;
            this.alert_close_button.Click += new System.EventHandler(this.alert_close_button_Click);
            // 
            // alert_item
            // 
            this.alert_item.AutoSize = true;
            this.alert_item.ForeColor = System.Drawing.Color.Crimson;
            this.alert_item.Location = new System.Drawing.Point(21, 132);
            this.alert_item.Name = "alert_item";
            this.alert_item.Size = new System.Drawing.Size(484, 31);
            this.alert_item.TabIndex = 0;
            this.alert_item.Text = "Put alert messags here........................";
            this.alert_item.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.alert_item.Click += new System.EventHandler(this.alert_item_Click);
            // 
            // AlertGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 362);
            this.ControlBox = false;
            this.Controls.Add(this.current_alert_panel);
            this.Name = "AlertGUI";
            this.Text = "Alert!";
            this.current_alert_panel.ResumeLayout(false);
            this.current_alert_panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox current_alert_panel;
        private System.Windows.Forms.Label alert_item;
        private System.Windows.Forms.Button alert_close_button;

    }
}