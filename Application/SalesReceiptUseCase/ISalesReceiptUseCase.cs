using Application.Sales.Request;
using Application.Sales.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Sales;

public interface ISalesReceiptUseCase
{
	Task<PrintReceipt> PrintReceipt(IEnumerable<CartItem> cartItems);
}
