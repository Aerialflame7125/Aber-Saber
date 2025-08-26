using System.Configuration;
using System.Web.Configuration;

namespace System.Web.Profile;

/// <summary>Manages user profile data and settings.</summary>
public static class ProfileManager
{
	private static ProfileSection config;

	private static ProfileProviderCollection providersCollection;

	/// <summary>Gets or sets the name of the application for which to store and retrieve profile information.</summary>
	/// <returns>The name of the application for which to store and retrieve profile information.</returns>
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

	/// <summary>Gets a value indicating whether the user profile will be automatically saved at the end of the execution of an ASP.NET page.</summary>
	/// <returns>
	///     <see langword="true" /> if the user profile will be automatically saved at the end of the execution of an ASP.NET page; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	/// <exception cref="T:System.Web.HttpException">An attempt was made to get the <see cref="P:System.Web.Profile.ProfileManager.AutomaticSaveEnabled" /> property value without at least <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" /> permission.</exception>
	public static bool AutomaticSaveEnabled => config.AutomaticSaveEnabled;

	/// <summary>Gets a value indicating whether the user profile is enabled for the application.</summary>
	/// <returns>
	///     <see langword="true" /> if the user profile is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public static bool Enabled => config.Enabled;

	/// <summary>Gets a reference to the default profile provider for the application.</summary>
	/// <returns>The default profile provider for the application.</returns>
	/// <exception cref="T:System.Web.HttpException">An attempt was made to get the <see cref="P:System.Web.Profile.ProfileManager.Provider" /> property value without at least <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" /> permission.</exception>
	[MonoTODO("check AspNetHostingPermissionLevel")]
	public static ProfileProvider Provider => Providers[config.DefaultProvider] ?? throw new ConfigurationErrorsException("Provider '" + config.DefaultProvider + "' was not found");

	/// <summary>Gets a collection of the profile providers for the ASP.NET application.</summary>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileProviderCollection" /> of the profile providers configured for the ASP.NET application.</returns>
	/// <exception cref="T:System.Web.HttpException">An attempt was made to get the <see cref="P:System.Web.Profile.ProfileManager.Providers" /> property value without at least <see cref="F:System.Web.AspNetHostingPermissionLevel.Medium" /> permission.</exception>
	public static ProfileProviderCollection Providers
	{
		get
		{
			CheckEnabled();
			if (providersCollection == null)
			{
				ProfileProviderCollection providers = new ProfileProviderCollection();
				ProvidersHelper.InstantiateProviders(config.Providers, providers, typeof(ProfileProvider));
				providersCollection = providers;
			}
			return providersCollection;
		}
	}

	static ProfileManager()
	{
		config = (ProfileSection)WebConfigurationManager.GetSection("system.web/profile");
	}

	/// <summary>Deletes user profile data for which the last activity date and time occurred before the specified date and time.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are deleted.</param>
	/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
	/// <returns>The number of profiles deleted from the data source.</returns>
	public static int DeleteInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
	{
		return Provider.DeleteInactiveProfiles(authenticationOption, userInactiveSinceDate);
	}

	/// <summary>Deletes the profile for the specified user name from the data source.</summary>
	/// <param name="username">The user name for the profile to be deleted.</param>
	/// <returns>
	///     <see langword="true" /> if the user profile was found and deleted; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> is an empty string ("") or contains a comma.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	public static bool DeleteProfile(string username)
	{
		return Provider.DeleteProfiles(new string[1] { username }) > 0;
	}

	/// <summary>Deletes profile properties and information for the supplied list of user names.</summary>
	/// <param name="usernames">A string array of user names for profiles to be deleted. </param>
	/// <returns>The number of profiles deleted from the data source.</returns>
	/// <exception cref="T:System.ArgumentException">The length of <paramref name="usernames" /> is zero.- or -One of the items in <paramref name="usernames" /> is an empty string ("") or contains a comma.- or -Two or more items in <paramref name="usernames" /> have the same value.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="usernames" /> is <see langword="null" />.- or -One of the items in <paramref name="usernames" /> is <see langword="null" />.</exception>
	public static int DeleteProfiles(string[] usernames)
	{
		return Provider.DeleteProfiles(usernames);
	}

	/// <summary>Deletes profile properties and information from the data source for the supplied list of profiles.</summary>
	/// <param name="profiles">A <see cref="T:System.Web.Profile.ProfileInfoCollection" />  that contains profile information for profiles to be deleted.</param>
	/// <returns>The number of profiles deleted from the data source.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="profiles" /> has a <see cref="P:System.Web.Profile.ProfileInfoCollection.Count" /> value of zero.- or -One of the <see cref="T:System.Web.Profile.ProfileInfo" /> objects in <paramref name="profiles" /> has a <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> that is an empty string ("") or contains a comma.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="profiles" /> is <see langword="null" />.- or -One of the <see cref="T:System.Web.Profile.ProfileInfo" /> objects in <paramref name="profiles" /> has a <see cref="P:System.Web.Profile.ProfileInfo.UserName" /> that is <see langword="null" />.</exception>
	public static int DeleteProfiles(ProfileInfoCollection profiles)
	{
		return Provider.DeleteProfiles(profiles);
	}

	/// <summary>Retrieves profile information for all profiles in which the last activity date occurred on or before the specified date and time and the user name for the profile matches the specified name.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" />  enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
	/// <param name="usernameToMatch">The user name for which to search.</param>
	/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for inactive profiles in which the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="usernameToMatch" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="usernameToMatch" /> is an empty string ("").</exception>
	public static ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate)
	{
		int totalRecords = 0;
		return Provider.FindInactiveProfilesByUserName(authenticationOption, usernameToMatch, userInactiveSinceDate, 0, int.MaxValue, out totalRecords);
	}

	/// <summary>Retrieves profile information in pages of data for profiles in which the last activity date occurred on or before the specified date and time and the user name for the profile matches the specified name.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" />  values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
	/// <param name="usernameToMatch">The user name for which to search.</param>
	/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
	/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="pageSize">The size of the page of results to return.</param>
	/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for inactive profiles where the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="usernameToMatch" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="usernameToMatch" /> is an empty string ("").- or -
	///         <paramref name="pageIndex" /> is less than zero.- or -
	///         <paramref name="pageSize" /> is less than 1.- or -
	///         <paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
	public static ProfileInfoCollection FindInactiveProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
	{
		return Provider.FindInactiveProfilesByUserName(authenticationOption, usernameToMatch, userInactiveSinceDate, pageIndex, pageSize, out totalRecords);
	}

	/// <summary>Retrieves all profile information for profiles in which the user name matches the specified name.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" />  enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
	/// <param name="usernameToMatch">The user name for which to search.</param>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for profiles where the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="usernameToMatch" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="usernameToMatch" /> is an empty string ("").</exception>
	public static ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch)
	{
		int totalRecords = 0;
		return Provider.FindProfilesByUserName(authenticationOption, usernameToMatch, 0, int.MaxValue, out totalRecords);
	}

	/// <summary>Retrieves profile information in pages of data for profiles in which the user name matches the specified name.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" />  enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
	/// <param name="usernameToMatch">The user name for which to search.</param>
	/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="pageSize">The size of the page of results to return.</param>
	/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for profiles where the user name matches the supplied <paramref name="usernameToMatch" /> parameter.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="usernameToMatch" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="usernameToMatch" /> is an empty string ("").- or -
	///         <paramref name="pageIndex" /> is less than zero.- or -
	///         <paramref name="pageSize" /> is less than 1.- or -
	///         <paramref name="pageIndex" /> multiplied by <paramref name="pageSize" /> is larger than <see cref="F:System.Int32.MaxValue" />.</exception>
	public static ProfileInfoCollection FindProfilesByUserName(ProfileAuthenticationOption authenticationOption, string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
	{
		return Provider.FindProfilesByUserName(authenticationOption, usernameToMatch, pageIndex, pageSize, out totalRecords);
	}

	/// <summary>Retrieves all user profile data for profiles in which the last activity date occurred on or before the specified date and time.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
	/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information about the inactive profiles.</returns>
	public static ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
	{
		int totalRecords = 0;
		return Provider.GetAllInactiveProfiles(authenticationOption, userInactiveSinceDate, 0, int.MaxValue, out totalRecords);
	}

	/// <summary>Retrieves a page of <see cref="T:System.Web.Profile.ProfileInfo" /> objects for user profiles in which the last activity date occurred on or before the specified date and time.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
	/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
	/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="pageSize">The size of the page of results to return.</param>
	/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information about the inactive profiles.</returns>
	public static ProfileInfoCollection GetAllInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate, int pageIndex, int pageSize, out int totalRecords)
	{
		return Provider.GetAllInactiveProfiles(authenticationOption, userInactiveSinceDate, pageIndex, pageSize, out totalRecords);
	}

	/// <summary>Retrieves user profile data for profiles in the data source.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for all of the profiles in the data source.</returns>
	public static ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption)
	{
		int totalRecords = 0;
		return Provider.GetAllProfiles(authenticationOption, 0, int.MaxValue, out totalRecords);
	}

	/// <summary>Retrieves pages of user profile data.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
	/// <param name="pageIndex">The index of the page of results to return. <paramref name="pageIndex" /> is zero-based.</param>
	/// <param name="pageSize">The size of the page of results to return.</param>
	/// <param name="totalRecords">When this method returns, contains an integer that identifies the total number of profiles. This parameter is passed uninitialized.</param>
	/// <returns>A <see cref="T:System.Web.Profile.ProfileInfoCollection" /> containing user profile information for all of the profiles in the data source.</returns>
	public static ProfileInfoCollection GetAllProfiles(ProfileAuthenticationOption authenticationOption, int pageIndex, int pageSize, out int totalRecords)
	{
		return Provider.GetAllProfiles(authenticationOption, pageIndex, pageSize, out totalRecords);
	}

	/// <summary>Gets the number of profiles in which the last activity date occurred on or before the specified date.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
	/// <param name="userInactiveSinceDate">A <see cref="T:System.DateTime" /> object that identifies which user profiles are considered inactive. If the <see cref="P:System.Web.Profile.ProfileInfo.LastActivityDate" />  of a user profile occurs on or before this date and time, the profile is considered inactive.</param>
	/// <returns>The number of profiles in the data source for which the last activity date occurred before the specified date and time.</returns>
	public static int GetNumberOfInactiveProfiles(ProfileAuthenticationOption authenticationOption, DateTime userInactiveSinceDate)
	{
		return Provider.GetNumberOfInactiveProfiles(authenticationOption, userInactiveSinceDate);
	}

	/// <summary>Gets the number of profiles in the data source.</summary>
	/// <param name="authenticationOption">One of the <see cref="T:System.Web.Profile.ProfileAuthenticationOption" /> enumeration values, specifying whether anonymous, authenticated, or both types of profiles are returned.</param>
	/// <returns>The number of profiles in the data source.</returns>
	public static int GetNumberOfProfiles(ProfileAuthenticationOption authenticationOption)
	{
		int totalRecords = 0;
		Provider.GetAllProfiles(authenticationOption, 0, 1, out totalRecords);
		return totalRecords;
	}

	private static void CheckEnabled()
	{
		if (!Enabled)
		{
			throw new Exception("This feature is not enabled.  To enable it, add <profile enabled=\"true\"> to your configuration file.");
		}
	}
}
