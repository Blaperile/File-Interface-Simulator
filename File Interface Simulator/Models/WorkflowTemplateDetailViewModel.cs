using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace File_Interface_Simulator.Models
{
    public class WorkflowTemplateDetailViewModel
    {
        public int WorkflowTemplateId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public string IsActive { get; set; }
        public IEnumerable<WorkflowTemplateStepDetailViewModel> CurrentWorkflowTemplateSteps { get; set; }
        public int NewSequenceNumber { get; set; }

        [Required]
        public string NewStep { get; set; }
        public IEnumerable<WorkflowTemplatePossibleFileSpecificationDetailViewModel> PossibleFileSpecifications { get; set; }
    }
}