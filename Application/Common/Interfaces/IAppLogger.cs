using System;

namespace Application.Common.Interfaces
{
	public interface IAppLogger
	{
		void CreateLogger<T>(string transactionId = "") where T : class;

		/// <summary>
		/// This method adds warning logs to an in-memory list, which are then sent to the final 
		/// datasource when the SendAllLogMessages method is called.
		/// </summary>
		/// <param name="message">Message that contains formatters as well.</param>
		/// <param name="args">Args which need to be formatted in the message</param>
		void AddWarning(string message, params object[] args);

		/// <summary>
		/// This method adds warning logs to an in-memory list, which are then sent to the final 
		/// datasource when the SendAllLogMessages method is called.
		/// </summary>
		/// <param name="message">Message that contains formatters as well.</param>
		/// <param name="data">if want to log some json data</param>
		void AddWarning<T>(string message, T data) where T : class;

		/// <summary>
		/// This method adds debug logs to an in-memory list, which are then sent to the final 
		/// datasource when the SendAllLogMessages method is called.
		/// </summary>
		/// <param name="message">Message that contains formatters as well.</param>
		/// <param name="args">Args which need to be formatted in the message</param>
		void AddDebug(string message, params object[] args);

		/// <summary>
		/// This method adds debug logs to an in-memory list, which are then sent to the final 
		/// datasource when the SendAllLogMessages method is called.
		/// </summary>
		/// <param name="message">Message that contains formatters as well.</param>
		/// <param name="data">if want to log some json data</param>
		void AddDebug<T>(string message, T data) where T : class;

		/// <summary>
		/// This method adds info logs to an in-memory list, which are then sent to the final 
		/// datasource when the SendAllLogMessages method is called.
		/// </summary>
		/// <param name="message">Message that contains formatters as well.</param>
		/// <param name="args">Args which need to be formatted in the message</param>
		void AddInfo(string message, params object[] args);

		/// <summary>
		/// This method adds info logs to an in-memory list, which are then sent to the final 
		/// datasource when the SendAllLogMessages method is called.
		/// </summary>
		/// <param name="message">Message that contains formatters as well.</param>
		/// <param name="data">if want to log some json data</param>
		void AddInfo<T>(string message, T data) where T : class;

		/// <summary>
		/// This method adds trace logs to an in-memory list, which are then sent to the final 
		/// datasource when the SendAllLogMessages method is called.
		/// </summary>
		/// <param name="message">Message that contains formatters as well.</param>
		/// <param name="args">Args which need to be formatted in the message</param>
		void AddTrace(string message, params object[] args);

		/// <summary>
		/// This method adds trace logs to an in-memory list, which are then sent to the final 
		/// datasource when the SendAllLogMessages method is called.
		/// </summary>
		/// <param name="message">Message that contains formatters as well.</param>
		/// <param name="data">if want to log some json data</param>
		void AddTrace<T>(string message, T data) where T : class;

		/// <summary>
		/// This method adds error logs to an in-memory list, which are then sent to the final 
		/// datasource when the SendAllLogMessages method is called.
		/// </summary>
		/// <param name="message">Message that contains formatters as well.</param>
		/// <param name="args">Args which need to be formatted in the message</param>
		void AddError(string message, params object[] args);

		/// <summary>
		/// This method adds error logs to an in-memory list, which are then sent to the final 
		/// datasource when the SendAllLogMessages method is called.
		/// </summary>
		/// <param name="message">Message that contains formatters as well.</param>
		/// <param name="data">if want to log some json data</param>
		void AddError<T>(string message, T data) where T : class;

		/// <summary>
		/// Log Debug message
		/// </summary>
		/// <param name="message">Debug message</param>
		/// <param name="args">we can add multiple arguments to string formatter</param>
		void LogDebug(string message, params object[] args);

		/// <summary>
		/// Log Trace message
		/// </summary>
		/// <param name="message">Trace message</param>
		/// <param name="args">we can add multiple arguments to string formatter</param>
		void LogTrace(string message, params object[] args);

		/// <summary>
		/// Log Information message
		/// </summary>
		/// <param name="message">Information message</param>
		/// <param name="args">we can add multiple arguments to string formatter</param>
		void LogInfo(string message, params object[] args);

		/// <summary>
		/// Log Warning message
		/// </summary>
		/// <param name="message">Warning message</param>
		/// <param name="args">we can add multiple arguments to string formatter</param>
		void LogWarning(string message, params object[] args);

		/// <summary>
		/// Log Error message with exception
		/// </summary>
		/// <param name="exception">Application Exception</param>
		/// <param name="message">Error message</param>
		/// <param name="args">we can add multiple arguments to string formatter</param>
		void LogError(Exception exception, string message, params object[] args);

		/// <summary>
		/// Log Critical Error message with exception
		/// </summary>
		/// <param name="exception">Application Exception</param>
		/// <param name="message">Error message</param>
		/// <param name="args">we can add multiple arguments to string formatter</param>
		void LogFatal(Exception exception, string message, params object[] args);

		/// <summary>
		/// Send all in-memory logs to final datasource
		/// </summary>
		void SendAllLogEvents();
	}
}
