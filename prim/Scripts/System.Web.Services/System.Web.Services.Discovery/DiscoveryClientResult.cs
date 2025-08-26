using System.Xml.Serialization;

namespace System.Web.Services.Discovery;

/// <summary>Represents the details of a discovery reference without the contents of the referenced document. This class cannot be inherited.</summary>
public sealed class DiscoveryClientResult
{
	private string referenceTypeName;

	private string url;

	private string filename;

	/// <summary>Name of the class representing the type of reference in the discovery document.</summary>
	/// <returns>Name of the class representing the type of a reference. Default value is <see langword="null" />.</returns>
	[XmlAttribute("referenceType")]
	public string ReferenceTypeName
	{
		get
		{
			return referenceTypeName;
		}
		set
		{
			referenceTypeName = value;
		}
	}

	/// <summary>Gets or sets the URL for the reference.</summary>
	/// <returns>The URL of the reference.</returns>
	[XmlAttribute("url")]
	public string Url
	{
		get
		{
			return url;
		}
		set
		{
			url = value;
		}
	}

	/// <summary>Gets or sets the name of the file in which the reference is saved.</summary>
	/// <returns>Name of the file in which the reference is saved.</returns>
	[XmlAttribute("filename")]
	public string Filename
	{
		get
		{
			return filename;
		}
		set
		{
			filename = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> class.</summary>
	public DiscoveryClientResult()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientResult" /> class and sets the <see cref="P:System.Web.Services.Discovery.DiscoveryClientResult.ReferenceTypeName" /> property to <paramref name="referenceType" />, the <see cref="P:System.Web.Services.Discovery.DiscoveryClientResult.Url" /> property to <paramref name="url" /> and the <see cref="P:System.Web.Services.Discovery.DiscoveryClientResult.Filename" /> property to <paramref name="filename" />.</summary>
	/// <param name="referenceType">Name of the class representing the type of reference in the discovery document. Sets the <see cref="P:System.Web.Services.Discovery.DiscoveryClientResult.ReferenceTypeName" /> property. </param>
	/// <param name="url">URL for the reference. Sets the <see cref="P:System.Web.Services.Discovery.DiscoveryClientResult.Url" /> property. </param>
	/// <param name="filename">Name of the file in which the reference was saved. Sets the <see cref="P:System.Web.Services.Discovery.DiscoveryClientResult.Filename" /> property. </param>
	public DiscoveryClientResult(Type referenceType, string url, string filename)
	{
		referenceTypeName = ((referenceType == null) ? string.Empty : referenceType.FullName);
		this.url = url;
		this.filename = filename;
	}
}
