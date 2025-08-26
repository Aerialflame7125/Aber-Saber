using System.Collections;

namespace System.Web.Services.Discovery;

/// <summary>Collects exceptions that occurred during XML Web services discovery. This class cannot be inherited.</summary>
public sealed class DiscoveryExceptionDictionary : DictionaryBase
{
	/// <summary>Gets or sets the <see cref="T:System.Exception" /> that occurred while discovering the specified URL from the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />.</summary>
	/// <param name="url">The URL of the discovery document that caused an exception to be thrown during XML Web services discovery. </param>
	/// <returns>An <see cref="T:System.Exception" /> that was thrown discovering <paramref name="url" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="url" /> is <see langword="null" />. </exception>
	public Exception this[string url]
	{
		get
		{
			return (Exception)base.Dictionary[url];
		}
		set
		{
			base.Dictionary[url] = value;
		}
	}

	/// <summary>Gets a <see cref="T:System.Collections.ICollection" /> object with all the keys in the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the keys of the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />.</returns>
	public ICollection Keys => base.Dictionary.Keys;

	/// <summary>Gets a <see cref="T:System.Collections.ICollection" /> object containing all the values in the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />.</summary>
	/// <returns>An <see cref="T:System.Collections.ICollection" /> containing the values in the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />.</returns>
	public ICollection Values => base.Dictionary.Values;

	/// <summary>Adds an <see cref="T:System.Exception" /> with a key of <paramref name="url" /> to the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />.</summary>
	/// <param name="url">The URL that caused an exception during XML Web services discovery. </param>
	/// <param name="value">The <see cref="T:System.Exception" /> that occurred during XML Web services discovery. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="url" /> is <see langword="null" />. </exception>
	/// <exception cref="T:System.ArgumentException">An entry with a key of <paramref name="url" /> already exists in the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />. </exception>
	public void Add(string url, Exception value)
	{
		base.Dictionary.Add(url, value);
	}

	/// <summary>Determines whether the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" /> contains an <see cref="T:System.Exception" /> with the specified URL.</summary>
	/// <param name="url">The URL of the <see cref="T:System.Exception" /> to locate within the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />. </param>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" /> contains an <see cref="T:System.Exception" /> with the specified URL; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="url" /> is <see langword="null" />. </exception>
	public bool Contains(string url)
	{
		return base.Dictionary.Contains(url);
	}

	/// <summary>Removes an <see cref="T:System.Exception" /> with the specified URL from the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />.</summary>
	/// <param name="url">The URL of the <see cref="T:System.Exception" /> to remove from the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" />. </param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="url" /> is <see langword="null" />. </exception>
	public void Remove(string url)
	{
		base.Dictionary.Remove(url);
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Services.Discovery.DiscoveryExceptionDictionary" /> class. </summary>
	public DiscoveryExceptionDictionary()
	{
	}
}
