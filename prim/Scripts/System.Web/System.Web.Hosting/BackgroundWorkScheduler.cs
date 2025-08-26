using System.Threading;
using System.Threading.Tasks;
using System.Web.Util;

namespace System.Web.Hosting;

internal sealed class BackgroundWorkScheduler : IRegisteredObject
{
	private readonly CancellationTokenHelper _cancellationTokenHelper = new CancellationTokenHelper(canceled: false);

	private int _numExecutingWorkItems;

	private readonly Action<BackgroundWorkScheduler> _unregisterCallback;

	private readonly Action<AppDomain, Exception> _logCallback;

	private readonly Action _workItemCompleteCallback;

	internal BackgroundWorkScheduler(Action<BackgroundWorkScheduler> unregisterCallback, Action<AppDomain, Exception> logCallback, Action workItemCompleteCallback = null)
	{
		_unregisterCallback = unregisterCallback;
		_logCallback = logCallback;
		_workItemCompleteCallback = workItemCompleteCallback;
	}

	private void FinalShutdown()
	{
		_unregisterCallback(this);
	}

	private async void RunWorkItemImpl(Func<CancellationToken, Task> workItem)
	{
		Task returnedTask = null;
		try
		{
			returnedTask = workItem(_cancellationTokenHelper.Token);
			await returnedTask.ConfigureAwait(continueOnCapturedContext: false);
		}
		catch (Exception ex)
		{
			if ((returnedTask == null || !returnedTask.IsCanceled) && (!(ex is OperationCanceledException ex2) || !(ex2.CancellationToken == _cancellationTokenHelper.Token)))
			{
				_logCallback(AppDomain.CurrentDomain, ex);
			}
		}
		finally
		{
			WorkItemComplete();
		}
	}

	public void ScheduleWorkItem(Func<CancellationToken, Task> workItem)
	{
		if (_cancellationTokenHelper.IsCancellationRequested)
		{
			return;
		}
		ThreadPool.UnsafeQueueUserWorkItem(delegate(object state)
		{
			lock (this)
			{
				if (_cancellationTokenHelper.IsCancellationRequested)
				{
					return;
				}
				_numExecutingWorkItems++;
			}
			RunWorkItemImpl((Func<CancellationToken, Task>)state);
		}, workItem);
	}

	public void Stop(bool immediate)
	{
		int numExecutingWorkItems;
		lock (this)
		{
			_cancellationTokenHelper.Cancel();
			numExecutingWorkItems = _numExecutingWorkItems;
		}
		if (numExecutingWorkItems == 0)
		{
			FinalShutdown();
		}
	}

	private void WorkItemComplete()
	{
		int num;
		bool isCancellationRequested;
		lock (this)
		{
			num = --_numExecutingWorkItems;
			isCancellationRequested = _cancellationTokenHelper.IsCancellationRequested;
		}
		if (_workItemCompleteCallback != null)
		{
			_workItemCompleteCallback();
		}
		if (num == 0 && isCancellationRequested)
		{
			FinalShutdown();
		}
	}
}
