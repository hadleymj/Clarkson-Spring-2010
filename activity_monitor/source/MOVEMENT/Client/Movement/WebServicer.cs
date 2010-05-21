using System;
using System.Collections.Generic;
using System.Text;
using Movement.UserInterface.movement_web_service;

namespace Movement.UserInterface
{
    public class WebServicer
    {
        static Service servicer;

        static WebServicer()
        {
            servicer = new Service();
            servicer.CookieContainer = new System.Net.CookieContainer();
        }

        public Service Servicer
        {
            get
            {
                return servicer;
            }
        }
    }
}
