using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace System.Web.Services.Discovery;

/// <summary>Represents an XML document that specifies a list of file system directory paths that should not be searched for references to add to a Web services discovery document.</summary>
[XmlRoot("dynamicDiscovery", Namespace = "urn:schemas-dynamicdiscovery:disco.2000-03-17")]
public sealed class DynamicDiscoveryDocument
{
	private ExcludePathInfo[] excludePaths = new ExcludePathInfo[0];

	/// <summary>Contains the dynamic discovery document namespace "urn:schemas-dynamicdiscovery:disco.2000-03-17". This field is constant.</summary>
	public const string Namespace = "urn:schemas-dynamicdiscovery:disco.2000-03-17";

	/// <summary>Gets or sets the file-system directory paths that should not be searched for references to add to a discovery document.</summary>
	/// <returns>An array of <see cref="T:System.Web.Services.Discovery.ExcludePathInfo" /> objects.</returns>
	[XmlElement("exclude", typeof(ExcludePathInfo))]
	public ExcludePathInfo[] ExcludePaths
	{
		get
		{
			return excludePaths;
		}
		set
		{
			if (value == null)
			{
				value = new ExcludePathInfo[0];
			}
			excludePaths = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DynamicDiscoveryDocument" /> class. </summary>
	public DynamicDiscoveryDocument()
	{
	}

	/// <summary>Serializes a <see cref="T:System.Web.Services.Discovery.DynamicDiscoveryDocument" /> instance into an XML document specified as an output stream.</summary>
	/// <param name="stream">A <see cref="T:System.IO.Stream" /> object to which the XML dynamic discovery document is serialized.</param>
	public void Write(Stream stream)
	{
		new XmlSerializer(typeof(DynamicDiscoveryDocument)).Serialize(new StreamWriter(stream, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false)), this);
	}

	/// <summary>Deserializes an XML document into a <see cref="T:System.Web.Services.Discovery.DynamicDiscoveryDocument" /> instance.</summary>
	/// <param name="stream">A <see cref="T:System.IO.Stream" /> object from which the XML dynamic discovery document is deserialized.</param>
	/// <returns>The <see cref="T:System.Web.Services.Discovery.DynamicDiscoveryDocument" /> that was loaded.</returns>
	public static DynamicDiscoveryDocument Load(Stream stream)
	{
		return (DynamicDiscoveryDocument)new XmlSerializer(typeof(DynamicDiscoveryDocument)).Deserialize(stream);
	}
}
