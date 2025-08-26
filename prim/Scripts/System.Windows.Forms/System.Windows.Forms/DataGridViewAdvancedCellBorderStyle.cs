namespace System.Windows.Forms;

/// <summary>Specifies the border styles that can be applied to the cells of a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
/// <filterpriority>2</filterpriority>
public enum DataGridViewAdvancedCellBorderStyle
{
	/// <summary>The border is not set.</summary>
	NotSet,
	/// <summary>No borders.</summary>
	None,
	/// <summary>A single-line border.</summary>
	Single,
	/// <summary>A single-line sunken border.</summary>
	Inset,
	/// <summary>A double-line sunken border.</summary>
	InsetDouble,
	/// <summary>A single-line raised border.</summary>
	Outset,
	/// <summary>A double-line raised border.</summary>
	OutsetDouble,
	/// <summary>A single-line border containing a raised portion.</summary>
	OutsetPartial
}
