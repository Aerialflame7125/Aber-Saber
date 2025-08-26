namespace System.Web.UI;

/// <summary>Specifies whether view state will be enabled for a control.</summary>
public enum ViewStateMode
{
	/// <summary>Inherit the value of <see cref="T:System.Web.UI.ViewStateMode" /> from the parent <see cref="T:System.Web.UI.Control" />.</summary>
	Inherit,
	/// <summary>Enable view state for this control even if the parent control has view state disabled.</summary>
	Enabled,
	/// <summary>Disable view state for this control even if the parent control has view state enabled.</summary>
	Disabled
}
