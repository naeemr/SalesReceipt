using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Threading.Tasks;

namespace Service.Middlewares;

public class CorrelationIdMiddleware
{
	private const string CorrelationIdHeader = "X-Correlation-ID";
	private readonly RequestDelegate _next;

	public CorrelationIdMiddleware(RequestDelegate next)
	{
		_next = next;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		// Check if the correlation ID exists in the incoming request header
		if (!context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
		{
			// Generate a new correlation ID if not present
			correlationId = Guid.NewGuid().ToString();
			context.Request.Headers.Add(CorrelationIdHeader, correlationId);
		}

		// Add the correlation ID to the response header
		context.Response.OnStarting(() =>
		{
			context.Response.Headers[CorrelationIdHeader] = correlationId;
			return Task.CompletedTask;
		});

		// Add the correlation ID to the NLog logging context
		using (ScopeContext.PushProperty("CorrelationId", correlationId.ToString().Trim('"')))
		{
			context.Items["CorrelationId"] = correlationId; // Store in HttpContext for later use
			await _next(context); // Continue to the next middleware
		}
	}
}
