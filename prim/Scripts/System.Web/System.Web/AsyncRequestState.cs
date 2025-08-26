using System.Threading;

namespace System.Web;

internal class AsyncRequestState : IAsyncResult
{
	private AsyncCallback cb;

	private object cb_data;

	private bool completed;

	private ManualResetEvent complete_event;

	public object AsyncState => cb_data;

	public bool CompletedSynchronously => false;

	public bool IsCompleted => completed;

	public WaitHandle AsyncWaitHandle => complete_event;

	internal AsyncRequestState(ManualResetEvent complete_event, AsyncCallback cb, object cb_data)
	{
		this.cb = cb;
		this.cb_data = cb_data;
		this.complete_event = complete_event;
	}

	internal void Complete()
	{
		completed = true;
		try
		{
			if (cb != null)
			{
				cb(this);
			}
		}
		catch
		{
		}
		complete_event.Set();
	}
}
