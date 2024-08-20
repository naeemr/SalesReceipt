using Application.Common.Model;
using Application.SalesReceipt.Model;
using FluentAssertions;
using IntegrationTest.Infrastructure;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace IntegrationTest;

public class SalesReceiptTest : IntegrationTestBase
{
	private readonly ITestOutputHelper _output;

	public SalesReceiptTest(IntegrationTestFixture integrationTestFixture,
		ITestOutputHelper output)
		: base(integrationTestFixture, output)
	{
		_output = output;
	}

	[Fact]
	public async Task PrintReceipt_CartItems1_ReturnReceipt()
	{
		//Arrange     
		List<CartItem> items = new List<CartItem>()
		{
			new CartItem{ProductId =1, Quantity = 1},
			new CartItem{ProductId =5, Quantity = 1},
			new CartItem{ProductId =2, Quantity = 1},
		};

		//Act
		var requestBuilder = NewRequest.AddRoute("sales/printreceipt");

		var response = await requestBuilder.Post(items);

		//Assert
		response.Should().NotBeNull();

		var content = await response.Content.ReadAsStringAsync();

		var deserilizedResponse = _jsonHelper.DeserializeObject<ApiResponse<PrintReceipt>>(content);

		deserilizedResponse.Result.ReceiptItems.Should().NotBeNull();

		deserilizedResponse.Result.TotalCost.Should().Be(Convert.ToDecimal(29.83));

		deserilizedResponse.Result.TotalSalesTax.Should().Be(Convert.ToDecimal(1.5));
	}

	[Fact]
	public async Task PrintReceipt_CartItems2_ReturnReceipt()
	{
		//Arrange   
		List<CartItem> items = new List<CartItem>()
		{
			new CartItem{ProductId =3, Quantity = 1},
			new CartItem{ProductId =6, Quantity = 1}
		};

		//Act
		var requestBuilder = NewRequest.AddRoute("sales/printreceipt");

		var response = await requestBuilder.Post(items);

		//Assert
		response.Should().NotBeNull();

		var content = await response.Content.ReadAsStringAsync();

		var deserilizedResponse = _jsonHelper.DeserializeObject<ApiResponse<PrintReceipt>>(content);

		deserilizedResponse.Result.ReceiptItems.Should().NotBeNull();

		deserilizedResponse.Result.TotalCost.Should().Be(Convert.ToDecimal(65.15));

		deserilizedResponse.Result.TotalSalesTax.Should().Be(Convert.ToDecimal(7.65));
	}

	[Fact]
	public async Task PrintReceipt_CartItems3_ReturnReceipt()
	{
		//Arrange     
		List<CartItem> items = new List<CartItem>()
		{
			new CartItem{ProductId =8,  Quantity = 1},
			new CartItem{ProductId =4,  Quantity = 1},
			new CartItem{ProductId =7,  Quantity = 1},
			new CartItem{ProductId =9,  Quantity = 1}
		};

		//Act
		var requestBuilder = NewRequest.AddRoute("sales/printreceipt");

		var response = await requestBuilder.Post(items);

		//Assert
		response.Should().NotBeNull();

		var content = await response.Content.ReadAsStringAsync();

		var deserilizedResponse = _jsonHelper.DeserializeObject<ApiResponse<PrintReceipt>>(content);

		deserilizedResponse.Result.ReceiptItems.Should().NotBeNull();

		deserilizedResponse.Result.TotalCost.Should().Be(Convert.ToDecimal(74.68));

		deserilizedResponse.Result.TotalSalesTax.Should().Be(Convert.ToDecimal(6.7));
	}
}
