using System.Collections.Generic;

namespace Application.Common.Model;

public class ApiResponse<T>
	where T : class
{
	public bool IsSuccess { get; private set; } = true;
	public T Result { get; private set; }
	public List<ApiError> Errors { get; private set; }

	public ApiResponse()
	{
		Errors = new List<ApiError>();
	}

	public ApiResponse(T result)
	{
		Result = result;
	}

	public void AddError(ApiError error)
	{
		IsSuccess = false;
		Errors.Add(error);
	}
}
