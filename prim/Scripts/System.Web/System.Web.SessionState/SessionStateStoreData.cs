namespace System.Web.SessionState;

/// <summary>Represents session-state data for a session store.</summary>
public class SessionStateStoreData
{
	private ISessionStateItemCollection sessionItems;

	private HttpStaticObjectsCollection staticObjects;

	private int timeout;

	/// <summary>The session variables and values for the current session.</summary>
	/// <returns>An <see cref="T:System.Web.SessionState.ISessionStateItemCollection" /> object that contains variables and values for the current session.</returns>
	public virtual ISessionStateItemCollection Items => sessionItems;

	/// <summary>Gets a collection of objects declared by <see langword="&lt;object Runat=&quot;Server&quot; Scope=&quot;Session&quot;/&gt;" /> tags within the ASP.NET application file Global.asax.</summary>
	/// <returns>An <see cref="T:System.Web.HttpStaticObjectsCollection" /> containing objects declared in the Global.asax file.</returns>
	public virtual HttpStaticObjectsCollection StaticObjects => staticObjects;

	/// <summary>Gets and sets the amount of time, in minutes, allowed between requests before the session-state provider terminates the session.</summary>
	/// <returns>The time-out period in minutes.</returns>
	public virtual int Timeout
	{
		get
		{
			return timeout;
		}
		set
		{
			timeout = value;
		}
	}

	/// <summary>Creates a new instance of the <see cref="T:System.Web.SessionState.SessionStateStoreData" /> class.</summary>
	/// <param name="sessionItems">The session variables and values for the current session.</param>
	/// <param name="staticObjects">The <see cref="T:System.Web.HttpStaticObjectsCollection" /> for the current session.</param>
	/// <param name="timeout">The <see cref="P:System.Web.SessionState.SessionStateStoreData.Timeout" /> for the current session.</param>
	public SessionStateStoreData(ISessionStateItemCollection sessionItems, HttpStaticObjectsCollection staticObjects, int timeout)
	{
		this.sessionItems = sessionItems;
		this.staticObjects = staticObjects;
		this.timeout = timeout;
	}
}
