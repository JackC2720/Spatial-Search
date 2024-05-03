using Microsoft.AspNetCore.Mvc;
using SpatialSearch.Core.Models;
using SpatialSearch.Core.Services.Interfaces;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;

namespace SpatialSearch.Core.Controllers.Surface
{
    public class AddPostcodeToDatabaseSurfaceController : BaseSurfaceController
    {
        private readonly IDataHandlerService _dataHandlerService;
        private readonly IPostcodeApiService _postcodeApiService;
        public AddPostcodeToDatabaseSurfaceController(IDataHandlerService dataHandlerService, IPostcodeApiService postcodeApiService, IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
        {
            _dataHandlerService = dataHandlerService;
            _postcodeApiService = postcodeApiService;
        }
        [HttpPost]
        public async Task<IActionResult> Submit(AddPostcodeToDatabaseModel form)
        {
            if (!ModelState.IsValid)
            {
                return CurrentUmbracoPage();
            }

            //Fetch postcode data from API
            var response = await _postcodeApiService.GetPostcodeData(form.Postcode);
            if (response == null)
            {
                ModelState.AddModelError("Error getting postcode data", "The postcode may be invalid.");
                return CurrentUmbracoPage();
            }

            //Write postcode data to DB
            var message = _dataHandlerService.WritePostcodeToDatabase(response);

            TempData["postcodeSuccess"] = message;
            return RedirectToCurrentUmbracoPage();
        }
    }
}
