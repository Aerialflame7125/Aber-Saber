using System.Web.UI.WebControls;

namespace System.Web.UI;

/// <summary>Defines the methods that a class must implement in order to support the creation of style rules.</summary>
public interface IStyleSheet
{
	/// <summary>When implemented by a class, creates a style rule for the specified document language element type, or selector.</summary>
	/// <param name="style">The style rule to be added to the embedded style sheet.</param>
	/// <param name="urlResolver">An <see cref="T:System.Web.UI.IUrlResolutionService" />-implemented object that contains the context information for the current location (URL).</param>
	/// <param name="selector">The part of the HTML page affected by the style.</param>
	void CreateStyleRule(Style style, IUrlResolutionService urlResolver, string selector);

	/// <summary>When implemented by a class, adds a new style rule to the embedded style sheet in the <see langword="&lt;head&gt;" /> section of a Web page.</summary>
	/// <param name="style">The style rule to be added to the embedded style sheet.</param>
	/// <param name="urlResolver">An <see cref="T:System.Web.UI.IUrlResolutionService" />-implemented object that contains the context information for the current location (URL).</param>
	void RegisterStyle(Style style, IUrlResolutionService urlResolver);
}
