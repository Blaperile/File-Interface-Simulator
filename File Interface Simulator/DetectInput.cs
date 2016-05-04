using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FIS.BL;
using System.Timers;
using System.Threading.Tasks;

namespace File_Interface_Simulator
{
    public class DetectInput
    {

        private static void HandleTimer()
        {
            OperationalManager opManager = new OperationalManager();
            opManager.DetectInput();
        }

        public static void Init()
        {
            // Create a timer
            var myTimer = new System.Timers.Timer(30000);
            // Tell the timer what to do when it elapses
            myTimer.Elapsed +=  (sender, e) => HandleTimer();
            // And start it        
            myTimer.Enabled = true;  
        }
    }
}