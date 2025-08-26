namespace System.Web.Services.Protocols;

/// <summary>Reads incoming request parameters for Web services implemented using HTTP with name-value pairs encoded in the URL's query string rather than as a SOAP message.</summary>
public class UrlParameterReader : ValueCollectionParameterReader
{
	/// <summary>Reads name/value pairs encoded in the query string of an HTTP request into Web method parameter values.</summary>
	/// <param name="request">A <see cref="T:System.Net.WebResponse" /> objectcontaining HTML URL-encoded name/value pairs.</param>
	/// <returns>An array of name/value pairs.</returns>
	public override object[] Read(HttpRequest request)
	{
		return Read(request.QueryString);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.UrlParameterReader" /> class. </summary>
	public UrlParameterReader()
	{
	}
}
