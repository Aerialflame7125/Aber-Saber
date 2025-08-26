using System.Net;

namespace System.Web.Services.Protocols;

/// <summary>The base class for XML Web service client proxies that use the HTTP-POST protocol.</summary>
public class HttpPostClientProtocol : HttpSimpleClientProtocol
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.HttpPostClientProtocol" /> class.</summary>
	public HttpPostClientProtocol()
	{
	}

	/// <summary>Creates a <see cref="T:System.Net.WebRequest" /> instance for the specified URI.</summary>
	/// <param name="uri">The <see cref="T:System.Uri" /> to use when creating the <see cref="T:System.Net.WebRequest" />. </param>
	/// <returns>The <see cref="T:System.Net.WebRequest" /> instance.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <paramref name="uri" /> parameter is null or has a length of zero. </exception>
	protected override WebRequest GetWebRequest(Uri uri)
	{
		WebRequest webRequest = base.GetWebRequest(uri);
		webRequest.Method = "POST";
		return webRequest;
	}
}
