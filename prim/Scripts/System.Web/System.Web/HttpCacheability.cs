namespace System.Web;

/// <summary>Provides enumerated values that are used to set the <see langword="Cache-Control" /> HTTP header.</summary>
public enum HttpCacheability
{
	/// <summary>Sets the <see langword="Cache-Control: no-cache" /> header. Without a field name, the directive applies to the entire request and a shared (proxy server) cache must force a successful revalidation with the origin Web server before satisfying the request. With a field name, the directive applies only to the named field; the rest of the response may be supplied from a shared cache. </summary>
	NoCache = 1,
	/// <summary>Default value. Sets <see langword="Cache-Control: private" /> to specify that the response is cacheable only on the client and not by shared (proxy server) caches. </summary>
	Private = 2,
	/// <summary>Specifies that the response is cached only at the origin server. Similar to the <see cref="F:System.Web.HttpCacheability.NoCache" /> option. Clients receive a <see langword="Cache-Control: no-cache" /> directive but the document is cached on the origin server. Equivalent to <see cref="F:System.Web.HttpCacheability.ServerAndNoCache" />.</summary>
	Server = 3,
	/// <summary>Sets <see langword="Cache-Control: public" /> to specify that the response is cacheable by clients and shared (proxy) caches. </summary>
	Public = 4,
	/// <summary>Indicates that the response is cached at the server and at the client but nowhere else. Proxy servers are not allowed to cache the response. </summary>
	ServerAndPrivate = 5,
	/// <summary>Applies the settings of both <see cref="F:System.Web.HttpCacheability.Server" /> and <see cref="F:System.Web.HttpCacheability.NoCache" /> to indicate that the content is cached at the server but all others are explicitly denied the ability to cache the response. </summary>
	ServerAndNoCache = 3
}
