using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Laptop_GUI
{
    public abstract class DailyTask
    {
        public abstract string display();

        public abstract int getTask();
        public int DailyTaskID;
    }
}
