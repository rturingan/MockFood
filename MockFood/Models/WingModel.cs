namespace MockFood.Models
{
    public class WingModel
    {
        public string ImageTitle { get; set; }
        public string WingAssort { get; set; }
        public float BasePrice { get; set; } = 350;
        public bool ClassicBuffalo { get; set; }
        public bool JalapChiliCheese { get; set; }
        public bool Ranch { get; set; }
        public bool BuffRanch { get; set; }
        public bool SweetSoy { get; set; }
        public bool SaltPepper { get; set; }
        public bool HoneyBasil { get; set;}
        public bool SrirachaHoney { get; set; }
        public float WingPrice { get; internal set; }
    }
}
