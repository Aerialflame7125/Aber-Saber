using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemInserting" /> event.</summary>
public class DetailsViewInsertEventArgs : CancelEventArgs
{
	private object argument;

	private IOrderedDictionary values;

	/// <summary>Gets the command argument for the insert operation passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</summary>
	/// <returns>The command argument for the insert operation passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</returns>
	public object CommandArgument => argument;

	/// <summary>Gets a dictionary that contains the field name/value pairs for the record to insert.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of key field name/value pairs for the record to insert.</returns>
	public IOrderedDictionary Values => values;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewInsertEventArgs" /> class.</summary>
	/// <param name="commandArgument">An optional command argument passed to the <see cref="T:System.Web.UI.WebControls.DetailsView" /> control.</param>
	public DetailsViewInsertEventArgs(object commandArgument)
	{
		argument = commandArgument;
	}

	internal DetailsViewInsertEventArgs(object argument, IOrderedDictionary values)
	{
		this.argument = argument;
		this.values = values;
	}
}
