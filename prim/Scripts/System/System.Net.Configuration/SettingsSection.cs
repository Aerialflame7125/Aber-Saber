using System.Configuration;

namespace System.Net.Configuration;

/// <summary>Represents the configuration section for sockets, IPv6, response headers, and service points. This class cannot be inherited.</summary>
public sealed class SettingsSection : ConfigurationSection
{
	private static ConfigurationPropertyCollection properties;

	private static ConfigurationProperty httpWebRequestProp;

	private static ConfigurationProperty ipv6Prop;

	private static ConfigurationProperty performanceCountersProp;

	private static ConfigurationProperty servicePointManagerProp;

	private static ConfigurationProperty webProxyScriptProp;

	private static ConfigurationProperty socketProp;

	/// <summary>Gets the configuration element that controls the settings used by an <see cref="T:System.Net.HttpWebRequest" /> object.</summary>
	/// <returns>The configuration element that controls the maximum response header length and other settings used by an <see cref="T:System.Net.HttpWebRequest" /> object.</returns>
	[ConfigurationProperty("httpWebRequest")]
	public HttpWebRequestElement HttpWebRequest => (HttpWebRequestElement)base[httpWebRequestProp];

	/// <summary>Gets the configuration element that enables Internet Protocol version 6 (IPv6).</summary>
	/// <returns>The configuration element that controls the setting used by IPv6.</returns>
	[ConfigurationProperty("ipv6")]
	public Ipv6Element Ipv6 => (Ipv6Element)base[ipv6Prop];

	/// <summary>Gets the configuration element that controls whether network performance counters are enabled.</summary>
	/// <returns>The configuration element that controls usage of network performance counters.</returns>
	[ConfigurationProperty("performanceCounters")]
	public PerformanceCountersElement PerformanceCounters => (PerformanceCountersElement)base[performanceCountersProp];

	/// <summary>Gets the configuration element that controls settings for connections to remote host computers.</summary>
	/// <returns>The configuration element that controls settings for connections to remote host computers.</returns>
	[ConfigurationProperty("servicePointManager")]
	public ServicePointManagerElement ServicePointManager => (ServicePointManagerElement)base[servicePointManagerProp];

	/// <summary>Gets the configuration element that controls settings for sockets.</summary>
	/// <returns>The configuration element that controls settings for sockets.</returns>
	[ConfigurationProperty("socket")]
	public SocketElement Socket => (SocketElement)base[socketProp];

	/// <summary>Gets the configuration element that controls the execution timeout and download timeout of Web proxy scripts.</summary>
	/// <returns>The configuration element that controls settings for the execution timeout and download timeout used by the Web proxy scripts.</returns>
	[ConfigurationProperty("webProxyScript")]
	public WebProxyScriptElement WebProxyScript => (WebProxyScriptElement)base[webProxyScriptProp];

	protected override ConfigurationPropertyCollection Properties => properties;

	static SettingsSection()
	{
		httpWebRequestProp = new ConfigurationProperty("httpWebRequest", typeof(HttpWebRequestElement));
		ipv6Prop = new ConfigurationProperty("ipv6", typeof(Ipv6Element));
		performanceCountersProp = new ConfigurationProperty("performanceCounters", typeof(PerformanceCountersElement));
		servicePointManagerProp = new ConfigurationProperty("servicePointManager", typeof(ServicePointManagerElement));
		socketProp = new ConfigurationProperty("socket", typeof(SocketElement));
		webProxyScriptProp = new ConfigurationProperty("webProxyScript", typeof(WebProxyScriptElement));
		properties = new ConfigurationPropertyCollection();
		properties.Add(httpWebRequestProp);
		properties.Add(ipv6Prop);
		properties.Add(performanceCountersProp);
		properties.Add(servicePointManagerProp);
		properties.Add(socketProp);
		properties.Add(webProxyScriptProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.ConnectionManagementSection" /> class.</summary>
	public SettingsSection()
	{
	}
}
