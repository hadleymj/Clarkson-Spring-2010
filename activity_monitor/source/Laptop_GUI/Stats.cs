using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Laptop_GUI
{
    public abstract class Stats
    {

        public abstract string display();

        public abstract int getStats();

        public long start_time;
        public long end_time;


    }
}
