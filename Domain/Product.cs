using Domain.Base;
using Domain.ValueObjects;
using System.Collections.Generic;

namespace Domain;

public partial class Product : BaseEntity, IAggregateRoot
{
	public string Name { get; private set; }
	public string Description { get; private set; }
	public int ProductCategoryId { get; private set; }
	public decimal Price { get; private set; }
	public bool IsImported { get; private set; }
	public virtual ProductCategory ProductCategory { get; private set; }
	public virtual ICollection<ReceiptItem> ReceiptItems { get; private set; }

	private Product() { }

	public Product(
		string name,
		string description,
		int productCategoryId,
		decimal price,
		bool isImported)
	{
		Name = name;
		Description = description;
		Price = price;
		IsImported = isImported;
		ProductCategoryId = productCategoryId;
	}

	public SalesTax CalculateTotalTax(decimal basicTax, decimal importDutyTax)
	{
		decimal taxRate = 0;

		if (!IsTaxExempt)
		{
			taxRate += basicTax;
		}

		if (IsImported)
		{
			taxRate += importDutyTax;
		}

		return SalesTax.Calculate(taxRate, Price);
	}
}

public partial class Product
{
	public bool IsTaxExempt
	{
		get
		{
			return ProductCategory != null ? ProductCategory.IsTaxExempt : false;
		}
	}

	/// <summary>
	/// We are using rich domain model and writing unit tests, it's important to maintain the 
	/// integrity of your domain entities while ensuring your tests are effective. Given that 
	/// Product class has a dependency on ProductCategory for determining if it's tax-exempt.
	/// So, we used nested builder pattern to build product during writing unit tests.
	/// We keep the access of Builder as Internal and set <InternalsVisibleTo Include="UnitTest" /> 
	/// in Domain .csproj file to allow access only for UnitTest project
	/// </summary>
	internal class Builder
	{
		private string _name;
		private string _description;
		private int _productCategoryId;
		private decimal _price;
		private bool _isImported;
		private ProductCategory _productCategory;

		internal Builder WithName(string name)
		{
			_name = name;
			return this;
		}

		internal Builder WithDescription(string description)
		{
			_description = description;
			return this;
		}

		internal Builder WithProductCategory(ProductCategory productCategory)
		{
			_productCategory = productCategory;
			_productCategoryId = productCategory.Id;
			return this;
		}

		internal Builder WithPrice(decimal price)
		{
			_price = price;
			return this;
		}

		internal Builder IsImported(bool isImported)
		{
			_isImported = isImported;
			return this;
		}

		internal Product Build()
		{
			var product = new Product(_name, _description, _productCategoryId, _price, _isImported);
			product.ProductCategory = _productCategory;
			return product;
		}
	}
}