namespace System.Web.Services;

internal class Soap
{
	internal class Attribute
	{
		internal const string MustUnderstand = "mustUnderstand";

		internal const string Actor = "actor";

		internal const string EncodingStyle = "encodingStyle";

		internal const string Lang = "lang";

		internal const string ConformsTo = "conformsTo";

		private Attribute()
		{
		}
	}

	internal class Element
	{
		internal const string Envelope = "Envelope";

		internal const string Header = "Header";

		internal const string Body = "Body";

		internal const string Fault = "Fault";

		internal const string FaultActor = "faultactor";

		internal const string FaultCode = "faultcode";

		internal const string FaultDetail = "detail";

		internal const string FaultString = "faultstring";

		internal const string StackTrace = "StackTrace";

		internal const string Message = "Message";

		internal const string Claim = "Claim";

		private Element()
		{
		}
	}

	internal class Code
	{
		internal const string Server = "Server";

		internal const string VersionMismatch = "VersionMismatch";

		internal const string MustUnderstand = "MustUnderstand";

		internal const string Client = "Client";

		private Code()
		{
		}
	}

	internal const string XmlNamespace = "http://www.w3.org/XML/1998/namespace";

	internal const string Encoding = "http://schemas.xmlsoap.org/soap/encoding/";

	internal const string Namespace = "http://schemas.xmlsoap.org/soap/envelope/";

	internal const string ConformanceClaim = "http://ws-i.org/schemas/conformanceClaim/";

	internal const string BasicProfile1_1 = "http://ws-i.org/profiles/basic/1.1";

	internal const string Action = "SOAPAction";

	internal const string ArrayType = "Array";

	internal const string Prefix = "soap";

	internal const string ClaimPrefix = "wsi";

	internal const string DimeContentType = "application/dime";

	internal const string SoapContentType = "text/xml";

	private Soap()
	{
	}
}
