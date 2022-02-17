using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mvz.RazorServices
{
	public static class IRazorTemplateRendererExtensions
	{
		public static async Task<string> RenderViewAsync<TModel>(this IRazorTemplateRenderer renderer, string name, TModel model)
		{
			using var ms = new MemoryStream();
			await renderer.RenderViewAsync(name, model, ms, Encoding.Unicode);
			ms.Seek(0, SeekOrigin.Begin);
			return Encoding.Unicode.GetString(ms.GetBuffer(), 0, (int)ms.Length);
		}
	}
}
