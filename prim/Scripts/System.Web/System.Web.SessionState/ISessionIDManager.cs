namespace System.Web.SessionState;

/// <summary>Defines the contract that a custom session-state identifier manager must implement.</summary>
public interface ISessionIDManager
{
	/// <summary>Creates a unique session identifier.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> object that references server objects used to process HTTP requests (for example, the <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties). </param>
	/// <returns>A unique session identifier.</returns>
	string CreateSessionID(HttpContext context);

	/// <summary>Gets the session identifier from the context of the current HTTP request.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> object that references server objects used to process HTTP requests (for example, the <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties).</param>
	/// <returns>The current session identifier sent with the HTTP request.</returns>
	string GetSessionID(HttpContext context);

	/// <summary>Initializes the <see cref="T:System.Web.SessionState.SessionIDManager" /> object.</summary>
	void Initialize();

	/// <summary>Performs per-request initialization of the <see cref="T:System.Web.SessionState.SessionIDManager" /> object.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> object that contains information about the current request.</param>
	/// <param name="suppressAutoDetectRedirect">
	///       <see langword="true" /> if the session-ID manager should redirect to determine cookie support; otherwise, <see langword="false" /> to suppress automatic redirection to determine cookie support.</param>
	/// <param name="supportSessionIDReissue">When this method returns, contains a Boolean that indicates whether the <see cref="T:System.Web.SessionState.ISessionIDManager" /> object supports issuing new session IDs when the original ID is out of date. This parameter is passed uninitialized. Session ID reuse is appropriate when the session-state ID is encoded on a URL and the potential exists for the URL to be shared or emailed.If a custom session-state implementation partitions cookies by virtual path, session state should also be supported.</param>
	/// <returns>
	///     <see langword="true" /> to indicate that the initialization performed a redirect; otherwise, <see langword="false" />.</returns>
	bool InitializeRequest(HttpContext context, bool suppressAutoDetectRedirect, out bool supportSessionIDReissue);

	/// <summary>Deletes the session identifier from the cookie or from the URL.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> object that references server objects used to process HTTP requests (for example, the <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties).</param>
	void RemoveSessionID(HttpContext context);

	/// <summary>Saves a newly created session identifier to the HTTP response.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> object that references server objects used to process HTTP requests (for example, the <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties).</param>
	/// <param name="id">The session identifier. </param>
	/// <param name="redirected">When this method returns, contains a Boolean value that is <see langword="true" /> if the response is redirected to the current URL with the session identifier added to the URL; otherwise, <see langword="false" />. </param>
	/// <param name="cookieAdded">When this method returns, contains a Boolean value that is <see langword="true" /> if a cookie has been added to the HTTP response; otherwise, <see langword="false" />. </param>
	void SaveSessionID(HttpContext context, string id, out bool redirected, out bool cookieAdded);

	/// <summary>Confirms that the supplied session identifier is valid.</summary>
	/// <param name="id">The session identifier to validate. </param>
	/// <returns>
	///     <see langword="true" /> if the session identifier is valid; otherwise, <see langword="false" />.</returns>
	bool Validate(string id);
}
