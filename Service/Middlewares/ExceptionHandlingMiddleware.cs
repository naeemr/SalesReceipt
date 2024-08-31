using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Infrastructure;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Service.Middlewares;

public class ExceptionHandlingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly JsonHelper _jsonHelper;
	private IAppLogger<ExceptionHandlingMiddleware> _logger;

	public ExceptionHandlingMiddleware(RequestDelegate next,
		JsonHelper jsonHelper,
		IAppLogger<ExceptionHandlingMiddleware> logger)
	{
		_next = next;
		_jsonHelper = jsonHelper;
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(context, ex).ConfigureAwait(false);
		}
	}

	private Task HandleExceptionAsync(HttpContext context, Exception exception)
	{
		var message = "An error has occurred, Please try again and if the error persists. " +
				"Contact the system administrator for assistance.";

		if (exception.GetType() == typeof(SqlException))
		{
			var sqlError = (SqlException)exception;

			if (sqlError.Number == 2 || sqlError.Number == 53)
			{
				message = string.Format("A SQL server '{0}' network connection error occurred, please " +
					"look into it on priority.", sqlError.Server);

				_logger.LogFatal(sqlError, message);
			}
			else
			{
				message = string.Format("The following error occurred in the infrastructure layer for the " +
					"SQL server '{0}'.", sqlError.Server);

				_logger.LogError(sqlError, message);
			}
		}
		else
		{
			_logger.LogError(exception, "The following error has been occurred.");
		}

		var statusCode = HttpStatusCode.InternalServerError; // 500 if unexpected
		var result = _jsonHelper.SerializeObject(new { error = exception.Message });
		context.Response.ContentType = "application/json";
		context.Response.StatusCode = (int)statusCode;
		return context.Response.WriteAsync(result);
	}
}
