using System.IO;
using System.Net;

namespace System.Web.Services.Protocols;

/// <summary>Provides a common base implementation for readers of incoming response return values for Web service clients implemented using HTTP but without SOAP.</summary>
public abstract class MimeReturnReader : MimeFormatter
{
	/// <summary>When overridden in a derived class, deserializes an HTTP response into a Web method return value.</summary>
	/// <param name="response">A <see cref="T:System.Net.WebResponse" /> object containing the output message for an operation.</param>
	/// <param name="responseStream">A <see cref="T:System.IO.Stream" /> whose content is the body of the HTTP response represented by the <see cref="T:System.Net.WebResponse" /> parameter.</param>
	/// <returns>An HTTP response deserialized into a Web method return value.</returns>
	public abstract object Read(WebResponse response, Stream responseStream);

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.MimeReturnReader" /> class. </summary>
	protected MimeReturnReader()
	{
	}
}
