using Application.Common.Interfaces;
using Application.Common.Model;
using Application.SalesReceipt;
using Application.SalesReceipt.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service.Controllers;

public class SalesController : ApiControllerBase<SalesController>
{
	private readonly ISalesReceiptUseCase _salesReceipt;

	public SalesController(ISalesReceiptUseCase salesReceipt,
		IAppLogger<SalesController> appLogger) : base(appLogger)
	{
		_salesReceipt = salesReceipt;
	}

	[HttpPost]
	[Route("printreceipt")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> PrintReceipt(IEnumerable<CartItem> cartItems)
	{
		ReadHeaderValue();

		_appLogger.LogInfo("Receipt printing is started.");

		if (cartItems == null || cartItems.Count() == 0)
		{
			var message = "Cart Item list is empty.";

			_appLogger.LogWarning(message);
			_appLogger.LogInfo("Receipt printing is completed.");

			return InvalidRequest(new ApiError(StatusCodes.Status400BadRequest.ToString(), message));
		}

		var receipt = await _salesReceipt.PrintReceipt(cartItems);

		if (receipt == null || receipt.ReceiptItems.Count() == 0)
		{
			var message = "Receipt is not generated.";

			_appLogger.LogWarning(message);
			_appLogger.LogInfo("Receipt printing is completed.");

			return InvalidRequest(new ApiError(StatusCodes.Status400BadRequest.ToString(), message));
		}

		_appLogger.LogTrace("Receipt is {JsonData}", receipt);

		_appLogger.LogInfo("Receipt printing is completed.");

		return Success(receipt);
	}
}
