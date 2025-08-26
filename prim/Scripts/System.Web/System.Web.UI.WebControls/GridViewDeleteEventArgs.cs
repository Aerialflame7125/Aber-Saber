using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowDeleting" /> event.</summary>
public class GridViewDeleteEventArgs : CancelEventArgs
{
	private int rowIndex;

	private IOrderedDictionary keys;

	private IOrderedDictionary values;

	/// <summary>Gets the index of the row being deleted.</summary>
	/// <returns>The zero-based index of the row being deleted.</returns>
	public int RowIndex => rowIndex;

	/// <summary>Gets a dictionary of field name/value pairs that represent the primary key of the row to delete.</summary>
	/// <returns>A dictionary that contains field name/value pairs that represent the primary key of the row to delete.</returns>
	public IOrderedDictionary Keys => keys;

	/// <summary>Gets a dictionary of the non-key field name/value pairs for the row to delete.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object that contains the non-key field name/value pairs of the row to delete.</returns>
	public IOrderedDictionary Values => values;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewDeleteEventArgs" /> class.</summary>
	/// <param name="rowIndex">The index of the row that contains the Delete button that raised the event. </param>
	public GridViewDeleteEventArgs(int rowIndex)
	{
		this.rowIndex = rowIndex;
	}

	internal GridViewDeleteEventArgs(int index, IOrderedDictionary keys, IOrderedDictionary values)
	{
		rowIndex = index;
		this.keys = keys;
		this.values = values;
	}
}
