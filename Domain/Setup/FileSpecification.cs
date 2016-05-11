using FIS.BL.Domain.Operational;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Setup
{
    public class FileSpecification
    {
        /*Contains the configuration information needed to handle incoming or outgoing messages*/
        public int FileSpecificationId { get; set; }
        public string Name { get; set; }
        public DateTime UploadDate { get; set; }
        public string Version { get; set; }
        public string Path { get; set; }
        public bool IsInput { get; set; }
        public ICollection<Directory> Directories { get; set; }
        public FieldSpecification FieldSpecification { get; set; }
        public ICollection<HeaderCondition> HeaderConditions { get; set; }
        public ICollection<GroupCondition> GroupConditions { get; set; }
        public ICollection<FileSpecFieldCondition> FileSpecFieldConditions { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<WorkflowTemplateStep> WorkflowTemplateSteps { get; set; }
        public ICollection<AnswerContent> AnswerContents { get; set; }
    }
}
