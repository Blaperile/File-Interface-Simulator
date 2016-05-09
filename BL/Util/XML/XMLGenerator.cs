using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIS.BL.Domain.Operational;

namespace FIS.BL.Util.XML
{
    public class XMLGenerator : IXMLGenerator
    {
        public string GenerateXmlString(Message message)
        {
            string xmlString = "";

            xmlString += String.Format("<header>", Environment.NewLine);

            foreach (HeaderField headerField in message.HeaderFields)
            {
                xmlString += String.Format("{0}<{1}>{2}</{1}>", Environment.NewLine, headerField.HeaderFieldCode, headerField.Description);
            }

            xmlString += String.Format("{0}</header>{0}", Environment.NewLine);

            return xmlString;
        }
    }
}
