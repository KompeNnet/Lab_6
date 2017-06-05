using System;

namespace Lab_4.Books.Fictions
{
    [Serializable]
    public class FairyTales : FantasticTales
    {
        public bool IsIllustrated { get; set; }

        public FairyTales() { }

        public FairyTales(FantasticTales f) : base(f) { }

        public FairyTales(FairyTales f) : base(f)
        {
            this.IsIllustrated = f.IsIllustrated;
        }
    }
}
