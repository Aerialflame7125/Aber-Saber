using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemUpdating" /> event.</summary>
public class DetailsViewUpdateEventArgs : CancelEventArgs
{
	private object argument;

	private IOrderedDictionary keys;

	private IOrderedDictionary newValues;

	private IOrderedDictionary oldValues;

	/// <summary>Gets the command argument for the update operation passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
	/// <returns>The command argument for the update operation passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
	public object CommandArgument => argument;

	/// <summary>Gets a dictionary that contains the key field name/value pairs for the record to update.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of key field name/value pairs for the record to update.</returns>
	public IOrderedDictionary Keys => keys;

	/// <summary>Gets a dictionary that contains the new field name/value pairs for the record to update.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the new field name/value pairs for the record to update.</returns>
	public IOrderedDictionary NewValues => newValues;

	/// <summary>Gets a dictionary that contains the original field name/value pairs for the record to update.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original field name/value pairs for the record to update.</returns>
	public IOrderedDictionary OldValues => oldValues;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewUpdateEventArgs" /> class.</summary>
	/// <param name="commandArgument">An optional command argument passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</param>
	public DetailsViewUpdateEventArgs(object commandArgument)
	{
		argument = commandArgument;
	}

	internal DetailsViewUpdateEventArgs(object argument, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
	{
		this.argument = argument;
		this.keys = keys;
		this.newValues = newValues;
		this.oldValues = oldValues;
	}
}
