using System.Threading;

namespace System.Web.Util;

internal sealed class CancellationTokenHelper : IDisposable
{
	private const int STATE_CREATED = 0;

	private const int STATE_CANCELING = 1;

	private const int STATE_CANCELED = 2;

	private const int STATE_DISPOSING = 3;

	private const int STATE_DISPOSED = 4;

	internal static readonly CancellationTokenHelper StaticDisposed = GetStaticDisposedHelper();

	private readonly CancellationTokenSource _cts = new CancellationTokenSource();

	private int _state;

	internal bool IsCancellationRequested => _cts.IsCancellationRequested;

	internal CancellationToken Token => _cts.Token;

	public CancellationTokenHelper(bool canceled)
	{
		if (canceled)
		{
			_cts.Cancel();
		}
		_state = (canceled ? 2 : 0);
	}

	public void Cancel()
	{
		if (Interlocked.CompareExchange(ref _state, 1, 0) != 0)
		{
			return;
		}
		ThreadPool.UnsafeQueueUserWorkItem(delegate
		{
			try
			{
				_cts.Cancel();
			}
			catch
			{
			}
			finally
			{
				if (Interlocked.CompareExchange(ref _state, 2, 1) == 3)
				{
					_cts.Dispose();
					Interlocked.Exchange(ref _state, 4);
				}
			}
		}, null);
	}

	public void Dispose()
	{
		switch (Interlocked.Exchange(ref _state, 3))
		{
		case 0:
		case 2:
			_cts.Dispose();
			Interlocked.Exchange(ref _state, 4);
			break;
		case 4:
			Interlocked.Exchange(ref _state, 4);
			break;
		case 1:
		case 3:
			break;
		}
	}

	private static CancellationTokenHelper GetStaticDisposedHelper()
	{
		CancellationTokenHelper cancellationTokenHelper = new CancellationTokenHelper(canceled: false);
		cancellationTokenHelper.Dispose();
		return cancellationTokenHelper;
	}
}
