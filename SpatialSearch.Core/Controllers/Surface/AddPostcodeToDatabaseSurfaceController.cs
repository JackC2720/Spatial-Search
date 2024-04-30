using Microsoft.AspNetCore.Mvc;
using SpatialSearch.Core.Models;
using System.Text.Json;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using IScopeProvider = Umbraco.Cms.Infrastructure.Scoping.IScopeProvider;

namespace SpatialSearch.Core.Controllers.Surface
{
	public class AddPostcodeToDatabaseSurfaceController : BaseSurfaceController
	{
		private readonly IScopeProvider _scopeProvider;
		public AddPostcodeToDatabaseSurfaceController(IScopeProvider scopeProvider, IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
		{
			_scopeProvider = scopeProvider;
		}
		[HttpPost]
		public async Task<IActionResult> Submit(AddPostcodeToDatabaseModel form)
		{
			if (!ModelState.IsValid)
			{
				return CurrentUmbracoPage();
			}

			//Fetch postcode data from API
			var response = await GetPostcodeData(form.Postcode);
			if (response == null)
			{
				ModelState.AddModelError("Error getting postcode data", "The postcode may be invalid.");
				return CurrentUmbracoPage();
			}

			//Write postcode data to DB
			var message = WritePostcodeToDatabase(response);

			TempData["postcodeSuccess"] = message;
			return RedirectToCurrentUmbracoPage();
		}

		private string WritePostcodeToDatabase(PostcodeSchemaModel postcodeSchema)
		{
			//avoid ambient scope error
			if (!ExecutionContext.IsFlowSuppressed())
			{
				ExecutionContext.SuppressFlow();
			}

			using (var scope = _scopeProvider.CreateScope(autoComplete: false))
			{
				var message = "Postcode already exsists";
				var existingRecord = scope.Database.Fetch<PostcodeSchemaModel>().FirstOrDefault(r => r.Postcode == postcodeSchema.Postcode);
				if (existingRecord == null)
				{
					scope.Database.Insert(postcodeSchema);
					message = "Postcode added successfully";
				}
				scope.Complete();
				return message;
			}
		}
		public async Task<PostcodeSchemaModel?> GetPostcodeData(string postcode)
		{
			var baseUrl = "https://api.postcodes.io/postcodes/";
			var url = baseUrl + postcode;

			using var client = new HttpClient();
			HttpResponseMessage response = await client.GetAsync(url);

			if (response.IsSuccessStatusCode)
			{
				var contentString = await response.Content.ReadAsStringAsync();
				var responseModel = JsonSerializer.Deserialize<PostcodeApiResponseModel>(contentString);

				PostcodeSchemaModel postcodeSchemaModel = new PostcodeSchemaModel()
				{
					Postcode = responseModel.result.postcode,
					X = responseModel.result.latitude,
					Y = responseModel.result.longitude,
				};
				return postcodeSchemaModel;
			}
			else
			{
				return null;
			}
		}
	}
}
