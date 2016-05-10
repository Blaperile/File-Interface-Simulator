using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class WorkflowTemplateViewModel
    {
        [Required]
        public string Name { get; set; }
    }
}