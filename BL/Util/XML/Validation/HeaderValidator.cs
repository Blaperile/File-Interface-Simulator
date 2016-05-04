using FIS.BL.Domain.Operational;
using FIS.BL.Domain.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Util.XML.Validation
{
    internal class HeaderValidator
    {

       internal HeaderValidator(ICollection<String> codes, IEnumerable<XMLElement> elements, ICollection<HeaderCondition> headerConditions, Message message)
        {
            foreach (HeaderCondition headerCondition in headerConditions)
            {
                IEnumerable<XMLElement> temp = elements.Where(e => e.Code.Equals(headerCondition.HeaderFieldCode)).Where(e => e.Level.Equals("Header")).ToList();

                HeaderField headerField = null;

                headerField = CheckAmountOfOccurencesOfHeaderField(codes, message, headerCondition, temp, headerField);
                temp = CheckContentOfHeaderField(headerCondition, temp, headerField);
                headerField = CheckDataTypeHeaderField(headerField, headerCondition);
                headerField = CheckSizeHeaderField(headerField, headerCondition);

            }

            XMLElement transactionCountElement = elements.Where(e => e.Code.Equals("TRANSACTIONCOUNT")).First();

            message.Transactions = new List<Transaction>();

            for (int i = 0; i < Int32.Parse(transactionCountElement.Value); i++)
            {
                message.Transactions.Add(new Transaction()
                {
                    Message = message,
                    Groups = new List<Group>()
                });
            }
        }

        private IEnumerable<XMLElement> CheckContentOfHeaderField(HeaderCondition headerCondition, IEnumerable<XMLElement> temp, HeaderField headerField)
        {
            if (temp.Count() > 0 && !String.IsNullOrEmpty(headerCondition.Description))
            {
                temp = temp.Where(e => e.Value.Equals(headerCondition.Description)).ToList();

                if (temp.Count() == 0)
                {
                    headerField.ErrorDescription = String.Format("Value of this field must be {0}", headerCondition.Description);
                }
            }

            return temp;
        }

        private HeaderField CheckAmountOfOccurencesOfHeaderField(ICollection<String> codes, Message message, HeaderCondition headerCondition, IEnumerable<XMLElement> temp, HeaderField headerField)
        {
            if (temp.Count() >= 1)
            {
                headerField = new HeaderField()
                {
                    HeaderFieldCode = headerCondition.HeaderFieldCode,
                    Description = temp.First().Value,
                    HeaderCondition = headerCondition,
                    Message = message
                };

                codes.Remove(headerCondition.HeaderFieldCode);
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

            return headerField;
        }

        private HeaderField CheckDataTypeHeaderField(HeaderField headerfield, HeaderCondition headercondition)
        {
            if (headercondition.Datatype != null && headerfield.Description != null)
            {
                if (headercondition.Datatype.Equals("INT"))
                {
                    bool isNumber = isNumeric(headerfield.Description, System.Globalization.NumberStyles.Integer);
                    if (isNumber == false)
                    {
                        headerfield.ErrorDescription += Environment.NewLine + "The datatype of the headerfield must be an Integer.";
                    }
                }
                if (headercondition.Datatype.Equals("DATE"))
                {
                    DateTime dateTime;
                    bool isDateTime = DateTime.TryParse(headerfield.Description, out dateTime);
                    if (isDateTime == false)
                    {
                        string dateConverted = headerfield.Description.Insert(4, "/");
                        dateConverted = dateConverted.Insert(7, "/");
                        isDateTime = DateTime.TryParse(dateConverted, out dateTime);
                        if (isDateTime == false)
                        {
                            dateConverted = headerfield.Description.Insert(3, "/");
                            dateConverted = dateConverted.Insert(5, "/");
                            isDateTime = DateTime.TryParse(dateConverted, out dateTime);
                            if (isDateTime == false)
                            {
                                headerfield.ErrorDescription += Environment.NewLine + "The value must be a date.";
                            }
                        }
                    }
                    string dateTimeFormatted = dateTime.ToString(headercondition.Format);
                    if (!dateTimeFormatted.Equals(headerfield.Description))
                    {
                        headerfield.ErrorDescription += Environment.NewLine + "The value doesn't match the format";
                    }
                }
            }
            return headerfield;
        }

        private HeaderField CheckSizeHeaderField(HeaderField headerField, HeaderCondition headerCondition)
        {
            if (headerCondition.Size != 0)
            {
                if (headerCondition.Size != headerField.Description.Length)
                {
                    headerField.ErrorDescription += Environment.NewLine + "The length of the value doesn't match the format.";
                }
            }

            return headerField;
        }

        public bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            return Double.TryParse(val, NumberStyle,
                System.Globalization.CultureInfo.CurrentCulture, out result);
        }
    }
}
