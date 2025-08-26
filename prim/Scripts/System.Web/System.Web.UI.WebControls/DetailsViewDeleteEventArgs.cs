using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemDeleting" /> event. </summary>
public class DetailsViewDeleteEventArgs : CancelEventArgs
{
	private int rowIndex;

	private IOrderedDictionary keys;

	private IOrderedDictionary values;

	/// <summary>Gets the index of the row being deleted.</summary>
	/// <returns>The index of the row being deleted.</returns>
	public int RowIndex => rowIndex;

	/// <summary>Gets an ordered dictionary of key field name/value pairs that contains the names and values of the key fields of the deleted items.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains an ordered dictionary of key field name/value pairs used to match the item to delete.</returns>
	public IOrderedDictionary Keys => keys;

	/// <summary>Gets a dictionary of the non-key field name/value pairs for the item to delete.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the non-key field name/value pairs for the item to delete.</returns>
	public IOrderedDictionary Values => values;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewDeleteEventArgs" /> class.</summary>
	/// <param name="rowIndex">The index of the row being deleted.</param>
	public DetailsViewDeleteEventArgs(int rowIndex)
	{
		this.rowIndex = rowIndex;
	}

	internal DetailsViewDeleteEventArgs(int index, IOrderedDictionary keys, IOrderedDictionary values)
	{
		rowIndex = index;
		this.keys = keys;
		this.values = values;
	}
}
