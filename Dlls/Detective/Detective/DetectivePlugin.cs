using System.Collections.Generic;
using Lab_4.Books.History;
using Lab_4.Helpers;
using Lab_4.Loaders.HistoryLoaders;

namespace Lab_4
{
    public class DetectivePlugin : IPlugin
    {
        Detective book = new Detective();
        string bookType = "Detective";
        string parentType = "Historical";

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
            return new Hierarchy(new DetectiveLoader(), new List<string>());
        }
    }
}
