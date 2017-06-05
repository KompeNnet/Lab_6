using System;

namespace Lab_4.Books.Fictions
{
    [Serializable]
    public class Horror : Fiction
    {
        public string Theme { get; set; }
        public bool IsPsyhological { get; set; }

        public Horror() { }

        public Horror(Fiction f) : base(f) { }

        public Horror(Horror t) : base(t)
        {
            this.Theme = t.Theme;
            this.IsPsyhological = t.IsPsyhological;
        }
    }
}
