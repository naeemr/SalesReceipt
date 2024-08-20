using Domain.ValueObjects;
using FluentAssertions;
using System;
using Xunit;

namespace UnitTest.Domain;

public class SalesTaxTest
{
	[Theory]
	[InlineData(10, 14.99, 1.50)]
	[InlineData(15, 47.50, 7.15)]
	[InlineData(5, 10.00, 0.5)]
	[InlineData(0, 14.99, 0)]
	public void CalculateTax_TaxRateAndPrice_ReturnSalesTaxAmount(int taxRate, decimal price, decimal output)
	{
		//Act
		var salesTax = SalesTax.Calculate(taxRate, price);

		//Assert
		salesTax.Amount.Should().Be(output);
	}

	[Theory]
	[InlineData(-5, 20.00)]
	public void CalculateTax_NegativeTaxRateValue_ReturnSalesTaxAmount(int taxRate, decimal price)
	{
		//Act
		Action act = () => SalesTax.Calculate(taxRate, price);

		//Assert
		act.Should().Throw<ArgumentException>()
		  .WithMessage("Tax rate must be between 0 and 100. (Parameter 'taxRate')");

	}
}
