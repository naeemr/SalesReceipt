using System.Collections.Generic;

namespace Domain.Services;

public class ReceiptService
{
	private readonly SalesTaxService _salesTaxService;

	public ReceiptService(SalesTaxService salesTaxService)
	{
		_salesTaxService = salesTaxService;
	}

	public Receipt GenerateReceipt(List<(Product, int)> productList, string number)
	{
		var receipt = new Receipt(number);

		foreach (var (product, quantity) in productList)
		{
			var itemTax = _salesTaxService.CalculateTotalTax(product);

			var itemPrice = (product.Price + itemTax) * quantity;

			receipt.AddReceiptItem(product, quantity, itemPrice, itemTax);
		}

		return receipt;
	}
}
