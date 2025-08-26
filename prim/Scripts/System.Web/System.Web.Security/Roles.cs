using System.Configuration;
using System.Configuration.Provider;
using System.Threading;
using System.Web.Configuration;

namespace System.Web.Security;

/// <summary>Manages user membership in roles for authorization checking in an ASP.NET application. This class cannot be inherited.</summary>
public static class Roles
{
	private static RoleManagerSection config;

	private static RoleProviderCollection providersCollection;

	private static string CurrentUser
	{
		get
		{
			if (HttpContext.Current != null && HttpContext.Current.User != null)
			{
				return HttpContext.Current.User.Identity.Name;
			}
			return Thread.CurrentPrincipal.Identity.Name;
		}
	}

	/// <summary>Gets or sets the name of the application to store and retrieve role information for.</summary>
	/// <returns>The name of the application to store and retrieve role information for.</returns>
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

	/// <summary>Gets a value indicating whether the current user's roles are cached in a cookie.</summary>
	/// <returns>
	///     <see langword="true" /> if the current user's roles are cached in a cookie; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public static bool CacheRolesInCookie => config.CacheRolesInCookie;

	/// <summary>Gets the name of the cookie where role names are cached.</summary>
	/// <returns>The name of the cookie where role names are cached. The default is .ASPXROLES.</returns>
	public static string CookieName => config.CookieName;

	/// <summary>Gets the path for the cached role names cookie.</summary>
	/// <returns>The path of the cookie where role names are cached. The default is /.</returns>
	public static string CookiePath => config.CookiePath;

	/// <summary>Gets a value that indicates how role names cached in a cookie are protected.</summary>
	/// <returns>One of the <see cref="T:System.Web.Security.CookieProtection" /> enumeration values indicating how role names that are cached in a cookie are protected. The default is <see langword="All" />.</returns>
	public static CookieProtection CookieProtectionValue => config.CookieProtection;

	/// <summary>Gets a value indicating whether the role names cookie requires SSL in order to be returned to the server.</summary>
	/// <returns>
	///     <see langword="true" /> if SSL is required to return the role names cookie to the server; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public static bool CookieRequireSSL => config.CookieRequireSSL;

	/// <summary>Indicates whether the role names cookie expiration date and time will be reset periodically.</summary>
	/// <returns>
	///     <see langword="true" /> if the role names cookie expiration date and time will be reset periodically; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public static bool CookieSlidingExpiration => config.CookieSlidingExpiration;

	/// <summary>Gets the number of minutes before the roles cookie expires.</summary>
	/// <returns>An integer specifying the number of minutes before the roles cookie expires. The default is 30 minutes.</returns>
	public static int CookieTimeout => (int)config.CookieTimeout.TotalMinutes;

	/// <summary>Gets a value indicating whether the role-names cookie is session-based or persistent.</summary>
	/// <returns>
	///     <see langword="true" /> if the role-names cookie is a persistent cookie; otherwise <see langword="false" />. The default is <see langword="false" />.</returns>
	public static bool CreatePersistentCookie => config.CreatePersistentCookie;

	/// <summary>Gets the value of the domain of the role-names cookie.</summary>
	/// <returns>The <see cref="P:System.Web.HttpCookie.Domain" /> of the role names cookie.</returns>
	public static string Domain => config.Domain;

	/// <summary>Gets or sets a value indicating whether role management is enabled for the current Web application.</summary>
	/// <returns>
	///     <see langword="true" /> if role management is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public static bool Enabled
	{
		get
		{
			return config.Enabled;
		}
		set
		{
			config.Enabled = value;
		}
	}

	/// <summary>Gets the maximum number of role names to be cached for a user.</summary>
	/// <returns>The maximum number of role names to be cached for a user. The default is 25.</returns>
	public static int MaxCachedResults => config.MaxCachedResults;

	/// <summary>Gets the default role provider for the application.</summary>
	/// <returns>The default role provider for the application, which is exposed as a class that inherits the <see cref="T:System.Web.Security.RoleProvider" /> abstract class.</returns>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static RoleProvider Provider => Providers[config.DefaultProvider] ?? throw new ConfigurationErrorsException("Default Role Provider could not be found: Cannot instantiate provider: '" + config.DefaultProvider + "'.");

	/// <summary>Gets a collection of the role providers for the ASP.NET application.</summary>
	/// <returns>A <see cref="T:System.Web.Security.RoleProviderCollection" /> that contains the role providers configured for the ASP.NET application.</returns>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static RoleProviderCollection Providers
	{
		get
		{
			CheckEnabled();
			if (providersCollection == null)
			{
				RoleProviderCollection providers = new RoleProviderCollection();
				ProvidersHelper.InstantiateProviders(config.Providers, providers, typeof(RoleProvider));
				providersCollection = providers;
			}
			return providersCollection;
		}
	}

	static Roles()
	{
		config = (RoleManagerSection)WebConfigurationManager.GetSection("system.web/roleManager");
	}

	/// <summary>Adds the specified users to the specified role.</summary>
	/// <param name="usernames">A string array of user names to add to the specified role. </param>
	/// <param name="roleName">The role to add the specified user names to.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" />.-or-One of the elements in <paramref name="usernames" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string or contains a comma (,).-or-One of the elements in <paramref name="usernames" /> is an empty string or contains a comma (,).-or-
	///         <paramref name="usernames" /> contains a duplicate element.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static void AddUsersToRole(string[] usernames, string roleName)
	{
		Provider.AddUsersToRoles(usernames, new string[1] { roleName });
	}

	/// <summary>Adds the specified users to the specified roles.</summary>
	/// <param name="usernames">A string array of user names to add to the specified roles. </param>
	/// <param name="roleNames">A string array of role names to add the specified user names to. </param>
	/// <exception cref="T:System.ArgumentNullException">One of the roles in <paramref name="roleNames" /> is <see langword="null" />.-or-One of the users in <paramref name="usernames" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">One of the roles in <paramref name="roleNames" /> is an empty string or contains a comma (,).-or-One of the users in <paramref name="usernames" /> is an empty string or contains a comma (,).-or-
	///         <paramref name="roleNames" /> contains a duplicate element.-or-
	///         <paramref name="usernames" /> contains a duplicate element.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static void AddUsersToRoles(string[] usernames, string[] roleNames)
	{
		Provider.AddUsersToRoles(usernames, roleNames);
	}

	/// <summary>Adds the specified user to the specified role.</summary>
	/// <param name="username">The user name to add to the specified role.</param>
	/// <param name="roleName">The role to add the specified user name to. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" />.-or-
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string or contains a comma (,).-or-
	///         <paramref name="username" /> is an empty string or contains a comma (,).</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled. -or-User is already assigned to the specified role.</exception>
	public static void AddUserToRole(string username, string roleName)
	{
		Provider.AddUsersToRoles(new string[1] { username }, new string[1] { roleName });
	}

	/// <summary>Adds the specified user to the specified roles.</summary>
	/// <param name="username">The user name to add to the specified roles. </param>
	/// <param name="roleNames">A string array of roles to add the specified user name to. </param>
	/// <exception cref="T:System.ArgumentNullException">One of the roles in <paramref name="roleNames" /> is <see langword="null" />.-or-
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">One of the roles in <paramref name="roleNames" /> is an empty string or contains a comma (,).-or-
	///         <paramref name="username" /> is an empty string or contains a comma (,).-or-
	///         <paramref name="roleNames" /> contains a duplicate element.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static void AddUserToRoles(string username, string[] roleNames)
	{
		Provider.AddUsersToRoles(new string[1] { username }, roleNames);
	}

	/// <summary>Adds a new role to the data source.</summary>
	/// <param name="roleName">The name of the role to create. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string.-or-
	///         <paramref name="roleName" /> contains a comma.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static void CreateRole(string roleName)
	{
		Provider.CreateRole(roleName);
	}

	/// <summary>Deletes the cookie where role names are cached.</summary>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static void DeleteCookie()
	{
		if (CacheRolesInCookie)
		{
			HttpCookieCollection cookies = ((HttpContext.Current ?? throw new HttpException("Context is null.")).Response ?? throw new HttpException("Response is null.")).Cookies;
			cookies.Remove(CookieName);
			cookies.Add(new HttpCookie(CookieName, "")
			{
				Expires = new DateTime(1999, 10, 12),
				Path = CookiePath
			});
		}
	}

	/// <summary>Removes a role from the data source.</summary>
	/// <param name="roleName">The name of the role to delete. </param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="roleName" /> was deleted from the data source; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string or contains a comma (,).</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">
	///         <paramref name="roleName" /> has one or more members.-or-Role management is not enabled.</exception>
	public static bool DeleteRole(string roleName)
	{
		return Provider.DeleteRole(roleName, throwOnPopulatedRole: true);
	}

	/// <summary>Removes a role from the data source.</summary>
	/// <param name="roleName">The name of the role to delete.</param>
	/// <param name="throwOnPopulatedRole">If <see langword="true" />, throws an exception if <paramref name="roleName" /> has one or more members.</param>
	/// <returns>
	///     <see langword="true" /> if <paramref name="roleName" /> was deleted from the data source; otherwise; <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">
	///         <paramref name="roleName" /> has one or more members and <paramref name="throwOnPopulatedRole" /> is <see langword="true" />.-or-Role management is not enabled.</exception>
	public static bool DeleteRole(string roleName, bool throwOnPopulatedRole)
	{
		return Provider.DeleteRole(roleName, throwOnPopulatedRole);
	}

	/// <summary>Gets a list of all the roles for the application.</summary>
	/// <returns>A string array containing the names of all the roles stored in the data source for the application.</returns>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static string[] GetAllRoles()
	{
		return Provider.GetAllRoles();
	}

	/// <summary>Gets a list of the roles that the currently logged-on user is in.</summary>
	/// <returns>A string array containing the names of all the roles that the currently logged-on user is in.</returns>
	/// <exception cref="T:System.ArgumentNullException">There is no current logged-on user.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static string[] GetRolesForUser()
	{
		return Provider.GetRolesForUser(CurrentUser);
	}

	/// <summary>Gets a list of the roles that a user is in.</summary>
	/// <param name="username">The user to return a list of roles for. </param>
	/// <returns>A string array containing the names of all the roles that the specified user is in.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="username" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="username" /> contains a comma (,). </exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static string[] GetRolesForUser(string username)
	{
		return Provider.GetRolesForUser(username);
	}

	/// <summary>Gets a list of users in the specified role.</summary>
	/// <param name="roleName">The role to get the list of users for. </param>
	/// <returns>A string array containing the names of all the users who are members of the specified role.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string or contains a comma (,).</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static string[] GetUsersInRole(string roleName)
	{
		return Provider.GetUsersInRole(roleName);
	}

	/// <summary>Gets a value indicating whether the currently logged-on user is in the specified role. The API is only intended to be called within the context of an ASP.NET request thread, and in that sanctioned use case it is thread-safe.</summary>
	/// <param name="roleName">The name of the role to search in. </param>
	/// <returns>
	///     <see langword="true" /> if the currently logged-on user is in the specified role; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" />.-or-There is no current logged-on user.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string or contains a comma (,).</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static bool IsUserInRole(string roleName)
	{
		return IsUserInRole(CurrentUser, roleName);
	}

	/// <summary>Gets a value indicating whether the specified user is in the specified role. The API is only intended to be called within the context of an ASP.NET request thread, and in that sanctioned use case it is thread-safe.</summary>
	/// <param name="username">The name of the user to search for. </param>
	/// <param name="roleName">The name of the role to search in. </param>
	/// <returns>
	///     <see langword="true" /> if the specified user is in the specified role; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" />.-or-
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string or contains a comma (,).-or-
	///         <paramref name="username" /> contains a comma (,).</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static bool IsUserInRole(string username, string roleName)
	{
		if (string.IsNullOrEmpty(username))
		{
			return false;
		}
		return Provider.IsUserInRole(username, roleName);
	}

	/// <summary>Removes the specified user from the specified role.</summary>
	/// <param name="username">The user to remove from the specified role.</param>
	/// <param name="roleName">The role to remove the specified user from.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" />.-or-
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string or contains a comma (,)
	///         <paramref name="username" /> is an empty string or contains a comma (,).</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static void RemoveUserFromRole(string username, string roleName)
	{
		Provider.RemoveUsersFromRoles(new string[1] { username }, new string[1] { roleName });
	}

	/// <summary>Removes the specified user from the specified roles.</summary>
	/// <param name="username">The user to remove from the specified roles. </param>
	/// <param name="roleNames">A string array of role names to remove the specified user from. </param>
	/// <exception cref="T:System.ArgumentNullException">One of the roles in <paramref name="roleNames" /> is <see langword="null" />.-or-
	///         <paramref name="username" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">One of the roles in <paramref name="roleNames" /> is an empty string or contains a comma (,).-or-
	///         <paramref name="username" /> is an empty string or contains a comma (,).-or-
	///         <paramref name="roleNames" /> contains a duplicate element.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static void RemoveUserFromRoles(string username, string[] roleNames)
	{
		Provider.RemoveUsersFromRoles(new string[1] { username }, roleNames);
	}

	/// <summary>Removes the specified users from the specified role.</summary>
	/// <param name="usernames">A string array of user names to remove from the specified roles. </param>
	/// <param name="roleName">The name of the role to remove the specified users from. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" />.-or-One of the user names in <paramref name="usernames" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string or contains a comma (,).-or-One of the user names in <paramref name="usernames" /> is an empty string or contains a comma (,).-or-
	///         <paramref name="usernames" /> contains a duplicate element.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static void RemoveUsersFromRole(string[] usernames, string roleName)
	{
		Provider.RemoveUsersFromRoles(usernames, new string[1] { roleName });
	}

	/// <summary>Removes the specified user names from the specified roles.</summary>
	/// <param name="usernames">A string array of user names to remove from the specified roles. </param>
	/// <param name="roleNames">A string array of role names to remove the specified users from. </param>
	/// <exception cref="T:System.ArgumentNullException">One of the roles specified in <paramref name="roleNames" /> is <see langword="null" />.-or-One of the users specified in <paramref name="usernames" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">One of the roles specified in <paramref name="roleNames" /> is an empty string or contains a comma (,).-or-One of the users specified in <paramref name="usernames" /> is an empty string or contains a comma (,).-or-
	///         <paramref name="roleNames" /> contains a duplicate element.-or-
	///         <paramref name="usernames" /> contains a duplicate element.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
	{
		Provider.RemoveUsersFromRoles(usernames, roleNames);
	}

	/// <summary>Gets a value indicating whether the specified role name already exists in the role data source.</summary>
	/// <param name="roleName">The name of the role to search for in the data source. </param>
	/// <returns>
	///     <see langword="true" /> if the role name already exists in the data source; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string or contains a comma (,).</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static bool RoleExists(string roleName)
	{
		return Provider.RoleExists(roleName);
	}

	/// <summary>Gets a list of users in a specified role where the user name contains the specified user name to match.</summary>
	/// <param name="roleName">The role to search in.</param>
	/// <param name="usernameToMatch">The user name to search for.</param>
	/// <returns>A string array containing the names of all the users whose user name matches <paramref name="usernameToMatch" /> and who are members of the specified role.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="roleName" /> is <see langword="null" /> (<see langword="Nothing" /> in Visual Basic).-or-
	///         <paramref name="usernameToMatch" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="roleName" /> is an empty string or contains a comma (,).-or-
	///         <paramref name="usernameToMatch" /> is an empty string.</exception>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">Role management is not enabled.</exception>
	public static string[] FindUsersInRole(string roleName, string usernameToMatch)
	{
		return Provider.FindUsersInRole(roleName, usernameToMatch);
	}

	private static void CheckEnabled()
	{
		if (!Enabled)
		{
			throw new ProviderException("This feature is not enabled.  To enable it, add <roleManager enabled=\"true\"> to your configuration file.");
		}
	}
}
