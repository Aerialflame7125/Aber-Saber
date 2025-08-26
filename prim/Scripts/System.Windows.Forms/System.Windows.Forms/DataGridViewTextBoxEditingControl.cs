using System.Runtime.InteropServices;

namespace System.Windows.Forms;

/// <summary>Represents a text box control that can be hosted in a <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" />. </summary>
/// <filterpriority>2</filterpriority>
[ClassInterface(ClassInterfaceType.AutoDispatch)]
[ComVisible(true)]
public class DataGridViewTextBoxEditingControl : TextBox, IDataGridViewEditingControl
{
	private DataGridView editingControlDataGridView;

	private int rowIndex;

	private bool editingControlValueChanged;

	private bool repositionEditingControlOnValueChange;

	/// <summary>Gets or sets the <see cref="T:System.Windows.Forms.DataGridView" /> that contains the text box control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridView" /> that contains the <see cref="T:System.Windows.Forms.DataGridViewTextBoxCell" /> that contains this control; otherwise, null if there is no associated <see cref="T:System.Windows.Forms.DataGridView" />.</returns>
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

	/// <summary>Gets or sets the formatted representation of the current value of the text box control.</summary>
	/// <returns>An object representing the current value of this control.</returns>
	public virtual object EditingControlFormattedValue
	{
		get
		{
			return base.Text;
		}
		set
		{
			base.Text = (string)value;
		}
	}

	/// <summary>Gets or sets the index of the owning cell's parent row.</summary>
	/// <returns>The index of the row that contains the owning cell; -1 if there is no owning row.</returns>
	public virtual int EditingControlRowIndex
	{
		get
		{
			return rowIndex;
		}
		set
		{
			rowIndex = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the current value of the text box control has changed.</summary>
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

	/// <summary>Gets the cursor used when the mouse pointer is over the <see cref="P:System.Windows.Forms.DataGridView.EditingPanel" /> but not over the editing control.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.Cursor" /> that represents the mouse pointer used for the editing panel. </returns>
	public virtual Cursor EditingPanelCursor => Cursors.Default;

	/// <summary>Gets a value indicating whether the cell contents need to be repositioned whenever the value changes.</summary>
	/// <returns>true if the cell's <see cref="P:System.Windows.Forms.DataGridViewCellStyle.WrapMode" /> is set to true and the alignment property is not set to one of the <see cref="T:System.Windows.Forms.DataGridViewContentAlignment" /> values that aligns the content to the top; otherwise, false.</returns>
	public virtual bool RepositionEditingControlOnValueChange => repositionEditingControlOnValueChange;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewTextBoxEditingControl" /> class.</summary>
	public DataGridViewTextBoxEditingControl()
	{
		repositionEditingControlOnValueChange = false;
	}

	/// <summary>Changes the control's user interface (UI) to be consistent with the specified cell style.</summary>
	/// <param name="dataGridViewCellStyle">The <see cref="T:System.Windows.Forms.DataGridViewCellStyle" /> to use as the model for the UI.</param>
	public virtual void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
	{
		Font = dataGridViewCellStyle.Font;
		BackColor = dataGridViewCellStyle.BackColor;
		ForeColor = dataGridViewCellStyle.ForeColor;
	}

	/// <summary>Determines whether the specified key is a regular input key that the editing control should process or a special key that the <see cref="T:System.Windows.Forms.DataGridView" /> should process.</summary>
	/// <returns>true if the specified key is a regular input key that should be handled by the editing control; otherwise, false.</returns>
	/// <param name="keyData">A <see cref="T:System.Windows.Forms.Keys" /> that represents the key that was pressed.</param>
	/// <param name="dataGridViewWantsInputKey">true when the <see cref="T:System.Windows.Forms.DataGridView" /> wants to process the <paramref name="keyData" />; otherwise, false.</param>
	public virtual bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
	{
		switch (keyData)
		{
		case Keys.Left:
			return base.SelectionStart != 0;
		case Keys.Right:
			return base.SelectionStart != TextLength;
		case Keys.Up:
		case Keys.Down:
			return false;
		default:
			return true;
		}
	}

	/// <summary>Retrieves the formatted value of the cell.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the formatted version of the cell contents.</returns>
	/// <param name="context">One of the <see cref="T:System.Windows.Forms.DataGridViewDataErrorContexts" /> values that specifies the data error context.</param>
	public virtual object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
	{
		return EditingControlFormattedValue;
	}

	/// <summary>Prepares the currently selected cell for editing.</summary>
	/// <param name="selectAll">true to select the cell contents; otherwise, false.</param>
	public virtual void PrepareEditingControlForEdit(bool selectAll)
	{
		Focus();
		if (selectAll)
		{
			SelectAll();
		}
		editingControlValueChanged = false;
	}

	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected override void OnMouseWheel(MouseEventArgs e)
	{
		base.OnMouseWheel(e);
	}

	protected override void OnTextChanged(EventArgs e)
	{
		base.OnTextChanged(e);
		editingControlValueChanged = true;
	}

	/// <summary>Processes key events.</summary>
	/// <returns>true if the key event was handled by the editing control; otherwise, false.</returns>
	/// <param name="m">A <see cref="T:System.Windows.Forms.Message" /> indicating the key that was pressed.</param>
	protected override bool ProcessKeyEventArgs(ref Message m)
	{
		return base.ProcessKeyEventArgs(ref m);
	}
}
