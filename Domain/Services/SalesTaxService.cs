namespace Domain.Services;

public class SalesTaxService
{
	private readonly SalesTax salesTax;

	public SalesTaxService()
	{
		salesTax = new SalesTax(0.10m, 0.05m);
	}

	public decimal CalculateTotalTax(Product product)
	{
		decimal totalSalesTax = 0;

		if (!product.IsTaxExempt)
		{
			totalSalesTax += salesTax.CalculateBasicTax(product.Price);
		}

		if (product.IsImported)
		{
			totalSalesTax += salesTax.CalculateImportDutyTax(product.Price);
		}

		return totalSalesTax;
	}
}
