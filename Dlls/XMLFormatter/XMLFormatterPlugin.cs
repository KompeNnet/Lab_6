using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace Lab_4.Helpers.Formatters
{
    class XMLFormatterPlugin : IFormatterPlugin
    {
        private static string func = "Transformation";
        private XMLFormatter formatter = new XMLFormatter();
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
            MenuItem tempItem = new MenuItem() { Name = "ChosenRules", Header = "ChooseRules" };
            tempItem.Click += new RoutedEventHandler(ChooseRules);
            menuItem.Items.Add(tempItem);
            return menuItem;
        }

        private void ChooseRules(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog() { Filter = "XSL files | *.xsl" };
            if (dlg.ShowDialog() == true)
            {
                XMLFormatter formatter = new XMLFormatter();
                formatter.SetRules(dlg.FileName);
                ((MenuItem)sender).Header = "Chosen " + Path.GetFileName(dlg.FileName);
            }
        }
    }
}
