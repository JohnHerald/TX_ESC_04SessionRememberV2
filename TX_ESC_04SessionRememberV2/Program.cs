using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace TX_ESC_04SessionRememberV2
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new reminder() 
			};

            TX_ESC_04SessionRememberV2.DataProcess.SendEmail(TX_ESC_04SessionRememberV2.Utility.MessageType.SessionReminder);

            ServiceBase.Run(ServicesToRun);
        }
    }
}
