namespace System.Web.Services.Protocols;

/// <summary>Reads incoming request parameters for Web services implemented using HTTP, with name-value pairs encoded like an HTML form rather than as a SOAP message.</summary>
public class HtmlFormParameterReader : ValueCollectionParameterReader
{
	internal const string MimeType = "application/x-www-form-urlencoded";

	/// <summary>Reads name-value pairs into Web method parameter values.</summary>
	/// <param name="request">An <see cref="T:System.Web.HttpRequest" /> object containing HTML name-value pairs encoded in the body of an HTTP request.</param>
	/// <returns>An array of objects contain the name-value pairs.</returns>
	public override object[] Read(HttpRequest request)
	{
		if (!ContentType.MatchesBase(request.ContentType, "application/x-www-form-urlencoded"))
		{
			return null;
		}
		return Read(request.Form);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.HtmlFormParameterReader" /> class. </summary>
	public HtmlFormParameterReader()
	{
	}
}
