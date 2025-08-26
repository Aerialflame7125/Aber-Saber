using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the ASP.NET process model settings on an Internet Information Services (IIS) Web server. This class cannot be inherited.</summary>
public sealed class ProcessModelSection : ConfigurationSection
{
	private static ConfigurationProperty autoConfigProp;

	private static ConfigurationProperty clientConnectedCheckProp;

	private static ConfigurationProperty comAuthenticationLevelProp;

	private static ConfigurationProperty comImpersonationLevelProp;

	private static ConfigurationProperty cpuMaskProp;

	private static ConfigurationProperty enableProp;

	private static ConfigurationProperty idleTimeoutProp;

	private static ConfigurationProperty logLevelProp;

	private static ConfigurationProperty maxAppDomainsProp;

	private static ConfigurationProperty maxIoThreadsProp;

	private static ConfigurationProperty maxWorkerThreadsProp;

	private static ConfigurationProperty memoryLimitProp;

	private static ConfigurationProperty minIoThreadsProp;

	private static ConfigurationProperty minWorkerThreadsProp;

	private static ConfigurationProperty passwordProp;

	private static ConfigurationProperty pingFrequencyProp;

	private static ConfigurationProperty pingTimeoutProp;

	private static ConfigurationProperty requestLimitProp;

	private static ConfigurationProperty requestQueueLimitProp;

	private static ConfigurationProperty responseDeadlockIntervalProp;

	private static ConfigurationProperty responseRestartDeadlockIntervalProp;

	private static ConfigurationProperty restartQueueLimitProp;

	private static ConfigurationProperty serverErrorMessageFileProp;

	private static ConfigurationProperty shutdownTimeoutProp;

	private static ConfigurationProperty timeoutProp;

	private static ConfigurationProperty userNameProp;

	private static ConfigurationProperty webGardenProp;

	private static ConfigurationPropertyCollection properties;

	private static ConfigurationElementProperty elementProperty;

	protected override ConfigurationElementProperty ElementProperty
	{
		protected internal get
		{
			return elementProperty;
		}
	}

	/// <summary>Gets or sets a value indicating whether ASP.NET performance settings are automatically configured for ASP.NET applications. </summary>
	/// <returns>
	///     <see langword="true" /> if performance settings are automatically configured for ASP.NET applications; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[ConfigurationProperty("autoConfig", DefaultValue = "False")]
	public bool AutoConfig
	{
		get
		{
			return (bool)base[autoConfigProp];
		}
		set
		{
			base[autoConfigProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating how long a request is left in the queue. </summary>
	/// <returns>A <see cref="T:System.TimeSpan" /> value indicating the queuing time. The default value is 5 seconds.</returns>
	[TypeConverter(typeof(InfiniteTimeSpanConverter))]
	[ConfigurationProperty("clientConnectedCheck", DefaultValue = "00:00:05")]
	public TimeSpan ClientConnectedCheck
	{
		get
		{
			return (TimeSpan)base[clientConnectedCheckProp];
		}
		set
		{
			base[clientConnectedCheckProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the level of authentication for DCOM security.</summary>
	/// <returns>One of the <see cref="T:System.Web.Configuration.ProcessModelComAuthenticationLevel" /> values. The default value is <see cref="F:System.Web.Configuration.ProcessModelComAuthenticationLevel.Connect" />.</returns>
	[ConfigurationProperty("comAuthenticationLevel", DefaultValue = "Connect")]
	public ProcessModelComAuthenticationLevel ComAuthenticationLevel
	{
		get
		{
			return (ProcessModelComAuthenticationLevel)base[comAuthenticationLevelProp];
		}
		set
		{
			base[comAuthenticationLevelProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the level of authentication for COM security.</summary>
	/// <returns>One of the <see cref="T:System.Web.Configuration.ProcessModelComImpersonationLevel" /> values. The default value is <see cref="F:System.Web.Configuration.ProcessModelComImpersonationLevel.Impersonate" />. </returns>
	[ConfigurationProperty("comImpersonationLevel", DefaultValue = "Impersonate")]
	public ProcessModelComImpersonationLevel ComImpersonationLevel
	{
		get
		{
			return (ProcessModelComImpersonationLevel)base[comImpersonationLevelProp];
		}
		set
		{
			base[comImpersonationLevelProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating which processors on a multiprocessor server are eligible to run ASP.NET processes. </summary>
	/// <returns>The number representing the bit pattern to apply. The default value is 0xFFFFFFFF.</returns>
	[ConfigurationProperty("cpuMask", DefaultValue = "0xffffffff")]
	public int CpuMask
	{
		get
		{
			return (int)base[cpuMaskProp];
		}
		set
		{
			base[cpuMaskProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the process model is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the process model is enabled; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[ConfigurationProperty("enable", DefaultValue = "True")]
	public bool Enable
	{
		get
		{
			return (bool)base[enableProp];
		}
		set
		{
			base[enableProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the period of inactivity after which ASP.NET automatically ends the worker process.</summary>
	/// <returns>A <see cref="T:System.TimeSpan" /> value indicating the idle time. The default value is Infinite, which corresponds to <see cref="F:System.TimeSpan.MaxValue" />. </returns>
	[TypeConverter(typeof(InfiniteTimeSpanConverter))]
	[ConfigurationProperty("idleTimeout", DefaultValue = "10675199.02:48:05.4775807")]
	public TimeSpan IdleTimeout
	{
		get
		{
			return (TimeSpan)base[idleTimeoutProp];
		}
		set
		{
			base[idleTimeoutProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the event types to be logged to the event log.</summary>
	/// <returns>One of the <see cref="T:System.Web.Configuration.ProcessModelLogLevel" /> values. The default value is <see cref="F:System.Web.Configuration.ProcessModelLogLevel.Errors" />. </returns>
	[ConfigurationProperty("logLevel", DefaultValue = "Errors")]
	public ProcessModelLogLevel LogLevel
	{
		get
		{
			return (ProcessModelLogLevel)base[logLevelProp];
		}
		set
		{
			base[logLevelProp] = value;
		}
	}

	/// <summary>Gets or sets the maximum allowed number of application domains in one process.</summary>
	/// <returns>The maximum allowed number of application domains in one process.</returns>
	[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
	[ConfigurationProperty("maxAppDomains", DefaultValue = "2000")]
	public int MaxAppDomains
	{
		get
		{
			return (int)base[maxAppDomainsProp];
		}
		set
		{
			base[maxAppDomainsProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the maximum number of I/O threads per CPU in the CLR thread pool. </summary>
	/// <returns>The maximum number of threads. The default is 20.</returns>
	[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
	[ConfigurationProperty("maxIoThreads", DefaultValue = "20")]
	public int MaxIOThreads
	{
		get
		{
			return (int)base[maxIoThreadsProp];
		}
		set
		{
			base[maxIoThreadsProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the maximum amount of worker threads per CPU in the CLR thread pool. </summary>
	/// <returns>The maximum number of threads. The default is 20.</returns>
	[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
	[ConfigurationProperty("maxWorkerThreads", DefaultValue = "20")]
	public int MaxWorkerThreads
	{
		get
		{
			return (int)base[maxWorkerThreadsProp];
		}
		set
		{
			base[maxWorkerThreadsProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the maximum allowed memory size.</summary>
	/// <returns>The percentage of the total system memory. The default is 60 percent. </returns>
	[ConfigurationProperty("memoryLimit", DefaultValue = "60")]
	public int MemoryLimit
	{
		get
		{
			return (int)base[memoryLimitProp];
		}
		set
		{
			base[memoryLimitProp] = value;
		}
	}

	/// <summary>Gets or sets the minimum number of I/O threads per CPU in the CLR thread pool.</summary>
	/// <returns>The minimum number of I/O threads per CPU in the CLR thread pool.</returns>
	[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
	[ConfigurationProperty("minIoThreads", DefaultValue = "1")]
	public int MinIOThreads
	{
		get
		{
			return (int)base[minIoThreadsProp];
		}
		set
		{
			base[minIoThreadsProp] = value;
		}
	}

	/// <summary>Gets or sets the minimum number of worker threads per CPU in the CLR thread pool.</summary>
	/// <returns>The minimum number of worker threads per CPU in the CLR thread pool</returns>
	[IntegerValidator(MinValue = 1, MaxValue = 2147483646)]
	[ConfigurationProperty("minWorkerThreads", DefaultValue = "1")]
	public int MinWorkerThreads
	{
		get
		{
			return (int)base[minWorkerThreadsProp];
		}
		set
		{
			base[minWorkerThreadsProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the password to use for the Windows identity.</summary>
	/// <returns>The password to use. The default value is AutoGenerate.</returns>
	[ConfigurationProperty("password", DefaultValue = "AutoGenerate")]
	public string Password
	{
		get
		{
			return (string)base[passwordProp];
		}
		set
		{
			base[passwordProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the time interval at which the ISAPI extension pings the worker process to determine whether it is running.</summary>
	/// <returns>The <see cref="T:System.TimeSpan" /> defining the time interval. The default is 30 seconds.</returns>
	[TypeConverter(typeof(InfiniteTimeSpanConverter))]
	[ConfigurationProperty("pingFrequency", DefaultValue = "10675199.02:48:05.4775807")]
	public TimeSpan PingFrequency
	{
		get
		{
			return (TimeSpan)base[pingFrequencyProp];
		}
		set
		{
			base[pingFrequencyProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the time interval after which a non-responsive worker process is restarted.</summary>
	/// <returns>The <see cref="T:System.TimeSpan" /> defining the time interval. The default is 5 seconds.</returns>
	[TypeConverter(typeof(InfiniteTimeSpanConverter))]
	[ConfigurationProperty("pingTimeout", DefaultValue = "10675199.02:48:05.4775807")]
	public TimeSpan PingTimeout
	{
		get
		{
			return (TimeSpan)base[pingTimeoutProp];
		}
		set
		{
			base[pingTimeoutProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the number of requests allowed before a worker process is recycled.</summary>
	/// <returns>The number of allowed requests. The default is Infinite.</returns>
	[TypeConverter(typeof(InfiniteIntConverter))]
	[IntegerValidator(MinValue = 0, MaxValue = int.MaxValue)]
	[ConfigurationProperty("requestLimit", DefaultValue = "2147483647")]
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

	/// <summary>Gets or sets a value indicating the number of requests allowed in the queue.</summary>
	/// <returns>The number of requests allowed to be queued. The default is 5000.</returns>
	[TypeConverter(typeof(InfiniteIntConverter))]
	[IntegerValidator(MinValue = 0, MaxValue = int.MaxValue)]
	[ConfigurationProperty("requestQueueLimit", DefaultValue = "5000")]
	public int RequestQueueLimit
	{
		get
		{
			return (int)base[requestQueueLimitProp];
		}
		set
		{
			base[requestQueueLimitProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the time interval for the worker process to respond.</summary>
	/// <returns>The <see cref="T:System.TimeSpan" /> defining the interval. The default is 3 minutes.</returns>
	[TypeConverter(typeof(InfiniteTimeSpanConverter))]
	[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
	[ConfigurationProperty("responseDeadlockInterval", DefaultValue = "00:03:00")]
	public TimeSpan ResponseDeadlockInterval
	{
		get
		{
			return (TimeSpan)base[responseDeadlockIntervalProp];
		}
		set
		{
			base[responseDeadlockIntervalProp] = value;
		}
	}

	/// <summary>No longer used.</summary>
	/// <returns>Not applicable.</returns>
	[TypeConverter(typeof(InfiniteTimeSpanConverter))]
	[ConfigurationProperty("responseRestartDeadlockInterval", DefaultValue = "00:03:00")]
	public TimeSpan ResponseRestartDeadlockInterval
	{
		get
		{
			return (TimeSpan)base[responseRestartDeadlockIntervalProp];
		}
		set
		{
			base[responseRestartDeadlockIntervalProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the maximum number of requests queued by the ISAPI while waiting for a new worker process to start handling the requests.</summary>
	/// <returns>The number of requests queued. The default is 10. </returns>
	[TypeConverter(typeof(InfiniteIntConverter))]
	[IntegerValidator(MinValue = 0, MaxValue = int.MaxValue)]
	[ConfigurationProperty("restartQueueLimit", DefaultValue = "10")]
	public int RestartQueueLimit
	{
		get
		{
			return (int)base[restartQueueLimitProp];
		}
		set
		{
			base[restartQueueLimitProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the file whose content must be used when a fatal error occurs.</summary>
	/// <returns>The path of the file used when a fatal error occurs.</returns>
	[ConfigurationProperty("serverErrorMessageFile", DefaultValue = "")]
	public string ServerErrorMessageFile
	{
		get
		{
			return (string)base[serverErrorMessageFileProp];
		}
		set
		{
			base[serverErrorMessageFileProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the time allowed for the worker process to shut down. </summary>
	/// <returns>The <see cref="T:System.TimeSpan" /> defining the interval. The default is 5 seconds.</returns>
	[TypeConverter(typeof(InfiniteTimeSpanConverter))]
	[TimeSpanValidator(MinValueString = "00:00:00", MaxValueString = "10675199.02:48:05.4775807")]
	[ConfigurationProperty("shutdownTimeout", DefaultValue = "00:00:05")]
	public TimeSpan ShutdownTimeout
	{
		get
		{
			return (TimeSpan)base[shutdownTimeoutProp];
		}
		set
		{
			base[shutdownTimeoutProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the number of minutes until ASP.NET launches a new worker process.</summary>
	/// <returns>The <see cref="T:System.TimeSpan" /> defining the interval. The default is Infinite.</returns>
	[TypeConverter(typeof(InfiniteTimeSpanConverter))]
	[ConfigurationProperty("timeout", DefaultValue = "10675199.02:48:05.4775807")]
	public TimeSpan Timeout
	{
		get
		{
			return (TimeSpan)base[timeoutProp];
		}
		set
		{
			base[timeoutProp] = value;
		}
	}

	/// <summary>Gets or sets a value indicating the user name for a Windows identity.</summary>
	/// <returns>The user name. The default value is Machine. </returns>
	[ConfigurationProperty("userName", DefaultValue = "machine")]
	public string UserName
	{
		get
		{
			return (string)base[userNameProp];
		}
		set
		{
			base[userNameProp] = value;
		}
	}

	/// <summary>Gets or sets a value enabling the available CPUs to run the worker processes.</summary>
	/// <returns>
	///     <see langword="true" />, if <see cref="P:System.Web.Configuration.ProcessModelSection.CpuMask" /> is used to map the worker processes to the number of eligible CPUs; <see langword="false" /> if <see cref="P:System.Web.Configuration.ProcessModelSection.CpuMask" /> is ignored.</returns>
	[ConfigurationProperty("webGarden", DefaultValue = "False")]
	public bool WebGarden
	{
		get
		{
			return (bool)base[webGardenProp];
		}
		set
		{
			base[webGardenProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	static ProcessModelSection()
	{
		autoConfigProp = new ConfigurationProperty("autoConfig", typeof(bool), false);
		clientConnectedCheckProp = new ConfigurationProperty("clientConnectedCheck", typeof(TimeSpan), TimeSpan.FromSeconds(5.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		comAuthenticationLevelProp = new ConfigurationProperty("comAuthenticationLevel", typeof(ProcessModelComAuthenticationLevel), ProcessModelComAuthenticationLevel.Connect, new GenericEnumConverter(typeof(ProcessModelComAuthenticationLevel)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		comImpersonationLevelProp = new ConfigurationProperty("comImpersonationLevel", typeof(ProcessModelComImpersonationLevel), ProcessModelComImpersonationLevel.Impersonate, new GenericEnumConverter(typeof(ProcessModelComImpersonationLevel)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		cpuMaskProp = new ConfigurationProperty("cpuMask", typeof(int), 268435455);
		enableProp = new ConfigurationProperty("enable", typeof(bool), true);
		idleTimeoutProp = new ConfigurationProperty("idleTimeout", typeof(TimeSpan), TimeSpan.MaxValue, PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		logLevelProp = new ConfigurationProperty("logLevel", typeof(ProcessModelLogLevel), ProcessModelLogLevel.Errors, new GenericEnumConverter(typeof(ProcessModelLogLevel)), PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		maxAppDomainsProp = new ConfigurationProperty("maxAppDomains", typeof(int), 2000, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromOneToMax_1Validator, ConfigurationPropertyOptions.None);
		maxIoThreadsProp = new ConfigurationProperty("maxIoThreads", typeof(int), 20, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromOneToMax_1Validator, ConfigurationPropertyOptions.None);
		maxWorkerThreadsProp = new ConfigurationProperty("maxWorkerThreads", typeof(int), 20, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromOneToMax_1Validator, ConfigurationPropertyOptions.None);
		memoryLimitProp = new ConfigurationProperty("memoryLimit", typeof(int), 60);
		minIoThreadsProp = new ConfigurationProperty("minIoThreads", typeof(int), 1, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromOneToMax_1Validator, ConfigurationPropertyOptions.None);
		minWorkerThreadsProp = new ConfigurationProperty("minWorkerThreads", typeof(int), 1, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromOneToMax_1Validator, ConfigurationPropertyOptions.None);
		passwordProp = new ConfigurationProperty("password", typeof(string), "AutoGenerate");
		pingFrequencyProp = new ConfigurationProperty("pingFrequency", typeof(TimeSpan), TimeSpan.MaxValue, PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		pingTimeoutProp = new ConfigurationProperty("pingTimeout", typeof(TimeSpan), TimeSpan.MaxValue, PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		requestLimitProp = new ConfigurationProperty("requestLimit", typeof(int), int.MaxValue, PropertyHelper.InfiniteIntConverter, PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);
		requestQueueLimitProp = new ConfigurationProperty("requestQueueLimit", typeof(int), 5000, PropertyHelper.InfiniteIntConverter, PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);
		responseDeadlockIntervalProp = new ConfigurationProperty("responseDeadlockInterval", typeof(TimeSpan), TimeSpan.FromMinutes(3.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);
		responseRestartDeadlockIntervalProp = new ConfigurationProperty("responseRestartDeadlockInterval", typeof(TimeSpan), TimeSpan.FromMinutes(3.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		restartQueueLimitProp = new ConfigurationProperty("restartQueueLimit", typeof(int), 10, PropertyHelper.InfiniteIntConverter, PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);
		serverErrorMessageFileProp = new ConfigurationProperty("serverErrorMessageFile", typeof(string), "");
		shutdownTimeoutProp = new ConfigurationProperty("shutdownTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(5.0), PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);
		timeoutProp = new ConfigurationProperty("timeout", typeof(TimeSpan), TimeSpan.MaxValue, PropertyHelper.InfiniteTimeSpanConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		userNameProp = new ConfigurationProperty("userName", typeof(string), "machine");
		webGardenProp = new ConfigurationProperty("webGarden", typeof(bool), false);
		properties = new ConfigurationPropertyCollection();
		properties.Add(autoConfigProp);
		properties.Add(clientConnectedCheckProp);
		properties.Add(comAuthenticationLevelProp);
		properties.Add(comImpersonationLevelProp);
		properties.Add(cpuMaskProp);
		properties.Add(enableProp);
		properties.Add(idleTimeoutProp);
		properties.Add(logLevelProp);
		properties.Add(maxAppDomainsProp);
		properties.Add(maxIoThreadsProp);
		properties.Add(maxWorkerThreadsProp);
		properties.Add(memoryLimitProp);
		properties.Add(minIoThreadsProp);
		properties.Add(minWorkerThreadsProp);
		properties.Add(passwordProp);
		properties.Add(pingFrequencyProp);
		properties.Add(pingTimeoutProp);
		properties.Add(requestLimitProp);
		properties.Add(requestQueueLimitProp);
		properties.Add(responseDeadlockIntervalProp);
		properties.Add(responseRestartDeadlockIntervalProp);
		properties.Add(restartQueueLimitProp);
		properties.Add(serverErrorMessageFileProp);
		properties.Add(shutdownTimeoutProp);
		properties.Add(timeoutProp);
		properties.Add(userNameProp);
		properties.Add(webGardenProp);
		elementProperty = new ConfigurationElementProperty(new CallbackValidator(typeof(ProcessModelSection), ValidateElement));
	}

	private static void ValidateElement(object o)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.ProcessModelSection" /> class using default settings.</summary>
	public ProcessModelSection()
	{
	}
}
