namespace MyApp.Models
{
    public class InvoiceLineMdl
	{
		public int Id { get; set; }
		public int InvoiceId { get; set; }
		public int LineOrder { get; set; }
		public string Description { get; set; }
		public decimal AmountEx { get; set; }
		public decimal Quantity { get; set; }
		public decimal LineTotalEx => AmountEx * Quantity;
	}
}
