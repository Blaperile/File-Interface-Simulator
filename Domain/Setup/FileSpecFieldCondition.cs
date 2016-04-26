using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Setup
{
    public class FileSpecFieldCondition
    {
        /*Contains validation rules for one field, uploaded from the File Specification document*/

        public int FileSpecFieldConditionId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsOptional { get; set; }
        public int Level { get; set; }
        public string Group { get; set; }
        public FieldSpecFieldCondition FieldSpecFieldCondition { get; set; }
    }
}
