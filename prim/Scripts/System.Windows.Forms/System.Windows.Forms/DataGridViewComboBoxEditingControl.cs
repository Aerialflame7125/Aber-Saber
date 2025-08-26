using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents the hosted combo box control in a <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" />.</summary>
/// <filterpriority>2</filterpriority>
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class DataGridViewComboBoxEditingControl : ComboBox, IDataGridViewEditingControl
{
	private DataGridView editingControlDataGridView;

	private object editingControlFormattedValue;

	private int editingControlRowIndex;

	private bool editingControlValueChanged;

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGridView" /> that contains the combo box control.</summary>
	/// <returns>The <see cref="T:System.Windows.Forms.DataGridView" /> that contains the <see cref="T:System.Windows.Forms.DataGridViewComboBoxCell" /> that contains this control; otherwise, null if there is no associated <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
	public virtual DataGridView EditingControlDataGridView
	{
		get
		{
			return editingControlDataGridView;
		}
		set
		{
			editingControlDataGridView = value;
		}
	}

	/// <summary>Gets or sets the formatted representation of the current value of the control.</summary>
	/// <returns>An object representing the current value of this control.</returns>
	public virtual object EditingControlFormattedValue
	{
		get
		{
			return editingControlFormattedValue;
		}
		set
		{
			editingControlFormattedValue = value;
		}
	}

	/// <summary>Gets or sets the index of the owning cell's parent row.</summary>
	/// <returns>The index of the row that contains the owning cell; -1 if there is no owning row.</returns>
	public virtual int EditingControlRowIndex
	{
		get
		{
			return editingControlRowIndex;
		}
		set
		{
			editingControlRowIndex = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the current value of the control has changed.</summary>
	/// <returns>true if the value of the control has changed; otherwise, false.</returns>
	public virtual bool EditingControlValueChanged
	{
		get
		{
			return editingControlValueChanged;
		}
		set
		{
			editingControlValueChanged = value;
		}
	}

	/// <summary>Gets the cursor used during editing.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the cursor image used by the mouse pointer during editing.</returns>
	public virtual Cursor EditingPanelCursor => Cursors.Default;

	/// <summary>Gets a value indicating whether the cell contents need to be repositioned whenever the value changes.</summary>
	/// <returns>false in all cases.</returns>
	public virtual bool RepositionEditingControlOnValueChange => false;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewComboBoxEditingControl" /> class.</summary>
	public DataGridViewComboBoxEditingControl()
	{
		editingControlValueChanged = false;
	}

	/// <summary>Changes the control's user interface (UI) to be consistent with the specified cell style.</summary>
	/// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to use as a pattern for the UI.</param>
	public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
	{
	}

	/// <summary>Determines whether the specified key is a regular input key that the editing control should process or a special key that the <see cref="T:System.Windows.Forms.DataGridView" /> should process.</summary>
	/// <returns>true if the specified key is a regular input key that should be handled by the editing control; otherwise, false.</returns>
	/// <param name="keyData">A bitwise combination of <see cref="T:System.Windows.Forms.Keys" /> values that represents the key that was pressed.</param>
	/// <param name="dataGridViewWantsInputKey">true to indicate that the <see cref="T:System.Windows.Forms.DataGridView" /> control can process the key; otherwise, false.</param>
	public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
	{
		return IsInputKey(keyData);
	}

	/// <summary>Retrieves the formatted value of the cell.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the formatted version of the cell contents.</returns>
	/// <param name="context">A bitwise combination of <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the data error context.</param>
	public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
	{
		return Text;
	}

	/// <summary>Prepares the currently selected cell for editing.</summary>
	/// <param name="selectAll">true to select all of the cell's content; otherwise, false.</param>
	public virtual void PrepareEditingControlForEdit(bool selectAll)
	{
	}

	/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
	protected override void OnSelectedIndexChanged(EventArgs e)
	{
		base.OnSelectedIndexChanged(e);
	}
}
