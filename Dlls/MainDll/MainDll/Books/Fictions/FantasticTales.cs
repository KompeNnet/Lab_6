using System;

namespace Lab_4.Books.Fictions
{
    [Serializable]
    public class FantasticTales : Fiction
    {
        public string CoAuthors { get; set; }

        public FantasticTales() { }

        public FantasticTales(Fiction f) : base(f) { }

        public FantasticTales(FantasticTales f) : base(f)
        {
            this.CoAuthors = f.CoAuthors;
        }
    }
}
