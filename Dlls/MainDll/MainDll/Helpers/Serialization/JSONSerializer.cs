using Lab_4.Books;
using Newtonsoft.Json;

namespace Lab_4.Helpers.Serialization
{
    class JSONSerializer : ISerializer
    {
        public string Serialize(Book smth)
        {
            return JsonConvert.SerializeObject(smth);
        }

        public T Deserialize<T>(string smth)
        {
            return JsonConvert.DeserializeObject<T>(smth);
        }
    }
}
