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
            xmlString += String.Format("{0}</header>", Environment.NewLine);
            xmlString += String.Format("{0}<body>", Environment.NewLine);

            List<Group> groupsHighestLevel = message.Transactions.ElementAt(0).Groups.Where(g => g.ParentGroup == null).ToList();
            foreach (Group group in groupsHighestLevel)
            {
                xmlString = AddGroup(xmlString, group, message);
            }

            xmlString += String.Format("{0}</body>", Environment.NewLine);

            return xmlString;
        }

        private static string AddGroup(string xmlString, Group group, Message message)
        {
                xmlString += String.Format("{0}<{1}>{2}</{1}>", Environment.NewLine, "GROUP", group.GroupCode);
                foreach(Field field in group.Fields)
                {
                    xmlString += String.Format("{0}<{1}>{2}</{1}>", Environment.NewLine, field.FieldCode, field.Value);
                }
                 List<Group> groups = message.Transactions.ElementAt(0).Groups.Where(g => g.ParentGroup!=null).ToList();
                if(groups.Where(g => g.ParentGroup.Equals(group)).Count()>0)
                {
                    foreach (Group subGroup in groups.Where(g => g.ParentGroup.Equals(group)))
                    {
                    xmlString = AddGroup(xmlString, subGroup, message);
                    }
                }
            return xmlString;
        }

    }
}
