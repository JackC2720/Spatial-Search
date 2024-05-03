using SpatialSearch.Core.Models;
using SpatialSearch.Core.Services.Interfaces;
using IScopeProvider = Umbraco.Cms.Infrastructure.Scoping.IScopeProvider;

namespace SpatialSearch.Core.Services
{
    public class DataHandlerService : IDataHandlerService
    {
        private readonly IScopeProvider _scopeProvider;
        public DataHandlerService(IScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;
        }
        public string WritePostcodeToDatabase(LocationInformationModel locationInformation)
        {
            //avoid ambient scope error
            if (!ExecutionContext.IsFlowSuppressed())
            {
                ExecutionContext.SuppressFlow();
            }

            using (var scope = _scopeProvider.CreateScope(autoComplete: false))
            {
                var message = "Postcode already exsists";
                var existingRecord = scope.Database.Fetch<PostcodesSchemaModel>().FirstOrDefault(r => r.Postcode == locationInformation.Postcode);
                if (existingRecord == null)
                {
                    string sql = @"INSERT INTO Postcodes (Postcode, Location)
                                   VALUES (@postcode, geography::Point(@lat, @lon, 4326))";

                    var parameters = new Dictionary<string, object>()
                        {
                            { "postcode", locationInformation.Postcode },
                            { "lat", locationInformation.Lat },
                            { "lon", locationInformation.Lon }
                        };
                    int rowsAffected = scope.Database.Execute(sql, parameters);

                    if (rowsAffected > 0)
                    {
                        message = "Postcode added successfully";
                    }
                    else
                    {
                        message = "Error writing location data to database";
                    }
                }
                scope.Complete();
                return message;
            }
        }
        public List<PostcodeResultsModel> RetrievePostcodes(LocationInformationModel locationInformation, int distance)
        {
            //avoid ambient scope error
            if (!ExecutionContext.IsFlowSuppressed())
            {
                ExecutionContext.SuppressFlow();
            }

            using (var scope = _scopeProvider.CreateScope(autoComplete: false))
            {
                string sql = @"SELECT P.Postcode , ROUND((P.Location.STDistance(geography::Point(@lat, @lon, 4326)) /1000),1) AS 'Distance' 
                                FROM [Postcodes] AS P
                                WHERE P.Location.STDistance(geography::Point(@lat, @lon, 4326)) < @distance;";

                var parameters = new Dictionary<string, object>()
                        {
                            { "distance", distance * 1000 },
                            { "lat", locationInformation.Lat },
                            { "lon", locationInformation.Lon }
                        };
                List<PostcodeResultsModel> results = scope.Database.Fetch<PostcodeResultsModel>(sql, parameters);
                scope.Complete();
                return results;
            }
        }
    }
}
