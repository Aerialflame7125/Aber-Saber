namespace System.Windows.Forms;

/// <summary>Defines values for specifying the parts of a <see cref="T:System.Windows.Forms.DataGridViewCell" /> that are to be painted.</summary>
[Flags]
public enum DataGridViewPaintParts
{
	/// <summary>Nothing should be painted.</summary>
	None = 0,
	/// <summary>The background of the cell should be painted.</summary>
	Background = 1,
	/// <summary>The border of the cell should be painted.</summary>
	Border = 2,
	/// <summary>The background of the cell content should be painted.</summary>
	ContentBackground = 4,
	/// <summary>The foreground of the cell content should be painted.</summary>
	ContentForeground = 8,
	/// <summary>The cell error icon should be painted.</summary>
	ErrorIcon = 0x10,
	/// <summary>The focus rectangle should be painted around the cell.</summary>
	Focus = 0x20,
	/// <summary>The background of the cell should be painted when the cell is selected.</summary>
	SelectionBackground = 0x40,
	/// <summary>All parts of the cell should be painted.</summary>
	All = 0x7F
}
