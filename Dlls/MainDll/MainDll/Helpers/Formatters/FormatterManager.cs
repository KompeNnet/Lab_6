using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Lab_4.Helpers.Formatters
{
    public class FormatterManager
    {
        private static readonly FormatterManager instance = new FormatterManager();

        public static FormatterManager GetInstance { get { return instance; } }

        private FormatterManager() { }

        private Dictionary<string, IFormatter> formatterDict = new Dictionary<string, IFormatter>();

        private Menu menu = new Menu()
        {
            Margin = new Thickness(0, 0, 0, 0),
            VerticalAlignment = VerticalAlignment.Top,
            Height = 21,
            Name = "Formattions"
        };
        
        public void AddMenuItem(MenuItem newItem)
        {
            menu.Items.Add(newItem);
        }

        public Menu GetMenu()
        {
            return menu;
        }

        public Dictionary<string, IFormatter> GetFormatters()
        {
            return formatterDict;
        }

        public IFormatter GetByKey(string key)
        {
            return formatterDict[key];
        }

        public bool AddFormatter(string key, IFormatter formatter, MenuItem newItem)
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
