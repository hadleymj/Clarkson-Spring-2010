using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Laptop_GUI
{
    public class WalkingStats : Stats
    {
        public int steps;
        public long start_time;
        public long end_time;


        public WalkingStats(int inSteps, long S_time, long E_time)
        {
            steps = inSteps;
            start_time = S_time;
            end_time = E_time;
        }
        public override string display()
        {
            return "Steps in " + (end_time - start_time) + " seconds is " + steps;
        }
        public override int getStats()//returns steps i guess
        {
            return steps;
        }


    }
}
