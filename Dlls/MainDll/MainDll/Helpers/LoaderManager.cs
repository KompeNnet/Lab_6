using Lab_4.Books;
using Lab_4.Books.Fictions;
using Lab_4.Books.History;
using Lab_4.Loaders;
using Lab_4.Loaders.FictionsLoaders;
using Lab_4.Loaders.HistoryLoaders;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Lab_4.Helpers
{
    public class LoaderManager
    {
        private static readonly LoaderManager instance = new LoaderManager();

        public static LoaderManager GetInstance { get { return instance; } }

        protected LoaderManager() { }

        private Dictionary<string, Hierarchy> loaderDict = new Dictionary<string, Hierarchy>
        {
            { "Book", new Hierarchy(new EncyclopediaLoader(), new List<string>() { "Encyclopedia", "Fiction", "Historical" } ) },
            { "Encyclopedia", new Hierarchy(new EncyclopediaLoader(), new List<string>() ) },
            { "Historical",new Hierarchy(new HistoricalLoader(), new List<string>() { "Art", "Biography" } ) },
            { "Art", new Hierarchy(new ArtLoader(), new List<string>() ) },
            { "Biography", new Hierarchy(new BiographyLoader(), new List<string>()) },
            { "Fiction", new Hierarchy(new FictionLoader(), new List<string>() { "Travelling", "FantasticTales" } ) },
            { "Travelling", new Hierarchy(new TravellingLoader(), new List<string>() ) },
            { "FantasticTales", new Hierarchy(new FantasticTalesLoader(), new List<string>() { "FairyTales", "ScienceFiction" } ) },
            { "ScienceFiction", new Hierarchy(new ScienceFictionLoader(), new List<string>() ) },
            { "FairyTales", new Hierarchy(new FairyTalesLoader(), new List<string>() ) }
        };

        private Type[] types = new Type[]
        {
            typeof(Book),
            typeof(Encyclopedia),
            typeof(Historical),
            typeof(Art),
            typeof(Biography),
            typeof(Fiction),
            typeof(Travelling),
            typeof(FantasticTales),
            typeof(ScienceFiction),
            typeof(FairyTales)
        };

        public Type[] GetTypes()
        {
            return types;
        }

        public BookLoader GetLoader(string key)
        {
            return loaderDict[key].Loader;
        }

        public List<string> GetChildren(string key)
        {
            return loaderDict[key].BookChild;
        }

        public void AddLoader(string key, string parent, Hierarchy member)
        {
            try
            {
                int count = types.Length;
                Array.Resize(ref types, count + 1);
                types[count] = Type.GetType(key);
                loaderDict.Add(key, member);
                loaderDict[parent].BookChild.Add(key);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("It is already exists", "Can't add", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
