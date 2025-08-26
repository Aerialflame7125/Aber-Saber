using System;
using System.Diagnostics;
using Zenject.Internal;

namespace Zenject;

[DebuggerStepThrough]
public class Kernel : IInitializable, IDisposable, ITickable, ILateTickable, IFixedTickable, ILateDisposable
{
	[InjectLocal]
	private TickableManager _tickableManager;

	[InjectLocal]
	private InitializableManager _initializableManager;

	[InjectLocal]
	private DisposableManager _disposablesManager;

	public virtual void Initialize()
	{
		_initializableManager.Initialize();
	}

	public virtual void Dispose()
	{
		_disposablesManager.Dispose();
	}

	public virtual void LateDispose()
	{
		_disposablesManager.LateDispose();
	}

	public virtual void Tick()
	{
		_tickableManager.Update();
	}

	public virtual void LateTick()
	{
		_tickableManager.LateUpdate();
	}

	public virtual void FixedTick()
	{
		_tickableManager.FixedUpdate();
	}

	private static object __zenCreate(object[] P_0)
	{
		return new Kernel();
	}

	private static void __zenFieldSetter0(object P_0, object P_1)
	{
		((Kernel)P_0)._tickableManager = (TickableManager)P_1;
	}

	private static void __zenFieldSetter1(object P_0, object P_1)
	{
		((Kernel)P_0)._initializableManager = (InitializableManager)P_1;
	}

	private static void __zenFieldSetter2(object P_0, object P_1)
	{
		((Kernel)P_0)._disposablesManager = (DisposableManager)P_1;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(Kernel), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[3]
		{
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter0, new InjectableInfo(optional: false, null, "_tickableManager", typeof(TickableManager), null, InjectSources.Local)),
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter1, new InjectableInfo(optional: false, null, "_initializableManager", typeof(InitializableManager), null, InjectSources.Local)),
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter2, new InjectableInfo(optional: false, null, "_disposablesManager", typeof(DisposableManager), null, InjectSources.Local))
		});
	}
}
