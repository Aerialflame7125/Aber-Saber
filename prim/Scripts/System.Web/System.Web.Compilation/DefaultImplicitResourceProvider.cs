using System.Collections;
using System.Globalization;
using System.Resources;

namespace System.Web.Compilation;

internal class DefaultImplicitResourceProvider : IImplicitResourceProvider
{
	private IResourceProvider _resourceProvider;

	private IDictionary _implicitResources;

	private bool _attemptedGetPageResources;

	internal DefaultImplicitResourceProvider(IResourceProvider resourceProvider)
	{
		_resourceProvider = resourceProvider;
	}

	public virtual object GetObject(ImplicitResourceKey entry, CultureInfo culture)
	{
		string resourceKey = ConstructFullKey(entry);
		return _resourceProvider.GetObject(resourceKey, culture);
	}

	public virtual ICollection GetImplicitResourceKeys(string keyPrefix)
	{
		EnsureGetPageResources();
		if (_implicitResources == null)
		{
			return null;
		}
		return (ICollection)_implicitResources[keyPrefix];
	}

	internal void EnsureGetPageResources()
	{
		if (_attemptedGetPageResources)
		{
			return;
		}
		_attemptedGetPageResources = true;
		IResourceReader resourceReader = _resourceProvider.ResourceReader;
		if (resourceReader == null)
		{
			return;
		}
		_implicitResources = new Hashtable(StringComparer.OrdinalIgnoreCase);
		foreach (DictionaryEntry item in resourceReader)
		{
			ImplicitResourceKey implicitResourceKey = ParseFullKey((string)item.Key);
			if (implicitResourceKey != null)
			{
				ArrayList arrayList = (ArrayList)_implicitResources[implicitResourceKey.KeyPrefix];
				if (arrayList == null)
				{
					arrayList = new ArrayList();
					_implicitResources[implicitResourceKey.KeyPrefix] = arrayList;
				}
				arrayList.Add(implicitResourceKey);
			}
		}
	}

	private static ImplicitResourceKey ParseFullKey(string key)
	{
		string filter = string.Empty;
		if (key.IndexOf(':') > 0)
		{
			string[] array = key.Split(':');
			if (array.Length > 2)
			{
				return null;
			}
			filter = array[0];
			key = array[1];
		}
		int num = key.IndexOf('.');
		if (num <= 0)
		{
			return null;
		}
		string keyPrefix = key.Substring(0, num);
		string property = key.Substring(num + 1);
		return new ImplicitResourceKey
		{
			Filter = filter,
			KeyPrefix = keyPrefix,
			Property = property
		};
	}

	private static string ConstructFullKey(ImplicitResourceKey entry)
	{
		string text = entry.KeyPrefix + "." + entry.Property;
		if (entry.Filter.Length > 0)
		{
			text = entry.Filter + ":" + text;
		}
		return text;
	}
}
