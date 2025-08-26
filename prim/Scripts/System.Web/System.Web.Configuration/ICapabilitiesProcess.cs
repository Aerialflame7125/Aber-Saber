using System.Collections;
using System.Collections.Specialized;

namespace System.Web.Configuration;

internal interface ICapabilitiesProcess
{
	CapabilitiesResult Process(string userAgent, IDictionary initialCapabilities);

	CapabilitiesResult Process(HttpRequest request, IDictionary initialCapabilities);

	CapabilitiesResult Process(NameValueCollection header, IDictionary initialCapabilities);
}
