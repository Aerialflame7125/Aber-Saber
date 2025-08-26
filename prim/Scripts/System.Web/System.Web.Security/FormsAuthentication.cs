using System.Collections.Specialized;
using System.IO;
using System.Security.Cryptography;
using System.Security.Permissions;
using System.Text;
using System.Web.Compilation;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Security;

/// <summary>Manages forms-authentication services for Web applications. This class cannot be inherited.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class FormsAuthentication
{
	private static string authConfigPath = "system.web/authentication";

	private static string machineKeyConfigPath = "system.web/machineKey";

	private static object locker = new object();

	private static bool initialized;

	private static string cookieName;

	private static string cookiePath;

	private static int timeout;

	private static FormsProtectionEnum protection;

	private static bool requireSSL;

	private static bool slidingExpiration;

	private static string cookie_domain;

	private static HttpCookieMode cookie_mode;

	private static bool cookies_supported;

	private static string default_url;

	private static bool enable_crossapp_redirects;

	private static string login_url;

	private static string[] indexFiles = new string[5] { "index.aspx", "Default.aspx", "default.aspx", "index.html", "index.htm" };

	/// <summary>Gets the amount of time before an authentication ticket expires.</summary>
	/// <returns>The amount of time before an authentication ticket expires.</returns>
	public static TimeSpan Timeout { get; private set; }

	/// <summary>Gets a value that indicates whether forms authentication is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if forms authentication is enabled; otherwise, <see langword="false" />.</returns>
	public static bool IsEnabled => initialized;

	internal static string ReturnUrl => HttpContext.Current.Request["RETURNURL"];

	/// <summary>Gets the name of the cookie used to store the forms-authentication ticket.</summary>
	/// <returns>The name of the cookie used to store the forms-authentication ticket. The default is ".ASPXAUTH".</returns>
	public static string FormsCookieName
	{
		get
		{
			Initialize();
			return cookieName;
		}
	}

	/// <summary>Gets the path for the forms-authentication cookie.</summary>
	/// <returns>The path of the cookie where the forms-authentication ticket information is stored. The default is "/".</returns>
	public static string FormsCookiePath
	{
		get
		{
			Initialize();
			return cookiePath;
		}
	}

	/// <summary>Gets a value indicating whether the forms-authentication cookie requires SSL in order to be returned to the server.</summary>
	/// <returns>
	///     <see langword="true" /> if SSL is required to return the forms-authentication cookie to the server; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public static bool RequireSSL
	{
		get
		{
			Initialize();
			return requireSSL;
		}
	}

	/// <summary>Gets a value indicating whether sliding expiration is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if sliding expiration is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public static bool SlidingExpiration
	{
		get
		{
			Initialize();
			return slidingExpiration;
		}
	}

	/// <summary>Gets the value of the domain of the forms-authentication cookie.</summary>
	/// <returns>The <see cref="P:System.Web.HttpCookie.Domain" /> of the forms-authentication cookie. The default is an empty string ("").</returns>
	public static string CookieDomain
	{
		get
		{
			Initialize();
			return cookie_domain;
		}
	}

	/// <summary>Gets a value that indicates whether the application is configured for cookieless forms authentication.</summary>
	/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values that indicates whether the application is configured for cookieless forms authentication. The default is <see cref="F:System.Web.HttpCookieMode.UseDeviceProfile" />.</returns>
	public static HttpCookieMode CookieMode
	{
		get
		{
			Initialize();
			return cookie_mode;
		}
	}

	/// <summary>Gets a value that indicates whether the application is configured to support cookieless forms authentication.</summary>
	/// <returns>
	///     <see langword="false" /> if the application is configured to support cookieless forms authentication; otherwise, <see langword="true" />.</returns>
	public static bool CookiesSupported
	{
		get
		{
			Initialize();
			return cookies_supported;
		}
	}

	/// <summary>Gets the URL that the <see cref="T:System.Web.Security.FormsAuthentication" /> class will redirect to if no redirect URL is specified.</summary>
	/// <returns>The URL that the <see cref="T:System.Web.Security.FormsAuthentication" /> class will redirect to if no redirect URL is specified. The default is "default.aspx."</returns>
	public static string DefaultUrl
	{
		get
		{
			Initialize();
			return default_url;
		}
	}

	/// <summary>Gets a value indicating whether authenticated users can be redirected to URLs in other Web applications.</summary>
	/// <returns>
	///     <see langword="true" /> if authenticated users can be redirected to URLs in other Web applications; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public static bool EnableCrossAppRedirects
	{
		get
		{
			Initialize();
			return enable_crossapp_redirects;
		}
	}

	/// <summary>Gets the URL for the login page that the <see cref="T:System.Web.Security.FormsAuthentication" /> class will redirect to.</summary>
	/// <returns>The URL for the login page that the <see cref="T:System.Web.Security.FormsAuthentication" /> class will redirect to. The default is "login.aspx."</returns>
	public static string LoginUrl
	{
		get
		{
			Initialize();
			return login_url;
		}
	}

	/// <summary>Enables forms authentication.</summary>
	/// <param name="configurationData">A name-value collection that contains values for "defaultUrl" and/or "loginUrl". The parameter can be null if there are no values for the default URL or the login URL. </param>
	/// <exception cref="T:System.InvalidOperationException">The application is not in the pre-start initialization phase.</exception>
	public static void EnableFormsAuthentication(NameValueCollection configurationData)
	{
		BuildManager.AssertPreStartMethodsRunning();
		if (configurationData != null && configurationData.Count != 0)
		{
			string value = configurationData["loginUrl"];
			if (!string.IsNullOrEmpty(value))
			{
				login_url = value;
			}
			value = configurationData["defaultUrl"];
			if (!string.IsNullOrEmpty(value))
			{
				default_url = value;
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.FormsAuthentication" /> class. </summary>
	public FormsAuthentication()
	{
	}

	/// <summary>Validates a user name and password against credentials stored in the configuration file for an application.</summary>
	/// <param name="name">The user name.</param>
	/// <param name="password">The password for the user. </param>
	/// <returns>
	///     <see langword="true" /> if the user name and password are valid; otherwise, <see langword="false" />.</returns>
	public static bool Authenticate(string name, string password)
	{
		if (name == null || password == null)
		{
			return false;
		}
		Initialize();
		if (HttpContext.Current == null)
		{
			throw new HttpException("Context is null!");
		}
		name = name.ToLower(Helpers.InvariantCulture);
		FormsAuthenticationCredentials credentials = ((AuthenticationSection)WebConfigurationManager.GetSection(authConfigPath)).Forms.Credentials;
		FormsAuthenticationUser formsAuthenticationUser = credentials.Users[name];
		string text = null;
		if (formsAuthenticationUser != null)
		{
			text = formsAuthenticationUser.Password;
		}
		if (text == null)
		{
			return false;
		}
		bool flag = true;
		switch (credentials.PasswordFormat)
		{
		case FormsAuthPasswordFormat.Clear:
			flag = false;
			break;
		case FormsAuthPasswordFormat.MD5:
			password = HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.MD5);
			break;
		case FormsAuthPasswordFormat.SHA1:
			password = HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.SHA1);
			break;
		}
		return string.Compare(password, text, flag ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) == 0;
	}

	private static FormsAuthenticationTicket Decrypt2(byte[] bytes)
	{
		if (protection == FormsProtectionEnum.None)
		{
			return FormsAuthenticationTicket.FromByteArray(bytes);
		}
		MachineKeySection section = (MachineKeySection)WebConfigurationManager.GetWebApplicationSection(machineKeyConfigPath);
		byte[] bytes2 = null;
		if (protection == FormsProtectionEnum.All)
		{
			bytes2 = MachineKeySectionUtils.VerifyDecrypt(section, bytes);
		}
		else if (protection == FormsProtectionEnum.Encryption)
		{
			bytes2 = MachineKeySectionUtils.Decrypt(section, bytes);
		}
		else if (protection == FormsProtectionEnum.Validation)
		{
			bytes2 = MachineKeySectionUtils.Verify(section, bytes);
		}
		return FormsAuthenticationTicket.FromByteArray(bytes2);
	}

	/// <summary>Creates a <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> object based on the encrypted forms-authentication ticket passed to the method.</summary>
	/// <param name="encryptedTicket">The encrypted authentication ticket. </param>
	/// <returns>A <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> object. If the <paramref name="encryptedTicket" /> parameter is not a valid ticket, <see langword="null" /> is returned.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="encryptedTicket" /> is <see langword="null" />.- or -
	///         <paramref name="encryptedTicket" /> is an empty string ("").- or -The length of <paramref name="encryptedTicket" /> is greater than 4096 characters.- or -
	///         <paramref name="encryptedTicket" /> is of an invalid format. </exception>
	public static FormsAuthenticationTicket Decrypt(string encryptedTicket)
	{
		if (string.IsNullOrEmpty(encryptedTicket))
		{
			throw new ArgumentException("Invalid encrypted ticket", "encryptedTicket");
		}
		Initialize();
		byte[] bytes = Convert.FromBase64String(encryptedTicket);
		try
		{
			return Decrypt2(bytes);
		}
		catch (Exception)
		{
			return null;
		}
	}

	/// <summary>Creates a string containing an encrypted forms-authentication ticket suitable for use in an HTTP cookie.</summary>
	/// <param name="ticket">The <see cref="T:System.Web.Security.FormsAuthenticationTicket" /> object with which to create the encrypted forms-authentication ticket. </param>
	/// <returns>A string containing an encrypted forms-authentication ticket.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="ticket" /> is <see langword="null" />.</exception>
	public static string Encrypt(FormsAuthenticationTicket ticket)
	{
		if (ticket == null)
		{
			throw new ArgumentNullException("ticket");
		}
		Initialize();
		byte[] array = ticket.ToByteArray();
		if (protection == FormsProtectionEnum.None)
		{
			return Convert.ToBase64String(array);
		}
		byte[] inArray = null;
		MachineKeySection section = (MachineKeySection)WebConfigurationManager.GetWebApplicationSection(machineKeyConfigPath);
		if (protection == FormsProtectionEnum.All)
		{
			inArray = MachineKeySectionUtils.EncryptSign(section, array);
		}
		else if (protection == FormsProtectionEnum.Encryption)
		{
			inArray = MachineKeySectionUtils.Encrypt(section, array);
		}
		else if (protection == FormsProtectionEnum.Validation)
		{
			inArray = MachineKeySectionUtils.Sign(section, array);
		}
		return Convert.ToBase64String(inArray);
	}

	/// <summary>Creates an authentication cookie for a given user name. This does not set the cookie as part of the outgoing response, so that an application can have more control over how the cookie is issued.</summary>
	/// <param name="userName">The name of the authenticated user. </param>
	/// <param name="createPersistentCookie">
	///       <see langword="true" /> to create a durable cookie (one that is saved across browser sessions); otherwise, <see langword="false" />. </param>
	/// <returns>An <see cref="T:System.Web.HttpCookie" /> that contains encrypted forms-authentication ticket information. The default value for the <see cref="P:System.Web.Security.FormsAuthentication.FormsCookiePath" /> property is used.</returns>
	public static HttpCookie GetAuthCookie(string userName, bool createPersistentCookie)
	{
		return GetAuthCookie(userName, createPersistentCookie, null);
	}

	/// <summary>Creates an authentication cookie for a given user name. This does not set the cookie as part of the outgoing response.</summary>
	/// <param name="userName">The name of the authenticated user. </param>
	/// <param name="createPersistentCookie">
	///       <see langword="true" /> to create a durable cookie (one that is saved across browser sessions); otherwise, <see langword="false" />. </param>
	/// <param name="strCookiePath">The <see cref="P:System.Web.HttpCookie.Path" /> of the authentication cookie. </param>
	/// <returns>An <see cref="T:System.Web.HttpCookie" /> that contains encrypted forms-authentication ticket information.</returns>
	public static HttpCookie GetAuthCookie(string userName, bool createPersistentCookie, string strCookiePath)
	{
		Initialize();
		if (userName == null)
		{
			userName = string.Empty;
		}
		if (strCookiePath == null || strCookiePath.Length == 0)
		{
			strCookiePath = cookiePath;
		}
		DateTime now = DateTime.Now;
		DateTime dateTime = now.AddMinutes(timeout);
		DateTime expires = (createPersistentCookie ? dateTime : DateTime.MinValue);
		FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, now, dateTime, createPersistentCookie, string.Empty, cookiePath);
		HttpCookie httpCookie = new HttpCookie(cookieName, Encrypt(ticket), strCookiePath, expires);
		if (requireSSL)
		{
			httpCookie.Secure = true;
		}
		if (!string.IsNullOrEmpty(cookie_domain))
		{
			httpCookie.Domain = cookie_domain;
		}
		return httpCookie;
	}

	/// <summary>Returns the redirect URL for the original request that caused the redirect to the login page.</summary>
	/// <param name="userName">The name of the authenticated user. </param>
	/// <param name="createPersistentCookie">This parameter is ignored.</param>
	/// <returns>A string that contains the redirect URL.</returns>
	public static string GetRedirectUrl(string userName, bool createPersistentCookie)
	{
		if (userName == null)
		{
			return null;
		}
		Initialize();
		HttpRequest request = HttpContext.Current.Request;
		string returnUrl = ReturnUrl;
		if (returnUrl != null)
		{
			return returnUrl;
		}
		returnUrl = request.ApplicationPath;
		string physicalApplicationPath = request.PhysicalApplicationPath;
		bool flag = false;
		string[] array = indexFiles;
		foreach (string text in array)
		{
			if (File.Exists(Path.Combine(physicalApplicationPath, text)))
			{
				returnUrl = UrlUtils.Combine(returnUrl, text);
				flag = true;
				break;
			}
		}
		if (!flag)
		{
			returnUrl = UrlUtils.Combine(returnUrl, "index.aspx");
		}
		return returnUrl;
	}

	private static string HashPasswordForStoringInConfigFile(string password, FormsAuthPasswordFormat passwordFormat)
	{
		if (password == null)
		{
			throw new ArgumentNullException("password");
		}
		return MachineKeySectionUtils.GetHexString(passwordFormat switch
		{
			FormsAuthPasswordFormat.MD5 => MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(password)), 
			FormsAuthPasswordFormat.SHA1 => SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(password)), 
			_ => throw new ArgumentException("The format must be either MD5 or SHA1", "passwordFormat"), 
		});
	}

	/// <summary>Produces a hash password suitable for storing in a configuration file based on the specified password and hash algorithm.</summary>
	/// <param name="password">The password to hash. </param>
	/// <param name="passwordFormat">The hash algorithm to use. <paramref name="passwordFormat" /> is a <see langword="String" /> that represents one of the <see cref="T:System.Web.Configuration.FormsAuthPasswordFormat" /> enumeration values.</param>
	/// <returns>The hashed password.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="password" /> is <see langword="null" />-or-
	///         <paramref name="passwordFormat" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="passwordFormat" /> is not a valid <see cref="T:System.Web.Configuration.FormsAuthPasswordFormat" /> value.</exception>
	public static string HashPasswordForStoringInConfigFile(string password, string passwordFormat)
	{
		if (password == null)
		{
			throw new ArgumentNullException("password");
		}
		if (passwordFormat == null)
		{
			throw new ArgumentNullException("passwordFormat");
		}
		if (string.Compare(passwordFormat, "MD5", StringComparison.OrdinalIgnoreCase) == 0)
		{
			return HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.MD5);
		}
		if (string.Compare(passwordFormat, "SHA1", StringComparison.OrdinalIgnoreCase) == 0)
		{
			return HashPasswordForStoringInConfigFile(password, FormsAuthPasswordFormat.SHA1);
		}
		throw new ArgumentException("The format must be either MD5 or SHA1", "passwordFormat");
	}

	/// <summary>Initializes the <see cref="T:System.Web.Security.FormsAuthentication" /> object based on the configuration settings for the application.</summary>
	public static void Initialize()
	{
		if (initialized)
		{
			return;
		}
		lock (locker)
		{
			if (!initialized)
			{
				FormsAuthenticationConfiguration forms = ((AuthenticationSection)WebConfigurationManager.GetSection(authConfigPath)).Forms;
				cookieName = forms.Name;
				Timeout = forms.Timeout;
				timeout = (int)forms.Timeout.TotalMinutes;
				cookiePath = forms.Path;
				protection = forms.Protection;
				requireSSL = forms.RequireSSL;
				slidingExpiration = forms.SlidingExpiration;
				cookie_domain = forms.Domain;
				cookie_mode = forms.Cookieless;
				cookies_supported = true;
				if (!string.IsNullOrEmpty(default_url))
				{
					default_url = MapUrl(default_url);
				}
				else
				{
					default_url = MapUrl(forms.DefaultUrl);
				}
				enable_crossapp_redirects = forms.EnableCrossAppRedirects;
				if (!string.IsNullOrEmpty(login_url))
				{
					login_url = MapUrl(login_url);
				}
				else
				{
					login_url = MapUrl(forms.LoginUrl);
				}
				initialized = true;
			}
		}
	}

	private static string MapUrl(string url)
	{
		if (UrlUtils.IsRelativeUrl(url))
		{
			return UrlUtils.Combine(HttpRuntime.AppDomainAppVirtualPath, url);
		}
		return UrlUtils.ResolveVirtualPathFromAppAbsolute(url);
	}

	/// <summary>Redirects an authenticated user back to the originally requested URL or the default URL.</summary>
	/// <param name="userName">The authenticated user name. </param>
	/// <param name="createPersistentCookie">
	///       <see langword="true" /> to create a durable cookie (one that is saved across browser sessions); otherwise, <see langword="false" />. </param>
	/// <exception cref="T:System.Web.HttpException">The return URL specified in the query string contains a protocol other than HTTP: or HTTPS:.</exception>
	public static void RedirectFromLoginPage(string userName, bool createPersistentCookie)
	{
		RedirectFromLoginPage(userName, createPersistentCookie, null);
	}

	/// <summary>Redirects an authenticated user back to the originally requested URL or the default URL using the specified cookie path for the forms-authentication cookie.</summary>
	/// <param name="userName">The authenticated user name. </param>
	/// <param name="createPersistentCookie">
	///       <see langword="true" /> to create a durable cookie (one that is saved across browser sessions); otherwise, <see langword="false" />. </param>
	/// <param name="strCookiePath">The cookie path for the forms-authentication ticket. </param>
	/// <exception cref="T:System.Web.HttpException">The return URL specified in the query string contains a protocol other than HTTP: or HTTPS:.</exception>
	public static void RedirectFromLoginPage(string userName, bool createPersistentCookie, string strCookiePath)
	{
		if (userName != null)
		{
			Initialize();
			SetAuthCookie(userName, createPersistentCookie, strCookiePath);
			Redirect(GetRedirectUrl(userName, createPersistentCookie), end: false);
		}
	}

	/// <summary>Conditionally updates the issue date and time and expiration date and time for a <see cref="T:System.Web.Security.FormsAuthenticationTicket" />.</summary>
	/// <param name="tOld">The forms-authentication ticket to update.</param>
	/// <returns>The updated <see cref="T:System.Web.Security.FormsAuthenticationTicket" />.</returns>
	public static FormsAuthenticationTicket RenewTicketIfOld(FormsAuthenticationTicket tOld)
	{
		if (tOld == null)
		{
			return null;
		}
		DateTime now = DateTime.Now;
		TimeSpan timeSpan = now - tOld.IssueDate;
		if (tOld.Expiration - now > timeSpan)
		{
			return tOld;
		}
		FormsAuthenticationTicket formsAuthenticationTicket = tOld.Clone();
		formsAuthenticationTicket.SetDates(now, now + (tOld.Expiration - tOld.IssueDate));
		return formsAuthenticationTicket;
	}

	/// <summary>Creates an authentication ticket for the supplied user name and adds it to the cookies collection of the response, or to the URL if you are using cookieless authentication.</summary>
	/// <param name="userName">The name of an authenticated user. This does not have to map to a Windows account. </param>
	/// <param name="createPersistentCookie">
	///       <see langword="true" /> to create a persistent cookie (one that is saved across browser sessions); otherwise, <see langword="false" />. </param>
	/// <exception cref="T:System.Web.HttpException">
	///         <see cref="P:System.Web.Security.FormsAuthentication.RequireSSL" /> is <see langword="true" /> and <see cref="P:System.Web.HttpRequest.IsSecureConnection" /> is <see langword="false" />.</exception>
	public static void SetAuthCookie(string userName, bool createPersistentCookie)
	{
		Initialize();
		SetAuthCookie(userName, createPersistentCookie, cookiePath);
	}

	/// <summary>Creates an authentication ticket for the supplied user name and adds it to the cookies collection of the response, using the supplied cookie path, or using the URL if you are using cookieless authentication.</summary>
	/// <param name="userName">The name of an authenticated user. </param>
	/// <param name="createPersistentCookie">
	///       <see langword="true" /> to create a durable cookie (one that is saved across browser sessions); otherwise, <see langword="false" />. </param>
	/// <param name="strCookiePath">The cookie path for the forms-authentication ticket.</param>
	/// <exception cref="T:System.Web.HttpException">
	///         <see cref="P:System.Web.Security.FormsAuthentication.RequireSSL" /> is <see langword="true" /> and <see cref="P:System.Web.HttpRequest.IsSecureConnection" /> is <see langword="false" />.</exception>
	public static void SetAuthCookie(string userName, bool createPersistentCookie, string strCookiePath)
	{
		((HttpContext.Current ?? throw new HttpException("Context is null!")).Response ?? throw new HttpException("Response is null!")).Cookies.Add(GetAuthCookie(userName, createPersistentCookie, strCookiePath));
	}

	/// <summary>Removes the forms-authentication ticket from the browser.</summary>
	public static void SignOut()
	{
		Initialize();
		HttpCookieCollection cookies = ((HttpContext.Current ?? throw new HttpException("Context is null!")).Response ?? throw new HttpException("Response is null!")).Cookies;
		cookies.Remove(cookieName);
		HttpCookie httpCookie = new HttpCookie(cookieName, string.Empty)
		{
			Expires = new DateTime(1999, 10, 12),
			Path = cookiePath
		};
		if (!string.IsNullOrEmpty(cookie_domain))
		{
			httpCookie.Domain = cookie_domain;
		}
		cookies.Add(httpCookie);
		Roles.DeleteCookie();
	}

	/// <summary>Redirects the browser to the login URL.</summary>
	public static void RedirectToLoginPage()
	{
		Redirect(LoginUrl);
	}

	/// <summary>Redirects the browser to the login URL with the specified query string.</summary>
	/// <param name="extraQueryString">The query string to include with the redirect URL.</param>
	[MonoTODO("needs more tests")]
	public static void RedirectToLoginPage(string extraQueryString)
	{
		Redirect(LoginUrl + "?" + extraQueryString);
	}

	private static void Redirect(string url)
	{
		HttpContext.Current.Response.Redirect(url);
	}

	private static void Redirect(string url, bool end)
	{
		HttpContext.Current.Response.Redirect(url, end);
	}
}
