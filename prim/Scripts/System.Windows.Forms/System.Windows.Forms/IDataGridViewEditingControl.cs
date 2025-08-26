namespace System.Windows.Forms;

/// <summary>Defines common functionality for controls that are hosted within cells of a <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
/// <filterpriority>2</filterpriority>
public interface IDataGridViewEditingControl
{
	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGridView" /> that contains the cell.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridView" /> that contains the <see cref="T:System.Windows.Forms.DataGridViewCell" /> that is being edited; null if there is no associated <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
	DataGridView EditingControlDataGridView { get; set; }

	/// <summary>Gets or sets the formatted value of the cell being modified by the editor.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the formatted value of the cell.</returns>
	object EditingControlFormattedValue { get; set; }

	/// <summary>Gets or sets the index of the hosting cell's parent row.</summary>
	/// <returns>The index of the row that contains the cell, or –1 if there is no parent row.</returns>
	int EditingControlRowIndex { get; set; }

	/// <summary>Gets or sets a value indicating whether the value of the editing control differs from the value of the hosting cell.</summary>
	/// <returns>true if the value of the control differs from the cell value; otherwise, false.</returns>
	bool EditingControlValueChanged { get; set; }

	/// <summary>Gets the cursor used when the mouse pointer is over the <see cref="P:System.Windows.Forms.DataGridView.EditingPanel" /> but not over the editing control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the mouse pointer used for the editing panel. </returns>
	Cursor EditingPanelCursor { get; }

	/// <summary>Gets or sets a value indicating whether the cell contents need to be repositioned whenever the value changes.</summary>
	/// <returns>true if the contents need to be repositioned; otherwise, false.</returns>
	bool RepositionEditingControlOnValueChange { get; }

	/// <summary>Changes the control's user interface (UI) to be consistent with the specified cell style.</summary>
	/// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to use as the model for the UI.</param>
	void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle);

	/// <summary>Determines whether the specified key is a regular input key that the editing control should process or a special key that the <see cref="T:System.Windows.Forms.DataGridView" /> should process.</summary>
	/// <returns>true if the specified key is a regular input key that should be handled by the editing control; otherwise, false.</returns>
	/// <param name="keyData">A <see cref="T:System.Windows.Forms.Keys" /> that represents the key that was pressed.</param>
	/// <param name="dataGridViewWantsInputKey">true when the <see cref="T:System.Windows.Forms.DataGridView" /> wants to process the <see cref="T:System.Windows.Forms.Keys" /> in <paramref name="keyData" />; otherwise, false.</param>
	bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey);

	/// <summary>Retrieves the formatted value of the cell.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the formatted version of the cell contents.</returns>
	/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the context in which the data is needed.</param>
	object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context);

	/// <summary>Prepares the currently selected cell for editing.</summary>
	/// <param name="selectAll">true to select all of the cell's content; otherwise, false.</param>
	void PrepareEditingControlForEdit(bool selectAll);
}
