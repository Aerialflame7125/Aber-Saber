using System.IO;
using System.Net;

namespace System.Web.Services.Protocols;

/// <summary>Serves as a non-acting reader of incoming response return values for Web service clients implemented using HTTP but without SOAP.</summary>
public class NopReturnReader : MimeReturnReader
{
	/// <summary>Returns an initializer for the specified method.</summary>
	/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the Web method for which the initializer is obtained.</param>
	/// <returns>An initializer for the specified method.</returns>
	public override object GetInitializer(LogicalMethodInfo methodInfo)
	{
		return this;
	}

	/// <summary>Initializes an instance.</summary>
	/// <param name="initializer">Another instance of the <see cref="T:System.Web.Services.Protocols.NopReturnReader" /> class, on which the <see cref="M:System.Web.Services.Protocols.NopReturnReader.GetInitializer(System.Web.Services.Protocols.LogicalMethodInfo)" /> method was previously called.</param>
	public override void Initialize(object initializer)
	{
	}

	/// <summary>Returns <see langword="null" /> instead of deserializing the HTTP response stream into a Web method return value.</summary>
	/// <param name="response">A <see cref="T:System.Net.WebResponse" /> object  containing the output message for an operation.</param>
	/// <param name="responseStream">A <see cref="T:System.IO.Stream" /> whose content is the body of the HTTP response represented by the <paramref name="response" /> parameter.</param>
	/// <returns>
	///     <see langword="null" />.</returns>
	public override object Read(WebResponse response, Stream responseStream)
	{
		response.Close();
		return null;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.NopReturnReader" /> class. </summary>
	public NopReturnReader()
	{
	}
}
