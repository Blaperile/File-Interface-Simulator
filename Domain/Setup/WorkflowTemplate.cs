using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Setup
{
    public class WorkflowTemplate
    {
        /*Contains configuration information for workflows that need to be followed*/

        public int WorkflowTemplateId { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public bool IsChosen { get; set; }
        public IEnumerable<FileSpecification> FileSpecifications { get; set; }
    }
}
