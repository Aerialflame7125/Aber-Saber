using System.Collections;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace System.Web.Configuration;

internal class CapabilitiesResult : HttpBrowserCapabilities
{
	public StringCollection Keys
	{
		get
		{
			string[] array = new string[base.Capabilities.Keys.Count];
			base.Capabilities.Keys.CopyTo(array, 0);
			Array.Sort(array);
			StringCollection stringCollection = new StringCollection();
			stringCollection.AddRange(array);
			return stringCollection;
		}
	}

	public string UserAgent => this[""];

	internal CapabilitiesResult(IDictionary items)
	{
		base.Capabilities = items;
		base.Capabilities["browsers"] = new ArrayList();
	}

	internal void AddCapabilities(string name, string value)
	{
		base.Capabilities[name] = value;
	}

	internal void AddMatchingBrowserId(string id)
	{
		if (base.Capabilities["browsers"] is ArrayList arrayList && !arrayList.Contains(id))
		{
			arrayList.Add(id);
		}
	}

	internal virtual string Replace(string item)
	{
		if (item.IndexOf('$') > -1)
		{
			MatchCollection matchCollection = Regex.Matches(item, "\\$\\{(?'Capability'\\w*)\\}");
			if (matchCollection.Count == 0)
			{
				return item;
			}
			for (int i = 0; i <= matchCollection.Count - 1; i++)
			{
				if (matchCollection[i].Success)
				{
					string text = matchCollection[i].Result("${Capability}");
					item = item.Replace("${" + text + "}", this[text]);
				}
			}
		}
		if (item.IndexOf('%') > -1)
		{
			MatchCollection matchCollection2 = Regex.Matches(item, "\\%\\{(?'Capability'\\w*)\\}");
			if (matchCollection2.Count == 0)
			{
				return item;
			}
			for (int j = 0; j <= matchCollection2.Count - 1; j++)
			{
				if (matchCollection2[j].Success)
				{
					string text2 = matchCollection2[j].Result("${Capability}");
					item = item.Replace("%{" + text2 + "}", this[text2]);
				}
			}
		}
		return item;
	}
}
