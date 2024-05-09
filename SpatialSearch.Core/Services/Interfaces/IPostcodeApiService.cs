using SpatialSearch.Core.Models;

namespace SpatialSearch.Core.Services.Interfaces
{
    public interface IPostcodeApiService
    {
        public Task<LocationInformationModel?> GetPostcodeData(string postcode);
        public Task<LocationInformationModel?> GetRandomPostcodeData();
    }
}
