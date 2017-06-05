using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Lab_4.Books.Fictions;
using Lab_4.Books;
using Lab_4.Helpers;
using Lab_4.Helpers.Serialization;

namespace Lab_4.Loaders.FictionsLoaders
{
    public class ScienceFictionLoader : FantasticTalesLoader
    {
        public override Book Create(GroupBox g)
        {
            ScienceFiction s = new ScienceFiction((FantasticTales)base.Create(g));

            GroupBox sfGroupBox = ((Grid)g.Content).Children.OfType<GroupBox>().First(x => x.Name == "FictFantScienceFictionGroup");
            IEnumerable<CheckBox> chbList = ((Grid)sfGroupBox.Content).Children.OfType<CheckBox>();

            s.IsEarth = chbList.First(x => x.Name == "CheckFictFantFairyIsEarth").IsChecked.Value;
            return s;
        }

        public override Book BaseCreate(GroupBox g)
        {
            return new ScienceFiction((FantasticTales)base.Create(g));
        }

        public override Grid Load(Book sTemp)
        {
            Grid g = base.Load((FantasticTales)sTemp);
            ScienceFiction s = (ScienceFiction)sTemp;

            Grid grg = FormCreator.CreateGrid(new Thickness(0, 0, 0, 0));
            grg.Children.Add(FormCreator.CreateCheckBox("CheckFictFantFairyIsEarth", "is Earth", new Thickness(10, 10, 10, 0), s.IsEarth));

            GroupBox gr = FormCreator.CreateGroupBox("FictFantScienceFictionGroup", "ScienceFiction", new Thickness(332, 184, 0, 0), 165, 170);
            gr.Content = grg;

            g.Children.Add(gr);

            GroupBox ftGroupBox = g.Children.OfType<GroupBox>().First(x => x.Name == "FictFantasticTalesGroup");
            IEnumerable<ComboBox> cbList = ((Grid)ftGroupBox.Content).Children.OfType<ComboBox>();
            ComboBox genreComboBox = cbList.First(x => x.Name == "ChooseFictFantType");
            genreComboBox.SelectedValue = "ScienceFiction";

            return g;
        }

        public override Book Deserialize(string d, ISerializer serializer)
        {
            return serializer.Deserialize<ScienceFiction>(d);
        }
    }
}
