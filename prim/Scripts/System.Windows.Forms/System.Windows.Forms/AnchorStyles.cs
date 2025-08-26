using System.ComponentModel;
using System.Drawing.Design;

namespace System.Windows.Forms;

/// <summary>Specifies how a control anchors to the edges of its container.</summary>
/// <filterpriority>2</filterpriority>
[Flags]
[Editor("System.Windows.Forms.Design.AnchorEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
public enum AnchorStyles
{
	/// <summary>The control is not anchored to any edges of its container.</summary>
	None = 0,
	/// <summary>The control is anchored to the top edge of its container.</summary>
	Top = 1,
	/// <summary>The control is anchored to the bottom edge of its container.</summary>
	Bottom = 2,
	/// <summary>The control is anchored to the left edge of its container.</summary>
	Left = 4,
	/// <summary>The control is anchored to the right edge of its container.</summary>
	Right = 8
}
