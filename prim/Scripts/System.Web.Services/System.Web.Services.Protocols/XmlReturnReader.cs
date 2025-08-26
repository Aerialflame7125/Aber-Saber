using System.IO;
using System.Net;
using System.Text;
using System.Web.Services.Diagnostics;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

/// <summary>Reads return values from XML that is encoded in the body of incoming responses for Web service clients implemented using HTTP but without SOAP.</summary>
public class XmlReturnReader : MimeReturnReader
{
	private XmlSerializer xmlSerializer;

	/// <summary>Initializes an instance.</summary>
	/// <param name="o">An <see cref="T:System.Xml.Serialization.XmlSerializer" /> for the return type of the Web method being invoked.</param>
	public override void Initialize(object o)
	{
		xmlSerializer = (XmlSerializer)o;
	}

	/// <summary>Returns an array of initializer objects corresponding to an input array of method definitions.</summary>
	/// <param name="methodInfos">An array of type <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the Web methods for which the initializers are obtained.</param>
	/// <returns>An array of initializer objects corresponding to an input array of method definitions.</returns>
	public override object[] GetInitializers(LogicalMethodInfo[] methodInfos)
	{
		return XmlReturn.GetInitializers(methodInfos);
	}

	/// <summary>Returns an initializer for the specified method.</summary>
	/// <param name="methodInfo">A <see cref="T:System.Web.Services.Protocols.LogicalMethodInfo" /> that specifies the Web method for which the initializer is obtained.</param>
	/// <returns>An initializer for the specified method.</returns>
	public override object GetInitializer(LogicalMethodInfo methodInfo)
	{
		return XmlReturn.GetInitializer(methodInfo);
	}

	/// <summary>Gets a return value deserialized from an XML document contained in the HTTP response.</summary>
	/// <param name="response">An <see cref="T:System.Web.HttpRequest" /> object containing the output message for an operation.</param>
	/// <param name="responseStream">A <see cref="T:System.IO.Stream" /> whose content is the body of the HTTP response represented by the <paramref name="response" /> parameter.</param>
	/// <returns>A return value deserialized from an XML document contained in the HTTP response.</returns>
	public override object Read(WebResponse response, Stream responseStream)
	{
		try
		{
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			if (!ContentType.MatchesBase(response.ContentType, "text/xml"))
			{
				throw new InvalidOperationException(Res.GetString("WebResultNotXml"));
			}
			Encoding encoding = RequestResponseUtils.GetEncoding(response.ContentType);
			StreamReader streamReader = new StreamReader(responseStream, encoding, detectEncodingFromByteOrderMarks: true);
			TraceMethod caller = (Tracing.On ? new TraceMethod(this, "Read") : null);
			if (Tracing.On)
			{
				Tracing.Enter(Tracing.TraceId("TraceReadResponse"), caller, new TraceMethod(xmlSerializer, "Deserialize", streamReader));
			}
			object result = xmlSerializer.Deserialize(streamReader);
			if (Tracing.On)
			{
				Tracing.Exit(Tracing.TraceId("TraceReadResponse"), caller);
			}
			return result;
		}
		finally
		{
			response.Close();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.XmlReturnReader" /> class. </summary>
	public XmlReturnReader()
	{
	}
}
