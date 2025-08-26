namespace System.Web.UI.WebControls;

/// <summary>Provides data for the <see cref="E:System.Web.UI.WebControls.Login.Authenticate" /> event.</summary>
public class AuthenticateEventArgs : EventArgs
{
	private bool _authenticated;

	/// <summary>Gets or sets a value indicating whether a user's authentication attempt succeeded.</summary>
	/// <returns>
	///     <see langword="true" /> if the authentication attempt succeeded; otherwise, <see langword="false" />.</returns>
	public bool Authenticated
	{
		get
		{
			return _authenticated;
		}
		set
		{
			_authenticated = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AuthenticateEventArgs" /> class with the <see cref="P:System.Web.UI.WebControls.AuthenticateEventArgs.Authenticated" /> property set to <see langword="false" />.</summary>
	public AuthenticateEventArgs()
		: this(authenticated: false)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.WebControls.AuthenticateEventArgs" /> class.</summary>
	/// <param name="authenticated">
	///       <see langword="true" /> if the user is authenticated; otherwise, <see langword="false" />. </param>
	public AuthenticateEventArgs(bool authenticated)
	{
		_authenticated = authenticated;
	}
}
