using System.Collections;
using System.Collections.Specialized;
using System.Security.Permissions;

namespace System.Web.SessionState;

/// <summary>Provides access to session-state values as well as session-level settings and lifetime management methods.</summary>
[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
public sealed class HttpSessionState : ICollection, IEnumerable
{
	private IHttpSessionState container;

	internal IHttpSessionState Container => container;

	/// <summary>Gets or sets the character-set identifier for the current session.</summary>
	/// <returns>The character-set identifier for the current session.</returns>
	public int CodePage
	{
		get
		{
			return container.CodePage;
		}
		set
		{
			container.CodePage = value;
		}
	}

	/// <summary>Gets a reference to the current session-state object.</summary>
	/// <returns>The current <see cref="T:System.Web.SessionState.HttpSessionState" />.</returns>
	public HttpSessionState Contents => this;

	/// <summary>Gets a value that indicates whether the application is configured for cookieless sessions.</summary>
	/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values that indicate whether the application is configured for cookieless sessions. The default is <see cref="F:System.Web.HttpCookieMode.UseCookies" />.</returns>
	public HttpCookieMode CookieMode
	{
		get
		{
			if (IsCookieless)
			{
				return HttpCookieMode.UseUri;
			}
			return HttpCookieMode.UseCookies;
		}
	}

	/// <summary>Gets the number of items in the session-state collection.</summary>
	/// <returns>The number of items in the collection.</returns>
	public int Count => container.Count;

	/// <summary>Gets a value indicating whether the session ID is embedded in the URL or stored in an HTTP cookie.</summary>
	/// <returns>
	///     <see langword="true" /> if the session is embedded in the URL; otherwise, <see langword="false" />.</returns>
	public bool IsCookieless => container.IsCookieless;

	/// <summary>Gets a value indicating whether the session was created with the current request.</summary>
	/// <returns>
	///     <see langword="true" /> if the session was created with the current request; otherwise, <see langword="false" />.</returns>
	public bool IsNewSession => container.IsNewSession;

	/// <summary>Gets a value indicating whether the session is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the session is read-only; otherwise, <see langword="false" />.</returns>
	public bool IsReadOnly => container.IsReadOnly;

	/// <summary>Gets a value indicating whether access to the collection of session-state values is synchronized (thread safe).</summary>
	/// <returns>
	///     <see langword="true" /> if access to the collection is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
	public bool IsSynchronized => container.IsSynchronized;

	/// <summary>Gets or sets a session value by name.</summary>
	/// <param name="name">The key name of the session value. </param>
	/// <returns>The session-state value with the specified name, or <see langword="null" /> if the item does not exist.</returns>
	public object this[string name]
	{
		get
		{
			return container[name];
		}
		set
		{
			container[name] = value;
		}
	}

	/// <summary>Gets or sets a session value by numerical index.</summary>
	/// <param name="index">The numerical index of the session value. </param>
	/// <returns>The session-state value stored at the specified index, or <see langword="null" /> if the item does not exist.</returns>
	public object this[int index]
	{
		get
		{
			return container[index];
		}
		set
		{
			container[index] = value;
		}
	}

	/// <summary>Gets a collection of the keys for all values stored in the session-state collection.</summary>
	/// <returns>The <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> that contains all the session keys.</returns>
	public NameObjectCollectionBase.KeysCollection Keys => container.Keys;

	/// <summary>Gets or sets the locale identifier (LCID) of the current session.</summary>
	/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> instance that specifies the culture of the current session.</returns>
	public int LCID
	{
		get
		{
			return container.LCID;
		}
		set
		{
			container.LCID = value;
		}
	}

	/// <summary>Gets the current session-state mode.</summary>
	/// <returns>One of the <see cref="T:System.Web.SessionState.SessionStateMode" /> values.</returns>
	public SessionStateMode Mode => container.Mode;

	/// <summary>Gets the unique identifier for the session.</summary>
	/// <returns>The unique session identifier.</returns>
	public string SessionID => container.SessionID;

	/// <summary>Gets a collection of objects declared by <see langword="&lt;object Runat=&quot;Server&quot; Scope=&quot;Session&quot;/&gt;" /> tags within the ASP.NET application file Global.asax.</summary>
	/// <returns>An <see cref="T:System.Web.HttpStaticObjectsCollection" /> containing objects declared in the Global.asax file.</returns>
	public HttpStaticObjectsCollection StaticObjects => container.StaticObjects;

	/// <summary>Gets an object that can be used to synchronize access to the collection of session-state values.</summary>
	/// <returns>An object that can be used to synchronize access to the collection.</returns>
	public object SyncRoot => container.SyncRoot;

	/// <summary>Gets and sets the amount of time, in minutes, allowed between requests before the session-state provider terminates the session.</summary>
	/// <returns>The time-out period, in minutes.</returns>
	public int Timeout
	{
		get
		{
			return container.Timeout;
		}
		set
		{
			container.Timeout = value;
		}
	}

	internal HttpSessionState(IHttpSessionState container)
	{
		this.container = container;
	}

	/// <summary>Cancels the current session.</summary>
	public void Abandon()
	{
		container.Abandon();
	}

	/// <summary>Adds a new item to the session-state collection.</summary>
	/// <param name="name">The name of the item to add to the session-state collection. </param>
	/// <param name="value">The value of the item to add to the session-state collection. </param>
	public void Add(string name, object value)
	{
		container.Add(name, value);
	}

	/// <summary>Removes all keys and values from the session-state collection.</summary>
	public void Clear()
	{
		container.Clear();
	}

	/// <summary>Copies the collection of session-state values to a one-dimensional array, starting at the specified index in the array.</summary>
	/// <param name="array">The <see cref="T:System.Array" /> that receives the session values. </param>
	/// <param name="index">The zero-based index in <paramref name="array" /> from which copying starts. </param>
	public void CopyTo(Array array, int index)
	{
		container.CopyTo(array, index);
	}

	/// <summary>Returns an enumerator that can be used to read all the session-state variable names in the current session.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can iterate through the variable names in the session-state collection.</returns>
	public IEnumerator GetEnumerator()
	{
		return container.GetEnumerator();
	}

	/// <summary>Deletes an item from the session-state collection.</summary>
	/// <param name="name">The name of the item to delete from the session-state collection. </param>
	public void Remove(string name)
	{
		container.Remove(name);
	}

	/// <summary>Removes all keys and values from the session-state collection.</summary>
	public void RemoveAll()
	{
		container.Clear();
	}

	/// <summary>Deletes an item at a specified index from the session-state collection.</summary>
	/// <param name="index">The index of the item to remove from the session-state collection. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is less than zero.- or -
	///         <paramref name="index" /> is equal to or greater than <see cref="P:System.Web.SessionState.HttpSessionState.Count" />.</exception>
	public void RemoveAt(int index)
	{
		container.RemoveAt(index);
	}
}
