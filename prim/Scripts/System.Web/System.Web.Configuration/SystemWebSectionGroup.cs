using System.Configuration;
using System.Web.Services.Configuration;

namespace System.Web.Configuration;

/// <summary>Allows the user to programmatically access the <see langword="system.web" /> group of the configuration file. This class cannot be inherited.</summary>
public sealed class SystemWebSectionGroup : ConfigurationSectionGroup
{
	/// <summary>Gets the <see langword="anonymousIdentification" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.AnonymousIdentificationSection" /> object.</returns>
	[ConfigurationProperty("anonymousIdentification")]
	public AnonymousIdentificationSection AnonymousIdentification => (AnonymousIdentificationSection)base.Sections["anonymousIdentification"];

	/// <summary>Gets the <see langword="authentication" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.AuthenticationSection" /> object.</returns>
	[ConfigurationProperty("authentication")]
	public AuthenticationSection Authentication => (AuthenticationSection)base.Sections["authentication"];

	/// <summary>Gets the <see langword="authorization" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.AuthorizationSection" /> object.</returns>
	[ConfigurationProperty("authorization")]
	public AuthorizationSection Authorization => (AuthorizationSection)base.Sections["authorization"];

	/// <summary>Gets the <see langword="browserCaps" /> section.</summary>
	/// <returns>The <see cref="T:System.Configuration.DefaultSection" /> object.</returns>
	[ConfigurationProperty("browserCaps")]
	public DefaultSection BrowserCaps => (DefaultSection)base.Sections["browserCaps"];

	/// <summary>Gets the <see langword="clientTarget" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.ClientTargetSection" /> object.</returns>
	[ConfigurationProperty("clientTarget")]
	public ClientTargetSection ClientTarget => (ClientTargetSection)base.Sections["clientTarget"];

	/// <summary>Gets the <see langword="compilation" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.CompilationSection" /> object.</returns>
	[ConfigurationProperty("compilation")]
	public CompilationSection Compilation => (CompilationSection)base.Sections["compilation"];

	/// <summary>Gets the <see langword="customErrors" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.CustomErrorsSection" /> object.</returns>
	[ConfigurationProperty("customErrors")]
	public CustomErrorsSection CustomErrors => (CustomErrorsSection)base.Sections["customErrors"];

	/// <summary>Gets the <see langword="deployment" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.DeploymentSection" /> object.</returns>
	[ConfigurationProperty("deployment")]
	public DeploymentSection Deployment => (DeploymentSection)base.Sections["deployment"];

	/// <summary>Gets the <see langword="deviceFilters" /> section.</summary>
	/// <returns>The <see cref="T:System.Configuration.DefaultSection" /> object.</returns>
	[ConfigurationProperty("deviceFilters")]
	public DefaultSection DeviceFilters => (DefaultSection)base.Sections["deviceFilters"];

	/// <summary>Gets the <see langword="globalization" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.GlobalizationSection" /> object.</returns>
	[ConfigurationProperty("globalization")]
	public GlobalizationSection Globalization => (GlobalizationSection)base.Sections["globalization"];

	/// <summary>Gets the <see langword="healthMonitoring" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.HealthMonitoringSection" /> object.</returns>
	[ConfigurationProperty("healthMonitoring")]
	public HealthMonitoringSection HealthMonitoring => (HealthMonitoringSection)base.Sections["healthMonitoring"];

	/// <summary>Gets the <see langword="hostingEnvironment" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.HostingEnvironmentSection" /> object refers to the <see langword="hostingEnvironment" /> section of the configuration file. </returns>
	[ConfigurationProperty("hostingEnvironment")]
	public HostingEnvironmentSection HostingEnvironment => (HostingEnvironmentSection)base.Sections["hostingEnvironment"];

	/// <summary>Gets the <see langword="httpCookies" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.HttpCookiesSection" /> object.</returns>
	[ConfigurationProperty("httpCookies")]
	public HttpCookiesSection HttpCookies => (HttpCookiesSection)base.Sections["httpCookies"];

	/// <summary>Gets the <see langword="httpHandlers" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.HttpHandlersSection" /> object.</returns>
	[ConfigurationProperty("httpHandlers")]
	public HttpHandlersSection HttpHandlers => (HttpHandlersSection)base.Sections["httpHandlers"];

	/// <summary>Gets the <see langword="httpModules" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.HttpModulesSection" /> object.</returns>
	[ConfigurationProperty("httpModules")]
	public HttpModulesSection HttpModules => (HttpModulesSection)base.Sections["httpModules"];

	/// <summary>Gets the <see langword="httpRuntime" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.HttpRuntimeSection" /> object.</returns>
	[ConfigurationProperty("httpRuntime")]
	public HttpRuntimeSection HttpRuntime => (HttpRuntimeSection)base.Sections["httpRuntime"];

	/// <summary>Gets the <see langword="identity" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.IdentitySection" /> object.</returns>
	[ConfigurationProperty("identity")]
	public IdentitySection Identity => (IdentitySection)base.Sections["identity"];

	/// <summary>Gets the <see langword="machineKey" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.MachineKeySection" /> object.</returns>
	[ConfigurationProperty("machineKey")]
	public MachineKeySection MachineKey => (MachineKeySection)base.Sections["machineKey"];

	/// <summary>Gets the <see langword="membership" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.MembershipSection" /> object.</returns>
	[ConfigurationProperty("membership")]
	public MembershipSection Membership => (MembershipSection)base.Sections["membership"];

	/// <summary>Gets the <see langword="mobileControls" /> section.</summary>
	/// <returns>The <see cref="T:System.Configuration.ConfigurationSection" /> object refers to the <see langword="mobileControls" /> section of the configuration file.</returns>
	[ConfigurationProperty("mobileControls")]
	[Obsolete("System.Web.Mobile.dll is obsolete.")]
	public ConfigurationSection MobileControls => base.Sections["MobileControls"];

	/// <summary>Gets the <see langword="pages" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.PagesSection" /> object.</returns>
	[ConfigurationProperty("pages")]
	public PagesSection Pages => (PagesSection)base.Sections["pages"];

	/// <summary>Gets the <see langword="processModel" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.ProcessModelSection" /> object.</returns>
	[ConfigurationProperty("processModel")]
	public ProcessModelSection ProcessModel => (ProcessModelSection)base.Sections["processModel"];

	/// <summary>Gets the <see langword="profile" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.ProfileSection" /> object.</returns>
	[ConfigurationProperty("profile")]
	public ProfileSection Profile => (ProfileSection)base.Sections["profile"];

	/// <summary>Gets the <see langword="protocols" /> section.</summary>
	/// <returns>The <see cref="T:System.Configuration.DefaultSection" /> object refers to the <see langword="protocols" /> section of the configuration file. </returns>
	[ConfigurationProperty("protocols")]
	public DefaultSection Protocols => (DefaultSection)base.Sections["protocols"];

	/// <summary>Gets the <see langword="roleManager" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.RoleManagerSection" /> object.</returns>
	[ConfigurationProperty("roleManager")]
	public RoleManagerSection RoleManager => (RoleManagerSection)base.Sections["roleManager"];

	/// <summary>Gets the <see langword="securityPolicy" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.SecurityPolicySection" /> object.</returns>
	[ConfigurationProperty("securityPolicy")]
	public SecurityPolicySection SecurityPolicy => (SecurityPolicySection)base.Sections["securityPolicy"];

	/// <summary>Gets the <see langword="sessionState" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.SessionStateSection" /> object.</returns>
	[ConfigurationProperty("sessionState")]
	public SessionStateSection SessionState => (SessionStateSection)base.Sections["sessionState"];

	/// <summary>Gets the <see langword="siteMap" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.SiteMapSection" /> object.</returns>
	[ConfigurationProperty("siteMap")]
	public SiteMapSection SiteMap => (SiteMapSection)base.Sections["siteMap"];

	/// <summary>Gets the <see langword="trace" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.TraceSection" /> object.</returns>
	[ConfigurationProperty("trace")]
	public TraceSection Trace => (TraceSection)base.Sections["trace"];

	/// <summary>Gets the <see langword="trust" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.TrustSection" /> object.</returns>
	[ConfigurationProperty("trust")]
	public TrustSection Trust => (TrustSection)base.Sections["trust"];

	/// <summary>Gets the <see langword="urlMappings" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.UrlMappingsSection" /> object refers to the <see langword="urlMappings" /> section of the configuration file. </returns>
	[ConfigurationProperty("urlMappings")]
	public UrlMappingsSection UrlMappings => (UrlMappingsSection)base.Sections["urlMappings"];

	/// <summary>Gets the <see langword="webControls" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.WebControlsSection" /> object refers to the <see langword="webControls" /> section of the configuration file. </returns>
	[ConfigurationProperty("webControls")]
	public WebControlsSection WebControls => (WebControlsSection)base.Sections["webControls"];

	/// <summary>Gets the <see langword="webParts" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.WebPartsSection" /> object refers to the <see langword="webParts" /> section of the configuration file.</returns>
	[ConfigurationProperty("webParts")]
	public WebPartsSection WebParts => (WebPartsSection)base.Sections["webParts"];

	/// <summary>Gets the <see langword="webServices" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Services.Configuration.WebServicesSection" /> object refers to the <see langword="webServices" /> section of the configuration file.</returns>
	[ConfigurationProperty("webServices")]
	public WebServicesSection WebServices => (WebServicesSection)base.Sections["webServices"];

	/// <summary>Gets the <see langword="xhtmlConformance" /> section.</summary>
	/// <returns>The <see cref="T:System.Web.Configuration.XhtmlConformanceSection" /> object refers to the <see langword="xhtmlConformance" /> section of the configuration file. </returns>
	[ConfigurationProperty("xhtmlConformance")]
	public XhtmlConformanceSection XhtmlConformance => (XhtmlConformanceSection)base.Sections["xhtmlConformance"];

	/// <summary>Creates a new instance of <see cref="T:System.Web.Configuration.SystemWebSectionGroup" />.</summary>
	public SystemWebSectionGroup()
	{
	}
}
