using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Web.SessionState;

namespace System.Web;

/// <summary>Encapsulates the HTTP intrinsic object that provides access to session-state values, session-level settings, and lifetime management methods.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public class HttpSessionStateWrapper : HttpSessionStateBase
{
	private readonly HttpSessionState _session;

	/// <summary>Gets or sets the character-set identifier for the current session.</summary>
	/// <returns>The character-set identifier for the current session.</returns>
	public override int CodePage
	{
		get
		{
			return _session.CodePage;
		}
		set
		{
			_session.CodePage = value;
		}
	}

	/// <summary>Gets a reference to the current session-state object.</summary>
	/// <returns>The current session-state object.</returns>
	public override HttpSessionStateBase Contents => this;

	/// <summary>Gets a value that indicates whether the application is configured for cookieless sessions.</summary>
	/// <returns>One of the cookie-mode values that indicate whether the application is configured for cookieless sessions. The default is <see cref="F:System.Web.HttpCookieMode.UseCookies" />.</returns>
	public override HttpCookieMode CookieMode => _session.CookieMode;

	/// <summary>Gets a value that indicates whether the session ID is embedded in the URL.</summary>
	/// <returns>
	///     <see langword="true" /> if the session ID is embedded in the URL; otherwise, <see langword="false" />.</returns>
	public override bool IsCookieless => _session.IsCookieless;

	/// <summary>Gets a value that indicates whether the session was created during the current request.</summary>
	/// <returns>
	///     <see langword="true" /> if the session was created during the current request; otherwise, <see langword="false" />.</returns>
	public override bool IsNewSession => _session.IsNewSession;

	/// <summary>Gets a value that indicates whether the session is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the session is read-only; otherwise, <see langword="false" />.</returns>
	public override bool IsReadOnly => _session.IsReadOnly;

	/// <summary>Gets a collection of the keys for all values that are stored in the session-state collection.</summary>
	/// <returns>The session keys.</returns>
	public override NameObjectCollectionBase.KeysCollection Keys => _session.Keys;

	/// <summary>Gets or sets the locale identifier (LCID) of the current session.</summary>
	/// <returns>The LCID (culture) of the current session.</returns>
	public override int LCID
	{
		get
		{
			return _session.LCID;
		}
		set
		{
			_session.LCID = value;
		}
	}

	/// <summary>Gets the current session-state mode.</summary>
	/// <returns>The session-state mode.</returns>
	public override SessionStateMode Mode => _session.Mode;

	/// <summary>Gets the unique identifier for the session.</summary>
	/// <returns>The unique session identifier.</returns>
	public override string SessionID => _session.SessionID;

	/// <summary>Gets a collection of objects that are declared by <see langword="object" /> elements that are marked as server controls and scoped to the current session in the application's Global.asax file.</summary>
	/// <returns>The objects that are declared in the Global.asax file.</returns>
	public override HttpStaticObjectsCollectionBase StaticObjects => new HttpStaticObjectsCollectionWrapper(_session.StaticObjects);

	/// <summary>Gets or sets the time, in minutes, that can elapse between requests before the session-state provider ends the session.</summary>
	/// <returns>The time-out period, in minutes.</returns>
	public override int Timeout
	{
		get
		{
			return _session.Timeout;
		}
		set
		{
			_session.Timeout = value;
		}
	}

	/// <summary>Gets or sets a session value by using the specified index.</summary>
	/// <param name="index">The index of the session value.</param>
	/// <returns>The session-state value that is stored at the specified index.</returns>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is outside the valid range of indexes for the collection.</exception>
	public override object this[int index]
	{
		get
		{
			return _session[index];
		}
		set
		{
			_session[index] = value;
		}
	}

	/// <summary>Gets or sets a session value by using the specified name.</summary>
	/// <param name="name">The key name of the session value.</param>
	/// <returns>The session-state value that has the specified name, or <see langword="null" /> if the item does not exist.</returns>
	public override object this[string name]
	{
		get
		{
			return _session[name];
		}
		set
		{
			_session[name] = value;
		}
	}

	/// <summary>Gets the number of items in the session-state collection.</summary>
	/// <returns>The number of items in the collection.</returns>
	public override int Count => _session.Count;

	/// <summary>Gets a value that indicates whether access to the collection of session-state values is synchronized (thread safe).</summary>
	/// <returns>
	///     <see langword="true" /> if access to the collection is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
	public override bool IsSynchronized => _session.IsSynchronized;

	/// <summary>Gets an object that can be used to synchronize access to the collection of session-state values.</summary>
	/// <returns>An object that can be used to synchronize access to the collection.</returns>
	public override object SyncRoot => _session.SyncRoot;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpSessionStateWrapper" /> class.</summary>
	/// <param name="httpSessionState">The object that this wrapper class provides access to.</param>
	/// <exception cref="T:System.ArgumentNullException">
	///         <paramref name="httpSessionState" /> is <see langword="null" />.</exception>
	public HttpSessionStateWrapper(HttpSessionState httpSessionState)
	{
		if (httpSessionState == null)
		{
			throw new ArgumentNullException("httpSessionState");
		}
		_session = httpSessionState;
	}

	/// <summary>Cancels the current session.</summary>
	public override void Abandon()
	{
		_session.Abandon();
	}

	/// <summary>Adds an item to the session-state collection.</summary>
	/// <param name="name">The name of the item to add to the session-state collection.</param>
	/// <param name="value">The value of the item to add to the session-state collection.</param>
	public override void Add(string name, object value)
	{
		_session.Add(name, value);
	}

	/// <summary>Removes all keys and values from the session-state collection.</summary>
	public override void Clear()
	{
		_session.Clear();
	}

	/// <summary>Deletes an item from the session-state collection.</summary>
	/// <param name="name">The name of the item to delete from the session-state collection.</param>
	public override void Remove(string name)
	{
		_session.Remove(name);
	}

	/// <summary>Removes all keys and values from the session-state collection.</summary>
	public override void RemoveAll()
	{
		_session.RemoveAll();
	}

	/// <summary>Deletes the item at the specified index from the session-state collection.</summary>
	/// <param name="index">The index of the item to remove from the session-state collection.</param>
	/// <exception cref="T:System.ArgumentOutOfRangeException">
	///         <paramref name="index" /> is less than zero.- or -
	///         <paramref name="index" /> is equal to or greater than <see cref="P:System.Web.SessionState.HttpSessionState.Count" />.</exception>
	public override void RemoveAt(int index)
	{
		_session.RemoveAt(index);
	}

	/// <summary>Copies the collection of session-state values to a one-dimensional array, starting at the specified index in the array.</summary>
	/// <param name="array">The array to copy session values to</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying starts.</param>
	public override void CopyTo(Array array, int index)
	{
		_session.CopyTo(array, index);
	}

	/// <summary>Returns an enumerator that can be used to read all the session-state variable names in the current session.</summary>
	/// <returns>An enumerator that can iterate through the variable names in the session-state collection.</returns>
	public override IEnumerator GetEnumerator()
	{
		return _session.GetEnumerator();
	}
}
