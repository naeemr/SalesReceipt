﻿using Microsoft.Extensions.Logging;
using System;

namespace Infrastructure;

public class Logger<T> : Application.Common.Interfaces.ILogger<T> where T : class
{
	private readonly ILogger<T> _logger;
	private readonly Application.Common.Interfaces.IJsonHelper _jsonHelper;

	public Logger(Microsoft.Extensions.Logging.ILogger<T> logger, Application.Common.Interfaces.IJsonHelper jsonHelper)
	{
		_logger = logger;
		_jsonHelper = jsonHelper;
	}

	/// <summary>
	/// Log Debug message
	/// </summary>
	/// <param name="message">Debug message</param>
	/// <param name="args">we can add multiple arguments to string formatter</param>
	public void LogDebug(string message, params object[] args)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			_logger.LogDebug(message, args);
		}
	}

	/// <summary>
	/// Log Debug message
	/// </summary>
	/// <param name="message">debug message</param>
	/// <param name="data">This would be use only when want to trace JSON data.</param>
	public void LogDebug(string message, object data)
	{
		if (_logger.IsEnabled(LogLevel.Debug))
		{
			var json = _jsonHelper.SerializeFormattedObject(data);

			_logger.LogDebug(message, json);
		}
	}

	/// <summary>
	/// Log Error message
	/// </summary>
	/// <param name="message">Error message</param>
	/// <param name="args">we can add multiple arguments to string formatter</param>
	public void LogError(string message, params object[] args)
	{
		if (_logger.IsEnabled(LogLevel.Error))
		{
			_logger.LogError(message, args);
		}
	}

	/// <summary>
	/// Log error message with the data
	/// </summary>
	/// <param name="message">Error message</param>
	/// <param name="data">This would be use only when want to log json formatted data.</param>
	public void LogError(string message, object data)
	{
		if (_logger.IsEnabled(LogLevel.Error))
		{
			var json = _jsonHelper.SerializeFormattedObject(data);

			_logger.LogError(message, json);
		}
	}

	/// <summary>
	/// Log Error message with exception
	/// </summary>
	/// <param name="exception">Application Exception</param>
	/// <param name="message">Error message</param>
	/// <param name="args">we can add multiple arguments to string formatter</param>
	public void LogError(Exception exception, string message, params object[] args)
	{
		if (_logger.IsEnabled(LogLevel.Error))
		{
			_logger.LogError(exception, message, args);

			//Task.Run(async () => await ServiceLocator.Current.GetInstance<IEmailNotification>()
			//.SendEmailOnError(message, exception)).Wait();
		}
	}

	/// <summary>
	/// Log error message with the data
	/// </summary>
	/// <param name="message">Error message</param>
	/// <param name="data">This would be use only when want to log json formatted data.</param>
	public void LogError(Exception exception, string message, object data)
	{
		if (_logger.IsEnabled(LogLevel.Error))
		{
			var json = _jsonHelper.SerializeFormattedObject(data);

			_logger.LogError(exception, message, json);

			//Task.Run(async () => await ServiceLocator.Current.GetInstance<IEmailNotification>()
			//.SendEmailOnError(message, exception)).Wait();
		}
	}

	/// <summary>
	/// Log Critical Error message with exception
	/// </summary>
	/// <param name="exception">Application Exception</param>
	/// <param name="message">Error message</param>
	/// <param name="args">we can add multiple arguments to string formatter</param>
	public void LogFatal(Exception exception, string message, params object[] args)
	{
		if (_logger.IsEnabled(LogLevel.Critical))
		{
			_logger.LogCritical(exception, message, args);
		}
	}

	/// <summary>
	/// Log Critical Error message with exception
	/// </summary>
	/// <param name="message">Error message</param>
	/// <param name="args">we can add multiple arguments to string formatter</param>
	public void LogFatal(string message, params object[] args)
	{
		if (_logger.IsEnabled(LogLevel.Critical))
		{
			_logger.LogCritical(message, args);
		}
	}

	/// <summary>
	/// Log Information message
	/// </summary>
	/// <param name="message">Information message</param>
	/// <param name="args">we can add multiple arguments to string formatter</param>
	public void LogInfo(string message, params object[] args)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			_logger.LogInformation(message, args);
		}
	}

	/// <summary>
	/// Log Info message
	/// </summary>
	/// <param name="message">Info message</param>
	/// <param name="data">This would be use only when want data for information.</param>
	public void LogInfo(string message, object data)
	{
		if (_logger.IsEnabled(LogLevel.Information))
		{
			var json = _jsonHelper.SerializeFormattedObject(data);

			_logger.LogInformation(message, json);
		}
	}

	/// <summary>
	/// Log Trace message
	/// </summary>
	/// <param name="message">Trace message</param>
	/// <param name="args">we can add multiple arguments to string formatter</param>
	public void LogTrace(string message, params object[] args)
	{
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			_logger.LogTrace(message, args);
		}
	}

	/// <summary>
	/// Log Trace message
	/// </summary>
	/// <param name="message">Trace message</param>
	/// <param name="data">This would be use only when want to trace JSON data.</param>
	public void LogTrace(string message, object data)
	{
		if (_logger.IsEnabled(LogLevel.Trace))
		{
			var json = _jsonHelper.SerializeFormattedObject(data);

			_logger.LogTrace(message, json);
		}
	}

	/// <summary>
	/// Log Warning message
	/// </summary>
	/// <param name="message">Warning message</param>
	/// <param name="args">we can add multiple arguments to string formatter</param>
	public void LogWarning(string message, params object[] args)
	{
		if (_logger.IsEnabled(LogLevel.Warning))
		{
			_logger.LogWarning(message, args);
		}
	}

	/// <summary>
	/// Log Warning message
	/// </summary>
	/// <param name="message">Warning message</param>
	/// <param name="data">This would be use only when want to trace JSON data.</param>
	public void LogWarning(string message, object data)
	{
		if (_logger.IsEnabled(LogLevel.Warning))
		{
			var json = _jsonHelper.SerializeFormattedObject(data);

			_logger.LogWarning(message, json);
		}
	}
}
