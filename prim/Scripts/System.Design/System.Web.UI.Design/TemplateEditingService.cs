using System.ComponentModel.Design;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design;

/// <summary>Provides services for editing control templates at design time. This class cannot be inherited.</summary>
[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
public sealed class TemplateEditingService : ITemplateEditingService, IDisposable
{
	private IDesignerHost _designerHost;

	/// <summary>Gets a value that indicates whether the service supports nested template editing.</summary>
	/// <returns>
	///   <see langword="true" /> if the service supports nested template editing; otherwise, <see langword="false" />.</returns>
	public bool SupportsNestedTemplateEditing => false;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateEditingService" /> class with the specified designer host.</summary>
	/// <param name="designerHost">An <see cref="T:System.ComponentModel.Design.IDesignerHost" /> implementation, used to access components at design time.</param>
	public TemplateEditingService(IDesignerHost designerHost)
	{
		if (designerHost == null)
		{
			throw new ArgumentNullException("designerHost");
		}
		_designerHost = designerHost;
	}

	/// <summary>Finalizes the service.</summary>
	~TemplateEditingService()
	{
		Dispose(disposing: false);
	}

	/// <summary>Creates a new template editing frame for the specified templated control designer, using the specified name and templates.</summary>
	/// <param name="designer">The <see cref="T:System.Web.UI.Design.TemplatedControlDesigner" /> that will use the template editing frame.</param>
	/// <param name="frameName">The name of the editing frame that will be displayed on the frame. Typically, this is the same as the <see cref="P:System.ComponentModel.Design.DesignerVerb.Text" /> used as the menu text for the <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> that is invoked to create the frame.</param>
	/// <param name="templateNames">An array of names for the templates that the template editing frame will contain.</param>
	/// <returns>The new <see cref="T:System.Web.UI.Design.ITemplateEditingFrame" />.</returns>
	[System.MonoTODO]
	public ITemplateEditingFrame CreateFrame(TemplatedControlDesigner designer, string frameName, string[] templateNames)
	{
		return CreateFrame(designer, frameName, templateNames, null, null);
	}

	/// <summary>Creates a new template editing frame for the specified <see cref="T:System.Web.UI.Design.TemplatedControlDesigner" /> object, using the specified name, template names, control style, and template styles.</summary>
	/// <param name="designer">The <see cref="T:System.Web.UI.Design.TemplatedControlDesigner" /> that will use the template editing frame.</param>
	/// <param name="frameName">The name of the editing frame that will be displayed on the frame. Typically, this is the same as the <see cref="P:System.ComponentModel.Design.DesignerVerb.Text" /> used as the menu text for the <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> that is invoked to create the frame.</param>
	/// <param name="templateNames">An array of names for the templates that the template editing frame will contain.</param>
	/// <param name="controlStyle">The control <see cref="T:System.Web.UI.WebControls.Style" /> for the editing frame.</param>
	/// <param name="templateStyles">An array of type <see cref="T:System.Web.UI.WebControls.Style" /> that represents the template styles for the editing frame.</param>
	/// <returns>The new <see cref="T:System.Web.UI.Design.ITemplateEditingFrame" />.</returns>
	[System.MonoTODO]
	public ITemplateEditingFrame CreateFrame(TemplatedControlDesigner designer, string frameName, string[] templateNames, Style controlStyle, Style[] templateStyles)
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases all resources that are used by the <see cref="T:System.Web.UI.Design.TemplateEditingService" /> object.</summary>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}

	private void Dispose(bool disposing)
	{
		if (disposing)
		{
			_designerHost = null;
		}
	}

	/// <summary>Gets the name of the parent template.</summary>
	/// <param name="control">The <see cref="T:System.Web.UI.Control" /> for which to get the name of the parent template.</param>
	/// <returns>The name of the parent template.</returns>
	[System.MonoTODO]
	public string GetContainingTemplateName(Control control)
	{
		throw new NotImplementedException();
	}
}
