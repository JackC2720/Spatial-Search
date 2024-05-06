namespace SpatialSearch.Core.Models
{
    public class LocationInformationModel
    {
        public LocationInformationModel(string postcode, float lat, float lon)
        {
            Postcode = postcode;
            Lat = lat;
            Lon = lon;
        }
        public string Postcode { get; set; }

        public float Lat { get; set; }
        public float Lon { get; set; }

    }
}
