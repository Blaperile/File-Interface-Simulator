using FIS.BL.Domain.Operational;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Util.XML
{
    public class XMLParser : IParser
    {
        public IEnumerable<IElement> Elements { get; set; }

        public XMLParser(string xmlString)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IElement> GetElements()
        {
            throw new NotImplementedException();
        }
    }
}
