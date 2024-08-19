using System.Collections.Generic;

namespace Domain.Services;

public class ReceiptService
{
	public ReceiptService() { }

	public Receipt GenerateReceipt(List<(Product, int)> productList, string number)
	{
		var receipt = new Receipt(number);

		foreach (var (product, quantity) in productList)
		{
			var salexTax = product.CalculateTotalTax(10, 5);

			var itemPrice = (product.Price + salexTax.Amount) * quantity;

			receipt.AddReceiptItem(product, quantity, itemPrice, salexTax);
		}

		return receipt;
	}
}
