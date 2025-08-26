using System.Collections.Generic;
using System.Security.Permissions;

namespace System.Web;

/// <summary>Provides a type-safe way to set the <see cref="P:System.Web.HttpCachePolicy.VaryByContentEncodings" /> property of the <see cref="T:System.Web.HttpCachePolicy" /> class.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpCacheVaryByContentEncodings
{
	private Dictionary<string, bool> encodings;

	/// <summary>Gets or sets a value that indicates whether the cache varies according to the specified content encoding.</summary>
	/// <param name="contentEncoding">The name of the content encoding.</param>
	/// <returns>
	///     <see langword="true" /> if the cache should vary by the specified content encoding; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.ArgumentNullException">The content encoding is <see langword="null" />.</exception>
	public bool this[string contentEncoding]
	{
		get
		{
			if (contentEncoding == null)
			{
				throw new ArgumentNullException("contentEncoding");
			}
			if (encodings.ContainsKey(contentEncoding))
			{
				return encodings[contentEncoding];
			}
			return false;
		}
		set
		{
			if (contentEncoding == null)
			{
				throw new ArgumentNullException("contentEncoding");
			}
			encodings[contentEncoding] = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCacheVaryByContentEncodings" /> class.</summary>
	public HttpCacheVaryByContentEncodings()
	{
		encodings = new Dictionary<string, bool>();
	}
}
