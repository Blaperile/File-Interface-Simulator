using FIS.BL.Domain.Operational;
using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Util.XML.Validation
{
    public class XMLValidator : IValidator
    {
        public ICollection<String> Codes { get; set; }

        public XMLValidator(IEnumerable<XMLElement> elements, FileSpecification fileSpecification, Message message)
        {
            Codes = new List<String>();

            foreach (IElement element in elements)
            {
                Codes.Add(((XMLElement)element).Code);
            }

            HeaderValidator headerValidator = new HeaderValidator(Codes, elements, fileSpecification.HeaderConditions, message);
            GroupValidator groupValidator = new GroupValidator(Codes, elements, fileSpecification, message);
            FieldValidator fieldValidator = new FieldValidator(Codes, elements, fileSpecification, message);        
        }
    }
}
