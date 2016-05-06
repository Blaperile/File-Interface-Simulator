using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class WorkflowDetailViewModel
    {
        public int Id { get; set; }
        public DateTime CreationDate { get; set; }
        public string TemplateWorkflow { get; set; }
        public int ErrorCount { get; set; }
        public string Successful { get; set; }
        public ICollection<WorkflowDetailMessageDetailViewModel> Messages { get; set; }
    }
}