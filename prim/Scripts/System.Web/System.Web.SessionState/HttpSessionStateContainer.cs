using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Threading;

namespace System.Web.SessionState;

/// <summary>Contains session-state values as well as session-level settings for the current request.</summary>
public class HttpSessionStateContainer : IHttpSessionState
{
	private string id;

	private HttpStaticObjectsCollection staticObjects;

	private int timeout;

	private bool newSession;

	private bool isCookieless;

	private SessionStateMode mode;

	private bool isReadOnly;

	internal bool abandoned;

	private ISessionStateItemCollection sessionItems;

	private HttpCookieMode cookieMode;

	/// <summary>Gets or sets the character-set identifier for the current session.</summary>
	/// <returns>The character-set identifier for the current session.</returns>
	public int CodePage
	{
		get
		{
			return HttpContext.Current?.Response.ContentEncoding.CodePage ?? Encoding.Default.CodePage;
		}
		set
		{
			HttpContext current = HttpContext.Current;
			if (current != null)
			{
				current.Response.ContentEncoding = Encoding.GetEncoding(value);
			}
		}
	}

	/// <summary>Gets a value that indicates whether the application is configured for cookieless sessions.</summary>
	/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values that indicates whether the application is configured for cookieless sessions. The default is <see cref="F:System.Web.HttpCookieMode.UseCookies" />.</returns>
	public HttpCookieMode CookieMode => cookieMode;

	/// <summary>Gets the number of items in the session-state collection.</summary>
	/// <returns>The number of items in the collection.</returns>
	public int Count
	{
		get
		{
			if (sessionItems != null)
			{
				return sessionItems.Count;
			}
			return 0;
		}
	}

	/// <summary>Gets a value indicating whether the current session has been abandoned.</summary>
	/// <returns>
	///     <see langword="true" /> if the current session has been abandoned; otherwise, <see langword="false" />.</returns>
	public bool IsAbandoned => abandoned;

	/// <summary>Gets a value indicating whether the session ID is embedded in the URL or stored in an HTTP cookie.</summary>
	/// <returns>
	///     <see langword="true" /> if the session is embedded in the URL; otherwise, <see langword="false" />.</returns>
	public bool IsCookieless => isCookieless;

	/// <summary>Gets a value indicating whether the session was created with the current request.</summary>
	/// <returns>
	///     <see langword="true" /> if the session was created with the current request; otherwise, <see langword="false" />.</returns>
	public bool IsNewSession => newSession;

	/// <summary>Gets a value indicating whether the session is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the session is read-only; otherwise, <see langword="false" />.</returns>
	public bool IsReadOnly => isReadOnly;

	/// <summary>Gets a value indicating whether access to the collection of session-state values is synchronized (thread safe).</summary>
	/// <returns>Always <see langword="false" />, because thread-safe <see cref="T:System.Web.SessionState.HttpSessionStateContainer" /> objects are not supported.</returns>
	public bool IsSynchronized => false;

	object IHttpSessionState.this[int index]
	{
		get
		{
			if (sessionItems == null || sessionItems.Count == 0)
			{
				return null;
			}
			return sessionItems[index];
		}
		set
		{
			if (sessionItems != null)
			{
				sessionItems[index] = value;
			}
		}
	}

	object IHttpSessionState.this[string name]
	{
		get
		{
			if (sessionItems == null || sessionItems.Count == 0)
			{
				return null;
			}
			return sessionItems[name];
		}
		set
		{
			if (sessionItems != null)
			{
				sessionItems[name] = value;
			}
		}
	}

	NameObjectCollectionBase.KeysCollection IHttpSessionState.Keys
	{
		get
		{
			if (sessionItems != null)
			{
				return sessionItems.Keys;
			}
			return null;
		}
	}

	/// <summary>Gets or sets the locale identifier (LCID) of the current session.</summary>
	/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> instance that specifies the culture of the current session.</returns>
	public int LCID
	{
		get
		{
			return Thread.CurrentThread.CurrentCulture.LCID;
		}
		set
		{
			Thread.CurrentThread.CurrentCulture = new CultureInfo(value);
		}
	}

	/// <summary>Gets the current session-state mode.</summary>
	/// <returns>One of the <see cref="T:System.Web.SessionState.SessionStateMode" /> values.</returns>
	public SessionStateMode Mode => mode;

	/// <summary>Gets the unique identifier for the session.</summary>
	/// <returns>The unique session identifier.</returns>
	public string SessionID => id;

	/// <summary>Gets a collection of objects declared by <see langword="&lt;object Runat=&quot;Server&quot; Scope=&quot;Session&quot;/&gt;" /> tags within the ASP.NET application file Global.asax.</summary>
	/// <returns>An <see cref="T:System.Web.HttpStaticObjectsCollection" /> containing objects declared in the Global.asax file.</returns>
	public HttpStaticObjectsCollection StaticObjects => staticObjects;

	/// <summary>Gets an object that can be used to synchronize access to the collection of session-state values.</summary>
	/// <returns>An object that can be used to synchronize access to the collection.</returns>
	public object SyncRoot => this;

	/// <summary>Gets and sets the amount of time, in minutes, allowed between requests before the session-state provider terminates the session.</summary>
	/// <returns>The time-out period, in minutes.</returns>
	/// <exception cref="T:System.ArgumentException">An attempt was made to set the <see cref="P:System.Web.SessionState.HttpSessionStateContainer.Timeout" /> value to an integer value less than 1.- or -An attempt was made to set the <see cref="P:System.Web.SessionState.HttpSessionStateContainer.Timeout" /> value to an integer value greater than the maximum allowed when <see cref="P:System.Web.SessionState.HttpSessionState.Mode" /> is set to <see cref="F:System.Web.SessionState.SessionStateMode.InProc" /> or <see cref="F:System.Web.SessionState.SessionStateMode.StateServer" />. The maximum allowed is 525,600 (one year). </exception>
	public int Timeout
	{
		get
		{
			return timeout;
		}
		set
		{
			if (value < 1)
			{
				throw new ArgumentException("The argument to SetTimeout must be greater than 0.");
			}
			timeout = value;
		}
	}

	/// <summary>Creates a new <see cref="T:System.Web.SessionState.HttpSessionStateContainer" /> object and initializes it with the specified settings and values.</summary>
	/// <param name="id">A session identifier for the new session. If <see langword="null" />, an <see cref="T:System.ArgumentException" /> is thrown.</param>
	/// <param name="sessionItems">An <see cref="T:System.Web.SessionState.ISessionStateItemCollection" /> that contains the session values for the new session-state provider.</param>
	/// <param name="staticObjects">An <see cref="T:System.Web.HttpStaticObjectsCollection" /> that specifies the objects declared by <see langword="&lt;object Runat=&quot;Server&quot; Scope=&quot;Session&quot;/&gt;" /> tags within the ASP.NET application file Global.asax.</param>
	/// <param name="timeout">The amount of time, in minutes, allowed between requests before the session-state provider terminates the session.</param>
	/// <param name="newSession">
	///       <see langword="true" /> to indicate the session was created with the current request; otherwise, <see langword="false" />. </param>
	/// <param name="cookieMode">The <see cref="P:System.Web.SessionState.HttpSessionStateContainer.CookieMode" /> for the new session-state provider.</param>
	/// <param name="mode">One of the <see cref="T:System.Web.SessionState.SessionStateMode" /> values that specifies the current session-state mode. </param>
	/// <param name="isReadonly">
	///       <see langword="true" /> to indicate the session is read-only; otherwise, <see langword="false" />.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="id" /> is <see langword="null" />.</exception>
	public HttpSessionStateContainer(string id, ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout, bool newSession, HttpCookieMode cookieMode, SessionStateMode mode, bool isReadonly)
	{
		if (id == null)
		{
			throw new ArgumentNullException("id");
		}
		this.sessionItems = sessionItems;
		this.id = id;
		this.staticObjects = staticObjects;
		this.timeout = timeout;
		this.newSession = newSession;
		this.cookieMode = cookieMode;
		this.mode = mode;
		isReadOnly = isReadonly;
		isCookieless = cookieMode == HttpCookieMode.UseUri;
	}

	internal void SetNewSession(bool value)
	{
		newSession = value;
	}

	/// <summary>Marks the current session as abandoned.</summary>
	public void Abandon()
	{
		abandoned = true;
	}

	/// <summary>Adds a new item to the session-state collection.</summary>
	/// <param name="name">The name of the item to add to the session-state collection. </param>
	/// <param name="value">The value of the item to add to the session-state collection. </param>
	public void Add(string name, object value)
	{
		if (sessionItems != null)
		{
			sessionItems[name] = value;
		}
	}

	/// <summary>Removes all values and keys from the session-state collection.</summary>
	public void Clear()
	{
		if (sessionItems != null)
		{
			sessionItems.Clear();
		}
	}

	/// <summary>Copies the collection of session-state values to a one-dimensional array, starting at the specified index in the array.</summary>
	/// <param name="array">The <see cref="T:System.Array" /> that receives the session values. </param>
	/// <param name="index">The zero-based index in <paramref name="array" /> from which copying starts. </param>
	public void CopyTo(Array array, int index)
	{
		if (sessionItems != null)
		{
			NameObjectCollectionBase.KeysCollection keys = sessionItems.Keys;
			for (int i = 0; i < keys.Count; i++)
			{
				array.SetValue(keys.Get(i), i + index);
			}
		}
	}

	/// <summary>Returns an enumerator that can be used to read all the session-state variable names in the current session.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can iterate through the variable names in the session-state collection.</returns>
	public IEnumerator GetEnumerator()
	{
		if (sessionItems == null)
		{
			return null;
		}
		return sessionItems.GetEnumerator();
	}

	/// <summary>Deletes an item from the session-state collection.</summary>
	/// <param name="name">The name of the item to delete from the session-state collection. </param>
	public void Remove(string name)
	{
		if (sessionItems != null)
		{
			sessionItems.Remove(name);
		}
	}

	/// <summary>Clears all session-state values.</summary>
	public void RemoveAll()
	{
		if (sessionItems != null)
		{
			sessionItems.Clear();
		}
	}

	/// <summary>Deletes an item at a specified index from the session-state collection.</summary>
	/// <param name="index">The index of the item to remove from the session-state collection. </param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is less than zero.- or -
	///         <paramref name="index" /> is equal to or greater than <see cref="P:System.Web.SessionState.HttpSessionStateContainer.Count" />.</exception>
	public void RemoveAt(int index)
	{
		if (sessionItems != null)
		{
			sessionItems.RemoveAt(index);
		}
	}
}
