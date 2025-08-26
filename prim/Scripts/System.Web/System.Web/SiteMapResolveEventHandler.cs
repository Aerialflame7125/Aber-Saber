namespace System.Web;

/// <summary>Represents the method that will handle the <see cref="E:System.Web.SiteMapProvider.SiteMapResolve" /> event of a specific instance of the <see cref="T:System.Web.SiteMapProvider" /> or static <see cref="T:System.Web.SiteMap" /> class.</summary>
/// <param name="sender">The source of the event, an instance of the <see cref="T:System.Web.SiteMapProvider" /> class.</param>
/// <param name="e">A <see cref="T:System.Web.SiteMapResolveEventArgs" /> that contains the event data.</param>
/// <returns>The <see cref="T:System.Web.SiteMapNode" /> that represents the result of the <see cref="T:System.Web.SiteMapResolveEventHandler" /> operation.</returns>
public delegate SiteMapNode SiteMapResolveEventHandler(object sender, SiteMapResolveEventArgs e);
