using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowCancelingEdit" /> event.</summary>
public class GridViewCancelEditEventArgs : CancelEventArgs
{
	private int _rowIndex;

	/// <summary>Gets the index of the row containing the Cancel button that raised the event.</summary>
	/// <returns>The zero-based index of the row containing the Cancel button that raised the event.</returns>
	public int RowIndex => _rowIndex;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewCancelEditEventArgs" /> class.</summary>
	/// <param name="rowIndex">The index of the row containing the Cancel button that raised the event. </param>
	public GridViewCancelEditEventArgs(int rowIndex)
	{
		_rowIndex = rowIndex;
	}
}
