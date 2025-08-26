using System.Runtime.CompilerServices;

namespace System.Web.Security;

/// <summary>Exposes and updates membership user information in the membership data store.</summary>
[Serializable]
[TypeForwardedFrom("System.Web, Version=2.0.0.0, Culture=Neutral, PublicKeyToken=b03f5f7f11d50a3a")]
public class MembershipUser
{
	private string providerName;

	private string name;

	private object providerUserKey;

	private string email;

	private string passwordQuestion;

	private string comment;

	private bool isApproved;

	private bool isLockedOut;

	private DateTime creationDate;

	private DateTime lastLoginDate;

	private DateTime lastActivityDate;

	private DateTime lastPasswordChangedDate;

	private DateTime lastLockoutDate;

	/// <summary>Gets or sets application-specific information for the membership user.</summary>
	/// <returns>Application-specific information for the membership user.</returns>
	public virtual string Comment
	{
		get
		{
			return comment;
		}
		set
		{
			comment = value;
		}
	}

	/// <summary>Gets the date and time when the user was added to the membership data store.</summary>
	/// <returns>The date and time when the user was added to the membership data store.</returns>
	public virtual DateTime CreationDate => creationDate.ToLocalTime();

	/// <summary>Gets or sets the email address for the membership user.</summary>
	/// <returns>The email address for the membership user.</returns>
	public virtual string Email
	{
		get
		{
			return email;
		}
		set
		{
			email = value;
		}
	}

	/// <summary>Gets or sets whether the membership user can be authenticated.</summary>
	/// <returns>
	///   <see langword="true" /> if the user can be authenticated; otherwise, <see langword="false" />.</returns>
	public virtual bool IsApproved
	{
		get
		{
			return isApproved;
		}
		set
		{
			isApproved = value;
		}
	}

	/// <summary>Gets a value indicating whether the membership user is locked out and unable to be validated.</summary>
	/// <returns>
	///   <see langword="true" /> if the membership user is locked out and unable to be validated; otherwise, <see langword="false" />.</returns>
	public virtual bool IsLockedOut => isLockedOut;

	/// <summary>Gets whether the user is currently online.</summary>
	/// <returns>
	///   <see langword="true" /> if the user is online; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
	public virtual bool IsOnline
	{
		get
		{
			int userIsOnlineTimeWindow = (MembershipProvider.Helper ?? throw new PlatformNotSupportedException("The method is not available.")).UserIsOnlineTimeWindow;
			return LastActivityDate > DateTime.Now - TimeSpan.FromMinutes(userIsOnlineTimeWindow);
		}
	}

	/// <summary>Gets or sets the date and time when the membership user was last authenticated or accessed the application.</summary>
	/// <returns>The date and time when the membership user was last authenticated or accessed the application.</returns>
	public virtual DateTime LastActivityDate
	{
		get
		{
			return lastActivityDate.ToLocalTime();
		}
		set
		{
			lastActivityDate = value.ToUniversalTime();
		}
	}

	/// <summary>Gets or sets the date and time when the user was last authenticated.</summary>
	/// <returns>The date and time when the user was last authenticated.</returns>
	public virtual DateTime LastLoginDate
	{
		get
		{
			return lastLoginDate.ToLocalTime();
		}
		set
		{
			lastLoginDate = value.ToUniversalTime();
		}
	}

	/// <summary>Gets the date and time when the membership user's password was last updated.</summary>
	/// <returns>The date and time when the membership user's password was last updated.</returns>
	public virtual DateTime LastPasswordChangedDate => lastPasswordChangedDate.ToLocalTime();

	/// <summary>Gets the most recent date and time that the membership user was locked out.</summary>
	/// <returns>A <see cref="T:System.DateTime" /> object that represents the most recent date and time that the membership user was locked out.</returns>
	public virtual DateTime LastLockoutDate => lastLockoutDate.ToLocalTime();

	/// <summary>Gets the password question for the membership user.</summary>
	/// <returns>The password question for the membership user.</returns>
	public virtual string PasswordQuestion => passwordQuestion;

	/// <summary>Gets the name of the membership provider that stores and retrieves user information for the membership user.</summary>
	/// <returns>The name of the membership provider that stores and retrieves user information for the membership user.</returns>
	public virtual string ProviderName => providerName;

	/// <summary>Gets the logon name of the membership user.</summary>
	/// <returns>The logon name of the membership user.</returns>
	public virtual string UserName => name;

	/// <summary>Gets the user identifier from the membership data source for the user.</summary>
	/// <returns>The user identifier from the membership data source for the user.</returns>
	public virtual object ProviderUserKey => providerUserKey;

	private MembershipProvider Provider => (MembershipProvider.Helper ?? throw new PlatformNotSupportedException("The method is not available.")).Providers[ProviderName] ?? throw new InvalidOperationException("Membership provider '" + ProviderName + "' not found.");

	/// <summary>Creates a new instance of a <see cref="T:System.Web.Security.MembershipUser" /> object for a class that inherits the <see cref="T:System.Web.Security.MembershipUser" /> class.</summary>
	protected MembershipUser()
	{
	}

	/// <summary>Creates a new membership user object with the specified property values.</summary>
	/// <param name="providerName">The <see cref="P:System.Web.Security.MembershipUser.ProviderName" /> string for the membership user.</param>
	/// <param name="name">The <see cref="P:System.Web.Security.MembershipUser.UserName" /> string for the membership user.</param>
	/// <param name="providerUserKey">The <see cref="P:System.Web.Security.MembershipUser.ProviderUserKey" /> identifier for the membership user.</param>
	/// <param name="email">The <see cref="P:System.Web.Security.MembershipUser.Email" /> string for the membership user.</param>
	/// <param name="passwordQuestion">The <see cref="P:System.Web.Security.MembershipUser.PasswordQuestion" /> string for the membership user.</param>
	/// <param name="comment">The <see cref="P:System.Web.Security.MembershipUser.Comment" /> string for the membership user.</param>
	/// <param name="isApproved">The <see cref="P:System.Web.Security.MembershipUser.IsApproved" /> value for the membership user.</param>
	/// <param name="isLockedOut">
	///   <see langword="true" /> to lock out the membership user; otherwise, <see langword="false" />.</param>
	/// <param name="creationDate">The <see cref="P:System.Web.Security.MembershipUser.CreationDate" /><see cref="T:System.DateTime" /> object for the membership user.</param>
	/// <param name="lastLoginDate">The <see cref="P:System.Web.Security.MembershipUser.LastLoginDate" /><see cref="T:System.DateTime" /> object for the membership user.</param>
	/// <param name="lastActivityDate">The <see cref="P:System.Web.Security.MembershipUser.LastActivityDate" /><see cref="T:System.DateTime" /> object for the membership user.</param>
	/// <param name="lastPasswordChangedDate">The <see cref="P:System.Web.Security.MembershipUser.LastPasswordChangedDate" /><see cref="T:System.DateTime" /> object for the membership user.</param>
	/// <param name="lastLockoutDate">The <see cref="P:System.Web.Security.MembershipUser.LastLockoutDate" /><see cref="T:System.DateTime" /> object for the membership user.</param>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="providerName" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="providerName" /> is not found in the <see cref="P:System.Web.Security.Membership.Providers" /> collection.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">The constructor is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, derive your class from the type and then call the default protected constructor, or change the application to target the full version of the .NET Framework.</exception>
	public MembershipUser(string providerName, string name, object providerUserKey, string email, string passwordQuestion, string comment, bool isApproved, bool isLockedOut, DateTime creationDate, DateTime lastLoginDate, DateTime lastActivityDate, DateTime lastPasswordChangedDate, DateTime lastLockoutDate)
	{
		this.providerName = providerName;
		this.name = name;
		this.providerUserKey = providerUserKey;
		this.email = email;
		this.passwordQuestion = passwordQuestion;
		this.comment = comment;
		this.isApproved = isApproved;
		this.isLockedOut = isLockedOut;
		this.creationDate = creationDate.ToUniversalTime();
		this.lastLoginDate = lastLoginDate.ToUniversalTime();
		this.lastActivityDate = lastActivityDate.ToUniversalTime();
		this.lastPasswordChangedDate = lastPasswordChangedDate.ToUniversalTime();
		this.lastLockoutDate = lastLockoutDate.ToUniversalTime();
	}

	private void UpdateSelf(MembershipUser fromUser)
	{
		try
		{
			Comment = fromUser.Comment;
		}
		catch (NotSupportedException)
		{
		}
		try
		{
			creationDate = fromUser.CreationDate;
		}
		catch (NotSupportedException)
		{
		}
		try
		{
			Email = fromUser.Email;
		}
		catch (NotSupportedException)
		{
		}
		try
		{
			IsApproved = fromUser.IsApproved;
		}
		catch (NotSupportedException)
		{
		}
		try
		{
			isLockedOut = fromUser.IsLockedOut;
		}
		catch (NotSupportedException)
		{
		}
		try
		{
			LastActivityDate = fromUser.LastActivityDate;
		}
		catch (NotSupportedException)
		{
		}
		try
		{
			lastLockoutDate = fromUser.LastLockoutDate;
		}
		catch (NotSupportedException)
		{
		}
		try
		{
			LastLoginDate = fromUser.LastLoginDate;
		}
		catch (NotSupportedException)
		{
		}
		try
		{
			lastPasswordChangedDate = fromUser.LastPasswordChangedDate;
		}
		catch (NotSupportedException)
		{
		}
		try
		{
			passwordQuestion = fromUser.PasswordQuestion;
		}
		catch (NotSupportedException)
		{
		}
		try
		{
			providerUserKey = fromUser.ProviderUserKey;
		}
		catch (NotSupportedException)
		{
		}
	}

	internal void UpdateUser()
	{
		MembershipUser user = Provider.GetUser(UserName, userIsOnline: false);
		UpdateSelf(user);
	}

	/// <summary>Updates the password for the membership user in the membership data store.</summary>
	/// <param name="oldPassword">The current password for the membership user.</param>
	/// <param name="newPassword">The new password for the membership user.</param>
	/// <returns>
	///   <see langword="true" /> if the update was successful; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="oldPassword" /> is an empty string.  
	/// -or-  
	/// <paramref name="newPassword" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="oldPassword" /> is <see langword="null" />.  
	/// -or-  
	/// <paramref name="newPassword" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
	public virtual bool ChangePassword(string oldPassword, string newPassword)
	{
		bool result = Provider.ChangePassword(UserName, oldPassword, newPassword);
		UpdateUser();
		return result;
	}

	/// <summary>Updates the password question and answer for the membership user in the membership data store.</summary>
	/// <param name="password">The current password for the membership user.</param>
	/// <param name="newPasswordQuestion">The new password question value for the membership user.</param>
	/// <param name="newPasswordAnswer">The new password answer value for the membership user.</param>
	/// <returns>
	///   <see langword="true" /> if the update was successful; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///   <paramref name="password" /> is an empty string.  
	/// -or-  
	/// <paramref name="newPasswordQuestion" /> is an empty string.  
	/// -or-  
	/// <paramref name="newPasswordAnswer" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///   <paramref name="password" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
	public virtual bool ChangePasswordQuestionAndAnswer(string password, string newPasswordQuestion, string newPasswordAnswer)
	{
		bool result = Provider.ChangePasswordQuestionAndAnswer(UserName, password, newPasswordQuestion, newPasswordAnswer);
		UpdateUser();
		return result;
	}

	/// <summary>Gets the password for the membership user from the membership data store.</summary>
	/// <returns>The password for the membership user.</returns>
	/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
	public virtual string GetPassword()
	{
		return GetPassword(null);
	}

	/// <summary>Gets the password for the membership user from the membership data store.</summary>
	/// <param name="passwordAnswer">The password answer for the membership user.</param>
	/// <returns>The password for the membership user.</returns>
	/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
	public virtual string GetPassword(string passwordAnswer)
	{
		return Provider.GetPassword(UserName, passwordAnswer);
	}

	/// <summary>Resets a user's password to a new, automatically generated password.</summary>
	/// <returns>The new password for the membership user.</returns>
	/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
	public virtual string ResetPassword()
	{
		return ResetPassword(null);
	}

	/// <summary>Resets a user's password to a new, automatically generated password.</summary>
	/// <param name="passwordAnswer">The password answer for the membership user.</param>
	/// <returns>The new password for the membership user.</returns>
	/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
	public virtual string ResetPassword(string passwordAnswer)
	{
		string result = Provider.ResetPassword(UserName, passwordAnswer);
		UpdateUser();
		return result;
	}

	/// <summary>Returns the user name for the membership user.</summary>
	/// <returns>The <see cref="P:System.Web.Security.MembershipUser.UserName" /> for the membership user.</returns>
	public override string ToString()
	{
		return UserName;
	}

	/// <summary>Clears the locked-out state of the user so that the membership user can be validated.</summary>
	/// <returns>
	///   <see langword="true" /> if the membership user was successfully unlocked; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.PlatformNotSupportedException">This method is not available. This can occur if the application targets the .NET Framework 4 Client Profile. To prevent this exception, override the method, or change the application to target the full version of the .NET Framework.</exception>
	public virtual bool UnlockUser()
	{
		bool result = Provider.UnlockUser(UserName);
		UpdateUser();
		return result;
	}
}
