namespace System.Web.UI.WebControls;

/// <summary>Determines the page that the user will go to when a login attempt is not successful.</summary>
public enum LoginFailureAction
{
	/// <summary>Refreshes the current page so that the <see cref="T:System.Web.UI.WebControls.Login" /> control can display an error message.</summary>
	Refresh,
	/// <summary>Redirects the user to the login page defined in the site's configuration files (Machine.config and Web.config).</summary>
	RedirectToLoginPage
}
