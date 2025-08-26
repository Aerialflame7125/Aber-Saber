namespace System.Web.UI;

/// <summary>Defines constants that specify how ASP.NET should compile .aspx pages and .ascx controls.</summary>
public enum CompilationMode
{
	/// <summary>ASP.NET will not compile the page, if possible.</summary>
	Auto,
	/// <summary>The page or control should never be dynamically compiled.</summary>
	Never,
	/// <summary>The page should always be compiled.</summary>
	Always
}
