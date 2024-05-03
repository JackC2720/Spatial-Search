using SpatialSearch.Core.PublishedModels;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace SpatialSearch.Core.Models.ViewModels
{
    public class HomeViewModel : Home
    {
        public HomeViewModel(Home content, IPublishedValueFallback publishedValueFallback) : base(content, publishedValueFallback)
        {
            Content = content;
        }
        public Home Content { get; set; }
        public List<PostcodeResultsModel> Results { get; set; }
    }
}
