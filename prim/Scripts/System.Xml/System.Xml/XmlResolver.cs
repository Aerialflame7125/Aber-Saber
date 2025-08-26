using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace System.Xml;

/// <summary>Resolves external XML resources named by a Uniform Resource Identifier (URI).</summary>
public abstract class XmlResolver
{
	/// <summary>When overridden in a derived class, sets the credentials used to authenticate web requests.</summary>
	/// <returns>The credentials to be used to authenticate web requests. If this property is not set, the value defaults to <see langword="null" />; that is, the <see langword="XmlResolver" /> has no user credentials.</returns>
	public virtual ICredentials Credentials
	{
		set
		{
		}
	}

	/// <summary>When overridden in a derived class, maps a URI to an object that contains the actual resource.</summary>
	/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" />.</param>
	/// <param name="role">Currently not used.</param>
	/// <param name="ofObjectToReturn">The type of object to return. The current version only returns System.IO.Stream objects.</param>
	/// <returns>A stream object or <see langword="null" /> if a type other than stream is specified.</returns>
	/// <exception cref="T:System.Xml.XmlException">
	///         <paramref name="ofObjectToReturn" /> is not a <see langword="Stream" /> type.</exception>
	/// <exception cref="T:System.UriFormatException">The specified URI is not an absolute URI.</exception>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="absoluteUri" /> is <see langword="null" />.</exception>
	/// <exception cref="T:System.Exception">There is a runtime error (for example, an interrupted server connection).</exception>
	public abstract object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn);

	/// <summary>When overridden in a derived class, resolves the absolute URI from the base and relative URIs.</summary>
	/// <param name="baseUri">The base URI used to resolve the relative URI.</param>
	/// <param name="relativeUri">The URI to resolve. The URI can be absolute or relative. If absolute, this value effectively replaces the <paramref name="baseUri" /> value. If relative, it combines with the <paramref name="baseUri" /> to make an absolute URI.</param>
	/// <returns>The absolute URI or <see langword="null" /> if the relative URI cannot be resolved.</returns>
	/// <exception cref="T:System.ArgumentException">
	///         <paramref name="relativeUri" /> is <see langword="null" />.</exception>
	public virtual Uri ResolveUri(Uri baseUri, string relativeUri)
	{
		if (baseUri == null || (!baseUri.IsAbsoluteUri && baseUri.OriginalString.Length == 0))
		{
			Uri uri = new Uri(relativeUri, UriKind.RelativeOrAbsolute);
			if (!uri.IsAbsoluteUri && uri.OriginalString.Length > 0)
			{
				uri = new Uri(Path.GetFullPath(relativeUri));
			}
			return uri;
		}
		if (relativeUri == null || relativeUri.Length == 0)
		{
			return baseUri;
		}
		if (!baseUri.IsAbsoluteUri)
		{
			throw new NotSupportedException(Res.GetString("Relative URIs are not supported."));
		}
		return new Uri(baseUri, relativeUri);
	}

	/// <summary>Enables the resolver to return types other than <see cref="T:System.IO.Stream" />.</summary>
	/// <param name="absoluteUri">The URI.</param>
	/// <param name="type">The type to return.</param>
	/// <returns>
	///     <see langword="true" /> if the <paramref name="type" /> is supported; otherwise, <see langword="false" />.</returns>
	public virtual bool SupportsType(Uri absoluteUri, Type type)
	{
		if (absoluteUri == null)
		{
			throw new ArgumentNullException("absoluteUri");
		}
		if (type == null || type == typeof(Stream))
		{
			return true;
		}
		return false;
	}

	/// <summary>Asynchronously maps a URI to an object that contains the actual resource.</summary>
	/// <param name="absoluteUri">The URI returned from <see cref="M:System.Xml.XmlResolver.ResolveUri(System.Uri,System.String)" />.</param>
	/// <param name="role">Currently not used.</param>
	/// <param name="ofObjectToReturn">The type of object to return. The current version only returns <see cref="T:System.IO.Stream" /> objects.</param>
	/// <returns>A stream object or <see langword="null" /> if a type other than stream is specified.</returns>
	public virtual Task<object> GetEntityAsync(Uri absoluteUri, string role, Type ofObjectToReturn)
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Xml.XmlResolver" /> class.</summary>
	protected XmlResolver()
	{
	}
}
