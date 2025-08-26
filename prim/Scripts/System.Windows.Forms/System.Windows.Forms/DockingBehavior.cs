namespace System.Windows.Forms;

/// <summary>Specifies how a control should be docked by default when added through a designer.</summary>
/// <filterpriority>2</filterpriority>
public enum DockingBehavior
{
	/// <summary>Do not prompt the user for the desired docking behavior.</summary>
	Never,
	/// <summary>Prompt the user for the desired docking behavior.</summary>
	Ask,
	/// <summary>Set the control's <see cref="P:System.Windows.Forms.Control.Dock" /> property to <see cref="F:System.Windows.Forms.DockStyle.Fill" />  when it is dropped into a container with no other child controls.</summary>
	AutoDock
}
