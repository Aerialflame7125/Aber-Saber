using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMMainThreadDispatcher : PersistentSingleton<HMMainThreadDispatcher>
{
	private static HMMainThreadDispatcher _instance;

	private static readonly Queue<Action> _mainThreadExecutionQueue = new Queue<Action>();

	private void OnEnable()
	{
		UnityEngine.Object.DontDestroyOnLoad(this);
	}

	public void Update()
	{
		lock (_mainThreadExecutionQueue)
		{
			while (_mainThreadExecutionQueue.Count > 0)
			{
				_mainThreadExecutionQueue.Dequeue()();
			}
		}
	}

	public void Enqueue(IEnumerator action)
	{
		lock (_mainThreadExecutionQueue)
		{
			_mainThreadExecutionQueue.Enqueue(delegate
			{
				StartCoroutine(action);
			});
		}
	}

	public void Enqueue(Action action)
	{
		Enqueue(ActionCoroutine(action));
	}

	private IEnumerator ActionCoroutine(Action action)
	{
		action();
		yield return null;
	}
}
