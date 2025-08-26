using System.Security.Permissions;
using System.Security.Principal;

namespace System.Web.Security;

/// <summary>Provides a class to be used by <see cref="T:System.Web.Security.PassportAuthenticationModule" />. It provides a way for an application to access the <see cref="M:System.Web.Security.PassportIdentity.Ticket(System.String)" /> method. This class cannot be inherited. This class is deprecated.</summary>
[MonoNotSupported("")]
[MonoTODO("Not implemented")]
[Obsolete("This type is obsolete. The Passport authentication product is no longer supported and has been superseded by Live ID.")]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class PassportIdentity : IIdentity, IDisposable
{
	/// <summary>Gets the type of authentication used to identify the user. This class is deprecated.</summary>
	/// <returns>The string "Passport".</returns>
	[MonoTODO("Not implemented")]
	public string AuthenticationType
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating the error state associated with the current Passport ticket. This class is deprecated.</summary>
	/// <returns>A 32-bit signed integer indicating the current error state.</returns>
	[MonoTODO("Not implemented")]
	public int Error
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets information on a Passport server connection and query string. This class is deprecated.</summary>
	/// <returns>
	///     <see langword="true" /> if a connection is coming back from the Passport server (logon, update, or registration) and if the Passport data contained on the query string is valid; otherwise, <see langword="false" />.</returns>
	[MonoTODO("Not implemented")]
	public bool GetFromNetworkServer
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets information on whether the Passport member's password was saved. This class is deprecated.</summary>
	/// <returns>
	///     <see langword="true" /> if the Passport member's ticket indicates that the password was saved on the Passport logon page the last time the ticket was refreshed; otherwise, <see langword="false" />.</returns>
	[MonoTODO("Not implemented")]
	public bool HasSavedPassword
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the query string includes a Passport ticket as a cookie. This class is deprecated.</summary>
	/// <returns>
	///     <see langword="true" /> if the query string includes a Passport ticket as a cookie; otherwise, <see langword="false" />.</returns>
	[MonoTODO("Not implemented")]
	public bool HasTicket
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the Passport Unique Identifier (PUID) for the currently authenticated user, in hexadecimal form. This class is deprecated.</summary>
	/// <returns>The PUID for the currently authenticated user, in hexadecimal form.</returns>
	[MonoTODO("Not implemented")]
	public string HexPUID
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets a value indicating whether the user is authenticated against a Passport authority. This class is deprecated.</summary>
	/// <returns>
	///     <see langword="true" /> if the user is authenticated against a central site responsible for Passport authentication; otherwise, <see langword="false" />.</returns>
	[MonoTODO("Not implemented")]
	public bool IsAuthenticated
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets Passport profile attributes. This class is deprecated.</summary>
	/// <param name="strProfileName">The Passport profile attribute to return. </param>
	/// <returns>The Passport profile attribute.</returns>
	[MonoTODO("Not implemented")]
	public string this[string strProfileName]
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the name of the current user. This class is deprecated.</summary>
	/// <returns>The name of the current user, which is the Passport Unique Identifier (PUID).</returns>
	[MonoTODO("Not implemented")]
	public string Name
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the time, in seconds, since the last ticket was issued or refreshed. This class is deprecated.</summary>
	/// <returns>The time, in seconds, since the last ticket was issued or refreshed.</returns>
	[MonoTODO("Not implemented")]
	public int TicketAge
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the time, in seconds, since a member's logon to the Passport logon server. This class is deprecated.</summary>
	/// <returns>The time, in seconds, since a member's logon to the Passport logon server.</returns>
	[MonoTODO("Not implemented")]
	public int TimeSinceSignIn
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.PassportIdentity" /> class. This class is deprecated.</summary>
	[SecurityPermission(SecurityAction.Demand, UnmanagedCode = true)]
	public PassportIdentity()
	{
	}

	/// <summary>Allows this passport identity to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
	~PassportIdentity()
	{
	}

	/// <summary>Returns a string containing the Login server URL for a member, as well as with optional information sent to the Login server in the query string. This class is deprecated.</summary>
	/// <returns>The Login server URL for a member, as well as optional information sent to the Login server in the query string.</returns>
	public string AuthUrl()
	{
		return AuthUrl(null, -1, -1, null, -1, null, -1, -1);
	}

	/// <summary>Returns a string containing the Login server URL for a member, along with optional information sent to the Login server in the query string. This class is deprecated.</summary>
	/// <param name="strReturnUrl">The URL of the location that the Login server should redirect to after logon is complete. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <returns>The Login server URL for a member, as well as the optional information sent to the Login server in the query string.</returns>
	public string AuthUrl(string strReturnUrl)
	{
		return AuthUrl(strReturnUrl, -1, -1, null, -1, null, -1, -1);
	}

	/// <summary>Returns the authentication server URL for a member. This class is deprecated.</summary>
	/// <param name="strReturnUrl">Sets the URL of the location that the Login server should redirect to after logon is complete. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="fForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter will be used. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strCoBrandedArgs">Specifies variables to be appended to the URL of the Cobranding Template script page that was specified at initial participant registration. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iLangID">Specifies the language in which the required domain authority page should be displayed. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strNameSpace">Specifies the domain in which the Passport should be created. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iKPP">Specifies data collection policies for purposes of Children's Online Privacy Protection Act (COPPA) compliance. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="bUseSecureAuth">Declares whether the actual logon UI should be served HTTPS from the Passport domain authority. Pass -1 to indicate that Passport should use the default value. </param>
	/// <returns>The Login server URL for a member, as well as the optional information sent to the Login server in the query string.</returns>
	public string AuthUrl(string strReturnUrl, int iTimeWindow, bool fForceLogin, string strCoBrandedArgs, int iLangID, string strNameSpace, int iKPP, bool bUseSecureAuth)
	{
		return AuthUrl(strReturnUrl, iTimeWindow, fForceLogin ? 1 : 0, strCoBrandedArgs, iLangID, strNameSpace, iKPP, bUseSecureAuth ? 1 : 0);
	}

	/// <summary>Returns a string containing the Login server URL for a member, along with the optional information sent to the Login server in the query string. This class is deprecated.</summary>
	/// <param name="strReturnUrl">Sets the URL of the location that the Login server should redirect to after logon is complete. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="iForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter will be used. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strCoBrandedArgs">Specifies variables to be appended to the URL of the Cobranding Template script page that was specified at initial participant registration. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iLangID">Specifies the language in which the required domain authority page should be displayed. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strNameSpace">Specifies the domain in which the Passport should be created. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iKPP">Specifies data collection policies for purposes of Children's Online Privacy Protection Act (COPPA) compliance. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="iUseSecureAuth">Declares whether the actual Login UI should be served HTTPS from the Passport domain authority. Pass -1 to indicate that Passport should use the default value. </param>
	/// <returns>The Login server URL for a member, as well as the optional information sent to the Login server in the query string.</returns>
	[MonoTODO("Not implemented")]
	public string AuthUrl(string strReturnUrl, int iTimeWindow, int iForceLogin, string strCoBrandedArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns a string containing the Login server URL for a member, as well as optional information sent to the Login server in the query string. This class is deprecated.</summary>
	/// <returns>The Login server URL for a member, as well as optional information sent to the Login server in the query string.</returns>
	public string AuthUrl2()
	{
		return AuthUrl2(null, -1, -1, null, -1, null, -1, -1);
	}

	/// <summary>Returns a string containing the Login server URL for a member, as well as optional information sent to the Login server in the query string. This class is deprecated.</summary>
	/// <param name="strReturnUrl">The URL of the location that the Login server should redirect to after logon is complete. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <returns>The Login server URL for a member, as well as the optional information sent to the Login server in the query string.</returns>
	public string AuthUrl2(string strReturnUrl)
	{
		return AuthUrl2(strReturnUrl, -1, -1, null, -1, null, -1, -1);
	}

	/// <summary>Returns a string containing the Login server URL for a member, as well as the optional information sent to the Login server in the query string. This class is deprecated.</summary>
	/// <param name="strReturnUrl">Sets the URL of the location that the Login server should redirect to after logon is complete. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="fForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter will be used. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strCoBrandedArgs">Specifies variables to be appended to the URL of the Cobranding Template script page that was specified at initial participant registration. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iLangID">Specifies the language in which the required domain authority page should be displayed. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strNameSpace">Specifies the domain in which the Passport should be created. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iKPP">Specifies data collection policies for purposes of Children's Online Privacy Protection Act (COPPA) compliance. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="bUseSecureAuth">Declares whether the actual logon UI should be served HTTPS from the Passport domain authority. Pass -1 to indicate that Passport should use the default value. </param>
	/// <returns>The Login server URL for a member, as well as the optional information sent to the Login server in the query string.</returns>
	public string AuthUrl2(string strReturnUrl, int iTimeWindow, bool fForceLogin, string strCoBrandedArgs, int iLangID, string strNameSpace, int iKPP, bool bUseSecureAuth)
	{
		return AuthUrl2(strReturnUrl, iTimeWindow, fForceLogin ? 1 : 0, strCoBrandedArgs, iLangID, strNameSpace, iKPP, bUseSecureAuth ? 1 : 0);
	}

	/// <summary>Retrieves a string containing the Login server URL for a member, as well as the optional information sent to the Login server in the query string. This class is deprecated.</summary>
	/// <param name="strReturnUrl">Sets the URL of the location that the Login server should redirect to after logon is complete. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="iForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter will be used. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strCoBrandedArgs">Specifies variables to be appended to the URL of the Cobranding Template script page that was specified at initial participant registration. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iLangID">Specifies the language in which the required domain authority page should be displayed. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strNameSpace">Specifies the domain in which the Passport should be created. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iKPP">Specifies data collection policies for purposes of Children's Online Privacy Protection Act (COPPA) compliance. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="iUseSecureAuth">Declares whether the actual logon UI should be served HTTPS from the Passport domain authority. Pass -1 to indicate that Passport should use the default value. </param>
	/// <returns>The Login server URL for a member, as well as the optional information sent to the Login server in the query string.</returns>
	[MonoTODO("Not implemented")]
	public string AuthUrl2(string strReturnUrl, int iTimeWindow, int iForceLogin, string strCoBrandedArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth)
	{
		throw new NotImplementedException();
	}

	/// <summary>Compresses data. This class is deprecated.</summary>
	/// <param name="strData">The data to be compressed. </param>
	/// <returns>The compressed data.</returns>
	[MonoTODO("Not implemented")]
	public static string Compress(string strData)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the state of a flag indicating if the Passport Manager is in a valid state for encryption. This class is deprecated.</summary>
	/// <returns>
	///     <see langword="true" /> if the key used for encryption and decryption is valid and if the Passport Manager is in a valid state for encryption; otherwise, <see langword="false" />.</returns>
	[MonoTODO("Not implemented")]
	public static bool CryptIsValid()
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets the key being used for Passport encryption and decryption. This class is deprecated.</summary>
	/// <param name="strHost">The host name or IP address. </param>
	/// <returns>An integer result code.</returns>
	[MonoTODO("Not implemented")]
	public static int CryptPutHost(string strHost)
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets the key being used for Passport encryption and decryption by referring to the site-name label assigned to that key when the key was first installed. This class is deprecated.</summary>
	/// <param name="strSite">The site label. </param>
	/// <returns>An integer result code.</returns>
	[MonoTODO("Not implemented")]
	public static int CryptPutSite(string strSite)
	{
		throw new NotImplementedException();
	}

	/// <summary>Decompresses data that has been compressed by the <see cref="M:System.Web.Security.PassportIdentity.Compress(System.String)" /> method. This class is deprecated.</summary>
	/// <param name="strData">The data to be decompressed. </param>
	/// <returns>The decompressed data.</returns>
	[MonoTODO("Not implemented")]
	public static string Decompress(string strData)
	{
		throw new NotImplementedException();
	}

	/// <summary>Decrypts data using the Passport participant key for the current site. This class is deprecated.</summary>
	/// <param name="strData">The data to be decrypted. </param>
	/// <returns>Data decrypted using the Passport participant key for the current site.</returns>
	[MonoTODO("Not implemented")]
	public static string Decrypt(string strData)
	{
		throw new NotImplementedException();
	}

	/// <summary>Encrypts data using the Passport participant key for the current site. This class is deprecated.</summary>
	/// <param name="strData">The data to be encrypted. </param>
	/// <returns>Data encrypted using the Passport participant key for the current site.</returns>
	[MonoTODO("Not implemented")]
	public static string Encrypt(string strData)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets the contents of a registry key under the HKLM\SW\Microsoft\Passport hive. This class is deprecated.</summary>
	/// <param name="strAttribute">The name of the registry key. </param>
	/// <returns>The contents of the registry key.</returns>
	[MonoTODO("Not implemented")]
	public object GetCurrentConfig(string strAttribute)
	{
		throw new NotImplementedException();
	}

	/// <summary>Provides information for a Passport domain by querying the Passport manager for the requested domain attribute. This class is deprecated.</summary>
	/// <param name="strAttribute">The name of the attribute value to retrieve. </param>
	/// <param name="iLCID">The language in which various Passport network pages should be displayed to the member. </param>
	/// <param name="strDomain">The domain authority name to query for an attribute. </param>
	/// <returns>A string representing the requested attribute.</returns>
	[MonoTODO("Not implemented")]
	public string GetDomainAttribute(string strAttribute, int iLCID, string strDomain)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the Passport domain from the member name string. This class is deprecated.</summary>
	/// <param name="strMemberName">The name of the Passport member </param>
	/// <returns>The Passport domain for the specified member.</returns>
	[MonoTODO("Not implemented")]
	public string GetDomainFromMemberName(string strMemberName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Indicates whether the user is authenticated by a central site responsible for Passport authentication. This class is deprecated.</summary>
	/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on to the calling domain. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="bForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter is used. </param>
	/// <param name="bCheckSecure">Enables checking for an encrypted logon. SSL sign-in is not available as an option in the current version Login servers, so the value passed in is ignored at the server. </param>
	/// <returns>
	///     <see langword="true" /> if the user is authenticated by a Passport authority; otherwise, <see langword="false" />.</returns>
	public bool GetIsAuthenticated(int iTimeWindow, bool bForceLogin, bool bCheckSecure)
	{
		return GetIsAuthenticated(iTimeWindow, bForceLogin ? 1 : 0, bCheckSecure ? 1 : 0);
	}

	/// <summary>Indicates whether the user is authenticated by a Passport authority. This class is deprecated.</summary>
	/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on to the calling domain. A value of -1 indicates that Passport should use the default value, 0 represents <see langword="false" />, and 1 represents <see langword="true" />. </param>
	/// <param name="iForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter is used. A value of -1 indicates that Passport should use the default value, 0 represents <see langword="false" />, and 1 represents <see langword="true" />. </param>
	/// <param name="iCheckSecure">Enables checking for an encrypted logon. A value of -1 indicates that Passport should use the default value, 0 represents <see langword="false" />, and 1 represents <see langword="true" />.A value of 10 or 100 for Passport version 2.1 Login servers specify SecureLevel 10 or 100 for the Passport Manager IsAuthenticated method. See the Passport version 2.1 SDK documentation for more information.SSL sign-in is not available as an option for Passport version 1.4 Login servers. The value of <paramref name="iCheckSecure" /> is ignored at the server. </param>
	/// <returns>
	///     <see langword="true" /> if the user is authenticated by a central site responsible for Passport authentication; otherwise, <see langword="false" />.</returns>
	[MonoTODO("Not implemented")]
	public bool GetIsAuthenticated(int iTimeWindow, int iForceLogin, int iCheckSecure)
	{
		throw new NotImplementedException();
	}

	/// <summary>Logs the user on, either by generating a 302 redirect URL or initiating a Passport-aware client authentication exchange. This class is deprecated.</summary>
	/// <returns>A string representing the Passport Login Challenge.</returns>
	public string GetLoginChallenge()
	{
		return GetLoginChallenge(null, -1, -1, null, -1, null, -1, -1, null);
	}

	/// <summary>Logs the user on by outputting the appropriate headers to either a 302 redirect URL or the initiation of a Passport-aware client authentication exchange. This class is deprecated.</summary>
	/// <param name="strReturnUrl">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <returns>A string representing the Passport Login Challenge.</returns>
	public string GetLoginChallenge(string strReturnUrl)
	{
		return GetLoginChallenge(strReturnUrl, -1, -1, null, -1, null, -1, -1, null);
	}

	/// <summary>Logs the user on, either by generating a 302 redirect URL or initiating a Passport-aware client authentication exchange. This class is deprecated.</summary>
	/// <param name="szRetURL">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iTimeWindow">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="fForceLogin">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="szCOBrandArgs">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iLangID">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strNameSpace">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iKPP">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="iUseSecureAuth">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="oExtraParams">See Passport documentation for IPassportManager3.GetLoginChallenge. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <returns>A string representing the Passport Login Challenge.</returns>
	[MonoTODO("Not implemented")]
	public string GetLoginChallenge(string szRetURL, int iTimeWindow, int fForceLogin, string szCOBrandArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth, object oExtraParams)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a specific Passport logon option. This class is deprecated.</summary>
	/// <param name="strOpt">The logon option to query. </param>
	/// <returns>The Passport logon option <paramref name="strOpt" />.</returns>
	[MonoTODO("Not implemented")]
	public object GetOption(string strOpt)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns Passport profile information for the specified profile attribute. This class is deprecated.</summary>
	/// <param name="strProfileName">The Passport profile attribute to return. </param>
	/// <returns>The value of the Passport profile attribute specified by the <paramref name="strProfileName" /> parameter.</returns>
	[MonoTODO("Not implemented")]
	public object GetProfileObject(string strProfileName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Indicates whether a given flag is set in this user's profile. This class is deprecated.</summary>
	/// <param name="iFlagMask">The Passport profile flag to query. </param>
	/// <returns>
	///     <see langword="true" /> if the Passport profile flag <paramref name="iFlagMask" /> is set in this user's profile; otherwise, <see langword="false" />.</returns>
	[MonoTODO("Not implemented")]
	public bool HasFlag(int iFlagMask)
	{
		throw new NotImplementedException();
	}

	/// <summary>Indicates whether a given profile attribute exists in this user's profile. This class is deprecated.</summary>
	/// <param name="strProfile">The Passport profile attribute to query. </param>
	/// <returns>
	///     <see langword="true" /> if the profile attribute <paramref name="strProfile" /> exists in this user's profile; otherwise, <see langword="false" />.</returns>
	[MonoTODO("Not implemented")]
	public bool HasProfile(string strProfile)
	{
		throw new NotImplementedException();
	}

	/// <summary>Indicates whether full consent is granted in this user's profile. This class is deprecated.</summary>
	/// <param name="bNeedFullConsent">
	///       <see langword="true" /> to indicate full consent is required for Passport Authentication; otherwise, <see langword="false" />. </param>
	/// <param name="bNeedBirthdate">
	///       <see langword="true" /> to indicate the user's birthdate is required for Passport Authentication; otherwise, <see langword="false" />. </param>
	/// <returns>
	///     <see langword="true" /> if full consent is granted in this user's profile.</returns>
	[MonoTODO("Not implemented")]
	public bool HaveConsent(bool bNeedFullConsent, bool bNeedBirthdate)
	{
		throw new NotImplementedException();
	}

	/// <summary>Logs the user on, either by generating a 302 redirect URL or initiating a Passport-aware client authentication exchange. This class is deprecated.</summary>
	/// <returns>An integer result code.</returns>
	public int LoginUser()
	{
		return LoginUser(null, -1, -1, null, -1, null, -1, -1, null);
	}

	/// <summary>Logs the user on, either by generating a 302 redirect URL or initiating a Passport-aware client authentication exchange. This class is deprecated.</summary>
	/// <param name="strReturnUrl">The URL to which the Login server should redirect users after sign in is complete. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <returns>An integer result code.</returns>
	public int LoginUser(string strReturnUrl)
	{
		return LoginUser(strReturnUrl, -1, -1, null, -1, null, -1, -1, null);
	}

	/// <summary>Logs the user on, either by generating a 302 redirect URL or by initiating a Passport-aware client authentication exchange. This class is deprecated.</summary>
	/// <param name="szRetURL">The URL to which the Login server should redirect users after sign in is complete. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iTimeWindow">The time value, in seconds. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="fForceLogin">
	///       <see langword="true" /> to have the Login server compare the <paramref name="iTimeWindow" /> parameter against the time since the user last signed in; <see langword="false" /> to have the Login server compare <paramref name="iTimeWindow" /> against the last time the Ticket was refreshed. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="szCOBrandArgs">A string specifying variables to be appended as query string variables to the URL of the participant's Cobranding Template script page. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iLangID">A locale identifier (LCID) specifying the language in which the Login page should be displayed. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strNameSpace">A domain name space to which you want to direct users without Passports to register. The specified name space must appear as a "domain name" entry in the Partner.xml Component Configuration Document (CCD). The typical default name space is "passport.com". Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iKPP">Pass -1 to indicate that Passport should use the default value. This parameter is only relevant when implementing Kids Passport service; however, Kids Passport service cannot currently support use of this method. </param>
	/// <param name="fUseSecureAuth">SSL sign-in is not available as an option in the current version Login servers. Passport Manager methods include SSL sign-in parameters and they may be required for syntax, but they are currently ignored at the server. Check the Passport Web site for updates on the status of SSL sign-in. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="oExtraParams">Name-value pairs to be inserted directly into the challenge authentication header, specifically for Passport-aware authentication interaction. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <returns>An integer result code.</returns>
	public int LoginUser(string szRetURL, int iTimeWindow, bool fForceLogin, string szCOBrandArgs, int iLangID, string strNameSpace, int iKPP, bool fUseSecureAuth, object oExtraParams)
	{
		return LoginUser(szRetURL, iTimeWindow, fForceLogin ? 1 : 0, szCOBrandArgs, iLangID, strNameSpace, iKPP, fUseSecureAuth ? 1 : 0, null);
	}

	/// <summary>Logs the user on, either by generating a 302 redirect URL or initiating a Passport-aware client authentication exchange. This class is deprecated.</summary>
	/// <param name="szRetURL">The URL to which the Login server should redirect users after sign in is complete. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iTimeWindow">The time value, in seconds. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="fForceLogin">Indicates whether the Login server should compare the <paramref name="iTimeWindow" /> parameter against the time since the user last signed in or against the last time the Ticket was refreshed. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="szCOBrandArgs">A string specifying variables to be appended as query string variables to the URL of the participant's Cobranding Template script page. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iLangID">A locale identifier (LCID) specifying the language in which the Login page should be displayed. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strNameSpace">A domain name space to which you want to direct users without Passports to register. The specified name space must appear as a "domain name" entry in the Partner.xml Component Configuration Document (CCD). The typical default name space is "passport.com". Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iKPP">Pass -1 to indicate that Passport should use the default value. This parameter is only relevant when implementing Kids Passport service; however, Kids Passport service cannot currently support use of this method. </param>
	/// <param name="iUseSecureAuth">SSL sign-in is not available as an option in the current version Login servers. Passport Manager methods include SSL sign-in parameters and they may be required for syntax, but they are currently ignored at the server. Check the Passport Web site for updates on the status of SSL sign-in. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="oExtraParams">Name-value pairs to be inserted directly into the challenge authentication header, specifically for Passport-aware authentication interaction. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <returns>An integer result code.</returns>
	[MonoTODO("Not implemented")]
	public int LoginUser(string szRetURL, int iTimeWindow, int fForceLogin, string szCOBrandArgs, int iLangID, string strNameSpace, int iKPP, int iUseSecureAuth, object oExtraParams)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns an HTML fragment containing an image tag for a Passport link. This class is deprecated.</summary>
	/// <returns>An HTML fragment containing an image tag for a Passport link.</returns>
	public string LogoTag()
	{
		return LogoTag(null, -1, -1, null, -1, -1, null, -1, -1);
	}

	/// <summary>Returns an HTML fragment containing an HTML &lt;img&gt; tag for a Passport link. This class is deprecated.</summary>
	/// <param name="strReturnUrl">Sets the URL of the location to which the Login server should redirect members after they log on. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <returns>An HTML fragment containing an image tag for a Passport link.</returns>
	public string LogoTag(string strReturnUrl)
	{
		return LogoTag(strReturnUrl, -1, -1, null, -1, -1, null, -1, -1);
	}

	/// <summary>Returns an HTML fragment containing an HTML &lt;img&gt; tag for a Passport link. This class is deprecated.</summary>
	/// <param name="strReturnUrl">Sets the URL of the location to which the Login server should redirect members after they log on. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="fForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter gets used. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strCoBrandedArgs">Specifies variables to be appended as query string variables to the URL of the participant's Cobranding Template script page. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iLangID">Specifies the language to be used for the logon page that is displayed to the member. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="fSecure">Declares whether this method is being called from an HTTPS (SSL) page. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strNameSpace">Specifies the domain in which the Passport should be created. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iKPP">Specifies data collection policies for purposes of Children's Online Privacy Protection Act (COPPA) compliance. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="bUseSecureAuth">Declares whether the actual logon UI should be served HTTPS from the Passport domain authority. Pass -1 to indicate that Passport should use the default value. </param>
	/// <returns>An HTML fragment containing an image tag for a Passport link.</returns>
	public string LogoTag(string strReturnUrl, int iTimeWindow, bool fForceLogin, string strCoBrandedArgs, int iLangID, bool fSecure, string strNameSpace, int iKPP, bool bUseSecureAuth)
	{
		return LogoTag(strReturnUrl, iTimeWindow, fForceLogin ? 1 : 0, strCoBrandedArgs, iLangID, fSecure ? 1 : 0, strNameSpace, iKPP, bUseSecureAuth ? 1 : 0);
	}

	/// <summary>Returns an HTML fragment containing an HTML &lt;img&gt; tag for a Passport link. This class is deprecated.</summary>
	/// <param name="strReturnUrl">Sets the URL of the location to which the Login server should redirect members after they log on. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="iForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter gets used. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strCoBrandedArgs">Specifies variables to be appended as query string variables to the URL of the participant's Cobranding Template script page. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iLangID">Specifies the language to be used for the logon page that is displayed to the member. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="iSecure">Declares whether this method is being called from an HTTPS (SSL) page. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strNameSpace">Specifies the domain in which the Passport should be created. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iKPP">Specifies data collection policies for purposes of Children's Online Privacy Protection Act (COPPA) compliance. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="iUseSecureAuth">Declares whether the actual logon UI should be served HTTPS from the Passport domain authority. Pass -1 to indicate that Passport should use the default value. </param>
	/// <returns>An HTML fragment containing an image tag for a Passport link.</returns>
	[MonoTODO("Not implemented")]
	public string LogoTag(string strReturnUrl, int iTimeWindow, int iForceLogin, string strCoBrandedArgs, int iLangID, int iSecure, string strNameSpace, int iKPP, int iUseSecureAuth)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns an HTML fragment containing an image tag for a Passport link. This class is deprecated.</summary>
	/// <returns>An HTML fragment containing an image tag for a Passport link.</returns>
	public string LogoTag2()
	{
		return LogoTag2(null, -1, -1, null, -1, -1, null, -1, -1);
	}

	/// <summary>Returns an HTML fragment containing an HTML &lt;img&gt; tag for a Passport link. This class is deprecated.</summary>
	/// <param name="strReturnUrl">Sets the URL of the location to which the Login server should redirect members after they log on. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <returns>An HTML fragment containing an image tag for a Passport link.</returns>
	public string LogoTag2(string strReturnUrl)
	{
		return LogoTag2(strReturnUrl, -1, -1, null, -1, -1, null, -1, -1);
	}

	/// <summary>Returns an HTML fragment containing an HTML &lt;img&gt; tag for a Passport link. This class is deprecated.</summary>
	/// <param name="strReturnUrl">Sets the URL of the location to which the Login server should redirect members after they log on. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="fForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter gets used. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strCoBrandedArgs">Specifies variables to be appended as query string variables to the URL of the participant's Cobranding Template script page. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iLangID">Specifies the language to be used for the logon page that is displayed to the member. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="fSecure">Declares whether this method is being called from an HTTPS (SSL) page. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strNameSpace">Specifies the domain in which the Passport should be created. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iKPP">Specifies data collection policies for purposes of Children's Online Privacy Protection Act (COPPA) compliance. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="bUseSecureAuth">Declares whether the actual logon UI should be served HTTPS from the Passport domain authority. Pass -1 to indicate that Passport should use the default value. </param>
	/// <returns>An HTML fragment containing an image tag for a Passport link.</returns>
	public string LogoTag2(string strReturnUrl, int iTimeWindow, bool fForceLogin, string strCoBrandedArgs, int iLangID, bool fSecure, string strNameSpace, int iKPP, bool bUseSecureAuth)
	{
		return LogoTag2(strReturnUrl, iTimeWindow, fForceLogin ? 1 : 0, strCoBrandedArgs, iLangID, fSecure ? 1 : 0, strNameSpace, iKPP, bUseSecureAuth ? 1 : 0);
	}

	/// <summary>Returns an HTML fragment containing an HTML &lt;img&gt; tag for a Passport link. This class is deprecated.</summary>
	/// <param name="strReturnUrl">Sets the URL of the location to which the Login server should redirect members after they log on. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iTimeWindow">Specifies the interval during which members must have last logged on. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="iForceLogin">Determines how the <paramref name="iTimeWindow" /> parameter gets used. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strCoBrandedArgs">Specifies variables to be appended as query string variables to the URL of the participant's Cobranding Template script page. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iLangID">Specifies the language to be used for the logon page that is displayed to the member. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="iSecure">Declares whether this method is being called from an HTTPS (SSL) page. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strNameSpace">Specifies the domain in which the Passport should be created. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iKPP">Specifies data collection policies for purposes of Children's Online Privacy Protection Act (COPPA) compliance. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="iUseSecureAuth">Declares whether the actual logon UI should be served HTTPS from the Passport domain authority. Pass -1 to indicate that Passport should use the default value. </param>
	/// <returns>An HTML fragment containing an image tag for a Passport link.</returns>
	[MonoTODO("Not implemented")]
	public string LogoTag2(string strReturnUrl, int iTimeWindow, int iForceLogin, string strCoBrandedArgs, int iLangID, int iSecure, string strNameSpace, int iKPP, int iUseSecureAuth)
	{
		throw new NotImplementedException();
	}

	/// <summary>Returns the Passport logout URL string. This class is deprecated.</summary>
	/// <returns>The Passport logout URL string.</returns>
	public string LogoutURL()
	{
		return LogoutURL(null, null, -1, null, -1);
	}

	/// <summary>Returns the Passport logout URL string using the specified parameters. This class is deprecated.</summary>
	/// <param name="szReturnURL">See IPassportManager3.LogoutUrl for more details. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="szCOBrandArgs">See IPassportManager3.LogoutUrl for more details. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iLangID">See IPassportManager3.LogoutUrl for more details. Pass -1 to indicate that Passport should use the default value. </param>
	/// <param name="strDomain">See IPassportManager3.LogoutUrl for more details. Pass <see langword="null" /> to indicate that Passport should use the default value. </param>
	/// <param name="iUseSecureAuth">See IPassportManager3.LogoutUrl for more details. Pass -1 to indicate that Passport should use the default value. </param>
	/// <returns>The Passport logout URL string.</returns>
	[MonoTODO("Not implemented")]
	public string LogoutURL(string szReturnURL, string szCOBrandArgs, int iLangID, string strDomain, int iUseSecureAuth)
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets a specific Passport logon option. This class is deprecated.</summary>
	/// <param name="strOpt">The option to set. </param>
	/// <param name="vOpt">The value to set. </param>
	[MonoTODO("Not implemented")]
	public void SetOption(string strOpt, object vOpt)
	{
		throw new NotImplementedException();
	}

	/// <summary>Logs off the given Passport member from the current session. This class is deprecated.</summary>
	/// <param name="strSignOutDotGifFileName">An HTML fragment containing an image for the user to click on to sign out. </param>
	[MonoTODO("Not implemented")]
	public static void SignOut(string strSignOutDotGifFileName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets information on a specific attribute of the Passport authentication ticket. This class is deprecated.</summary>
	/// <param name="strAttribute">A string identifying the Passport authentication ticket to return. </param>
	/// <returns>An object representing an attribute of the Passport authentication ticket.</returns>
	[MonoTODO("Not implemented")]
	public object Ticket(string strAttribute)
	{
		throw new NotImplementedException();
	}

	/// <summary>Releases all resources used by the <see cref="T:System.Web.Security.PassportIdentity" /> class. This class is deprecated.</summary>
	void IDisposable.Dispose()
	{
	}
}
