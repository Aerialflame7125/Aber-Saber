using System.ComponentModel;
using System.Threading.Tasks;

namespace System.Web;

/// <summary>Provides methods that a derived task handler class can implement in order to process an asynchronous task.</summary>
public abstract class HttpTaskAsyncHandler : IHttpAsyncHandler, IHttpHandler
{
	/// <summary>When overridden in a derived class, gets a value that indicates whether the task handler class instance can be reused for another asynchronous task.</summary>
	/// <returns>
	///     <see langword="true" /> if the handler can be reused; otherwise, <see langword="false" />.  The default is <see langword="false" />.</returns>
	public virtual bool IsReusable => false;

	/// <summary>When overridden in a derived class, provides code that handles a synchronous task.</summary>
	/// <param name="context">The HTTP context.</param>
	/// <exception cref="T:System.NotSupportedException">The method is implemented but does not provide any default handling for synchronous tasks.</exception>
	[EditorBrowsable(EditorBrowsableState.Never)]
	public virtual void ProcessRequest(HttpContext context)
	{
		throw new NotSupportedException("This handler cannot be executed synchronously.");
	}

	/// <summary>When overridden in a derived class, provides code that handles an asynchronous task.</summary>
	/// <param name="context">The HTTP context.</param>
	/// <returns>The asynchronous task.</returns>
	public abstract Task ProcessRequestAsync(HttpContext context);

	/// <summary>Initiates asynchronous processing of a task in an HTTP task handler.</summary>
	/// <param name="context">The HTTP context.</param>
	/// <param name="cb">The callback method to invoke when the method returns.</param>
	/// <param name="extraData">Additional data for processing the task.</param>
	/// <returns>An object that contains status data about the asynchronous operation.</returns>
	IAsyncResult IHttpAsyncHandler.BeginProcessRequest(HttpContext context, AsyncCallback cb, object extraData)
	{
		return TaskAsyncResult.GetAsyncResult(ProcessRequestAsync(context), cb, extraData);
	}

	/// <summary>Ends asynchronous processing of a task in an HTTP task handler.</summary>
	/// <param name="result">The status of the asynchronous operation.</param>
	void IHttpAsyncHandler.EndProcessRequest(IAsyncResult result)
	{
		TaskAsyncResult.Wait(result);
	}

	/// <summary>
	///     Called from constructors in derived classes to initialize the <see cref="T:System.Web.HttpTaskAsyncHandler" /> class.</summary>
	protected HttpTaskAsyncHandler()
	{
	}
}
