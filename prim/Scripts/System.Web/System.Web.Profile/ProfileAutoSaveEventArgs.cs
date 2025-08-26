namespace System.Web.Profile;

/// <summary>Provides data for the <see cref="E:System.Web.Profile.ProfileModule.ProfileAutoSaving" /> event of the <see cref="T:System.Web.Profile.ProfileModule" /> class.</summary>
public sealed class ProfileAutoSaveEventArgs : EventArgs
{
	private HttpContext context;

	private bool continueWithProfileAutoSave;

	/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> for the current request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpContext" /> for the current request</returns>
	public HttpContext Context => context;

	/// <summary>Gets or sets a value indicating whether the <see cref="T:System.Web.Profile.ProfileModule" /> will automatically save the user profile.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.Profile.ProfileModule" /> will automatically save the user profile; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public bool ContinueWithProfileAutoSave
	{
		get
		{
			return continueWithProfileAutoSave;
		}
		set
		{
			continueWithProfileAutoSave = value;
		}
	}

	/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileAutoSaveEventArgs" /> class.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> of the current request.</param>
	public ProfileAutoSaveEventArgs(HttpContext context)
	{
		this.context = context;
		continueWithProfileAutoSave = true;
	}
}
