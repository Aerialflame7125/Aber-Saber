namespace System.Web.Profile;

/// <summary>Provides data for the <see cref="E:System.Web.Profile.ProfileModule.MigrateAnonymous" /> event of the <see cref="T:System.Web.Profile.ProfileModule" /> class.</summary>
public sealed class ProfileMigrateEventArgs : EventArgs
{
	private HttpContext context;

	private string anonymousId;

	/// <summary>Gets the anonymous identifier for the anonymous profile from which to migrate profile property values.</summary>
	/// <returns>The anonymous identifier for the anonymous profile from which to migrate profile property values.</returns>
	public string AnonymousID => anonymousId;

	/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> for the current request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpContext" /> for the current request</returns>
	public HttpContext Context => context;

	/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileMigrateEventArgs" /> class.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> of the current request.</param>
	/// <param name="anonymousId">The anonymous identifier being migrated from.</param>
	public ProfileMigrateEventArgs(HttpContext context, string anonymousId)
	{
		this.context = context;
		this.anonymousId = anonymousId;
	}
}
