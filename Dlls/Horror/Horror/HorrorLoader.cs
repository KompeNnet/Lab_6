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
    public class HorrorLoader : FictionLoader
    {
        public override Book Create(GroupBox g)
        {
            Horror t = new Horror((Fiction)base.Create(g));

            GroupBox horrorGroupBox = ((Grid)g.Content).Children.OfType<GroupBox>().First(x => x.Name == "FictHorrorGroup");
            IEnumerable<TextBox> tbList = ((Grid)horrorGroupBox.Content).Children.OfType<TextBox>();

            t.Theme = tbList.First(x => x.Name == "InpFictHorrTheme").Text;
            
            IEnumerable<CheckBox> cbList = ((Grid)horrorGroupBox.Content).Children.OfType<CheckBox>();

            t.IsPsyhological = cbList.First(x => x.Name == "CheckFictHorrIsPsychological").IsChecked.Value;
            return t;
        }

        public override Book BaseCreate(GroupBox g)
        {
            return new Horror((Fiction)base.Create(g));
        }

        public override Grid Load(Book hTemp)
        {
            Grid g = base.Load((Fiction)hTemp);
            Horror h = (Horror)hTemp;

            Grid grg = FormCreator.CreateGrid(new Thickness(0, 0, 0, 0));
            grg.Children.Add(FormCreator.CreateLabel("Theme", new Thickness(10, 10, 73, 0)));
            grg.Children.Add(FormCreator.CreateTextBox("InpFictHorrTheme", h.Theme, new Thickness(10, 38, 10, 0)));

            grg.Children.Add(FormCreator.CreateCheckBox("CheckFictHorrIsPsychological", "is Psychological", new Thickness(10, 88, 10, 0), h.IsPsyhological));

            GroupBox gr = FormCreator.CreateGroupBox("FictHorrorGroup", "Horror", new Thickness(329, 0, 10, 10), 174, 384);
            gr.Content = grg;

            g.Children.Add(gr);

            GroupBox fictionGroupBox = g.Children.OfType<GroupBox>().First(x => x.Name == "FictionGroup");
            IEnumerable<ComboBox> cbList = ((Grid)fictionGroupBox.Content).Children.OfType<ComboBox>();
            ComboBox genreComboBox = cbList.First(x => x.Name == "ChooseFictType");
            genreComboBox.SelectedValue = "Horror";

            return g;
        }

        public override Book Deserialize(string d, ISerializer serializer)
        {
            return serializer.Deserialize<Horror>(d);
        }
    }
}
