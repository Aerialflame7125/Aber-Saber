using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the <see langword="caching" /> group within a configuration file. This class cannot be inherited. </summary>
public sealed class SystemWebCachingSectionGroup : ConfigurationSectionGroup
{
	/// <summary>Gets the <see langword="cache" /> section contained within the configuration.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.CacheSection" /> object.</returns>
	[ConfigurationProperty("cache")]
	public CacheSection Cache => (CacheSection)base.Sections["cache"];

	/// <summary>Gets the <see langword="outputCache" /> section contained within the configuration.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheSection" /> object.</returns>
	[ConfigurationProperty("outputCache")]
	public OutputCacheSection OutputCache => (OutputCacheSection)base.Sections["outputCache"];

	/// <summary>Gets the <see langword="outputCacheSettings" /> section contained within the configuration.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.OutputCacheSettingsSection" /> object.</returns>
	[ConfigurationProperty("outputCacheSettings")]
	public OutputCacheSettingsSection OutputCacheSettings => (OutputCacheSettingsSection)base.Sections["outputCacheSettings"];

	/// <summary>Gets the <see langword="sqlCacheDependency" /> section contained within the configuration.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.SqlCacheDependencySection" /> object.</returns>
	[ConfigurationProperty("sqlCacheDependency")]
	public SqlCacheDependencySection SqlCacheDependency => (SqlCacheDependencySection)base.Sections["sqlCacheDependency"];

	/// <summary>Creates a new instance of <see cref="T:System.Web.Configuration.SystemWebCachingSectionGroup" />.</summary>
	public SystemWebCachingSectionGroup()
	{
	}
}
