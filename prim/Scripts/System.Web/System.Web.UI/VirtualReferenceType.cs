namespace System.Web.UI;

/// <summary>Specifies the type of resource referenced by a parsed virtual path.</summary>
public enum VirtualReferenceType
{
	/// <summary>The parsed virtual path references an ASP.NET page.</summary>
	Page,
	/// <summary>The parsed virtual path references an ASP.NET user control.</summary>
	UserControl,
	/// <summary>The parsed virtual path references a master page file.</summary>
	Master,
	/// <summary>The parsed virtual path references a code file that is compiled using a specific language compiler.</summary>
	SourceFile,
	/// <summary>The parsed virtual path references a resource that is not an ASP.NET page, master page, user control, or language-specific code file.</summary>
	Other
}
