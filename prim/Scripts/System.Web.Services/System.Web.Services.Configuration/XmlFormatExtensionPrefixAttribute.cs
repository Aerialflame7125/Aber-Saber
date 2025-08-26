namespace System.Web.Services.Configuration;

/// <summary>Specifies the XML namespace and XML namespace prefix to use for a service description format extension within a service description. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class XmlFormatExtensionPrefixAttribute : Attribute
{
	private string prefix;

	private string ns;

	/// <summary>Gets or sets the XML namespace prefix associated with a service description format extension.</summary>
	/// <returns>The XML namespace prefix associated with a service description format extension.</returns>
	public string Prefix
	{
		get
		{
			if (prefix != null)
			{
				return prefix;
			}
			return string.Empty;
		}
		set
		{
			prefix = value;
		}
	}

	/// <summary>Gets or sets the XML namespace associated with a service description format extension.</summary>
	/// <returns>The XML namespace associated with a service description format extension.</returns>
	public string Namespace
	{
		get
		{
			if (ns != null)
			{
				return ns;
			}
			return string.Empty;
		}
		set
		{
			ns = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionPrefixAttribute" /> class.</summary>
	public XmlFormatExtensionPrefixAttribute()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Configuration.XmlFormatExtensionPrefixAttribute" /> class, setting the XML namespace and XML namespace prefix for a service description format extension.</summary>
	/// <param name="prefix">The XML namespace prefix associated with a service description format extension.</param>
	/// <param name="ns">The XML namespace associated with a service description format extension.</param>
	public XmlFormatExtensionPrefixAttribute(string prefix, string ns)
	{
		this.prefix = prefix;
		this.ns = ns;
	}
}
