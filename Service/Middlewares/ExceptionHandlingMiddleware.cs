using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;
using System;
using Application.Common.Interfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Data.SqlClient;

namespace Service.Middlewares;

public class ExceptionHandlingMiddleware
{
	private readonly RequestDelegate _next;
	private readonly IAppLogger<ExceptionHandlingMiddleware> _logger;
	private readonly IJsonHelper _jsonHelper;

	public ExceptionHandlingMiddleware(RequestDelegate next, 
		IAppLogger<ExceptionHandlingMiddleware> logger, 
		IJsonHelper jsonHelper)
	{
		_next = next;
		_logger = logger;
		_jsonHelper = jsonHelper;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		try
		{
			await _next(context);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "An unhandled exception has occurred.");
			await HandleExceptionAsync(context, ex);
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
