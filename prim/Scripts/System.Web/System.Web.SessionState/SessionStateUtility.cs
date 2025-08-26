namespace System.Web.SessionState;

/// <summary>Provides helper methods used by session-state modules and session-state store providers to manage session information for an ASP.NET application. This class cannot be inherited.</summary>
public static class SessionStateUtility
{
	/// <summary>Applies the session data to the context for the current request.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> object to which to add the <see cref="T:System.Web.SessionState.HttpSessionState" /> object.</param>
	/// <param name="container">The <see cref="T:System.Web.SessionState.IHttpSessionState" /> implementation instance to add to the specified HTTP context.</param>
	/// <exception cref="T:System.Web.HttpException">An <see cref="T:System.Web.SessionState.HttpSessionState" /> object for the current session has already been added to the specified <paramref name="context" />.</exception>
	public static void AddHttpSessionStateToContext(HttpContext context, IHttpSessionState container)
	{
		if (context != null && container != null)
		{
			if (context.Session != null)
			{
				throw new HttpException("An HttpSessionState object for the current session has already been added to the specified context.");
			}
			HttpSessionState session = new HttpSessionState(container);
			context.SetSession(session);
		}
	}

	/// <summary>Retrieves session data from the context for the current request.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> from which to retrieve session data.</param>
	/// <returns>An <see cref="T:System.Web.SessionState.IHttpSessionState" /> implementation instance populated with session data from the current request.</returns>
	public static IHttpSessionState GetHttpSessionStateFromContext(HttpContext context)
	{
		HttpSessionState session;
		if (context == null || (session = context.Session) == null)
		{
			return null;
		}
		return session.Container;
	}

	/// <summary>Gets a reference to the static objects collection for the specified context.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> from which to get the static objects collection.</param>
	/// <returns>An <see cref="T:System.Web.HttpStaticObjectsCollection" /> collection populated with the <see cref="P:System.Web.SessionState.HttpSessionState.StaticObjects" /> property value for the specified <see cref="T:System.Web.HttpContext" />.</returns>
	public static HttpStaticObjectsCollection GetSessionStaticObjects(HttpContext context)
	{
		HttpSessionState session;
		if (context == null || (session = context.Session) == null)
		{
			return null;
		}
		return session.Container.StaticObjects;
	}

	/// <summary>Executes the Session_OnEnd event defined in the Global.asax file for the ASP.NET application.</summary>
	/// <param name="session">The <see cref="T:System.Web.SessionState.IHttpSessionState" /> implementation instance for the session that has ended.</param>
	/// <param name="eventSource">The event source object to supply to the <see langword="Session_OnEnd" /> event.</param>
	/// <param name="eventArgs">The <see cref="T:System.EventArgs" /> object to supply to the <see langword="Session_OnEnd" /> event.</param>
	public static void RaiseSessionEnd(IHttpSessionState session, object eventSource, EventArgs eventArgs)
	{
		HttpApplicationFactory.InvokeSessionEnd(new HttpSessionState(session), eventSource, eventArgs);
	}

	/// <summary>Removes session data from the specified context.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> from which to remove session data.</param>
	public static void RemoveHttpSessionStateFromContext(HttpContext context)
	{
		context?.SetSession(null);
	}
}
