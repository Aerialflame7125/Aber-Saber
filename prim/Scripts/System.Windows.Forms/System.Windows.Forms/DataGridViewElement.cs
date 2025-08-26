using System.ComponentModel;

namespace System.Windows.Forms;

/// <summary>Provides the base class for elements of a <see cref="T:System.Windows.Forms.DataGridView" /> control.</summary>
/// <filterpriority>2</filterpriority>
public class DataGridViewElement
{
	private DataGridView dataGridView;

	private DataGridViewElementStates state;

	/// <summary>Gets the <see cref="T:System.Windows.Forms.DataGridView" /> control associated with this element.</summary>
	/// <returns>A <see cref="T:System.Windows.Forms.DataGridView" /> control that contains this element. The default is null.</returns>
	/// <filterpriority>1</filterpriority>
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public DataGridView DataGridView => dataGridView;

	/// <summary>Gets the user interface (UI) state of the element.</summary>
	/// <returns>A bitwise combination of the <see cref="T:System.Windows.Forms.DataGridViewElementStates" /> values representing the state.</returns>
	[Browsable(false)]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	public virtual DataGridViewElementStates State => state;

	/// <summary>Initializes a new instance of the <see cref="T:System.Windows.Forms.DataGridViewElement" /> class.</summary>
	public DataGridViewElement()
	{
		dataGridView = null;
	}

	/// <summary>Called when the element is associated with a different <see cref="T:System.Windows.Forms.DataGridView" />.</summary>
	protected virtual void OnDataGridViewChanged()
	{
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellClick" /> event. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
	protected void RaiseCellClick(DataGridViewCellEventArgs e)
	{
		if (dataGridView != null)
		{
			dataGridView.InternalOnCellClick(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContentClick" /> event. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
	protected void RaiseCellContentClick(DataGridViewCellEventArgs e)
	{
		if (dataGridView != null)
		{
			dataGridView.InternalOnCellContentClick(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellContentDoubleClick" /> event. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
	protected void RaiseCellContentDoubleClick(DataGridViewCellEventArgs e)
	{
		if (dataGridView != null)
		{
			dataGridView.InternalOnCellContentDoubleClick(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.CellValueChanged" /> event. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewCellEventArgs" /> that contains the event data. </param>
	protected void RaiseCellValueChanged(DataGridViewCellEventArgs e)
	{
		if (dataGridView != null)
		{
			dataGridView.InternalOnCellValueChanged(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.DataGridView.DataError" /> event. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.DataGridViewDataErrorEventArgs" /> that contains the event data. </param>
	protected void RaiseDataError(DataGridViewDataErrorEventArgs e)
	{
		if (dataGridView != null)
		{
			dataGridView.InternalOnDataError(e);
		}
	}

	/// <summary>Raises the <see cref="E:System.Windows.Forms.Control.MouseWheel" /> event. </summary>
	/// <param name="e">A <see cref="T:System.Windows.Forms.MouseEventArgs" /> that contains the event data. </param>
	protected void RaiseMouseWheel(MouseEventArgs e)
	{
		if (dataGridView != null)
		{
			dataGridView.InternalOnMouseWheel(e);
		}
	}

	internal virtual void SetDataGridView(DataGridView dataGridView)
	{
		if (dataGridView != DataGridView)
		{
			this.dataGridView = dataGridView;
			OnDataGridViewChanged();
		}
	}

	internal virtual void SetState(DataGridViewElementStates state)
	{
		this.state = state;
	}
}
