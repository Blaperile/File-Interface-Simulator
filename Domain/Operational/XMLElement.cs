using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Operational
{
    public class XMLElement: IElement
    {
        public string Code { get; set; }
        public string Value { get; set; }
        public IDictionary<String,String> Attributes { get; set; }
        public int Level { get; set; }
        public int SequenceNumber { get; set; }
    }
}
