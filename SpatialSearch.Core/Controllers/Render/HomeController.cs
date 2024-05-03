using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.Extensions.Logging;
using SpatialSearch.Core.Models;
using SpatialSearch.Core.Models.ViewModels;
using SpatialSearch.Core.PublishedModels;
using System.Text.Json;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;

namespace SpatialSearch.Core.Controllers.Render
{
    public class HomeController : RenderController
    {
        private readonly IVariationContextAccessor _variationContextAccessor;
        private readonly ServiceContext _serviceContext;
        public HomeController(ILogger<RenderController> logger, ICompositeViewEngine compositeViewEngine, IUmbracoContextAccessor umbracoContextAccessor, IVariationContextAccessor variationContextAccessor, ServiceContext serviceContext) : base(logger, compositeViewEngine, umbracoContextAccessor)
        {
            _variationContextAccessor = variationContextAccessor;
            _serviceContext = serviceContext;
        }
        public override IActionResult Index()
        {
            if (CurrentPage is not null)
            {
                var currentPage = CurrentPage as Home;
                HomeViewModel model = new(currentPage, new PublishedValueFallback(_serviceContext, _variationContextAccessor));

                if (TempData.ContainsKey("PostcodeData"))
                {
                    var results = TempData["PostcodeData"].ToString();
                    var deserializedResults = JsonSerializer.Deserialize<List<PostcodeResultsModel>>(results);

                    model.Results = deserializedResults;
                }

                return CurrentTemplate(model);
            }
            else
            {
                throw new Exception("Cannot get current page content.");
            }
        }
    }
}