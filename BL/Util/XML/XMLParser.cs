﻿using FIS.BL.Domain.Operational;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Util.XML
{
    public class XMLParser : IParser
    {

        public IEnumerable<IElement> GetElements(Message message, String xmlString)
        {
            List<IElement> elements = new List<IElement>();
            XDocument doc = XDocument.Parse(xmlString);
            int sequence = 0;

            XMLElement typeElement = new XMLElement();
            typeElement.Code = "formfillXML";
            List<Domain.Operational.Attribute> typeAttributes = new List<Domain.Operational.Attribute>();
            Domain.Operational.Attribute typeAttribute = new Domain.Operational.Attribute();
            typeAttribute.Name = doc.Element("formfillXML").FirstAttribute.Name.ToString();
            typeAttribute.Value = doc.Element("formfillXML").FirstAttribute.Value.ToString();
            typeAttributes.Add(typeAttribute);
            typeElement.Attributes = typeAttributes;
            typeElement.Level = "0";
            typeElement.SequenceNumber = sequence;
            typeElement.Message = message; ;
            elements.Add(typeElement);
            sequence++;

            List <XElement> hearderElements = doc.Descendants("header").ToList();
            foreach(XElement xElement in hearderElements.Elements())
            {
                XMLElement element = new XMLElement();
                element.Code = xElement.Name.ToString();
                element.Level = "Header";
                if (xElement.HasAttributes)
                {
                    List <Domain.Operational.Attribute> attributes = new List<Domain.Operational.Attribute>();
                    foreach (var attribute in xElement.Attributes())
                    {
                        Domain.Operational.Attribute xmlAttribute = new Domain.Operational.Attribute();
                        xmlAttribute.Name = attribute.Name.ToString();
                        xmlAttribute.Value = attribute.Value;
                        xmlAttribute.xmlElement = element;
                        attributes.Add(xmlAttribute);
                    }
                    element.Attributes = attributes;
                }
                element.SequenceNumber = sequence;
                sequence++;
                element.Value = xElement.Value;
                element.Message = message;
                elements.Add(element);
            }

            List<XElement> bodyElements = doc.Descendants("body").ToList();
            int velden = 2;
            int groepen = 1;
            foreach (XElement xElement in bodyElements.Descendants())
            {
                if (xElement.Name != "record")
                {
                    XMLElement element = new XMLElement();
                    element.Code = xElement.Name.ToString();
                    if (xElement.HasAttributes)
                    {
                        List<Domain.Operational.Attribute> attributes = new List<Domain.Operational.Attribute>();
                        foreach (var attribute in xElement.Attributes())
                        {
                            Domain.Operational.Attribute xmlAttribute = new Domain.Operational.Attribute();
                            xmlAttribute.Name = attribute.Name.ToString();
                            xmlAttribute.Value = attribute.Value;
                            xmlAttribute.xmlElement = element;
                            attributes.Add(xmlAttribute);
                        }
                        element.Attributes = attributes;
                    }
                    element.SequenceNumber = sequence;
                    element.Value = xElement.Value;
                    if (xElement.Parent.HasAttributes == true)
                    {
                        if (xElement.Parent.FirstAttribute.Name == "level" && xElement.Parent.FirstAttribute.Value != groepen.ToString())
                        {
                            velden++;
                            groepen++;
                        }
                    }
                    if (xElement.Name.ToString().Equals("GROUP"))
                    {
                        element.Level = groepen.ToString();
                    }
                    else { element.Level = velden.ToString(); }
                    element.Message = message;
                    elements.Add(element);
                    sequence++;
                }
            }

            message.Elements = elements;

            return elements;
        }
    }
}
