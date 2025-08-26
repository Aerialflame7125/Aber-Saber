using System.Collections.Specialized;
using System.IO;
using System.Security.Permissions;
using System.Security.Principal;
using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.Security;

/// <summary>Represents security information for the current HTTP request, including role membership. This class cannot be inherited.</summary>
[Serializable]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public class RolePrincipal : IPrincipal
{
	private IIdentity _identity;

	private bool _listChanged;

	private string[] _cachedArray;

	private HybridDictionary _cachedRoles;

	private readonly string _providerName;

	private int _version = 1;

	private string _cookiePath;

	private DateTime _issueDate;

	private DateTime _expireDate;

	/// <summary>Gets a value indicating whether the list of role names cached with the <see cref="T:System.Web.Security.RolePrincipal" /> object has been modified.</summary>
	/// <returns>
	///     <see langword="true" /> if the list of role names cached with the <see cref="T:System.Web.Security.RolePrincipal" /> object has been modified; otherwise, <see langword="false" />.</returns>
	public bool CachedListChanged => _listChanged;

	/// <summary>Gets the path for the cached role names cookie.</summary>
	/// <returns>The path of the cookie where role names are cached. The default is /.</returns>
	public string CookiePath => _cookiePath;

	/// <summary>Gets a value indicating whether the roles cookie has expired.</summary>
	/// <returns>
	///     <see langword="true" /> if the roles cookie has expired; otherwise, <see langword="false" />.</returns>
	public bool Expired => ExpireDate < DateTime.Now;

	/// <summary>Gets the date and time when the roles cookie will expire.</summary>
	/// <returns>The <see cref="T:System.DateTime" /> value when the roles cookie will expire.</returns>
	public DateTime ExpireDate => _expireDate;

	/// <summary>Gets the security identity for the current HTTP request.</summary>
	/// <returns>The security identity for the current HTTP request.</returns>
	public IIdentity Identity => _identity;

	/// <summary>Gets a value indicating whether the list of roles for the user has been cached in a cookie.</summary>
	/// <returns>
	///     <see langword="true" /> if role names are cached in a cookie; otherwise, <see langword="false" />.</returns>
	public bool IsRoleListCached
	{
		get
		{
			if (_cachedRoles != null)
			{
				return RoleManagerConfig.CacheRolesInCookie;
			}
			return false;
		}
	}

	/// <summary>Gets the date and time that the roles cookie was issued.</summary>
	/// <returns>The <see cref="T:System.DateTime" /> that the roles cookie was issued.</returns>
	public DateTime IssueDate => _issueDate;

	/// <summary>Gets the name of the role provider that stores and retrieves role information for the user.</summary>
	/// <returns>The name of the role provider that stores and retrieves role information for the user.</returns>
	public string ProviderName
	{
		get
		{
			if (!string.IsNullOrEmpty(_providerName))
			{
				return _providerName;
			}
			return Provider.Name;
		}
	}

	/// <summary>Gets the version number of the roles cookie.</summary>
	/// <returns>The version number of the roles cookie.</returns>
	public int Version => _version;

	private RoleProvider Provider
	{
		get
		{
			if (string.IsNullOrEmpty(_providerName))
			{
				return Roles.Provider;
			}
			return Roles.Providers[_providerName];
		}
	}

	private RoleManagerSection RoleManagerConfig => (RoleManagerSection)WebConfigurationManager.GetSection("system.web/roleManager");

	private MachineKeySection MachineConfig => (MachineKeySection)WebConfigurationManager.GetSection("system.web/machineKey");

	/// <summary>Instantiates a <see cref="T:System.Web.Security.RolePrincipal" /> object for the specified <paramref name="identity" />.</summary>
	/// <param name="identity">The user identity to create the <see cref="T:System.Web.Security.RolePrincipal" /> for.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="identity" /> is <see langword="null" />.</exception>
	public RolePrincipal(IIdentity identity)
	{
		if (identity == null)
		{
			throw new ArgumentNullException("identity");
		}
		_identity = identity;
		_cookiePath = RoleManagerConfig.CookiePath;
		_issueDate = DateTime.Now;
		_expireDate = _issueDate.Add(RoleManagerConfig.CookieTimeout);
	}

	/// <summary>Instantiates a <see cref="T:System.Web.Security.RolePrincipal" /> object for the specified <paramref name="identity" /> with role information from the specified <paramref name="encryptedTicket" />.</summary>
	/// <param name="identity">The user identity to create the <see cref="T:System.Web.Security.RolePrincipal" /> for.</param>
	/// <param name="encryptedTicket">A string that contains encrypted role information.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="identity" /> is <see langword="null" />.-or-
	///         <paramref name="encryptedTicket" /> is <see langword="null" />.</exception>
	public RolePrincipal(IIdentity identity, string encryptedTicket)
		: this(identity)
	{
		DecryptTicket(encryptedTicket);
	}

	/// <summary>Instantiates a <see cref="T:System.Web.Security.RolePrincipal" /> object for the specified <paramref name="identity" /> using the specified <paramref name="providerName" />.</summary>
	/// <param name="providerName">The name of the role provider for the user.</param>
	/// <param name="identity">The user identity to create the <see cref="T:System.Web.Security.RolePrincipal" /> for.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="identity" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="providerName" /> is <see langword="null" />.-or-
	///         <paramref name="providerName" /> refers to a role provider that does not exist in the configuration for the application.</exception>
	public RolePrincipal(string providerName, IIdentity identity)
		: this(identity)
	{
		if (providerName == null)
		{
			throw new ArgumentNullException("providerName");
		}
		_providerName = providerName;
	}

	/// <summary>Instantiates a <see cref="T:System.Web.Security.RolePrincipal" /> object for the specified <paramref name="identity" /> using the specified <paramref name="providerName" /> and role information from the specified <paramref name="encryptedTicket" />.</summary>
	/// <param name="providerName">The name of the role provider for the user.</param>
	/// <param name="identity">The user identity to create the <see cref="T:System.Web.Security.RolePrincipal" /> for.</param>
	/// <param name="encryptedTicket">A string that contains encrypted role information.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="identity" /> is <see langword="null" />.-or-
	///         <paramref name="encryptedTicket" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="providerName" /> is <see langword="null" />.-or-
	///         <paramref name="providerName" /> refers to a role provider that does not exist in the configuration for the application.</exception>
	public RolePrincipal(string providerName, IIdentity identity, string encryptedTicket)
		: this(providerName, identity)
	{
		DecryptTicket(encryptedTicket);
	}

	/// <summary>Gets a list of roles that the <see cref="T:System.Web.Security.RolePrincipal" /> is a member of.</summary>
	/// <returns>The list of roles that the <see cref="T:System.Web.Security.RolePrincipal" /> is a member of.</returns>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see cref="P:System.Web.Security.RolePrincipal.Identity" /> property is <see langword="null" />.</exception>
	public string[] GetRoles()
	{
		if (!_identity.IsAuthenticated)
		{
			return new string[0];
		}
		if (!IsRoleListCached || Expired)
		{
			_cachedArray = Provider.GetRolesForUser(_identity.Name);
			_cachedRoles = new HybridDictionary(caseInsensitive: true);
			string[] cachedArray = _cachedArray;
			foreach (string text in cachedArray)
			{
				_cachedRoles.Add(text, text);
			}
			_listChanged = true;
		}
		return _cachedArray;
	}

	/// <summary>Gets a value indicating whether the user represented by the <see cref="T:System.Web.Security.RolePrincipal" /> is in the specified role.</summary>
	/// <param name="role">The role to search for.</param>
	/// <returns>
	///     <see langword="true" /> if user represented by the <see cref="T:System.Web.Security.RolePrincipal" /> is in the specified role; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">The <see cref="P:System.Web.Security.RolePrincipal.Identity" /> property is <see langword="null" />.</exception>
	public bool IsInRole(string role)
	{
		if (!_identity.IsAuthenticated)
		{
			return false;
		}
		GetRoles();
		return _cachedRoles[role] != null;
	}

	/// <summary>Returns the role information cached with the <see cref="T:System.Web.Security.RolePrincipal" /> object encrypted based on the <see cref="P:System.Web.Security.Roles.CookieProtectionValue" />.</summary>
	/// <returns>The role information cached with the <see cref="T:System.Web.Security.RolePrincipal" /> object encrypted based on the <see cref="P:System.Web.Security.Roles.CookieProtectionValue" />.</returns>
	public string ToEncryptedTicket()
	{
		string text = string.Join(",", GetRoles());
		string cookiePath = RoleManagerConfig.CookiePath;
		int capacity = text.Length + cookiePath.Length + 64;
		if (_cachedArray.Length > Roles.MaxCachedResults)
		{
			return null;
		}
		MemoryStream memoryStream = new MemoryStream(capacity);
		BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
		binaryWriter.Write(Version);
		binaryWriter.Write(DateTime.Now.Ticks);
		binaryWriter.Write(_expireDate.Ticks);
		binaryWriter.Write(cookiePath);
		binaryWriter.Write(text);
		CookieProtection cookieProtection = RoleManagerConfig.CookieProtection;
		byte[] array = memoryStream.GetBuffer();
		switch (cookieProtection)
		{
		case CookieProtection.All:
			array = MachineKeySectionUtils.EncryptSign(MachineConfig, array);
			break;
		case CookieProtection.Encryption:
			array = MachineKeySectionUtils.Encrypt(MachineConfig, array);
			break;
		case CookieProtection.Validation:
			array = MachineKeySectionUtils.Sign(MachineConfig, array);
			break;
		}
		return GetBase64FromBytes(array, 0, array.Length);
	}

	private void DecryptTicket(string encryptedTicket)
	{
		if (encryptedTicket == null || encryptedTicket == string.Empty)
		{
			throw new ArgumentException("Invalid encrypted ticket", "encryptedTicket");
		}
		byte[] bytesFromBase = GetBytesFromBase64(encryptedTicket);
		byte[] array = null;
		switch (RoleManagerConfig.CookieProtection)
		{
		case CookieProtection.All:
			array = MachineKeySectionUtils.VerifyDecrypt(MachineConfig, bytesFromBase);
			break;
		case CookieProtection.Encryption:
			array = MachineKeySectionUtils.Decrypt(MachineConfig, bytesFromBase);
			break;
		case CookieProtection.Validation:
			array = MachineKeySectionUtils.Verify(MachineConfig, bytesFromBase);
			break;
		}
		if (array == null)
		{
			throw new HttpException("ticket validation failed");
		}
		BinaryReader binaryReader = new BinaryReader(new MemoryStream(array));
		_version = binaryReader.ReadInt32();
		_issueDate = new DateTime(binaryReader.ReadInt64());
		_expireDate = new DateTime(binaryReader.ReadInt64());
		_cookiePath = binaryReader.ReadString();
		string decryptedRoles = binaryReader.ReadString();
		if (!Expired)
		{
			InitializeRoles(decryptedRoles);
			if (Roles.CookieSlidingExpiration && _expireDate - DateTime.Now < TimeSpan.FromTicks(RoleManagerConfig.CookieTimeout.Ticks / 2))
			{
				_issueDate = DateTime.Now;
				_expireDate = DateTime.Now.Add(RoleManagerConfig.CookieTimeout);
				SetDirty();
			}
		}
		else
		{
			_issueDate = DateTime.Now;
			_expireDate = _issueDate.Add(RoleManagerConfig.CookieTimeout);
		}
	}

	private void InitializeRoles(string decryptedRoles)
	{
		_cachedArray = decryptedRoles.Split(',');
		_cachedRoles = new HybridDictionary(caseInsensitive: true);
		string[] cachedArray = _cachedArray;
		foreach (string text in cachedArray)
		{
			_cachedRoles.Add(text, text);
		}
	}

	/// <summary>Marks the cached role list as having been changed.</summary>
	public void SetDirty()
	{
		_listChanged = true;
		_cachedRoles = null;
		_cachedArray = null;
	}

	private static string GetBase64FromBytes(byte[] bytes, int offset, int len)
	{
		return Convert.ToBase64String(bytes, offset, len);
	}

	private static byte[] GetBytesFromBase64(string base64String)
	{
		return Convert.FromBase64String(base64String);
	}
}
