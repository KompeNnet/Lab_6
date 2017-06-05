using System;

namespace Lab_4.Books.History
{
    [Serializable]
    public class Detective : Historical
    {
        public string Seriousness { get; set; }
        public string Review { get; set; }

        public Detective() { }

        public Detective(Historical h) : base(h) { }

        public Detective(Detective d) : base(d)
        {
            this.Seriousness = d.Seriousness;
            this.Review = d.Review;
        }
    }
}
