﻿using Application.Common.Interfaces;
using Application.Common.Model;
using Application.Sales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Service.Controllers;

[Route("[controller]")]
[ApiController]
public class ApiControllerBase<T> : ControllerBase
{
	protected readonly IAppLogger<T> _appLogger;
	protected string transactionId = default;

	public ApiControllerBase(IAppLogger<T> appLogger)
	{
		_appLogger = appLogger;
	}

	public override OkObjectResult Ok([ActionResultObjectValue] object value)
	{
		ApiResponse<object> response = new();

		response.Result = value;

		response.StatusCode = StatusCodes.Status200OK;

		return base.Ok(response);
	}

	public override BadRequestObjectResult BadRequest([ActionResultObjectValue] object error)
	{
		ApiResponse<object> response = new();

		response.Error = (ApiError)error;

		response.StatusCode = StatusCodes.Status400BadRequest;

		response.Success = false;

		return base.BadRequest(response);
	}

	public override ConflictObjectResult Conflict([ActionResultObjectValue] object error)
	{
		ApiResponse<object> response = new();

		response.Error = (ApiError)error;

		response.StatusCode = StatusCodes.Status409Conflict;

		response.Success = false;

		return base.Conflict(response);
	}

	protected void ReadHeaderValue()
	{
		if (Request.Headers.ContainsKey("TransactionId"))
			transactionId = Request.Headers["TransactionId"];
	}
}
