namespace SpatialSearch.Core.Models
{
    using NPoco;

    [TableName("Postcodes")]
    public class PostcodesSchemaModel
    {
        public string Postcode { get; set; }

        public object Location { get; set; }

    }
}
