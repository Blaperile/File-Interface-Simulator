using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Setup
{
    public class FieldSpecification: Specification
    {
        /*Represents the Field Specification document, a long document containing detailed validation information about fields*/

        public IEnumerable<FieldSpecFieldCondition> FieldSpecFieldConditions { get; set; }
    }
}
