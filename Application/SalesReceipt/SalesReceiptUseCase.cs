using Application.Common.Interfaces;
using Application.Interfaces;
using Application.SalesReceipt.Model;
using Domain;
using Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.SalesReceipt;

public class SalesReceiptUseCase : ISalesReceiptUseCase
{
	private readonly IProductRepository _productRepository;
	private readonly ReceiptService _receiptService;
	private readonly IAppLogger _appLogger;

	public SalesReceiptUseCase(IProductRepository productRepository,
		ReceiptService receiptService,
		IAppLogger appLogger)
	{
		_productRepository = productRepository;
		_receiptService = receiptService;
		_appLogger = appLogger;
	}

	public async Task<PrintReceipt> PrintReceipt(IEnumerable<CartItem> cartItems)
	{
		var productIds = cartItems.Select(p => p.ProductId).Distinct().ToList();

		_appLogger.AddTrace(string.Format("ProductIds are {0}", string.Join(',', productIds.Select(s => s))));

		try
		{
			var products = await _productRepository.GetProducts(productIds);

			_appLogger.AddInfo(string.Format("Total Product found {0}", products?.Count()));

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

				printReceipt.TotalSalesTax = receipt.ReceiptItems.Sum(r => r.SalesTax.Amount);

				printReceipt.ReceiptItems.AddRange(receipt.ReceiptItems.Select(s => s.ToString()).ToList());
			}

			return printReceipt;
		}
		catch (Exception ex)
		{
			_appLogger.LogError(ex, string.Format("GetProducts: ProductIds are {0}",
				string.Join(',', productIds.Select(s => s))));

			throw;
		}
	}
}
