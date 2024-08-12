using Application.Common.Model;
using Application.Sales;
using Application.Sales.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Controllers;

public class SalesController : ApiControllerBase
{
	private readonly IShoppingCart _shoppingCart;

	public SalesController(IShoppingCart shoppingCart)
	{
		_shoppingCart = shoppingCart;
	}

	[HttpPost]
	[Route("printreceipt")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> PrintReceipt(IEnumerable<CartItem> cartItems)
	{
		if (cartItems == null || cartItems.Count() == 0)
		{
			return BadRequest(new ApiError("Cart cannot be empty."));
		}

		return Ok(await _shoppingCart.PrintReceipt(cartItems));
	}
}
