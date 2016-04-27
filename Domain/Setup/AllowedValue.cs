using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Setup
{
   public class AllowedValue
    {
        public int AllowedValueId { get; set; }
        public String Value { get; set; }
        public FieldSpecFieldCondition fieldSpecFieldCondition { get; set; }
    }
}
