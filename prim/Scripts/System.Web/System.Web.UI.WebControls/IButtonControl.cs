namespace System.Web.UI.WebControls;

/// <summary>Defines properties and events that must be implemented to allow a control to act like a button on a Web page.</summary>
public interface IButtonControl
{
	/// <summary>Gets or sets a value indicating whether clicking the button causes page validation to occur.</summary>
	/// <returns>
	///     <see langword="true" /> if clicking the button causes page validation to occur; otherwise, <see langword="false" />.</returns>
	bool CausesValidation { get; set; }

	/// <summary>Gets or sets an optional argument that is propagated to the <see cref="E:System.Web.UI.WebControls.IButtonControl.Command" /> event.</summary>
	/// <returns>The argument that is propagated to the <see cref="E:System.Web.UI.WebControls.IButtonControl.Command" /> event.</returns>
	string CommandArgument { get; set; }

	/// <summary>Gets or sets the command name that is propagated to the <see cref="E:System.Web.UI.WebControls.IButtonControl.Command" /> event.</summary>
	/// <returns>The name of the command that is propagated to the <see cref="E:System.Web.UI.WebControls.IButtonControl.Command" /> event.</returns>
	string CommandName { get; set; }

	/// <summary>Gets or sets the URL of the Web page to post to from the current page when the button control is clicked.</summary>
	/// <returns>The URL of the Web page to post to from the current page when the button control is clicked.</returns>
	string PostBackUrl { get; set; }

	/// <summary>Gets or sets the text caption displayed for the button.</summary>
	/// <returns>The text caption displayed for the button.</returns>
	string Text { get; set; }

	/// <summary>Gets or sets the name for the group of controls for which the button control causes validation when it posts back to the server.</summary>
	/// <returns>The name for the group of controls for which the button control causes validation when it posts back to the server.</returns>
	string ValidationGroup { get; set; }

	/// <summary>Occurs when the button control is clicked.</summary>
	event EventHandler Click;

	/// <summary>Occurs when the button control is clicked.</summary>
	event CommandEventHandler Command;
}
