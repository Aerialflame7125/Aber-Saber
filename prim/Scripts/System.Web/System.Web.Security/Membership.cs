using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;

namespace System.Web.Security;

/// <summary>Validates user credentials and manages user settings. This class cannot be inherited.</summary>
public static class Membership
{
	private static MembershipProviderCollection providers;

	private static MembershipProvider provider;

	private static int onlineTimeWindow;

	private static string hashAlgorithmType;

	/// <summary>Gets or sets the name of the application.</summary>
	/// <returns>The name of the application.</returns>
	public static string ApplicationName
	{
		get
		{
			return Provider.ApplicationName;
		}
		set
		{
			Provider.ApplicationName = value;
		}
	}

	/// <summary>Gets a value indicating whether the current membership provider is configured to allow users to reset their passwords.</summary>
	/// <returns>
	///     <see langword="true" /> if the membership provider supports password reset; otherwise, <see langword="false" />.</returns>
	public static bool EnablePasswordReset => Provider.EnablePasswordReset;

	/// <summary>Gets a value indicating whether the current membership provider is configured to allow users to retrieve their passwords.</summary>
	/// <returns>
	///     <see langword="true" /> if the membership provider supports password retrieval; otherwise, <see langword="false" />.</returns>
	public static bool EnablePasswordRetrieval => Provider.EnablePasswordRetrieval;

	/// <summary>The identifier of the algorithm used to hash passwords.</summary>
	/// <returns>The identifier of the algorithm used to hash passwords, or blank to use the default hash algorithm.</returns>
	public static string HashAlgorithmType => hashAlgorithmType;

	/// <summary>Gets a value indicating whether the default membership provider requires the user to answer a password question for password reset and retrieval.</summary>
	/// <returns>
	///     <see langword="true" /> if a password answer is required for password reset and retrieval; otherwise, <see langword="false" />.</returns>
	public static bool RequiresQuestionAndAnswer => Provider.RequiresQuestionAndAnswer;

	/// <summary>Gets the number of invalid password or password-answer attempts allowed before the membership user is locked out.</summary>
	/// <returns>The number of invalid password or password-answer attempts allowed before the membership user is locked out.</returns>
	public static int MaxInvalidPasswordAttempts => Provider.MaxInvalidPasswordAttempts;

	/// <summary>Gets the minimum number of special characters that must be present in a valid password.</summary>
	/// <returns>The minimum number of special characters that must be present in a valid password.</returns>
	public static int MinRequiredNonAlphanumericCharacters => Provider.MinRequiredNonAlphanumericCharacters;

	/// <summary>Gets the minimum length required for a password.</summary>
	/// <returns>The minimum length required for a password. </returns>
	public static int MinRequiredPasswordLength => Provider.MinRequiredPasswordLength;

	/// <summary>Gets the time window between which consecutive failed attempts to provide a valid password or password answer are tracked.</summary>
	/// <returns>The time window, in minutes, during which consecutive failed attempts to provide a valid password or password answer are tracked. The default is 10 minutes. If the interval between the current failed attempt and the last failed attempt is greater than the <see cref="P:System.Web.Security.Membership.PasswordAttemptWindow" /> property setting, each failed attempt is treated as if it were the first failed attempt.</returns>
	public static int PasswordAttemptWindow => Provider.PasswordAttemptWindow;

	/// <summary>Gets the regular expression used to evaluate a password.</summary>
	/// <returns>A regular expression used to evaluate a password.</returns>
	public static string PasswordStrengthRegularExpression => Provider.PasswordStrengthRegularExpression;

	/// <summary>Gets a reference to the default membership provider for the application.</summary>
	/// <returns>The default membership provider for the application exposed using the <see cref="T:System.Web.Security.MembershipProvider" /> abstract base class.</returns>
	public static MembershipProvider Provider => provider;

	/// <summary>Gets a collection of the membership providers for the ASP.NET application.</summary>
	/// <returns>A <see cref="T:System.Web.Security.MembershipProviderCollection" /> of the membership providers configured for the ASP.NET application.</returns>
	public static MembershipProviderCollection Providers => providers;

	/// <summary>Specifies the number of minutes after the last-activity date/time stamp for a user during which the user is considered online.</summary>
	/// <returns>The number of minutes after the last-activity date/time stamp for a user during which the user is considered online.</returns>
	public static int UserIsOnlineTimeWindow => onlineTimeWindow;

	/// <summary>Occurs when a user is created, a password is changed, or a password is reset.</summary>
	public static event MembershipValidatePasswordEventHandler ValidatingPassword
	{
		add
		{
			Provider.ValidatingPassword += value;
		}
		remove
		{
			Provider.ValidatingPassword -= value;
		}
	}

	static Membership()
	{
		MembershipSection membershipSection = (MembershipSection)WebConfigurationManager.GetSection("system.web/membership");
		providers = new MembershipProviderCollection();
		ProvidersHelper.InstantiateProviders(membershipSection.Providers, providers, typeof(MembershipProvider));
		provider = providers[membershipSection.DefaultProvider];
		onlineTimeWindow = (int)membershipSection.UserIsOnlineTimeWindow.TotalMinutes;
		hashAlgorithmType = membershipSection.HashAlgorithmType;
		if (string.IsNullOrEmpty(hashAlgorithmType))
		{
			MachineKeySection machineKeySection = WebConfigurationManager.GetSection("system.web/machineKey") as MachineKeySection;
			hashAlgorithmType = new MachineKeyValidationConverter().ConvertTo(null, null, machineKeySection.Validation, typeof(string)) as string;
		}
		if (string.IsNullOrEmpty(hashAlgorithmType))
		{
			hashAlgorithmType = "SHA1";
		}
	}

	/// <summary>Adds a new user to the data store.</summary>
	/// <param name="username">The user name for the new user. </param>
	/// <param name="password">The password for the new user. </param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object for the newly created user.</returns>
	/// <exception cref="T:System.Web.Security.MembershipCreateUserException">The user was not created. Check the <see cref="P:System.Web.Security.MembershipCreateUserException.StatusCode" /> property for a <see cref="T:System.Web.Security.MembershipCreateStatus" /> value. </exception>
	public static MembershipUser CreateUser(string username, string password)
	{
		return CreateUser(username, password, null);
	}

	/// <summary>Adds a new user with a specified e-mail address to the data store.</summary>
	/// <param name="username">The user name for the new user. </param>
	/// <param name="password">The password for the new user. </param>
	/// <param name="email">The e-mail address for the new user. </param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object for the newly created user.</returns>
	/// <exception cref="T:System.Web.Security.MembershipCreateUserException">The user was not created. Check the <see cref="P:System.Web.Security.MembershipCreateUserException.StatusCode" /> property for a <see cref="T:System.Web.Security.MembershipCreateStatus" /> value. </exception>
	public static MembershipUser CreateUser(string username, string password, string email)
	{
		MembershipCreateStatus status;
		return CreateUser(username, password, email, null, null, isApproved: true, out status) ?? throw new MembershipCreateUserException(status);
	}

	/// <summary>Adds a new user with specified property values to the data store and returns a status parameter indicating that the user was successfully created or the reason the user creation failed.</summary>
	/// <param name="username">The user name for the new user. </param>
	/// <param name="password">The password for the new user. </param>
	/// <param name="email">The e-mail address for the new user. </param>
	/// <param name="passwordQuestion">The password-question value for the membership user.</param>
	/// <param name="passwordAnswer">The password-answer value for the membership user.</param>
	/// <param name="isApproved">A Boolean that indicates whether the new user is approved to log on.</param>
	/// <param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus" /> indicating that the user was created successfully or the reason that creation failed. </param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object for the newly created user. If no user was created, this method returns <see langword="null" />.</returns>
	public static MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, out MembershipCreateStatus status)
	{
		return CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, null, out status);
	}

	/// <summary>Adds a new user with specified property values and a unique identifier to the data store and returns a status parameter indicating that the user was successfully created or the reason the user creation failed.</summary>
	/// <param name="username">The user name for the new user.</param>
	/// <param name="password">The password for the new user.</param>
	/// <param name="email">The e-mail address for the new user.</param>
	/// <param name="passwordQuestion">The password-question value for the membership user.</param>
	/// <param name="passwordAnswer">The password-answer value for the membership user.</param>
	/// <param name="isApproved">A Boolean that indicates whether the new user is approved to log on.</param>
	/// <param name="providerUserKey">The user identifier for the user that should be stored in the membership data store.</param>
	/// <param name="status">A <see cref="T:System.Web.Security.MembershipCreateStatus" /> indicating that the user was created successfully or the reason creation failed.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object for the newly created user. If no user was created, this method returns <see langword="null" />.</returns>
	public static MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
	{
		if (string.IsNullOrEmpty(username))
		{
			status = MembershipCreateStatus.InvalidUserName;
			return null;
		}
		if (string.IsNullOrEmpty(password))
		{
			status = MembershipCreateStatus.InvalidPassword;
			return null;
		}
		return Provider.CreateUser(username, password, email, passwordQuestion, passwordAnswer, isApproved, providerUserKey, out status);
	}

	/// <summary>Deletes a user and any related user data from the database.</summary>
	/// <param name="username">The name of the user to delete. </param>
	/// <returns>
	///     <see langword="true" /> if the user was deleted; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> is an empty string or contains a comma (,). </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null." /></exception>
	public static bool DeleteUser(string username)
	{
		return Provider.DeleteUser(username, deleteAllRelatedData: true);
	}

	/// <summary>Deletes a user from the database.</summary>
	/// <param name="username">The name of the user to delete.</param>
	/// <param name="deleteAllRelatedData">
	///       <see langword="true" /> to delete data related to the user from the database; <see langword="false" /> to leave data related to the user in the database.</param>
	/// <returns>
	///     <see langword="true" /> if the user was deleted; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> is an empty string or contains a comma (,). </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	public static bool DeleteUser(string username, bool deleteAllRelatedData)
	{
		return Provider.DeleteUser(username, deleteAllRelatedData);
	}

	/// <summary>Generates a random password of the specified length.</summary>
	/// <param name="length">The number of characters in the generated password. The length must be between 1 and 128 characters. </param>
	/// <param name="numberOfNonAlphanumericCharacters">The minimum number of non-alphanumeric characters (such as @, #, !, %, &amp;, and so on) in the generated password.</param>
	/// <returns>A random password of the specified length.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="length" /> is less than 1 or greater than 128 -or-
	///         <paramref name="numberOfNonAlphanumericCharacters" /> is less than 0 or greater than <paramref name="length" />. </exception>
	public static string GeneratePassword(int length, int numberOfNonAlphanumericCharacters)
	{
		RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
		byte[] array = new byte[length];
		int num = 0;
		randomNumberGenerator.GetBytes(array);
		for (int i = 0; i < length; i++)
		{
			array[i] = (byte)(array[i] % 93 + 33);
			if ((array[i] >= 33 && array[i] <= 47) || (array[i] >= 58 && array[i] <= 64) || (array[i] >= 91 && array[i] <= 96) || (array[i] >= 123 && array[i] <= 126))
			{
				num++;
			}
			if (array[i] == 34 || array[i] == 39)
			{
				array[i]++;
			}
			else if (array[i] == 96)
			{
				array[i]--;
			}
		}
		if (num < numberOfNonAlphanumericCharacters)
		{
			for (int i = 0; i < length; i++)
			{
				if (num == numberOfNonAlphanumericCharacters)
				{
					break;
				}
				if (array[i] >= 48 && array[i] <= 57)
				{
					array[i] = (byte)(array[i] - 48 + 33);
					num++;
				}
				else if (array[i] >= 65 && array[i] <= 90)
				{
					array[i] = (byte)((array[i] - 65) % 13 + 33);
					num++;
				}
				else if (array[i] >= 97 && array[i] <= 122)
				{
					array[i] = (byte)((array[i] - 97) % 13 + 33);
					num++;
				}
				if (array[i] == 34 || array[i] == 39)
				{
					array[i]++;
				}
				else if (array[i] == 96)
				{
					array[i]--;
				}
			}
		}
		return Encoding.ASCII.GetString(array);
	}

	/// <summary>Gets a collection of all the users in the database.</summary>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> of <see cref="T:System.Web.Security.MembershipUser" /> objects representing all of the users in the database.</returns>
	public static MembershipUserCollection GetAllUsers()
	{
		int totalRecords;
		return GetAllUsers(0, int.MaxValue, out totalRecords);
	}

	/// <summary>Gets a collection of all the users in the database in pages of data.</summary>
	/// <param name="pageIndex">The index of the page of results to return. Use 0 to indicate the first page.</param>
	/// <param name="pageSize">The size of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="totalRecords">The total number of users.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> of <see cref="T:System.Web.Security.MembershipUser" /> objects representing all the users in the database for the configured <see langword="applicationName" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="pageIndex" /> is less than zero.-or-
	///         <paramref name="pageSize" /> is less than 1.</exception>
	public static MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
	{
		return Provider.GetAllUsers(pageIndex, pageSize, out totalRecords);
	}

	/// <summary>Gets the number of users currently accessing an application.</summary>
	/// <returns>The number of users currently accessing an application.</returns>
	public static int GetNumberOfUsersOnline()
	{
		return Provider.GetNumberOfUsersOnline();
	}

	/// <summary>Gets the information from the data source and updates the last-activity date/time stamp for the current logged-on membership user.</summary>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the current logged-on user.</returns>
	/// <exception cref="T:System.ArgumentException">No membership user is currently logged in.</exception>
	public static MembershipUser GetUser()
	{
		return GetUser(HttpContext.Current.User.Identity.Name, userIsOnline: true);
	}

	/// <summary>Gets the information from the data source for the current logged-on membership user. Updates the last-activity date/time stamp for the current logged-on membership user, if specified.</summary>
	/// <param name="userIsOnline">If <see langword="true" />, updates the last-activity date/time stamp for the specified user. </param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the current logged-on user.</returns>
	/// <exception cref="T:System.ArgumentException">No membership user is currently logged in.</exception>
	public static MembershipUser GetUser(bool userIsOnline)
	{
		return GetUser(HttpContext.Current.User.Identity.Name, userIsOnline);
	}

	/// <summary>Gets the information from the data source for the specified membership user.</summary>
	/// <param name="username">The name of the user to retrieve.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the specified user. If the <paramref name="username" /> parameter does not correspond to an existing user, this method returns <see langword="null" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> contains a comma (,). </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	public static MembershipUser GetUser(string username)
	{
		return GetUser(username, userIsOnline: false);
	}

	/// <summary>Gets the information from the data source for the specified membership user. Updates the last-activity date/time stamp for the user, if specified.</summary>
	/// <param name="username">The name of the user to retrieve. </param>
	/// <param name="userIsOnline">If <see langword="true" />, updates the last-activity date/time stamp for the specified user. </param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the specified user. If the <paramref name="username" /> parameter does not correspond to an existing user, this method returns <see langword="null" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> contains a comma (,). </exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	public static MembershipUser GetUser(string username, bool userIsOnline)
	{
		return Provider.GetUser(username, userIsOnline);
	}

	/// <summary>Gets the information from the data source for the membership user associated with the specified unique identifier.</summary>
	/// <param name="providerUserKey">The unique user identifier from the membership data source for the user.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the user associated with the specified unique identifier.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="providerUserKey" /> is <see langword="null" />. </exception>
	public static MembershipUser GetUser(object providerUserKey)
	{
		return GetUser(providerUserKey, userIsOnline: false);
	}

	/// <summary>Gets the information from the data source for the membership user associated with the specified unique identifier. Updates the last-activity date/time stamp for the user, if specified.</summary>
	/// <param name="providerUserKey">The unique user identifier from the membership data source for the user.</param>
	/// <param name="userIsOnline">If <see langword="true" />, updates the last-activity date/time stamp for the specified user.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUser" /> object representing the user associated with the specified unique identifier.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="providerUserKey" /> is <see langword="null" />. </exception>
	public static MembershipUser GetUser(object providerUserKey, bool userIsOnline)
	{
		return Provider.GetUser(providerUserKey, userIsOnline);
	}

	/// <summary>Gets a user name where the e-mail address for the user matches the specified e-mail address.</summary>
	/// <param name="emailToMatch">The e-mail address to search for. </param>
	/// <returns>The user name where the e-mail address for the user matches the specified e-mail address. If no match is found, <see langword="null" /> is returned.</returns>
	public static string GetUserNameByEmail(string emailToMatch)
	{
		return Provider.GetUserNameByEmail(emailToMatch);
	}

	/// <summary>Updates the database with the information for the specified user.</summary>
	/// <param name="user">A <see cref="T:System.Web.Security.MembershipUser" /> object that represents the user to be updated and the updated information for the user. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="user" /> is <see langword="null" />.</exception>
	public static void UpdateUser(MembershipUser user)
	{
		Provider.UpdateUser(user);
	}

	/// <summary>Verifies that the supplied user name and password are valid.</summary>
	/// <param name="username">The name of the user to be validated. </param>
	/// <param name="password">The password for the specified user. </param>
	/// <returns>
	///     <see langword="true" /> if the supplied user name and password are valid; otherwise, <see langword="false" />.</returns>
	public static bool ValidateUser(string username, string password)
	{
		return Provider.ValidateUser(username, password);
	}

	/// <summary>Gets a collection of membership users where the e-mail address contains the specified e-mail address to match.</summary>
	/// <param name="emailToMatch">The e-mail address to search for.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> that contains all users that match the <paramref name="emailToMatch" /> parameter.Leading and trailing spaces are trimmed from the <paramref name="emailToMatch" /> parameter value.</returns>
	public static MembershipUserCollection FindUsersByEmail(string emailToMatch)
	{
		int totalRecords;
		return Provider.FindUsersByEmail(emailToMatch, 0, int.MaxValue, out totalRecords);
	}

	/// <summary>Gets a collection of membership users, in a page of data, where the e-mail address contains the specified e-mail address to match.</summary>
	/// <param name="emailToMatch">The e-mail address to search for.</param>
	/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="pageSize">The size of the page of results to return.</param>
	/// <param name="totalRecords">The total number of matched users.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="pageIndex" /> is less than zero.-or-
	///         <paramref name="pageSize" /> is less than 1.</exception>
	public static MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
	{
		return Provider.FindUsersByEmail(emailToMatch, pageIndex, pageSize, out totalRecords);
	}

	/// <summary>Gets a collection of membership users where the user name contains the specified user name to match.</summary>
	/// <param name="usernameToMatch">The user name to search for.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> that contains all users that match the <paramref name="usernameToMatch" /> parameter.Leading and trailing spaces are trimmed from the <paramref name="usernameToMatch" /> parameter value.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="usernameToMatch" /> is an empty string.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="usernameToMatch" /> is <see langword="null" />.</exception>
	public static MembershipUserCollection FindUsersByName(string usernameToMatch)
	{
		int totalRecords;
		return Provider.FindUsersByName(usernameToMatch, 0, int.MaxValue, out totalRecords);
	}

	/// <summary>Gets a collection of membership users, in a page of data, where the user name contains the specified user name to match.</summary>
	/// <param name="usernameToMatch">The user name to search for.</param>
	/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="pageSize">The size of the page of results to return.</param>
	/// <param name="totalRecords">The total number of matched users.</param>
	/// <returns>A <see cref="T:System.Web.Security.MembershipUserCollection" /> that contains a page of <paramref name="pageSize" /><see cref="T:System.Web.Security.MembershipUser" /> objects beginning at the page specified by <paramref name="pageIndex" />.Leading and trailing spaces are trimmed from the <paramref name="usernameToMatch" /> parameter value.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="usernameToMatch" /> is an empty string.-or-
	///         <paramref name="pageIndex" /> is less than zero.-or-
	///         <paramref name="pageSize" /> is less than 1.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="usernameToMatch" /> is <see langword="null" />.</exception>
	public static MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
	{
		return Provider.FindUsersByName(usernameToMatch, pageIndex, pageSize, out totalRecords);
	}
}
