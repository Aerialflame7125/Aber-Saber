using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web;

/// <summary>Provides a type-safe way to manipulate HTTP cookies.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpCookieCollection : NameObjectCollectionBase
{
	private bool auto_fill;

	/// <summary>Gets the cookie with the specified numerical index from the cookie collection.</summary>
	/// <param name="index">The index of the cookie to retrieve from the collection. </param>
	/// <returns>The <see cref="T:System.Web.HttpCookie" /> specified by <paramref name="index" />.</returns>
	public HttpCookie this[int index] => (HttpCookie)BaseGet(index);

	/// <summary>Gets the cookie with the specified name from the cookie collection.</summary>
	/// <param name="name">Name of cookie to retrieve. </param>
	/// <returns>The <see cref="T:System.Web.HttpCookie" /> specified by <paramref name="name." /></returns>
	public HttpCookie this[string name]
	{
		get
		{
			HttpCookie httpCookie = (HttpCookie)BaseGet(name);
			if (!base.IsReadOnly && auto_fill && httpCookie == null)
			{
				httpCookie = new HttpCookie(name);
				BaseAdd(name, httpCookie);
			}
			return httpCookie;
		}
	}

	/// <summary>Gets a string array containing all the keys (cookie names) in the cookie collection.</summary>
	/// <returns>An array of cookie names.</returns>
	public string[] AllKeys
	{
		get
		{
			string[] array = new string[Keys.Count];
			((ICollection)Keys).CopyTo((Array)array, 0);
			return array;
		}
	}

	[Obsolete("Don't use this constructor, use the (bool, bool) one, as it's more clear what it does")]
	internal HttpCookieCollection(HttpResponse Response, bool ReadOnly)
		: base(StringComparer.OrdinalIgnoreCase)
	{
		auto_fill = Response != null;
		base.IsReadOnly = ReadOnly;
	}

	internal HttpCookieCollection(bool auto_fill, bool read_only)
		: base(StringComparer.OrdinalIgnoreCase)
	{
		this.auto_fill = auto_fill;
		base.IsReadOnly = read_only;
	}

	internal HttpCookieCollection(string cookies)
		: base(StringComparer.OrdinalIgnoreCase)
	{
		if (string.IsNullOrEmpty(cookies))
		{
			return;
		}
		string[] array = cookies.Split(';');
		foreach (string text in array)
		{
			int num = text.IndexOf('=');
			if (num != -1)
			{
				string text2 = text.Substring(0, num);
				string text3 = text.Substring(num + 1);
				Add(new HttpCookie(text2.Trim(), text3.Trim()));
			}
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpCookieCollection" /> class.</summary>
	public HttpCookieCollection()
		: base(StringComparer.OrdinalIgnoreCase)
	{
	}

	/// <summary>Adds the specified cookie to the cookie collection.</summary>
	/// <param name="cookie">The <see cref="T:System.Web.HttpCookie" /> to add to the collection. </param>
	public void Add(HttpCookie cookie)
	{
		BaseAdd(cookie.Name, cookie);
	}

	/// <summary>Clears all cookies from the cookie collection.</summary>
	public void Clear()
	{
		BaseClear();
	}

	/// <summary>Copies members of the cookie collection to an <see cref="T:System.Array" /> beginning at the specified index of the array.</summary>
	/// <param name="dest">The destination <see cref="T:System.Array" />. </param>
	/// <param name="index">The index of the destination array where copying starts. </param>
	public void CopyTo(Array dest, int index)
	{
		BaseGetAllValues().CopyTo(dest, index);
	}

	/// <summary>Returns the key (name) of the cookie at the specified numerical index.</summary>
	/// <param name="index">The index of the key to retrieve from the collection. </param>
	/// <returns>The name of the cookie specified by <paramref name="index" />.</returns>
	public string GetKey(int index)
	{
		return ((HttpCookie)BaseGet(index))?.Name;
	}

	/// <summary>Removes the cookie with the specified name from the collection.</summary>
	/// <param name="name">The name of the cookie to remove from the collection. </param>
	public void Remove(string name)
	{
		BaseRemove(name);
	}

	/// <summary>Updates the value of an existing cookie in a cookie collection.</summary>
	/// <param name="cookie">The <see cref="T:System.Web.HttpCookie" /> object to update. </param>
	public void Set(HttpCookie cookie)
	{
		BaseSet(cookie.Name, cookie);
	}

	/// <summary>Returns the <see cref="T:System.Web.HttpCookie" /> item with the specified index from the cookie collection.</summary>
	/// <param name="index">The index of the cookie to return from the collection. </param>
	/// <returns>The <see cref="T:System.Web.HttpCookie" /> specified by <paramref name="index" />.</returns>
	public HttpCookie Get(int index)
	{
		return (HttpCookie)BaseGet(index);
	}

	/// <summary>Returns the cookie with the specified name from the cookie collection.</summary>
	/// <param name="name">The name of the cookie to retrieve from the collection. </param>
	/// <returns>The <see cref="T:System.Web.HttpCookie" /> specified by <paramref name="name" />.</returns>
	public HttpCookie Get(string name)
	{
		return this[name];
	}
}
