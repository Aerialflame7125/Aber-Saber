using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;

namespace System.Web.UI.Design;

/// <summary>Extends design-time behavior for template-based server controls.</summary>
public abstract class TemplatedControlDesigner : ControlDesigner
{
	private ITemplateEditingFrame _activeTemplateFrame;

	private bool _enableTemplateEditing = true;

	private bool _templateMode;

	private EventHandler _templateVerbHandler;

	/// <summary>Gets a value indicating whether the designer allows data binding.</summary>
	/// <returns>
	///   <see langword="true" />, if the designer allows data binding; otherwise, <see langword="false" />.</returns>
	protected override bool DataBindingsEnabled
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a collection of template groups, each containing a template definition.</summary>
	/// <returns>A collection of <see cref="T:System.Web.UI.Design.TemplateGroup" /> elements.</returns>
	public override TemplateGroupCollection TemplateGroups
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the active template editing frame.</summary>
	/// <returns>An <see cref="T:System.Web.UI.Design.ITemplateEditingFrame" /> that is the currently active template editing frame.</returns>
	[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
	public ITemplateEditingFrame ActiveTemplateEditingFrame => _activeTemplateFrame;

	/// <summary>Gets a value indicating whether or not this designer will allow the viewing or editing of templates.</summary>
	/// <returns>
	///   <see langword="true" /> if the designer will allow the viewing or editing of templates; otherwise, <see langword="false" />.</returns>
	public bool CanEnterTemplateMode => _enableTemplateEditing;

	/// <summary>Gets a value indicating whether the designer document is in template mode.</summary>
	/// <returns>
	///   <see langword="true" /> if the designer document is in template mode; otherwise, <see langword="false" />.</returns>
	[Obsolete("Use ControlDesigner.InTemplateMode instead")]
	public new bool InTemplateMode => _templateMode;

	internal EventHandler TemplateEditingVerbHandler => _templateVerbHandler;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplatedControlDesigner" /> class.</summary>
	public TemplatedControlDesigner()
	{
	}

	/// <summary>Initializes the designer and loads the specified component.</summary>
	/// <param name="component">The control element being designed.</param>
	[System.MonoTODO]
	public override void Initialize(IComponent component)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, creates a template editing frame for the specified verb.</summary>
	/// <param name="verb">The template editing verb to create a template editing frame for.</param>
	/// <returns>The new template editing frame.</returns>
	[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
	protected abstract ITemplateEditingFrame CreateTemplateEditingFrame(TemplateEditingVerb verb);

	/// <summary>Gets the cached template editing verbs.</summary>
	/// <returns>An array of <see cref="T:System.Web.UI.Design.TemplateEditingVerb" /> objects, if any.</returns>
	[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
	protected abstract TemplateEditingVerb[] GetCachedTemplateEditingVerbs();

	/// <summary>When overridden in a derived class, gets the template's content.</summary>
	/// <param name="editingFrame">The template editing frame to retrieve the content of.</param>
	/// <param name="templateName">The name of the template.</param>
	/// <param name="allowEditing">
	///   <see langword="true" /> if the template's content can be edited; <see langword="false" /> if the content is read-only.</param>
	/// <returns>The content of the template.</returns>
	[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
	public abstract string GetTemplateContent(ITemplateEditingFrame editingFrame, string templateName, out bool allowEditing);

	/// <summary>When overridden in a derived class, sets the specified template's content to the specified content.</summary>
	/// <param name="editingFrame">The template editing frame to provide content for.</param>
	/// <param name="templateName">The name of the template.</param>
	/// <param name="templateContent">The content to set for the template.</param>
	[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
	public abstract void SetTemplateContent(ITemplateEditingFrame editingFrame, string templateName, string templateContent);

	/// <summary>Opens a particular template frame object for editing in the designer.</summary>
	/// <param name="newTemplateEditingFrame">The template editing frame object to open in the designer.</param>
	[System.MonoTODO]
	[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
	public void EnterTemplateMode(ITemplateEditingFrame newTemplateEditingFrame)
	{
		throw new NotImplementedException();
	}

	/// <summary>Closes the currently active template editing frame after saving any relevant changes.</summary>
	/// <param name="fSwitchingTemplates">
	///   <see langword="true" /> when switching from one template editing frame to another; otherwise <see langword="false" />.</param>
	/// <param name="fNested">
	///   <see langword="true" /> if this designer is nested (one or more levels) within another control whose designer is also in template editing mode; otherwise <see langword="false" />.</param>
	/// <param name="fSave">
	///   <see langword="true" /> if templates should be saved on exit; otherwise, <see langword="false" />.</param>
	[System.MonoTODO]
	[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
	public void ExitTemplateMode(bool fSwitchingTemplates, bool fNested, bool fSave)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the data item property of the template's container.</summary>
	/// <param name="templateName">The name of the template.</param>
	/// <returns>A string representing the data.</returns>
	[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
	public virtual string GetTemplateContainerDataItemProperty(string templateName)
	{
		return string.Empty;
	}

	/// <summary>Gets the data source of the template's container.</summary>
	/// <param name="templateName">The name of the template.</param>
	/// <returns>The data source of the container of the specified template.</returns>
	[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
	public virtual IEnumerable GetTemplateContainerDataSource(string templateName)
	{
		return null;
	}

	/// <summary>Gets the template editing verbs available to the designer.</summary>
	/// <returns>The template editing verbs available to the designer.</returns>
	[System.MonoTODO]
	[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
	public TemplateEditingVerb[] GetTemplateEditingVerbs()
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates a template from the specified text.</summary>
	/// <param name="text">The text to retrieve a template from.</param>
	/// <returns>An <see cref="T:System.Web.UI.ITemplate" /> from the specified text.</returns>
	[System.MonoTODO]
	protected ITemplate GetTemplateFromText(string text)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the type of the parent of the template property.</summary>
	/// <param name="templateName">The name of the template to return the type of the parent for.</param>
	/// <returns>The type of the object that has the template property.</returns>
	[Obsolete("Template editing is supported in ControlDesigner.TemplateGroups with SetViewFlags(ViewFlags.TemplateEditing, true) in 2.0.")]
	public virtual Type GetTemplatePropertyParentType(string templateName)
	{
		return base.Component.GetType();
	}

	/// <summary>Gets a string of text that represents the specified template.</summary>
	/// <param name="template">The <see cref="T:System.Web.UI.ITemplate" /> to convert to text.</param>
	/// <returns>A string that represents the specified template.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="template" /> is <see langword="null" />.</exception>
	[System.MonoTODO]
	protected string GetTextFromTemplate(ITemplate template)
	{
		throw new NotImplementedException();
	}

	/// <summary>Provides an opportunity to perform additional processing when a behavior is attached to the designer.</summary>
	[System.MonoTODO]
	[Obsolete("Use ControlDesigner.Tag instead")]
	protected override void OnBehaviorAttached()
	{
		throw new NotImplementedException();
	}

	/// <summary>Delegate to handle the component changed event.</summary>
	/// <param name="sender">The object sending the event.</param>
	/// <param name="ce">A <see cref="T:System.ComponentModel.Design.ComponentChangedEventArgs" /> that provides data for the event.</param>
	[System.MonoTODO]
	public override void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
	{
		throw new NotImplementedException();
	}

	/// <summary>Provides an opportunity to perform additional processing when the parent of this designer is changed.</summary>
	[System.MonoTODO]
	public override void OnSetParent()
	{
		throw new NotImplementedException();
	}

	/// <summary>Provides an opportunity to perform additional processing when the template mode is changed.</summary>
	[System.MonoTODO]
	protected virtual void OnTemplateModeChanged()
	{
		throw new NotImplementedException();
	}

	/// <summary>Saves the active template editing frame.</summary>
	[System.MonoTODO]
	protected void SaveActiveTemplateEditingFrame()
	{
		throw new NotImplementedException();
	}

	/// <summary>Updates the design-time HTML.</summary>
	[System.MonoTODO]
	public override void UpdateDesignTimeHtml()
	{
		throw new NotImplementedException();
	}
}
