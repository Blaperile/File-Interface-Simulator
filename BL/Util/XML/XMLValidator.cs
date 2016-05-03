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
        public ICollection<String> Codes { get; set; }

        public XMLValidator(IEnumerable<IElement> elements, FileSpecification fileSpecification, Message message)
        {
            Codes = new List<String>();

            foreach (IElement element in elements)
            {
                Codes.Add(((XMLElement)element).Code);
            }
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
