using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SpatialSearch.Core.Models
{
	public class SearchPostcodeModel
	{
		public SearchPostcodeModel()
		{

		}
		[DisplayName("Postcode")]
		[Required]
		[RegularExpression(@"([Gg][Ii][Rr] 0[Aa]{2})|((([A-Za-z][0-9]{1,2})|(([A-Za-z][A-Ha-hJ-Yj-y][0-9]{1,2})|(([A-Za-z][0-9][A-Za-z])|([A-Za-z][A-Ha-hJ-Yj-y][0-9][A-Za-z]?))))\s?[0-9][A-Za-z]{2})", ErrorMessage = "Postcode is invalid")]
		public string Postcode { get; set; }
		[DisplayName("Distance")]
		[Required]
		public int Distance { get; set; }
	}
}
