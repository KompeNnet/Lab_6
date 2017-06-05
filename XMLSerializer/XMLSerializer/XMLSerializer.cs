using Lab_4.Books;
using System;
using System.IO;
using System.Xml.Serialization;

namespace Lab_4.Helpers.Serialization
{
    class XMLSerializer : ISerializer
    {
        public string Serialize(Book smth)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Book), LoaderManager.GetTypes());
            StringWriter writer = new StringWriter();
            serializer.Serialize(writer, smth);
            return writer.ToString();
        }

        public T Deserialize<T>(string smth)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Book), LoaderManager.GetTypes());
            TextReader reader = new StringReader(smth);
            return (T)serializer.Deserialize(reader);
        }
    }
}
