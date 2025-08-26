using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the output cache for a Web application. This class cannot be inherited.</summary>
public sealed class OutputCacheSection : ConfigurationSection
{
	private static ConfigurationProperty enableFragmentCacheProp;

	private static ConfigurationProperty enableOutputCacheProp;

	private static ConfigurationProperty omitVaryStarProp;

	private static ConfigurationProperty sendCacheControlHeaderProp;

	private static ConfigurationProperty enableKernelCacheForVaryByStarProp;

	private static ConfigurationProperty providersProp;

	private static ConfigurationProperty defaultProviderNameProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets a value indicating whether the fragment cache is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the fragment cache is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("enableFragmentCache", DefaultValue = "True")]
	public bool EnableFragmentCache
	{
		get
		{
			return (bool)base[enableFragmentCacheProp];
		}
		set
		{
			base[enableFragmentCacheProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the output cache is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the output cache is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("enableOutputCache", DefaultValue = "True")]
	public bool EnableOutputCache
	{
		get
		{
			return (bool)base[enableOutputCacheProp];
		}
		set
		{
			base[enableOutputCacheProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether kernel caching is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if kernel caching is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("enableKernelCacheForVaryByStar", DefaultValue = "False")]
	public bool EnableKernelCacheForVaryByStar
	{
		get
		{
			return (bool)base[enableKernelCacheForVaryByStarProp];
		}
		set
		{
			base[enableKernelCacheForVaryByStarProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see langword="vary" /> header is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see langword="vary" /> header is enabled; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("omitVaryStar", DefaultValue = "False")]
	public bool OmitVaryStar
	{
		get
		{
			return (bool)base[omitVaryStarProp];
		}
		set
		{
			base[omitVaryStarProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the <see langword="cache-control:private" /> header is sent by the output cache module by default.</summary>
	/// <returns>
	///     <see langword="true" /> if the sending of <see langword="cache-control:private" /> header is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("sendCacheControlHeader", DefaultValue = "True")]
	public bool SendCacheControlHeader
	{
		get
		{
			return (bool)base[sendCacheControlHeaderProp];
		}
		set
		{
			base[sendCacheControlHeaderProp] = value;
		}
	}

	/// <summary>Gets or sets the name of the ASP.NET default output-cache provider that is stored in the <see cref="T:System.Web.Configuration.OutputCacheSection" /> element of a configuration file.</summary>
	/// <returns>The name of the default provider.</returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("defaultProvider", DefaultValue = "AspNetInternalProvider")]
	public string DefaultProviderName
	{
		get
		{
			return base[defaultProviderNameProp] as string;
		}
		set
		{
			base[defaultProviderNameProp] = value;
		}
	}

	/// <summary>Gets or sets the collection of output-cache providers that are stored in the <see cref="T:System.Web.Configuration.OutputCacheSection" /> element of a configuration file. </summary>
	/// <returns>The collection of providers.</returns>
	[ConfigurationProperty("providers")]
	public ProviderSettingsCollection Providers => base[providersProp] as ProviderSettingsCollection;

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static OutputCacheSection()
	{
		enableFragmentCacheProp = new ConfigurationProperty("enableFragmentCache", typeof(bool), true);
		enableOutputCacheProp = new ConfigurationProperty("enableOutputCache", typeof(bool), true);
		omitVaryStarProp = new ConfigurationProperty("omitVaryStar", typeof(bool), false);
		sendCacheControlHeaderProp = new ConfigurationProperty("sendCacheControlHeader", typeof(bool), true);
		enableKernelCacheForVaryByStarProp = new ConfigurationProperty("enableKernelCacheForVaryByStar", typeof(bool), false);
		providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection));
		defaultProviderNameProp = new ConfigurationProperty("defaultProvider", typeof(string), "AspNetInternalProvider");
		properties = new ConfigurationPropertyCollection();
		properties.Add(enableFragmentCacheProp);
		properties.Add(enableOutputCacheProp);
		properties.Add(omitVaryStarProp);
		properties.Add(sendCacheControlHeaderProp);
		properties.Add(enableKernelCacheForVaryByStarProp);
		properties.Add(providersProp);
		properties.Add(defaultProviderNameProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.OutputCacheSection" /> class.</summary>
	public OutputCacheSection()
	{
	}
}
