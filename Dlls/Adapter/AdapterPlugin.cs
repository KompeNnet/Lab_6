using System;
using System.Windows.Controls;

namespace Lab_4.Helpers.Formatters
{
    class AdapterPlugin : IFormatterPlugin
    {
        private static string func = "Encoding";
        private Adapter formatter = new Adapter();
        private MenuItem menuItem;

        public IFormatter GetFormatter()
        {
            return formatter;
        }

        public string GetFunc()
        {
            return func;
        }

        public MenuItem GetMenuItem()
        {
            menuItem = new MenuItem() { Name = func, Header = func };
            menuItem.Items.Add(new MenuItem() { Name = "Enable", Header = "Is enabled", IsCheckable = true });
            return menuItem;
        }
    }
}
