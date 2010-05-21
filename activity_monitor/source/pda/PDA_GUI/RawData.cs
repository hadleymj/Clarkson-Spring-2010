using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace PDA_GUI
{
    public class RawData
    {
        public int accel_x, accel_y, accel_z;
        public long time;

        public RawData() { }

        public RawData(int x, int y, int z, long in_time)
        {
            time = in_time;
            accel_x = x;
            accel_y = y;
            accel_z = z;
        }
    }
}

