namespace System.Web.Configuration;

/// <summary>Used internally at run time by <see cref="T:System.Web.Configuration.BrowserCapabilitiesFactory" /> and <see cref="T:System.Web.Configuration.BrowserCapabilitiesCodeGenerator" /> to parse request data and identify the browser.</summary>
public class RegexWorker
{
	/// <summary>Accessor to this class.</summary>
	/// <param name="key">The specified key.</param>
	/// <returns>The internal value associated with the specified<paramref name="key" />.</returns>
	[MonoTODO("Mono does not currently need this routine. Not implemented.")]
	public string this[string key]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Creates a new instance of <see cref="T:System.Web.Configuration.RegexWorker" />.</summary>
	/// <param name="browserCaps">The <see cref="T:System.Web.Configuration.HttpCapabilitiesBase" /> object to be configured.</param>
	public RegexWorker(HttpBrowserCapabilities browserCaps)
	{
	}

	/// <summary>Used internally at run time to determine whether the specified request-header value matches any of the capabilities of an internal collection of browsers.</summary>
	/// <param name="target">The capabilities value from an internal collection of browsers.</param>
	/// <param name="regexExpression">The specified request-header value.</param>
	/// <returns>
	///     <see langword="true" /> if the specified request-header value matches any of the capabilities of an internal collection of browsers; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[MonoTODO("Mono does not currently need this routine.  Always returns false.")]
	public bool ProcessRegex(string target, string regexExpression)
	{
		return false;
	}
}
