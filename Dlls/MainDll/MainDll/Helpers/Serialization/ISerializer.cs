using Lab_4.Books;

namespace Lab_4.Helpers.Serialization
{
    public interface ISerializer
    {
        string Serialize(Book smth);

        T Deserialize<T>(string smth);
    }
}
