using System.Windows.Forms;

namespace System.Web.UI.Design;

/// <summary>Provides methods to start specific user interfaces for building properties at design-time.</summary>
public interface IWebFormsBuilderUIService
{
	/// <summary>Starts a <see cref="T:System.Web.UI.Design.ColorBuilder" /> to build a color property.</summary>
	/// <param name="owner">The control used to parent the dialog shown by the <see cref="T:System.Web.UI.Design.ColorBuilder" />.</param>
	/// <param name="initialColor">The initial color for the editor to pre-select.</param>
	/// <returns>The color that was selected. This value will be a named color, or an RGB color expressed in HTML color format (#RRGGBB).</returns>
	string BuildColor(System.Windows.Forms.Control owner, string initialColor);

	/// <summary>Launches an editor to build a URL property.</summary>
	/// <param name="owner">The control used to parent the dialog shown by the <see cref="T:System.Web.UI.Design.UrlBuilder" />.</param>
	/// <param name="initialUrl">The initial URL to display in the selection interface.</param>
	/// <param name="baseUrl">The base URL used to construct relative URLs.</param>
	/// <param name="caption">A caption that presents a message in the selection interface.</param>
	/// <param name="filter">The filter string to use to optionally filter the files displayed in the selection interface.</param>
	/// <param name="options">A <see cref="T:System.Web.UI.Design.UrlBuilderOptions" /> that indicates the options for the <see cref="T:System.Web.UI.Design.UrlBuilder" />.</param>
	/// <returns>A string that contains the URL returned by the <see cref="T:System.Web.UI.Design.UrlBuilder" />.</returns>
	string BuildUrl(System.Windows.Forms.Control owner, string initialUrl, string baseUrl, string caption, string filter, UrlBuilderOptions options);
}
