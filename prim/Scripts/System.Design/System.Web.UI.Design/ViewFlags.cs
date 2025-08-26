namespace System.Web.UI.Design;

/// <summary>Indicates which features are enabled with the <see cref="M:System.Web.UI.Design.ControlDesigner.SetViewFlags(System.Web.UI.Design.ViewFlags,System.Boolean)" /> method of a designer.</summary>
[Flags]
public enum ViewFlags
{
	/// <summary>Enables painting events for displaying a custom control at design time.</summary>
	CustomPaint = 1,
	/// <summary>Postpones all events until after the control is completely loaded.</summary>
	DesignTimeHtmlRequiresLoadComplete = 2,
	/// <summary>Enables template editing at design time.</summary>
	TemplateEditing = 4
}
