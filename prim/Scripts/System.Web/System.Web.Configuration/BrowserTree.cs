using System.Collections.Specialized;

namespace System.Web.Configuration;

internal class BrowserTree : OrderedDictionary
{
	internal BrowserTree()
		: base(StringComparer.OrdinalIgnoreCase)
	{
	}
}
