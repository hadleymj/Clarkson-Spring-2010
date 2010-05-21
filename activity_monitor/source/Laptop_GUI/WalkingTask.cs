using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Laptop_GUI
{
    class WalkingTask : DailyTask
    {
        public override string display()
        {

            return "Walk " + steps.ToString() + " steps";

        }

        public override int getTask()
        {
            return steps;
        }

        public WalkingTask(int inSteps)
        {
            steps = inSteps;
        }

        private int steps;
    }
}
