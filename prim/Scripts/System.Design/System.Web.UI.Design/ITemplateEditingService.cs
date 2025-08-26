using System.Web.UI.WebControls;

namespace System.Web.UI.Design;

/// <summary>Provides services for editing control templates at design time.</summary>
[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
public interface ITemplateEditingService
{
	/// <summary>Gets a value that indicates whether the service supports nested template editing.</summary>
	/// <returns>
	///   <see langword="true" /> if the service supports nested template editing; otherwise, <see langword="false" />.</returns>
	bool SupportsNestedTemplateEditing { get; }

	/// <summary>Creates a new template editing frame for the specified templated control designer, using the specified name and templates.</summary>
	/// <param name="designer">The <see cref="T:System.Web.UI.Design.TemplatedControlDesigner" /> that will use the template editing frame.</param>
	/// <param name="frameName">The name of the editing frame that will be displayed on the frame. Typically this is the same as the <see cref="P:System.ComponentModel.Design.DesignerVerb.Text" /> property used as the menu text for the <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> that is invoked to create the frame.</param>
	/// <param name="templateNames">An array of names for the templates that the template editing frame will contain.</param>
	/// <returns>The new <see cref="T:System.Web.UI.Design.ITemplateEditingFrame" />.</returns>
	ITemplateEditingFrame CreateFrame(TemplatedControlDesigner designer, string frameName, string[] templateNames);

	/// <summary>Creates a new template editing frame for the specified <see cref="T:System.Web.UI.Design.TemplatedControlDesigner" />, using the specified name, template names, control style, and template styles.</summary>
	/// <param name="designer">The <see cref="T:System.Web.UI.Design.TemplatedControlDesigner" /> that will use the template editing frame.</param>
	/// <param name="frameName">The name of the editing frame that will be displayed on the frame. Typically this is the same as the <see cref="P:System.ComponentModel.Design.DesignerVerb.Text" /> property used as the menu text for the <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> that is invoked to create the frame.</param>
	/// <param name="templateNames">An array of names for the templates that the template editing frame will contain.</param>
	/// <param name="controlStyle">The control <see cref="T:System.Web.UI.WebControls.Style" /> for the editing frame.</param>
	/// <param name="templateStyles">An array of type <see cref="T:System.Web.UI.WebControls.Style" /> that represents the template styles for the editing frame.</param>
	/// <returns>The new <see cref="T:System.Web.UI.Design.ITemplateEditingFrame" />.</returns>
	ITemplateEditingFrame CreateFrame(TemplatedControlDesigner designer, string frameName, string[] templateNames, Style controlStyle, Style[] templateStyles);

	/// <summary>Gets the name of the parent template.</summary>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> for which to get the name of the parent template.</param>
	/// <returns>The name of the parent template.</returns>
	string GetContainingTemplateName(Control control);
}
