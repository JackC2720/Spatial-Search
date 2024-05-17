namespace SpatialSearch.Core.Composers
{

    using Microsoft.Extensions.DependencyInjection;
    using SpatialSearch.Core.Services;
    using SpatialSearch.Core.Services.Interfaces;
    using System.Diagnostics.CodeAnalysis;
    using Umbraco.Cms.Core.Composing;
    using Umbraco.Cms.Core.DependencyInjection;

    [ExcludeFromCodeCoverage]
    public class DiComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddSingleton<IDataHandlerService, DataHandler>();
            builder.Services.AddSingleton<IPostcodeApiService, PostcodeApiService>();
        }
    }
}
