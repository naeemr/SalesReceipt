using Domain.Base;
using Domain.ValueObjects;
using System.Collections.Generic;

namespace Domain;

public class Receipt : BaseEntity, IAggregateRoot
{
	public string Number { get; private set; }
	public virtual ICollection<ReceiptItem> ReceiptItems { get; private set; }

	private Receipt()
	{
		ReceiptItems = new List<ReceiptItem>();
	}

	public Receipt(string number)
	{
		ReceiptItems = new List<ReceiptItem>();
	}

	public void AddReceiptItem(Product product,
		int quantity,
		decimal itemPrice,
		SalesTax salesTax)
	{
		var receiptItem = new ReceiptItem(product.Name, product.Id, quantity, itemPrice, salesTax);
		ReceiptItems.Add(receiptItem);
	}
}

