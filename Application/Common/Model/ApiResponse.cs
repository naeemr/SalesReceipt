using System.Collections.Generic;

namespace Application.Common.Model;

public class ApiResponse<T>
	where T : class
{
	public int? Id { get; private set; }
	public bool IsSuccess { get; private set; } = true;
	public T Result { get; private set; }
	public List<ApiError> Errors { get; private set; }

	private ApiResponse(T result, int id)
	{
		Id = id;
		Result = result;
		Errors = new List<ApiError>();
	}

	private ApiResponse(List<ApiError> errors)
	{
		Errors = errors;
	}

	public static ApiResponse<T> Success(T result, int id = default)
	{
		return new ApiResponse<T>(result, id);
	}

	public static ApiResponse<T> Failure(ApiError error)
	{
		List<ApiError> errors = new List<ApiError>() { error };
		return new ApiResponse<T>(errors);
	}

	public static ApiResponse<T> Failure(List<ApiError> errors)
	{
		return new ApiResponse<T>(errors);
	}
}
