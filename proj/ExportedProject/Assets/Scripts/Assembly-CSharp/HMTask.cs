using System;
using System.Threading;
using UnityEngine;

public class HMTask
{
	private Thread _thread;

	private Action _job;

	private Action _finnish;

	private bool _canceled;

	private bool _started;

	public HMTask(Action job, Action finnish = null)
	{
		_job = job;
		if (PersistentSingleton<HMMainThreadDispatcher>.instance != null)
		{
			_finnish = finnish;
		}
		else
		{
			Debug.LogError("HMMainThreadDispatcher is not available Something went wrong.");
		}
	}

	public void Run()
	{
		if (!_started)
		{
			_started = true;
			_thread = new Thread(RunJob);
			_thread.Start();
		}
	}

	private void RunJob()
	{
		_job();
		PersistentSingleton<HMMainThreadDispatcher>.instance.Enqueue(delegate
		{
			if (!_canceled)
			{
				_finnish();
			}
		});
	}

	public void Cancel()
	{
		_canceled = true;
	}
}
