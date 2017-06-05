using System.Collections.Generic;
using Lab_4.Loaders.FictionsLoaders;
using Lab_4.Books.Fictions;
using Lab_4.Helpers;

namespace Lab_4
{
    public class HorrorPlugin : IPlugin
    {
        Horror book = new Horror();
        string bookType = "Horror";
        string parentType = "Fiction";

        public string GetName()
        {
            return bookType;
        }

        public string GetParent()
        {
            return parentType;
        }

        public Hierarchy GetHierarchy()
        {
            return new Hierarchy(new HorrorLoader(), new List<string>());
        }
    }
}
