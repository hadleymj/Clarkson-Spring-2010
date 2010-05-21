using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WiiMoteConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Activity Monitor Super-Awesome Application that does stuff");
            WiimoteLib.Wiimote main = new WiimoteLib.Wiimote();
            main.Connect();
        }
    }
}
