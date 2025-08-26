using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowUpdating" /> event.</summary>
public class GridViewUpdateEventArgs : CancelEventArgs
{
	private int rowIndex;

	private IOrderedDictionary keys;

	private IOrderedDictionary newValues;

	private IOrderedDictionary oldValues;

	/// <summary>Gets the index of the row being updated.</summary>
	/// <returns>The index of the row being updated.</returns>
	public int RowIndex => rowIndex;

	/// <summary>Gets a dictionary of field name/value pairs that represent the primary key of the row to update.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object containing field name/value pairs that represent the primary key of the row to update.</returns>
	public IOrderedDictionary Keys => keys;

	/// <summary>Gets a dictionary containing the revised values of the non-key field name/value pairs in the row to update.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object containing the revised values of the non-key field name/value pairs in the row to update.</returns>
	public IOrderedDictionary NewValues => newValues;

	/// <summary>Gets a dictionary containing the original field name/value pairs in the row to update.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> object that contains the original values of the field name/value pairs in the row to update.</returns>
	public IOrderedDictionary OldValues => oldValues;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewUpdateEventArgs" /> class.</summary>
	/// <param name="rowIndex">The index of the row being updated.</param>
	public GridViewUpdateEventArgs(int rowIndex)
	{
		this.rowIndex = rowIndex;
	}

	internal GridViewUpdateEventArgs(int rowIndex, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
	{
		this.rowIndex = rowIndex;
		this.keys = keys;
		this.newValues = newValues;
		this.oldValues = oldValues;
	}
}
