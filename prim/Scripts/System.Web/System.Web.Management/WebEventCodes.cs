namespace System.Web.Management;

/// <summary>Defines the codes associated with the ASP.NET health monitoring events.</summary>
public sealed class WebEventCodes
{
	/// <summary>Represents the event code indicating that the event code value is not allowed. This field is constant.</summary>
	public const int InvalidEventCode = -1;

	/// <summary>Represents the event code indicating that the major event code value is not defined. This field is constant.</summary>
	public const int UndefinedEventCode = 0;

	/// <summary>Represents the event code indicating that the detail event code value is not defined. This field is constant.</summary>
	public const int UndefinedEventDetailCode = 0;

	/// <summary>Identifies the offset for the ASP.NET health-monitoring application event codes. This field is constant.</summary>
	public const int ApplicationCodeBase = 1000;

	/// <summary>Represents the event code indicating that an application has started. This field is constant.</summary>
	public const int ApplicationStart = 1001;

	/// <summary>Represents the event code indicating that an application has shut down. This field is constant.</summary>
	public const int ApplicationShutdown = 1002;

	/// <summary>Represents the event code indicating that the compilation of the application has started. This field is constant. </summary>
	public const int ApplicationCompilationStart = 1003;

	/// <summary>Represents the event code indicating that the compilation of the application has finished. This field is constant. </summary>
	public const int ApplicationCompilationEnd = 1004;

	/// <summary>Represents the event code indicating that a heartbeat event occurred. This field is constant.</summary>
	public const int ApplicationHeartbeat = 1005;

	/// <summary>Identifies the offset for the ASP.NET health-monitoring Web-request event codes. This field is constant.</summary>
	public const int RequestCodeBase = 2000;

	/// <summary>Represents the event code indicating that the Web request was completed. This field is constant.</summary>
	public const int RequestTransactionComplete = 2001;

	/// <summary>Represents the event code indicating that the Web request was aborted. This field is constant.</summary>
	public const int RequestTransactionAbort = 2002;

	/// <summary>Identifies the offset for the ASP.NET health-monitoring error event codes. This field is constant.</summary>
	public const int ErrorCodeBase = 3000;

	/// <summary>Represents the event code indicating that the Web request has been aborted.</summary>
	public const int RuntimeErrorRequestAbort = 3001;

	/// <summary>Represents the event code indicating that a view-state failure occurred. This field is constant.</summary>
	public const int RuntimeErrorViewStateFailure = 3002;

	/// <summary>Represents the event code indicating that a validation error occurred. This field is constant.</summary>
	public const int RuntimeErrorValidationFailure = 3003;

	/// <summary>Represents the event code indicating that the size of the posted information exceeded the allowed limits. This field is constant.</summary>
	public const int RuntimeErrorPostTooLarge = 3004;

	/// <summary>Represents the event code indicating an unhandled exception occurred. This field is constant.</summary>
	public const int RuntimeErrorUnhandledException = 3005;

	/// <summary>Represents the event code indicating a parser error occurred.</summary>
	public const int WebErrorParserError = 3006;

	/// <summary>Indicates that a compilation error occurred.</summary>
	public const int WebErrorCompilationError = 3007;

	/// <summary>Indicates that a configuration error occurred. This field is constant.</summary>
	public const int WebErrorConfigurationError = 3008;

	/// <summary>Represents the event code indicating that an unclassified error occurred. This field is constant.</summary>
	public const int WebErrorOtherError = 3009;

	/// <summary>Represents the event code indicating that there was an error during the deserialization of a property. This field is constant.</summary>
	public const int WebErrorPropertyDeserializationError = 3010;

	/// <summary>Represents the event code indicating that there was an error during the deserialization of the type or value of an object. This field is constant.</summary>
	public const int WebErrorObjectStateFormatterDeserializationError = 3011;

	/// <summary>Identifies the offset for the ASP.NET health-monitoring audit event codes. This field is constant.</summary>
	public const int AuditCodeBase = 4000;

	/// <summary>Represents the event code indicating a form-authentication success occurred during a Web request. This field is constant.</summary>
	public const int AuditFormsAuthenticationSuccess = 4001;

	/// <summary>Represents the event code indicating that a membership-authentication success occurred during a Web request. This field is constant.</summary>
	public const int AuditMembershipAuthenticationSuccess = 4002;

	/// <summary>Represents the event code indicating a URL-authorization success occurred during a Web request. This field is constant.</summary>
	public const int AuditUrlAuthorizationSuccess = 4003;

	/// <summary>Represents the event code indicating that a file-authorization success occurred during a Web request. This field is constant.</summary>
	public const int AuditFileAuthorizationSuccess = 4004;

	/// <summary>Represents the event code indicating a form authentication failure occurred during a Web request. This field is constant.</summary>
	public const int AuditFormsAuthenticationFailure = 4005;

	/// <summary>Represents the event code indicating that a membership-authentication failure occurred during a Web request. This field is constant.</summary>
	public const int AuditMembershipAuthenticationFailure = 4006;

	/// <summary>Represents the event code indicating that a URL-authorization failure occurred during a Web request. This field is constant.</summary>
	public const int AuditUrlAuthorizationFailure = 4007;

	/// <summary>Represents the event code indicating that a file-authorization failure occurred during a Web request. This field is constant.</summary>
	public const int AuditFileAuthorizationFailure = 4008;

	/// <summary>Represents the event code indicating that the view-state verification failed. This field is constant.</summary>
	public const int AuditInvalidViewStateFailure = 4009;

	/// <summary>Represents the event code indicating that an unhandled security exception occurred during a Web request. This field is constant.</summary>
	public const int AuditUnhandledSecurityException = 4010;

	/// <summary>Represents the event code indicating that an unhandled access exception occurred during a Web request. This field is constant.</summary>
	public const int AuditUnhandledAccessException = 4011;

	/// <summary>Identifies the offset for the ASP.NET health-monitoring Web miscellaneous event codes. This field is constant.</summary>
	public const int MiscCodeBase = 6000;

	/// <summary>Represents the event code used by providers to record nonstandard information about an event. This field is constant.</summary>
	public const int WebEventProviderInformation = 6001;

	/// <summary>Identifies the offset for the application detail event codes. This field is constant.</summary>
	public const int ApplicationDetailCodeBase = 50000;

	/// <summary>Represents the event code indicating that the application shutdown reason is unknown. This field is constant.</summary>
	public const int ApplicationShutdownUnknown = 50001;

	/// <summary>Represents the event code indicating that the hosting environment is shutting down. This field is constant.</summary>
	public const int ApplicationShutdownHostingEnvironment = 50002;

	/// <summary>Represents the event code indicating that the Global.asax file has changed. This field is constant.</summary>
	public const int ApplicationShutdownChangeInGlobalAsax = 50003;

	/// <summary>Represents the event code indicating that the configuration file has changed. This field is constant.</summary>
	public const int ApplicationShutdownConfigurationChange = 50004;

	/// <summary>Represents the event code indicating that the application domain was explicitly unloaded. This field is constant.</summary>
	public const int ApplicationShutdownUnloadAppDomainCalled = 50005;

	/// <summary>Represents the event code indicating that the security policy file has changed. This field is constant.</summary>
	public const int ApplicationShutdownChangeInSecurityPolicyFile = 50006;

	/// <summary>Represents the event code indicating a subdirectory in the application's Bin directory was changed or renamed. This field is constant.</summary>
	public const int ApplicationShutdownBinDirChangeOrDirectoryRename = 50007;

	/// <summary>Represents the event code indicating a subdirectory in the Browsers application directory was changed or renamed. This field is constant.</summary>
	public const int ApplicationShutdownBrowsersDirChangeOrDirectoryRename = 50008;

	/// <summary>Represents the event code indicating a subdirectory in the App_Code directory was changed or renamed. This field is constant.</summary>
	public const int ApplicationShutdownCodeDirChangeOrDirectoryRename = 50009;

	/// <summary>Represents the event code indicating a subdirectory in the App_Resources directory was changed or renamed. This field is constant.</summary>
	public const int ApplicationShutdownResourcesDirChangeOrDirectoryRename = 50010;

	/// <summary>Represents the event code indicating that the idle time-out was exceeded. This field is constant.</summary>
	public const int ApplicationShutdownIdleTimeout = 50011;

	/// <summary>Represents the event code indicating that the physical path of the application has changed. This field is constant.</summary>
	public const int ApplicationShutdownPhysicalApplicationPathChanged = 50012;

	/// <summary>Represents the event code indicating that the ASP.NET run time was explicitly closed. This field is constant.</summary>
	public const int ApplicationShutdownHttpRuntimeClose = 50013;

	/// <summary>Represents the event code indicating an application-initialization error occurred. This field is constant.</summary>
	public const int ApplicationShutdownInitializationError = 50014;

	/// <summary>Represents the event code indicating that the maximum number of recompilations was reached. This field is constant.</summary>
	public const int ApplicationShutdownMaxRecompilationsReached = 50015;

	/// <summary>Represents the event code indicating that an error occurred while communicating with the state server. This field is constant.</summary>
	public const int StateServerConnectionError = 50016;

	/// <summary>Identifies the offset for the ASP.NET audit-detail event codes. This field is constant.</summary>
	public const int AuditDetailCodeBase = 50200;

	/// <summary>Represents the event code indicating that the supplied ticket is invalid. This field is constant.</summary>
	public const int InvalidTicketFailure = 50201;

	/// <summary>Represents the event code indicating that the supplied ticket is expired. This field is constant.</summary>
	public const int ExpiredTicketFailure = 50202;

	/// <summary>Represents the event code indicating that the supplied view state failed the integrity check. This field is constant.</summary>
	public const int InvalidViewStateMac = 50203;

	/// <summary>Represents the event code indicating that the supplied view state is invalid. This field is constant.</summary>
	public const int InvalidViewState = 50204;

	/// <summary>Identifies the offset for the ASP.NET health-monitoring Web-detail event codes. </summary>
	public const int WebEventDetailCodeBase = 50300;

	/// <summary>Represents the event code indicating that the SQL provider dropped events. This field is constant.</summary>
	public const int SqlProviderEventsDropped = 50301;

	/// <summary>Identifies the offset for the custom event codes. This field is constant.</summary>
	public const int WebExtendedBase = 100000;
}
