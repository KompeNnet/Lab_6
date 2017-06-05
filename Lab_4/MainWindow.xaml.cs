using System.Windows;
using System.Windows.Controls;
using Lab_4.Books;
using Lab_4.Helpers;
using Lab_4.Helpers.Formatters;
using Lab_4.Loaders;
using System.Collections.Generic;
using System.Linq;

namespace Lab_4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            GroupBox newGroupBox = FormCreator.CreateGroupBox("MainGroup", "Book", new Thickness(0, 0, 0, 0), 887, 384);
            Grid g = new BookLoader().Load(new Book());
            g.Children.Add(new BookLoader().CreateButtonsGroup("Book"));
            newGroupBox.Content = g;

            MainGrid.Children.Add(newGroupBox);
        }

        private void BookListForm_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BookListForm.SelectedIndex != -1)
            {
                IEnumerable<ListView> listList = MainGrid.Children.OfType<ListView>();
                
                ListView list = listList.First(x => x.Name == "BookListForm");
                MainGrid.Children.Clear();
                MainGrid.Children.Add(list);

                ItemInList elem = (ItemInList)BookListForm.Items.GetItemAt(BookListForm.SelectedIndex);

                var loader = LoaderManager.GetLoader(elem.Type);

                GroupBox newGroupBox = FormCreator.CreateGroupBox("MainGroup", "Book", new Thickness(0, 0, 0, 0), 887, 384);
                Grid g = loader.Load(elem.Data);
                g.Children.Add(loader.CreateButtonsGroup(elem.Type));
                newGroupBox.Content = g;

                MainGrid.Children.Add(newGroupBox);

                if (FormatterManager.GetFormatters().Count != 0) MainGrid.Children.Add(loader.GetMenu(MainGrid));
            }
        }
    }
}
