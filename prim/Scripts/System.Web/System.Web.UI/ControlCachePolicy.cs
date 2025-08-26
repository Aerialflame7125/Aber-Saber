using System.Web.Caching;

namespace System.Web.UI;

/// <summary>Provides programmatic access to an ASP.NET user control's output cache settings.</summary>
public sealed class ControlCachePolicy
{
	private BasePartialCachingControl bpcc;

	private bool cached;

	/// <summary>Gets or sets a value indicating whether fragment caching is enabled for the user control.</summary>
	/// <returns>
	///     <see langword="true" /> if the user control's output is cached; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.- or -The <see cref="P:System.Web.UI.ControlCachePolicy.Cached" /> property is set outside of the initialization and rendering stages of the control.</exception>
	public bool Cached
	{
		get
		{
			AssertBasePartialCachingControl();
			return cached;
		}
		set
		{
			AssertBasePartialCachingControl();
			cached = value;
		}
	}

	/// <summary>Gets or sets an instance of the <see cref="T:System.Web.Caching.CacheDependency" /> class associated with the cached user control output.</summary>
	/// <returns>The <see cref="T:System.Web.Caching.CacheDependency" /> associated with the control. The default is <see langword="null" />.</returns>
	/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.- or -The <see cref="P:System.Web.UI.ControlCachePolicy.Dependency" /> property is set outside of the initialization and rendering stages of the control.</exception>
	public CacheDependency Dependency
	{
		get
		{
			AssertBasePartialCachingControl();
			return bpcc.Dependency;
		}
		set
		{
			AssertBasePartialCachingControl();
			bpcc.Dependency = value;
		}
	}

	/// <summary>Gets or sets the amount of time that cached items are to remain in the output cache.</summary>
	/// <returns>A <see cref="T:System.TimeSpan" /> that represents the amount of time a user control is to remain in the output cache. The default is <see cref="F:System.TimeSpan.Zero" />.</returns>
	/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.- or -The <see cref="P:System.Web.UI.ControlCachePolicy.Duration" /> property is set outside of the initialization and rendering stages of the control.</exception>
	public TimeSpan Duration
	{
		get
		{
			AssertBasePartialCachingControl();
			return TimeSpan.FromMinutes(bpcc.Duration);
		}
		set
		{
			AssertBasePartialCachingControl();
			bpcc.Duration = value.Minutes;
		}
	}

	/// <summary>Gets or sets the name of the output-cache provider that is associated with a control instance.</summary>
	/// <returns>The name of the provider.</returns>
	/// <exception cref="T:System.Configuration.Provider.ProviderException">The provider name was not found.</exception>
	/// <exception cref="T:System.Web.HttpException">An attempt was made to set the <see cref="P:System.Web.UI.ControlCachePolicy.ProviderName" /> property during or after the <see cref="E:System.Web.UI.Control.PreRender" /> event.</exception>
	public string ProviderName
	{
		get
		{
			AssertBasePartialCachingControl();
			return bpcc.ProviderName;
		}
		set
		{
			AssertBasePartialCachingControl();
			bpcc.ProviderName = value;
		}
	}

	/// <summary>Gets a value indicating whether the user control supports caching.</summary>
	/// <returns>
	///     <see langword="true" /> if the user control supports caching; otherwise, <see langword="false" />.</returns>
	public bool SupportsCaching => bpcc != null;

	/// <summary>Gets or sets a list of control identifiers to vary the cached output by.</summary>
	/// <returns>A semicolon-separated list of strings used to vary a user control's output cache. These strings represent the <see cref="P:System.Web.UI.Control.ID" /> property values of ASP.NET server controls declared in the user control.</returns>
	/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.- or -The <see cref="P:System.Web.UI.ControlCachePolicy.VaryByControl" /> property is set outside of the initialization and rendering stages of the control.</exception>
	public string VaryByControl
	{
		get
		{
			AssertBasePartialCachingControl();
			return bpcc.VaryByControls;
		}
		set
		{
			AssertBasePartialCachingControl();
			bpcc.VaryByControls = value;
		}
	}

	/// <summary>Gets or sets a list of <see langword="GET" /> or <see langword="POST" /> parameter names to vary the cached output by. </summary>
	/// <returns>A semicolon-separated list of strings used to vary the output cache. </returns>
	/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.</exception>
	public HttpCacheVaryByParams VaryByParams
	{
		get
		{
			AssertBasePartialCachingControl();
			throw new NotImplementedException();
		}
	}

	internal ControlCachePolicy()
		: this(null)
	{
	}

	internal ControlCachePolicy(BasePartialCachingControl bpcc)
	{
		this.bpcc = bpcc;
	}

	/// <summary>Instructs the <see cref="T:System.Web.UI.BasePartialCachingControl" /> control that wraps the user control to expire the cache entry at the specified date and time.</summary>
	/// <param name="expirationTime">A <see cref="T:System.DateTime" /> after which the cached entry expires.</param>
	/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.</exception>
	public void SetExpires(DateTime expirationTime)
	{
		AssertBasePartialCachingControl();
		bpcc.ExpirationTime = expirationTime;
	}

	/// <summary>Instructs the <see cref="T:System.Web.UI.BasePartialCachingControl" /> control that wraps the user control to set the user control's cache entry to use sliding or absolute expiration. </summary>
	/// <param name="useSlidingExpiration">
	///       <see langword="true" /> to use sliding cache expiration instead of absolute expiration; otherwise, <see langword="false" />.</param>
	/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.</exception>
	public void SetSlidingExpiration(bool useSlidingExpiration)
	{
		AssertBasePartialCachingControl();
		bpcc.SlidingExpiration = useSlidingExpiration;
	}

	/// <summary>Sets a list of custom strings that the output cache will use to vary the user control.</summary>
	/// <param name="varyByCustom">The list of custom strings.</param>
	/// <exception cref="T:System.Web.HttpException">The user control is not associated with a <see cref="T:System.Web.UI.BasePartialCachingControl" /> and is not cacheable.</exception>
	public void SetVaryByCustom(string varyByCustom)
	{
		AssertBasePartialCachingControl();
		bpcc.VaryByCustom = varyByCustom;
	}

	private void AssertBasePartialCachingControl()
	{
		if (bpcc == null)
		{
			throw new HttpException("The user control is not associated with a 'BasePartialCachingControl' and is not cacheable.");
		}
	}
}
