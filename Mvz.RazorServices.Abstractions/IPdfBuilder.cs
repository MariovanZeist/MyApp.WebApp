using System.IO;

namespace Mvz.RazorServices
{
	public interface IPdfBuilder
	{
		Stream CreatePdfStream(string html);
	}
}
