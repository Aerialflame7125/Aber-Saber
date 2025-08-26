using System.Threading;

namespace System.Web.Util;

internal sealed class DisposableAction : IDisposable
{
	public static readonly DisposableAction Empty = new DisposableAction(null);

	private Action _disposeAction;

	public DisposableAction(Action disposeAction)
	{
		_disposeAction = disposeAction;
	}

	public void Dispose()
	{
		Interlocked.Exchange(ref _disposeAction, null)?.Invoke();
	}
}
