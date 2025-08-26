using System.Web.UI.WebControls;

namespace System.Web.UI.Design;

/// <summary>Provides an interface to manage a template editing area.</summary>
[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
public interface ITemplateEditingFrame : IDisposable
{
	/// <summary>Gets the style for the editing frame.</summary>
	/// <returns>A <see cref="T:System.Web.UI.WebControls.Style" /> that represents the Web server control style attributes for the editing frame.</returns>
	Style ControlStyle { get; }

	/// <summary>Gets or sets the initial height of the control.</summary>
	/// <returns>The initial height of the control.</returns>
	int InitialHeight { get; set; }

	/// <summary>Gets or sets the initial width of the control.</summary>
	/// <returns>The initial width of the control.</returns>
	int InitialWidth { get; set; }

	/// <summary>Gets the name of the editing frame.</summary>
	/// <returns>The name of the editing frame.</returns>
	string Name { get; }

	/// <summary>Gets a set of names of templates to use.</summary>
	/// <returns>An array of template names.</returns>
	string[] TemplateNames { get; }

	/// <summary>Gets the template styles for the control.</summary>
	/// <returns>An array of <see cref="T:System.Web.UI.WebControls.Style" /> objects that represent the template styles for the control.</returns>
	Style[] TemplateStyles { get; }

	/// <summary>Gets or sets the verb that invokes the template.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> that invokes the template.</returns>
	TemplateEditingVerb Verb { get; set; }

	/// <summary>Closes the control and optionally saves any changes.</summary>
	/// <param name="saveChanges">
	///   <see langword="true" /> if changes to the document should be saved; otherwise, <see langword="false" />.</param>
	void Close(bool saveChanges);

	/// <summary>Opens and displays the control.</summary>
	void Open();

	/// <summary>Resizes the control to the specified width and height.</summary>
	/// <param name="width">The new width for the control.</param>
	/// <param name="height">The new height for the control.</param>
	void Resize(int width, int height);

	/// <summary>Saves any changes to the document.</summary>
	void Save();

	/// <summary>Changes the name of the control to the specified name.</summary>
	/// <param name="newName">The new name for the control.</param>
	void UpdateControlName(string newName);
}
