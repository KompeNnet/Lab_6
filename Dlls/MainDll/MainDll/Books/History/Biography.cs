using System;

namespace Lab_4.Books.History
{
    [Serializable]
    public class Biography : Historical
    {
        public string Person { get; set; }
        public string Years { get; set; }

        public Biography() { }

        public Biography(Historical h) : base(h) { }

        public Biography(Biography b) : base(b)
        {
            this.Person = b.Person;
            this.Years = b.Years;
        }
    }
}
