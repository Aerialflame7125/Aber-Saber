using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemUpdating" /> event.</summary>
public class FormViewUpdateEventArgs : CancelEventArgs
{
	private object argument;

	private IOrderedDictionary keys;

	private IOrderedDictionary oldValues;

	private IOrderedDictionary newValues;

	/// <summary>Gets the command argument for the update operation passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>The command argument for the update operation passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	public object CommandArgument => argument;

	/// <summary>Gets a dictionary that contains the original key field name/value pairs for the record to update.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original key field name/value pairs for the record to update.</returns>
	public IOrderedDictionary Keys => keys;

	/// <summary>Gets a dictionary that contains the new field name/value pairs for the record to update.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the new field name/value pairs for the record to update.</returns>
	public IOrderedDictionary NewValues => newValues;

	/// <summary>Gets a dictionary that contains the original non-key field name/value pairs for the record to update.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of the original non-key field name/value pairs for the record to update.</returns>
	public IOrderedDictionary OldValues => oldValues;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewUpdateEventArgs" /> class.</summary>
	/// <param name="commandArgument">An optional command argument passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</param>
	public FormViewUpdateEventArgs(object commandArgument)
	{
		argument = commandArgument;
	}

	internal FormViewUpdateEventArgs(object argument, IOrderedDictionary keys, IOrderedDictionary oldValues, IOrderedDictionary newValues)
		: this(argument)
	{
		this.keys = keys;
		this.oldValues = oldValues;
		this.newValues = newValues;
	}
}
