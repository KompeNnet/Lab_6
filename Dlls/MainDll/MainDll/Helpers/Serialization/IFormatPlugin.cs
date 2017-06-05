namespace Lab_4.Helpers.Serialization
{
    public interface IFormatPlugin
    {
        string GetName();
        ISerializer GetSerializer();
    }
}
