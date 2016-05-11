using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class WorkflowOverviewDetailViewModel
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string TemplateName { get; set; }
        public bool IsFinished { get; set; }
        public bool IsSuccesfull { get; set; }
        public int AmountOfErrors { get; set; }
    }
}