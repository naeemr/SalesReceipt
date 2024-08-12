using Newtonsoft.Json;
using System.Collections.Generic;

namespace Application.Common.Interfaces
{
	public interface IJsonHelper
	{
		T DeserializeObject<T>(string value,
			 JsonSerializerSettings settings = null);

		Dictionary<string, T> DeserializeDynamicObject<T>(string value,
			JsonSerializerSettings settings = null);

		string SerializeObject(object value,
			JsonSerializerSettings settings = null);

		string SerializeFormattedObject(object value);

		bool IsValidJsonFor<T>(T schemaType, string jsonString)
			where T : class;

	}
}
