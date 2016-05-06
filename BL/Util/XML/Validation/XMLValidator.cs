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

            IEnumerable<XMLElement> fieldElements = elements.Where(e => !e.Level.Equals("Header")).Where(e => !e.Code.Equals("GROUP")).ToList();

            CheckFields(fileSpecification, message, fieldElements);

            // IEnumerable<Group> groups = message.Transactions.ElementAt(0).Groups.Where(g => g.GroupCode.Count() > 1);
          
        }

        private static void CheckFields(FileSpecification fileSpecification, Message message, IEnumerable<XMLElement> fieldElements)
        {
            CheckIfMandatoryFieldExists(fileSpecification, message, fieldElements);
            AddFieldandCheckIfOnlyOccursOnce(fileSpecification, message, fieldElements);
            CheckIFGroupContainsAllFields(fileSpecification, message);

        }

        private static void CheckIFGroupContainsAllFields(FileSpecification fileSpecification, Message message)
        {
            IEnumerable<Group> groups = message.Transactions.ElementAt(0).Groups.Where(g => message.Transactions.ElementAt(0).Groups.Where(gr => gr.GroupCode.Equals(g.GroupCode)).Count() > 1);
            foreach (Group group in groups)
            {
                foreach (FileSpecFieldCondition fileSpecFieldCondition in fileSpecification.GroupConditions.Where(g => g.Code.Equals(group.GroupCode)).First().FileSpecFieldConditions.Where(f => f.IsOptional == false))
                {

                    if (message.Transactions.ElementAt(0).Groups.Where(g => g.Equals(group)).First().Fields.Where(f => f.FieldCode.Equals(fileSpecFieldCondition.Code)).Count() == 0)
                    {

                        group.ErrorDescription += String.Format("Field {0} is missing." + Environment.NewLine, fileSpecFieldCondition.Code);
                    }
                }
            }
        }

        private static void AddFieldandCheckIfOnlyOccursOnce(FileSpecification fileSpecification, Message message, IEnumerable<XMLElement> fieldElements)
        {
            foreach (FileSpecFieldCondition fileSpecfieldCondition in fileSpecification.FileSpecFieldConditions)
            {

                IEnumerable<XMLElement> temp = fieldElements.Where(e => e.Code.Equals(fileSpecfieldCondition.Code)).ToList();
                foreach (XMLElement element in temp)
                {
                    Field field;
                    Group group;
                    AddField(message, fileSpecfieldCondition, element, out field, out group);
                    CheckIfFieldOnlyOccursOnce(message, fileSpecfieldCondition, field, group);
                }
            }
        }

        private static void CheckIfMandatoryFieldExists(FileSpecification fileSpecification, Message message, IEnumerable<XMLElement> fieldElements)
        {
            foreach (FileSpecFieldCondition fileSpecfieldCondition in fileSpecification.FileSpecFieldConditions.Where(f => f.IsOptional == false))
            {
                IEnumerable<XMLElement> temp = fieldElements.Where(e => e.Code.Equals(fileSpecfieldCondition.Code)).ToList();

                if (temp.Count() == 0)
                {
                    message.Transactions.ElementAt(0).Groups.Where(g => g.GroupCode.Equals(fileSpecfieldCondition.Group.Code)).First().ErrorDescription += String.Format("Field {0} is missing." + Environment.NewLine, fileSpecfieldCondition.Code);
                }
            }
        }

        private static void AddField(Message message, FileSpecFieldCondition fileSpecfieldCondition, XMLElement element, out Field field, out Group group)
        {
            field = new Field()
            {
                FieldCode = element.Code,
                Value = element.Value,
                Level = element.Level,
                FileSpecFieldCondition = fileSpecfieldCondition
            };
            List<Group> potentialGroups = message.Transactions.ElementAt(0).Groups.Where(g => g.GroupCode.Equals(fileSpecfieldCondition.Group.Code)).ToList();

            group = new Group();
            foreach (Group groupP in potentialGroups)
            {
                if (groupP.Sequence < element.SequenceNumber)
                {
                    if (groupP.Sequence > group.Sequence || group == null)
                    {
                        group = groupP;
                    }
                }
            }
            field.Group = group;
        }

        private static void CheckIfFieldOnlyOccursOnce(Message message, FileSpecFieldCondition fileSpecfieldCondition, Field field, Group group)
        {
            if (message.Transactions.ElementAt(0).Groups.Where(g => g.Sequence == group.Sequence).First().Fields.Where(f => f.FieldCode.Equals(field.FieldCode)).Count() == 0)
            {
                message.Transactions.ElementAt(0).Groups.Where(g => g.Sequence == group.Sequence).First().Fields.Add(field);
            }
            else
            {
                message.Transactions.ElementAt(0).Groups.Where(g => g.Sequence == group.Sequence).First().ErrorDescription += String.Format("Field {0} can only occur once in this group." + Environment.NewLine, fileSpecfieldCondition.Code);
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
