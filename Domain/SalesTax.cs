using Domain.Base;
using System;

namespace Domain;

public class SalesTax : BaseEntity
{
	public decimal BasicTax { get; private set; }
	public decimal ImportDutyTax { get; private set; }

	private SalesTax() { }

	public SalesTax(decimal basicTax, decimal importDutyTax)
	{
		BasicTax = basicTax;
		ImportDutyTax = importDutyTax;
	}

	/// <summary>
	/// product price
	/// </summary>
	/// <param name="price"></param>
	/// <returns>SalesTax</returns>
	public decimal CalculateBasicTax(decimal price)
	{
		var taxAmount = price * BasicTax;
		return RoundToNearest(taxAmount);
	}

	/// <summary>
	/// product price
	/// </summary>
	/// <param name="price"></param>
	/// <returns>SalesTax</returns>
	public decimal CalculateImportDutyTax(decimal price)
	{
		var taxAmount = price * ImportDutyTax;
		return RoundToNearest(taxAmount);
	}

	/// <summary>
	/// The rounding rules for sales tax are that for a tax rate of n%, a shelf price of p 
	/// contains (np/100 rounded up to the nearest 0.05) amount of sales tax
	/// </summary>
	/// <param name="taxAmount">Sales tax amount</param>
	/// <returns></returns>
	private decimal RoundToNearest(decimal taxAmount)
		=> Convert.ToDecimal(Math.Ceiling(taxAmount * 20) / 20);
}
