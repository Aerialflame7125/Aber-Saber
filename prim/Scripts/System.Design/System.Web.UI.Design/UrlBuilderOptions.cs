namespace System.Web.UI.Design;

/// <summary>Defines identifiers for settings of a <see cref="T:System.Web.UI.Design.UrlBuilder" />.</summary>
[Flags]
public enum UrlBuilderOptions
{
	/// <summary>Use no additional options for the <see cref="T:System.Web.UI.Design.UrlBuilder" />.</summary>
	None = 0,
	/// <summary>Build a URL that references a path relative to the current path, rather than one that references a fully qualified, absolute path.</summary>
	NoAbsolute = 1
}
