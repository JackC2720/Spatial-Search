namespace SpatialSearch.Core.Models
{
	using NPoco;

	[TableName("Postcodes")]
	public class PostcodeSchemaModel
	{
		public string Postcode { get; set; }
		public double X { get; set; }
		public double Y { get; set; }
	}
}
