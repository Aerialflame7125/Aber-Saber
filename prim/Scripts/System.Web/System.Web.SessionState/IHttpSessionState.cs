using System.Collections;
using System.Collections.Specialized;

namespace System.Web.SessionState;

/// <summary>Defines the contract to implement a custom session-state container.</summary>
public interface IHttpSessionState
{
	/// <summary>Gets or sets the code-page identifier for the current session.</summary>
	/// <returns>The code-page identifier for the current session.</returns>
	int CodePage { get; set; }

	/// <summary>Gets a value that indicates whether the application is configured for cookieless sessions.</summary>
	/// <returns>One of the <see cref="T:System.Web.HttpCookieMode" /> values that indicate whether the application is configured for cookieless sessions. The default is <see cref="F:System.Web.HttpCookieMode.UseCookies" />.</returns>
	HttpCookieMode CookieMode { get; }

	/// <summary>Gets the number of items in the session-state item collection.</summary>
	/// <returns>The number of items in the session-state item collection.</returns>
	int Count { get; }

	/// <summary>Gets a value indicating whether the session ID is embedded in the URL or stored in an HTTP cookie.</summary>
	/// <returns>
	///     <see langword="true" /> if the session is embedded in the URL; otherwise, <see langword="false" />.</returns>
	bool IsCookieless { get; }

	/// <summary>Gets a value indicating whether the session was created with the current request.</summary>
	/// <returns>
	///     <see langword="true" /> if the session was created with the current request; otherwise, <see langword="false" />.</returns>
	bool IsNewSession { get; }

	/// <summary>Gets a value indicating whether the session is read-only.</summary>
	/// <returns>
	///     <see langword="true" /> if the session is read-only; otherwise, <see langword="false" />.</returns>
	bool IsReadOnly { get; }

	/// <summary>Gets a value indicating whether access to the collection of session-state values is synchronized (thread safe).</summary>
	/// <returns>
	///     <see langword="true" /> if access to the collection is synchronized (thread safe); otherwise, <see langword="false" />.</returns>
	bool IsSynchronized { get; }

	/// <summary>Gets or sets a session-state item value by numerical index.</summary>
	/// <param name="index">The numerical index of the session-state item value. </param>
	/// <returns>The session-state item value specified in the <paramref name="index" /> parameter.</returns>
	object this[int index] { get; set; }

	/// <summary>Gets or sets a session-state item value by name.</summary>
	/// <param name="name">The key name of the session-state item value. </param>
	/// <returns>The session-state item value specified in the <paramref name="name" /> parameter.</returns>
	object this[string name] { get; set; }

	/// <summary>Gets a collection of the keys for all values stored in the session-state item collection.</summary>
	/// <returns>The <see cref="T:System.Collections.Specialized.NameObjectCollectionBase.KeysCollection" /> that contains all the session-item keys.</returns>
	NameObjectCollectionBase.KeysCollection Keys { get; }

	/// <summary>Gets or sets the locale identifier (LCID) of the current session.</summary>
	/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> instance that specifies the culture of the current session.</returns>
	int LCID { get; set; }

	/// <summary>Gets the current session-state mode.</summary>
	/// <returns>One of the <see cref="T:System.Web.SessionState.SessionStateMode" /> values.</returns>
	SessionStateMode Mode { get; }

	/// <summary>Gets the unique session identifier for the session.</summary>
	/// <returns>The session ID.</returns>
	string SessionID { get; }

	/// <summary>Gets a collection of objects declared by <see langword="&lt;object Runat=&quot;Server&quot; Scope=&quot;Session&quot;/&gt;" /> tags within the ASP.NET application file Global.asax.</summary>
	/// <returns>An <see cref="T:System.Web.HttpStaticObjectsCollection" /> containing objects declared in the Global.asax file.</returns>
	HttpStaticObjectsCollection StaticObjects { get; }

	/// <summary>Gets an object that can be used to synchronize access to the collection of session-state values.</summary>
	/// <returns>An object that can be used to synchronize access to the collection.</returns>
	object SyncRoot { get; }

	/// <summary>Gets and sets the time-out period (in minutes) allowed between requests before the session-state provider terminates the session.</summary>
	/// <returns>The time-out period, in minutes.</returns>
	int Timeout { get; set; }

	/// <summary>Ends the current session.</summary>
	void Abandon();

	/// <summary>Adds a new item to the session-state collection.</summary>
	/// <param name="name">The name of the item to add to the session-state collection. </param>
	/// <param name="value">The value of the item to add to the session-state collection. </param>
	void Add(string name, object value);

	/// <summary>Clears all values from the session-state item collection.</summary>
	void Clear();

	/// <summary>Copies the collection of session-state item values to a one-dimensional array, starting at the specified index in the array.</summary>
	/// <param name="array">The <see cref="T:System.Array" /> that receives the session values. </param>
	/// <param name="index">The index in <paramref name="array" /> where copying starts. </param>
	void CopyTo(Array array, int index);

	/// <summary>Returns an enumerator that can be used to read all the session-state item values in the current session.</summary>
	/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can iterate through the values in the session-state item collection.</returns>
	IEnumerator GetEnumerator();

	/// <summary>Deletes an item from the session-state item collection.</summary>
	/// <param name="name">The name of the item to delete from the session-state item collection. </param>
	void Remove(string name);

	/// <summary>Clears all values from the session-state item collection.</summary>
	void RemoveAll();

	/// <summary>Deletes an item at a specified index from the session-state item collection.</summary>
	/// <param name="index">The index of the item to remove from the session-state collection. </param>
	void RemoveAt(int index);
}
