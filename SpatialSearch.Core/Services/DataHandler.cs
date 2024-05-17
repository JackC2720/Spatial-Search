using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SpatialSearch.Core.Models;
using SpatialSearch.Core.Services.Interfaces;

namespace SpatialSearch.Core.Services
{
    public class DataHandler : IDataHandlerService
    {
        private readonly IConfiguration _configuration;

        public DataHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string WritePostcodeToDatabase(LocationInformationModel locationInformation)
        {
            string sqlSelect = $@"SELECT COUNT(*) FROM [Postcodes] AS P WHERE P.Postcode = '{locationInformation.Postcode}';";

            string sqlInsert = @"INSERT INTO Postcodes(Postcode, Location)
                                  VALUES(@Postcode, geography::Point(@Lat, @Lon, 4326));";

            using (var connection = new SqlConnection(_configuration.GetConnectionString("PostcodesDatabase")))
            {
                connection.Open();
                var count = connection.ExecuteScalar<int>(sqlSelect);
                if (count > 0)
                {
                    return "Postcode already exsists";
                }
                var rowsAffected = connection.Execute(sqlInsert, locationInformation);
                if (rowsAffected > 0)
                {
                    return "Postcode added successfully";
                }
                return "Error adding postcode";
            }
        }
        public List<PostcodeResultsModel> RetrievePostcodes(LocationInformationModel locationInformation, int distance)
        {
            string sql = $@"SELECT P.Postcode, P.Location.Lat AS 'Lat', P.Location.Long AS 'Long', ROUND((P.Location.STDistance(geography::Point({locationInformation.Lat}, {locationInformation.Lon}, 4326)) /1000),1) AS Distance 
                           FROM [Postcodes] AS P
                           WHERE P.Location.STDistance(geography::Point({locationInformation.Lat}, {locationInformation.Lon}, 4326)) < {distance * 1000}
                           ORDER BY Distance ASC;";
            var results = new List<PostcodeResultsModel>();
            PostcodeResultsModel searchData = new()
            {
                Postcode = locationInformation.Postcode,
                Lat = locationInformation.Lat,
                Long = locationInformation.Lon,
                Distance = distance,
            };
            using (var connection = new SqlConnection(_configuration.GetConnectionString("PostcodesDatabase")))
            {
                connection.Open();
                results = connection.Query<PostcodeResultsModel>(sql).ToList();
            }
            results.Add(searchData);
            return results;
        }
    }
}
