namespace System.Web.UI.WebControls;

/// <summary>Indicates the page that the user will be directed to when he or she logs out of the Web site. </summary>
public enum LogoutAction
{
	/// <summary>Reloads the current page with the user logged out.</summary>
	Refresh,
	/// <summary>Redirects the user to a specified URL.</summary>
	Redirect,
	/// <summary>Redirects the user to the login page defined in the site's configuration files (Machine.config and Web.config).</summary>
	RedirectToLoginPage
}
