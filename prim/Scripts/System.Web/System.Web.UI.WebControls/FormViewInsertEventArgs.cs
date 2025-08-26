using System.Collections.Specialized;
using System.ComponentModel;

namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemInserting" /> event.</summary>
public class FormViewInsertEventArgs : CancelEventArgs
{
	private object argument;

	private IOrderedDictionary values;

	/// <summary>Gets the command argument for the insert operation passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</summary>
	/// <returns>The command argument for the insert operation passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</returns>
	public object CommandArgument => argument;

	/// <summary>Gets a dictionary that contains the field name/value pairs for the record to insert.</summary>
	/// <returns>An <see cref="T:System.Collections.Specialized.IOrderedDictionary" /> that contains a dictionary of key field name/value pairs for the record to insert.</returns>
	public IOrderedDictionary Values => values;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewInsertEventArgs" /> class.</summary>
	/// <param name="commandArgument">An optional command argument passed to the <see cref="T:System.Web.UI.WebControls.FormView" /> control.</param>
	public FormViewInsertEventArgs(object commandArgument)
	{
		argument = commandArgument;
	}

	internal FormViewInsertEventArgs(object argument, IOrderedDictionary values)
	{
		this.values = values;
		this.argument = argument;
	}
}
