using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System.Text;

namespace Mvz.RazorServices
{
    public class RazorTemplateRenderer : IRazorTemplateRenderer
	{
		private readonly IServiceProvider _serviceProvider;
		private readonly IRazorViewEngine _viewEngine;
		private readonly ITempDataProvider _tempDataProvider;

		public RazorTemplateRenderer(IServiceProvider serviceProvider, IRazorViewEngine viewEngine, ITempDataProvider tempDataProvider)
		{
			_viewEngine = viewEngine;
			_tempDataProvider = tempDataProvider;
			_serviceProvider = serviceProvider;
		}

		public async Task RenderViewAsync<TModel>(string name, TModel model, Stream output, Encoding encoding)
		{
			var actionContext = new ActionContext(new DefaultHttpContext { RequestServices = _serviceProvider }, new RouteData(), new ActionDescriptor());
			var view = FindView(actionContext, name);
			using var writer = new StreamWriter(output, encoding, leaveOpen: true);
			var viewContext = new ViewContext(actionContext, view, new ViewDataDictionary<TModel>(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = model }, new TempDataDictionary(actionContext.HttpContext, _tempDataProvider), writer, new HtmlHelperOptions());
			await view.RenderAsync(viewContext);
		}

		private IView FindView(ActionContext actionContext, string partialName)
		{
			var getPartialResult = _viewEngine.GetView(null, partialName, false);
			if (getPartialResult.Success)
			{
				return getPartialResult.View;
			}
			var findPartialResult = _viewEngine.FindView(actionContext, partialName, false);
			if (findPartialResult.Success)
			{
				return findPartialResult.View;
			}
			var searchedLocations = getPartialResult.SearchedLocations.Concat(findPartialResult.SearchedLocations);
			var errorMessage = string.Join(
				Environment.NewLine,
				new[] { $"Unable to find partial '{partialName}'. The following locations were searched:" }.Concat(searchedLocations)); ;
			throw new InvalidOperationException(errorMessage);
		}
	}
}
