using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApp.Models
{
	public class InvoiceMdl
	{
		public int Id { get; set; }
		public int InvoiceNr { get; set; }
		public DateTime InvoiceDate { get; set; }
		public DateTime DueDate { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
		public string ZipCode { get; set; }
		public string City { get; set; }
		public string Contact { get; set; }
		public string CompanyLogo { get; set; }
		public string Remark { get; set; }
		public string DebtorNr { get; set; }
		public IEnumerable<InvoiceLineMdl> Invoicelines { get; set; }
		public decimal TotalExVat => Invoicelines.Sum(il => il.LineTotalEx);
	}
}
