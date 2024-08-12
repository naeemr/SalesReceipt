using System;

namespace Application.Common.Interfaces
{
	public interface IAppLogger<T>
	{
		/// <summary>
		/// Log Debug message
		/// </summary>
		/// <param name="message">Debug message</param>
		/// <param name="args">we can add multiple arguments to string formatter</param>
		void LogDebug(string message, params object[] args);

		/// <summary>
		/// Log Debug message
		/// </summary>
		/// <param name="message">debug message</param>
		/// <param name="data">This would be use only when want to trace JSON data.</param>
		void LogDebug(string message, object data);

		/// <summary>
		/// Log Trace message
		/// </summary>
		/// <param name="message">Trace message</param>
		/// <param name="args">we can add multiple arguments to string formatter</param>
		void LogTrace(string message, params object[] args);

		/// <summary>
		/// Log Trace message
		/// </summary>
		/// <param name="message">Trace message</param>
		/// <param name="data">This would be use only when want to trace JSON data.</param>
		void LogTrace(string message, object data);

		/// <summary>
		/// Log Information message
		/// </summary>
		/// <param name="message">Information message</param>
		/// <param name="args">we can add multiple arguments to string formatter</param>
		void LogInfo(string message, params object[] args);

		/// <summary>
		/// Log Info message
		/// </summary>
		/// <param name="message">Info message</param>
		/// <param name="data">This would be use only when want data for information.</param>
		void LogInfo(string message, object data);

		/// <summary>
		/// Log Warning message
		/// </summary>
		/// <param name="message">Warning message</param>
		/// <param name="args">we can add multiple arguments to string formatter</param>
		void LogWarning(string message, params object[] args);

		/// <summary>
		/// Log Warning message
		/// </summary>
		/// <param name="message">Warning message</param>
		/// <param name="data">This would be use only when want to trace JSON data.</param>
		void LogWarning(string message, object data);

		/// <summary>
		/// Log Error message
		/// </summary>
		/// <param name="message">Error message</param>
		/// <param name="args">we can add multiple arguments to string formatter</param>
		void LogError(string message, params object[] args);

		/// <summary>
		/// Log error message with the data
		/// </summary>
		/// <param name="message">Error message</param>
		/// <param name="data">This would be use only when want to log json formatted data.</param>
		void LogError(string message, object data);

		/// <summary>
		/// Log error message with the data
		/// </summary>
		/// <param name="message">Error message</param>
		/// <param name="data">This would be use only when want to log json formatted data.</param>
		void LogError(Exception exception, string message, object data);

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
