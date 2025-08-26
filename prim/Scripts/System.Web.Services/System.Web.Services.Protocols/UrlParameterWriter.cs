using System.Globalization;
using System.IO;
using System.Text;

namespace System.Web.Services.Protocols;

/// <summary>Writes out-going request parameters for Web services implemented using HTTP with name-value pairs encoded in the URL's query string rather than as a SOAP message.</summary>
public class UrlParameterWriter : UrlEncodedParameterWriter
{
	/// <summary>Writes Web method parameter values to the query string of an HTTP request.</summary>
	/// <param name="url">The HTTP request's original URL.</param>
	/// <param name="parameters">The Web method parameter values to be added to the URL, if necessary.</param>
	/// <returns>A <see cref="T:System.String" /> object that contains the query string and the parameter values.</returns>
	public override string GetRequestUrl(string url, object[] parameters)
	{
		if (parameters.Length == 0)
		{
			return url;
		}
		StringBuilder stringBuilder = new StringBuilder(url);
		stringBuilder.Append('?');
		TextWriter textWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture);
		Encode(textWriter, parameters);
		textWriter.Flush();
		return stringBuilder.ToString();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.UrlParameterWriter" /> class.</summary>
	public UrlParameterWriter()
	{
	}
}
