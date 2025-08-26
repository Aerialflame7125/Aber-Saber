using System.Collections.Specialized;
using System.Threading;

namespace System.Web;

/// <summary>Represents the properties and methods of a default HTTP handler.</summary>
public class DefaultHttpHandler : IHttpAsyncHandler, IHttpHandler
{
	private sealed class DefaultHandlerAsyncResult : IAsyncResult
	{
		public object AsyncState { get; private set; }

		public WaitHandle AsyncWaitHandle => null;

		public bool CompletedSynchronously => true;

		public bool IsCompleted => true;

		public DefaultHandlerAsyncResult(AsyncCallback callback, object state)
		{
			AsyncState = state;
			callback?.Invoke(this);
		}
	}

	private NameValueCollection executeUrlHeaders;

	/// <summary>Gets the context that is associated with the current <see cref="T:System.Web.DefaultHttpHandler" /> object.</summary>
	/// <returns>An <see cref="T:System.Web.HttpContext" /> object that contains the current context.</returns>
	protected HttpContext Context { get; private set; }

	/// <summary>Gets a Boolean value indicating that another request can use the current instance of the <see cref="T:System.Web.DefaultHttpHandler" /> class.</summary>
	/// <returns>
	///     <see langword="true" /> if the <see cref="T:System.Web.DefaultHttpHandler" /> is reusable; otherwise, <see langword="false" />.</returns>
	public virtual bool IsReusable => false;

	/// <summary>Gets a collection of request headers and request values to transfer along with the request.</summary>
	/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> containing request headers and values.</returns>
	protected NameValueCollection ExecuteUrlHeaders
	{
		get
		{
			HttpRequest httpRequest = Context?.Request;
			if (httpRequest != null && executeUrlHeaders != null)
			{
				executeUrlHeaders = new NameValueCollection(httpRequest.Headers);
			}
			return executeUrlHeaders;
		}
	}

	/// <summary>Initiates an asynchronous call to the HTTP handler.</summary>
	/// <param name="context">An object that provides references to intrinsic server objects that are used to service HTTP requests.</param>
	/// <param name="callback">The method to call when the asynchronous method call is complete. If <paramref name="callback" /> is <see langword="null" />, the delegate is not called.</param>
	/// <param name="state">Any state data that is needed to process the request.</param>
	/// <returns>An <see cref="T:System.IAsyncResult" /> that contains information about the status of the process.</returns>
	/// <exception cref="T:System.Web.HttpException">The preconditions for processing a request fail and either the requested file has the suffix .asp or the request was sent through POST.</exception>
	public virtual IAsyncResult BeginProcessRequest(HttpContext context, AsyncCallback callback, object state)
	{
		Context = context;
		HttpRequest httpRequest = context?.Request;
		string text = httpRequest?.FilePath;
		if (!string.IsNullOrEmpty(text) && string.Compare(".asp", VirtualPathUtility.GetExtension(text), StringComparison.OrdinalIgnoreCase) == 0)
		{
			throw new HttpException($"Access to file '{text}' is forbidden.");
		}
		if (httpRequest != null && string.Compare("POST", httpRequest.HttpMethod, StringComparison.OrdinalIgnoreCase) == 0)
		{
			throw new HttpException($"Method '{httpRequest.HttpMethod}' is not allowed when accessing file '{text}'");
		}
		new StaticFileHandler().ProcessRequest(context);
		return new DefaultHandlerAsyncResult(callback, state);
	}

	/// <summary>Provides an end method for an asynchronous process.</summary>
	/// <param name="result">An object that contains information about the status of the process.</param>
	public virtual void EndProcessRequest(IAsyncResult result)
	{
	}

	/// <summary>Enables a <see cref="T:System.Web.DefaultHttpHandler" /> object to process of HTTP Web requests.</summary>
	/// <param name="context">An object that provides references to intrinsic server objects used to service HTTP requests.</param>
	/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Web.DefaultHttpHandler.ProcessRequest(System.Web.HttpContext)" /> is called synchronously.</exception>
	public virtual void ProcessRequest(HttpContext context)
	{
		throw new InvalidOperationException("The ProcessRequest cannot be called synchronously.");
	}

	/// <summary>Called when preconditions prevent the <see cref="T:System.Web.DefaultHttpHandler" /> object from processing a request.</summary>
	public virtual void OnExecuteUrlPreconditionFailure()
	{
	}

	/// <summary>Overrides the target URL for the current request.</summary>
	/// <returns>The overridden URL to use in the request; or <see langword="null" /> if an overridden URL is not provided.</returns>
	public virtual string OverrideExecuteUrlPath()
	{
		return null;
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.DefaultHttpHandler" /> class.</summary>
	public DefaultHttpHandler()
	{
	}
}
