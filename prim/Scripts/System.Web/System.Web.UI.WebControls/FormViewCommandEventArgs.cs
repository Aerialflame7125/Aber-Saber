namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.FormView.ItemCommand" /> event.</summary>
public class FormViewCommandEventArgs : CommandEventArgs
{
	private object source;

	/// <summary>Gets the control that raised the event.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the control that raised the event.</returns>
	public object CommandSource => source;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.FormViewCommandEventArgs" /> class.</summary>
	/// <param name="commandSource">The source of the command.</param>
	/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains event data.</param>
	public FormViewCommandEventArgs(object commandSource, CommandEventArgs originalArgs)
		: base(originalArgs)
	{
		source = commandSource;
	}
}
