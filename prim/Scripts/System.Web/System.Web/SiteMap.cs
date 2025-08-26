using System.Configuration;
using System.Web.Configuration;

namespace System.Web;

/// <summary>The <see cref="T:System.Web.SiteMap" /> class is an in-memory representation of the navigation structure for a site, which is provided by one or more site map providers. This class cannot be inherited. </summary>
public static class SiteMap
{
	private static SiteMapProvider provider;

	private static SiteMapProviderCollection providers;

	private static object locker = new object();

	/// <summary>Gets a <see cref="T:System.Web.SiteMapNode" /> control that represents the currently requested page.</summary>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> instance that represents the currently requested page; otherwise, <see langword="null" />, if no representative node exists in the site map information. </returns>
	/// <exception cref="T:System.InvalidOperationException">The site map feature is not enabled.</exception>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The default provider specified in the configuration does not exist.</exception>
	/// <exception cref="T:System.Web.HttpException">The feature is supported only when running in Low trust or higher.</exception>
	public static SiteMapNode CurrentNode => Provider.CurrentNode;

	/// <summary>Gets a <see cref="T:System.Web.SiteMapNode" /> object that represents the top-level page of the navigation structure for the site.</summary>
	/// <returns>A <see cref="T:System.Web.SiteMapNode" /> that represents the top-level page of the site's navigation structure; otherwise, <see langword="null" />, if security trimming is enabled and the node cannot be returned to the current user.</returns>
	/// <exception cref="T:System.InvalidOperationException">The site map feature is not enabled.- or -The <see cref="P:System.Web.SiteMap.RootNode" /> resolves to <see langword="null" />, which occurs if security trimming is enabled and the root node is not visible to the current user. </exception>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The default provider specified in the configuration does not exist.</exception>
	/// <exception cref="T:System.Web.HttpException">The feature is supported only when running in Low trust or higher.</exception>
	public static SiteMapNode RootNode => Provider.RootNode;

	/// <summary>Gets the default <see cref="T:System.Web.SiteMapProvider" /> object for the current site map.</summary>
	/// <returns>The default site map provider for the <see cref="T:System.Web.SiteMap" />. </returns>
	/// <exception cref="T:System.InvalidOperationException">The site map feature is not enabled.</exception>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The default provider specified in the configuration does not exist.</exception>
	/// <exception cref="T:System.Web.HttpException">The feature is supported only when running in Low trust or higher.</exception>
	public static SiteMapProvider Provider
	{
		get
		{
			Init();
			return provider;
		}
	}

	/// <summary>Gets a read-only collection of named <see cref="T:System.Web.SiteMapProvider" /> objects that are available to the <see cref="T:System.Web.SiteMap" /> class.</summary>
	/// <returns>A <see cref="T:System.Web.SiteMapProviderCollection" /> of named <see cref="T:System.Web.SiteMapProvider" /> objects.</returns>
	/// <exception cref="T:System.InvalidOperationException">The site map feature is not enabled.</exception>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The default provider specified in the configuration does not exist.</exception>
	/// <exception cref="T:System.Web.HttpException">The feature is supported only when running in Low trust or higher.</exception>
	public static SiteMapProviderCollection Providers
	{
		get
		{
			Init();
			return providers;
		}
	}

	/// <summary>Gets a Boolean value indicating if a site map provider is specified in the Web.config file and if the site map provider is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if a site map provider is configured and enabled; otherwise, <see langword="false" />.</returns>
	public static bool Enabled => ((SiteMapSection)WebConfigurationManager.GetSection("system.web/siteMap")).Enabled;

	/// <summary>Occurs when the <see cref="P:System.Web.SiteMap.CurrentNode" /> property is accessed. </summary>
	public static event SiteMapResolveEventHandler SiteMapResolve
	{
		add
		{
			Provider.SiteMapResolve += value;
		}
		remove
		{
			Provider.SiteMapResolve -= value;
		}
	}

	private static void Init()
	{
		lock (locker)
		{
			if (provider == null)
			{
				SiteMapSection siteMapSection = (SiteMapSection)WebConfigurationManager.GetSection("system.web/siteMap");
				if (!siteMapSection.Enabled)
				{
					throw new InvalidOperationException("This feature is currently disabled.  Please enable it in the system.web/siteMap section in the web.config file.");
				}
				providers = siteMapSection.ProvidersInternal;
				providers.SetReadOnly();
				provider = providers[siteMapSection.DefaultProvider];
				if (provider == null)
				{
					throw new ConfigurationErrorsException($"The default sitemap provider '{siteMapSection.DefaultProvider}' does not exist in the provider collection.");
				}
			}
		}
	}
}
