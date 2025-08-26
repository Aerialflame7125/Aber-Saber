using System.Collections.Generic;

namespace System.Web;

internal sealed class ExceptionPageTemplateValues
{
	private Dictionary<string, ExceptionPageTemplateFragmentValue> values;

	private Dictionary<string, ExceptionPageTemplateFragmentValue> Values
	{
		get
		{
			if (values == null)
			{
				values = new Dictionary<string, ExceptionPageTemplateFragmentValue>(StringComparer.Ordinal);
			}
			return values;
		}
	}

	public int Count
	{
		get
		{
			if (values != null)
			{
				return values.Count;
			}
			return 0;
		}
	}

	public string Get(string name)
	{
		if (values == null || values.Count == 0 || string.IsNullOrEmpty(name))
		{
			return null;
		}
		if (values.TryGetValue(name, out var value))
		{
			return value.Value;
		}
		return null;
	}

	public void Add(string name, Func<string, string> valueProvider)
	{
		if (string.IsNullOrEmpty(name))
		{
			throw new ArgumentNullException("name");
		}
		if ((valueProvider != null || values != null) && !Values.ContainsKey(name))
		{
			Values[name] = new ExceptionPageTemplateFragmentValue(name, valueProvider);
		}
	}

	public void Add(string name, string value)
	{
		if (string.IsNullOrEmpty(name))
		{
			throw new ArgumentNullException("name");
		}
		if ((value != null || values != null) && !Values.ContainsKey(name))
		{
			Values[name] = new ExceptionPageTemplateFragmentValue(name, value);
		}
	}
}
