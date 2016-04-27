using FIS.BL.Domain.Operational;
using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Util.XML
{
    public class XMLValidator : IValidator
    {
        public IEnumerable<String> Codes { get; set; }

        public XMLValidator(IEnumerable<IElement> elements, FileSpecification fileSpecification, Message message)
        {
            throw new NotImplementedException();
        }

        public IElement GetElement(string elementName)
        {
            throw new NotImplementedException();
        }

        private IElement FindElement(string elementName)
        {
            throw new NotImplementedException();
        }

        private void RemoveCodeFromList(string code)
        {
            throw new NotImplementedException();
        }
    }
}
