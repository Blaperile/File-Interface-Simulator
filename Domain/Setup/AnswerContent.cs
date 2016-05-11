using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Setup
{
    public class AnswerContent
    {
        public int AnswerContentID { get; set; }
        public string Name { get; set; }
        public DateTime UploadDate { get; set; }
        public string Path { get; set; }
        public ICollection<WorkflowTemplateStep> WorkflowTemplateSteps { get; set; }
        public FileSpecification fileSpecification { get; set; }
        public ICollection<AnswerContentLine> AnswerContentLines { get; set; }

    }
}
