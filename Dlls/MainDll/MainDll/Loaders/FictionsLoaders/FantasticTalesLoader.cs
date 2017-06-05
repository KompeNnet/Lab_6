using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Lab_4.Books;
using Lab_4.Books.Fictions;
using Lab_4.Helpers;
using Lab_4.Helpers.Serialization;

namespace Lab_4.Loaders.FictionsLoaders
{
    public class FantasticTalesLoader : FictionLoader
    {
        public override Book Create(GroupBox g)
        {
            FantasticTales f = new FantasticTales((Fiction)base.Create(g));

            GroupBox ftGroupBox = ((Grid)g.Content).Children.OfType<GroupBox>().First(x => x.Name == "FictFantasticTalesGroup");
            IEnumerable<TextBox> tbList = ((Grid)ftGroupBox.Content).Children.OfType<TextBox>();

            f.CoAuthors = tbList.First(x => x.Name == "InpFictFantCoWorkers").Text;
            return f;
        }

        public override Book BaseCreate(GroupBox g)
        {
            return new FantasticTales((Fiction)base.Create(g));
        }

        public override Grid Load(Book fTemp)
        {
            Grid g = base.Load((Fiction)fTemp);
            FantasticTales f = (FantasticTales)fTemp;

            Grid grg = FormCreator.CreateGrid(new Thickness(0, 0, 0, 0));
            grg.Children.Add(FormCreator.CreateLabel("coWorkers", new Thickness(10, 9, 0, 0)));
            grg.Children.Add(FormCreator.CreateTextBox("InpFictFantCoWorkers", f.CoAuthors, new Thickness(10, 37, 0, 0)));
            grg.Children.Add(FormCreator.CreateLabel("Type", new Thickness(10, 59, 0, 0)));

            ComboBox cb = FormCreator.CreateComboBox("ChooseFictFantType", new Thickness(10, 87, 0, 0), LoaderManager.GetChildren("FantasticTales"));
            cb.SelectionChanged += new SelectionChangedEventHandler(SelectionChanged);
            grg.Children.Add(cb);

            GroupBox gr = FormCreator.CreateGroupBox("FictFantasticTalesGroup", "FantasticTales", new Thickness(329, 0, 0, 0), 171, 361);
            gr.Content = grg;

            g.Children.Add(gr);

            GroupBox fictionGroupBox = g.Children.OfType<GroupBox>().First(x => x.Name == "FictionGroup");
            IEnumerable<ComboBox> cbList = ((Grid)fictionGroupBox.Content).Children.OfType<ComboBox>();
            ComboBox genreComboBox = cbList.First(x => x.Name == "ChooseFictType");
            genreComboBox.SelectedValue = "FantasticTales";

            return g;
        }

        public override Book Deserialize(string d, ISerializer serializer)
        {
            return serializer.Deserialize<FantasticTales>(d);
        }
    }
}
