using System.Collections;

namespace System.Web.Services.Discovery;

/// <summary>Represents a collection of documents discovered during XML Web services discovery that have been downloaded to the client. This class cannot be inherited.</summary>
public sealed class DiscoveryClientDocumentCollection : DictionaryBase
{
	/// <summary>Gets or sets a client discovery document object from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" /> with the specified URL.</summary>
	/// <param name="url">The URL of the discovery document to get or set from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />. </param>
	/// <returns>An <see cref="T:System.Object" /> representing the document discovered and downloaded to the client. The underlying type of the object can be a <see cref="T:System.Web.Services.Description.ServiceDescription" />, <see cref="T:System.Xml.Schema.XmlSchema" />, or <see cref="T:System.Web.Services.Discovery.DiscoveryDocument" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="url" /> is <see langword="null" />. </exception>
	public object this[string url]
	{
		get
		{
			return base.Dictionary[url];
		}
		set
		{
			base.Dictionary[url] = value;
		}
	}

	/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object with all the keys in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />.</returns>
	public ICollection Keys => base.Dictionary.Keys;

	/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> object with all the values in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />.</returns>
	public ICollection Values => base.Dictionary.Values;

	/// <summary>Adds an object with the specified URL to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />.</summary>
	/// <param name="url">The URL for the document to add to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />. </param>
	/// <param name="value">A discovered document to add to the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="url" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">An entry with a key of <paramref name="url" /> already exists in the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />. </exception>
	public void Add(string url, object value)
	{
		base.Dictionary.Add(url, value);
	}

	/// <summary>Determines if the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" /> contains an object with the specified URL.</summary>
	/// <param name="url">The URL for the document to locate within the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />. </param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" /> contains an object with the specified URL; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="url" /> is <see langword="null" />. </exception>
	public bool Contains(string url)
	{
		return base.Dictionary.Contains(url);
	}

	/// <summary>Removes an object with the specified URL from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />.</summary>
	/// <param name="url">The URL for the discovered document to remove from the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" />. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="url" /> is <see langword="null" />. </exception>
	public void Remove(string url)
	{
		base.Dictionary.Remove(url);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryClientDocumentCollection" /> class. </summary>
	public DiscoveryClientDocumentCollection()
	{
	}
}
