namespace System.Web;

/// <summary>Provides data for an event that is raised by calling the <see cref="P:System.Web.SiteMapProvider.CurrentNode" /> property of the <see cref="T:System.Web.SiteMapProvider" /> class. </summary>
public class SiteMapResolveEventArgs : EventArgs
{
	private HttpContext _context;

	private SiteMapProvider _provider;

	/// <summary>Gets the context of the page request that the requested node represents.</summary>
	/// <returns>An <see cref="T:System.Web.HttpContext" />, if one is specified; otherwise, <see langword="null" />.</returns>
	public HttpContext Context => _context;

	/// <summary>Gets the <see cref="T:System.Web.SiteMapProvider" /> object that raised the <see cref="E:System.Web.SiteMapProvider.SiteMapResolve" /> event. </summary>
	/// <returns>The <see cref="T:System.Web.SiteMapProvider" /> that raised the event; otherwise, <see langword="null" />, if no provider is specified during the <see langword="EventArgs" /> object construction.</returns>
	public SiteMapProvider Provider => _provider;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.SiteMapResolveEventArgs" /> class using the specified <see cref="T:System.Web.HttpContext" /> and <see cref="T:System.Web.SiteMapProvider" /> objects. </summary>
	/// <param name="context">An <see cref="T:System.Web.HttpContext" /> that represents the context of the current page request.</param>
	/// <param name="provider">The <see cref="T:System.Web.SiteMapProvider" /> that raised the <see cref="E:System.Web.SiteMapProvider.SiteMapResolve" /> event.</param>
	public SiteMapResolveEventArgs(HttpContext context, SiteMapProvider provider)
	{
		_context = context;
		_provider = provider;
	}
}
