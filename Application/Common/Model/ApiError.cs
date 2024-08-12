using Microsoft.Extensions.Logging;

namespace Application.Common.Model;

public class ApiError
{
	public LogLevel LogLevel { get; private set; }
	public string Code { get; private set; }
	public string Message { get; private set; }
	public string Help { get; private set; }
	public object Data { get; private set; }

	public ApiError(string message, string help = "")
	{
		Message = message;
		Help = help;
	}

	public ApiError(string code, string message, string help = "")
	{
		Code = code;
		Message = message;
		Help = help;
	}

	public ApiError(LogLevel logLevel, string message, object data, string help = "")
	{
		LogLevel = logLevel;
		Message = message;
		Data = data;
		Help = help;
	}
}
