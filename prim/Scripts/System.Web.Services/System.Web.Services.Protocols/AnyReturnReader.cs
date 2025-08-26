using System.IO;
using System.Net;

namespace System.Web.Services.Protocols;

/// <summary>Provides a minimal reader of incoming response return values for Web service clients implemented using HTTP but without SOAP. </summary>
public class AnyReturnReader : MimeReturnReader
{
	/// <summary>Initializes an instance of the <see cref="T:System.Web.Services.Protocols.AnyReturnReader" /> class.</summary>
	/// <param name="o">Another instance of the <see cref="T:System.Web.Services.Protocols.AnyReturnReader" /> class, on which the <see cref="M:System.Web.Services.Protocols.AnyReturnReader.GetInitializer(System.Web.Services.Protocols.LogicalMethodInfo)" /> method was previously called.</param>
	public override void Initialize(object o)
	{
	}

	/// <summary>Returns the parameter passed to the <see cref="M:System.Web.Services.Protocols.AnyReturnReader.Initialize(System.Object)" /> method.</summary>
	/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" />  that indicates the Web method for which the initializer is obtained.</param>
	/// <returns>The parameter passed to the <see cref="M:System.Web.Services.Protocols.AnyReturnReader.Initialize(System.Object)" /> method.</returns>
	public override object GetInitializer(LogicalMethodInfo methodInfo)
	{
		if (methodInfo.IsVoid)
		{
			return null;
		}
		return this;
	}

	/// <summary>Returns the input HTTP response stream.</summary>
	/// <param name="response">A representation of the HTTP response sent by a Web service, containing the output message for an operation.</param>
	/// <param name="responseStream">An output stream whose content is the body of the HTTP response represented by the <paramref name="response" /> parameter.</param>
	/// <returns>The input HTTP response stream.</returns>
	public override object Read(WebResponse response, Stream responseStream)
	{
		return responseStream;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.AnyReturnReader" /> class. </summary>
	public AnyReturnReader()
	{
	}
}
