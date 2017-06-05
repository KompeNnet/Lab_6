using System;

namespace Lab_4.Books
{
    [Serializable]
    public class Historical : Book
    {
        public string Period { get; set; }

        public Historical() { }

        public Historical(Book b) : base(b) { }

        public Historical(Historical h) : base(h)
        {
            this.Period = h.Period;
        }
    }
}
