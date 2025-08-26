namespace System.Web;

/// <summary>Defines the contract that HTTP asynchronous handler objects must implement.</summary>
public interface IHttpAsyncHandler : IHttpHandler
{
	/// <summary>Initiates an asynchronous call to the HTTP handler.</summary>
	/// <param name="context">An <see cref="T:System.Web.HttpContext" /> object that provides references to intrinsic server objects (for example, <see langword="Request" />, <see langword="Response" />, <see langword="Session" />, and <see langword="Server" />) used to service HTTP requests. </param>
	/// <param name="cb">The <see cref="T:System.AsyncCallback" /> to call when the asynchronous method call is complete. If <paramref name="cb" /> is <see langword="null" />, the delegate is not called. </param>
	/// <param name="extraData">Any extra data needed to process the request. </param>
	/// <returns>An <see cref="T:System.IAsyncResult" /> that contains information about the status of the process.</returns>
	IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData);

	/// <summary>Provides an asynchronous process End method when the process ends.</summary>
	/// <param name="result">An <see cref="T:System.IAsyncResult" /> that contains information about the status of the process. </param>
	void EndProcessRequest(IAsyncResult result);
}
