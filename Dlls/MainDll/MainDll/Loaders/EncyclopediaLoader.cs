using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Lab_4.Books;
using Lab_4.Helpers;
using Lab_4.Helpers.Serialization;

namespace Lab_4.Loaders
{
    public class EncyclopediaLoader : BookLoader
    {
        public override Book Create(GroupBox g)
        {
            Encyclopedia e = new Encyclopedia(base.Create(g));

            GroupBox encGroupBox = ((Grid)g.Content).Children.OfType<GroupBox>().First(x => x.Name == "EncycloGroup");
            IEnumerable<TextBox> tbList = ((Grid)encGroupBox.Content).Children.OfType<TextBox>();

            e.Genre = "Encyclopedia";
            e.Subject = tbList.First(x => x.Name == "InpEnSubject").Text;
            return e;
        }

        public override Book BaseCreate(GroupBox g)
        {
            return new Encyclopedia(base.Create(g));
        }

        public override Grid Load(Book eTemp)
        {
            Grid g = base.Load(eTemp);
            Encyclopedia e = (Encyclopedia)eTemp;

            Grid grg = FormCreator.CreateGrid(new Thickness(0, 0, 0, 0));
            grg.Children.Add(FormCreator.CreateLabel("Subject", new Thickness(10, 10, 0, 0)));
            grg.Children.Add(FormCreator.CreateTextBox("InpEnSubject", e.Subject, new Thickness(10, 38, 0, 0)));

            GroupBox gr = FormCreator.CreateGroupBox("EncycloGroup", "Encyclopedia", new Thickness(155, 0, 0, 0), 174, 361);
            gr.Content = grg;

            g.Children.Add(gr);

            IEnumerable<ComboBox> cbList = g.Children.OfType<ComboBox>();
            ComboBox genreComboBox = cbList.First(x => x.Name == "ChooseGenre");
            genreComboBox.SelectedIndex = 0;

            return g;
        }

        public override Book Deserialize(string d, ISerializer serializer)
        {
            return serializer.Deserialize<Encyclopedia>(d);
        }
    }
}
