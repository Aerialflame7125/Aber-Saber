using System.Security.Permissions;

namespace System.Web.UI;

/// <summary>Defines the metadata attribute that Web Forms user controls (.ascx files) use to indicate if and how their output is cached. This class cannot be inherited.</summary>
[AttributeUsage(AttributeTargets.Class)]
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class PartialCachingAttribute : Attribute
{
	private int duration;

	private string varyByControls;

	private string varyByCustom;

	private string varyByParams;

	private bool shared;

	private string sqlDependency;

	/// <summary>Gets the amount of time, in seconds, that cached items should remain in the output cache.</summary>
	/// <returns>The amount of time, in seconds, a user control should remain in the output cache.</returns>
	public int Duration => duration;

	/// <summary>Gets or sets the name of the provider that is used to store the output-cached data for the associated control.</summary>
	/// <returns>The name of the provider.</returns>
	public string ProviderName { get; set; }

	/// <summary>Gets a list of query string or form <see langword="POST" /> parameters that the output cache will use to vary the user control.</summary>
	/// <returns>The list of query string or form <see langword="POST" /> parameters.</returns>
	public string VaryByParams => varyByParams;

	/// <summary>Gets a list of user control properties that the output cache uses to vary the user control.</summary>
	/// <returns>The list of user control properties.</returns>
	public string VaryByControls => varyByControls;

	/// <summary>Gets a list of custom strings that the output cache will use to vary the user control.</summary>
	/// <returns>The list of custom strings.</returns>
	public string VaryByCustom => varyByCustom;

	/// <summary>Gets a value indicating whether user control output can be shared with multiple pages.</summary>
	/// <returns>
	///     <see langword="true" /> if user control output can be shared between multiple pages; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
	public bool Shared => shared;

	/// <summary>Gets a delimited string that identifies one or more database and table name pairs that the cached user control is dependent on.</summary>
	/// <returns>A delimited string that identifies a set of database and table names that the user control cache entry is dependent on.</returns>
	public string SqlDependency => sqlDependency;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PartialCachingAttribute" /> class with the specified duration assigned to the user control to be cached.</summary>
	/// <param name="duration">The amount of time, in seconds, a user control should remain in the output cache. </param>
	public PartialCachingAttribute(int duration)
	{
		this.duration = duration;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PartialCachingAttribute" /> class, specifying the caching duration, any GET and POST values, control names, and custom output caching requirements used to vary the cache.</summary>
	/// <param name="duration">The amount of time, in seconds, that the user control is cached. </param>
	/// <param name="varyByParams">A semicolon-separated list of strings used to vary the output cache. By default, these strings correspond to a query string value sent with GET method attributes or to a parameter sent using the POST method. When this attribute is set to multiple parameters, the output cache contains a different version of the requested document for each specified parameter. Possible values include "none", "*", and any valid query string or POST parameter name. </param>
	/// <param name="varyByControls">A semicolon-separated list of strings used to vary the output cache. These strings represent fully qualified names of properties on a user control. When this parameter is used for a user control, the user control output is varied to the cache for each specified user control property. </param>
	/// <param name="varyByCustom">Any text that represents custom output caching requirements. If this parameter is given a value of "browser", the cache is varied by browser name and major version information. If a custom string is entered, you must override the <see cref="M:System.Web.HttpApplication.GetVaryByCustomString(System.Web.HttpContext,System.String)" /> method in your application's Global.asax file. </param>
	public PartialCachingAttribute(int duration, string varyByParams, string varyByControls, string varyByCustom)
	{
		this.duration = duration;
		this.varyByParams = varyByParams;
		this.varyByControls = varyByControls;
		this.varyByCustom = varyByCustom;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PartialCachingAttribute" /> class, specifying the caching duration, any <see langword="GET" /> and <see langword="POST" /> values, control names, custom output caching requirements used to vary the cache, and whether the user control output can be shared with multiple pages.</summary>
	/// <param name="duration">The amount of time, in seconds, that the user control is cached.</param>
	/// <param name="varyByParams">A semicolon-separated list of strings used to vary the output cache. By default, these strings correspond to a query string value sent with <see langword="GET" /> method attributes, or a parameter sent using the <see langword="POST" /> method. When this attribute is set to multiple parameters, the output cache contains a different version of the requested document for each specified parameter. Possible values include "none", "*", and any valid query string or <see langword="POST" /> parameter name.</param>
	/// <param name="varyByControls">A semicolon-separated list of strings used to vary the output cache. These strings represent fully qualified names of properties on a user control. When this parameter is used for a user control, the user control output is varied to the cache for each specified user control property.</param>
	/// <param name="varyByCustom">Any text that represents custom output caching requirements. If this parameter is given a value of "browser", the cache is varied by browser name and major version information. If a custom string is entered, you must override the <see cref="M:System.Web.HttpApplication.GetVaryByCustomString(System.Web.HttpContext,System.String)" /> method in your application's Global.asax file.</param>
	/// <param name="shared">
	///       <see langword="true" /> to indicate that the user control output can be shared with multiple pages; otherwise, <see langword="false" />. </param>
	public PartialCachingAttribute(int duration, string varyByParams, string varyByControls, string varyByCustom, bool shared)
	{
		this.duration = duration;
		this.varyByParams = varyByParams;
		this.varyByControls = varyByControls;
		this.varyByCustom = varyByCustom;
		this.shared = shared;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PartialCachingAttribute" /> class, specifying the caching duration, any <see langword="GET" /> and <see langword="POST" /> values, control names, custom output caching requirements used to vary the cache, the database dependencies, and whether the user control output can be shared with multiple pages. </summary>
	/// <param name="duration">The amount of time, in seconds, that the user control is cached.</param>
	/// <param name="varyByParams">A semicolon-separated list of strings used to vary the output cache. By default, these strings correspond to a query string value sent with <see langword="GET" /> method attributes, or a parameter sent using the <see langword="POST" /> method. When this attribute is set to multiple parameters, the output cache contains a different version of the requested document for each specified parameter. Possible values include "none", "*", and any valid query string or <see langword="POST" /> parameter name.</param>
	/// <param name="varyByControls">A semicolon-separated list of strings used to vary the output cache. These strings represent fully qualified names of properties on a user control. When this parameter is used for a user control, the user control output is varied to the cache for each specified user control property.</param>
	/// <param name="varyByCustom">Any text that represents custom output caching requirements. If this parameter is given a value of "browser", the cache is varied by browser name and major version information. If a custom string is entered, you must override the <see cref="M:System.Web.HttpApplication.GetVaryByCustomString(System.Web.HttpContext,System.String)" /> method in your application's Global.asax file.</param>
	/// <param name="sqlDependency">A delimited list of database names and table names that, when changed, explicitly expire a cache entry in the ASP.NET cache. These database names match those SQL Server cache dependencies identified in your Web configuration section.</param>
	/// <param name="shared">
	///       <see langword="true" /> to indicate that the user control output can be shared with multiple pages; otherwise, <see langword="false" />.</param>
	public PartialCachingAttribute(int duration, string varyByParams, string varyByControls, string varyByCustom, string sqlDependency, bool shared)
	{
		this.duration = duration;
		this.varyByParams = varyByParams;
		this.varyByControls = varyByControls;
		this.varyByCustom = varyByCustom;
		this.sqlDependency = sqlDependency;
		this.shared = shared;
	}
}
