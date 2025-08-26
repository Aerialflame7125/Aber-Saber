using System.Collections.Generic;
using System.Linq;

namespace System.Web.ModelBinding;

internal static class DictionaryHelpers
{
	public static IEnumerable<KeyValuePair<string, TValue>> FindKeysWithPrefix<TValue>(IDictionary<string, TValue> dictionary, string prefix)
	{
		if (dictionary.TryGetValue(prefix, out var value))
		{
			yield return new KeyValuePair<string, TValue>(prefix, value);
		}
		foreach (KeyValuePair<string, TValue> item in dictionary)
		{
			string key = item.Key;
			if (key.Length > prefix.Length && key.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
			{
				char c = key[prefix.Length];
				if (c == '.' || c == '[')
				{
					yield return item;
				}
			}
		}
	}

	public static bool DoesAnyKeyHavePrefix<TValue>(IDictionary<string, TValue> dictionary, string prefix)
	{
		return FindKeysWithPrefix(dictionary, prefix).Any();
	}
}
