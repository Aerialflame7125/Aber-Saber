using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject;

public abstract class MonoKernel : MonoBehaviour
{
	[InjectLocal]
	private TickableManager _tickableManager;

	[InjectLocal]
	private InitializableManager _initializableManager;

	[InjectLocal]
	private DisposableManager _disposablesManager;

	private bool _hasInitialized;

	private bool _isDestroyed;

	protected bool IsDestroyed => _isDestroyed;

	public virtual void Start()
	{
		Initialize();
	}

	public void Initialize()
	{
		if (!_hasInitialized)
		{
			_hasInitialized = true;
			_initializableManager.Initialize();
		}
	}

	public virtual void Update()
	{
		if (_tickableManager != null)
		{
			_tickableManager.Update();
		}
	}

	public virtual void FixedUpdate()
	{
		if (_tickableManager != null)
		{
			_tickableManager.FixedUpdate();
		}
	}

	public virtual void LateUpdate()
	{
		if (_tickableManager != null)
		{
			_tickableManager.LateUpdate();
		}
	}

	public virtual void OnDestroy()
	{
		if (_disposablesManager != null)
		{
			Assert.That(!_isDestroyed);
			_isDestroyed = true;
			_disposablesManager.Dispose();
			_disposablesManager.LateDispose();
		}
	}

	private static void __zenFieldSetter0(object P_0, object P_1)
	{
		((MonoKernel)P_0)._tickableManager = (TickableManager)P_1;
	}

	private static void __zenFieldSetter1(object P_0, object P_1)
	{
		((MonoKernel)P_0)._initializableManager = (InitializableManager)P_1;
	}

	private static void __zenFieldSetter2(object P_0, object P_1)
	{
		((MonoKernel)P_0)._disposablesManager = (DisposableManager)P_1;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MonoKernel), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[3]
		{
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter0, new InjectableInfo(optional: false, null, "_tickableManager", typeof(TickableManager), null, InjectSources.Local)),
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter1, new InjectableInfo(optional: false, null, "_initializableManager", typeof(InitializableManager), null, InjectSources.Local)),
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter2, new InjectableInfo(optional: false, null, "_disposablesManager", typeof(DisposableManager), null, InjectSources.Local))
		});
	}
}
