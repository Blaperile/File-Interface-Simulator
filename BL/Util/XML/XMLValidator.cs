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

        public XMLValidator(IEnumerable<XMLElement> elements, FileSpecification fileSpecification, Message message)
        {
            Codes = new List<String>();

            message.HeaderFields = new List<HeaderField>();

            foreach (IElement element in elements)
            {
                Codes.Add(((XMLElement)element).Code);
            }

            foreach (HeaderCondition headerCondition in fileSpecification.HeaderConditions)
            {
                IEnumerable<XMLElement> temp = elements.Where(e => e.Code.Equals(headerCondition.HeaderFieldCode)).Where(e => e.Level.Equals("Header")).ToList();

                HeaderField headerField = null;

                if (temp.Count() >= 1)
                {
                    headerField = new HeaderField()
                    {
                        HeaderFieldCode = headerCondition.HeaderFieldCode,
                        Description = headerCondition.Description,
                        HeaderCondition = headerCondition,
                        Message = message
                    };

                    Codes.Remove(headerCondition.HeaderFieldCode);
                    message.HeaderFields.Add(headerField);
                }

                if (temp.Count() > 1)
                {
                    headerField.ErrorDescription = "There can be only one occurence of every headerfield code.";
                }
                else if (temp.Count() == 0)
                {
                    message.HeaderErrorDescription += String.Format("Headerfield {0} is missing from this message.", headerCondition.HeaderFieldCode);
                }

                if (temp.Count() > 0 && !String.IsNullOrEmpty(headerCondition.Description))
                {
                    temp = temp.Where(e => e.Value.Equals(headerCondition.Description)).ToList();

                    if (temp.Count() == 0)
                    {
                        headerField.ErrorDescription = String.Format("Value of this field must be {0}", headerCondition.Description);
                    }
                }
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
