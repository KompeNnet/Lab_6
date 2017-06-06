using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab_4.Helpers.Serialization
{
    public class SerializerManager
    {
        private static readonly SerializerManager instance = new SerializerManager();

        public static SerializerManager GetInstance { get { return instance; } }

        private SerializerManager() { }

        private Dictionary<string, ISerializer> serializersDict = new Dictionary<string, ISerializer>
        {
            { "json", new JSONSerializer() }
        };

        private List<string> availableFormats = new List<string>
        {
            "json"
        };

        public List<string> GetAvailableFormats()
        {
            return availableFormats;
        }

        public void LoadFormat(string type, ISerializer serializer)
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

        public ISerializer GetSerializer(string key)
        {
            return serializersDict[key];
        }
    }
}
