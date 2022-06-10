using System;
using System.Threading;
using System.Configuration;
using System.Diagnostics;

using System.IO;
using System.Net;
using System.Net.Sockets;

namespace TX_ESC_04SessionRememberV2.worker
{
    public class Worker
    {
        // indicates service status
        private bool serviceStarted = false;

        //thread id to identify in  multi thread

        private int threadid = 0;

        // the event log of service

        private EventLog serviceEventLog;

        //interval at which the task is executed

        //private int interval = Convert.ToInt32(ConfigurationSettings.AppSettings["interval"]);


        public Worker()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public Worker(int id, EventLog ServiceEventLog)
        {
            this.threadid = id;
            this.serviceEventLog = ServiceEventLog;
        }

        public void Monitor() //Created by VM 7-16-2021
        {
            while (serviceStarted)
            {
                System.Net.Sockets.TcpListener listener = null;
                try
                {

                    IPHostEntry iPAddress = Dns.GetHostByName("www.uptimerobot.com");
                    IPAddress[] IPAdd = iPAddress.AddressList;

                    string ipString = string.Empty;

                    foreach (IPAddress ip in IPAdd)
                        ipString += ip.ToString() + "|";
                    if (IPAdd != null)
                    {
                        //listener = new TcpListener(IPAddress.Parse("168.171.3.200"), 1050);

                        listener = new TcpListener(IPAddress.Any, 1140);
                        listener.Start();

                    }

                    while (true)
                    {
                        System.Diagnostics.EventLog.WriteEntry("Waiting for incoming client connectios from TX_ESC_04SessionRememberV2 Session reminder Monitor....", "");
                        TcpClient client = listener.AcceptTcpClient();
                        System.Diagnostics.EventLog.WriteEntry("Connected from TX_ESC_04SessionRememberV2 session reminder Monitor....", "");
                        //StreamReader reader = new StreamReader(client.GetStream());
                        //System.Diagnostics.EventLog.WriteEntry("Create Reader  from from Monitor....", "");

                    }
                }
                catch (Exception e)
                {
                    System.Diagnostics.EventLog.WriteEntry("Error is from listener", e.Message);
                }

            }


            Thread.CurrentThread.Abort();
        }

        public void ExecuteTask()
        {


            while (serviceStarted)
            {
                // check the current time against the last run plus interval
                //if ( ((TimeSpan) (DateTime.UtcNow.Subtract(lastRunTime))).TotalSeconds >= interval) 
                //{


                try
                {
                    if (threadid == 1)
                    {


                        DataProcess.SendEmail(Utility.MessageType.SessionReminder);
                        // DataProcess.DataProcess.SendEmail(Utility.Utility.MessageType.SessionremiderToFaci);




                    }


                    // If time to do something, do so
                    // Note: exception handling is very important here 
                    // if you dont, the error will vanish along with your worker thread

                    //serviceEventLog.WriteEntry ("Multithreaded Service; Thread" + threadid.ToString() + 
                    //	" Tick :" + DateTime.Now.ToString());    
                }
                catch (System.Exception ex)
                {
                    // add some robust logging here
                    serviceEventLog.WriteEntry("Error! " + ex.Message, System.Diagnostics.EventLogEntryType.Error);
                }

                // set new run time
                //lastRunTime = DateTime.UtcNow;
                //}

                // yield
                if (serviceStarted)
                {
                    Thread.Sleep(new TimeSpan(24, 0, 0));   		 // sleep thread for 15 minutes before wake up		 
                }
            }

            Thread.CurrentThread.Abort();
        }


        /// <summary>
        /// Flag to start/stop the processing on the thread
        /// </summary>
        public bool ServiceStarted
        {
            get
            {
                return serviceStarted;
            }
            set
            {
                serviceStarted = value;
            }
        }
    }
}
