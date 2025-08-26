namespace System.Web.Security;

/// <summary>Represents the method that handles the <see langword="PassportAuthentication_OnAuthenticate" /> event of a <see cref="T:System.Web.Security.PassportAuthenticationModule" />. This class is deprecated.</summary>
/// <param name="sender">The object that raised the event. </param>
/// <param name="e">A <see cref="T:System.Web.Security.PassportAuthenticationEventArgs" /> object that contains the event data. </param>
[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
public delegate void PassportAuthenticationEventHandler(object sender, PassportAuthenticationEventArgs e);
