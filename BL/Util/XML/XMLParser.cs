using FIS.BL.Domain.Operational;
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

        public IEnumerable<IElement> GetElements(String xmlString)
        {
            List<XMLElement> elements = new List<XMLElement>();
            XDocument doc = XDocument.Parse(xmlString);
            int sequence = 0;
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
                    elements.Add(element);
                    sequence++;
                }
            }

            return elements;
        }
    }
}
