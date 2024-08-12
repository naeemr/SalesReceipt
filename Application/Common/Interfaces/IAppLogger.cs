using System;

namespace Application.Common.Interfaces
{
	public interface IAppLogger<T>
	{

		void SetTransactionId(string transactionId);

		void AddWarning(object data, string message, params object[] args);

		void AddDebug(object data, string message, params object[] args);

		void AddInfo(object data, string message, params object[] args);

		void AddTrace(object data, string message, params object[] args);

		void AddError(object data, string message, params object[] args);

		void AddFatal(object data, string message, params object[] args);

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
