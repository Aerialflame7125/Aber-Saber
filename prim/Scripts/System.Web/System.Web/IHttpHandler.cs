namespace System.Web;

/// <summary>Defines the contract that ASP.NET implements to synchronously process HTTP Web requests using custom HTTP handlers.</summary>
public interface IHttpHandler
{
	/// <summary>Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler" /> instance.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.IHttpHandler" /> instance is reusable; otherwise, <see langword="false" />.</returns>
	bool IsReusable { get; }

	/// <summary>Enables processing of HTTP Web requests by a custom <see langword="HttpHandler" /> that implements the <see cref="T:System.Web.IHttpHandler" /> interface.</summary>
	/// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to the intrinsic server objects (for example, <see langword="Request" />, <see langword="Response" />, <see langword="Session" />, and <see langword="Server" />) used to service HTTP requests. </param>
	void ProcessRequest(HttpContext context);
}
