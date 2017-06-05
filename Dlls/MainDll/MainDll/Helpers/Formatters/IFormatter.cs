namespace Lab_4.Helpers.Formatters
{
    public interface IFormatter
    {
        bool IsCompatible(string extension);
        string GetRules();
        void SetRules(string rules);
        string Format(string input);
        string ReFormat(string input);
    }
}
