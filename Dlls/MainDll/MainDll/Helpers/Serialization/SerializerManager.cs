using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab_4.Helpers.Serialization
{
    public class SerializerManager
    {
        public static Dictionary<string, ISerializer> serializersDict = new Dictionary<string, ISerializer>
        {
            { "json", new JSONSerializer() }
        };

        public static List<string> availableFormats = new List<string>
        {
            "json"
        };

        public static void LoadFormat(string type, ISerializer serializer)
        {
            try
            {
                serializersDict.Add(type, serializer);
                availableFormats.Add(type);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("It is already exists", "Can't add", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static ISerializer GetSerializer(string key)
        {
            return serializersDict[key];
        }
    }
}
