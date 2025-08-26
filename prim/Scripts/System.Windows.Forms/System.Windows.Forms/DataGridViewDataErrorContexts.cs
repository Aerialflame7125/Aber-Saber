namespace System.Windows.Forms;

/// <summary>Represents the state of a data-bound <see cref="T:System.Windows.Forms.DataGridView" /> control when a data error occurred.</summary>
[Flags]
public enum DataGridViewDataErrorContexts
{
	/// <summary>A data error occurred when trying to format data that is either being sent to a data store, or being loaded from a data store. This value indicates that a change to a cell failed to format correctly. Either the new cell value needs to be corrected or the cell's formatting needs to change.</summary>
	Formatting = 1,
	/// <summary>A data error occurred when displaying a cell that was populated by a data source. This value indicates that the value from the data source cannot be displayed by the cell, or a mapping that translates the value from the data source to the cell is missing.</summary>
	Display = 2,
	/// <summary>A data error occurred when calculating the preferred size of a cell. This value indicates that the <see cref="T:System.Windows.Forms.DataGridView" /> failed to calculate the preferred width or height of a cell when programmatically resizing a column or row. This can occur if the cell failed to format its value.</summary>
	PreferredSize = 4,
	/// <summary>A data error occurred when deleting a row. This value indicates that the underlying data store threw an exception when a data-bound <see cref="T:System.Windows.Forms.DataGridView" /> deleted a row.</summary>
	RowDeletion = 8,
	/// <summary>A data error occurred when parsing new data. This value indicates that the <see cref="T:System.Windows.Forms.DataGridView" /> could not parse new data that was entered by the user or loaded from the underlying data store.</summary>
	Parsing = 0x100,
	/// <summary>A data error occurred when committing changes to the data store. This value indicates that data entered in a cell could not be committed to the underlying data store.</summary>
	Commit = 0x200,
	/// <summary>A data error occurred when restoring a cell to its previous value. This value indicates that a cell tried to cancel an edit and the rollback to the initial value failed. This can occur if the cell formatting changed so that it is incompatible with the initial value.</summary>
	InitialValueRestoration = 0x400,
	/// <summary>A data error occurred when the <see cref="T:System.Windows.Forms.DataGridView" /> lost focus. This value indicates that the <see cref="T:System.Windows.Forms.DataGridView" /> could not commit user changes after losing focus.</summary>
	LeaveControl = 0x800,
	/// <summary>A data error occurred when the selection cursor moved to another cell. This value indicates that a user selected a cell when the previously selected cell had an error condition.</summary>
	CurrentCellChange = 0x1000,
	/// <summary>A data error occurred when scrolling a new region into view. This value indicates that a cell with data errors scrolled into view programmatically or with the scroll bar.</summary>
	Scroll = 0x2000,
	/// <summary>A data error occurred when copying content to the Clipboard. This value indicates that the cell value could not be converted to a string.</summary>
	ClipboardContent = 0x4000
}
