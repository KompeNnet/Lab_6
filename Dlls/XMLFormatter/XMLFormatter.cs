using System.IO;
using System.Xml;
using System.Xml.Xsl;

namespace Lab_4.Helpers.Formatters
{
    public class XMLFormatter : IFormatter
    {
        private static string formatterRules = "";
        private string extension = ".xml";

        public bool IsCompatible(string extension)
        {
            return extension == this.extension ? true : false;
        }

        public string GetRules()
        {
            return formatterRules;
        }

        public void SetRules(string rules)
        {
            formatterRules = rules;
        }

        public string Format(string input)
        {
            XslCompiledTransform myXslTransform = new XslCompiledTransform();
            myXslTransform.Load(formatterRules);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(input);

            TextWriter text = new StringWriter();

            myXslTransform.Transform(xmlDoc, null, text);
            return text.ToString();
        }

        public string ReFormat(string input)
        {
            XslTransform myXslTransform;
            myXslTransform = new XslTransform();
            myXslTransform.Load(formatterRules);
            myXslTransform.Transform(input, "ISBNBookList.xml");
            return "";
        }
    }
}
