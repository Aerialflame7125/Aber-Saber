using System.IO;
using System.Net;
using System.Security.Permissions;

namespace System.Web.Services.Protocols;

/// <summary>Reads return values from HTTP response text for Web service clients implemented using HTTP but without SOAP.</summary>
[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
public class TextReturnReader : MimeReturnReader
{
	private PatternMatcher matcher;

	/// <summary>Initializes an instance.</summary>
	/// <param name="o">A <see cref="T:System.Web.Services.Protocols.PatternMatcher" /> object for the return type of the Web method being invoked.</param>
	public override void Initialize(object o)
	{
		matcher = (PatternMatcher)o;
	}

	/// <summary>Returns an initializer for the specified method.</summary>
	/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the Web method for which the initializer is obtained.</param>
	/// <returns>An initializer for the specified method</returns>
	public override object GetInitializer(LogicalMethodInfo methodInfo)
	{
		return new PatternMatcher(methodInfo.ReturnType);
	}

	/// <summary>Parses text contained in the HTTP response.</summary>
	/// <param name="response">A <see cref="T:System.Net.WebResponse" /> object  containing the output message for an operation.</param>
	/// <param name="responseStream">A <see cref="T:System.IO.Stream" /> whose content is the body of the HTTP response represented by the <paramref name="response" /> parameter.</param>
	/// <returns>An object containing the deserialized Web method return value.</returns>
	public override object Read(WebResponse response, Stream responseStream)
	{
		try
		{
			string text = RequestResponseUtils.ReadResponse(response);
			return matcher.Match(text);
		}
		finally
		{
			response.Close();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.TextReturnReader" /> class. </summary>
	public TextReturnReader()
	{
	}
}
