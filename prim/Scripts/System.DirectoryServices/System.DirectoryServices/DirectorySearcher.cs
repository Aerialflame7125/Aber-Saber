using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using Novell.Directory.Ldap;

namespace System.DirectoryServices;

/// <summary>Performs queries against Active Directory Domain Services.</summary>
public class DirectorySearcher : Component
{
	private static readonly TimeSpan DefaultTimeSpan = new TimeSpan(-10000000L);

	private DirectoryEntry _SearchRoot;

	private bool _CacheResults = true;

	private TimeSpan _ClientTimeout = DefaultTimeSpan;

	private string _Filter = "(objectClass=*)";

	private int _PageSize;

	private StringCollection _PropertiesToLoad = new StringCollection();

	private bool _PropertyNamesOnly;

	private ReferralChasingOption _ReferralChasing = ReferralChasingOption.External;

	private SearchScope _SearchScope = SearchScope.Subtree;

	private TimeSpan _ServerPageTimeLimit = DefaultTimeSpan;

	private TimeSpan _serverTimeLimit = DefaultTimeSpan;

	private int _SizeLimit;

	private LdapConnection _conn;

	private string _Host;

	private int _Port = 389;

	private SearchResultCollection _SrchColl;

	internal SearchResultCollection SrchColl
	{
		get
		{
			if (_SrchColl == null)
			{
				_SrchColl = new SearchResultCollection();
				DoSearch();
			}
			return _SrchColl;
		}
	}

	/// <summary>Gets or sets a value indicating whether the result is cached on the client computer.</summary>
	/// <returns>
	///   <see langword="true" /> if the result is cached on the client computer; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[DSDescription("The cacheability of results.")]
	[DefaultValue(true)]
	public bool CacheResults
	{
		get
		{
			return _CacheResults;
		}
		set
		{
			_CacheResults = value;
		}
	}

	/// <summary>Gets or sets the maximum amount of time that the client waits for the server to return results. If the server does not respond within this time, the search is aborted and no results are returned.</summary>
	/// <returns>A <see cref="T:System.TimeSpan" /> structure that contains the maximum amount of time for the client to wait for the server to return results.  
	///  The default value is -1 second, which means to wait indefinitely.</returns>
	[DSDescription("The maximum amount of time that the client waits for the server to return results.")]
	public TimeSpan ClientTimeout
	{
		get
		{
			return _ClientTimeout;
		}
		set
		{
			_ClientTimeout = value;
		}
	}

	/// <summary>Gets or sets a value indicating the Lightweight Directory Access Protocol (LDAP) format filter string.</summary>
	/// <returns>The search filter string in LDAP format, such as "(objectClass=user)". The default is "(objectClass=*)", which retrieves all objects.</returns>
	[DSDescription("The Lightweight Directory Access Protocol (Ldap) format filter string.")]
	[DefaultValue("(objectClass=*)")]
	[RecommendedAsConfigurable(true)]
	[TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public string Filter
	{
		get
		{
			return _Filter;
		}
		set
		{
			_Filter = value;
			ClearCachedResults();
		}
	}

	/// <summary>Gets or sets a value indicating the page size in a paged search.</summary>
	/// <returns>The maximum number of objects the server can return in a paged search. The default is zero, which means do not do a paged search.</returns>
	/// <exception cref="T:System.ArgumentException">The new value is less than zero.</exception>
	[DSDescription("The page size in a paged search.")]
	[DefaultValue(0)]
	public int PageSize
	{
		get
		{
			return _PageSize;
		}
		set
		{
			_PageSize = value;
		}
	}

	/// <summary>Gets a value indicating the list of properties to retrieve during the search.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.StringCollection" /> object that contains the set of properties to retrieve during the search.  
	///  The default is an empty <see cref="T:System.Collections.Specialized.StringCollection" />, which retrieves all properties.</returns>
	[DSDescription("The set of properties retrieved during the search.")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public StringCollection PropertiesToLoad => _PropertiesToLoad;

	/// <summary>Gets or sets a value indicating whether the search retrieves only the names of attributes to which values have been assigned.</summary>
	/// <returns>
	///   <see langword="true" /> if the search obtains only the names of attributes to which values have been assigned; <see langword="false" /> if the search obtains the names and values for all the requested attributes. The default value is <see langword="false" />.</returns>
	[DSDescription("A value indicating whether the search retrieves only the names of attributes to which values have been assigned.")]
	[DefaultValue(false)]
	public bool PropertyNamesOnly
	{
		get
		{
			return _PropertyNamesOnly;
		}
		set
		{
			_PropertyNamesOnly = value;
		}
	}

	/// <summary>Gets or sets a value indicating how referrals are chased.</summary>
	/// <returns>One of the <see cref="T:System.DirectoryServices.ReferralChasingOption" /> values. The default is <see cref="F:System.DirectoryServices.ReferralChasingOption.External" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.DirectoryServices.ReferralChasingOption" /> values.</exception>
	[DSDescription("How referrals are chased.")]
	[DefaultValue(ReferralChasingOption.External)]
	public ReferralChasingOption ReferralChasing
	{
		get
		{
			return _ReferralChasing;
		}
		set
		{
			_ReferralChasing = value;
		}
	}

	/// <summary>Gets or sets a value indicating the node in the Active Directory Domain Services hierarchy where the search starts.</summary>
	/// <returns>The <see cref="T:System.DirectoryServices.DirectoryEntry" /> object in the Active Directory Domain Services hierarchy where the search starts. The default is a null reference (<see langword="Nothing" /> in Visual Basic).</returns>
	[DSDescription("The node in the Ldap Directory hierarchy where the search starts.")]
	[DefaultValue(null)]
	public DirectoryEntry SearchRoot
	{
		get
		{
			return _SearchRoot;
		}
		set
		{
			_SearchRoot = value;
			ClearCachedResults();
		}
	}

	/// <summary>Gets or sets a value indicating the scope of the search that is observed by the server.</summary>
	/// <returns>One of the <see cref="T:System.DirectoryServices.SearchScope" /> values. The default is <see cref="F:System.DirectoryServices.SearchScope.Subtree" />.</returns>
	/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">The value is not one of the <see cref="T:System.DirectoryServices.SearchScope" /> values.</exception>
	[DSDescription("The scope of the search that is observed by the server.")]
	[DefaultValue(SearchScope.Subtree)]
	[RecommendedAsConfigurable(true)]
	public SearchScope SearchScope
	{
		get
		{
			return _SearchScope;
		}
		set
		{
			_SearchScope = value;
			ClearCachedResults();
		}
	}

	/// <summary>Gets or sets a value indicating the maximum amount of time the server should search for an individual page of results. This is not the same as the time limit for the entire search.</summary>
	/// <returns>A <see cref="T:System.TimeSpan" /> that represents the amount of time the server should search for a page of results.  
	///  The default value is -1 seconds, which means to search indefinitely.</returns>
	[DSDescription("The time limit the server should observe to search an individual page of results.")]
	public TimeSpan ServerPageTimeLimit
	{
		get
		{
			return _ServerPageTimeLimit;
		}
		set
		{
			_ServerPageTimeLimit = value;
		}
	}

	/// <summary>The <see cref="P:System.DirectoryServices.DirectorySearcher.ServerTimeLimit" /> property gets or sets a value indicating the maximum amount of time the server spends searching. If the time limit is reached, only entries that are found up to that point are returned.</summary>
	/// <returns>A <see cref="T:System.TimeSpan" /> that represents the amount of time that the server should search.  
	///  The default value is -1 seconds, which means to use the server-determined default of 120 seconds.</returns>
	[DSDescription("The time limit the server should observe to search.")]
	public TimeSpan ServerTimeLimit
	{
		[System.MonoTODO]
		get
		{
			return _serverTimeLimit;
		}
		[System.MonoTODO]
		set
		{
			_serverTimeLimit = value;
		}
	}

	/// <summary>Gets or sets a value indicating the maximum number of objects that the server returns in a search.</summary>
	/// <returns>The maximum number of objects that the server returns in a search. The default value is zero, which means to use the server-determined default size limit of 1000 entries.</returns>
	/// <exception cref="T:System.ArgumentException">The new value is less than zero.</exception>
	[DSDescription("The maximum number of objects the server returns in a search.")]
	[DefaultValue(0)]
	public int SizeLimit
	{
		get
		{
			return _SizeLimit;
		}
		set
		{
			if (value < 0)
			{
				throw new ArgumentException();
			}
			_SizeLimit = value;
		}
	}

	/// <summary>Gets or sets a value indicating the property on which the results are sorted.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.SortOption" /> object that specifies the property and direction that the search results should be sorted on.</returns>
	/// <exception cref="T:System.ArgumentNullException">The property value is <see langword="null" /> (Nothing in Visual Basic).</exception>
	[DSDescription("An object that defines how the data should be sorted.")]
	[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public SortOption Sort
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

	private void InitBlock()
	{
		_conn = new LdapConnection();
		LdapUrl ldapUrl = new LdapUrl(SearchRoot.ADsPath);
		_Host = ldapUrl.Host;
		_Port = ldapUrl.Port;
		_conn.Connect(_Host, _Port);
		_conn.Bind(SearchRoot.Username, SearchRoot.Password, (Novell.Directory.Ldap.AuthenticationTypes)SearchRoot.AuthenticationType);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySearcher" /> class with default values.</summary>
	public DirectorySearcher()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySearcher" /> class using the specified search root.</summary>
	/// <param name="searchRoot">The node in the Active Directory Domain Services hierarchy where the search starts. The <see cref="P:System.DirectoryServices.DirectorySearcher.SearchRoot" /> property is initialized to this value.</param>
	public DirectorySearcher(DirectoryEntry searchRoot)
	{
		_SearchRoot = searchRoot;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySearcher" /> class with the specified search filter.</summary>
	/// <param name="filter">The search filter string in Lightweight Directory Access Protocol (LDAP) format. The <see cref="P:System.DirectoryServices.DirectorySearcher.Filter" /> property is initialized to this value.</param>
	public DirectorySearcher(string filter)
	{
		_Filter = filter;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySearcher" /> class with the specified search root and search filter.</summary>
	/// <param name="searchRoot">The node in the Active Directory Domain Services hierarchy where the search starts. The <see cref="P:System.DirectoryServices.DirectorySearcher.SearchRoot" /> property is initialized to this value.</param>
	/// <param name="filter">The search filter string in Lightweight Directory Access Protocol (LDAP) format. The <see cref="P:System.DirectoryServices.DirectorySearcher.Filter" /> property is initialized to this value.</param>
	public DirectorySearcher(DirectoryEntry searchRoot, string filter)
	{
		_SearchRoot = searchRoot;
		_Filter = filter;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySearcher" /> class with the specified search filter and properties to retrieve.</summary>
	/// <param name="filter">The search filter string in Lightweight Directory Access Protocol (LDAP) format. The  <see cref="P:System.DirectoryServices.DirectorySearcher.Filter" /> property is initialized to this value.</param>
	/// <param name="propertiesToLoad">The set of properties to retrieve during the search. The <see cref="P:System.DirectoryServices.DirectorySearcher.PropertiesToLoad" /> property is initialized to this value.</param>
	public DirectorySearcher(string filter, string[] propertiesToLoad)
	{
		_Filter = filter;
		PropertiesToLoad.AddRange(propertiesToLoad);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySearcher" /> class with the specified search root, search filter, and properties to retrieve.</summary>
	/// <param name="searchRoot">The node in the Active Directory Domain Services hierarchy where the search starts. The <see cref="P:System.DirectoryServices.DirectorySearcher.SearchRoot" /> property is initialized to this value.</param>
	/// <param name="filter">The search filter string in Lightweight Directory Access Protocol (LDAP) format. The <see cref="P:System.DirectoryServices.DirectorySearcher.Filter" /> property is initialized to this value.</param>
	/// <param name="propertiesToLoad">The set of properties that are retrieved during the search. The <see cref="P:System.DirectoryServices.DirectorySearcher.PropertiesToLoad" /> property is initialized to this value.</param>
	public DirectorySearcher(DirectoryEntry searchRoot, string filter, string[] propertiesToLoad)
	{
		_SearchRoot = searchRoot;
		_Filter = filter;
		PropertiesToLoad.AddRange(propertiesToLoad);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySearcher" /> class with the specified search filter, properties to retrieve, and search scope.</summary>
	/// <param name="filter">The search filter string in Lightweight Directory Access Protocol (LDAP) format. The <see cref="P:System.DirectoryServices.DirectorySearcher.Filter" /> property is initialized to this value.</param>
	/// <param name="propertiesToLoad">The set of properties to retrieve during the search. The <see cref="P:System.DirectoryServices.DirectorySearcher.PropertiesToLoad" /> property is initialized to this value.</param>
	/// <param name="scope">The scope of the search that is observed by the server. The <see cref="T:System.DirectoryServices.SearchScope" /> property is initialized to this value.</param>
	public DirectorySearcher(string filter, string[] propertiesToLoad, SearchScope scope)
	{
		_SearchScope = scope;
		_Filter = filter;
		PropertiesToLoad.AddRange(propertiesToLoad);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.DirectoryServices.DirectorySearcher" /> class with the specified search root, search filter, properties to retrieve, and search scope.</summary>
	/// <param name="searchRoot">The node in the Active Directory Domain Services hierarchy where the search starts. The <see cref="P:System.DirectoryServices.DirectorySearcher.SearchRoot" /> property is initialized to this value.</param>
	/// <param name="filter">The search filter string in Lightweight Directory Access Protocol (LDAP) format. The <see cref="P:System.DirectoryServices.DirectorySearcher.Filter" /> property is initialized to this value.</param>
	/// <param name="propertiesToLoad">The set of properties to retrieve during the search. The <see cref="P:System.DirectoryServices.DirectorySearcher.PropertiesToLoad" /> property is initialized to this value.</param>
	/// <param name="scope">The scope of the search that is observed by the server. The <see cref="T:System.DirectoryServices.SearchScope" /> property is initialized to this value.</param>
	public DirectorySearcher(DirectoryEntry searchRoot, string filter, string[] propertiesToLoad, SearchScope scope)
	{
		_SearchRoot = searchRoot;
		_SearchScope = scope;
		_Filter = filter;
		PropertiesToLoad.AddRange(propertiesToLoad);
	}

	/// <summary>Executes the search and returns only the first entry that is found.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.SearchResult" /> object that contains the first entry that is found during the search.</returns>
	public SearchResult FindOne()
	{
		if (SrchColl.Count == 0)
		{
			return null;
		}
		return SrchColl[0];
	}

	/// <summary>Executes the search and returns a collection of the entries that are found.</summary>
	/// <returns>A <see cref="T:System.DirectoryServices.SearchResultCollection" /> object that contains the results of the search.</returns>
	/// <exception cref="T:System.InvalidOperationException">The specified <see cref="T:System.DirectoryServices.DirectoryEntry" /> is not a container.</exception>
	/// <exception cref="T:System.NotSupportedException">Searching is not supported by the provider that is being used.</exception>
	public SearchResultCollection FindAll()
	{
		return SrchColl;
	}

	private void DoSearch()
	{
		InitBlock();
		string[] array = new string[PropertiesToLoad.Count];
		PropertiesToLoad.CopyTo(array, 0);
		LdapSearchConstraints searchConstraints = _conn.SearchConstraints;
		if (SizeLimit > 0)
		{
			searchConstraints.MaxResults = SizeLimit;
		}
		if (ServerTimeLimit != DefaultTimeSpan)
		{
			searchConstraints.ServerTimeLimit = (int)ServerTimeLimit.TotalSeconds;
		}
		int num = 2;
		num = _SearchScope switch
		{
			SearchScope.Base => 0, 
			SearchScope.OneLevel => 1, 
			SearchScope.Subtree => 2, 
			_ => 2, 
		};
		LdapSearchResults ldapSearchResults = _conn.Search(SearchRoot.Fdn, num, Filter, array, PropertyNamesOnly, searchConstraints);
		while (ldapSearchResults.hasMore())
		{
			LdapEntry ldapEntry = null;
			try
			{
				ldapEntry = ldapSearchResults.next();
			}
			catch (LdapException ex)
			{
				int resultCode = ex.ResultCode;
				if ((uint)(resultCode - 3) <= 1u || resultCode == 10)
				{
					continue;
				}
				throw ex;
			}
			DirectoryEntry directoryEntry = new DirectoryEntry(_conn);
			PropertyCollection propertyCollection = new PropertyCollection();
			directoryEntry.Path = DirectoryEntry.GetLdapUrlString(_Host, _Port, ldapEntry.DN);
			IEnumerator enumerator = ldapEntry.getAttributeSet().GetEnumerator();
			if (enumerator != null)
			{
				while (enumerator.MoveNext())
				{
					LdapAttribute ldapAttribute = (LdapAttribute)enumerator.Current;
					string name = ldapAttribute.Name;
					propertyCollection[name].AddRange(ldapAttribute.StringValueArray);
				}
			}
			if (!propertyCollection.Contains("ADsPath"))
			{
				propertyCollection["ADsPath"].Add(directoryEntry.Path);
			}
			_SrchColl.Add(new SearchResult(directoryEntry, propertyCollection));
		}
	}

	/// <summary>Releases the managed resources that are used by the <see cref="T:System.DirectoryServices.DirectorySearcher" /> object and, optionally, releases unmanaged resources.</summary>
	/// <param name="disposing">
	///   <see langword="true" /> if this method releases both managed and unmanaged resources; <see langword="false" /> if it releases only unmanaged resources.</param>
	[System.MonoTODO]
	protected override void Dispose(bool disposing)
	{
		if (disposing && _conn != null && _conn.Connected)
		{
			_conn.Disconnect();
		}
		base.Dispose(disposing);
	}

	private void ClearCachedResults()
	{
		_SrchColl = null;
	}
}
