using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpatialSearch.Core.Models;
using SpatialSearch.Core.Services.Interfaces;
using System.Net;
using Umbraco.Cms.Web.Common.Controllers;

namespace SpatialSearch.Core.Controllers.Api
{
    // Endpoint for this controller will be /umbraco/api/Postcodes/{method name}
    public class PostcodesController : UmbracoApiController
    {
        private readonly IDataHandlerService _dataHandlerService;
        private readonly IPostcodeApiService _postcodeApiService;
        public PostcodesController(IDataHandlerService dataHandlerService, IPostcodeApiService postcodeApiService)
        {
            _dataHandlerService = dataHandlerService;
            _postcodeApiService = postcodeApiService;
        }
        [HttpPost]
        public List<PostcodeResultsModel> GetPostcodeResults(SearchPostcodeModel search)
        {
            if (!ModelState.IsValid)
            {
                throw new BadHttpRequestException("Postcode or distance is invalid", (int)HttpStatusCode.BadRequest);
            }
            var locationData = _postcodeApiService.GetPostcodeData(search.SearchPostcode).Result;
            var results = _dataHandlerService.RetrievePostcodes(locationData, search.Distance);

            return results;
        }
    }
}
