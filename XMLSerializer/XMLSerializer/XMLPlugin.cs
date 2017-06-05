using Lab_4.Helpers.Serialization;

namespace Lab_4
{
    public class XMLSerializerPlugin : IFormatPlugin
    {
        XMLSerializer serializer = new XMLSerializer();
        string type = "xml";

        public string GetName()
        {
            return type;
        }

        public ISerializer GetSerializer()
        {
            return serializer;
        }
    }
}
