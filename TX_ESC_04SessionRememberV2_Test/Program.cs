using System;
using System.Collections.Generic;
using System.Text;
using TX_ESC_04SessionRememberV2;

namespace TX_ESC_04SessionRememberV2_Test
{
    class Program
    {
        static void Main(string[] args)
        {
            DataProcess.SendEmail(TX_ESC_04SessionRememberV2.Utility.MessageType.SessionReminder);
        }
    }
}
