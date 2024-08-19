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

public class SalesController : ApiControllerBase
{
	private readonly ISalesReceiptUseCase _salesReceipt;

	public SalesController(ISalesReceiptUseCase salesReceipt,
		IAppLogger appLogger) : base(appLogger)
	{
		_salesReceipt = salesReceipt;
	}

	[HttpPost]
	[Route("printreceipt")]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public async Task<IActionResult> PrintReceipt(IEnumerable<CartItem> cartItems)
	{
		ReadHeaderValue();

		_appLogger.CreateLogger<SalesReceiptUseCase>(TransactionId);

		_appLogger.AddInfo("Receipt printing is started.");

		if (cartItems == null || cartItems.Count() == 0)
		{
			var message = "Cart Item list is empty.";

			_appLogger.AddWarning(message);
			_appLogger.AddInfo("Receipt printing is completed.");
			_appLogger.SendAllLogEvents();

			return InvalidRequest(new ApiError(StatusCodes.Status400BadRequest.ToString(), message));
		}

		var receipt = await _salesReceipt.PrintReceipt(cartItems);

		if (receipt == null || receipt.ReceiptItems.Count() == 0)
		{
			var message = "Receipt is not generated.";

			_appLogger.AddWarning(message);
			_appLogger.AddInfo("Receipt printing is completed.");
			_appLogger.SendAllLogEvents();

			return InvalidRequest(new ApiError(StatusCodes.Status400BadRequest.ToString(), message));
		}

		_appLogger.AddTrace("Receipt is {JsonData}", receipt);

		_appLogger.AddInfo("Receipt printing is completed.");

		_appLogger.SendAllLogEvents();

		return Success(receipt);
	}
}
