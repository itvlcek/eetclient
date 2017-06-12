namespace EETLib.Models
{
    public class EETControl : IEETControl
    {
        public PkpElement PKP { get; set; }

        public BkpElement BKP { get; set; }
    }

    public class PkpElement
    {
        public string[] Text { get; set; }
    }

    public class BkpElement
    {
        public string[] Text { get; set; }
    }
}
