using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PDA_GUI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        
        static void Main()
        {

            
            MainMenu mainMenuDialog = new MainMenu();
            Application.Run(mainMenuDialog);
        }


    }
    

 
        
    
}