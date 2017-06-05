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
    public class FairyTalesLoader : FantasticTalesLoader
    {
        public override Book Create(GroupBox g)
        {
            FairyTales f = new FairyTales((FantasticTales)base.Create(g));

            GroupBox fairyGroupBox = ((Grid)g.Content).Children.OfType<GroupBox>().First(x => x.Name == "FictFantFairyTalesGroup");
            IEnumerable<CheckBox> chbList = ((Grid)fairyGroupBox.Content).Children.OfType<CheckBox>();

            f.IsIllustrated = chbList.First(x => x.Name == "CheckFictFantFairyIsIllustrated").IsChecked.Value;
            return f;
        }

        public override Book BaseCreate(GroupBox g)
        {
            return new FairyTales((FantasticTales)base.Create(g));
        }

        public override Grid Load(Book fTemp)
        {
            Grid g = base.Load((FantasticTales)fTemp);
            FairyTales f = (FairyTales)fTemp;

            Grid grg = FormCreator.CreateGrid(new Thickness(0, 0, 0, 0));
            grg.Children.Add(FormCreator.CreateCheckBox("CheckFictFantFairyIsIllustrated", "is illustrated", new Thickness(10, 9, 0, 0), f.IsIllustrated));

            GroupBox gr = FormCreator.CreateGroupBox("FictFantFairyTalesGroup", "FairyTales", new Thickness(332, 184, 0, 0), 165, 170);
            gr.Content = grg;

            g.Children.Add(gr);

            GroupBox ftGroupBox = g.Children.OfType<GroupBox>().First(x => x.Name == "FictFantasticTalesGroup");
            IEnumerable<ComboBox> cbList = ((Grid)ftGroupBox.Content).Children.OfType<ComboBox>();
            ComboBox genreComboBox = cbList.First(x => x.Name == "ChooseFictFantType");
            genreComboBox.SelectedValue = "FairyTales";

            return g;
        }

        public override Book Deserialize(string d, ISerializer serializer)
        {
            return serializer.Deserialize<FairyTales>(d);
        }
    }
}
