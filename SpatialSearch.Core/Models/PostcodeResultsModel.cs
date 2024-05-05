namespace SpatialSearch.Core.Models
{
    public class PostcodeResultsModel
    {
        public string Postcode { get; set; }
        public float Lat { get; set; }
        public float Long { get; set; }
        public double Distance { get; set; }
    }
}
