using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PDA_GUI
{
    public abstract class Connection
    {
        public  abstract void connect();

        public abstract void disconnect();
    }
}
