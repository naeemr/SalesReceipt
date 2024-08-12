using Domain.Base;
using System.Collections.Generic;
using System.Linq;

namespace Domain;

public partial class Receipt : BaseEntity, IAggregateRoot
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

	public void AddReceiptItem(Product product, int quantity, decimal itemPrice, decimal taxAmount)
	{
		var receiptItem = new ReceiptItem(product.Name, product.Id, quantity, itemPrice, taxAmount);
		ReceiptItems.Add(receiptItem);
	}
}

