namespace System.Web.Profile;

/// <summary>Provides data for the <see cref="E:System.Web.Profile.ProfileModule.Personalize" /> event of the <see cref="T:System.Web.Profile.ProfileModule" /> class.</summary>
public sealed class ProfileEventArgs : EventArgs
{
	private HttpContext _Context;

	private ProfileBase _Profile;

	/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> for the current request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpContext" /> for the current request</returns>
	public HttpContext Context => _Context;

	/// <summary>Gets or sets the user profile for the current request.</summary>
	/// <returns>The user profile to use for the current request. The default is <see langword="null" />.</returns>
	public ProfileBase Profile
	{
		get
		{
			return _Profile;
		}
		set
		{
			_Profile = value;
		}
	}

	/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileEventArgs" /> class.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> of the current request.</param>
	public ProfileEventArgs(HttpContext context)
	{
		_Context = context;
	}
}
