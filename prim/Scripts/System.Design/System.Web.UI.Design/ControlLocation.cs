namespace System.Web.UI.Design;

/// <summary>Specifies the possible locations for adding a control in a container.</summary>
public enum ControlLocation
{
	/// <summary>Adds the control before the current selection or current control.</summary>
	Before,
	/// <summary>Adds the control after the current selection or current control.</summary>
	After,
	/// <summary>Adds the control at the start of the document.</summary>
	First,
	/// <summary>Adds the control at the end of the document.</summary>
	Last,
	/// <summary>Adds the control as the first child of the selected control, if the selected control is a container control.</summary>
	FirstChild,
	/// <summary>Adds the control as the last child of the selected control, if the selected control is a container control.</summary>
	LastChild
}
