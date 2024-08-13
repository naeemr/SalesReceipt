using Application.Common.Interfaces;
using Application.Common.Model;
using Application.Sales;
using Application.Sales.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Controllers;

public class SalesController : ApiControllerBase<ShoppingCart>
{
	private readonly IShoppingCart _shoppingCart;

	public SalesController(IShoppingCart shoppingCart,
		IAppLogger<ShoppingCart> appLogger) : base(appLogger)
	{
		_shoppingCart = shoppingCart;
	}

	[HttpPost]
	[Route("printreceipt")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> PrintReceipt(IEnumerable<CartItem> cartItems)
	{
		ReadHeaderValue();
		_appLogger.SetTransactionId(transactionId);

		if (cartItems == null || cartItems.Count() == 0)
		{
			return BadRequest(new ApiError("Cart cannot be empty."));
		}

		_appLogger.AddInfo("Receipt printing is started.", default);

		var receipt = await _shoppingCart.PrintReceipt(cartItems);

		if (receipt == null || receipt.ReceiptItems.Count() == 0)
		{
			return BadRequest(new ApiError("Receipt is not generated."));
		}

		_appLogger.LogAllMessages();

		return Ok(receipt);
	}
}
