using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Operational
{
    public class Attribute
    {
        public int AttributeId { get; set; }
        public String Name { get; set; }
        public String Value { get; set; }
        public XMLElement xmlElement { get; set;  }
    }
}
