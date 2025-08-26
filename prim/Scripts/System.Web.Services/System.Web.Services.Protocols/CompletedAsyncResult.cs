using System.Threading;

namespace System.Web.Services.Protocols;

internal class CompletedAsyncResult : IAsyncResult
{
	private object asyncState;

	private bool completedSynchronously;

	public object AsyncState => asyncState;

	public bool CompletedSynchronously => completedSynchronously;

	public bool IsCompleted => true;

	public WaitHandle AsyncWaitHandle => null;

	internal CompletedAsyncResult(object asyncState, bool completedSynchronously)
	{
		this.asyncState = asyncState;
		this.completedSynchronously = completedSynchronously;
	}
}
