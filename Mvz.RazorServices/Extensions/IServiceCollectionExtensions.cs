using Microsoft.Extensions.DependencyInjection.Extensions;
using Mvz.RazorServices;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection RegisterPdfBuilder(this IServiceCollection services)
        {
            services.TryAddTransient<IPdfBuilder, PdfBuilder>();
            return services;
        }
    }
}
