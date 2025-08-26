namespace System.Web.UI.WebControls;

/// <summary>Provides data for a cancelable event.</summary>
public class LoginCancelEventArgs : EventArgs
{
	private bool _cancel;

	/// <summary>Gets or sets a value indicating whether the event should be canceled.</summary>
	/// <returns>
	///     <see langword="true" /> if the event should be canceled; otherwise, <see langword="false" />.</returns>
	public bool Cancel
	{
		get
		{
			return _cancel;
		}
		set
		{
			_cancel = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> class with the <see cref="P:System.Web.UI.WebControls.LoginCancelEventArgs.Cancel" /> property set to <see langword="false" />.</summary>
	public LoginCancelEventArgs()
		: this(cancel: false)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.LoginCancelEventArgs" /> class with the <see cref="P:System.Web.UI.WebControls.LoginCancelEventArgs.Cancel" /> property set to the specified value.</summary>
	/// <param name="cancel">
	///       <see langword="true" /> to cancel the event; otherwise, <see langword="false" />.</param>
	public LoginCancelEventArgs(bool cancel)
	{
		_cancel = cancel;
	}
}
