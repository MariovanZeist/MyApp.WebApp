using HiQPdf;

namespace Mvz.RazorServices
{
	public class PdfBuilder : IPdfBuilder
	{
		public Stream CreatePdfStream(string html)
		{
			var htmlToPdfConverter = new HtmlToPdf();
			var ms = new MemoryStream();

			htmlToPdfConverter.Document.Margins = new PdfMargins(30, 30, 30, 30);
			htmlToPdfConverter.Document.PageSize = PdfPageSize.A4;
			htmlToPdfConverter.ConvertHtmlToStream(html, "localhost", ms);
			ms.Position = 0;
			return ms;
		}
	}
}
