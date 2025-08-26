using System.Collections;
using System.IO;
using System.Text;
using System.Web.Services.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery;

/// <summary>Represents a discovery document. This class cannot be inherited.</summary>
[XmlRoot("discovery", Namespace = "http://schemas.xmlsoap.org/disco/")]
public sealed class DiscoveryDocument
{
	/// <summary>Namespace of the discovery XML element of a discovery document.</summary>
	public const string Namespace = "http://schemas.xmlsoap.org/disco/";

	private ArrayList references = new ArrayList();

	/// <summary>A list of references contained within the discovery document.</summary>
	/// <returns>An <see cref="T:System.Collections.IList" /> containing the references within the discovery document.</returns>
	[XmlIgnore]
	public IList References => references;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> class.</summary>
	public DiscoveryDocument()
	{
	}

	/// <summary>Reads and returns a <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> from the passed <see cref="T:System.IO.Stream" />.</summary>
	/// <param name="stream">The <see cref="T:System.IO.Stream" /> from which to read the <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" />. </param>
	/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> containing the contents of a discovery document from the passed <see cref="T:System.IO.Stream" />.</returns>
	public static DiscoveryDocument Read(Stream stream)
	{
		return Read(new XmlTextReader(stream)
		{
			WhitespaceHandling = WhitespaceHandling.Significant,
			XmlResolver = null,
			DtdProcessing = DtdProcessing.Prohibit
		});
	}

	/// <summary>Reads and returns a <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> from the passed <see cref="T:System.IO.TextReader" />.</summary>
	/// <param name="reader">The <see cref="T:System.IO.TextReader" /> from which to read the <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" />. </param>
	/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> containing the contents of a discovery document from the passed <see cref="T:System.IO.TextReader" />.</returns>
	public static DiscoveryDocument Read(TextReader reader)
	{
		return Read(new XmlTextReader(reader)
		{
			WhitespaceHandling = WhitespaceHandling.Significant,
			XmlResolver = null,
			DtdProcessing = DtdProcessing.Prohibit
		});
	}

	/// <summary>Reads and returns a <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> from the passed <see cref="T:System.Xml.XmlReader" />.</summary>
	/// <param name="xmlReader">The <see cref="T:System.Xml.XmlReader" /> from which to read the <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" />. </param>
	/// <returns>A <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> containing the contents of a discovery document from the passed <see cref="T:System.Xml.XmlReader" />.</returns>
	public static DiscoveryDocument Read(XmlReader xmlReader)
	{
		return (DiscoveryDocument)WebServicesSection.Current.DiscoveryDocumentSerializer.Deserialize(xmlReader);
	}

	/// <summary>Returns a value indicating whether the passed <see cref="T:System.Xml.XmlReader" /> can be deserialized into a <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" />.</summary>
	/// <param name="xmlReader">The <see cref="T:System.Xml.XmlReader" /> that is tested whether it can be deserialized into a <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" />. </param>
	/// <returns>
	///     <see langword="true" /> if <see cref="T:System.Xml.XmlReader" /> can be deserialized into a <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" />; otherwise, <see langword="false" />.</returns>
	public static bool CanRead(XmlReader xmlReader)
	{
		return WebServicesSection.Current.DiscoveryDocumentSerializer.CanDeserialize(xmlReader);
	}

	/// <summary>Writes this <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> into the passed <see cref="T:System.IO.TextWriter" />.</summary>
	/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> into which this <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> is written. </param>
	public void Write(TextWriter writer)
	{
		XmlTextWriter xmlTextWriter = new XmlTextWriter(writer);
		xmlTextWriter.Formatting = Formatting.Indented;
		xmlTextWriter.Indentation = 2;
		Write(xmlTextWriter);
	}

	/// <summary>Writes this <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> into the passed <see cref="T:System.IO.Stream" />.</summary>
	/// <param name="stream">The <see cref="T:System.IO.Stream" /> into which this <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> is written. </param>
	public void Write(Stream stream)
	{
		TextWriter writer = new StreamWriter(stream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false));
		Write(writer);
	}

	/// <summary>Writes this <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> into the passed <see cref="T:System.Xml.XmlWriter" />.</summary>
	/// <param name="writer">The <see cref="T:System.Xml.XmlWriter" /> into which this <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" /> is written. </param>
	public void Write(XmlWriter writer)
	{
		XmlSerializer discoveryDocumentSerializer = WebServicesSection.Current.DiscoveryDocumentSerializer;
		XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
		discoveryDocumentSerializer.Serialize(writer, this, namespaces);
	}
}
