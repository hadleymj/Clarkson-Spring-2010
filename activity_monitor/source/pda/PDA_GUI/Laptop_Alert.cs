using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laptop_GUI
{
    class Laptop_Alert:Alert
    {
        public override void getMessage()
        {
            

        }

        public Laptop_Alert(String inMessage)
        {
            message = inMessage;
        }
        private string message;
    }
}
