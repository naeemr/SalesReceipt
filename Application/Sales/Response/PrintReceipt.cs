using System.Collections.Generic;

namespace Application.Sales.Response
{
	public class PrintReceipt
	{
		public string Number { get; set; }
		public decimal TotalCost { get; set; }
		public decimal TotalSalesTax { get; set; }
		public List<string> ReceiptItems { get; set; }

		public PrintReceipt()
		{
			ReceiptItems = new();
		}
	}
}
