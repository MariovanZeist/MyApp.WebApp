using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Mvz.RazorServices;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IMvcCoreBuilderExtensions
    {
		public static IMvcBuilder AddRazorTemplateRenderer(this IMvcBuilder builder)
		{
			builder.Services.TryAddEnumerable(ServiceDescriptor.Transient<IConfigureOptions<RazorViewEngineOptions>, RazorTemplateRendererOptionsSetup>());
			builder.Services.TryAddScoped<IRazorTemplateRenderer, RazorTemplateRenderer>();
			return builder;
		}

		class RazorTemplateRendererOptionsSetup : IConfigureOptions<RazorViewEngineOptions>
		{
			public void Configure(RazorViewEngineOptions options)
				=> options.ViewLocationExpanders.Add(new RazorTemplateViewLocationExpander());
		}

		class RazorTemplateViewLocationExpander : IViewLocationExpander
		{
			public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
				=> viewLocations.Concat(new[] { "/Templates/{1}/{0}.cshtml" });

			public void PopulateValues(ViewLocationExpanderContext context)
			{
			}
		}


	}
}
