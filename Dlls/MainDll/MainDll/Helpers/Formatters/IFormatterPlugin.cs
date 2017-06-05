using System.Windows.Controls;

namespace Lab_4.Helpers.Formatters
{
    public interface IFormatterPlugin
    {
        IFormatter GetFormatter();
        string GetFunc();
        MenuItem GetMenuItem();
    }
}
