using Domain.Base;

namespace Domain;

public partial class Product : BaseEntity, IAggregateRoot
{
	public string Name { get; private set; }
	public string Description { get; private set; }
	public int ProductCategoryId { get; private set; }
	public decimal Price { get; private set; }
	public bool IsImported { get; private set; } //true if product is imported and local manufactured.
	public virtual ProductCategory ProductCategory { get; private set; }

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

	/// <summary>
	/// This will be auto-generated in the database, but it is included as a constructor parameter 
	/// to facilitate the creation of in-memory data.
	/// </summary>
	/// <param name="id"></param>
	/// <param name="name"></param>
	/// <param name="productCategoryId"></param>
	/// <param name="price"></param>
	/// <param name="isImported"></param>
	public Product(int id,
		string name,
		string description,
		int productCategoryId,
		decimal price,
		bool isImported) : base(id)
	{
		Name = name;
		Description = description;
		Price = price;
		IsImported = isImported;
		ProductCategoryId = productCategoryId;
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
}