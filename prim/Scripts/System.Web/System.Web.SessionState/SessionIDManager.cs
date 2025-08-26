using System.Web.Configuration;
using System.Web.Util;

namespace System.Web.SessionState;

/// <summary>Manages unique identifiers for ASP.NET session state.</summary>
public class SessionIDManager : ISessionIDManager
{
	private SessionStateSection config;

	/// <summary>Gets the maximum length of a valid session identifier.</summary>
	/// <returns>The maximum length of a valid session identifier.</returns>
	public static int SessionIDMaxLength => 80;

	/// <summary>Creates an instance of the <see cref="T:System.Web.SessionState.SessionIDManager" /> class.</summary>
	public SessionIDManager()
	{
	}

	/// <summary>Creates a unique session identifier for the session.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> object that references server objects used to process HTTP requests (for example, the <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties).</param>
	/// <returns>A unique session identifier.</returns>
	public virtual string CreateSessionID(HttpContext context)
	{
		return SessionId.Create();
	}

	/// <summary>Decodes a URL-encoded session identifier obtained from a cookie or the URL.</summary>
	/// <param name="id">The session identifier to decode. </param>
	/// <returns>The decoded session identifier.</returns>
	public virtual string Decode(string id)
	{
		return HttpUtility.UrlDecode(id);
	}

	/// <summary>Encodes the session identifier for saving to either a cookie or the URL.</summary>
	/// <param name="id">The session identifier to encode. </param>
	/// <returns>The encoded session identifier.</returns>
	public virtual string Encode(string id)
	{
		return HttpUtility.UrlEncode(id);
	}

	/// <summary>Gets the session-identifier value from the current Web request.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> object that references server objects used to process HTTP requests (for example, the <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties).</param>
	/// <returns>The current <see cref="P:System.Web.SessionState.HttpSessionState.SessionID" />.</returns>
	/// <exception cref="T:System.Web.HttpException">The length of the session-identifier value retrieved from the HTTP request exceeds the <see cref="P:System.Web.SessionState.SessionIDManager.SessionIDMaxLength" /> value.</exception>
	public string GetSessionID(HttpContext context)
	{
		string text = null;
		if (SessionStateModule.IsCookieLess(context, config))
		{
			string text2 = context.Request.Headers["AspFilterSessionId"];
			if (text2 != null)
			{
				text = Decode(text2);
			}
		}
		else
		{
			HttpCookie httpCookie = context.Request.Cookies[config.CookieName];
			if (httpCookie != null)
			{
				text = Decode(httpCookie.Value);
			}
		}
		if (text != null && text.Length > SessionIDMaxLength)
		{
			throw new HttpException("The length of the session-identifier value retrieved from the HTTP request exceeds the SessionIDMaxLength value.");
		}
		if (!Validate(text))
		{
			throw new HttpException("Invalid session ID");
		}
		return text;
	}

	/// <summary>Initializes the <see cref="T:System.Web.SessionState.SessionIDManager" /> object with information from configuration files.</summary>
	public void Initialize()
	{
		config = WebConfigurationManager.GetSection("system.web/sessionState") as SessionStateSection;
	}

	/// <summary>Performs per-request initialization of the <see cref="T:System.Web.SessionState.SessionIDManager" /> object.</summary>
	/// <param name="context">The <see cref="T:System.Web.HttpContext" /> object that contains information about the current request.</param>
	/// <param name="suppressAutoDetectRedirect">
	///       <see langword="true" /> to redirect to determine cookie support; otherwise, <see langword="false" /> to suppress automatic redirection to determine cookie support.</param>
	/// <param name="supportSessionIDReissue">When this method returns, contains a Boolean that indicates whether the <see cref="T:System.Web.SessionState.SessionIDManager" /> object supports issuing new session IDs when the original ID is out of date. This parameter is passed uninitialized.</param>
	/// <returns>
	///     <see langword="true" /> to indicate the <see cref="T:System.Web.SessionState.SessionIDManager" /> object has done a redirect to determine cookie support; otherwise, <see langword="false" />.</returns>
	public bool InitializeRequest(HttpContext context, bool suppressAutoDetectRedirect, out bool supportSessionIDReissue)
	{
		if (config.CookieLess)
		{
			supportSessionIDReissue = true;
			return false;
		}
		supportSessionIDReissue = false;
		return false;
	}

	/// <summary>Deletes the session-identifier cookie from the HTTP response.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> object that references server objects used to process HTTP requests (for example, the <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties).</param>
	public void RemoveSessionID(HttpContext context)
	{
		context.Response.Cookies.Remove(config.CookieName);
	}

	/// <summary>Saves a newly created session identifier to the HTTP response.</summary>
	/// <param name="context">The current <see cref="T:System.Web.HttpContext" /> object that references server objects used to process HTTP requests (for example, the <see cref="P:System.Web.HttpContext.Request" /> and <see cref="P:System.Web.HttpContext.Response" /> properties).</param>
	/// <param name="id">The session identifier. </param>
	/// <param name="redirected">When this method returns, contains a Boolean value that is<see langword=" true" /> if the response is redirected to the current URL with the session identifier added to the URL; otherwise, <see langword="false" />. </param>
	/// <param name="cookieAdded">When this method returns, contains a Boolean value that is<see langword=" true" /> if a cookie has been added to the HTTP response; otherwise, <see langword="false" />. </param>
	/// <exception cref="T:System.Web.HttpException">The response has already been sent.-or-The session ID passed to this method failed validation. </exception>
	public void SaveSessionID(HttpContext context, string id, out bool redirected, out bool cookieAdded)
	{
		if (!Validate(id))
		{
			throw new HttpException("Invalid session ID");
		}
		HttpRequest request = context.Request;
		if (!SessionStateModule.IsCookieLess(context, config))
		{
			HttpCookie httpCookie = new HttpCookie(config.CookieName, id);
			httpCookie.Path = request.ApplicationPath;
			context.Response.AppendCookie(httpCookie);
			cookieAdded = true;
			redirected = false;
		}
		else
		{
			request.SetHeader("AspFilterSessionId", id);
			cookieAdded = false;
			redirected = true;
			UriBuilder uriBuilder = new UriBuilder(request.Url);
			uriBuilder.Path = UrlUtils.InsertSessionId(id, request.FilePath);
			context.Response.Redirect(uriBuilder.Uri.PathAndQuery, endResponse: false);
		}
	}

	/// <summary>Gets a value indicating whether a session identifier is valid.</summary>
	/// <param name="id">The session identifier to validate. </param>
	/// <returns>
	///     <see langword="true" /> if the session identifier is valid; otherwise, <see langword="false" />.</returns>
	public virtual bool Validate(string id)
	{
		return true;
	}
}
