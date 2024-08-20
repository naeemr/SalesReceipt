using Application.SalesReceipt.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.SalesReceipt;

public interface ISalesReceiptUseCase
{
	Task<PrintReceipt> PrintReceipt(IEnumerable<CartItem> cartItems);
}
