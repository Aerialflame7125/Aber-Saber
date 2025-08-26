using System.Threading;
using System.Threading.Tasks;

namespace System.Web;

internal sealed class TaskAsyncResult : IAsyncResult
{
	private static readonly Action<Task, object> invokeCallback = InvokeCallback;

	private readonly Task task;

	private readonly AsyncCallback callback;

	public object AsyncState { get; private set; }

	public WaitHandle AsyncWaitHandle => ((IAsyncResult)task).AsyncWaitHandle;

	public bool CompletedSynchronously { get; private set; }

	public bool IsCompleted => task.IsCompleted;

	private TaskAsyncResult(Task task, AsyncCallback callback, object state)
	{
		this.task = task;
		this.callback = callback;
		AsyncState = state;
		CompletedSynchronously = task.IsCompleted;
	}

	public static IAsyncResult GetAsyncResult(Task task, AsyncCallback callback, object state)
	{
		if (task == null)
		{
			return null;
		}
		TaskAsyncResult taskAsyncResult = new TaskAsyncResult(task, callback, state);
		if (callback != null)
		{
			if (taskAsyncResult.CompletedSynchronously)
			{
				callback(taskAsyncResult);
			}
			else
			{
				task.ContinueWith(invokeCallback, taskAsyncResult);
			}
		}
		return taskAsyncResult;
	}

	public static void Wait(IAsyncResult result)
	{
		if (result == null)
		{
			throw new ArgumentNullException("result");
		}
		((result as TaskAsyncResult) ?? throw new ArgumentException("The provided IAsyncResult is invalid.", "result")).task.GetAwaiter().GetResult();
	}

	private static void InvokeCallback(Task task, object state)
	{
		TaskAsyncResult taskAsyncResult = (TaskAsyncResult)state;
		taskAsyncResult.callback(taskAsyncResult);
	}
}
