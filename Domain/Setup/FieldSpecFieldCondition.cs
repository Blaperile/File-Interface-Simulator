using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Setup
{
    public class FieldSpecFieldCondition
    {
        /*Contains validation rules for one field, uploaded from the Field Specification document*/

        public int FieldSpecFieldConditionId { get; set; }
        public string FieldCode { get; set; }
        public string Datatype { get; set; }
        public int Size { get; set; }
        public string Format { get; set; }
        public IEnumerable<String> AllowedValues { get; set; }
    }
}
