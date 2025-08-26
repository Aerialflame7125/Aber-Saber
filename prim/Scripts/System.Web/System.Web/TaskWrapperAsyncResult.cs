using System.Threading;
using System.Threading.Tasks;

namespace System.Web;

internal sealed class TaskWrapperAsyncResult : IAsyncResult
{
	private bool _forceCompletedSynchronously;

	public object AsyncState { get; private set; }

	public WaitHandle AsyncWaitHandle => ((IAsyncResult)Task).AsyncWaitHandle;

	public bool CompletedSynchronously
	{
		get
		{
			if (!_forceCompletedSynchronously)
			{
				return ((IAsyncResult)Task).CompletedSynchronously;
			}
			return true;
		}
	}

	public bool IsCompleted => ((IAsyncResult)Task).IsCompleted;

	internal Task Task { get; private set; }

	internal TaskWrapperAsyncResult(Task task, object asyncState)
	{
		Task = task;
		AsyncState = asyncState;
	}

	internal void ForceCompletedSynchronously()
	{
		_forceCompletedSynchronously = true;
	}
}
