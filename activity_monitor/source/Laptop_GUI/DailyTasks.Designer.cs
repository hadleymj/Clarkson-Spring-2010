namespace Laptop_GUI
{
    partial class DailyTasks
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
            this.current_task_panel = new System.Windows.Forms.GroupBox();
            this.task_item = new System.Windows.Forms.Label();
            this.prev_task_panel = new System.Windows.Forms.GroupBox();
            this.prev_task_listbox = new System.Windows.Forms.ListBox();
            this.tasks_main_menu_button = new System.Windows.Forms.Button();
            this.current_task_panel.SuspendLayout();
            this.prev_task_panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // current_task_panel
            // 
            this.current_task_panel.BackColor = System.Drawing.SystemColors.Info;
            this.current_task_panel.Controls.Add(this.task_item);
            this.current_task_panel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.current_task_panel.Location = new System.Drawing.Point(106, 30);
            this.current_task_panel.Name = "current_task_panel";
            this.current_task_panel.Size = new System.Drawing.Size(430, 199);
            this.current_task_panel.TabIndex = 1;
            this.current_task_panel.TabStop = false;
            this.current_task_panel.Text = "Today\'s Task";
            // 
            // task_item
            // 
            this.task_item.AutoSize = true;
            this.task_item.Location = new System.Drawing.Point(59, 95);
            this.task_item.Name = "task_item";
            this.task_item.Size = new System.Drawing.Size(203, 31);
            this.task_item.TabIndex = 0;
            this.task_item.Text = "No task loaded!";
            // 
            // prev_task_panel
            // 
            this.prev_task_panel.BackColor = System.Drawing.SystemColors.Info;
            this.prev_task_panel.Controls.Add(this.prev_task_listbox);
            this.prev_task_panel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.prev_task_panel.Location = new System.Drawing.Point(106, 253);
            this.prev_task_panel.Name = "prev_task_panel";
            this.prev_task_panel.Size = new System.Drawing.Size(430, 202);
            this.prev_task_panel.TabIndex = 2;
            this.prev_task_panel.TabStop = false;
            this.prev_task_panel.Text = "Previous Tasks";
            // 
            // prev_task_listbox
            // 
            this.prev_task_listbox.FormattingEnabled = true;
            this.prev_task_listbox.ItemHeight = 31;
            this.prev_task_listbox.Items.AddRange(new object[] {
            "Walking Task",
            "Running Task",
            "Arm Raising Task"});
            this.prev_task_listbox.Location = new System.Drawing.Point(0, 73);
            this.prev_task_listbox.Name = "prev_task_listbox";
            this.prev_task_listbox.Size = new System.Drawing.Size(430, 128);
            this.prev_task_listbox.TabIndex = 1;
            // 
            // tasks_main_menu_button
            // 
            this.tasks_main_menu_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 32F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tasks_main_menu_button.Location = new System.Drawing.Point(-1, 480);
            this.tasks_main_menu_button.Name = "tasks_main_menu_button";
            this.tasks_main_menu_button.Size = new System.Drawing.Size(315, 141);
            this.tasks_main_menu_button.TabIndex = 6;
            this.tasks_main_menu_button.Text = "Main Menu";
            this.tasks_main_menu_button.UseVisualStyleBackColor = true;
            this.tasks_main_menu_button.Click += new System.EventHandler(this.tasks_main_menu_button_Click);
            // 
            // DailyTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 615);
            this.Controls.Add(this.tasks_main_menu_button);
            this.Controls.Add(this.prev_task_panel);
            this.Controls.Add(this.current_task_panel);
            this.Name = "DailyTasks";
            this.Text = "Daily Tasks";
            this.current_task_panel.ResumeLayout(false);
            this.current_task_panel.PerformLayout();
            this.prev_task_panel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox current_task_panel;
        private System.Windows.Forms.Label task_item;
        private System.Windows.Forms.GroupBox prev_task_panel;
        private System.Windows.Forms.ListBox prev_task_listbox;
        private System.Windows.Forms.Button tasks_main_menu_button;
    }
}