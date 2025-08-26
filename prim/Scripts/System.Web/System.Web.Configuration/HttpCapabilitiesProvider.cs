namespace System.Web.Configuration;

/// <summary>Enables you to customize browser definitions. You can also customize the algorithm that identifies the browser based on information in the incoming <see cref="T:System.Web.HttpRequest" />.</summary>
public abstract class HttpCapabilitiesProvider
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpCapabilitiesProvider" /> class.</summary>
	protected HttpCapabilitiesProvider()
	{
	}

	/// <summary>Gets the <see cref="T:System.Web.HttpBrowserCapabilities" /> object for the current browser.</summary>
	/// <param name="request">The current <see cref="T:System.Web.HttpRequest" /> object.</param>
	/// <returns>The <see cref="T:System.Web.HttpBrowserCapabilities" /> object for the current browser.</returns>
	public abstract HttpBrowserCapabilities GetBrowserCapabilities(HttpRequest request);
}
