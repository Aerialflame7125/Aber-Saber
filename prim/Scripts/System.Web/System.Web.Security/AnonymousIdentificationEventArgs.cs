namespace System.Web.Security;

/// <summary>Provides data for the AnonymousIdentification_Creating event. This class cannot be inherited.</summary>
public sealed class AnonymousIdentificationEventArgs : EventArgs
{
	private HttpContext context;

	private string anonymousId;

	/// <summary>Gets the <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</summary>
	/// <returns>The <see cref="T:System.Web.HttpContext" /> object for the current HTTP request.</returns>
	public HttpContext Context => context;

	/// <summary>Gets or sets the anonymous identifier for the user.</summary>
	/// <returns>The anonymous identifier for the user.</returns>
	public string AnonymousID
	{
		get
		{
			return anonymousId;
		}
		set
		{
			anonymousId = value;
		}
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.Security.AnonymousIdentificationEventArgs" /> class.</summary>
	/// <param name="context">The context for the event.</param>
	public AnonymousIdentificationEventArgs(HttpContext context)
	{
		this.context = context;
	}
}
