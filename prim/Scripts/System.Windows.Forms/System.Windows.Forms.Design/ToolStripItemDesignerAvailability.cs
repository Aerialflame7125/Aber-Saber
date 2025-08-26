namespace System.Windows.Forms.Design;

/// <summary>Specifies controls that are visible in the designer.</summary>
[Flags]
public enum ToolStripItemDesignerAvailability
{
	/// <summary>Specifies that no controls are visible.</summary>
	None = 0,
	/// <summary>Specifies that <see cref="T:System.Windows.Forms.ToolStrip" /> is visible.</summary>
	ToolStrip = 1,
	/// <summary>Specifies that <see cref="T:System.Windows.Forms.MenuStrip" /> is visible.</summary>
	MenuStrip = 2,
	/// <summary>Specifies that <see cref="T:System.Windows.Forms.ContextMenuStrip" /> is visible.</summary>
	ContextMenuStrip = 4,
	/// <summary>Specifies that <see cref="T:System.Windows.Forms.StatusStrip" /> is visible.</summary>
	StatusStrip = 8,
	/// <summary>Specifies that all controls are visible.</summary>
	All = 0xF
}
