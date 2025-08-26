using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace System.Web.Configuration;

internal abstract class CapabilitiesBuild : ICapabilitiesProcess
{
	protected abstract Collection<string> HeaderNames(Collection<string> list);

	public CapabilitiesResult Process(string userAgent, IDictionary initialCapabilities)
	{
		NameValueCollection nameValueCollection = new NameValueCollection(1);
		nameValueCollection.Add("User-Agent", userAgent);
		return Process(nameValueCollection, initialCapabilities);
	}

	public CapabilitiesResult Process(HttpRequest request, IDictionary initialCapabilities)
	{
		if (request != null)
		{
			return Process(request.Headers, initialCapabilities);
		}
		return Process("", initialCapabilities);
	}

	public abstract CapabilitiesResult Process(NameValueCollection header, IDictionary initialCapabilities);
}
