using System.IO;
using System.Net;
using System.Net.Cache;
using System.Security.Permissions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Xml;

/// <summary>Resolves external XML resources named by a Uniform Resource Identifier (URI).</summary>
public class XmlUrlResolver : XmlResolver
{
	private static object s_DownloadManager;

	private ICredentials _credentials;

	private IWebProxy _proxy;

	private RequestCachePolicy _cachePolicy;

	private static XmlDownloadManager DownloadManager
	{
		get
		{
			if (s_DownloadManager == null)
			{
				object value = new XmlDownloadManager();
				Interlocked.CompareExchange<object>(ref s_DownloadManager, value, (object)null);
			}
			return (XmlDownloadManager)s_DownloadManager;
		}
	}

	/// <summary>Sets credentials used to authenticate web requests.</summary>
	/// <returns>The credentials to be used to authenticate web requests. If this property is not set, the value defaults to <see langword="null" />; that is, the <see langword="XmlUrlResolver" /> has no user credentials.</returns>
	public override ICredentials Credentials
	{
		set
		{
			_credentials = value;
		}
	}

	/// <summary>Gets or sets the network proxy for the underlying <see cref="T:System.Net.WebRequest" /> object.</summary>
	/// <returns>The <see cref="T:System.Net.IWebProxy" /> to use to access the Internet resource.</returns>
	public IWebProxy Proxy
	{
		set
		{
			_proxy = value;
		}
	}

	/// <summary>Gets or sets the cache policy for the underlying <see cref="T:System.Net.WebRequest" /> object.</summary>
	/// <returns>The cache policy for the underlying web request.</returns>
	public RequestCachePolicy CachePolicy
	{
		set
		{
			_cachePolicy = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlUrlResolver" /> class.</summary>
	public XmlUrlResolver()
	{
	}

	/// <summary>Maps a URI to an object that contains the actual resource.</summary>
	/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" />.</param>
	/// <param name="role">Currently not used.</param>
	/// <param name="ofObjectToReturn">The type of object to return. The current implementation only returns <see cref="T:System.IO.Stream" /> objects.</param>
	/// <returns>A stream object or <see langword="null" /> if a type other than stream is specified.</returns>
	/// <exception cref="T:System.Xml.XmlException">
	///         <paramref name="ofObjectToReturn" /> is neither <see langword="null" /> nor a <see langword="Stream" /> type.</exception>
	/// <exception cref="T:System.UriFormatException">The specified URI is not an absolute URI.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="absoluteUri" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Exception">There is a runtime error (for example, an interrupted server connection).</exception>
	public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
	{
		if (ofObjectToReturn == null || ofObjectToReturn == typeof(Stream) || ofObjectToReturn == typeof(object))
		{
			return DownloadManager.GetStream(absoluteUri, _credentials, _proxy, _cachePolicy);
		}
		throw new XmlException("Object type is not supported.", string.Empty);
	}

	/// <summary>Resolves the absolute URI from the base and relative URIs.</summary>
	/// <param name="baseUri">The base URI used to resolve the relative URI.</param>
	/// <param name="relativeUri">The URI to resolve. The URI can be absolute or relative. If absolute, this value effectively replaces the <paramref name="baseUri" /> value. If relative, it combines with the <paramref name="baseUri" /> to make an absolute URI.</param>
	/// <returns>The absolute URI, or <see langword="null" /> if the relative URI cannot be resolved.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="baseUri" /> is <see langword="null" /> or <paramref name="relativeUri" /> is <see langword="null" />.</exception>
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public override Uri ResolveUri(Uri baseUri, string relativeUri)
	{
		return base.ResolveUri(baseUri, relativeUri);
	}

	/// <summary>Asynchronously maps a URI to an object that contains the actual resource.</summary>
	/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" />.</param>
	/// <param name="role">Currently not used.</param>
	/// <param name="ofObjectToReturn">The type of object to return. The current implementation only returns <see cref="T:System.IO.Stream" /> objects.</param>
	/// <returns>A stream object or <see langword="null" /> if a type other than stream is specified.</returns>
	public override async Task<object> GetEntityAsync(Uri absoluteUri, string role, Type ofObjectToReturn)
	{
		if (ofObjectToReturn == null || ofObjectToReturn == typeof(Stream) || ofObjectToReturn == typeof(object))
		{
			return await DownloadManager.GetStreamAsync(absoluteUri, _credentials, _proxy, _cachePolicy).ConfigureAwait(continueOnCapturedContext: false);
		}
		throw new XmlException("Object type is not supported.", string.Empty);
	}
}
