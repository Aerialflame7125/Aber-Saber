namespace System.Web;

/// <summary>Converts task-returning asynchronous methods into methods that use the asynchronous programming model used in previous versions of ASP.NET and that is based on begin and end events.</summary>
public sealed class EventHandlerTaskAsyncHelper
{
	private readonly TaskEventHandler taskEventHandler;

	private readonly BeginEventHandler beginEventHandler;

	private static readonly EndEventHandler endEventHandler = TaskAsyncResult.Wait;

	/// <summary>Represents the <see cref="T:System.Web.BeginEventHandler" /> method for an asynchronous task.</summary>
	/// <returns>The method that handles the begin event for the asynchronous task.</returns>
	public BeginEventHandler BeginEventHandler => beginEventHandler;

	/// <summary>Represents the <see cref="T:System.Web.EndEventHandler" /> method for an asynchronous task.</summary>
	/// <returns>The method that handles the end event for the asynchronous task.</returns>
	public EndEventHandler EndEventHandler => endEventHandler;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.EventHandlerTaskAsyncHelper" /> class.</summary>
	/// <param name="handler">The asynchronous task.</param>
	public EventHandlerTaskAsyncHelper(TaskEventHandler handler)
	{
		if (handler == null)
		{
			throw new ArgumentNullException("handler");
		}
		taskEventHandler = handler;
		beginEventHandler = GetAsyncResult;
	}

	private IAsyncResult GetAsyncResult(object sender, EventArgs e, AsyncCallback callback, object state)
	{
		return TaskAsyncResult.GetAsyncResult(taskEventHandler(sender, e), callback, state);
	}
}
