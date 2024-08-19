using Domain.Base;
using System.Collections.Generic;

namespace Domain;

public class ProductCategory : BaseEntity, IAggregateRoot
{
	public string Name { get; private set; }
	public bool IsTaxExempt { get; private set; } //true if product is exempted for sales tax, otherwise fals
	public virtual ICollection<Product> Products { get; private set; }

	private ProductCategory() { }

	public ProductCategory(string name, bool isTaxExempt)
	{
		Name = name;
		IsTaxExempt = isTaxExempt;
	}

	/// <summary>
	/// This will be auto-generated in the database, but it is included as a 
	/// constructor parameter to facilitate the creation of in-memory data.
	/// </summary>
	public ProductCategory(int id, string name, bool isTaxExempt) : base(id)
	{
		Name = name;
		IsTaxExempt = isTaxExempt;
	}
}
