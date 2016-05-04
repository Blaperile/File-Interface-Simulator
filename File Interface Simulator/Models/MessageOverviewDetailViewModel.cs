using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class MessageOverviewDetailViewModel
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string Type { get; set; }
        public string MessageState { get; set; }
        public bool HasErrors { get; set; }
    }
}