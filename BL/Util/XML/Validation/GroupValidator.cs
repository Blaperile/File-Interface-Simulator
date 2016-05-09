using FIS.BL.Domain.Operational;
using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Util.XML.Validation
{
    internal class GroupValidator
    {
        internal GroupValidator(ICollection<String> Codes, IEnumerable<XMLElement> elements, FileSpecification fileSpecification, Message message)
        {
            CheckGroups(elements, fileSpecification, message, Codes);
        }

        private void CheckGroups(IEnumerable<XMLElement> elements, FileSpecification fileSpecification, Message message, ICollection<String> codes)
        {
            IEnumerable<XMLElement> groupElements = elements.Where(e => e.Code.Equals("GROUP")).Where(e => !e.Level.Equals("Header")).ToList();

            foreach (GroupCondition groupCondition in fileSpecification.GroupConditions)
            {
                IEnumerable<XMLElement> temp = groupElements.Where(e => e.Value.Equals(groupCondition.Code)).ToList();

                CheckIfCodeExist(message, groupCondition, temp);

                CheckLevelAndRange(message, groupCondition, temp, codes);

                CheckParentGroup(message, groupCondition);

            }
        }

        private static void CheckParentGroup(Message message, GroupCondition groupCondition)
        {
            Group group = message.Transactions.ElementAt(0).Groups.Where(g => g.GroupCode.Equals(groupCondition.Code)).First();

            if (String.IsNullOrEmpty(groupCondition.ParentGroup) && group.ParentGroup != null)
            {
                message.AmountOfErrors++;
                group.ErrorDescription = "This group is not allowed to have a parent group.";

            }
            if (!String.IsNullOrEmpty(groupCondition.ParentGroup) && group.ParentGroup == null)
            {
                message.AmountOfErrors++;
                group.ErrorDescription = "This group must have a parent group.";

            }
            if (!String.IsNullOrEmpty(groupCondition.ParentGroup) && group.ParentGroup != null)
            {
                if (groupCondition.ParentGroup != group.ParentGroup.GroupCode)
                {
                    message.AmountOfErrors++;
                    group.ErrorDescription = "This group has the wrong parent group.";
                }
            }

        }

        private void CheckLevelAndRange(Message message, GroupCondition groupCondition, IEnumerable<XMLElement> temp, ICollection<String> codes)
        {
            foreach (XMLElement groupElement in temp)
            {
                Group group = CheckRange(message, groupCondition, temp, groupElement, codes);
                CheckLevel(groupCondition, groupElement, group, message);
                message.Transactions.ElementAt(0).Groups.Add(group);
            }
        }

        private Group CheckRange(Message message, GroupCondition groupCondition, IEnumerable<XMLElement> temp, XMLElement groupElement, ICollection<String> codes)
        {
            Group group = new Group()
            {
                GroupCondition = groupCondition,
                GroupCode = groupCondition.Code,
                Transaction = message.Transactions.ElementAt(0),
                Level = groupElement.Level,
                Sequence = groupElement.SequenceNumber,
                Fields = new List<Field>()
            };

            if (!String.IsNullOrEmpty(groupCondition.ParentGroup) && message.Transactions.ElementAt(0).Groups.Count() > 0)
            {
                if (message.Transactions.ElementAt(0).Groups.Where(g => g.GroupCode.Equals(groupCondition.ParentGroup)).Count() > 0)
                {
                    group.ParentGroup = message.Transactions.ElementAt(0).Groups.Where(g => g.GroupCode.Equals(groupCondition.ParentGroup)).Last();
                }
            }

            codes.Remove(groupCondition.Code);

            if (temp.Count() < Int32.Parse(groupCondition.MinimumAmountOfOccurences))
            {
                message.AmountOfErrors++;
                group.ErrorDescription += "This group needs to occur at least " + groupCondition.MinimumAmountOfOccurences + " times";
            }

            if (!groupCondition.MaximumAmountOfOccurences.Equals("n") && temp.Count() > Int32.Parse(groupCondition.MaximumAmountOfOccurences))
            {
                message.AmountOfErrors++;
                group.ErrorDescription += "This group needs to occur less than " + (Int32.Parse(groupCondition.MaximumAmountOfOccurences) + 1) + " times";
            }

            return group;
        }

        private static void CheckLevel(GroupCondition groupCondition, XMLElement groupElement, Group group, Message message)
        {
            if (!groupElement.Level.Equals(groupCondition.Level))
            {
                message.AmountOfErrors++;
                group.ErrorDescription = "This group is on the wrong level.";
            }
        }

        private static void CheckIfCodeExist(Message message, GroupCondition groupCondition, IEnumerable<XMLElement> temp)
        {
            if (temp.Count() == 0 && Int32.Parse(groupCondition.MinimumAmountOfOccurences) > 0)
            {
                message.AmountOfErrors++;
                message.Transactions.ElementAt(0).GroupsErrorDescription += String.Format("Group {0} is missing." + Environment.NewLine, groupCondition.Code);
            }
        }

    }
}
