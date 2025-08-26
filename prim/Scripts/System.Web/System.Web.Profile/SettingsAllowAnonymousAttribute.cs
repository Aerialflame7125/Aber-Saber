namespace System.Web.Profile;

/// <summary>Identifies whether a profile property can be set or accessed for an anonymous user.</summary>
[AttributeUsage(AttributeTargets.Property)]
public sealed class SettingsAllowAnonymousAttribute : Attribute
{
	private bool allow;

	/// <summary>Gets a value indicating whether the associated property of a custom profile implementation can be accessed if the user is an anonymous user.</summary>
	/// <returns>
	///     <see langword="true" /> if anonymous users can access the associated profile property; otherwise, <see langword="false" />.</returns>
	public bool Allow => allow;

	/// <summary>Creates a new instance of the <see cref="T:System.Web.Profile.SettingsAllowAnonymousAttribute" /> class and specifies whether to allow anonymous access to the associated profile property.</summary>
	/// <param name="allow">
	///       <see langword="true" /> if anonymous users can access the associated profile property; otherwise <see langword="false" />.</param>
	public SettingsAllowAnonymousAttribute(bool allow)
	{
		this.allow = allow;
	}

	/// <summary>Gets a value indicating whether the <see cref="P:System.Web.Profile.SettingsAllowAnonymousAttribute.Allow" /> property is set to the default value.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="P:System.Web.Profile.SettingsAllowAnonymousAttribute.Allow" /> property is set to the default value; otherwise <see langword="false" />.</returns>
	[MonoTODO("Not implemented")]
	public override bool IsDefaultAttribute()
	{
		throw new NotImplementedException();
	}
}
