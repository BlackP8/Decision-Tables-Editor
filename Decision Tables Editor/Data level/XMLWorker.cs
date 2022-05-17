using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace Decision_Tables_Editor.Data_level
{
    class XMLWorker
    {
        internal Data Data
        {
            get => default;
            set
            {
            }
        }

        public XmlDocument toXmlDocument(XDocument xDocument)
        {
            var xmlDocument = new XmlDocument();
            using (var xmlReader = xDocument.CreateReader())
            {
                xmlDocument.Load(xmlReader);
            }
            return xmlDocument;
        }

        public XDocument toXDocument(XmlDocument xmlDocument)
        {
            return XDocument.Parse(xmlDocument.OuterXml);
        }

        public XmlDocument createEmptyXmlDocument()
        {
            //  Return an empty XmlDocument if the open file window was closed
            XmlDocument emptyMyStream = new XmlDocument();
            return emptyMyStream;
        }
    }
}
