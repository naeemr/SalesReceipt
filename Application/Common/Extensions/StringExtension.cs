using System;
using System.Text.RegularExpressions;

namespace Application.Common.Extensions;

public static class StringExtension
{
	/// <summary>
	/// this will replace the formatted value with argument repectively like 
	/// api/example/{companyId} is input value and output value will be api/example/1
	/// </summary>
	/// <param name="value">value which need to be format</param>
	/// <param name="args">number of arguments which will replace formatted values</param>
	/// <returns>Formatted string</returns>
	/// <CreatedBy>Naeem Raza</CreatedBy>
	public static string FormatString(this string value, params object[] args)
	{
		try
		{
			Regex regex = new System.Text.RegularExpressions.Regex(@"\{.*?\}");
			MatchCollection mc = regex.Matches(value);
			int count = 0;
			foreach (Match obj in mc)
			{
				if (args.Length > count)
				{
					if (args[count] != null)
						value = value.Replace(obj.Value, args[count].ToString());
					// If any element of arguments is null then replace with "Null" 
					//string. Null or Empty string is not allowed in uri
					// Add respective handling later on in code.
					else value = value.Replace(obj.Value, System.Net.WebUtility.UrlEncode("Null"));
					count += 1;
				}
			}
		}
		catch (Exception)
		{
			throw;
		}

		return value;
	}
}
