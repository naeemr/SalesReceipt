using Application.Common.Interfaces;
using Application.Interfaces;
using Application.SalesReceipt;
using Application.SalesReceipt.Model;
using Domain;
using Domain.Services;
using FluentAssertions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Domain.Product;

namespace UnitTest.Application;

public class SalesReceiptUseCaseTest
{
	ProductCategory booksProductCategory = new ProductCategory("Books", true);
	ProductCategory FoodProductCategory = new ProductCategory("Food", true);
	ProductCategory MusicCDsProductCategory = new ProductCategory("Music CDs", false);
	ProductCategory PerfumeProductCategory = new ProductCategory("Perfumes", false);
	ProductCategory MedicalProductCategory = new ProductCategory("Medical Products", true);

	private ApplicationDbContext GetInMemoryDbContext(string databaseName)
	{
		var options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.UseInMemoryDatabase(databaseName: databaseName)
		.Options;

		var dbContext = new ApplicationDbContext(options);
		return dbContext;
	}

	[Fact]
	public async Task PrintReceipt_AddProductsWithoutImportDuty_ReturnReceipt()
	{
		// Arrange
		var cartItems = new List<CartItem> {
			new CartItem { ProductId = 1, Quantity = 1 },
			new CartItem { ProductId = 2, Quantity = 1 },
			new CartItem { ProductId = 3, Quantity = 1 }
		};

		var dbContext = GetInMemoryDbContext("SalesDB1");

		var builder = new Builder();

		var product1 = builder.WithName("Book").WithPrice(Convert.ToDecimal(12.49)).IsImported(false)
			.WithProductCategory(booksProductCategory).Build();

		var product2 = builder.WithName("Chocolate Bar").WithPrice(Convert.ToDecimal(0.85)).IsImported(false)
			.WithProductCategory(FoodProductCategory).Build();

		var product3 = builder.WithName("Music CD").WithPrice(Convert.ToDecimal(14.99)).IsImported(false)
			.WithProductCategory(MusicCDsProductCategory).Build();

		var products = new List<Product> { product1, product2, product3 };

		using (dbContext)
		{
			dbContext.Products.AddRange(products);
			await dbContext.SaveChangesAsync();

			var mockAppLogger = new Mock<IAppLogger>();
			var repository = new ProductRepository(dbContext, mockAppLogger.Object);

			var receiptService = new ReceiptService();

			//Act
			var salesReceiptUseCase = new SalesReceiptUseCase(repository, receiptService, mockAppLogger.Object);
			var receipt = await salesReceiptUseCase.PrintReceipt(cartItems);

			//Assert
			receipt.Should().NotBeNull();

			receipt.ReceiptItems.Should().NotBeNull();

			receipt.TotalCost.Should().Be(Convert.ToDecimal(29.83));

			receipt.TotalSalesTax.Should().Be(Convert.ToDecimal(1.5));
		}
	}

	[Fact]
	public async Task PrintReceipt_AddProductsWithImportDuty_ReturnReceipt()
	{
		// Arrange
		var cartItems = new List<CartItem> {
			new CartItem { ProductId = 1, Quantity = 1 },
			new CartItem { ProductId = 2, Quantity = 1 }
		};

		var dbContext = GetInMemoryDbContext("SalesDB2");

		var builder = new Builder();

		var product1 = builder.WithName("Imported Chocolates").WithPrice(Convert.ToDecimal(10.00)).IsImported(true)
			.WithProductCategory(FoodProductCategory).Build();

		var product2 = builder.WithName("Imported Perfume").WithPrice(Convert.ToDecimal(47.50)).IsImported(true)
			.WithProductCategory(PerfumeProductCategory).Build();

		var products = new List<Product> { product1, product2 };

		using (dbContext)
		{
			dbContext.Products.AddRange(products);
			await dbContext.SaveChangesAsync();

			var mockAppLogger = new Mock<IAppLogger>();
			var repository = new ProductRepository(dbContext, mockAppLogger.Object);

			var receiptService = new ReceiptService();

			//Act
			var salesReceiptUseCase = new SalesReceiptUseCase(repository, receiptService, mockAppLogger.Object);
			var receipt = await salesReceiptUseCase.PrintReceipt(cartItems);

			//Assert
			receipt.Should().NotBeNull();

			receipt.ReceiptItems.Should().NotBeNull();

			receipt.TotalCost.Should().Be(Convert.ToDecimal(65.15));

			receipt.TotalSalesTax.Should().Be(Convert.ToDecimal(7.65));
		}
	}

	[Fact]
	public async Task PrintReceipt_AddProductsBothWithImportAndWithoutImportDuty_ReturnReceipt()
	{
		// Arrange
		var cartItems = new List<CartItem> {
			new CartItem { ProductId = 1, Quantity = 1 },
			new CartItem { ProductId = 2, Quantity = 1 },
			new CartItem { ProductId = 3, Quantity = 1 },
			new CartItem { ProductId = 4, Quantity = 1 }
		};

		var dbContext = GetInMemoryDbContext("SalesDB3");

		var builder = new Builder();

		var product1 = builder.WithName("Imported Chocolates").WithPrice(Convert.ToDecimal(11.25)).IsImported(true)
			.WithProductCategory(FoodProductCategory).Build();

		var product2 = builder.WithName("Perfume").WithPrice(Convert.ToDecimal(18.99)).IsImported(false)
			.WithProductCategory(PerfumeProductCategory).Build();

		var product3 = builder.WithName("Headache Pills").WithPrice(Convert.ToDecimal(9.75)).IsImported(false)
			.WithProductCategory(MedicalProductCategory).Build();

		var product4 = builder.WithName("Imported Perfume").WithPrice(Convert.ToDecimal(27.99)).IsImported(true)
			.WithProductCategory(PerfumeProductCategory).Build();

		var products = new List<Product> { product1, product2, product3, product4 };

		using (dbContext)
		{
			dbContext.Products.AddRange(products);
			await dbContext.SaveChangesAsync();

			var mockAppLogger = new Mock<IAppLogger>();
			var repository = new ProductRepository(dbContext, mockAppLogger.Object);

			var receiptService = new ReceiptService();

			//Act
			var salesReceiptUseCase = new SalesReceiptUseCase(repository, receiptService, mockAppLogger.Object);
			var receipt = await salesReceiptUseCase.PrintReceipt(cartItems);

			//Assert
			receipt.Should().NotBeNull();

			receipt.ReceiptItems.Should().NotBeNull();

			receipt.TotalCost.Should().Be(Convert.ToDecimal(74.68));

			receipt.TotalSalesTax.Should().Be(Convert.ToDecimal(6.7));
		}
	}
}
