using Domain.Base;

namespace Domain;

public partial class ReceiptItem : BaseEntity
{
	public int ProductId { get; private set; }
	public int Quantity { get; private set; }
	public decimal TaxAmount { get; private set; }
	public decimal TotalPrice { get; private set; }
	public virtual Receipt Receipt { get; private set; }

	private ReceiptItem() { }

	public ReceiptItem(string productName, int productId, int quantity, decimal price, decimal taxamount)
	{
		ProductId = productId;
		Quantity = quantity;
		TaxAmount = taxamount;
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