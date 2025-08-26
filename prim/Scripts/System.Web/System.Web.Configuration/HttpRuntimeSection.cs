using System.ComponentModel;
using System.Configuration;

namespace System.Web.Configuration;

/// <summary>Configures the ASP.NET HTTP runtime. This class cannot be inherited.</summary>
public sealed class HttpRuntimeSection : ConfigurationSection
{
	private static ConfigurationProperty apartmentThreadingProp;

	private static ConfigurationProperty appRequestQueueLimitProp;

	private static ConfigurationProperty delayNotificationTimeoutProp;

	private static ConfigurationProperty enableProp;

	private static ConfigurationProperty enableHeaderCheckingProp;

	private static ConfigurationProperty enableKernelOutputCacheProp;

	private static ConfigurationProperty enableVersionHeaderProp;

	private static ConfigurationProperty executionTimeoutProp;

	private static ConfigurationProperty maxRequestLengthProp;

	private static ConfigurationProperty maxWaitChangeNotificationProp;

	private static ConfigurationProperty minFreeThreadsProp;

	private static ConfigurationProperty minLocalRequestFreeThreadsProp;

	private static ConfigurationProperty requestLengthDiskThresholdProp;

	private static ConfigurationProperty requireRootedSaveAsPathProp;

	private static ConfigurationProperty sendCacheControlHeaderProp;

	private static ConfigurationProperty shutdownTimeoutProp;

	private static ConfigurationProperty useFullyQualifiedRedirectUrlProp;

	private static ConfigurationProperty waitChangeNotificationProp;

	private static ConfigurationProperty requestPathInvalidCharactersProp;

	private static ConfigurationProperty requestValidationTypeProp;

	private static ConfigurationProperty requestValidationModeProp;

	private static ConfigurationProperty maxQueryStringLengthProp;

	private static ConfigurationProperty maxUrlLengthProp;

	private static ConfigurationProperty encoderTypeProp;

	private static ConfigurationProperty relaxedUrlToFileSystemMappingProp;

	private static ConfigurationProperty targetFrameworkProp;

	private static ConfigurationProperty allowDynamicModuleRegistrationProp;

	private static ConfigurationPropertyCollection properties;

	/// <summary>Gets or sets a value that indicates whether application apartment threading is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if application apartment threading is enabled; otherwise, <see langword="false" />. </returns>
	[ConfigurationProperty("apartmentThreading", DefaultValue = "False")]
	public bool ApartmentThreading
	{
		get
		{
			return (bool)base[apartmentThreadingProp];
		}
		set
		{
			base[apartmentThreadingProp] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates the maximum number of requests that ASP.NET queues for the application.</summary>
	/// <returns>The maximum number of requests that can be queued.</returns>
	[IntegerValidator(MinValue = 1, MaxValue = int.MaxValue)]
	[ConfigurationProperty("appRequestQueueLimit", DefaultValue = "5000")]
	public int AppRequestQueueLimit
	{
		get
		{
			return (int)base[appRequestQueueLimitProp];
		}
		set
		{
			base[appRequestQueueLimitProp] = value;
		}
	}

	/// <summary>Gets or sets the change notification delay.</summary>
	/// <returns>The time, in seconds, that specifies the change notification delay.</returns>
	[TypeConverter(typeof(TimeSpanSecondsConverter))]
	[ConfigurationProperty("delayNotificationTimeout", DefaultValue = "00:00:05")]
	public TimeSpan DelayNotificationTimeout
	{
		get
		{
			return (TimeSpan)base[delayNotificationTimeoutProp];
		}
		set
		{
			base[delayNotificationTimeoutProp] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the application domain is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the application domain is enabled; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
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

	/// <summary>Gets or sets a value that indicates whether the header checking is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if the header checking is enabled; otherwise, <see langword="false" />. The default value is <see langword="true" />. </returns>
	[ConfigurationProperty("enableHeaderChecking", DefaultValue = "True")]
	public bool EnableHeaderChecking
	{
		get
		{
			return (bool)base[enableHeaderCheckingProp];
		}
		set
		{
			base[enableHeaderCheckingProp] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether output caching is enabled.</summary>
	/// <returns>
	///     <see langword="true" /> if output caching is enabled; otherwise, <see langword="false" />. The default value is <see langword="true" />. </returns>
	[ConfigurationProperty("enableKernelOutputCache", DefaultValue = "True")]
	public bool EnableKernelOutputCache
	{
		get
		{
			return (bool)base[enableKernelOutputCacheProp];
		}
		set
		{
			base[enableKernelOutputCacheProp] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether ASP.NET should output a version header.</summary>
	/// <returns>
	///     <see langword="true" /> if the output of the version header is enabled; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[ConfigurationProperty("enableVersionHeader", DefaultValue = "True")]
	public bool EnableVersionHeader
	{
		get
		{
			return (bool)base[enableVersionHeaderProp];
		}
		set
		{
			base[enableVersionHeaderProp] = value;
		}
	}

	/// <summary>Gets or sets the allowed execution time for the request.</summary>
	/// <returns>A <see cref="T:System.TimeSpan" /> value that indicates the allowed execution time for the request.</returns>
	/// <exception cref="T:System.Web.HttpException">The request execution time exceeded the limit set by the execution time-out.</exception>
	[TypeConverter(typeof(TimeSpanSecondsConverter))]
	[TimeSpanValidator(MinValueString = "00:00:00")]
	[ConfigurationProperty("executionTimeout", DefaultValue = "00:01:50")]
	public TimeSpan ExecutionTimeout
	{
		get
		{
			return (TimeSpan)base[executionTimeoutProp];
		}
		set
		{
			base[executionTimeoutProp] = value;
		}
	}

	/// <summary>Gets or sets the maximum request size.</summary>
	/// <returns>The maximum request size in kilobytes. The default size is 4096 KB (4 MB).</returns>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value is less than <see cref="P:System.Web.Configuration.HttpRuntimeSection.RequestLengthDiskThreshold" />.</exception>
	[IntegerValidator(MinValue = 0, MaxValue = int.MaxValue)]
	[ConfigurationProperty("maxRequestLength", DefaultValue = "4096")]
	public int MaxRequestLength
	{
		get
		{
			return (int)base[maxRequestLengthProp];
		}
		set
		{
			base[maxRequestLengthProp] = value;
		}
	}

	/// <summary>Gets or sets the time interval between the first change notification and the time at which the application domain is restarted.</summary>
	/// <returns>The maximum time interval, in seconds, from the first change notification and the time when the application domain is restarted.</returns>
	[IntegerValidator(MinValue = 0, MaxValue = int.MaxValue)]
	[ConfigurationProperty("maxWaitChangeNotification", DefaultValue = "0")]
	public int MaxWaitChangeNotification
	{
		get
		{
			return (int)base[maxWaitChangeNotificationProp];
		}
		set
		{
			base[maxWaitChangeNotificationProp] = value;
		}
	}

	/// <summary>Gets or sets the minimum number of threads that must be free before a request for resources in this configuration scope can be serviced.</summary>
	/// <returns>The minimum number of free threads in the common language runtime (CLR) thread pool before a request in this configuration scope will be serviced. The default value is 8.</returns>
	[IntegerValidator(MinValue = 0, MaxValue = int.MaxValue)]
	[ConfigurationProperty("minFreeThreads", DefaultValue = "8")]
	public int MinFreeThreads
	{
		get
		{
			return (int)base[minFreeThreadsProp];
		}
		set
		{
			base[minFreeThreadsProp] = value;
		}
	}

	/// <summary>Gets or sets the minimum number of free threads required to service a local request.</summary>
	/// <returns>The minimum number of free threads assigned to local requests. The default value is 4.</returns>
	[IntegerValidator(MinValue = 0, MaxValue = int.MaxValue)]
	[ConfigurationProperty("minLocalRequestFreeThreads", DefaultValue = "4")]
	public int MinLocalRequestFreeThreads
	{
		get
		{
			return (int)base[minLocalRequestFreeThreadsProp];
		}
		set
		{
			base[minLocalRequestFreeThreadsProp] = value;
		}
	}

	/// <summary>Gets or sets the input-stream buffering threshold.</summary>
	/// <returns>The number of bytes that indicate the input-stream buffering threshold. The default is 80 kilobytes.</returns>
	/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The selected value is greater than <see cref="P:System.Web.Configuration.HttpRuntimeSection.MaxRequestLength" />.</exception>
	[IntegerValidator(MinValue = 1, MaxValue = int.MaxValue)]
	[ConfigurationProperty("requestLengthDiskThreshold", DefaultValue = "80")]
	public int RequestLengthDiskThreshold
	{
		get
		{
			return (int)base[requestLengthDiskThresholdProp];
		}
		set
		{
			base[requestLengthDiskThresholdProp] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the file name must be a fully qualified physical file path.</summary>
	/// <returns>
	///     <see langword="true" /> if the file name must be a fully qualified physical file path; otherwise, <see langword="false" />. The default value is <see langword="true" />.</returns>
	[ConfigurationProperty("requireRootedSaveAsPath", DefaultValue = "True")]
	public bool RequireRootedSaveAsPath
	{
		get
		{
			return (bool)base[requireRootedSaveAsPathProp];
		}
		set
		{
			base[requireRootedSaveAsPathProp] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the cache-control:private header is sent as part of the HTTP response.</summary>
	/// <returns>
	///     <see langword="true" /> if the cache-control:private header is to be sent; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
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

	/// <summary>Gets or sets the length of time the application is allowed to idle before it is terminated.</summary>
	/// <returns>A <see cref="T:System.TimeSpan" /> value that indicates the length of time the application is allowed to idle before it is terminated.</returns>
	[TypeConverter(typeof(TimeSpanSecondsConverter))]
	[ConfigurationProperty("shutdownTimeout", DefaultValue = "00:01:30")]
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

	/// <summary>Gets or sets a value that indicates whether the client-side redirects are fully qualified.</summary>
	/// <returns>
	///     <see langword="true" /> if client-side redirects are fully qualified; otherwise, <see langword="false" />. The default value is <see langword="false" />.</returns>
	[ConfigurationProperty("useFullyQualifiedRedirectUrl", DefaultValue = "False")]
	public bool UseFullyQualifiedRedirectUrl
	{
		get
		{
			return (bool)base[useFullyQualifiedRedirectUrlProp];
		}
		set
		{
			base[useFullyQualifiedRedirectUrlProp] = value;
		}
	}

	/// <summary>Gets or sets the waiting time before the next change notification.</summary>
	/// <returns>The waiting time, in seconds, before the next change notification that triggers an application domain to restart. The default value is 0.</returns>
	[IntegerValidator(MinValue = 0, MaxValue = int.MaxValue)]
	[ConfigurationProperty("waitChangeNotification", DefaultValue = "0")]
	public int WaitChangeNotification
	{
		get
		{
			return (int)base[waitChangeNotificationProp];
		}
		set
		{
			base[waitChangeNotificationProp] = value;
		}
	}

	/// <summary>Gets or sets a list of characters that are specified as invalid in a path that is part of an HTTP request.</summary>
	/// <returns>A comma-separated list of invalid characters. The following list contains the default set of invalid characters: 
	///     &lt;,&gt;,*,%,&amp;,:,\\
	///   </returns>
	[ConfigurationProperty("requestPathInvalidCharacters", DefaultValue = ",*,%,&,:,\\,?")]
	public string RequestPathInvalidCharacters
	{
		get
		{
			return (string)base[requestPathInvalidCharactersProp];
		}
		set
		{
			base[requestPathInvalidCharactersProp] = value;
		}
	}

	/// <summary>Gets or sets the name of a type that is used to validate HTTP requests.</summary>
	/// <returns>The name of a type that handles request validation tasks. The default is the fully qualified name of the <see cref="T:System.Web.Util.RequestValidator" /> type that ASP.NET uses for validation.</returns>
	[ConfigurationProperty("requestValidationType", DefaultValue = "System.Web.Util.RequestValidator")]
	[StringValidator(MinLength = 1)]
	public string RequestValidationType
	{
		get
		{
			return (string)base[requestValidationTypeProp];
		}
		set
		{
			base[requestValidationTypeProp] = value;
		}
	}

	/// <summary>Gets or sets a version number that indicates which ASP.NET version-specific approach to validation will be used.</summary>
	/// <returns>A value that indicates which ASP.NET version-specific approach to validation will be used. The default is 4.5. </returns>
	[ConfigurationProperty("requestValidationMode", DefaultValue = "4.0")]
	[TypeConverter("System.Web.Configuration.VersionConverter")]
	public Version RequestValidationMode
	{
		get
		{
			return (Version)base[requestValidationModeProp];
		}
		set
		{
			base[requestValidationModeProp] = value;
		}
	}

	/// <summary>Gets or sets the maximum possible length, in number of characters, of a query string in an HTTP request.</summary>
	/// <returns>The maximum length of the query string, in number of characters. The default is 2048. </returns>
	[IntegerValidator(MinValue = 0)]
	[ConfigurationProperty("maxQueryStringLength", DefaultValue = "2048")]
	public int MaxQueryStringLength
	{
		get
		{
			return (int)base[maxQueryStringLengthProp];
		}
		set
		{
			base[maxQueryStringLengthProp] = value;
		}
	}

	/// <summary>Gets or sets the maximum possible length, in number of characters, of the URL in an HTTP request.</summary>
	/// <returns>The length of the URL, in number of characters. The default is 260.</returns>
	[IntegerValidator(MinValue = 0)]
	[ConfigurationProperty("maxUrlLength", DefaultValue = "260")]
	public int MaxUrlLength
	{
		get
		{
			return (int)base[maxUrlLengthProp];
		}
		set
		{
			base[maxUrlLengthProp] = value;
		}
	}

	/// <summary>Gets or sets the name of a custom type that can be used to handle HTML and URL encoding. </summary>
	/// <returns>The name of a type that can be used to handle HTML and URL encoding. </returns>
	[StringValidator(MinLength = 1)]
	[ConfigurationProperty("encoderType", DefaultValue = "System.Web.Util.HttpEncoder")]
	public string EncoderType
	{
		get
		{
			return (string)base[encoderTypeProp];
		}
		set
		{
			base[encoderTypeProp] = value;
		}
	}

	/// <summary>Gets or sets a value that indicates whether the URL in an HTTP request is required to be a valid Windows file path.</summary>
	/// <returns>
	///     <see langword="true" /> if the URL does not have to comply with Windows path rules; otherwise <see langword="false" />. The default is <see langword="false" />. </returns>
	[ConfigurationProperty("relaxedUrlToFileSystemMapping", DefaultValue = "False")]
	public bool RelaxedUrlToFileSystemMapping
	{
		get
		{
			return (bool)base[relaxedUrlToFileSystemMappingProp];
		}
		set
		{
			base[relaxedUrlToFileSystemMappingProp] = value;
		}
	}

	/// <summary>Gets or sets the target .NET framework.</summary>
	/// <returns>The target .NET framework.</returns>
	[ConfigurationProperty("targetFramework", DefaultValue = "4.0")]
	[TypeConverter("System.Web.Configuration.VersionConverter")]
	public Version TargetFramework
	{
		get
		{
			return (Version)base[targetFrameworkProp];
		}
		set
		{
			base[targetFrameworkProp] = value;
		}
	}

	protected override ConfigurationPropertyCollection Properties
	{
		protected internal get
		{
			return properties;
		}
	}

	/// <summary>Gets or sets a value that indicates whether <see cref="M:System.Web.HttpApplication.RegisterModule(System.Type)" /> method calls are allowed. The default is <see langword="true" />.</summary>
	/// <returns>
	///     <see langword="true" /> if <see cref="M:System.Web.HttpApplication.RegisterModule(System.Type)" /> method calls are allowed; otherwise, <see langword="false" />.</returns>
	[ConfigurationProperty("allowDynamicModuleRegistration", DefaultValue = "True")]
	public bool AllowDynamicModuleRegistration
	{
		get
		{
			return (bool)base[allowDynamicModuleRegistrationProp];
		}
		set
		{
			base[allowDynamicModuleRegistrationProp] = value;
		}
	}

	static HttpRuntimeSection()
	{
		apartmentThreadingProp = new ConfigurationProperty("apartmentThreading", typeof(bool), false);
		appRequestQueueLimitProp = new ConfigurationProperty("appRequestQueueLimit", typeof(int), 5000, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);
		delayNotificationTimeoutProp = new ConfigurationProperty("delayNotificationTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(5.0), PropertyHelper.TimeSpanSecondsConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		enableProp = new ConfigurationProperty("enable", typeof(bool), true);
		enableHeaderCheckingProp = new ConfigurationProperty("enableHeaderChecking", typeof(bool), true);
		enableKernelOutputCacheProp = new ConfigurationProperty("enableKernelOutputCache", typeof(bool), true);
		enableVersionHeaderProp = new ConfigurationProperty("enableVersionHeader", typeof(bool), true);
		executionTimeoutProp = new ConfigurationProperty("executionTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(110.0), PropertyHelper.TimeSpanSecondsConverter, PropertyHelper.PositiveTimeSpanValidator, ConfigurationPropertyOptions.None);
		maxRequestLengthProp = new ConfigurationProperty("maxRequestLength", typeof(int), 4096, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);
		maxWaitChangeNotificationProp = new ConfigurationProperty("maxWaitChangeNotification", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);
		minFreeThreadsProp = new ConfigurationProperty("minFreeThreads", typeof(int), 8, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);
		minLocalRequestFreeThreadsProp = new ConfigurationProperty("minLocalRequestFreeThreads", typeof(int), 4, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);
		requestLengthDiskThresholdProp = new ConfigurationProperty("requestLengthDiskThreshold", typeof(int), 80, TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(1, int.MaxValue), ConfigurationPropertyOptions.None);
		requireRootedSaveAsPathProp = new ConfigurationProperty("requireRootedSaveAsPath", typeof(bool), true);
		sendCacheControlHeaderProp = new ConfigurationProperty("sendCacheControlHeader", typeof(bool), true);
		shutdownTimeoutProp = new ConfigurationProperty("shutdownTimeout", typeof(TimeSpan), TimeSpan.FromSeconds(90.0), PropertyHelper.TimeSpanSecondsConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		useFullyQualifiedRedirectUrlProp = new ConfigurationProperty("useFullyQualifiedRedirectUrl", typeof(bool), false);
		waitChangeNotificationProp = new ConfigurationProperty("waitChangeNotification", typeof(int), 0, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);
		requestPathInvalidCharactersProp = new ConfigurationProperty("requestPathInvalidCharacters", typeof(string), "<,>,*,%,&,:,\\,?");
		requestValidationTypeProp = new ConfigurationProperty("requestValidationType", typeof(string), "System.Web.Util.RequestValidator", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);
		requestValidationModeProp = new ConfigurationProperty("requestValidationMode", typeof(Version), new Version(4, 0), PropertyHelper.VersionConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		maxQueryStringLengthProp = new ConfigurationProperty("maxQueryStringLength", typeof(int), 2048, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);
		maxUrlLengthProp = new ConfigurationProperty("maxUrlLength", typeof(int), 260, TypeDescriptor.GetConverter(typeof(int)), PropertyHelper.IntFromZeroToMaxValidator, ConfigurationPropertyOptions.None);
		encoderTypeProp = new ConfigurationProperty("encoderType", typeof(string), "System.Web.Util.HttpEncoder", TypeDescriptor.GetConverter(typeof(string)), PropertyHelper.NonEmptyStringValidator, ConfigurationPropertyOptions.None);
		relaxedUrlToFileSystemMappingProp = new ConfigurationProperty("relaxedUrlToFileSystemMapping", typeof(bool), false);
		targetFrameworkProp = new ConfigurationProperty("targetFramework", typeof(Version), new Version(4, 0), PropertyHelper.VersionConverter, PropertyHelper.DefaultValidator, ConfigurationPropertyOptions.None);
		allowDynamicModuleRegistrationProp = new ConfigurationProperty("allowDynamicModuleRegistration", typeof(bool), true, ConfigurationPropertyOptions.None);
		properties = new ConfigurationPropertyCollection();
		properties.Add(apartmentThreadingProp);
		properties.Add(appRequestQueueLimitProp);
		properties.Add(delayNotificationTimeoutProp);
		properties.Add(enableProp);
		properties.Add(enableHeaderCheckingProp);
		properties.Add(enableKernelOutputCacheProp);
		properties.Add(enableVersionHeaderProp);
		properties.Add(executionTimeoutProp);
		properties.Add(maxRequestLengthProp);
		properties.Add(maxWaitChangeNotificationProp);
		properties.Add(minFreeThreadsProp);
		properties.Add(minLocalRequestFreeThreadsProp);
		properties.Add(requestLengthDiskThresholdProp);
		properties.Add(requireRootedSaveAsPathProp);
		properties.Add(sendCacheControlHeaderProp);
		properties.Add(shutdownTimeoutProp);
		properties.Add(useFullyQualifiedRedirectUrlProp);
		properties.Add(waitChangeNotificationProp);
		properties.Add(requestPathInvalidCharactersProp);
		properties.Add(requestValidationTypeProp);
		properties.Add(requestValidationModeProp);
		properties.Add(maxQueryStringLengthProp);
		properties.Add(maxUrlLengthProp);
		properties.Add(encoderTypeProp);
		properties.Add(relaxedUrlToFileSystemMappingProp);
		properties.Add(targetFrameworkProp);
		properties.Add(allowDynamicModuleRegistrationProp);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Configuration.HttpRuntimeSection" /> class using default settings.</summary>
	public HttpRuntimeSection()
	{
	}
}
