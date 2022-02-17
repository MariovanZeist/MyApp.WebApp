using Microsoft.AspNetCore.Mvc;
using Mvz.RazorServices;
using MyApp.Models;

namespace MyApp.WebApp.Controllers
{
	[ApiController]
	public class PdfController : ControllerBase
	{
		private readonly IRazorTemplateRenderer _razorTemplateRenderer;
		private readonly IPdfBuilder _pdfBuilder;

		public PdfController(IRazorTemplateRenderer razorTemplateRenderer, IPdfBuilder pdfBuilder)
		{
			_razorTemplateRenderer = razorTemplateRenderer;
			_pdfBuilder = pdfBuilder;
		}


		[HttpGet]
		[Route("api/download/pdf/{dummyText}")]
		public async Task<IActionResult> Get(string dummyText)
		{
			var stream = await CreateInvoice(CreateDummyInvoice(dummyText));
			Response.RegisterForDispose(stream);
			if (stream != null)
			{
				return new FileStreamResult(stream, "application/pdf");
			}
			return StatusCode(404);
		}


		//Here we create the Invoice from a Model
		private async Task<Stream> CreateInvoice(InvoiceMdl invoice)
		{
			var html = await _razorTemplateRenderer.RenderViewAsync("Invoice", invoice);
			var stream = _pdfBuilder.CreatePdfStream(html);
			stream.Position = 0;
			return stream;
		}


		//Just some random data
		private InvoiceMdl CreateDummyInvoice(string dummyText)
		{
			var invoice = new InvoiceMdl
			{
				Name = dummyText,
				Address = "Streetname",
				City = "Hometown",
				InvoiceNr = 123456,
				Remark = "Nice invoice",
				ZipCode = "1122zh",
				CompanyLogo = "Logo.png",
				InvoiceDate = DateTime.Now,
				DueDate = DateTime.Now.AddDays(14),
				DebtorNr = "DN10000"
			};

			invoice.Invoicelines = new[]
			{
				new InvoiceLineMdl { AmountEx=100, Quantity=1, Description="Item 1" },
				new InvoiceLineMdl { AmountEx=9.25M, Quantity=3, Description="Another Item" }
			};
			return invoice;
		}

	}
}
