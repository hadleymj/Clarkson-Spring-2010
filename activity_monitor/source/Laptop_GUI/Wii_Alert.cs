using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laptop_GUI
{
    class Wii_Alert : Alert
    {
        public override void getMessage()
        {

        }


        public Wii_Alert(int inVibration_time, int inTime_to_flush, int inTone)
        {
         
            vibration_time = inVibration_time;
            time_to_flush = inTime_to_flush;
            tone = inTone;
            
        }

        
        private int vibration_time;
        private int time_to_flush;
        private int tone;
    }
}
