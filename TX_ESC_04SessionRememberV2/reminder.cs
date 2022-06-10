using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using TX_ESC_04SessionRememberV2.worker;

namespace TX_ESC_04SessionRememberV2
{
    public partial class reminder : ServiceBase
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        //private System.ComponentModel.Container components = null;

        // array of worker Threads

        Thread[] workerThreads;

        // the objects that do the actual work

        Worker[] arrWorkers;

        // thread number

        int threadno = 2;//Convert.ToInt32(ConfigurationSettings.AppSettings["threadsno"]);

        public reminder()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.

            arrWorkers = new Worker[threadno];

            workerThreads = new Thread[threadno];
            for (int i = 0; i < threadno; i++)
            {
                // create an object
                
                arrWorkers[i] = new Worker(i + 1, EventLog);

                // set properties on the object
                arrWorkers[i].ServiceStarted = true;

                // create a thread and attach to the object
                ThreadStart st;
                if(i==0)
                    st = new ThreadStart(arrWorkers[i].ExecuteTask);
                else
                    st = new ThreadStart(arrWorkers[i].Monitor);

                workerThreads[i] = new Thread(st);
            }

            // start the threads
            for (int i = 0; i < threadno; i++)
            {
                workerThreads[i].Start();
            }
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.

            for (int i = 0; i < threadno; i++)
            {
                // set flag to stop worker thread.
                arrWorkers[i].ServiceStarted = false;

                // give it a little time to finish any pending work
                workerThreads[i].Join(new TimeSpan(0, 0, 3)); // after 1 min of stop time it will send out email
            }
        }
    }
}
