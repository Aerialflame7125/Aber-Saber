namespace System.Web.UI;

/// <summary>Contains information about an asynchronous task registered to a page. This class cannot be inherited.</summary>
public sealed class PageAsyncTask
{
	private BeginEventHandler beginHandler;

	private EndEventHandler endHandler;

	private EndEventHandler timeoutHandler;

	private bool executeInParallel;

	private object state;

	/// <summary>Gets the method to call when beginning an asynchronous task.</summary>
	/// <returns>A <see cref="T:System.Web.BeginEventHandler" /> delegate that represents the method to call when beginning the asynchronous task. </returns>
	public BeginEventHandler BeginHandler => beginHandler;

	/// <summary>Gets the method to call when the task completes successfully within the time-out period.</summary>
	/// <returns>An <see cref="T:System.Web.EndEventHandler" /> delegate that represents the method to call when the task completes successfully within the time-out period.</returns>
	public EndEventHandler EndHandler => endHandler;

	/// <summary>Gets the method to call when the task does not complete successfully within the time-out period.</summary>
	/// <returns>An <see cref="T:System.Web.EndEventHandler" /> delegate that represents the method to call when the task does not complete successfully within the time-out period.</returns>
	public EndEventHandler TimeoutHandler => timeoutHandler;

	/// <summary>Gets a value that indicates whether the task can be processed in parallel with other tasks.</summary>
	/// <returns>
	///     <see langword="true" /> if the task should be processed in parallel with other tasks; otherwise, <see langword="false" />.</returns>
	public bool ExecuteInParallel => executeInParallel;

	/// <summary>Gets an object that represents the state of the task.</summary>
	/// <returns>An <see cref="T:System.Object" /> that represents the state of the task.</returns>
	public object State => state;

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageAsyncTask" /> class using the default value for executing in parallel. </summary>
	/// <param name="beginHandler">The handler to call when beginning an asynchronous task.</param>
	/// <param name="endHandler">The handler to call when the task is completed successfully within the time-out period.</param>
	/// <param name="timeoutHandler">The handler to call when the task is not completed successfully within the time-out period.</param>
	/// <param name="state">The object that represents the state of the task.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginHandler" /> parameter or <paramref name="endHandler" /> parameter is not specified.</exception>
	public PageAsyncTask(BeginEventHandler beginHandler, EndEventHandler endHandler, EndEventHandler timeoutHandler, object state)
		: this(beginHandler, endHandler, timeoutHandler, state, executeInParallel: false)
	{
	}

	/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageAsyncTask" /> class using the specified value for executing in parallel. </summary>
	/// <param name="beginHandler">The handler to call when beginning an asynchronous task.</param>
	/// <param name="endHandler">The handler to call when the task is completed successfully within the time-out period.</param>
	/// <param name="timeoutHandler">The handler to call when the task is not completed successfully within the time-out period.</param>
	/// <param name="state">The object that represents the state of the task.</param>
	/// <param name="executeInParallel">The value that indicates whether the task can be processed in parallel with other tasks.</param>
	/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginHandler" /> parameter or <paramref name="endHandler" /> parameter is not specified.</exception>
	public PageAsyncTask(BeginEventHandler beginHandler, EndEventHandler endHandler, EndEventHandler timeoutHandler, object state, bool executeInParallel)
	{
		this.beginHandler = beginHandler;
		this.endHandler = endHandler;
		this.timeoutHandler = timeoutHandler;
		this.state = state;
		this.executeInParallel = executeInParallel;
	}
}
