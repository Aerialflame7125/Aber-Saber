namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see langword="Command" /> event.</summary>
public class CommandEventArgs : EventArgs
{
	private string commandName;

	private object argument;

	/// <summary>Gets the name of the command.</summary>
	/// <returns>The name of the command to perform.</returns>
	public string CommandName => commandName;

	/// <summary>Gets the argument for the command.</summary>
	/// <returns>A <see cref="T:System.Object" /> that contains the argument for the command.</returns>
	public object CommandArgument => argument;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> class with another <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> object.</summary>
	/// <param name="e">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains the event data. </param>
	public CommandEventArgs(CommandEventArgs e)
		: this(e.CommandName, e.CommandArgument)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> class with the specified command name and argument.</summary>
	/// <param name="commandName">The name of the command. </param>
	/// <param name="argument">A <see cref="T:System.Object" /> that contains the arguments for the command. </param>
	public CommandEventArgs(string commandName, object argument)
	{
		this.commandName = commandName;
		this.argument = argument;
	}
}
