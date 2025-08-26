namespace System.Web;

/// <summary>Provides enumerated values that are used to set revalidation-specific <see langword="Cache-Control" /> HTTP headers.</summary>
public enum HttpCacheRevalidation
{
	/// <summary>Sets the HTTP header to <see langword="Cache-Control: must-revalidate" />.</summary>
	AllCaches = 1,
	/// <summary>Sets the HTTP header to <see langword="Cache-Control: proxy-revalidate" />.</summary>
	ProxyCaches,
	/// <summary>If this value is set, no cache-revalidation directive is sent. The default value. </summary>
	None
}
