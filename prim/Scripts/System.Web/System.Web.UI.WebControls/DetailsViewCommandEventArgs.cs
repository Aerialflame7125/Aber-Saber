namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.DetailsView.ItemCommand" /> event.</summary>
public class DetailsViewCommandEventArgs : CommandEventArgs
{
	private object _commandSource;

	/// <summary>Gets the source of the command.</summary>
	/// <returns>An instance of the <see cref="T:System.Object" /> class that represents the source of the command.</returns>
	public object CommandSource => _commandSource;

	/// <summary>Gets or sets a value that indicates whether the control has handled the event.</summary>
	/// <returns>
	///     <see langword="true" /> if data-bound event code was skipped or has finished running; otherwise, <see langword="false" />.</returns>
	public bool Handled { get; set; }

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.DetailsViewCommandEventArgs" /> class.</summary>
	/// <param name="commandSource">The source of the command.</param>
	/// <param name="originalArgs">A <see cref="T:System.Web.UI.WebControls.CommandEventArgs" /> that contains event data.</param>
	public DetailsViewCommandEventArgs(object commandSource, CommandEventArgs originalArgs)
		: base(originalArgs)
	{
		_commandSource = commandSource;
	}
}
