using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Lab_4.Helpers.Formatters
{
    public class FormatterManager
    {
        private static Dictionary<string, IFormatter> formatterDict = new Dictionary<string, IFormatter>();

        private static Menu menu = new Menu()
        {
            Margin = new Thickness(0, 0, 0, 0),
            VerticalAlignment = VerticalAlignment.Top,
            Height = 21,
            Name = "Formattions"
        };
        
        public static void AddMenuItem(MenuItem newItem)
        {
            menu.Items.Add(newItem);
        }

        public static Menu GetMenu()
        {
            return menu;
        }

        public static Dictionary<string, IFormatter> GetFormatters()
        {
            return formatterDict;
        }

        public static IFormatter GetByKey(string key)
        {
            return formatterDict[key];
        }

        public static bool AddFormatter(string key, IFormatter formatter, MenuItem newItem)
        {
            try
            {
                formatterDict.Add(key, formatter);
                if (!menu.Items.Contains(newItem)) { AddMenuItem(newItem); return true; }
                else { MessageBox.Show("It is already exists", "Can't add", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
            }
            catch { MessageBox.Show("It is already exists", "Can't add", MessageBoxButton.OK, MessageBoxImage.Error); return false; }
        }
    }
}
