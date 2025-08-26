using System.Xml.Serialization;

namespace System.Web.Services.Discovery;

/// <summary>Represents a file system directory path that should not be searched for references to add to a Web services discovery document.</summary>
public sealed class ExcludePathInfo
{
	private string path;

	/// <summary>Gets or sets the file system directory path that should not be searched for references to add to a discovery document.</summary>
	/// <returns>The file system directory path that should be excluded from searches.</returns>
	[XmlAttribute("path")]
	public string Path
	{
		get
		{
			return path;
		}
		set
		{
			path = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.ExcludePathInfo" /> class. </summary>
	public ExcludePathInfo()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.ExcludePathInfo" /> class and specifies the file system path to exclude from searches. </summary>
	/// <param name="path">The path to exclude from searches.</param>
	public ExcludePathInfo(string path)
	{
		this.path = path;
	}
}
