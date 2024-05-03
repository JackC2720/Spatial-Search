using SpatialSearch.Core.Models;

namespace SpatialSearch.Core.Services.Interfaces
{
    public interface IDataHandlerService
    {
        public string WritePostcodeToDatabase(LocationInformationModel postcodeSchema);
        public List<PostcodeResultsModel> RetrievePostcodes(LocationInformationModel locationInformation, int distance);
    }
}
