using FIS.BL.Domain.Operational;
using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Util.XML.Validation
{
    internal class FieldValidator
    {
        internal FieldValidator(ICollection<String> codes, IEnumerable<XMLElement> elements, FileSpecification fileSpecification, Message message)
        {
            IEnumerable<XMLElement> fieldElements = elements.Where(e => !e.Level.Equals("Header")).Where(e => !e.Code.Equals("GROUP")).ToList();

            CheckFields(fileSpecification, message, fieldElements, codes);
        }

        private void CheckFields(FileSpecification fileSpecification, Message message, IEnumerable<XMLElement> fieldElements, ICollection<String> codes)
        {
            CheckIfMandatoryFieldExists(fileSpecification, message, fieldElements);
            AddFieldandCheckIfOnlyOccursOnce(fileSpecification, message, fieldElements, codes);
            CheckIFGroupContainsAllFields(fileSpecification, message);

        }

        private Field CheckDataTypeOfField(Field field, FieldSpecFieldCondition fieldCondition, Message message)
        {
            if (fieldCondition.Datatype.Equals("INT"))
            {
                bool isNumber = isNumeric(field.Value, System.Globalization.NumberStyles.Integer);
                if (isNumber == false)
                {
                    message.AmountOfErrors++;
                    field.ErrorDescription += Environment.NewLine + "The datatype of the field must be an Integer.";
                }
            }
            else if (fieldCondition.Datatype.Equals("DEC"))
            {
                bool isNumber = isNumeric(field.Value, System.Globalization.NumberStyles.Float);
                if (isNumber == false)
                {
                    message.AmountOfErrors++;
                    field.ErrorDescription += Environment.NewLine + "The datatype of the field must be a Decimal.";
                }
            }
            else if (fieldCondition.Datatype.Equals("DATE"))
            {
                DateTime dateTime;
                bool isDateTime = DateTime.TryParse(field.Value, out dateTime);
                if (!isDateTime)
                {
                    string dateConverted = field.Value.Insert(4, "/");
                    dateConverted = dateConverted.Insert(7, "/");
                    isDateTime = DateTime.TryParse(dateConverted, out dateTime);
                    if (isDateTime == false)
                    {
                        dateConverted = field.Value.Insert(3, "/");
                        dateConverted = dateConverted.Insert(5, "/");
                        isDateTime = DateTime.TryParse(dateConverted, out dateTime);
                        if (isDateTime == false)
                        {
                            message.AmountOfErrors++;
                            field.ErrorDescription += Environment.NewLine + "The value must be a date.";
                        }
                    }
                }
                string dateTimeFormatted = dateTime.ToString(fieldCondition.Format);
                if (!dateTimeFormatted.Equals(field.Value))
                {
                    message.AmountOfErrors++;
                    field.ErrorDescription += Environment.NewLine + "The value doesn't match the format";
                }
            }

            return field;
        }

        private bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }

        private Field CheckSizeOfField(Field field, FieldSpecFieldCondition fieldCondition, Message message)
        {
            if (fieldCondition.Size != 0)
            {
                if (fieldCondition.Size != field.Value.Length)
                {
                    message.AmountOfErrors++;
                    field.ErrorDescription += Environment.NewLine + "The length of the value doesn't match the required length.";
                }
            }

            return field;
        }

        private void CheckIFGroupContainsAllFields(FileSpecification fileSpecification, Message message)
        {
            IEnumerable<Group> groups = message.Transactions.ElementAt(0).Groups.Where(g => message.Transactions.ElementAt(0).Groups.Where(gr => gr.GroupCode.Equals(g.GroupCode)).Count() > 1);
            foreach (Group group in groups)
            {
                foreach (FileSpecFieldCondition fileSpecFieldCondition in fileSpecification.GroupConditions.Where(g => g.Code.Equals(group.GroupCode)).First().FileSpecFieldConditions.Where(f => f.IsOptional == false))
                {

                    if (message.Transactions.ElementAt(0).Groups.Where(g => g.Equals(group)).First().Fields.Where(f => f.FieldCode.Equals(fileSpecFieldCondition.Code)).Count() == 0)
                    {
                        message.AmountOfErrors++;
                        group.ErrorDescription += String.Format("Field {0} is missing." + Environment.NewLine, fileSpecFieldCondition.Code);
                    }
                }
            }
        }

        private void AddFieldandCheckIfOnlyOccursOnce(FileSpecification fileSpecification, Message message, IEnumerable<XMLElement> fieldElements, ICollection<String> codes)
        {
            foreach (FileSpecFieldCondition fileSpecfieldCondition in fileSpecification.FileSpecFieldConditions)
            {

                IEnumerable<XMLElement> temp = fieldElements.Where(e => e.Code.Equals(fileSpecfieldCondition.Code)).ToList();
                foreach (XMLElement element in temp)
                {
                    Field field;
                    Group group;
                    AddField(message, fileSpecfieldCondition, element, out field, out group, codes);
                    CheckIfFieldOnlyOccursOnce(message, fileSpecfieldCondition, field, group);
                    CheckDataTypeOfField(field, fileSpecfieldCondition.FieldSpecFieldCondition, message);
                    CheckSizeOfField(field, fileSpecfieldCondition.FieldSpecFieldCondition, message);
                    CheckAllowedValuesOfField(field, fileSpecfieldCondition.FieldSpecFieldCondition, message);
                    CheckLevelOfField(fileSpecfieldCondition, field, message);
                    CheckGroupOfField(fileSpecfieldCondition, field, message);
                }
            }
        }

        private void CheckGroupOfField(FileSpecFieldCondition fileSpecfieldCondition, Field field, Message message)
        {
            if (!field.Group.GroupCode.Equals(fileSpecfieldCondition.Group.Code))
            {
                message.AmountOfErrors++;
                field.ErrorDescription += String.Format("This field should be in group {0} instead of group {1}", fileSpecfieldCondition.Group.Code, field.Group.GroupCode);
            }
        }

        private void CheckLevelOfField(FileSpecFieldCondition fileSpecfieldCondition, Field field, Message message)
        {
            if (fileSpecfieldCondition.Level != Int32.Parse(field.Level))
            {
                message.AmountOfErrors++;
                field.ErrorDescription += String.Format("{0}This field should be on level {1} instead of level {2}", Environment.NewLine, fileSpecfieldCondition.Level, field.Level);
            }
        }

        private void CheckAllowedValuesOfField(Field field, FieldSpecFieldCondition fieldCondition, Message message)
        {
            if (fieldCondition.AllowedValues.Where(av => !String.IsNullOrEmpty(av.Value)).Count() > 0)
            {
                bool fieldHasAllowedValue = false;

                foreach (AllowedValue allowedValue in fieldCondition.AllowedValues)
                {
                    if (allowedValue.Value.Equals(field.Value))
                    {
                        fieldHasAllowedValue = true;
                    }
                }

                if (!fieldHasAllowedValue)
                {
                    message.AmountOfErrors++;
                    field.ErrorDescription += Environment.NewLine + "The value of this field is not an allowed value.";
                }
            }
        }

        private void CheckIfMandatoryFieldExists(FileSpecification fileSpecification, Message message, IEnumerable<XMLElement> fieldElements)
        {
            foreach (FileSpecFieldCondition fileSpecfieldCondition in fileSpecification.FileSpecFieldConditions.Where(f => f.IsOptional == false))
            {
                IEnumerable<XMLElement> temp = fieldElements.Where(e => e.Code.Equals(fileSpecfieldCondition.Code)).ToList();

                if (temp.Count() == 0)
                {
                    message.AmountOfErrors++;
                    message.Transactions.ElementAt(0).Groups.Where(g => g.GroupCode.Equals(fileSpecfieldCondition.Group.Code)).First().ErrorDescription += String.Format("Field {0} is missing." + Environment.NewLine, fileSpecfieldCondition.Code);
                }
            }
        }

        private void AddField(Message message, FileSpecFieldCondition fileSpecfieldCondition, XMLElement element, out Field field, out Group group, ICollection<String> codes)
        {
            field = new Field()
            {
                FieldCode = element.Code,
                Value = element.Value,
                Level = element.Level,
                FileSpecFieldCondition = fileSpecfieldCondition
            };
            List<Group> potentialGroups = message.Transactions.ElementAt(0).Groups.ToList();

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

            codes.Remove(field.FieldCode);
        }

        private void CheckIfFieldOnlyOccursOnce(Message message, FileSpecFieldCondition fileSpecfieldCondition, Field field, Group group)
        {
            if (message.Transactions.ElementAt(0).Groups.Where(g => g.Sequence == group.Sequence).First().Fields.Where(f => f.FieldCode.Equals(field.FieldCode)).Count() == 0)
            {
                message.Transactions.ElementAt(0).Groups.Where(g => g.Sequence == group.Sequence).First().Fields.Add(field);
            }
            else
            {
                message.AmountOfErrors++;
                message.Transactions.ElementAt(0).Groups.Where(g => g.Sequence == group.Sequence).First().ErrorDescription += String.Format("Field {0} can only occur once in this group." + Environment.NewLine, fileSpecfieldCondition.Code);
            }
        }
    }
}
