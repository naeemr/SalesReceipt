using Domain.Base;
using System.Collections.Generic;

namespace Domain;

public class ProductCategory : BaseEntity, IAggregateRoot
{
	public string Name { get; private set; }
	public bool IsTaxExempt { get; private set; }
	public virtual ICollection<Product> Products { get; private set; }

	private ProductCategory() { }

	public ProductCategory(string name, bool isTaxExempt)
	{
		Name = name;
		IsTaxExempt = isTaxExempt;
	}

	internal void RemoveCyclicReference()
	{
		Products = null;
	}
}