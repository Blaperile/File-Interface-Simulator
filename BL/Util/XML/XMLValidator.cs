﻿using FIS.BL.Domain.Operational;
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

            CheckHeaderFields(elements, fileSpecification, message);

        }

        private void CheckHeaderFields(IEnumerable<XMLElement> elements, FileSpecification fileSpecification, Message message)
        {
            foreach (HeaderCondition headerCondition in fileSpecification.HeaderConditions)
            {
                IEnumerable<XMLElement> temp = elements.Where(e => e.Code.Equals(headerCondition.HeaderFieldCode)).Where(e => e.Level.Equals("Header")).ToList();

                HeaderField headerField = null;

                headerField = CheckAmountOfOccurencesOfHeaderField(message, headerCondition, temp, headerField);
                temp = CheckContentOfHeaderField(headerCondition, temp, headerField);
                headerField = CheckDataTypeHeaderField(headerField, headerCondition);
                headerField = CheckSizeHeaderField(headerField, headerCondition);

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

        private HeaderField CheckAmountOfOccurencesOfHeaderField(Message message, HeaderCondition headerCondition, IEnumerable<XMLElement> temp, HeaderField headerField)
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

            return headerField;
        }

        private HeaderField CheckDataTypeHeaderField(HeaderField headerfield, HeaderCondition headercondition)
        {
                if (headercondition.Datatype != null && headerfield.Description != null)
                {
                    if(headercondition.Datatype.Equals("INT"))
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
                        if(isDateTime == false)
                        {
                            dateConverted = headerfield.Description.Insert(3, "/");
                            dateConverted = dateConverted.Insert(5, "/");
                            isDateTime = DateTime.TryParse(dateConverted, out dateTime);
                            if (isDateTime == false) {
                                headerfield.ErrorDescription += Environment.NewLine + "The value must be a date.";
                            }
                        }
                    }
                    string dateTimeFormatted = dateTime.ToString(headercondition.Format);
                    if (!dateTimeFormatted.Equals(headerfield.Description)) {
                        headerfield.ErrorDescription += Environment.NewLine + "The value doesn't match the format";
                    }
                }
            }
            return headerfield;
        }

        private HeaderField CheckSizeHeaderField(HeaderField headerField, HeaderCondition headerCondition)
        {
            if(headerCondition.Size != 0)
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
