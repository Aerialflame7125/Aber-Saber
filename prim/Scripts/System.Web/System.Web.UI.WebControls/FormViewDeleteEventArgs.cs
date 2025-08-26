using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemDeleting" /> event.</summary>
public class FormViewDeleteEventArgs : CancelEventArgs
{
	private int rowIndex;

	private IOrderedDictionary keys;

	private IOrderedDictionary values;

	/// <summary>Gets the index of the record being deleted from the data source.</summary>
	/// <returns>The index of the record being deleted from the data source.</returns>
	public int RowIndex => rowIndex;

	/// <summary>Gets an ordered dictionary of key field name/value pairs for the record to delete.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.OrderedDictionary" /> that contains the key field name/value pairs for the record to delete.</returns>
	public IOrderedDictionary Keys => keys;

	/// <summary>Gets a dictionary of the non-key field name/value pairs for the item to delete.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.OrderedDictionary" /> that contains the non-key field name/value pairs for the item to delete.</returns>
	public IOrderedDictionary Values => values;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewDeleteEventArgs" /> class.</summary>
	/// <param name="rowIndex">The index of the row being deleted.</param>
	public FormViewDeleteEventArgs(int rowIndex)
	{
		this.rowIndex = rowIndex;
	}

	internal FormViewDeleteEventArgs(int index, IOrderedDictionary keys, IOrderedDictionary values)
		: this(index)
	{
		this.keys = keys;
		this.values = values;
	}
}
