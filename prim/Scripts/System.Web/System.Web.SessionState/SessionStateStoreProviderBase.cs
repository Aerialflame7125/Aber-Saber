using System.Configuration.Provider;

namespace System.Web.SessionState;

/// <summary>Defines the required members of a session-state provider for a data store.</summary>
public abstract class SessionStateStoreProviderBase : ProviderBase
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Web.SessionState.SessionStateStoreProviderBase" /> class.</summary>
	protected SessionStateStoreProviderBase()
	{
	}

	/// <summary>Creates a new <see cref="T:System.Web.SessionState.SessionStateStoreData" /> object to be used for the current request.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> for the current request.</param>
	/// <param name="timeout">The session-state <see cref="P:System.Web.SessionState.HttpSessionState.Timeout" /> value for the new <see cref="T:System.Web.SessionState.SessionStateStoreData" />.</param>
	/// <returns>A new <see cref="T:System.Web.SessionState.SessionStateStoreData" /> for the current request.</returns>
	public abstract SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout);

	/// <summary>Adds a new session-state item to the data store.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> for the current request.</param>
	/// <param name="id">The <see cref="P:System.Web.SessionState.HttpSessionState.SessionID" /> for the current request.</param>
	/// <param name="timeout">The session <see cref="P:System.Web.SessionState.HttpSessionState.Timeout" /> for the current request.</param>
	public abstract void CreateUninitializedItem(HttpContext context, string id, int timeout);

	/// <summary>Releases all resources used by the <see cref="T:System.Web.SessionState.SessionStateStoreProviderBase" /> implementation.</summary>
	public abstract void Dispose();

	/// <summary>Called by the <see cref="T:System.Web.SessionState.SessionStateModule" /> object at the end of a request.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> for the current request.</param>
	public abstract void EndRequest(HttpContext context);

	/// <summary>Returns read-only session-state data from the session data store.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> for the current request.</param>
	/// <param name="id">The <see cref="P:System.Web.SessionState.HttpSessionState.SessionID" /> for the current request.</param>
	/// <param name="locked">When this method returns, contains a Boolean value that is set to <see langword="true" /> if the requested session item is locked at the session data store; otherwise, <see langword="false" />.</param>
	/// <param name="lockAge">When this method returns, contains a <see cref="T:System.TimeSpan" /> object that is set to the amount of time that an item in the session data store has been locked.</param>
	/// <param name="lockId">When this method returns, contains an object that is set to the lock identifier for the current request. For details on the lock identifier, see "Locking Session-Store Data" in the <see cref="T:System.Web.SessionState.SessionStateStoreProviderBase" /> class summary.</param>
	/// <param name="actions">When this method returns, contains one of the <see cref="T:System.Web.SessionState.SessionStateActions" /> values, indicating whether the current session is an uninitialized, cookieless session.</param>
	/// <returns>A <see cref="T:System.Web.SessionState.SessionStateStoreData" /> populated with session values and information from the session data store.</returns>
	public abstract SessionStateStoreData GetItem(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions);

	/// <summary>Returns read-only session-state data from the session data store.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> for the current request.</param>
	/// <param name="id">The <see cref="P:System.Web.SessionState.HttpSessionState.SessionID" /> for the current request.</param>
	/// <param name="locked">When this method returns, contains a Boolean value that is set to <see langword="true" /> if a lock is successfully obtained; otherwise, <see langword="false" />.</param>
	/// <param name="lockAge">When this method returns, contains a <see cref="T:System.TimeSpan" /> object that is set to the amount of time that an item in the session data store has been locked.</param>
	/// <param name="lockId">When this method returns, contains an object that is set to the lock identifier for the current request. For details on the lock identifier, see "Locking Session-Store Data" in the <see cref="T:System.Web.SessionState.SessionStateStoreProviderBase" /> class summary.</param>
	/// <param name="actions">When this method returns, contains one of the <see cref="T:System.Web.SessionState.SessionStateActions" /> values, indicating whether the current session is an uninitialized, cookieless session.</param>
	/// <returns>A <see cref="T:System.Web.SessionState.SessionStateStoreData" /> populated with session values and information from the session data store.</returns>
	public abstract SessionStateStoreData GetItemExclusive(HttpContext context, string id, out bool locked, out TimeSpan lockAge, out object lockId, out SessionStateActions actions);

	/// <summary>Called by the <see cref="T:System.Web.SessionState.SessionStateModule" /> object for per-request initialization.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> for the current request.</param>
	public abstract void InitializeRequest(HttpContext context);

	/// <summary>Releases a lock on an item in the session data store.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> for the current request.</param>
	/// <param name="id">The session identifier for the current request.</param>
	/// <param name="lockId">The lock identifier for the current request. </param>
	public abstract void ReleaseItemExclusive(HttpContext context, string id, object lockId);

	/// <summary>Deletes item data from the session data store.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> for the current request.</param>
	/// <param name="id">The session identifier for the current request.</param>
	/// <param name="lockId">The lock identifier for the current request.</param>
	/// <param name="item">The <see cref="T:System.Web.SessionState.SessionStateStoreData" /> that represents the item to delete from the data store.</param>
	public abstract void RemoveItem(HttpContext context, string id, object lockId, SessionStateStoreData item);

	/// <summary>Updates the expiration date and time of an item in the session data store.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> for the current request.</param>
	/// <param name="id">The session identifier for the current request.</param>
	public abstract void ResetItemTimeout(HttpContext context, string id);

	/// <summary>Updates the session-item information in the session-state data store with values from the current request, and clears the lock on the data.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> for the current request.</param>
	/// <param name="id">The session identifier for the current request.</param>
	/// <param name="item">The <see cref="T:System.Web.SessionState.SessionStateStoreData" /> object that contains the current session values to be stored.</param>
	/// <param name="lockId">The lock identifier for the current request. </param>
	/// <param name="newItem">
	///       <see langword="true" /> to identify the session item as a new item; <see langword="false" /> to identify the session item as an existing item.</param>
	public abstract void SetAndReleaseItemExclusive(HttpContext context, string id, SessionStateStoreData item, object lockId, bool newItem);

	/// <summary>Sets a reference to the <see cref="T:System.Web.SessionState.SessionStateItemExpireCallback" /> delegate for the <see langword="Session_OnEnd" /> event defined in the Global.asax file.</summary>
	/// <param name="expireCallback">The <see cref="T:System.Web.SessionState.SessionStateItemExpireCallback" />  delegate for the <see langword="Session_OnEnd" /> event defined in the Global.asax file.</param>
	/// <returns>
	///     <see langword="true" /> if the session-state store provider supports calling the <see langword="Session_OnEnd" /> event; otherwise, <see langword="false" />.</returns>
	public abstract bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback);
}
