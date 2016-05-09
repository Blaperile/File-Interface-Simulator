using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Setup
{
    public class WorkflowTemplateStep
    {
        public int WorkflowTemplateStepId { get; set; }
        public int StepNumber { get; set; }
        public FileSpecification fileSpecification { get; set; }
        public WorkflowTemplate WorkflowTemplate { get; set; }
    }
}
