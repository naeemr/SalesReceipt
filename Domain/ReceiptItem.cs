using Domain.Base;
using Domain.ValueObjects;

namespace Domain;

public partial class ReceiptItem : BaseEntity
{
	public int ReceiptId { get; private set; }
	public int ProductId { get; private set; }
	public int Quantity { get; private set; }
	public decimal TotalPrice { get; private set; }
	public SalesTax SalesTax { get; private set; }
	public virtual Receipt Receipt { get; private set; }
	public virtual Product Product { get; private set; }

	private ReceiptItem() { }

	public ReceiptItem(string productName,
		int productId,
		int quantity,
		decimal price,
		SalesTax salesTax)
	{
		ProductId = productId;
		Quantity = quantity;
		SalesTax = salesTax;
		TotalPrice = price;
		ProductName = productName;
	}

	public override string ToString()
	{
		return $"{Quantity} {ProductName}: {TotalPrice}";
	}
}

public partial class ReceiptItem
{
	public string ProductName { get; private set; }
}