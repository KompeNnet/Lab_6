using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Lab_4.Books;
using Lab_4.Helpers;
using Lab_4.Helpers.Serialization;

namespace Lab_4.Loaders
{
    public class HistoricalLoader : BookLoader
    {
        public override Book Create(GroupBox g)
        {
            Historical h = new Historical(base.Create(g));

            GroupBox histGroupBox = ((Grid)g.Content).Children.OfType<GroupBox>().First(x => x.Name == "HistoricalGroup");
            IEnumerable<TextBox> tbList = ((Grid)histGroupBox.Content).Children.OfType<TextBox>();

            h.Period = tbList.First(x => x.Name == "InpHistPeriod").Text;
            h.Genre = "Historical";
            return h;
        }

        public override Book BaseCreate(GroupBox g)
        {
            return new Historical(base.Create(g));
        }

        public override Grid Load(Book hTemp)
        {
            Grid g = base.Load(hTemp);
            Historical h = (Historical)hTemp;

            Grid grg = FormCreator.CreateGrid(new Thickness(0, 0, 0, 0));
            grg.Children.Add(FormCreator.CreateLabel("Period", new Thickness(10, 10, 0, 0)));
            grg.Children.Add(FormCreator.CreateTextBox("InpHistPeriod", h.Period, new Thickness(10, 38, 0, 0)));
            grg.Children.Add(FormCreator.CreateLabel("Type", new Thickness(10, 60, 0, 0)));

            ComboBox cb = FormCreator.CreateComboBox("ChooseHistType", new Thickness(10, 88, 0, 0), LoaderManager.GetChildren("Historical"));
            cb.SelectionChanged += new SelectionChangedEventHandler(SelectionChanged);
            grg.Children.Add(cb);

            GroupBox gr = FormCreator.CreateGroupBox("HistoricalGroup", "Historical", new Thickness(155, 0, 0, 0), 174, 361);
            gr.Content = grg;

            g.Children.Add(gr);

            IEnumerable<ComboBox> cbList = g.Children.OfType<ComboBox>();
            ComboBox genreComboBox = cbList.First(x => x.Name == "ChooseGenre");
            genreComboBox.SelectedIndex = 2;

            return g;
        }

        public override Book Deserialize(string d, ISerializer serializer)
        {
            return serializer.Deserialize<Historical>(d);
        }
    }
}
