using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject;

public abstract class RunnableContext : Context
{
	[SerializeField]
	[Tooltip("When false, wait until run method is explicitly called. Otherwise run on initialize")]
	private bool _autoRun = true;

	private static bool _staticAutoRun = true;

	public bool Initialized { get; private set; }

	protected void Initialize()
	{
		if (_staticAutoRun && _autoRun)
		{
			Run();
		}
		else
		{
			_staticAutoRun = true;
		}
	}

	public void Run()
	{
		Assert.That(!Initialized, "The context already has been initialized!");
		RunInternal();
		Initialized = true;
	}

	protected abstract void RunInternal();

	public static T CreateComponent<T>(GameObject gameObject) where T : RunnableContext
	{
		_staticAutoRun = false;
		T result = gameObject.AddComponent<T>();
		Assert.That(_staticAutoRun);
		return result;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(RunnableContext), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
