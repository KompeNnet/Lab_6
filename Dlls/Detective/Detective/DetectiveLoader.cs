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
    public class DetectiveLoader : HistoricalLoader
    {
        /*public string Seriousness { get; set; }
        public string Review { get; set; }*/
        public override Book Create(GroupBox g)
        {
            Detective d = new Detective((Historical)base.Create(g));

            GroupBox DetectiveGroupBox = ((Grid)g.Content).Children.OfType<GroupBox>().First(x => x.Name == "HistDetectiveGroup");
            IEnumerable<TextBox> tbList = ((Grid)DetectiveGroupBox.Content).Children.OfType<TextBox>();

            d.Seriousness = tbList.First(x => x.Name == "InpHistDetectSerious").Text;
            d.Review = tbList.First(x => x.Name == "InpHistDetectReview").Text;
            return d;
        }

        public override Book BaseCreate(GroupBox g)
        {
            return new Detective((Historical)base.Create(g));
        }

        public override Grid Load(Book aTemp)
        {
            Grid g = base.Load((Historical)aTemp);
            Detective d = (Detective)aTemp;

            Grid grg = FormCreator.CreateGrid(new Thickness(0, 0, 0, 0));
            grg.Children.Add(FormCreator.CreateLabel("Book's seriousness", new Thickness(10, 10, 43, 0)));
            grg.Children.Add(FormCreator.CreateTextBox("InpHistDetectSerious", d.Seriousness, new Thickness(9, 38, 10, 0)));
            grg.Children.Add(FormCreator.CreateLabel("Short review", new Thickness(10, 60, 63, 0)));
            grg.Children.Add(FormCreator.CreateTextBox("InpHistDetectReview", d.Review, new Thickness(10, 88, 10, 0)));

            GroupBox gr = FormCreator.CreateGroupBox("HistDetectiveGroup", "Detective", new Thickness(329, 0, 10, 10), 174, 384);
            gr.Content = grg;

            g.Children.Add(gr);

            GroupBox ftGroupBox = g.Children.OfType<GroupBox>().First(x => x.Name == "HistoricalGroup");
            IEnumerable<ComboBox> cbList = ((Grid)ftGroupBox.Content).Children.OfType<ComboBox>();
            ComboBox genreComboBox = cbList.First(x => x.Name == "ChooseHistType");
            genreComboBox.SelectedValue = "Detective";

            return g;
        }

        public override Book Deserialize(string d, ISerializer serializer)
        {
            return serializer.Deserialize<Detective>(d);
        }
    }
}
