using System.IO;
using System.Net;
using System.Text;

namespace System.Web.Services.Protocols;

/// <summary>Writes outgoing request parameters for Web services implemented using HTTP with name-value pairs encoded like an HTML form rather than as a SOAP message.</summary>
public class HtmlFormParameterWriter : UrlEncodedParameterWriter
{
	/// <summary>Gets a value that indicates whether Web method parameter values are serialized to the outgoing HTTP request body.</summary>
	/// <returns>
	///     <see langword="true" /> to indicate that the Web method parameters are serialized. This property always returns <see langword="true" />.</returns>
	public override bool UsesWriteRequest => true;

	/// <summary>Initializes the outgoing HTTP request. </summary>
	/// <param name="request">The outgoing request.</param>
	/// <param name="values">The Web method parameter values.</param>
	public override void InitializeRequest(WebRequest request, object[] values)
	{
		request.ContentType = ContentType.Compose("application/x-www-form-urlencoded", RequestEncoding);
	}

	/// <summary>Serializes Web method parameter values into a stream representing the outgoing HTTP request body.</summary>
	/// <param name="requestStream">An input stream for the outgoing HTTP request's body.</param>
	/// <param name="values">The Web method parameter values.</param>
	public override void WriteRequest(Stream requestStream, object[] values)
	{
		if (values.Length != 0)
		{
			TextWriter textWriter = new StreamWriter(requestStream, new ASCIIEncoding());
			Encode(textWriter, values);
			textWriter.Flush();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.HtmlFormParameterWriter" /> class. </summary>
	public HtmlFormParameterWriter()
	{
	}
}
