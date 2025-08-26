using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

/// <summary>Represents the data received from a SOAP header that was not understood by the recipient XML Web service or XML Web service client. This class cannot be inherited.</summary>
public sealed class SoapUnknownHeader : SoapHeader
{
	private XmlElement element;

	/// <summary>Gets or sets the XML Header element for a SOAP request or response.</summary>
	/// <returns>An <see cref="T:System.Xml.XmlElement" /> representing the raw XML of the SOAP header.</returns>
	[XmlIgnore]
	public XmlElement Element
	{
		get
		{
			if (element == null)
			{
				return null;
			}
			if (version == SoapProtocolVersion.Soap12)
			{
				if (InternalMustUnderstand)
				{
					element.SetAttribute("mustUnderstand", "http://www.w3.org/2003/05/soap-envelope", "1");
				}
				element.RemoveAttribute("mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/");
				string internalActor = InternalActor;
				if (internalActor != null && internalActor.Length != 0)
				{
					element.SetAttribute("role", "http://www.w3.org/2003/05/soap-envelope", internalActor);
				}
				element.RemoveAttribute("actor", "http://schemas.xmlsoap.org/soap/envelope/");
			}
			else if (version == SoapProtocolVersion.Soap11)
			{
				if (InternalMustUnderstand)
				{
					element.SetAttribute("mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/", "1");
				}
				element.RemoveAttribute("mustUnderstand", "http://www.w3.org/2003/05/soap-envelope");
				string internalActor2 = InternalActor;
				if (internalActor2 != null && internalActor2.Length != 0)
				{
					element.SetAttribute("actor", "http://schemas.xmlsoap.org/soap/envelope/", internalActor2);
				}
				element.RemoveAttribute("role", "http://www.w3.org/2003/05/soap-envelope");
				element.RemoveAttribute("relay", "http://www.w3.org/2003/05/soap-envelope");
			}
			return element;
		}
		set
		{
			if (value == null && element != null)
			{
				base.InternalMustUnderstand = InternalMustUnderstand;
				base.InternalActor = InternalActor;
			}
			element = value;
		}
	}

	internal override bool InternalMustUnderstand
	{
		get
		{
			if (element == null)
			{
				return base.InternalMustUnderstand;
			}
			string elementAttribute = GetElementAttribute("mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/", element);
			if (elementAttribute == null)
			{
				elementAttribute = GetElementAttribute("mustUnderstand", "http://www.w3.org/2003/05/soap-envelope", element);
				if (elementAttribute == null)
				{
					return false;
				}
			}
			switch (elementAttribute)
			{
			case "false":
			case "0":
				return false;
			case "true":
			case "1":
				return true;
			default:
				return false;
			}
		}
		set
		{
			base.InternalMustUnderstand = value;
			if (element != null)
			{
				if (value)
				{
					element.SetAttribute("mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/", "1");
				}
				else
				{
					element.RemoveAttribute("mustUnderstand", "http://schemas.xmlsoap.org/soap/envelope/");
				}
				element.RemoveAttribute("mustUnderstand", "http://www.w3.org/2003/05/soap-envelope");
			}
		}
	}

	internal override string InternalActor
	{
		get
		{
			if (element == null)
			{
				return base.InternalActor;
			}
			string elementAttribute = GetElementAttribute("actor", "http://schemas.xmlsoap.org/soap/envelope/", element);
			if (elementAttribute == null)
			{
				elementAttribute = GetElementAttribute("role", "http://www.w3.org/2003/05/soap-envelope", element);
				if (elementAttribute == null)
				{
					return "";
				}
			}
			return elementAttribute;
		}
		set
		{
			base.InternalActor = value;
			if (element != null)
			{
				if (value == null || value.Length == 0)
				{
					element.RemoveAttribute("actor", "http://schemas.xmlsoap.org/soap/envelope/");
				}
				else
				{
					element.SetAttribute("actor", "http://schemas.xmlsoap.org/soap/envelope/", value);
				}
				element.RemoveAttribute("role", "http://www.w3.org/2003/05/soap-envelope");
			}
		}
	}

	internal override bool InternalRelay
	{
		get
		{
			if (element == null)
			{
				return base.InternalRelay;
			}
			string elementAttribute = GetElementAttribute("relay", "http://www.w3.org/2003/05/soap-envelope", element);
			if (elementAttribute == null)
			{
				return false;
			}
			switch (elementAttribute)
			{
			case "false":
			case "0":
				return false;
			case "true":
			case "1":
				return true;
			default:
				return false;
			}
		}
		set
		{
			base.InternalRelay = value;
			if (element != null)
			{
				if (value)
				{
					element.SetAttribute("relay", "http://www.w3.org/2003/05/soap-envelope", "1");
				}
				else
				{
					element.RemoveAttribute("relay", "http://www.w3.org/2003/05/soap-envelope");
				}
			}
		}
	}

	private string GetElementAttribute(string name, string ns, XmlElement element)
	{
		if (element == null)
		{
			return null;
		}
		if (element.Prefix.Length == 0 && element.NamespaceURI == ns)
		{
			if (element.HasAttribute(name))
			{
				return element.GetAttribute(name);
			}
			return null;
		}
		if (element.HasAttribute(name, ns))
		{
			return element.GetAttribute(name, ns);
		}
		return null;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapUnknownHeader" /> class. </summary>
	public SoapUnknownHeader()
	{
	}
}
