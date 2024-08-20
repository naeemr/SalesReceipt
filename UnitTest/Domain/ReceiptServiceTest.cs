using Domain;
using Domain.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using static Domain.Product;

namespace UnitTest.Domain;

public class ReceiptServiceTest
{
	ProductCategory booksProductCategory = new ProductCategory("Books", true);
	ProductCategory FoodProductCategory = new ProductCategory("Food", true);
	ProductCategory MusicCDsProductCategory = new ProductCategory("Music CDs", false);
	ProductCategory PerfumeProductCategory = new ProductCategory("Perfumes", false);
	ProductCategory MedicalProductCategory = new ProductCategory("Medical Products", true);

	[Fact]
	public void GenerateReceipt_AddProductsWithoutImportDuty_ReturnReceipt()
	{
		//Arrange
		var builder = new Builder();

		var product1 = builder.WithName("Book").WithPrice(Convert.ToDecimal(12.49)).IsImported(false)
			.WithProductCategory(booksProductCategory).Build();

		var product2 = builder.WithName("Chocolate Bar").WithPrice(Convert.ToDecimal(0.85)).IsImported(false)
			.WithProductCategory(FoodProductCategory).Build();

		var product3 = builder.WithName("Music CD").WithPrice(Convert.ToDecimal(14.99)).IsImported(false)
			.WithProductCategory(MusicCDsProductCategory).Build();

		List<(Product, int)> products = new List<(Product, int)>();
		products.Add((product1, 1));
		products.Add((product2, 1));
		products.Add((product3, 1));

		//Act
		var receiptService = new ReceiptService();
		var receipt = receiptService.GenerateReceipt(products, "001");

		//Assert
		receipt.Should().NotBeNull();

		receipt.ReceiptItems.Should().NotBeNull();

		receipt.ReceiptItems.Sum(s => s.TotalPrice).Should().Be(Convert.ToDecimal(29.83));

		receipt.ReceiptItems.Sum(s => s.SalesTax.Amount).Should().Be(Convert.ToDecimal(1.5));
	}

	[Fact]
	public void GenerateReceipt_AddProductsWithImportDuty_ReturnReceipt()
	{
		//Arrange
		var builder = new Builder();

		var product1 = builder.WithName("Imported Chocolates").WithPrice(Convert.ToDecimal(10.00)).IsImported(true)
			.WithProductCategory(FoodProductCategory).Build();

		var product2 = builder.WithName("Imported Perfume").WithPrice(Convert.ToDecimal(47.50)).IsImported(true)
			.WithProductCategory(PerfumeProductCategory).Build();

		List<(Product, int)> products = new List<(Product, int)>();
		products.Add((product1, 1));
		products.Add((product2, 1));

		//Act
		var receiptService = new ReceiptService();
		var receipt = receiptService.GenerateReceipt(products, "001");

		//Assert
		receipt.Should().NotBeNull();

		receipt.ReceiptItems.Should().NotBeNull();

		receipt.ReceiptItems.Sum(s => s.TotalPrice).Should().Be(Convert.ToDecimal(65.15));

		receipt.ReceiptItems.Sum(s => s.SalesTax.Amount).Should().Be(Convert.ToDecimal(7.65));
	}

	[Fact]
	public void GenerateReceipt_AddProductsBothWithImportAndWithoutImportDuty_ReturnReceipt()
	{
		//Arrange	
		var builder = new Builder();

		var product1 = builder.WithName("Imported Chocolates").WithPrice(Convert.ToDecimal(11.25)).IsImported(true)
			.WithProductCategory(FoodProductCategory).Build();

		var product2 = builder.WithName("Perfume").WithPrice(Convert.ToDecimal(18.99)).IsImported(false)
			.WithProductCategory(PerfumeProductCategory).Build();

		var product3 = builder.WithName("Headache Pills").WithPrice(Convert.ToDecimal(9.75)).IsImported(false)
			.WithProductCategory(MedicalProductCategory).Build();

		var product4 = builder.WithName("Imported Perfume").WithPrice(Convert.ToDecimal(27.99)).IsImported(true)
			.WithProductCategory(PerfumeProductCategory).Build();

		List<(Product, int)> products = new List<(Product, int)>();
		products.Add((product1, 1));
		products.Add((product2, 1));
		products.Add((product3, 1));
		products.Add((product4, 1));

		//Act
		var receiptService = new ReceiptService();
		var receipt = receiptService.GenerateReceipt(products, "001");

		//Assert
		receipt.Should().NotBeNull();

		receipt.ReceiptItems.Should().NotBeNull();

		receipt.ReceiptItems.Sum(s => s.TotalPrice).Should().Be(Convert.ToDecimal(74.68));

		receipt.ReceiptItems.Sum(s => s.SalesTax.Amount).Should().Be(Convert.ToDecimal(6.7));
	}

	[Fact]
	public void GenerateReceipt_EmptyProductList_ReturnReceipt()
	{
		//Arrange
		List<(Product, int)> products = new List<(Product, int)>();

		//Act
		var receiptService = new ReceiptService();
		var receipt = receiptService.GenerateReceipt(products, "001");

		//Assert
		receipt.Should().BeNull();
	}

	[Fact]
	public void GenerateReceipt_NullProductList_ReturnReceipt()
	{
		//Arrange
		List<(Product, int)> products = default;

		//Act
		var receiptService = new ReceiptService();
		var receipt = receiptService.GenerateReceipt(products, "001");

		//Assert
		receipt.Should().BeNull();
	}
}
