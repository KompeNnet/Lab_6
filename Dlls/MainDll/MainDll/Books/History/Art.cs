using System;

namespace Lab_4.Books.History
{
    [Serializable]
    public class Art : Historical
    {
        public string ArtForm { get; set; }

        public Art() { }

        public Art(Historical h) : base(h) { }

        public Art(Art a) : base(a)
        {
            this.ArtForm = a.ArtForm;
        }
    }
}
