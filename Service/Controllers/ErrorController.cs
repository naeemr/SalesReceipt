using Application.Common.Interfaces;
using Application.Common.Model;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace GedLawyers.Inspire.CMS.Services.Controllers
{
	[ApiController]
	public class ErrorController : ControllerBase
	{
		private readonly ILogger<ErrorController> _logger;

		public ErrorController(ILogger<ErrorController> logger)
		{
			_logger = logger;
		}

		[Route("/error")]
		[HttpGet]
		public ApiError Error()
		{
			var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
			var error = context.Error;

			var message = "An error has occurred, Please try again and if the error persists. " +
				"Contact the system administrator for assistance.";

			if (error.GetType() == typeof(SqlException))
			{
				var sqlError = (SqlException)error;
				if (sqlError.Number == 2 || sqlError.Number == 53)
				{
					message = string.Format("A SQL server '{0}' network connection error occurred, please look into it on priority.", sqlError.Server);
					_logger.LogFatal(sqlError, message);
				}
				else
				{
					message = string.Format("The following error occurred in the infrastructure layer for the SQL server '{0}'.", sqlError.Server);
					_logger.LogError(sqlError, message);
				}
			}
			else
			{
				_logger.LogError(error, "The following error has been occurred.");
			}

			ApiError response = new ApiError(StatusCodes.Status400BadRequest, message);
			return response;
		}
	}
}

