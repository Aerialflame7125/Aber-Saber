namespace System.Web.UI.WebControls.WebParts;

/// <summary>Defines common user interface (UI) properties used by ASP.NET <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls. </summary>
public interface IWebPart
{
	/// <summary>Gets or sets the URL to an image that represents a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control in a catalog of controls.</summary>
	/// <returns>A string that represents the URL to an image used to represent the control in a catalog. The default value is an empty string ("").</returns>
	string CatalogIconImageUrl { get; set; }

	/// <summary>Gets or sets a brief phrase that summarizes what a control does, for use in ToolTips and catalogs of <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> controls.</summary>
	/// <returns>A string that briefly summarizes the control's functionality. The default value is an empty string ("").</returns>
	string Description { get; set; }

	/// <summary>Gets a string that is concatenated with the <see cref="P:System.Web.UI.WebControls.WebParts.IWebPart.Title" /> property value to form a complete title for a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
	/// <returns>A string that serves as a subtitle for the control. The default value is an empty string ("").</returns>
	string Subtitle { get; }

	/// <summary>Gets or sets the title of a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
	/// <returns>A string that contains the title of the control. The default value is an empty string ("").</returns>
	string Title { get; set; }

	/// <summary>Gets or sets the URL to an image used to represent a Web Parts control in the control's own title bar.</summary>
	/// <returns>A string that represents the URL to an image. The default value is an empty string ("").</returns>
	string TitleIconImageUrl { get; set; }

	/// <summary>Gets or sets a URL to supplemental information about a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control.</summary>
	/// <returns>A string that represents a URL to more information about a <see cref="T:System.Web.UI.WebControls.WebParts.WebPart" /> control. The default value is an empty string ("").</returns>
	string TitleUrl { get; set; }
}
