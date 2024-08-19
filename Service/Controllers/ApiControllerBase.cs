using Application.Common.Interfaces;
using Application.Common.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections;
using System.Collections.Generic;

namespace Service.Controllers;

[Route("[controller]")]
[ApiController]
public class ApiControllerBase : ControllerBase
{
	protected readonly IAppLogger _appLogger;
	protected string TransactionId { get; set; }

	public ApiControllerBase(IAppLogger appLogger)
	{
		_appLogger = appLogger;
	}

	protected IActionResult Success([ActionResultObjectValue] object value)
	{
		ApiResponse<object> response = new(value);

		return base.Ok(response);
	}

	protected IActionResult InvalidRequest([ActionResultObjectValue] object value)
	{

		ApiResponse<object> response = new();

		if (IsList(value))
		{
			foreach (var error in (List<ApiError>)value)
			{
				response.AddError(error);
			}

			return base.BadRequest(response);
		}

		var apiError = (ApiError)value;

		response.AddError(apiError);

		return base.BadRequest(response);
	}

	protected IActionResult ConcurrencyError([ActionResultObjectValue] object value)
	{
		ApiResponse<object> response = new();

		var error = (ApiError)value;

		response.AddError(error);

		return base.Conflict(response);
	}

	protected void ReadHeaderValue()
	{
		if (Request.Headers.ContainsKey("TransactionId"))
			TransactionId = Request.Headers["TransactionId"];
	}

	private bool IsList(object value)
	{
		if (value == null) return false;

		return value is IList &&
			   value.GetType().IsGenericType &&
			   value.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
	}
}
