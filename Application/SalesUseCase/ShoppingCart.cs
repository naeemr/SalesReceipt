using Application.Common.Interfaces;
using Application.Interfaces;
using Application.Sales.Request;
using Application.Sales.Response;
using Domain;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Sales;

public class ShoppingCart : IShoppingCart
{
	private readonly IProductRepository _productRepository;
	private readonly ReceiptService _receiptService;
	private readonly IAppLogger<ShoppingCart> _appLogger;

	public ShoppingCart(IProductRepository productRepository,
		ReceiptService receiptService,
		IAppLogger<ShoppingCart> appLogger)
	{
		_productRepository = productRepository;
		_receiptService = receiptService;
		_appLogger = appLogger;
	}

	public async Task<PrintReceipt> PrintReceipt(IEnumerable<CartItem> cartItems)
	{
		var productIds = cartItems.Select(p => p.ProductId).Distinct().ToList();

		_appLogger.AddTrace(default, "ProductIds are {0}", productIds);

		var products = await _productRepository.GetProducts(productIds);

		_appLogger.AddInfo(default, "Total Product found {0}", products?.Count());

		List<(Product, int)> productList = new List<(Product, int)>();

		// Loop through each product id and quantity pair
		foreach (var item in cartItems)
		{
			// Find the product with the corresponding product id
			var product = products.FirstOrDefault(p => p.Id == item.ProductId);

			if (product != null)
			{
				// Add the product and quantity to the list
				productList.Add((product, item.Quantity));
			}
		}

		var receipt = _receiptService.GenerateReceipt(productList, Guid.NewGuid().ToString());

		PrintReceipt printReceipt = new();

		if (receipt != null)
		{
			printReceipt.TotalCost = receipt.ReceiptItems.Sum(r => r.TotalPrice);
			printReceipt.TotalSalesTax = receipt.ReceiptItems.Sum(r => r.TaxAmount);

			printReceipt.ReceiptItems.AddRange(receipt.ReceiptItems.Select(s => s.ToString()).ToList());
		}

		_appLogger.AddTrace(printReceipt, "Receipt is");

		return printReceipt;
	}
}
