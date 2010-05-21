using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;
using System.Text;
using System.IO;
using System.Reflection;

namespace Movement.UserInterface
{
    class Task
    {
        private PictureBox status;
        private Label description;
        private int offset;     //offset in the x direction from default location

        public Task(Image status_image, String description)
        {
            //set the PictureBox properties
            status = new PictureBox();
            status.Image = status_image;
            status.SizeMode = PictureBoxSizeMode.AutoSize;
            
            //set properties for the label
            this.description = new Label();
            this.description.AutoSize = true;
            this.description.Text = description;
            this.description.Height = status.Height;
            this.description.TextAlign = ContentAlignment.MiddleLeft;
            this.description.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            

            offset = 0;
        }

        public Task(Image status_image, String description, int offset) : this(status_image, description)
        {
            this.offset = offset;
        }

        /// <summary>
        /// Image property
        /// </summary>
        public PictureBox Image        
        {
            get{ return status; }
            set{ status = value;}
        }

        /// <summary>
        /// Description property
        /// </summary>
        public Label Description
        {
            get{ return description;}
            set{ description = value;}
        }


        /// <summary>
        /// Offset property
        /// </summary>
        public int Offset 
        {
            get { return offset; }
            set { offset = value; }        
        }
    }
}
