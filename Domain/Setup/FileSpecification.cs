using System;
using System.Collections.Generic;
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
        public Directory InDirectory { get; set; }
        public Directory ArchiveDirectory { get; set; }
        public Directory ErrorDirectory { get; set; }
        public Directory OutDirectory { get; set; }
        public WorkflowTemplate WorkflowTemplate { get; set; }
        public FieldSpecification FieldSpecification { get; set; }
        public IEnumerable<HeaderCondition> HeaderConditions { get; set; }
        public IEnumerable<GroupCondition> GroupConditions { get; set; }
        public IEnumerable<FileSpecFieldCondition> FileSpecFieldConditions { get; set; }
    }
}
