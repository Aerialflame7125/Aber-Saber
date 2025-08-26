namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.Repeater.ItemCommand" /> event of a <see cref="T:System.Web.UI.WebControls.Repeater" />. This class cannot be inherited.</summary>
public class RepeaterCommandEventArgs : CommandEventArgs
{
	private RepeaterItem item;

	private object commandSource;

	/// <summary>Gets the <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> associated with the event.</summary>
	/// <returns>The <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> associated with the event.</returns>
	public RepeaterItem Item => item;

	/// <summary>Gets the source of the command.</summary>
	/// <returns>The command source.</returns>
	public object CommandSource => commandSource;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.RepeaterCommandEventArgs" /> class.</summary>
	/// <param name="item">A <see cref="T:System.Web.UI.WebControls.RepeaterItem" /> that represents an item in the <see cref="T:System.Web.UI.WebControls.Repeater" />. The <see cref="P:System.Web.UI.WebControls.RepeaterCommandEventArgs.Item" /> property is set to this value. </param>
	/// <param name="commandSource">The command source. The <see cref="P:System.Web.UI.WebControls.RepeaterCommandEventArgs.CommandSource" /> property is set to this value. </param>
	/// <param name="originalArgs">The original event arguments. </param>
	public RepeaterCommandEventArgs(RepeaterItem item, object commandSource, CommandEventArgs originalArgs)
		: base(originalArgs)
	{
		this.item = item;
		this.commandSource = commandSource;
	}
}
