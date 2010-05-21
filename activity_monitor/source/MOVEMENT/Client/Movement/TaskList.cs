using System;
using System.Collections.Generic;
using System.Collections;
using System.Windows.Forms;
using System.Drawing;
using System.Text;

namespace Movement.UserInterface
{
    /// <summary>
    /// Hold a list of tasks.  It takes care of spacing between tasks, and between
    /// the image and task description.  
    /// </summary>
    class TaskList
    {
        //Data members
        private Point location;
        private ArrayList tasks;
        const int spacing = 30;

        /// <summary>
        /// Constructor for TaskList with starting location as a parameter
        /// </summary>
        /// <param name="starting_location">Loction on the panel where the list should start</param>
        public TaskList(Point starting_location)
        {
            location = starting_location;
            tasks = new ArrayList();
        }


        /// <summary>
        /// Constructor for TaskList
        /// </summary>
        public TaskList()
        {
            tasks = new ArrayList();
            location = new Point(0,0);
        }
        

        /// <summary>
        /// Adds a new task to the list of tasks
        /// </summary>
        /// <param name="status_image">Image to display for the taks.  Image will appear on to the left</param>
        /// <param name="description">Task name or description</param>
        public void AddTask(Image status_image, String description)
        {
            tasks.Add(new Task(status_image, description));
        }

        public void AddTask(Image status_image, String description, int offset)
        {
            tasks.Add(new Task(status_image, description, offset));
        }


        /// <summary>
        /// Displays the list of tasks appropriately spaced
        /// </summary>
        /// <param name="container">Panel to display the list on</param>
        public void Display(Panel container)
        {
            //temporary value for location.  We don't want to lose the original value
            Point temp_location = location;

            //display all tasks on the panel passed to the method
            for (int x = 0; x < tasks.Count; x++)
            {
                //set the locations for the image and description
                ((Task)tasks[x]).Image.Location = temp_location;
                ((Task)tasks[x]).Image.Location = new Point(temp_location.X + ((Task)tasks[x]).Offset, temp_location.Y);
                ((Task)tasks[x]).Description.Location = new Point(temp_location.X + ((Task)tasks[x]).Image.Width + ((Task)tasks[x]).Offset, temp_location.Y);
                
                //add the controls to the panel
                container.Controls.Add(((Task)tasks[x]).Image);
                container.Controls.Add(((Task)tasks[x]).Description);
                
                //set the spacing for the next task
                temp_location.Y += spacing;
            }
        }

        public void ModifyImage(int task_index, Image status_image)
        {
            ((Task)tasks[task_index]).Image.Image = status_image;

        }

        public void RemoveLast()
        {
            tasks.RemoveAt(tasks.Count - 1);
        }


        /// <summary>
        /// Removes all tasks from the list
        /// </summary>
        public void Clear()
        {
            tasks.Clear();
        }
    }
}
