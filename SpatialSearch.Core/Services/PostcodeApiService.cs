using SpatialSearch.Core.Models;
using SpatialSearch.Core.Services.Interfaces;
using System.Text.Json;

namespace SpatialSearch.Core.Services
{
    public class PostcodeApiService : IPostcodeApiService
    {
        public async Task<LocationInformationModel?> GetPostcodeData(string postcode)
        {
            var baseUrl = "https://api.postcodes.io/postcodes/";
            var url = baseUrl + postcode;

            using var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var contentString = await response.Content.ReadAsStringAsync();
                var responseModel = JsonSerializer.Deserialize<PostcodeApiResponseModel>(contentString);

                LocationInformationModel postcodeSchemaModel = new LocationInformationModel(responseModel.result.postcode, responseModel.result.latitude, responseModel.result.longitude);
                return postcodeSchemaModel;
            }
            else
            {
                return null;
            }
        }
        public async Task<LocationInformationModel?> GetRandomPostcodeData()
        {
            var url = "https://api.postcodes.io/random/postcodes";

            using var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var contentString = await response.Content.ReadAsStringAsync();
                var responseModel = JsonSerializer.Deserialize<PostcodeApiResponseModel>(contentString);

                LocationInformationModel postcodeSchemaModel = new LocationInformationModel(responseModel.result.postcode, responseModel.result.latitude, responseModel.result.longitude);
                return postcodeSchemaModel;
            }
            else
            {
                return null;
            }
        }
    }
}
