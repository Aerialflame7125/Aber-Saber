using System.Collections;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Web.SessionState;

namespace System.Web;

/// <summary>Serves as the base class for classes that provides access to session-state values, session-level settings, and lifetime management methods.</summary>
[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
public abstract class HttpSessionStateBase : ICollection, IEnumerable
{
	/// <summary>When overridden in a derived class, gets or sets the character-set identifier for the current session.</summary>
	/// <returns>The character-set identifier for the current session.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int CodePage
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a reference to the current session-state object.</summary>
	/// <returns>The current session-state object.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpSessionStateBase Contents
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the application is configured for cookieless sessions.</summary>
	/// <returns>One of the cookie-mode values that indicate whether the application is configured for cookieless sessions.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpCookieMode CookieMode
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the session ID is embedded in the URL.</summary>
	/// <returns>
	///     <see langword="true" /> if the session ID is embedded in the URL; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool IsCookieless
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the session was created during the current request.</summary>
	/// <returns>
	///     <see langword="true" /> if the session was created during the current request; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool IsNewSession
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether the session is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the session is read-only; otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool IsReadOnly
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a collection of the keys for all values that are stored in the session-state collection.</summary>
	/// <returns>The session keys.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual NameObjectCollectionBase.KeysCollection Keys
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets or sets the locale identifier (LCID) of the current session.</summary>
	/// <returns>The LCID (culture) of the current session.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int LCID
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the current session-state mode.</summary>
	/// <returns>The session-state mode.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual SessionStateMode Mode
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the unique identifier for the session.</summary>
	/// <returns>The unique session identifier.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual string SessionID
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a collection of objects that are declared by <see langword="object" /> elements that are marked as server controls and scoped to the current session in the application's Global.asax file.</summary>
	/// <returns>The objects that are declared in the Global.asax file.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual HttpStaticObjectsCollectionBase StaticObjects
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets or sets the time, in minutes, that can elapse between requests before the session-state provider ends the session.</summary>
	/// <returns>The time-out period, in minutes.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int Timeout
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets or sets a session value by using the specified index.</summary>
	/// <param name="index">The index of the session value.</param>
	/// <returns>The session-state value that is stored at the specified index.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual object this[int index]
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets or sets a session value by using the specified name.</summary>
	/// <param name="name">The key name of the session value.</param>
	/// <returns>The session-state value that has the specified name, or <see langword="null" /> if the item does not exist.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual object this[string name]
	{
		get
		{
			throw new NotImplementedException();
		}
		set
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets the number of items in the session-state collection.</summary>
	/// <returns>The number of items in the collection.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual int Count
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets a value that indicates whether access to the collection of session-state values is synchronized (thread safe).</summary>
	/// <returns>
	///     <see langword="true" /> if access to the collection is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual bool IsSynchronized
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, gets an object that can be used to synchronize access to the collection of session-state values.</summary>
	/// <returns>An object that can be used to synchronize access to the collection.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual object SyncRoot
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	/// <summary>When overridden in a derived class, cancels the current session.</summary>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Abandon()
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, adds an item to the session-state collection.</summary>
	/// <param name="name">The name of the item to add to the session-state collection.</param>
	/// <param name="value">The value of the item to add to the session-state collection.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Add(string name, object value)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, removes all keys and values from the session-state collection.</summary>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Clear()
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, deletes an item from the session-state collection.</summary>
	/// <param name="name">The name of the item to delete from the session-state collection.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void Remove(string name)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, removes all keys and values from the session-state collection.</summary>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RemoveAll()
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, deletes the item at the specified index from the session-state collection.</summary>
	/// <param name="index">The index of the item to remove from the session-state collection.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void RemoveAt(int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, copies the collection of session-state values to a one-dimensional array, starting at the specified index in the array.</summary>
	/// <param name="array">The array to copy session values to.</param>
	/// <param name="index">The zero-based index in <paramref name="array" /> at which copying starts.</param>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual void CopyTo(Array array, int index)
	{
		throw new NotImplementedException();
	}

	/// <summary>When overridden in a derived class, returns an enumerator that can be used to read all the session-state variable names in the current session.</summary>
	/// <returns>An enumerator that can iterate through the variable names in the session-state collection.</returns>
	/// <exception cref="T:System.NotImplementedException">Always.</exception>
	public virtual IEnumerator GetEnumerator()
	{
		throw new NotImplementedException();
	}

	/// <summary>Initializes the class for use by an inherited class instance. This constructor can only be called by an inherited class.</summary>
	protected HttpSessionStateBase()
	{
	}
}
