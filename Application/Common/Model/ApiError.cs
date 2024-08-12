namespace Application.Common.Model;

public class ApiError
{
	public int ErrorCode { get; private set; }
	public string ErrorMessage { get; private set; }
	public string Help { get; private set; }

	public ApiError()
	{

	}

	public ApiError(string message, string help = "")
	{
		ErrorMessage = message;
		Help = help;
	}

	public ApiError(int code, string message, string help = "")
	{
		ErrorCode = code;
		ErrorMessage = message;
		Help = help;
	}
}
