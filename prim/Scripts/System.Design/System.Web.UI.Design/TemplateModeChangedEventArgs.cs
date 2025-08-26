namespace System.Web.UI.Design;

/// <summary>Provides data for a <see cref="E:System.Web.UI.Design.IControlDesignerView.ViewEvent" /> event that is raised when the template mode changes for a control on the design surface.</summary>
public class TemplateModeChangedEventArgs : EventArgs
{
	private TemplateGroup group;

	/// <summary>Gets the template group that was created when you entered template editing mode.</summary>
	/// <returns>A <see cref="T:System.Web.UI.Design.TemplateGroup" /> if you entered template editing mode with a new template; otherwise, <see langword="null" />.</returns>
	public TemplateGroup NewTemplateGroup => group;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.Design.TemplateModeChangedEventArgs" /> class with the specified template group.</summary>
	/// <param name="newTemplateGroup">A new template group that is used to initialize the <see cref="P:System.Web.UI.Design.TemplateModeChangedEventArgs.NewTemplateGroup" />.</param>
	public TemplateModeChangedEventArgs(TemplateGroup newTemplateGroup)
	{
		group = newTemplateGroup;
	}
}
