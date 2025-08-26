using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols;

/// <summary>When overridden in a derived class, represents the content of a SOAP header.</summary>
[XmlType(IncludeInSchema = false)]
[SoapType(IncludeInSchema = false)]
public abstract class SoapHeader
{
	private string actor;

	private bool mustUnderstand;

	private bool didUnderstand;

	private bool relay;

	internal SoapProtocolVersion version;

	/// <summary>Gets or sets the value of the <see langword="mustUnderstand" /> XML attribute for the SOAP header when communicating with SOAP protocol version 1.1.</summary>
	/// <returns>The value of the <see langword="mustUnderstand" /> attribute. The default is "0".</returns>
	/// <exception cref="T:System.ArgumentException">The property is set to a value other than: "0", "1", "true", or "false". </exception>
	[XmlAttribute("mustUnderstand", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	[SoapAttribute("mustUnderstand", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	[DefaultValue("0")]
	public string EncodedMustUnderstand
	{
		get
		{
			if (version == SoapProtocolVersion.Soap12 || !MustUnderstand)
			{
				return "0";
			}
			return "1";
		}
		set
		{
			switch (value)
			{
			case "false":
			case "0":
				MustUnderstand = false;
				break;
			case "true":
			case "1":
				MustUnderstand = true;
				break;
			default:
				throw new ArgumentException(Res.GetString("WebHeaderInvalidMustUnderstand", value));
			}
		}
	}

	/// <summary>Gets or sets the value of the <see langword="mustUnderstand" /> XML attribute for the SOAP header when communicating with SOAP protocol version 1.2.</summary>
	/// <returns>The value of the <see langword="mustUnderstand" /> XML attribute of a SOAP header. The default is "0".</returns>
	/// <exception cref="T:System.ArgumentException">The property is set to a value other than: "0", "1", "true", or "false". </exception>
	[XmlAttribute("mustUnderstand", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
	[SoapAttribute("mustUnderstand", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
	[DefaultValue("0")]
	[ComVisible(false)]
	public string EncodedMustUnderstand12
	{
		get
		{
			if (version == SoapProtocolVersion.Soap11 || !MustUnderstand)
			{
				return "0";
			}
			return "1";
		}
		set
		{
			EncodedMustUnderstand = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> must be understood.</summary>
	/// <returns>
	///     <see langword="true" /> if the XML Web service must properly interpret and process the <see cref="T:System.Web.Services.Protocols.SoapHeader" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[XmlIgnore]
	[SoapIgnore]
	public bool MustUnderstand
	{
		get
		{
			return InternalMustUnderstand;
		}
		set
		{
			InternalMustUnderstand = value;
		}
	}

	internal virtual bool InternalMustUnderstand
	{
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		get
		{
			return mustUnderstand;
		}
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		set
		{
			mustUnderstand = value;
		}
	}

	/// <summary>Gets or sets the recipient of the SOAP header.</summary>
	/// <returns>The recipient of the SOAP header. The default is an empty string ("").</returns>
	[XmlAttribute("actor", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	[SoapAttribute("actor", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
	[DefaultValue("")]
	public string Actor
	{
		get
		{
			if (version == SoapProtocolVersion.Soap12)
			{
				return "";
			}
			return InternalActor;
		}
		set
		{
			InternalActor = value;
		}
	}

	/// <summary>Gets or sets the recipient of the SOAP header.</summary>
	/// <returns>A URI that represents the recipient of the SOAP header. The default is an empty string ("").</returns>
	[XmlAttribute("role", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
	[SoapAttribute("role", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
	[DefaultValue("")]
	[ComVisible(false)]
	public string Role
	{
		get
		{
			if (version == SoapProtocolVersion.Soap11)
			{
				return "";
			}
			return InternalActor;
		}
		set
		{
			InternalActor = value;
		}
	}

	internal virtual string InternalActor
	{
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		get
		{
			if (actor != null)
			{
				return actor;
			}
			return string.Empty;
		}
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		set
		{
			actor = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether an XML Web service method properly processed a SOAP header.</summary>
	/// <returns>
	///     <see langword="true" /> if the SOAP header was properly processed; otherwise <see langword="false" />.</returns>
	[XmlIgnore]
	[SoapIgnore]
	public bool DidUnderstand
	{
		get
		{
			return didUnderstand;
		}
		set
		{
			didUnderstand = value;
		}
	}

	/// <summary>Gets or sets the relay attribute of the SOAP 1.2 header.</summary>
	/// <returns>Either "0", "false", "1", or "true".</returns>
	[XmlAttribute("relay", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
	[SoapAttribute("relay", Namespace = "http://www.w3.org/2003/05/soap-envelope")]
	[DefaultValue("0")]
	[ComVisible(false)]
	public string EncodedRelay
	{
		get
		{
			if (version == SoapProtocolVersion.Soap11 || !Relay)
			{
				return "0";
			}
			return "1";
		}
		set
		{
			switch (value)
			{
			case "false":
			case "0":
				Relay = false;
				break;
			case "true":
			case "1":
				Relay = true;
				break;
			default:
				throw new ArgumentException(Res.GetString("WebHeaderInvalidRelay", value));
			}
		}
	}

	/// <summary>Gets or sets a value that indicates whether the SOAP header is to be relayed to the next SOAP node if the current node does not understand the header.</summary>
	/// <returns>
	///     <see langword="true" /> if the SOAP header has a "relay" attribute set to "true"; otherwise, <see langword="false" />.</returns>
	[XmlIgnore]
	[SoapIgnore]
	[ComVisible(false)]
	public bool Relay
	{
		get
		{
			return InternalRelay;
		}
		set
		{
			InternalRelay = value;
		}
	}

	internal virtual bool InternalRelay
	{
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		get
		{
			return relay;
		}
		[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
		set
		{
			relay = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Protocols.SoapHeader" /> class. </summary>
	protected SoapHeader()
	{
	}
}
