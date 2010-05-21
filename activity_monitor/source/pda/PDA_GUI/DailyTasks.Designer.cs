namespace PDA_GUI
{
    partial class DailyTasks
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
            this.today_task_panel = new System.Windows.Forms.Panel();
            this.task_item = new System.Windows.Forms.Label();
            this.today_task_label = new System.Windows.Forms.Label();
            this.previous_task_panel = new System.Windows.Forms.Panel();
            this.tasks_main_menu_button = new System.Windows.Forms.Button();
            this.previous_task_list_box = new System.Windows.Forms.ListBox();
            this.previous_task_label = new System.Windows.Forms.Label();
            this.today_task_panel.SuspendLayout();
            this.previous_task_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // today_task_panel
            // 
            this.today_task_panel.BackColor = System.Drawing.SystemColors.Info;
            this.today_task_panel.Controls.Add(this.task_item);
            this.today_task_panel.Controls.Add(this.today_task_label);
            this.today_task_panel.Location = new System.Drawing.Point(0, 0);
            this.today_task_panel.Name = "today_task_panel";
            this.today_task_panel.Size = new System.Drawing.Size(225, 122);
            // 
            // task_item
            // 
            this.task_item.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.task_item.Location = new System.Drawing.Point(0, 45);
            this.task_item.Name = "task_item";
            this.task_item.Size = new System.Drawing.Size(222, 68);
            this.task_item.Text = "No Task Added!";
            this.task_item.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // today_task_label
            // 
            this.today_task_label.BackColor = System.Drawing.Color.Silver;
            this.today_task_label.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.today_task_label.Location = new System.Drawing.Point(0, 0);
            this.today_task_label.Name = "today_task_label";
            this.today_task_label.Size = new System.Drawing.Size(226, 24);
            this.today_task_label.Text = "Today\'s Task";
            this.today_task_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // previous_task_panel
            // 
            this.previous_task_panel.BackColor = System.Drawing.SystemColors.Info;
            this.previous_task_panel.Controls.Add(this.tasks_main_menu_button);
            this.previous_task_panel.Controls.Add(this.previous_task_list_box);
            this.previous_task_panel.Controls.Add(this.previous_task_label);
            this.previous_task_panel.Location = new System.Drawing.Point(0, 125);
            this.previous_task_panel.Name = "previous_task_panel";
            this.previous_task_panel.Size = new System.Drawing.Size(226, 159);
            // 
            // tasks_main_menu_button
            // 
            this.tasks_main_menu_button.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.tasks_main_menu_button.Location = new System.Drawing.Point(0, 108);
            this.tasks_main_menu_button.Name = "tasks_main_menu_button";
            this.tasks_main_menu_button.Size = new System.Drawing.Size(120, 48);
            this.tasks_main_menu_button.TabIndex = 5;
            this.tasks_main_menu_button.TabStop = false;
            this.tasks_main_menu_button.Text = "Main Menu";
            this.tasks_main_menu_button.Click += new System.EventHandler(this.tasks_main_menu_button_tasks_Click);
            // 
            // previous_task_list_box
            // 
            this.previous_task_list_box.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.previous_task_list_box.Items.Add("Walking Task");
            this.previous_task_list_box.Items.Add("Running Task");
            this.previous_task_list_box.Items.Add("Diving Task");
            this.previous_task_list_box.Items.Add("Eating Task");
            this.previous_task_list_box.Location = new System.Drawing.Point(0, 36);
            this.previous_task_list_box.Name = "previous_task_list_box";
            this.previous_task_list_box.Size = new System.Drawing.Size(225, 59);
            this.previous_task_list_box.TabIndex = 4;
            this.previous_task_list_box.TabStop = false;
            // 
            // previous_task_label
            // 
            this.previous_task_label.BackColor = System.Drawing.Color.Silver;
            this.previous_task_label.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.previous_task_label.Location = new System.Drawing.Point(0, 0);
            this.previous_task_label.Name = "previous_task_label";
            this.previous_task_label.Size = new System.Drawing.Size(225, 24);
            this.previous_task_label.Text = "Previous Tasks";
            this.previous_task_label.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // DailyTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(240, 268);
            this.Controls.Add(this.previous_task_panel);
            this.Controls.Add(this.today_task_panel);
            this.Location = new System.Drawing.Point(0, 0);
            this.Menu = this.mainMenu1;
            this.Name = "DailyTasks";
            this.Text = "DailyTasks";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.today_task_panel.ResumeLayout(false);
            this.previous_task_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel today_task_panel;
        private System.Windows.Forms.Label today_task_label;
        private System.Windows.Forms.Panel previous_task_panel;
        private System.Windows.Forms.Label previous_task_label;
        private System.Windows.Forms.Button tasks_main_menu_button;
        private System.Windows.Forms.ListBox previous_task_list_box;
        private System.Windows.Forms.Label task_item;

    }
}