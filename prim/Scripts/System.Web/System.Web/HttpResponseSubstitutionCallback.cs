namespace System.Web;

/// <summary>Represents the method that handles post-cache substitution.</summary>
/// <param name="context">The <see cref="T:System.Web.HttpContext" /> that contains the HTTP request information for the page with the control that requires post-cache substitution.</param>
/// <returns>The content inserted into the cached response before being sent to the client. </returns>
public delegate string HttpResponseSubstitutionCallback(HttpContext context);
