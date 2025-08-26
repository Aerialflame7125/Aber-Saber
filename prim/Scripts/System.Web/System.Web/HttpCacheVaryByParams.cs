using System.Collections;
using System.Security.Permissions;
using System.Text;

namespace System.Web;

/// <summary>Provides a type-safe way to set the <see cref="P:System.Web.HttpCachePolicy.VaryByParams" /> property.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpCacheVaryByParams
{
	private bool ignore_parms;

	private Hashtable parms;

	/// <summary>Gets or sets a value indicating whether an HTTP response varies by <see langword="Get" /> or <see langword="Post" /> parameters. </summary>
	/// <returns>
	///     <see langword="true" /> if HTTP request parameters are ignored; otherwise, <see langword="false" />.</returns>
	public bool IgnoreParams
	{
		get
		{
			return ignore_parms;
		}
		set
		{
			ignore_parms = value;
		}
	}

	/// <summary>Gets or sets a value indicating whether the cache varies according to the specified HTTP request parameter.</summary>
	/// <param name="header">The name of the custom parameter. </param>
	/// <returns>
	///     <see langword="true" /> if the cache should vary by the specified parameter value.</returns>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="header" /> is <see langword="null" />. </exception>
	public bool this[string header]
	{
		get
		{
			if (header == null)
			{
				throw new ArgumentNullException();
			}
			return parms.Contains(header);
		}
		set
		{
			if (header == null)
			{
				throw new ArgumentNullException();
			}
			ignore_parms = false;
			if (value)
			{
				if (!parms.Contains(header))
				{
					parms.Add(header, true);
				}
				else
				{
					parms.Remove(header);
				}
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCacheVaryByParams" /> class.</summary>
	public HttpCacheVaryByParams()
	{
		parms = new Hashtable(StringComparer.InvariantCultureIgnoreCase);
	}

	internal string[] GetParamNames()
	{
		string[] array = new string[parms.Count];
		parms.Keys.CopyTo(array, 0);
		return array;
	}

	internal string GetResponseHeaderValue()
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (string key in parms.Keys)
		{
			stringBuilder.Append(key);
			stringBuilder.Append("; ");
		}
		if (stringBuilder.Length == 0)
		{
			return null;
		}
		return stringBuilder.ToString();
	}
}
