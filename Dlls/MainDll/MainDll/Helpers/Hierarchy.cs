using System.Collections.Generic;
using Lab_4.Loaders;

namespace Lab_4.Helpers
{
    public class Hierarchy
     {
        public List<string> BookChild { get; set; }
        public BookLoader Loader { get; set; }

        public Hierarchy(BookLoader loader, List<string> bookChild)
        {
            this.BookChild = bookChild;
            this.Loader = loader;
        }
    } 
}
