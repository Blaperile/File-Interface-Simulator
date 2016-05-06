using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class HomeViewModel
    {
        public ICollection<String> WorkflowTemplates { get; set; }
        public string ChosenWorkflowTemplate { get; set; }
    }
}