using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms;

/// <summary>Specifies the position and manner in which a control is docked.</summary>
/// <filterpriority>2</filterpriority>
[Editor("System.Windows.Forms.Design.DockEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
public enum DockStyle
{
	/// <summary>The control is not docked.</summary>
	None,
	/// <summary>The control's top edge is docked to the top of its containing control.</summary>
	Top,
	/// <summary>The control's bottom edge is docked to the bottom of its containing control.</summary>
	Bottom,
	/// <summary>The control's left edge is docked to the left edge of its containing control.</summary>
	Left,
	/// <summary>The control's right edge is docked to the right edge of its containing control.</summary>
	Right,
	/// <summary>All the control's edges are docked to the all edges of its containing control and sized appropriately.</summary>
	Fill
}
