using Application.Common.Extensions;
using Application.Common.Interfaces;
using Application.Common.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace Infrastructure;

public class AppLogger : IAppLogger
{
	/// <summary>
	/// Trace = 0, Debug = 1, Information = 2, Warning = 3, Error = 4, Critical = 5, and None = 6.
	/// When a LogLevel is specified, logging is enabled for messages at the specified level and higher. 
	/// In the preceding JSON, the Default category is logged for Information and higher. For example, Information, 
	/// Warning, Error, and Critical messages are logged. If no LogLevel is specified, logging defaults to the 
	/// Information level
	/// </summary>

	public string TransactionId { get; private set; }
	public ILogger Logger { get; private set; }
	private readonly ILoggerFactory _factory;
	private readonly IJsonHelper _jsonHelper;

	public List<LogEvent> Messages { get; private set; }

	public AppLogger(ILoggerFactory factory,
		IJsonHelper jsonHelper)
	{
		_factory = factory;
		_jsonHelper = jsonHelper;
		Messages = new List<LogEvent>();
	}

	public void CreateLogger<T>(string transactionId = "") where T : class
	{
		Logger = _factory.CreateLogger<T>();

		TransactionId = string.IsNullOrEmpty(transactionId)
			? Guid.NewGuid().ToString() : transactionId;
	}

	public void AddWarning(string message, params object[] args)
	{
		if (!Logger.IsEnabled(LogLevel.Warning)) return;

		message = message.FormatString(args);
		Messages.Add(new LogEvent(LogLevel.Warning, message));

	}

	public void AddWarning<T>(string message, T data) where T : class
	{
		if (!Logger.IsEnabled(LogLevel.Warning)) return;

		Messages.Add(new LogEvent(LogLevel.Warning, message, data));
	}

	public void AddDebug(string message, params object[] args)
	{
		if (!Logger.IsEnabled(LogLevel.Debug)) return;

		message = message.FormatString(args);
		Messages.Add(new LogEvent(LogLevel.Debug, message));
	}

	public void AddDebug<T>(string message, T data) where T : class
	{
		if (!Logger.IsEnabled(LogLevel.Debug)) return;

		Messages.Add(new LogEvent(LogLevel.Debug, message, data));
	}

	public void AddInfo(string message, params object[] args)
	{
		if (!Logger.IsEnabled(LogLevel.Information)) return;

		message = message.FormatString(args);
		Messages.Add(new LogEvent(LogLevel.Information, message));
	}

	public void AddInfo<T>(string message, T data) where T : class
	{
		if (!Logger.IsEnabled(LogLevel.Information)) return;

		Messages.Add(new LogEvent(LogLevel.Information, message, data));
	}

	public void AddTrace(string message, params object[] args)
	{
		if (!Logger.IsEnabled(LogLevel.Trace)) return;

		message = message.FormatString(args);
		Messages.Add(new LogEvent(LogLevel.Trace, message));
	}

	public void AddTrace<T>(string message, T data) where T : class
	{
		if (!Logger.IsEnabled(LogLevel.Trace)) return;

		Messages.Add(new LogEvent(LogLevel.Trace, message, data));
	}

	public void AddError(string message, params object[] args)
	{
		if (!Logger.IsEnabled(LogLevel.Error)) return;

		message = message.FormatString(args);
		Messages.Add(new LogEvent(LogLevel.Error, message));
	}

	public void AddError<T>(string message, T data) where T : class
	{
		if (!Logger.IsEnabled(LogLevel.Error)) return;

		Messages.Add(new LogEvent(LogLevel.Error, message, data));
	}

	public void LogDebug(string message, params object[] args)
	{
		Logger.LogDebug(message, args);
	}

	public void LogInfo(string message, params object[] args)
	{
		Logger.LogInformation(message, args);
	}

	public void LogTrace(string message, params object[] args)
	{
		Logger.LogTrace(message, args);
	}

	public void LogWarning(string message, params object[] args)
	{
		Logger.LogWarning(message, args);
	}

	public void LogError(Exception exception, string message, params object[] args)
	{
		Logger.LogError(exception, message, args);
	}

	public void LogFatal(Exception exception, string message, params object[] args)
	{
		Logger.LogCritical(exception, message, args);
	}

	public void SendAllLogEvents()
	{
		try
		{
			using (Logger.BeginScope(TransactionId))
			{
				Logger.LogDebug("{TransactionId} | The logs writing is started for the transaction.", TransactionId);

				foreach (var message in Messages)
				{
					var json = message.Data != null ?
						_jsonHelper.SerializeFormattedObject(message.Data) : default;

					string messageText = message.Message;

					messageText = !string.IsNullOrEmpty(messageText) ? "{TransactionId} | " + messageText : " {TransactionId} |";

					if (string.IsNullOrEmpty(json))
					{
						Logger.Log(message.LogLevel, messageText, TransactionId);
					}
					else
					{
						Logger.Log(message.LogLevel, messageText, TransactionId, json);
					}
				}

				Logger.LogDebug("{TransactionId} The logs writing is ended for the transaction.", TransactionId);
			}
		}
		catch (Exception)
		{
			throw;
		}
	}
}
