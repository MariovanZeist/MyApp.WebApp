using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Mvz.RazorServices
{
	public interface IRazorTemplateRenderer
	{
		Task RenderViewAsync<TModel>(string name, TModel model, Stream output, Encoding encoding);
	}
}
