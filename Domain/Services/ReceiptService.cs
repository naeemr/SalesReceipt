using System.Collections.Generic;

namespace Domain.Services;

public class ReceiptService
{
	public ReceiptService() { }

	public Receipt GenerateReceipt(List<(Product, int)> products, string number)
	{
		if (products == null || products.Count == 0) return default;

		var receipt = new Receipt(number);

		foreach (var (product, quantity) in products)
		{
			var salexTax = product.CalculateTotalTax(10, 5);

			var itemPrice = (product.Price + salexTax.Amount) * quantity;

			receipt.AddReceiptItem(product, quantity, itemPrice, salexTax);
		}

		return receipt;
	}
}
