using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Infrastructure;

public class AppLogger<T> : IAppLogger<T> where T : class
{
	/// <summary>
	/// Trace = 0, Debug = 1, Information = 2, Warning = 3, Error = 4, Critical = 5, and None = 6.
	/// When a LogLevel is specified, logging is enabled for messages at the specified level and higher. 
	/// In the preceding JSON, the Default category is logged for Information and higher. For example, Information, 
	/// Warning, Error, and Critical messages are logged. If no LogLevel is specified, logging defaults to the 
	/// Information level
	/// </summary>

	public string TransactionId { get; private set; }
	private readonly ILogger<T> _logger;
	private readonly IJsonHelper _jsonHelper;

	//Logger log = LogManager.GetCurrentClassLogger();

	public List<ApiError> Messages { get; private set; }

	public AppLogger(ILogger<T> logger,
		IJsonHelper jsonHelper)
	{
		_logger = logger;
		_jsonHelper = jsonHelper;
		Messages = new List<ApiError>();
	}

	public void SetTransactionId(string transactionId)
	{
		TransactionId = transactionId;
	}

	public void AddWarning(string message, object data, params object[] args)
	{
		message = message.FormatString(args);
		Messages.Add(new ApiError(LogLevel.Warning, message, data));
	}

	public void AddDebug(string message, object data, params object[] args)
	{
		message = message.FormatString(args);
		Messages.Add(new ApiError(LogLevel.Debug, message, data));
	}

	public void AddInfo(string message, object data, params object[] args)
	{
		message = message.FormatString(args);
		Messages.Add(new ApiError(LogLevel.Information, message, data));
	}

	public void AddTrace(string message, object data, params object[] args)
	{
		message = message.FormatString(args);
		Messages.Add(new ApiError(LogLevel.Trace, message, data));
	}

	public void AddError(string message, object data, params object[] args)
	{
		message = message.FormatString(args);
		Messages.Add(new ApiError(LogLevel.Error, message, data));
	}

	public void AddFatal(string message, object data, params object[] args)
	{
		message = message.FormatString(args);
		Messages.Add(new ApiError(LogLevel.Critical, message, data));
	}

	public void LogAllMessages()
	{
		TransactionId = string.IsNullOrEmpty(TransactionId)
			? Guid.NewGuid().ToString() : TransactionId;

		try
		{
			using (_logger.BeginScope(TransactionId))
			{
				_logger.LogDebug("{TransactionId} | The logs writing is started for the transaction.", TransactionId);

				foreach (var message in Messages)
				{
					var json = message.Data != null ?
						_jsonHelper.SerializeFormattedObject(message.Data) : default;

					string messageText = message.Message;

					messageText = !string.IsNullOrEmpty(messageText) ? "{TransactionId} | " + messageText : " {TransactionId} |";

					if (string.IsNullOrEmpty(json))
					{
						_logger.Log(message.LogLevel, messageText, TransactionId);
					}
					else
					{
						_logger.Log(message.LogLevel, messageText, TransactionId, json);
					}
				}

				_logger.LogDebug("{TransactionId} The logs writing is ended for the transaction.", TransactionId);
			}
		}
		catch (Exception)
		{
			throw;
		}
	}

	/// <summary>
	/// Log Debug message
	/// </summary>
	/// <param name="message">Debug message</param>
	/// <param name="args">we can add multiple arguments to string formatter</param>
	public void LogDebug(string message, params object[] args)
	{
		_logger.LogDebug(message, args);
	}

	/// <summary>
	/// Log Information message
	/// </summary>
	/// <param name="message">Information message</param>
	/// <param name="args">we can add multiple arguments to string formatter</param>
	public void LogInfo(string message, params object[] args)
	{
		_logger.LogInformation(message, args);
	}

	/// <summary>
	/// Log Trace message
	/// </summary>
	/// <param name="message">Trace message</param>
	/// <param name="args">we can add multiple arguments to string formatter</param>
	public void LogTrace(string message, params object[] args)
	{
		_logger.LogTrace(message, args);
	}

	/// <summary>
	/// Log Warning message
	/// </summary>
	/// <param name="message">Warning message</param>
	/// <param name="args">we can add multiple arguments to string formatter</param>
	public void LogWarning(string message, params object[] args)
	{
		_logger.LogWarning(message, args);
	}

	/// <summary>
	/// Log Error message
	/// </summary>
	/// <param name="message">Error message</param>
	/// <param name="args">we can add multiple arguments to string formatter</param>
	public void LogError(Exception exception, string message, params object[] args)
	{
		_logger.LogError(exception, message, args);
	}

	/// <summary>
	/// Log Critical Error message with exception
	/// </summary>
	/// <param name="exception">Application Exception</param>
	/// <param name="message">Error message</param>
	/// <param name="args">we can add multiple arguments to string formatter</param>
	public void LogFatal(Exception exception, string message, params object[] args)
	{
		_logger.LogCritical(exception, message, args);
	}

	/// <summary>
	/// Log Critical Error message with exception
	/// </summary>
	/// <param name="message">Error message</param>
	/// <param name="args">we can add multiple arguments to string formatter</param>
	public void LogFatal(string message, params object[] args)
	{
		_logger.LogCritical(message, args);
	}
}
