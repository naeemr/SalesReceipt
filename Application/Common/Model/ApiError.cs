namespace Application.Common.Model;

public class ApiError
{
	public string Code { get; private set; }
	public string Message { get; private set; }
	public string Help { get; private set; }

	public ApiError(string code, string message)
	{
		Message = message;
		Code = code;
	}

	public ApiError(string code, string message, string help)
	{
		Code = code;
		Message = message;
		Help = help;
	}
}
