using System.Collections.Specialized;

namespace System.Web.Configuration;

/// <summary>Used internally at run time by the configuration system to contain the names of the available adapters used to render server controls on different browsers. </summary>
[Serializable]
public class AdapterDictionary : OrderedDictionary
{
	/// <summary>Used internally at run time by the configuration system to get or set a specified adapter name.</summary>
	/// <param name="key">Key of the specified adapter.</param>
	/// <returns>The name of the specified adapter.</returns>
	public string this[string key]
	{
		get
		{
			return (string)base[key];
		}
		set
		{
			base[key] = value;
		}
	}

	/// <summary>Used internally at run time by the configuration system to create a new instance of this class.</summary>
	public AdapterDictionary()
	{
	}
}
