namespace System.Web.Profile;

/// <summary>Provides information about a user profile.</summary>
[Serializable]
public class ProfileInfo
{
	private string _UserName;

	private DateTime _LastActivityDate;

	private DateTime _LastUpdatedDate;

	private bool _IsAnonymous;

	private int _Size;

	/// <summary>Gets the user name for the profile.</summary>
	/// <returns>The user name for the profile.</returns>
	public virtual string UserName => _UserName;

	/// <summary>Gets the last date and time when the profile was read or updated.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> that represents the last date and time when the profile was read or updated.</returns>
	public virtual DateTime LastActivityDate => _LastActivityDate.ToLocalTime();

	/// <summary>Gets the last date and time when the profile was updated.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> that represents the last date and time when the profile was updated.</returns>
	public virtual DateTime LastUpdatedDate => _LastUpdatedDate.ToLocalTime();

	/// <summary>Gets whether the user name for the profile is anonymous.</summary>
	/// <returns>
	///     <see langword="true" /> if the user name for the profile is anonymous; otherwise, <see langword="false" />.</returns>
	public virtual bool IsAnonymous => _IsAnonymous;

	/// <summary>Gets the size of the profile property names and values stored in the data source.</summary>
	/// <returns>The size of the profile property names and values stored in the data source.</returns>
	public virtual int Size => _Size;

	/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileInfo" /> class with the specified property values.</summary>
	/// <param name="username">The user name for the profile.</param>
	/// <param name="isAnonymous">
	///       <see langword="true" /> to indicate the profile is for an anonymous user; <see langword="false" /> to indicate the profile is for an authenticated user.</param>
	/// <param name="lastActivityDate">The last date and time when the profile was read or updated.</param>
	/// <param name="lastUpdatedDate">The last date and time when the profile was updated.</param>
	/// <param name="size">The size of the profile information and values stored in the data source.</param>
	public ProfileInfo(string username, bool isAnonymous, DateTime lastActivityDate, DateTime lastUpdatedDate, int size)
	{
		if (username != null)
		{
			username = username.Trim();
		}
		_UserName = username;
		if (lastActivityDate.Kind == DateTimeKind.Local)
		{
			lastActivityDate = lastActivityDate.ToUniversalTime();
		}
		_LastActivityDate = lastActivityDate;
		if (lastUpdatedDate.Kind == DateTimeKind.Local)
		{
			lastUpdatedDate = lastUpdatedDate.ToUniversalTime();
		}
		_LastUpdatedDate = lastUpdatedDate;
		_IsAnonymous = isAnonymous;
		_Size = size;
	}

	/// <summary>Creates an instance of the <see cref="T:System.Web.Profile.ProfileInfo" /> object for a class that inherits the <see cref="T:System.Web.Profile.ProfileInfo" /> class.</summary>
	protected ProfileInfo()
	{
	}
}
