using System.Collections.Specialized;

namespace System.Web.Security;

/// <summary>Manages storage of membership information for an ASP.NET application in Active Directory and Active Directory Application Mode servers.</summary>
[MonoTODO("that's only a stub")]
public class ActiveDirectoryMembershipProvider : MembershipProvider
{
	/// <summary>The name of the application using the custom membership provider.</summary>
	/// <returns>The name of the application using the custom membership provider.</returns>
	/// <exception cref="T:System.NotSupportedException">An attempt was made to set the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.ApplicationName" /> property.</exception>
	/// <exception cref="T:System.InvalidOperationException">An attempt to access the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.ApplicationName" /> property was made before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance was initialized.</exception>
	[MonoTODO("Not implemented")]
	public override string ApplicationName
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the current level of security being used to protect communications with the server.</summary>
	/// <returns>One of the <see cref="T:System.Web.Security.ActiveDirectoryConnectionProtection" /> values. </returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt to access the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.CurrentConnectionProtection" /> property was made before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance was initialized.</exception>
	[MonoTODO("Not implemented")]
	public ActiveDirectoryConnectionProtection CurrentConnectionProtection
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is configured to allow users to reset their passwords.</summary>
	/// <returns>
	///     <see langword="true" /> if password reset is allowed; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt to access the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.EnablePasswordReset" /> property was made before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance was initialized.</exception>
	[MonoTODO("Not implemented")]
	public override bool EnablePasswordReset
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the user's password can be retrieved from the Active Directory data store. This property always returns <see langword="false" />.</summary>
	/// <returns>
	///     <see langword="false" /> to indicate that password retrieval is not supported by the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> class.</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt to access the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.EnablePasswordRetrieval" /> property was made before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance was initialized.</exception>
	[MonoTODO("Not implemented")]
	public override bool EnablePasswordRetrieval
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether search-oriented <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> methods are available.</summary>
	/// <returns>
	///     <see langword="true" /> if search methods are available; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt to access the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.EnableSearchMethods" /> property was made before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance was initialized.</exception>
	[MonoTODO("Not implemented")]
	public bool EnableSearchMethods
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating the format of passwords in the Active Directory data store.</summary>
	/// <returns>One of the <see cref="T:System.Web.Security.MembershipPasswordFormat" /> values. The <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.PasswordFormat" /> property always returns <see cref="F:System.Web.Security.MembershipPasswordFormat.Hashed" />.</returns>
	[MonoTODO("Not implemented")]
	public override MembershipPasswordFormat PasswordFormat
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the membership provider is configured to require a password question and answer when creating a user.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> object is configured to require a password question and answer for a user; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt to access the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.RequiresQuestionAndAnswer" /> property was made before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance was initialized.</exception>
	[MonoTODO("Not implemented")]
	public override bool RequiresQuestionAndAnswer
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether an e-mail address stored on the Active Directory server must be unique.</summary>
	/// <returns>
	///     <see langword="true" /> if e-mail addresses must be unique; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt to access the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.RequiresUniqueEmail" /> property was made before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance was initialized.</exception>
	[MonoTODO("Not implemented")]
	public override bool RequiresUniqueEmail
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the number of failed answer attempts a user is allowed for the password-reset question.</summary>
	/// <returns>The number of failed password answer attempts a user is allowed before the account is locked. The default is 5. </returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt to access the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.MaxInvalidPasswordAttempts" /> property was made before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance was initialized.</exception>
	[MonoTODO("Not implemented")]
	public override int MaxInvalidPasswordAttempts
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the minimum number of special characters that must be present in a valid password.</summary>
	/// <returns>The minimum number of special characters that must be present in a valid password.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.MinRequiredNonAlphanumericCharacters" /> property is accessed before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is initialized.</exception>
	[MonoTODO("Not implemented")]
	public override int MinRequiredNonAlphanumericCharacters
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the minimum length required for a password.</summary>
	/// <returns>The minimum length required for a password. </returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.MinRequiredPasswordLength" /> property is accessed before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is initialized.</exception>
	[MonoTODO("Not implemented")]
	public override int MinRequiredPasswordLength
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the time window during which consecutive failed attempts to provide a valid password or a valid password answer are tracked.</summary>
	/// <returns>The time window, in minutes, during which consecutive failed attempts to provide a valid password or a valid password answer are tracked. The default is 10 minutes. If the interval between each failed attempt is greater than the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.PasswordAttemptWindow" /> property setting, the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance treats each failed attempt as if it were the first failed attempt.</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt to access the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.PasswordAttemptWindow" /> property was made before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance was initialized.</exception>
	[MonoTODO("Not implemented")]
	public override int PasswordAttemptWindow
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Get the length of time for which a user account is locked out after the user makes too many bad password-answer attempts.</summary>
	/// <returns>The time, in minutes, that a user is locked out after providing too many incorrect password answers.</returns>
	/// <exception cref="T:System.InvalidOperationException">An attempt to access the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.PasswordAnswerAttemptLockoutDuration" /> property was made before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance was initialized.</exception>
	[MonoTODO("Not implemented")]
	public int PasswordAnswerAttemptLockoutDuration
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the regular expression used to evaluate a password.</summary>
	/// <returns>A regular expression used to evaluate a password.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.PasswordStrengthRegularExpression" /> property is accessed before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is initialized.</exception>
	[MonoTODO("Not implemented")]
	public override string PasswordStrengthRegularExpression
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Changes the password for the specified user.</summary>
	/// <param name="username">The name of the user to update the password for.</param>
	/// <param name="oldPassword">The current password for the specified user.</param>
	/// <param name="newPassword">The new password for the specified user.</param>
	/// <returns>
	///     <see langword="true" /> if the password was updated successfully; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> is empty, or exceeds the maximum length for the user name (usually 256 characters).- or -
	///         <paramref name="username" /> contains commas.- or -The user name is mapped to the <see langword="userPrincipalName" /> attribute and the <paramref name="username" /> parameter contains backslashes.- or -
	///         <paramref name="oldPassword" /> or <paramref name="newPassword" /> is a zero-length string.- or -
	///         <paramref name="oldPassword" /> or <paramref name="newPassword" /> exceeds the maximum password length (usually 128 characters).- or -
	///         <paramref name="newPassword" /> is less than the minimum password size specified in the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.MinRequiredPasswordLength" /> property - or -
	///         <paramref name="newPassword" /> contains fewer than the number of non-alphabetic characters specified in the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.MinRequiredNonAlphanumericCharacters" /> property.- or -
	///         <paramref name="newPassword" /> fails validation by the regular expression defined in the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.PasswordStrengthRegularExpression" /> property.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.- or -
	///         <paramref name="oldPassword" /> is <see langword="null" />.- or -
	///         <paramref name="newPassword" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Web.Security.MembershipPasswordException">
	///         <paramref name="newPassword" /> does not meet the complexity requirements defined by the Active Directory server.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">A secure connection could not be made to an Active Directory Application Mode server.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.ChangePassword(System.String,System.String,System.String)" /> method is called before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is initialized.</exception>
	/// <exception cref="T:System.Exception">An unhandled exception occurred.</exception>
	[MonoTODO("Not implemented")]
	public override bool ChangePassword(string username, string oldPassword, string newPassword)
	{
		throw new NotImplementedException();
	}

	/// <summary>Updates the password question and answer for a user in the Active Directory store.</summary>
	/// <param name="username">The user to change the password question and answer for.</param>
	/// <param name="password">The password for the specified user.</param>
	/// <param name="newPasswordQuestion">The new password question for the specified user.</param>
	/// <param name="newPasswordAnswer">The new password answer for the specified user.</param>
	/// <returns>
	///     <see langword="true" /> if the update was successful; otherwise, <see langword="false" />. A value of <see langword="false" /> is also returned if the password is incorrect, the user is locked out, or the user does not exist in the Active Directory data store.</returns>
	/// <exception cref="T:System.NotSupportedException">The administrator has not mapped the password question-and-answer fields to attributes of the Active Directory schema.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> is empty, or exceeds the maximum length for the user name (usually 256 characters).- or -
	///         <paramref name="username" /> contains commas.- or -The user name is mapped to the <see langword="userPrincipalName" /> attribute and the <paramref name="username" /> parameter contains backslashes.- or -
	///         <paramref name="password" /> is a zero-length string.- or -
	///         <paramref name="password" /> exceeds the maximum password length (usually 128 characters).- or -
	///         <paramref name="newPasswordQuestion" /> is empty and the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.RequiresQuestionAndAnswer" /> property value is <see langword="true" />.- or -
	///         <paramref name="newPasswordQuestion" /> exceeds 256 characters.- or -
	///         <paramref name="newPasswordAnswer" /> exceeds 128 characters after the answer is encrypted.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.- or -
	///         <paramref name="password" /> is <see langword="null" />.- or -
	///         <paramref name="newPasswordQuestion" /> is <see langword="null" /> and <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.RequiresQuestionAndAnswer" /> property is <see langword="true" />.- or -
	///         <paramref name="newPasswordAnswer" /> is <see langword="null" /> and <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.RequiresQuestionAndAnswer" /> property is <see langword="true" />.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">The machineKey Element (ASP.NET Settings Schema) configuration element indicates an auto-generated machine encryption key. You must explicitly set the <see langword="decriptionKey" /> attribute of the machineKey Element (ASP.NET Settings Schema) element to store password answers with the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.ChangePasswordQuestionAndAnswer(System.String,System.String,System.String,System.String)" /> method is called before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is initialized.</exception>
	[MonoTODO("Not implemented")]
	public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
	{
		throw new NotImplementedException();
	}

	/// <summary>Adds a new user to the Active Directory data store.</summary>
	/// <param name="username">The user name for the new user.</param>
	/// <param name="password">The password for the new user.</param>
	/// <param name="email">The e-mail address of the new user.</param>
	/// <param name="passwordQuestion">The password question for the new user.</param>
	/// <param name="passwordAnswer">The password answer for the new user.</param>
	/// <param name="isApproved">Whether or not the new user is approved to be validated.</param>
	/// <param name="providerUserKey">The unique identifier from the membership data source for the user. This parameter must be <see langword="null" /> when using the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> class.</param>
	/// <param name="status">When this method returns, contains one of the <see cref="T:System.Web.Security.MembershipCreateStatus" /> enumeration values indicating whether the user was created successfully.</param>
	/// <returns>An <see cref="T:System.Web.Security.ActiveDirectoryMembershipUser" /> instance containing the information for the newly created user, or <see langword="null" /> if the user was not successfully created.</returns>
	/// <exception cref="T:System.ArgumentException">The <paramref name="providerUserKey" /> parameter is not <see langword="null" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The administrator has not mapped the password question-and-answer fields to attributes of the Active Directory schema, and either the <paramref name="passwordQuestion" /> or <paramref name="passwordAnswer" /> parameter is not <see langword="null" />.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">The machineKey Element (ASP.NET Settings Schema) configuration element indicates an auto-generated machine encryption key. You must explicitly set the <see langword="decriptionKey" /> attribute of the machineKey Element (ASP.NET Settings Schema) element to store password answers with the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" />.- or -The <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> was unable to establish a secure connection to the directory when attempting to set the password for the new user.</exception>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred while attempting to create the user.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.CreateUser(System.String,System.String,System.String,System.String,System.String,System.Boolean,System.Object,System.Web.Security.MembershipCreateStatus@)" /> method is called before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is initialized.</exception>
	[MonoTODO("Not implemented")]
	public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
	{
		throw new NotImplementedException();
	}

	/// <summary>Removes a user's membership information from the Active Directory data store.</summary>
	/// <param name="username">The name of the user to delete.</param>
	/// <param name="deleteAllRelatedData">This parameter is ignored by the <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.DeleteUser(System.String,System.Boolean)" /> method. </param>
	/// <returns>
	///     <see langword="true" /> if the user was deleted; otherwise, <see langword="false" /> if the user was not found in the data store.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> is empty, or exceeds the maximum length for the user name (usually 256 characters).- or -
	///         <paramref name="username" /> contains commas.- or -The user name is mapped to the <see langword="userPrincipalName" /> attribute and the <paramref name="userName" /> parameter contains backslashes.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred while attempting to delete the user.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.DeleteUser(System.String,System.Boolean)" /> method is called before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is initialized.</exception>
	[MonoTODO("Not implemented")]
	public override bool DeleteUser(string username, bool deleteAllRelatedData)
	{
		throw new NotImplementedException();
	}

	/// <summary>Generates a random password.</summary>
	/// <returns>A random password.</returns>
	[MonoTODO("Not implemented")]
	public virtual string GeneratePassword()
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a collection of membership users from the Active Directory data store based on the user's e-mail address.</summary>
	/// <param name="emailToMatch">E-mail address or portion of e-mail address to search for.</param>
	/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="pageSize">The size of the page of results to return.</param>
	/// <param name="totalRecords">When this method returns, contains the total number of users returned in the collection. This parameter is passed uninitialized.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> containing <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> instances beginning at the page specified by <paramref name="pageIndex" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.FindUsersByEmail(System.String,System.Int32,System.Int32,System.Int32@)" /> method is called before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is initialized.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="emailToMatch" /> is empty.- or -
	///         <paramref name="emailToMatch" /> exceeds 256 characters.- or -
	///         <paramref name="pageIndex" /> is less than 0.- or -
	///         <paramref name="pageSize" /> is less than 1.- or -
	///         <paramref name="pageSize" /> multiplied by <paramref name="pageIndex" />, plus <paramref name="pageSize" />, minus 1 is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.EnableSearchMethods" /> property is <see langword="false" />.</exception>
	[MonoTODO("Not implemented")]
	public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a collection of users from the Active Directory data store based on the user name.</summary>
	/// <param name="usernameToMatch">The user name or portion of a user name to search for.</param>
	/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="pageSize">The size of the page of results to return.</param>
	/// <param name="totalRecords">When this method returns, contains the total number of records returned in the collection. This parameter is passed uninitialized.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> containing <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> instances beginning at the page specified by <paramref name="pageIndex" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.FindUsersByName(System.String,System.Int32,System.Int32,System.Int32@)" /> method is called before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is initialized.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="usernameToMatch" /> is empty, or exceeds the maximum length for the user name (usually 256 characters).- or -
	///         <paramref name="usernameToMatch" /> contains commas.- or -
	///         <paramref name="pageIndex" /> is less than 0.-or
	///         <paramref name="pageSize" /> is less than 1.- or -
	///         <paramref name="pageSize" /> multiplied by <paramref name="pageIndex" />, plus <paramref name="pageSize" />, minus 1 is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="usernameToMatch" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.EnableSearchMethods" /> property is <see langword="false" />.</exception>
	[MonoTODO("Not implemented")]
	public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a collection of all the users stored in an Active Directory data source.</summary>
	/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="pageSize">The size of the page of results to return.</param>
	/// <param name="totalRecords">When this method returns, contains the total number of records returned in the collection. This parameter is passed uninitialized.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> containing <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> instances beginning at the page specified by <paramref name="pageIndex" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.GetAllUsers(System.Int32,System.Int32,System.Int32@)" /> method is called before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is initialized.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="pageIndex" /> is less than 0.-or
	///         <paramref name="pageSize" /> is less than 1.- or -
	///         <paramref name="pageSize" /> multiplied by <paramref name="pageIndex" />, plus <paramref name="pageSize" />, minus 1 is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.EnableSearchMethods" /> property is <see langword="false" />.</exception>
	[MonoTODO("Not implemented")]
	public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
	{
		throw new NotImplementedException();
	}

	/// <summary>Throws a <see cref="T:System.NotSupportedException" /> exception in all cases.</summary>
	/// <returns>A <see cref="T:System.NotSupportedException" /> in all cases.</returns>
	/// <exception cref="T:System.NotSupportedException">Any call to the <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.GetNumberOfUsersOnline" /> method.</exception>
	[MonoTODO("Not implemented")]
	public override int GetNumberOfUsersOnline()
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the password of the specified user from the database. The <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> class does not support this method.</summary>
	/// <param name="username">The user to retrieve the password for.</param>
	/// <param name="passwordAnswer">The password answer for the user.</param>
	/// <returns>Always throws a <see cref="T:System.NotSupportedException" /> exception.</returns>
	/// <exception cref="T:System.NotSupportedException">The <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> does not support password retrieval.</exception>
	[MonoTODO("Not implemented")]
	public override string GetPassword(string username, string passwordAnswer)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the membership user information associated with the specified user name.</summary>
	/// <param name="username">The name of the user to get information for.</param>
	/// <param name="userIsOnline">The <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.GetUser(System.String,System.Boolean)" /> method ignores this parameter.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> instance representing the user specified. If no user is found in the Active Directory data store for the specified <paramref name="username" /> value, <see langword="null" /> is returned.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> is empty, or exceeds the maximum length for the user name (usually 256 characters).- or -
	///         <paramref name="username" /> contains commas.- or -The user name is mapped to the <see langword="userPrincipalName" /> attribute and the <paramref name="username" /> parameter contains backslashes.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.GetUser(System.String,System.Boolean)" /> method is called before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is initialized.</exception>
	[MonoTODO("Not implemented")]
	public override MembershipUser GetUser(string username, bool userIsOnline)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the membership user information associated with the specified user key.</summary>
	/// <param name="providerUserKey">The unique identifier for the user.</param>
	/// <param name="userIsOnline">The <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.GetUser(System.Object,System.Boolean)" /> method ignores this parameter.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> instance representing the user specified. If no user is found in the Active Directory data store for the specified <paramref name="providerUserKey" /> value, <see langword="null" /> is returned.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.GetUser(System.Object,System.Boolean)" /> method is called before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is initialized.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="providerUserKey" /> is not of type <see cref="T:System.Security.Principal.SecurityIdentifier" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="providerUserKey" /> is <see langword="null" />.</exception>
	[MonoTODO("Not implemented")]
	public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the user name associated with the specified e-mail address.</summary>
	/// <param name="email">The e-mail address to search for.</param>
	/// <returns>The user name associated with the specified e-mail address.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="email" /> exceeds 256 characters- or -after trimming, <paramref name="email" /> is empty.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">More than one user exists in the data store with the same e-mail address and the <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.RequiresUniqueEmail" /> property value is <see langword="true" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The provider is not initialized.</exception>
	[MonoTODO("Not implemented")]
	public override string GetUserNameByEmail(string email)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance with the property values from the application's configuration files. This method is not intended to be called from your code.</summary>
	/// <param name="name">The name of the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance to initialize.</param>
	/// <param name="config">A <see cref="T:System.Collections.Specialized.NameValueCollection" /> containing the names and values of the configuration options for the membership provider.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="config" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see langword="applicationName" /> attribute specified in the application configuration exceeds 256 characters.- or -The <see langword="connectionStringName" /> attribute is empty or does not exist in the application configuration.- or -The connection string specified in the <see langword="connectionStringName" /> attribute in the application configuration file is empty or does not exist.- or -The <see langword="connectionProtection" /> attribute is set to a value other than <see cref="F:System.Web.Security.ActiveDirectoryConnectionProtection.SignAndSeal" /> or <see cref="F:System.Web.Security.ActiveDirectoryConnectionProtection.None" /> in the application configuration file.- or -The <see langword="connectionUsername" /> attribute is specified in the application configuration file but its value is empty.- or -The <see langword="connectionPassword" /> attribute is specified in the application configuration file but its value is empty.- or -Either the <see langword="connectionUsername" /> attribute or the <see langword="connectionPassword" /> attribute is specified in the application configuration file, but not both.- or -The <see langword="clientSearchTimeout" /> attribute is specified but is not a positive integer.- or -The <see langword="serverSearchTimeout" /> attribute is specified but is not a positive integer.- or -The <see langword="enableSearchMethods" /> attribute is specified, but is not a Boolean value.- or -The <see langword="requiresUniqueEmail" /> attribute is specified, but is not a Boolean value.- or -The <see langword="enablePasswordReset" /> attribute is specified, but is not a Boolean value.- or -The <see langword="requiresQuestionAndAnswer" /> attribute is specified, but is not a Boolean value.- or -The <see langword="minRequiredPasswordLength" /> attribute is specified and it is either negative or greater than 128.- or -The <see langword="minRequiredNonalphanumericCharacters" /> attribute is specified and it is either negative or greater than 128.- or -The regular expression specified in the <see langword="passwordStrengthRegularExpression" /> attribute in the application configuration file is not a valid regular expression- or -The <see langword="attributeMapUsername" /> attribute is specified, but it is an empty string.- or -The <see langword="connectionString" /> attribute does not begin with "LDAP".- or -The connection string specified in the <see langword="connectionString" /> attribute is invalid.- or -The connection string in the <see langword="connectionString" /> attribute specifies a server-less bind.- or -The <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> could not establish a connection with the domain or directory server.- or -The <see langword="connectionProtection" /> attribute is set to <see cref="F:System.Web.Security.ActiveDirectoryConnectionProtection.SignAndSeal" />, but neither an SSL nor a signed and sealed connection can be established with the server.- or -The <see langword="connectionProtection" /> attribute is set to <see cref="F:System.Web.Security.ActiveDirectoryConnectionProtection.None" /> but the <see langword="connectionUsername" /> and <see langword="connectionPassword" /> attributes are not set.- or -The <see langword="connectionProtection" /> attribute is set to <see cref="F:System.Web.Security.ActiveDirectoryConnectionProtection.SignAndSeal" /> but the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> could not establish an SSL connection with an ADAM server.- or -The <see langword="connectionString" /> attribute specifies either the global catalog (GC) or an SSL global catalog port.- or -The <see langword="connectionString" /> attribute specifies an Active Directory server or domain, but the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> could not retrieve the default naming context for the domain.- or -The <see langword="connectionString" /> attribute specifies an Active Directory server or domain, but the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> could not retrieve the default users container.- or -The <see langword="connectionString" /> attribute specifies an ADAM server, but it does not specify an application partition or container.- or -The <see langword="connectionString" /> attribute specifies an application partition or container, but the specified container does not exist.- or -The <see langword="connectionString" /> attribute specifies a container that is not allowed to contain user instances.An attribute mapping is specified but its value was empty.- or -An attribute mapping is specified twice.- or -An attribute mapping is specified but the attribute does not exist on the user instance.- or -An attribute mapping is specified but the directory attribute is not of the correct data type.- or -An attribute mapping is specified but the directory attribute is multi-valued.- or -The <see langword="attributeMapUserName" /> attribute is specified but was mapped to neither the sAMAccountName nor the userPrincipalName directory attribute when connecting to an Active Directory.- or -The <see langword="attributeMapUserName" /> attribute is specified but was not mapped to the userPrincipalName directory attribute when connecting to an ADAM server.- or -The <see langword="enablePasswordReset" /> attribute is <see langword="true" /> and the <see langword="requiresQuestionAndAnswer" /> attribute is <see langword="false" /> in the application configuration file.- or -The <see langword="maxInvalidPasswordAttempts" /> attribute was specified but is not a non-zero positive integer.- or -The <see langword="passwordAttemptWindow" /> attribute was specified but is not a non-zero positive integer.- or -The <see langword="passwordAnswerAttemptLockoutDuration" /> attribute was specified but is not a non-zero positive integer.- or -The <see langword="enablePasswordReset" /> attribute is <see langword="true" /> and any of the <see langword="attributeMapFailedPasswordAnswerCount" />, <see langword="attributeMapFailedPasswordAnswerTime" />, and <see langword="attributeMapFailedPasswordAnswerLockoutTime" /> attributes are empty.- or -The <see langword="requiresQuestionAndAnswer" /> attribute is <see langword="true" /> but either the <see langword="attributeMapPasswordQuestion" /> or the <see langword="attributeMapPasswordAnswer" /> attribute is empty.- or -An attribute specified in the application configuration file is not valid.</exception>
	/// <exception cref="T:System.Web.HttpException">The application is running in a hosted environment and the <see cref="T:System.Web.AspNetHostingPermissionLevel" /> is set to <see cref="F:System.Web.AspNetHostingPermissionLevel.Minimal" />.</exception>
	/// <exception cref="T:System.Runtime.InteropServices.COMException">An error occurred while querying the directory.</exception>
	[MonoTODO("Not implemented")]
	public override void Initialize(string name, NameValueCollection config)
	{
		throw new NotImplementedException();
	}

	/// <summary>Resets a user's password to a new, automatically generated password.</summary>
	/// <param name="username">The user to reset the password for.</param>
	/// <param name="passwordAnswer">The password answer for the specified user.</param>
	/// <returns>The new password for the specified user.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.ResetPassword(System.String,System.String)" /> method is called before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is initialized.</exception>
	/// <exception cref="T:System.NotSupportedException">The <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.EnablePasswordReset" /> property value is <see langword="false" />.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="passwordAnswer" /> is <see langword="null" />.- or -
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="passwordAnswer" /> is empty after trimming- or -
	///         <paramref name="passwordAnswer" /> exceeds 128 characters.- or -
	///         <paramref name="username" /> is empty, or exceeds the maximum length allowed for user names (usually 256 characters).- or -
	///         <paramref name="username" /> contains commas.- or -The user name is mapped to <see langword="userPrincipalName" /> but the <paramref name="username" /> parameter contains backslashes.</exception>
	/// <exception cref="T:System.Web.Security.MembershipPasswordException">The user is locked out because of too many bad logon attempts or too many attempted password-answer reset attempts.- or -
	///         <paramref name="passwordAnswer" /> does not match the stored password answer.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">The user specified in <paramref name="username" /> does not exist in the Active Directory data store.- or -A generated password does not pass a custom validation handler.- or -The generated password is not complex enough to satisfy custom password policies set on the Active Directory server.- or -A secure connection cannot be made to an Active Directory Application Mode server to set the new password.</exception>
	/// <exception cref="T:System.Exception">An unhandled exception occurred.</exception>
	[MonoTODO("Not implemented")]
	public override string ResetPassword(string username, string passwordAnswer)
	{
		throw new NotImplementedException();
	}

	/// <summary>Updates information about a user in the Active Directory data store.</summary>
	/// <param name="user">A <see cref="T:System.Web.Security.MembershipUser" /> instance representing the user to update and the updated information for the user.</param>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.UpdateUser(System.Web.Security.MembershipUser)" /> method is called before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is initialized.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="user" /> is <see langword="null" />.- or -The <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.RequiresUniqueEmail" /> property is <see langword="true" /> but the email address from the supplied <see cref="T:System.Web.Security.MembershipUser" /> instance is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Web.Security.MembershipUser.Email" /> property is empty after trimming.- or -The <see cref="P:System.Web.Security.MembershipUser.Email" /> property exceeds 256 characters.- or -The <see cref="P:System.Web.Security.MembershipUser.Comment" /> property exceeds 1024 characters.- or -The <see cref="P:System.Web.Security.MembershipUser.Comment" /> property is empty.- or -The user name from the supplied <see cref="T:System.Web.Security.MembershipUser" /> instance is empty, or exceeds the maximum length allowed for user names (usually 256 characters).- or -The user name from the supplied <see cref="T:System.Web.Security.MembershipUser" /> instance contains commas.- or -The user name is mapped to <see langword="userPrincipalName" /> but the user name from the supplied <see cref="T:System.Web.Security.MembershipUser" /> instance contains backslashes.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">The specified user is not found in the Active Directory data store.- or -The <see cref="P:System.Web.Security.ActiveDirectoryMembershipProvider.RequiresUniqueEmail" /> property is <see langword="true" />, and the new value of the <see cref="P:System.Web.Security.MembershipUser.Email" /> property duplicates an existing e-mail address.</exception>
	[MonoTODO("Not implemented")]
	public override void UpdateUser(MembershipUser user)
	{
		throw new NotImplementedException();
	}

	/// <summary>Verifies that the specified user name and password exist in the Active Directory data store.</summary>
	/// <param name="username">The name of the user to validate.</param>
	/// <param name="password">The password for the specified user.</param>
	/// <returns>
	///     <see langword="true" /> if the specified <paramref name="username" /> and <paramref name="password" /> are valid; otherwise, <see langword="false" />. If the user specified does not exist in the Active Directory data store, the <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.ValidateUser(System.String,System.String)" /> method returns <see langword="false" />.</returns>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.ValidateUser(System.String,System.String)" /> method is called before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is initialized.</exception>
	[MonoTODO("Not implemented")]
	public override bool ValidateUser(string username, string password)
	{
		throw new NotImplementedException();
	}

	/// <summary>Clears a lock so that a membership user can be validated.</summary>
	/// <param name="username">The name of the membership user to clear the lock status for.</param>
	/// <returns>
	///     <see langword="true" /> if the membership user was successfully unlocked; otherwise, <see langword="false" />. The <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.UnlockUser(System.String)" /> method also returns <see langword="false" /> when the membership user is not found in the data store.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> is empty, or exceeds the maximum length allowed for user names (usually 256 characters).- or -
	///         <paramref name="username" /> contains commas.- or -The user name is mapped to <see langword="userPrincipalName" /> but the <paramref name="username" /> parameter contains backslashes.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.Security.ActiveDirectoryMembershipProvider.UnlockUser(System.String)" /> method is called before the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> instance is initialized.</exception>
	[MonoTODO("Not implemented")]
	public override bool UnlockUser(string username)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.Security.ActiveDirectoryMembershipProvider" /> class.</summary>
	public ActiveDirectoryMembershipProvider()
	{
	}
}
