using System;

namespace Application.Common.Interfaces
{
	public interface IAppLogger<T>
	{
		void SetTransactionId(string transactionId);

		void AddWarning(string message, object data, params object[] args);

		void AddDebug(string message, object data, params object[] args);

		void AddInfo(string message, object data, params object[] args);

		void AddTrace(string message, object data, params object[] args);

		void AddError(string message, object data, params object[] args);

		void AddFatal(string message, object data, params object[] args);

		void LogAllMessages();

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
		/// <param name="message">Error message</param>
		/// <param name="args">we can add multiple arguments to string formatter</param>
		void LogFatal(string message, params object[] args);

		/// <summary>
		/// Log Critical Error message with exception
		/// </summary>
		/// <param name="exception">Application Exception</param>
		/// <param name="message">Error message</param>
		/// <param name="args">we can add multiple arguments to string formatter</param>
		void LogFatal(Exception exception, string message, params object[] args);
	}
}
