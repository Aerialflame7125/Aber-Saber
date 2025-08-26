using System.ComponentModel;
using System.Configuration;
using System.Web.UI;

namespace System.Web.Configuration;

/// <summary>Configures the output cache profile that can be used by the application pages. This class cannot be inherited.</summary>
public sealed class OutputCacheProfile : ConfigurationElement
{
	private static ConfigurationProperty durationProp;

	private static ConfigurationProperty enabledProp;

	private static ConfigurationProperty locationProp;

	private static ConfigurationProperty nameProp;

	private static ConfigurationProperty noStoreProp;

	private static ConfigurationProperty sqlDependencyProp;

	private static ConfigurationProperty varyByContentEncodingProp;

	private static ConfigurationProperty varyByControlProp;

	private static ConfigurationProperty varyByCustomProp;

	private static ConfigurationProperty varyByHeaderProp;

	private static ConfigurationProperty varyByParamProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets the time duration during which the page or control is cached.</summary>
	/// <returns>The time duration in seconds.</returns>
	[ConfigurationProperty("duration", DefaultValue = "-1")]
	public int Duration
	{
		get
		{
			return (int)base[durationProp];
		}
		set
		{
			base[durationProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether caching is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if caching is enabled; otherwise, <see langword="false" />. The default value is <see langword="false" />. </returns>
	[ConfigurationProperty("enabled", DefaultValue = "True")]
	public bool Enabled
	{
		get
		{
			return (bool)base[enabledProp];
		}
		set
		{
			base[enabledProp] = value;
		}
	}

	/// <summary>Gets or sets the output cache location.</summary>
	/// <returns>One of the <see cref="T:System.Web.UI.OutputCacheLocation" /> enumeration values. The default is <see langword="Any" />.</returns>
	[ConfigurationProperty("location")]
	public OutputCacheLocation Location
	{
		get
		{
			return (OutputCacheLocation)base[locationProp];
		}
		set
		{
			base[locationProp] = value;
		}
	}

	/// <summary>Gets or sets the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> name.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheProfile" /> name.</returns>
	[StringValidator(MinLength = 1)]
	[TypeConverter(typeof(WhiteSpaceTrimStringConverter))]
	[ConfigurationProperty("name", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
	public string Name
	{
		get
		{
			return (string)base[nameProp];
		}
		set
		{
			base[nameProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether secondary storage is enabled. </summary>
	/// <returns>
	///     <see langword="true" /> if secondary storage is enabled; otherwise, <see langword="false" />. The default value is <see langword="false" />. </returns>
	[ConfigurationProperty("noStore", DefaultValue = "False")]
	public bool NoStore
	{
		get
		{
			return (bool)base[noStoreProp];
		}
		set
		{
			base[noStoreProp] = value;
		}
	}

	/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.OutputCacheProfile.SqlDependency" /> property. </summary>
	/// <returns>The <see cref="P:System.Web.Configuration.OutputCacheProfile.SqlDependency" /> value.</returns>
	[ConfigurationProperty("sqlDependency")]
	public string SqlDependency
	{
		get
		{
			return (string)base[sqlDependencyProp];
		}
		set
		{
			base[sqlDependencyProp] = value;
		}
	}

	/// <summary>Gets or sets the semicolon-delimited set of content encodings to be cached.</summary>
	/// <returns>The list of content encodings.</returns>
	[ConfigurationProperty("varyByContentEncoding")]
	public string VaryByContentEncoding
	{
		get
		{
			return (string)base[varyByContentEncodingProp];
		}
		set
		{
			base[varyByContentEncodingProp] = value;
		}
	}

	/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByControl" /> property.</summary>
	/// <returns>The <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByControl" /> value.</returns>
	[ConfigurationProperty("varyByControl")]
	public string VaryByControl
	{
		get
		{
			return (string)base[varyByControlProp];
		}
		set
		{
			base[varyByControlProp] = value;
		}
	}

	/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByCustom" /> property.</summary>
	/// <returns>The <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByCustom" /> value.</returns>
	[ConfigurationProperty("varyByCustom")]
	public string VaryByCustom
	{
		get
		{
			return (string)base[varyByCustomProp];
		}
		set
		{
			base[varyByCustomProp] = value;
		}
	}

	/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByHeader" /> property.</summary>
	/// <returns>The <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByHeader" /> value.</returns>
	[ConfigurationProperty("varyByHeader")]
	public string VaryByHeader
	{
		get
		{
			return (string)base[varyByHeaderProp];
		}
		set
		{
			base[varyByHeaderProp] = value;
		}
	}

	/// <summary>Gets or sets the <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByParam" /> property.</summary>
	/// <returns>The <see cref="P:System.Web.Configuration.OutputCacheProfile.VaryByParam" /> value.</returns>
	[ConfigurationProperty("varyByParam")]
	public string VaryByParam
	{
		get
		{
			return (string)base[varyByParamProp];
		}
		set
		{
			base[varyByParamProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static OutputCacheProfile()
	{
		durationProp = new ConfigurationProperty("duration", typeof(int), -1);
		enabledProp = new ConfigurationProperty("enabled", typeof(bool), true);
		locationProp = new ConfigurationProperty("location", typeof(OutputCacheLocation), null, new GenericEnumConverter(typeof(OutputCacheLocation)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		nameProp = new ConfigurationProperty("name", typeof(string), "", PropertyHelper.WhiteSpaceTrimStringConverter, PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);
		noStoreProp = new ConfigurationProperty("noStore", typeof(bool), false);
		sqlDependencyProp = new ConfigurationProperty("sqlDependency", typeof(string));
		varyByContentEncodingProp = new ConfigurationProperty("varyByContentEncoding", typeof(string));
		varyByControlProp = new ConfigurationProperty("varyByControl", typeof(string));
		varyByCustomProp = new ConfigurationProperty("varyByCustom", typeof(string));
		varyByHeaderProp = new ConfigurationProperty("varyByHeader", typeof(string));
		varyByParamProp = new ConfigurationProperty("varyByParam", typeof(string));
		properties = new ConfigurationPropertyCollection();
		properties.Add(durationProp);
		properties.Add(enabledProp);
		properties.Add(locationProp);
		properties.Add(nameProp);
		properties.Add(noStoreProp);
		properties.Add(sqlDependencyProp);
		properties.Add(varyByContentEncodingProp);
		properties.Add(varyByControlProp);
		properties.Add(varyByCustomProp);
		properties.Add(varyByHeaderProp);
		properties.Add(varyByParamProp);
	}

	internal OutputCacheProfile()
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.OutputCacheProfile" /> class.</summary>
	/// <param name="name">The name value to use.</param>
	public OutputCacheProfile(string name)
	{
		Name = name;
	}
}
