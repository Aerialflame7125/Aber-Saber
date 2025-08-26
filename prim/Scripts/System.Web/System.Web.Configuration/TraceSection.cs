using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the ASP.NET trace service. This class cannot be inherited.</summary>
public sealed class TraceSection : ConfigurationSection
{
	private static ConfigurationProperty enabledProp;

	private static ConfigurationProperty localOnlyProp;

	private static ConfigurationProperty mostRecentProp;

	private static ConfigurationProperty pageOutputProp;

	private static ConfigurationProperty requestLimitProp;

	private static ConfigurationProperty traceModeProp;

	private static ConfigurationProperty writeToDiagnosticsTraceProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets a value indicating whether the ASP.NET trace service is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if trace is enabled; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("enabled", DefaultValue = "False")]
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

	/// <summary>Gets or sets a value indicating whether the ASP.NET trace viewer (Trace.axd) is available only to requests from the host Web server.</summary>
	/// <returns>
	///     <see langword="true" /> if the ASP.NET trace viewer (Trace.axd) is available only to requests from the host Web server; otherwise, <see langword="false" />. The default is <see langword="true" />.</returns>
	[ConfigurationProperty("localOnly", DefaultValue = "True")]
	public bool LocalOnly
	{
		get
		{
			return (bool)base[localOnlyProp];
		}
		set
		{
			base[localOnlyProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the most recent requests are always stored on the server.</summary>
	/// <returns>
	///     <see langword="true" /> if the most recent requests are always stored in the trace log; otherwise, <see langword="false" />. The default is<see langword=" false" />.</returns>
	[ConfigurationProperty("mostRecent", DefaultValue = "False")]
	public bool MostRecent
	{
		get
		{
			return (bool)base[mostRecentProp];
		}
		set
		{
			base[mostRecentProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the ASP.NET trace information is appended to the output of each page.</summary>
	/// <returns>
	///     <see langword="true" /> if the trace information is appended to each page; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	[ConfigurationProperty("pageOutput", DefaultValue = "False")]
	public bool PageOutput
	{
		get
		{
			return (bool)base[pageOutputProp];
		}
		set
		{
			base[pageOutputProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the maximum number of requests to the application for which ASP.NET stores trace information. </summary>
	/// <returns>The maximum number of requests to store on the server. The default is 10.</returns>
	[IntegerValidator(MinValue = 0, MaxValue = int.MaxValue)]
	[ConfigurationProperty("requestLimit", DefaultValue = "10")]
	public int RequestLimit
	{
		get
		{
			return (int)base[requestLimitProp];
		}
		set
		{
			base[requestLimitProp] = value;
		}
	}

	/// <summary>Gets or sets the order in which ASP.NET trace information is displayed.</summary>
	/// <returns>One of the <see cref="T:System.Web.Configuration.TraceDisplayMode" /> values, indicating the order in which trace information is displayed.</returns>
	[ConfigurationProperty("traceMode", DefaultValue = "SortByTime")]
	public TraceDisplayMode TraceMode
	{
		get
		{
			return (TraceDisplayMode)base[traceModeProp];
		}
		set
		{
			base[traceModeProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the messages emitted through the page trace are forwarded to an instance of the <see cref="T:System.Diagnostics.Trace" /> class.</summary>
	/// <returns>
	///     <see langword="true" /> if the trace messages are sent to the <see cref="T:System.Diagnostics.Trace" /> class; otherwise, <see langword="false" />. The default is<see langword=" false" />.</returns>
	[ConfigurationProperty("writeToDiagnosticsTrace", DefaultValue = "False")]
	public bool WriteToDiagnosticsTrace
	{
		get
		{
			return (bool)base[writeToDiagnosticsTraceProp];
		}
		set
		{
			base[writeToDiagnosticsTraceProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static TraceSection()
	{
		enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);
		localOnlyProp = new ConfigurationProperty("localOnly", typeof(bool), true);
		mostRecentProp = new ConfigurationProperty("mostRecent", typeof(bool), false);
		pageOutputProp = new ConfigurationProperty("pageOutput", typeof(bool), false);
		requestLimitProp = new ConfigurationProperty("requestLimit", typeof(int), 10, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);
		traceModeProp = new ConfigurationProperty("traceMode", typeof(TraceDisplayMode), TraceDisplayMode.SortByTime, new GenericEnumConverter(typeof(TraceDisplayMode)), null, ConfigurationPropertyOptions.None);
		writeToDiagnosticsTraceProp = new ConfigurationProperty("writeToDiagnosticsTrace", typeof(bool), false);
		properties = new ConfigurationPropertyCollection();
		properties.Add(enabledProp);
		properties.Add(localOnlyProp);
		properties.Add(mostRecentProp);
		properties.Add(pageOutputProp);
		properties.Add(requestLimitProp);
		properties.Add(traceModeProp);
		properties.Add(writeToDiagnosticsTraceProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.TraceSection" /> class using default settings.</summary>
	public TraceSection()
	{
	}
}
