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

            message.HeaderFields = new List<HeaderField>();

            foreach (IElement element in elements)
            {
                Codes.Add(((XMLElement)element).Code);
            }

            HeaderValidator headerValidator = new HeaderValidator(Codes, elements, fileSpecification.HeaderConditions, message);

            IEnumerable<XMLElement> groupElements = elements.Where(e => e.Code.Equals("GROUP")).Where(e => !e.Level.Equals("Header")).ToList();

            foreach (GroupCondition groupCondition in fileSpecification.GroupConditions)
            {
                IEnumerable<XMLElement> temp = groupElements.Where(e => e.Value.Equals(groupCondition.Code)).ToList();

                if (temp.Count() == 0 && Int32.Parse(groupCondition.MinimumAmountOfOccurences) > 0)
                {
                    message.Transactions.ElementAt(0).GroupsErrorDescription += String.Format("Group {0} is missing." + Environment.NewLine, groupCondition.Code);
                }

                foreach (XMLElement groupElement in temp)
                {
                    Group group = new Group()
                    {
                        GroupCondition = groupCondition,
                        GroupCode = groupCondition.Code,
                        Transaction = message.Transactions.ElementAt(0),
                        Level = groupElement.Level
                    };

                    if (!String.IsNullOrEmpty(groupCondition.ParentGroup) && message.Transactions.ElementAt(0).Groups.Count() > 0)
                    {
                        if (message.Transactions.ElementAt(0).Groups.Where(g => g.GroupCode.Equals(groupCondition.ParentGroup)).Count() > 0)
                        {
                            group.ParentGroup = message.Transactions.ElementAt(0).Groups.Where(g => g.GroupCode.Equals(groupCondition.ParentGroup)).Last();
                        }
                    }

                    Codes.Remove(groupCondition.Code);

                    if (temp.Count() < Int32.Parse(groupCondition.MinimumAmountOfOccurences))
                    {
                        group.ErrorDescription += "This group needs to occur at least " + groupCondition.MinimumAmountOfOccurences + " times";
                    }

                    if (!groupCondition.MaximumAmountOfOccurences.Equals("n") && temp.Count() > Int32.Parse(groupCondition.MaximumAmountOfOccurences))
                    {
                        group.ErrorDescription += "This group needs to occur less than " + (Int32.Parse(groupCondition.MaximumAmountOfOccurences) + 1) + " times";
                    }

                    if (!groupElement.Level.Equals(groupCondition.Level))
                    {
                        group.ErrorDescription = "This group is on the wrong level.";
                    }

                    message.Transactions.ElementAt(0).Groups.Add(group);
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
