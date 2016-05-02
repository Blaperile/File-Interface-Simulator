using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FIS.BL;

namespace File_Interface_Simulator
{
    public class DetectInput
    {
        public static void Init()
        {
            IOperationalManager opManager = new OperationalManager();
            opManager.DetectInput();
        }
    }
}