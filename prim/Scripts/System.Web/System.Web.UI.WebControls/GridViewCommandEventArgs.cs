namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.GridView.RowCommand" /> event.</summary>
public class GridViewCommandEventArgs : CommandEventArgs
{
	private GridViewRow _row;

	private object _commandSource;

	/// <summary>Gets the source of the command.</summary>
	/// <returns>A instance of the <see cref="T:System.Object" /> class that represents the source of the command.</returns>
	public object CommandSource => _commandSource;

	/// <summary>Gets or sets a value that indicates whether the control has handled the event.</summary>
	/// <returns>
	///     <see langword="true" /> if data-bound event code was skipped or has finished; otherwise, <see langword="false" />.</returns>
	public bool Handled { get; set; }

	internal GridViewRow Row => _row;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewCommandEventArgs" /> class using the specified row, source of the command, and event arguments.</summary>
	/// <param name="row">A <see cref="T:System.Web.UI.WebControls.GridViewRow" /> object that represents the row containing the button.</param>
	/// <param name="commandSource">The source of the command.</param>
	/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> object that contains event data.</param>
	public GridViewCommandEventArgs(GridViewRow row, object commandSource, CommandEventArgs originalArgs)
		: base(originalArgs)
	{
		_row = row;
		_commandSource = commandSource;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.GridViewCommandEventArgs" /> class using the specified source of the command and event arguments.</summary>
	/// <param name="commandSource">The source of the command.</param>
	/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> object that contains event data.</param>
	public GridViewCommandEventArgs(object commandSource, CommandEventArgs originalArgs)
		: base(originalArgs)
	{
		_commandSource = commandSource;
	}
}
