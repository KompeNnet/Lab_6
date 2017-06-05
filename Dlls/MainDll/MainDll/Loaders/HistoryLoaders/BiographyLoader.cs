using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Lab_4.Books;
using Lab_4.Books.History;
using Lab_4.Helpers;
using Lab_4.Helpers.Serialization;

namespace Lab_4.Loaders.HistoryLoaders
{
    public class BiographyLoader : HistoricalLoader
    {
        public override Book Create(GroupBox g)
        {
            Biography b = new Biography((Historical)base.Create(g));

            GroupBox bioGroupBox = ((Grid)g.Content).Children.OfType<GroupBox>().First(x => x.Name == "HistBiographyGroup");
            IEnumerable<TextBox> tbList = ((Grid)bioGroupBox.Content).Children.OfType<TextBox>();

            b.Person = tbList.First(x => x.Name == "InpHistBioPerson").Text;
            b.Years = tbList.First(x => x.Name == "InpHistBioYears").Text;
            return b;
        }

        public override Book BaseCreate(GroupBox g)
        {
            return new Biography((Historical)base.Create(g));
        }

        public override Grid Load(Book bTemp)
        {
            Grid g = base.Load((Historical)bTemp);
            Biography b = (Biography)bTemp;

            Grid grg = FormCreator.CreateGrid(new Thickness(0, 0, 0, 0));
            grg.Children.Add(FormCreator.CreateLabel("Person", new Thickness(10, 10, 73, 0)));
            grg.Children.Add(FormCreator.CreateTextBox("InpHistBioPerson", b.Person, new Thickness(9, 38, 10, 0)));
            grg.Children.Add(FormCreator.CreateLabel("Years of life", new Thickness(10, 60, 63, 0)));
            grg.Children.Add(FormCreator.CreateTextBox("InpHistBioYears", b.Years, new Thickness(10, 88, 10, 0)));

            GroupBox gr = FormCreator.CreateGroupBox("HistBiographyGroup", "Biography", new Thickness(329, 0, 10, 10), 174, 384);
            gr.Content = grg;

            g.Children.Add(gr);

            GroupBox ftGroupBox = g.Children.OfType<GroupBox>().First(x => x.Name == "HistoricalGroup");
            IEnumerable<ComboBox> cbList = ((Grid)ftGroupBox.Content).Children.OfType<ComboBox>();
            ComboBox genreComboBox = cbList.First(x => x.Name == "ChooseHistType");
            genreComboBox.SelectedValue = "Biography";

            return g;
        }

        public override Book Deserialize(string d, ISerializer serializer)
        {
            return serializer.Deserialize<Biography>(d);
        }
    }
}
