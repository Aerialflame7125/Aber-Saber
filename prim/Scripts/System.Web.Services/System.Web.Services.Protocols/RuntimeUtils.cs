using System.Globalization;
using System.IO;
using System.Web.Services.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

internal class RuntimeUtils
{
	private RuntimeUtils()
	{
	}

	internal static XmlDeserializationEvents GetDeserializationEvents()
	{
		XmlDeserializationEvents result = default(XmlDeserializationEvents);
		result.OnUnknownElement = OnUnknownElement;
		result.OnUnknownAttribute = OnUnknownAttribute;
		return result;
	}

	private static void OnUnknownAttribute(object sender, XmlAttributeEventArgs e)
	{
		if (e.Attr == null || IsKnownNamespace(e.Attr.NamespaceURI))
		{
			return;
		}
		Tracing.OnUnknownAttribute(sender, e);
		if (e.ExpectedAttributes == null)
		{
			throw new InvalidOperationException(Res.GetString("WebUnknownAttribute", e.Attr.Name, e.Attr.Value));
		}
		if (e.ExpectedAttributes.Length == 0)
		{
			throw new InvalidOperationException(Res.GetString("WebUnknownAttribute2", e.Attr.Name, e.Attr.Value));
		}
		throw new InvalidOperationException(Res.GetString("WebUnknownAttribute3", e.Attr.Name, e.Attr.Value, e.ExpectedAttributes));
	}

	internal static string ElementString(XmlElement element)
	{
		StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
		stringWriter.Write("<");
		stringWriter.Write(element.Name);
		if (element.NamespaceURI != null && element.NamespaceURI.Length > 0)
		{
			stringWriter.Write(" xmlns");
			if (element.Prefix != null && element.Prefix.Length > 0)
			{
				stringWriter.Write(":");
				stringWriter.Write(element.Prefix);
			}
			stringWriter.Write("='");
			stringWriter.Write(element.NamespaceURI);
			stringWriter.Write("'");
		}
		stringWriter.Write(">..</");
		stringWriter.Write(element.Name);
		stringWriter.Write(">");
		return stringWriter.ToString();
	}

	internal static void OnUnknownElement(object sender, XmlElementEventArgs e)
	{
		if (e.Element == null)
		{
			return;
		}
		string text = ElementString(e.Element);
		Tracing.OnUnknownElement(sender, e);
		if (e.ExpectedElements == null)
		{
			throw new InvalidOperationException(Res.GetString("WebUnknownElement", text));
		}
		if (e.ExpectedElements.Length == 0)
		{
			throw new InvalidOperationException(Res.GetString("WebUnknownElement1", text));
		}
		throw new InvalidOperationException(Res.GetString("WebUnknownElement2", text, e.ExpectedElements));
	}

	internal static bool IsKnownNamespace(string ns)
	{
		switch (ns)
		{
		case "http://www.w3.org/2001/XMLSchema-instance":
			return true;
		case "http://www.w3.org/XML/1998/namespace":
			return true;
		case "http://schemas.xmlsoap.org/soap/encoding/":
		case "http://schemas.xmlsoap.org/soap/envelope/":
			return true;
		case "http://www.w3.org/2003/05/soap-envelope":
		case "http://www.w3.org/2003/05/soap-encoding":
		case "http://www.w3.org/2003/05/soap-rpc":
			return true;
		default:
			return false;
		}
	}

	internal static string EscapeUri(Uri uri)
	{
		if (null == uri)
		{
			throw new ArgumentNullException("uri");
		}
		return uri.GetComponents(UriComponents.SerializationInfoString, UriFormat.UriEscaped).Replace("#", "%23");
	}
}
