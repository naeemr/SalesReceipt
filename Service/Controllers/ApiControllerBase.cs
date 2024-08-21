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
	protected string TransactionId { get; private set; }

	public ApiControllerBase(IAppLogger appLogger)
	{
		_appLogger = appLogger;
	}

	protected IActionResult Success([ActionResultObjectValue] object value)
	{
		return base.Ok(ApiResponse<object>.Success(value));
	}

	protected IActionResult InvalidRequest([ActionResultObjectValue] object value)
	{
		if (IsList(value))
		{
			return base.BadRequest(ApiResponse<object>.Failure((List<ApiError>)value));
		}

		return base.BadRequest(ApiResponse<object>.Failure((ApiError)value));
	}

	protected IActionResult ConcurrencyError([ActionResultObjectValue] object value)
	{
		return base.Conflict(ApiResponse<object>.Failure((ApiError)value));
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
