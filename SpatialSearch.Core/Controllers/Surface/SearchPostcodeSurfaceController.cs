using Microsoft.AspNetCore.Mvc;
using SpatialSearch.Core.Models;
using SpatialSearch.Core.Services.Interfaces;
using System.Text.Json;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;

namespace SpatialSearch.Core.Controllers.Surface
{
    public class SearchPostcodeSurfaceController : BaseSurfaceController
    {
        private readonly IDataHandlerService _dataHandlerService;
        private readonly IPostcodeApiService _postcodeApiService;
        public SearchPostcodeSurfaceController(IDataHandlerService dataHandlerService, IPostcodeApiService postcodeApiService, IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _dataHandlerService = dataHandlerService;
            _postcodeApiService = postcodeApiService;
        }
        [HttpPost]
        public async Task<IActionResult> Submit(SearchPostcodeModel form)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }
            var locationData = _postcodeApiService.GetPostcodeData(form.SearchPostcode).Result;
            var results = _dataHandlerService.RetrievePostcodes(locationData, form.Distance);
            var stringResults = JsonSerializer.Serialize(results);
            TempData["PostcodeData"] = stringResults;
            return RedirectToCurrentUmbracoPage();
        }

    }
}
