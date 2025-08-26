namespace System.Web.UI;

/// <summary>Encapsulates the output cache initialization settings parsed from an @ OutputCache page directive by ASP.NET. This class cannot be inherited.</summary>
public sealed class OutputCacheParameters
{
	private string _cacheProfile;

	private int _duration;

	private bool _enabled;

	private OutputCacheLocation _location;

	private bool _noStore;

	private string _sqlDependency;

	private string _varByControl;

	private string _varByCustom;

	private string _varByHeader;

	private string _varByParam;

	private string _varyByContentEncoding;

	/// <summary>Gets or sets an <see cref="T:System.Web.Configuration.OutputCacheProfile" /> name that is associated with the settings of the output cache entry.</summary>
	/// <returns>An <see cref="T:System.Web.Configuration.OutputCacheProfile" /> name that is associated with the settings of the output cache entry.</returns>
	public string CacheProfile
	{
		get
		{
			return _cacheProfile;
		}
		set
		{
			_cacheProfile = value;
		}
	}

	/// <summary>Gets or sets the amount of time that a cache entry is to remain in the output cache.</summary>
	/// <returns>The amount of time, in seconds, that a cache entry is to remain in the output cache. The default is 0, which indicates an infinite duration.</returns>
	public int Duration
	{
		get
		{
			return _duration;
		}
		set
		{
			_duration = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether output caching is enabled for the current content.</summary>
	/// <returns>
	///     <see langword="true" /> if output caching is enabled for the current content; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	public bool Enabled
	{
		get
		{
			return _enabled;
		}
		set
		{
			_enabled = value;
		}
	}

	/// <summary>Gets or sets a value that determines the location of the cache entry.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.OutputCacheLocation" /> values.</returns>
	public OutputCacheLocation Location
	{
		get
		{
			return _location;
		}
		set
		{
			_location = value;
		}
	}

	/// <summary>Gets or sets a value that determines whether the HTTP <see langword="Cache-Control: no-store" /> directive is set.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see langword="Cache-Control: no-store" /> directive is set on <see cref="T:System.Web.HttpResponse" />; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool NoStore
	{
		get
		{
			return _noStore;
		}
		set
		{
			_noStore = value;
		}
	}

	/// <summary>Gets or sets a set of database and table name pairs that the cache entry depends on.</summary>
	/// <returns>A string that identifies a set of database and table name pairs that the cache entry depends on. The cache entry is expired when the table's data is updated or changes.</returns>
	public string SqlDependency
	{
		get
		{
			return _sqlDependency;
		}
		set
		{
			_sqlDependency = value;
		}
	}

	/// <summary>Gets or sets a comma-delimited set of character sets (content encodings) used to vary the cache entry.</summary>
	/// <returns>A list of character sets by which to vary the content.</returns>
	public string VaryByContentEncoding
	{
		get
		{
			return _varyByContentEncoding;
		}
		set
		{
			_varyByContentEncoding = value;
		}
	}

	/// <summary>Gets or sets a semicolon-delimited set of control identifiers contained within the current page or user control used to vary the current cache entry.</summary>
	/// <returns>A semicolon-separated list of strings used to vary an entry's output cache. The <see cref="P:System.Web.UI.OutputCacheParameters.VaryByControl" /> property is set to fully qualified control identifiers, where the identifier is a concatenation of control IDs starting from the top-level parent control and delimited with a dollar sign ($) character.</returns>
	public string VaryByControl
	{
		get
		{
			return _varByControl;
		}
		set
		{
			_varByControl = value;
		}
	}

	/// <summary>Gets a list of custom strings that the output cache uses to vary the cache entry.</summary>
	/// <returns>The list of custom strings.</returns>
	public string VaryByCustom
	{
		get
		{
			return _varByCustom;
		}
		set
		{
			_varByCustom = value;
		}
	}

	/// <summary>Gets or sets a comma-delimited set of header names used to vary the cache entry. The header names identify HTTP headers associated with the request.</summary>
	/// <returns>A list of headers by which to vary the content.</returns>
	public string VaryByHeader
	{
		get
		{
			return _varByHeader;
		}
		set
		{
			_varByHeader = value;
		}
	}

	/// <summary>Gets a semicolon-delimited list of query string or form POST parameters that the output cache uses to vary the cache entry.</summary>
	/// <returns>The list of query string or form POST parameters.</returns>
	public string VaryByParam
	{
		get
		{
			return _varByParam;
		}
		set
		{
			_varByParam = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.OutputCacheParameters" /> class. </summary>
	public OutputCacheParameters()
	{
		Duration = 0;
		Enabled = true;
		Location = OutputCacheLocation.Any;
		NoStore = false;
	}
}
