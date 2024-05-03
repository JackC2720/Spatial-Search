namespace SpatialSearch.Core.Models
{
    public class LocationInformationModel
    {
        public LocationInformationModel(string postcode, double lat, double lon)
        {
            Postcode = postcode;
            Lat = lat;
            Lon = lon;
        }
        public string Postcode { get; set; }

        public double Lat { get; set; }
        public double Lon { get; set; }

    }
}
