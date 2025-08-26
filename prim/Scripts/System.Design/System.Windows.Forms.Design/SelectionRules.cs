namespace System.Windows.Forms.Design;

/// <summary>Defines identifiers that are used to indicate selection rules for a component.</summary>
[Flags]
public enum SelectionRules
{
	/// <summary>Indicates the component supports sizing in all directions.</summary>
	AllSizeable = 0xF,
	/// <summary>Indicates the component supports resize from the bottom.</summary>
	BottomSizeable = 2,
	/// <summary>Indicates the component supports resize from the left.</summary>
	LeftSizeable = 4,
	/// <summary>Indicates the component is locked to its container. Overrides the <see cref="F:System.Windows.Forms.Design.SelectionRules.Moveable" />, <see cref="F:System.Windows.Forms.Design.SelectionRules.AllSizeable" />, <see cref="F:System.Windows.Forms.Design.SelectionRules.BottomSizeable" />, <see cref="F:System.Windows.Forms.Design.SelectionRules.LeftSizeable" />, <see cref="F:System.Windows.Forms.Design.SelectionRules.RightSizeable" />, and <see cref="F:System.Windows.Forms.Design.SelectionRules.TopSizeable" /> bit flags of this enumeration.</summary>
	Locked = int.MinValue,
	/// <summary>Indicates the component supports a location property that allows it to be moved on the screen.</summary>
	Moveable = 0x10000000,
	/// <summary>Indicates no special selection attributes.</summary>
	None = 0,
	/// <summary>Indicates the component supports resize from the right.</summary>
	RightSizeable = 8,
	/// <summary>Indicates the component supports resize from the top.</summary>
	TopSizeable = 1,
	/// <summary>Indicates the component has some form of visible user interface and the selection service is drawing a selection border around this user interface. If a selected component has this rule set, you can assume that the component implements <see cref="T:System.ComponentModel.IComponent" /> and that it is associated with a corresponding designer instance.</summary>
	Visible = 0x40000000
}
