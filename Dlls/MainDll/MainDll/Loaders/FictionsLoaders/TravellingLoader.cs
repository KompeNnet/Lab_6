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
    public class TravellingLoader : FictionLoader
    {
        public override Book Create(GroupBox g)
        {
            Travelling t = new Travelling((Fiction)base.Create(g));

            GroupBox travelGroupBox = ((Grid)g.Content).Children.OfType<GroupBox>().First(x => x.Name == "FictTravellingGroup");
            IEnumerable<TextBox> tbList = ((Grid)travelGroupBox.Content).Children.OfType<TextBox>();

            t.Countries = tbList.First(x => x.Name == "InpFictTravCountries").Text;
            return t;
        }

        public override Book BaseCreate(GroupBox g)
        {
            return new Travelling((Fiction)base.Create(g));
        }

        public override Grid Load(Book tTemp)
        {
            Grid g = base.Load((Fiction)tTemp);
            Travelling t = (Travelling)tTemp;

            Grid grg = FormCreator.CreateGrid(new Thickness(0, 0, 0, 0));
            grg.Children.Add(FormCreator.CreateLabel("Countries", new Thickness(10, 10, 45, 0)));
            grg.Children.Add(FormCreator.CreateTextBox("InpFictTravCountries", t.Countries, new Thickness(10, 38, 9, 10)));

            GroupBox gr = FormCreator.CreateGroupBox("FictTravellingGroup", "Travelling", new Thickness(329, 0, 10, 10), 174, 384);
            gr.Content = grg;

            g.Children.Add(gr);

            GroupBox fictionGroupBox = g.Children.OfType<GroupBox>().First(x => x.Name == "FictionGroup");
            IEnumerable<ComboBox> cbList = ((Grid)fictionGroupBox.Content).Children.OfType<ComboBox>();
            ComboBox genreComboBox = cbList.First(x => x.Name == "ChooseFictType");
            genreComboBox.SelectedValue = "Travelling";

            return g;
        }

        public override Book Deserialize(string d, ISerializer serializer)
        {
            return serializer.Deserialize<Travelling>(d);
        }
    }
}
