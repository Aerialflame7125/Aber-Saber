namespace System.Web.UI.Design;

/// <summary>Enables the extension of specific behaviors of a control designer.</summary>
[Obsolete("Use IControlDesignerTag interface instead")]
public interface IControlDesignerBehavior
{
	/// <summary>Gets the design-time view control object for the designer.</summary>
	/// <returns>The view control object for the designer.</returns>
	object DesignTimeElementView { get; }

	/// <summary>Gets or sets the design-time HTML for the designer's control.</summary>
	/// <returns>The HTML used at design time to format the control.</returns>
	string DesignTimeHtml { get; set; }

	/// <summary>Provides an opportunity to perform processing when the designer enters or exits template mode.</summary>
	void OnTemplateModeChanged();
}
