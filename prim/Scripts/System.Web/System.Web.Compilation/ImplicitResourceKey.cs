namespace System.Web.Compilation;

/// <summary>Contains fields that identify an implicit resource key.</summary>
public sealed class ImplicitResourceKey
{
	private string _filter;

	private string _keyPrefix;

	private string _property;

	/// <summary>Gets or sets the filter value of an implicit resource key.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the filter value for the implicit resource expression.</returns>
	public string Filter
	{
		get
		{
			return _filter;
		}
		set
		{
			_filter = value;
		}
	}

	/// <summary>Gets or sets the prefix for identifying a group of properties.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the prefix for an implicit resource expression.</returns>
	public string KeyPrefix
	{
		get
		{
			return _keyPrefix;
		}
		set
		{
			_keyPrefix = value;
		}
	}

	/// <summary>Gets or sets a property and subproperty, if provided, for an implicit resource key.</summary>
	/// <returns>A <see cref="T:System.String" /> containing the property and subproperty for an implicit resource expression.</returns>
	public string Property
	{
		get
		{
			return _property;
		}
		set
		{
			_property = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ImplicitResourceKey" /> class. </summary>
	public ImplicitResourceKey()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Compilation.ImplicitResourceKey" /> class with the specified values for the <see cref="P:System.Web.Compilation.ImplicitResourceKey.Filter" />, <see cref="P:System.Web.Compilation.ImplicitResourceKey.KeyPrefix" /> and <see cref="P:System.Web.Compilation.ImplicitResourceKey.Property" /> properties.</summary>
	/// <param name="filter">The filter value of an implicit resource key.</param>
	/// <param name="keyPrefix">The prefix for identifying a group of properties.</param>
	/// <param name="property">A property and subproperty, if provided, for an implicit resource key.</param>
	public ImplicitResourceKey(string filter, string keyPrefix, string property)
	{
		_filter = filter;
		_keyPrefix = keyPrefix;
		_property = property;
	}
}
