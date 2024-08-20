using System;

namespace Domain.ValueObjects;

public class SalesTax
{
	public decimal TaxRate { get; }
	public decimal Amount { get; }

	public SalesTax(decimal taxRate, decimal amount)
	{
		if (taxRate < 0 || taxRate > 100)
			throw new ArgumentException("Tax rate must be between 0 and 100.", nameof(taxRate));

		TaxRate = taxRate;
		Amount = RoundUpToNearestFiveCents(amount);
	}

	/// <summary>
	/// The rounding rules for sales tax are that for a tax rate of n%, a shelf price of p 
	/// contains (np/100 rounded up to the nearest 0.05) amount of sales tax
	/// </summary>
	/// <param name="taxAmount">Sales tax amount</param>
	/// <returns></returns>
	private decimal RoundUpToNearestFiveCents(decimal amount)
	{
		return Math.Ceiling(amount * 20) / 20; // Rounds up to the nearest 0.05
	}

	public static SalesTax Calculate(decimal taxRate, decimal price)
	{
		var taxAmount = (taxRate / 100) * price;
		return new SalesTax(taxRate, taxAmount);
	}

	public override string ToString() => Amount.ToString("0.00");

	// Equality is important for value objects
	public override bool Equals(object obj)
	{
		if (obj is SalesTax other)
		{
			return TaxRate == other.TaxRate && Amount == other.Amount;
		}
		return false;
	}

	public override int GetHashCode() => (TaxRate, Amount).GetHashCode();
}
