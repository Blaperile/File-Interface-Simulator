using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Operational
{
    public class XMLElement: IElement
    {
        public int XMLElementId { get; set; }
        public string Code { get; set; }
        public string Value { get; set; }
        public ICollection<Attribute> Attributes { get; set; }
        public String Level { get; set; }
        public int SequenceNumber { get; set; }
        public Message Message { get; set; }
    }
}
