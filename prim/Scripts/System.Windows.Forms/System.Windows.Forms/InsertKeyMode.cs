namespace System.Windows.Forms;

/// <summary>Represents the insertion mode used by text boxes.</summary>
public enum InsertKeyMode
{
	/// <summary>Honors the current INSERT key mode of the keyboard.</summary>
	Default,
	/// <summary>Indicates that the insertion mode is enabled regardless of the INSERT key mode of the keyboard.</summary>
	Insert,
	/// <summary>Indicates that the overwrite mode is enabled regardless of the INSERT key mode of the keyboard.</summary>
	Overwrite
}
