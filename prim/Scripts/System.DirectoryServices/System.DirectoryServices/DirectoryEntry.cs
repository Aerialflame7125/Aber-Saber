using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.DirectoryServices.Design;
using System.Runtime.InteropServices;
using Novell.Directory.Ldap;
using Novell.Directory.Ldap.Utilclass;

namespace System.DirectoryServices;

/// <summary>The <see cref="T:System.DirectoryServices.DirectoryEntry" /> class encapsulates a node or object in the Active Directory Domain Services hierarchy.</summary>
[TypeConverter(typeof(DirectoryEntryConverter))]
public class DirectoryEntry : Component
{
	private static readonly string DEFAULT_LDAP_HOST = "System.DirectoryServices.DefaultLdapHost";

	private static readonly string DEFAULT_LDAP_PORT = "System.DirectoryServices.DefaultLdapPort";

	private LdapConnection _conn;

	private AuthenticationTypes _AuthenticationType;

	private DirectoryEntries _Children;

	private string _Fdn;

	private string _Path = "";

	private string _Name;

	private DirectoryEntry _Parent;

	private string _Username;

	private string _Password;

	private PropertyCollection _Properties;

	private string _SchemaClassName;

	private bool _Nflag;

	private bool _usePropertyCache = true;

	private bool _inPropertiesLoading;

	internal string Fdn
	{
		get
		{
			if (_Fdn == null)
			{
				string dN = new LdapUrl(ADsPath).getDN();
				if (dN != null)
				{
					_Fdn = dN;
				}
				else
				{
					_Fdn = string.Empty;
				}
			}
			return _Fdn;
		}
	}

	internal LdapConnection conn
	{
		get
		{
			if (_conn == null)
			{
				InitBlock();
			}
			return _conn;
		}
		set
		{
			_conn = value;
		}
	}

	internal bool Nflag
	{
		get
		{
			return _Nflag;
		}
		set
		{
			_Nflag = value;
		}
	}

	/// <summary>Gets or sets the type of authentication to use.</summary>
	/// <returns>One of the <see cref="T:System.DirectoryServices.AuthenticationTypes" /> values.</returns>
	[DSDescription("Type of authentication to use while Binding to Ldap server")]
	[DefaultValue(AuthenticationTypes.None)]
	public AuthenticationTypes AuthenticationType
	{
		get
		{
			return _AuthenticationType;
		}
		set
		{
			_AuthenticationType = value;
		}
	}

	/// <summary>Gets the child entries of this node in the Active Directory Domain Services hierarchy.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntries" /> object containing the child entries of this node in the Active Directory Domain Services hierarchy.</returns>
	[DSDescription("Child entries of this node")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public DirectoryEntries Children
	{
		get
		{
			_Children = new DirectoryEntries(ADsPath, conn);
			return _Children;
		}
	}

	/// <summary>Gets the GUID of the <see cref="T:System.DirectoryServices.DirectoryEntry" />.</summary>
	/// <returns>A <see cref="T:System.Guid" /> structure that represents the GUID of the <see cref="T:System.DirectoryServices.DirectoryEntry" />.</returns>
	[DSDescription("A globally unique identifier for this DirectoryEntry")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[System.MonoTODO]
	public Guid Guid
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the name of the object as named with the underlying directory service.</summary>
	/// <returns>The name of the object as named with the underlying directory service.</returns>
	[DSDescription("The name of the object as named with the underlying directory")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public string Name
	{
		get
		{
			if (_Name == null)
			{
				if (!CheckEntry(conn, ADsPath))
				{
					throw new SystemException("There is no such object on the server");
				}
				InitEntry();
			}
			return _Name;
		}
	}

	/// <summary>Gets this entry's parent in the Active Directory Domain Services hierarchy.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the parent of this entry.</returns>
	[DSDescription("This entry's parent in the Ldap Directory hierarchy.")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public DirectoryEntry Parent
	{
		get
		{
			if (_Parent == null)
			{
				if (!CheckEntry(conn, ADsPath))
				{
					throw new SystemException("There is no such object on the server");
				}
				InitEntry();
			}
			return _Parent;
		}
	}

	/// <summary>Gets the GUID of the <see cref="T:System.DirectoryServices.DirectoryEntry" />, as returned from the provider.</summary>
	/// <returns>A <see cref="T:System.Guid" /> structure that represents the GUID of the <see cref="T:System.DirectoryServices.DirectoryEntry" />, as returned from the provider.</returns>
	[DSDescription("The globally unique identifier of the DirectoryEntry, as returned from the provider")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	[System.MonoTODO]
	public string NativeGuid
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets the native Active Directory Service Interfaces (ADSI) object.</summary>
	/// <returns>The native ADSI object.</returns>
	[DSDescription("The native Active Directory Service Interfaces (ADSI) object.")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public object NativeObject
	{
		[System.MonoTODO]
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets the security descriptor for this entry.</summary>
	/// <returns>An <see cref="T:System.DirectoryServices.ActiveDirectorySecurity" /> object that represents the security descriptor for this directory entry.</returns>
	[DSDescription("An ActiveDirectorySecurity object that represents the security descriptor for this directory entry.")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public ActiveDirectorySecurity ObjectSecurity
	{
		[System.MonoTODO]
		get
		{
			throw new NotImplementedException();
		}
		[System.MonoTODO]
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Gets or sets a value indicating whether the cache should be committed after each operation.</summary>
	/// <returns>
	///   <see langword="true" /> if the cache should not be committed after each operation; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DSDescription("Determines if a cache should be used.")]
	[DefaultValue(true)]
	public bool UsePropertyCache
	{
		get
		{
			return _usePropertyCache;
		}
		set
		{
			_usePropertyCache = value;
		}
	}

	/// <summary>Gets the provider-specific options for this entry.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntryConfiguration" /> object that contains the provider-specific options for this entry.</returns>
	[DSDescription("The provider-specific options for this entry.")]
	[Browsable(false)]
	[System.MonoTODO]
	public DirectoryEntryConfiguration Options
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>Sets the password to use when authenticating the client.</summary>
	/// <returns>The password to use when authenticating the client.</returns>
	[DSDescription("The password to use when authenticating the client.")]
	[DefaultValue(null)]
	[Browsable(false)]
	public string Password
	{
		get
		{
			return _Password;
		}
		set
		{
			_Password = value;
		}
	}

	/// <summary>Gets or sets the user name to use when authenticating the client.</summary>
	/// <returns>The user name to use when authenticating the client.</returns>
	[DSDescription("The user name to use when authenticating the client.")]
	[DefaultValue(null)]
	[Browsable(false)]
	[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string Username
	{
		get
		{
			return _Username;
		}
		set
		{
			_Username = value;
		}
	}

	/// <summary>Gets or sets the path for this <see cref="T:System.DirectoryServices.DirectoryEntry" />.</summary>
	/// <returns>The path of this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object. The default is an empty string ("").</returns>
	[DSDescription("The path for this DirectoryEntry.")]
	[DefaultValue("")]
	[RecommendedAsConfigurable(true)]
	[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string Path
	{
		get
		{
			return _Path;
		}
		set
		{
			if (value == null)
			{
				_Path = string.Empty;
			}
			else
			{
				_Path = value;
			}
		}
	}

	internal string ADsPath
	{
		get
		{
			if (Path == null || Path == string.Empty)
			{
				DirectoryEntry directoryEntry = new DirectoryEntry();
				directoryEntry.InitToRootDse(null, -1);
				string text = (string)directoryEntry.Properties["defaultNamingContext"].Value;
				if (text == null)
				{
					text = (string)directoryEntry.Properties["namingContexts"].Value;
				}
				return new LdapUrl(DefaultHost, DefaultPort, text).ToString();
			}
			return Path;
		}
	}

	/// <summary>Gets the Active Directory Domain Services properties for this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.PropertyCollection" /> object that contains the properties that are set on this entry.</returns>
	[DSDescription("Properties set on this object.")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public PropertyCollection Properties => GetProperties(forceLoad: true);

	/// <summary>Gets the name of the schema class for this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object.</summary>
	/// <returns>The name of the schema class for this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object.</returns>
	[DSDescription("The name of the schema used for this DirectoryEntry.")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public string SchemaClassName
	{
		get
		{
			if (_SchemaClassName == null)
			{
				_SchemaClassName = FindAttrValue("structuralObjectClass");
			}
			return _SchemaClassName;
		}
	}

	/// <summary>Gets the schema object for this entry.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the schema class for this entry.</returns>
	[DSDescription("The current schema directory entry.")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
	[Browsable(false)]
	public DirectoryEntry SchemaEntry
	{
		[System.MonoTODO]
		get
		{
			throw new NotImplementedException();
		}
	}

	private string DefaultHost
	{
		get
		{
			string text = (string)AppDomain.CurrentDomain.GetData(DEFAULT_LDAP_HOST);
			if (text == null)
			{
				NameValueCollection nameValueCollection = (NameValueCollection)ConfigurationSettings.GetConfig("mainsoft.directoryservices/settings");
				if (nameValueCollection != null)
				{
					text = nameValueCollection["servername"];
				}
				if (text == null)
				{
					text = "localhost";
				}
				AppDomain.CurrentDomain.SetData(DEFAULT_LDAP_HOST, text);
			}
			return text;
		}
	}

	private int DefaultPort
	{
		get
		{
			string text = (string)AppDomain.CurrentDomain.GetData(DEFAULT_LDAP_PORT);
			if (text == null)
			{
				NameValueCollection nameValueCollection = (NameValueCollection)ConfigurationSettings.GetConfig("mainsoft.directoryservices/settings");
				if (nameValueCollection != null)
				{
					text = nameValueCollection["port"];
				}
				if (text == null)
				{
					text = "389";
				}
				AppDomain.CurrentDomain.SetData(DEFAULT_LDAP_PORT, text);
			}
			return int.Parse(text);
		}
	}

	private void InitBlock()
	{
		try
		{
			_conn = new LdapConnection();
			LdapUrl ldapUrl = new LdapUrl(ADsPath);
			_conn.Connect(ldapUrl.Host, ldapUrl.Port);
			_conn.Bind(Username, Password, (Novell.Directory.Ldap.AuthenticationTypes)AuthenticationType);
		}
		catch (LdapException ex)
		{
			throw ex;
		}
		catch (Exception ex2)
		{
			throw ex2;
		}
	}

	private void InitEntry()
	{
		LdapUrl ldapUrl = new LdapUrl(ADsPath);
		string dN = ldapUrl.getDN();
		if (dN != null)
		{
			if (string.Compare(dN, "rootDSE", ignoreCase: true) == 0)
			{
				InitToRootDse(ldapUrl.Host, ldapUrl.Port);
				return;
			}
			DN dN2 = new DN(dN);
			string[] array = dN2.explodeDN(noTypes: false);
			_Name = array[0];
			_Parent = new DirectoryEntry(conn);
			_Parent.Path = GetLdapUrlString(ldapUrl.Host, ldapUrl.Port, dN2.Parent.ToString());
		}
		else
		{
			_Name = ldapUrl.Host + ":" + ldapUrl.Port;
			_Parent = new DirectoryEntry(conn);
			_Parent.Path = "Ldap:";
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryEntry" /> class.</summary>
	public DirectoryEntry()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryEntry" /> class that binds to the specified native Active Directory Domain Services object.</summary>
	/// <param name="adsObject">The name of the native Active Directory Domain Services object to bind to.</param>
	public DirectoryEntry(object adsObject)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryEntry" /> class that binds this instance to the node in Active Directory Domain Services located at the specified path.</summary>
	/// <param name="path">The path at which to bind the <see cref="M:System.DirectoryServices.DirectoryEntry.#ctor(System.String)" /> to the directory. The <see cref="P:System.DirectoryServices.DirectoryEntry.Path" /> property is initialized to this value.</param>
	public DirectoryEntry(string path)
	{
		_Path = path;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryEntry" /> class.</summary>
	/// <param name="path">The path of this <see cref="T:System.DirectoryServices.DirectoryEntry" />. The <see cref="P:System.DirectoryServices.DirectoryEntry.Path" /> property is initialized to this value.</param>
	/// <param name="username">The user name to use when authenticating the client. The <see cref="P:System.DirectoryServices.DirectoryEntry.Username" /> property is initialized to this value.</param>
	/// <param name="password">The password to use when authenticating the client. The <see cref="P:System.DirectoryServices.DirectoryEntry.Password" /> property is initialized to this value.</param>
	public DirectoryEntry(string path, string username, string password)
	{
		_Path = path;
		_Username = username;
		_Password = password;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectoryEntry" /> class.</summary>
	/// <param name="path">The path of this <see cref="T:System.DirectoryServices.DirectoryEntry" />. The <see cref="P:System.DirectoryServices.DirectoryEntry.Path" /> property is initialized to this value.</param>
	/// <param name="username">The user name to use when authenticating the client. The <see cref="P:System.DirectoryServices.DirectoryEntry.Username" /> property is initialized to this value.</param>
	/// <param name="password">The password to use when authenticating the client. The <see cref="P:System.DirectoryServices.DirectoryEntry.Password" /> property is initialized to this value.</param>
	/// <param name="authenticationType">One of the <see cref="T:System.DirectoryServices.AuthenticationTypes" /> values. The <see cref="P:System.DirectoryServices.DirectoryEntry.AuthenticationType" /> property is initialized to this value.</param>
	public DirectoryEntry(string path, string username, string password, AuthenticationTypes authenticationType)
	{
		_Path = path;
		_Username = username;
		_Password = password;
		_AuthenticationType = authenticationType;
	}

	internal DirectoryEntry(LdapConnection lconn)
	{
		conn = lconn;
	}

	private void InitToRootDse(string host, int port)
	{
		if (host == null)
		{
			host = DefaultHost;
		}
		if (port < 0)
		{
			port = DefaultPort;
		}
		LdapUrl ldapUrl = new LdapUrl(host, port, string.Empty);
		string[] propertiesToLoad = new string[2] { "+", "*" };
		SearchResult searchResult = new DirectorySearcher(new DirectoryEntry(ldapUrl.ToString(), Username, Password, AuthenticationType), null, propertiesToLoad, SearchScope.Base).FindOne();
		PropertyCollection propertyCollection = new PropertyCollection();
		foreach (string propertyName in searchResult.Properties.PropertyNames)
		{
			IEnumerator enumerator2 = searchResult.Properties[propertyName].GetEnumerator();
			if (enumerator2 == null)
			{
				continue;
			}
			while (enumerator2.MoveNext())
			{
				if (string.Compare(propertyName, "ADsPath", ignoreCase: true) != 0)
				{
					propertyCollection[propertyName].Add(enumerator2.Current);
				}
			}
		}
		SetProperties(propertyCollection);
		_Name = "rootDSE";
	}

	private void SetProperties(PropertyCollection pcoll)
	{
		_Properties = pcoll;
	}

	private PropertyCollection GetProperties(bool forceLoad)
	{
		if (_Properties == null)
		{
			PropertyCollection properties = new PropertyCollection(this);
			if (forceLoad && !Nflag)
			{
				LoadProperties(properties, null);
			}
			_Properties = properties;
		}
		return _Properties;
	}

	private void LoadProperties(PropertyCollection properties, string[] propertyNames)
	{
		_inPropertiesLoading = true;
		try
		{
			LdapSearchResults ldapSearchResults = conn.Search(Fdn, 0, "objectClass=*", propertyNames, typesOnly: false);
			if (!ldapSearchResults.hasMore())
			{
				return;
			}
			LdapEntry ldapEntry = ldapSearchResults.next();
			string[] array = null;
			int num = 0;
			if (propertyNames != null)
			{
				num = propertyNames.Length;
				array = new string[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = propertyNames[i].ToLower();
				}
			}
			foreach (LdapAttribute item in ldapEntry.getAttributeSet())
			{
				string name = item.Name;
				if (propertyNames == null || Array.IndexOf(array, name.ToLower()) != -1)
				{
					properties[name].Value = null;
					properties[name].AddRange(item.StringValueArray);
					properties[name].Mbit = false;
				}
			}
		}
		finally
		{
			_inPropertiesLoading = false;
		}
	}

	private string FindAttrValue(string attrName)
	{
		string result = null;
		string[] attrs = new string[1] { attrName };
		LdapSearchResults ldapSearchResults = conn.Search(Fdn, 0, "objectClass=*", attrs, typesOnly: false);
		if (ldapSearchResults.hasMore())
		{
			LdapEntry ldapEntry = null;
			try
			{
				ldapEntry = ldapSearchResults.next();
			}
			catch (LdapException ex)
			{
				throw ex;
			}
			result = ldapEntry.getAttribute(attrName).StringValue;
		}
		return result;
	}

	private void ModEntry(LdapModification[] mods)
	{
		try
		{
			conn.Modify(Fdn, mods);
		}
		catch (LdapException ex)
		{
			throw ex;
		}
	}

	private static bool CheckEntry(LdapConnection lconn, string epath)
	{
		string text = new LdapUrl(epath).getDN();
		if (text == null)
		{
			text = string.Empty;
		}
		else if (string.Compare(text, "rootDSE", ignoreCase: true) == 0)
		{
			return true;
		}
		string[] attrs = new string[1] { "objectClass" };
		try
		{
			LdapSearchResults ldapSearchResults = lconn.Search(text, 0, "objectClass=*", attrs, typesOnly: false);
			if (ldapSearchResults.hasMore())
			{
				try
				{
					ldapSearchResults.next();
				}
				catch (LdapException ex)
				{
					throw ex;
				}
			}
		}
		catch (LdapException ex2)
		{
			if (ex2.ResultCode == 32)
			{
				return false;
			}
			throw ex2;
		}
		catch (Exception ex3)
		{
			throw ex3;
		}
		return true;
	}

	/// <summary>Closes the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object and releases any system resources that are associated with this component.</summary>
	public void Close()
	{
		if (_conn != null && _conn.Connected)
		{
			_conn.Disconnect();
		}
	}

	/// <summary>Creates a copy of this entry as a child of the specified parent.</summary>
	/// <param name="newParent">The distinguished name of the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that will be the parent for the copy that is being created.</param>
	/// <returns>A <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that represents the copy of this entry as a child of the new parent.</returns>
	/// <exception cref="T:System.InvalidOperationException">The specified <see cref="T:System.DirectoryServices.DirectoryEntry" /> is not a container.</exception>
	[System.MonoTODO]
	public DirectoryEntry CopyTo(DirectoryEntry newParent)
	{
		throw new NotImplementedException();
	}

	/// <summary>Deletes this entry and its entire subtree from the Active Directory Domain Services hierarchy.</summary>
	/// <exception cref="T:System.InvalidOperationException">The specified <see cref="T:System.DirectoryServices.DirectoryEntry" /> is not a container.</exception>
	public void DeleteTree()
	{
		IEnumerator enumerator = Children.GetEnumerator();
		while (enumerator.MoveNext())
		{
			DirectoryEntry directoryEntry = (DirectoryEntry)enumerator.Current;
			conn.Delete(directoryEntry.Fdn);
		}
		conn.Delete(Fdn);
	}

	/// <summary>Determines if the specified path represents an actual entry in the directory service.</summary>
	/// <param name="path">The path of the entry to verify.</param>
	/// <returns>
	///   <see langword="true" /> if the specified path represents a valid entry in the directory service; otherwise, <see langword="false" />.</returns>
	public static bool Exists(string path)
	{
		LdapConnection ldapConnection = new LdapConnection();
		LdapUrl ldapUrl = new LdapUrl(path);
		ldapConnection.Connect(ldapUrl.Host, ldapUrl.Port);
		ldapConnection.Bind("", "");
		if (CheckEntry(ldapConnection, path))
		{
			return true;
		}
		return false;
	}

	/// <summary>Moves this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object to the specified parent.</summary>
	/// <param name="newParent">The parent to which you want to move this entry.</param>
	/// <exception cref="T:System.InvalidOperationException">The specified <see cref="T:System.DirectoryServices.DirectoryEntry" /> is not a container.</exception>
	public void MoveTo(DirectoryEntry newParent)
	{
		string fdn = Parent.Fdn;
		conn.Rename(Fdn, Name, newParent.Fdn, deleteOldRdn: true);
		Path = Path.Replace(fdn, newParent.Fdn);
		RefreshEntry();
	}

	/// <summary>Moves this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object to the specified parent and changes its name to the specified value.</summary>
	/// <param name="newParent">The parent to which you want to move this entry.</param>
	/// <param name="newName">The new name of this entry.</param>
	/// <exception cref="T:System.InvalidOperationException">The specified <see cref="T:System.DirectoryServices.DirectoryEntry" /> is not a container.</exception>
	public void MoveTo(DirectoryEntry newParent, string newName)
	{
		string fdn = Parent.Fdn;
		conn.Rename(Fdn, newName, newParent.Fdn, deleteOldRdn: true);
		Path = Path.Replace(fdn, newParent.Fdn).Replace(Name, newName);
		RefreshEntry();
	}

	/// <summary>Changes the name of this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object.</summary>
	/// <param name="newName">The new name of the entry.</param>
	public void Rename(string newName)
	{
		string name = Name;
		conn.Rename(Fdn, newName, deleteOldRdn: true);
		Path = Path.Replace(name, newName);
		RefreshEntry();
	}

	/// <summary>Calls a method on the native Active Directory Domain Services object.</summary>
	/// <param name="methodName">The name of the method to invoke.</param>
	/// <param name="args">An array of type <see cref="T:System.Object" /> objects that contains the arguments of the method to invoke.</param>
	/// <returns>The return value of the invoked method.</returns>
	/// <exception cref="T:System.DirectoryServices.DirectoryServicesCOMException">The native method threw a <see cref="T:System.Runtime.InteropServices.COMException" /> exception.</exception>
	/// <exception cref="T:System.Reflection.TargetInvocationException">The native method threw a <see cref="T:System.Reflection.TargetInvocationException" /> exception. The <see cref="P:System.Exception.InnerException" /> property contains a <see cref="T:System.Runtime.InteropServices.COMException" /> exception that contains information about the actual error that occurred.</exception>
	[System.MonoTODO]
	public object Invoke(string methodName, params object[] args)
	{
		throw new NotImplementedException();
	}

	/// <summary>Gets a property from the native Active Directory Domain Services object.</summary>
	/// <param name="propertyName">The name of the property to get.</param>
	/// <returns>An object that represents the requested property.</returns>
	[ComVisible(false)]
	[System.MonoNotSupported("")]
	public object InvokeGet(string propertyName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Sets a property on the native Active Directory Domain Services object.</summary>
	/// <param name="propertyName">The name of the property to set.</param>
	/// <param name="args">The Active Directory Domain Services object to set.</param>
	[ComVisible(false)]
	[System.MonoNotSupported("")]
	public void InvokeSet(string propertyName, params object[] args)
	{
		throw new NotImplementedException();
	}

	/// <summary>Creates a copy of this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object, as a child of the specified parent <see cref="T:System.DirectoryServices.DirectoryEntry" /> object, with the specified new name.</summary>
	/// <param name="newParent">The DN of the <see cref="T:System.DirectoryServices.DirectoryEntry" /> object that will be the parent for the copy that is being created.</param>
	/// <param name="newName">The name of the copy of this entry.</param>
	/// <returns>A renamed copy of this entry as a child of the specified parent.</returns>
	/// <exception cref="T:System.InvalidOperationException">The specified <see cref="T:System.DirectoryServices.DirectoryEntry" /> object is not a container.</exception>
	[System.MonoTODO]
	public DirectoryEntry CopyTo(DirectoryEntry newParent, string newName)
	{
		throw new NotImplementedException();
	}

	/// <summary>Saves changes that are made to a directory entry to the underlying directory store.</summary>
	public void CommitChanges()
	{
		if (UsePropertyCache)
		{
			CommitEntry();
		}
	}

	private void CommitEntry()
	{
		PropertyCollection properties = GetProperties(forceLoad: false);
		if (!Nflag)
		{
			ArrayList arrayList = new ArrayList();
			foreach (string propertyName in properties.PropertyNames)
			{
				LdapAttribute ldapAttribute = null;
				if (properties[propertyName].Mbit)
				{
					switch (properties[propertyName].Count)
					{
					case 0:
						ldapAttribute = new LdapAttribute(propertyName, new string[0]);
						arrayList.Add(new LdapModification(1, ldapAttribute));
						break;
					case 1:
					{
						string attrString = (string)properties[propertyName].Value;
						ldapAttribute = new LdapAttribute(propertyName, attrString);
						arrayList.Add(new LdapModification(2, ldapAttribute));
						break;
					}
					default:
					{
						object[] sourceArray = (object[])properties[propertyName].Value;
						string[] array = new string[properties[propertyName].Count];
						Array.Copy(sourceArray, 0, array, 0, properties[propertyName].Count);
						ldapAttribute = new LdapAttribute(propertyName, array);
						arrayList.Add(new LdapModification(2, ldapAttribute));
						break;
					}
					}
					properties[propertyName].Mbit = false;
				}
			}
			if (arrayList.Count > 0)
			{
				LdapModification[] array2 = new LdapModification[arrayList.Count];
				Type typeFromHandle = typeof(LdapModification);
				array2 = (LdapModification[])arrayList.ToArray(typeFromHandle);
				ModEntry(array2);
			}
			return;
		}
		LdapAttributeSet ldapAttributeSet = new LdapAttributeSet();
		foreach (string propertyName2 in properties.PropertyNames)
		{
			if (properties[propertyName2].Count == 1)
			{
				string attrString2 = (string)properties[propertyName2].Value;
				ldapAttributeSet.Add(new LdapAttribute(propertyName2, attrString2));
				continue;
			}
			object[] sourceArray2 = (object[])properties[propertyName2].Value;
			string[] array3 = new string[properties[propertyName2].Count];
			Array.Copy(sourceArray2, 0, array3, 0, properties[propertyName2].Count);
			ldapAttributeSet.Add(new LdapAttribute(propertyName2, array3));
		}
		LdapEntry entry = new LdapEntry(Fdn, ldapAttributeSet);
		conn.Add(entry);
		Nflag = false;
	}

	internal void CommitDeferred()
	{
		if (!_inPropertiesLoading && !UsePropertyCache && !Nflag)
		{
			CommitEntry();
		}
	}

	private void RefreshEntry()
	{
		_Properties = null;
		_Fdn = null;
		_Name = null;
		_Parent = null;
		_SchemaClassName = null;
		InitEntry();
	}

	/// <summary>Loads the property values for this <see cref="T:System.DirectoryServices.DirectoryEntry" /> object into the property cache.</summary>
	public void RefreshCache()
	{
		PropertyCollection properties = new PropertyCollection();
		LoadProperties(properties, null);
		SetProperties(properties);
	}

	/// <summary>Loads the values of the specified properties into the property cache.</summary>
	/// <param name="propertyNames">An array of the specified properties.</param>
	public void RefreshCache(string[] propertyNames)
	{
		LoadProperties(GetProperties(forceLoad: false), propertyNames);
	}

	/// <summary>Disposes of the resources (other than memory) that are used by the <see cref="T:System.DirectoryServices.DirectoryEntry" />.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> to release both managed and unmanaged resources; <see langword="false" /> to release only unmanaged resources.</param>
	protected override void Dispose(bool disposing)
	{
		if (disposing)
		{
			Close();
		}
		base.Dispose(disposing);
	}

	internal static string GetLdapUrlString(string host, int port, string dn)
	{
		LdapUrl ldapUrl = ((port != 389) ? new LdapUrl(host, port, dn) : new LdapUrl(host, 0, dn));
		return ldapUrl.ToString();
	}
}
