using Application.Common.Interfaces;
using Domain;
using FluentAssertions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using static Domain.Product;

namespace UnitTest.Infrastructure;

public class ProductRepositoryTest
{
	private ApplicationDbContext GetInMemoryDbContext()
	{
		var options = new DbContextOptionsBuilder<ApplicationDbContext>()
			.UseInMemoryDatabase(databaseName: "SalesDBForInfrastructure")
		.Options;

		var dbContext = new ApplicationDbContext(options);
		return dbContext;
	}

	[Fact]
	public async Task GetProducts_WhenProductIdsAreValid_ShouldReturnProducts()
	{
		// Arrange
		var productIds = new List<int> { 1, 2, 3 };
		var dbContext = GetInMemoryDbContext();

		var products = BuildProducts();

		using (dbContext)
		{
			dbContext.Products.AddRange(products);
			await dbContext.SaveChangesAsync();

			foreach (var product in products)
			{
				product.ProductCategory.RemoveCyclicReference();
			}

			var mockAppLogger = new Mock<IAppLogger>();
			var repository = new ProductRepository(dbContext, mockAppLogger.Object);

			// Act
			var result = await repository.GetProducts(productIds);

			foreach (var product in result)
			{
				product.ProductCategory.RemoveCyclicReference();
			}

			// Assert
			result.Should().BeEquivalentTo(products);
		}
	}

	[Fact]
	public async Task GetProducts_WhenExceptionIsThrown_ShouldLogError()
	{
		// Arrange
		var productIds = new List<int> { 1, 2, 3 };
		var dbContext = GetInMemoryDbContext();

		var mockLogger = new Mock<IAppLogger>();

		// Simulate an error by disposing the context before querying
		var products = BuildProducts();

		using (dbContext)
		{
			foreach (var product in products)
			{
				dbContext.Products.Add(product);
			}

			await dbContext.SaveChangesAsync();

			// Act
			var productRepository = new ProductRepository(dbContext, mockLogger.Object);

			// Simulate exception by forcing one in the repository method
			dbContext.Database.EnsureDeleted();

			var exception = new Exception("Value cannot be null. (Parameter 'source')");
			mockLogger.Setup(l => l.LogError(It.IsAny<Exception>(), It.IsAny<string>()))
				.Throws(exception);

			dbContext.Products = null;

			Func<Task> act = async () => await productRepository.GetProducts(productIds);

			await act.Should().ThrowAsync<Exception>().WithMessage("Value cannot be null. (Parameter 'source')");
		}

		mockLogger.Verify(l => l.LogError(
			It.Is<ArgumentNullException>(e => e.Message.Contains("Value cannot be null. (Parameter 'source')")),
			It.Is<string>(s => s.Contains("GetProducts: ProductIds are 1,2,3"))),
			Times.Once);
	}

	private List<Product> BuildProducts()
	{
		ProductCategory booksProductCategory = new ProductCategory("Books", true);
		ProductCategory FoodProductCategory = new ProductCategory("Food", true);
		ProductCategory MusicCDsProductCategory = new ProductCategory("Music CDs", false);

		var builder = new Builder();

		var product1 = builder.WithName("Book").WithPrice(Convert.ToDecimal(12.49)).IsImported(false)
			.WithProductCategory(booksProductCategory).Build();

		var product2 = builder.WithName("Chocolate Bar").WithPrice(Convert.ToDecimal(0.85)).IsImported(false)
			.WithProductCategory(FoodProductCategory).Build();

		var product3 = builder.WithName("Music CD").WithPrice(Convert.ToDecimal(14.99)).IsImported(false)
			.WithProductCategory(MusicCDsProductCategory).Build();

		var products = new List<Product> { product1, product2, product3 };

		return products;
	}
}
