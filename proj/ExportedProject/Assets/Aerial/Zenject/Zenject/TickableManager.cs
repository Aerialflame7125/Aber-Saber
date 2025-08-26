using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;
using Zenject.Internal;

namespace Zenject;

public class TickableManager
{
	[Inject(Optional = true, Source = InjectSources.Local)]
	private readonly List<ITickable> _tickables;

	[Inject(Optional = true, Source = InjectSources.Local)]
	private readonly List<IFixedTickable> _fixedTickables;

	[Inject(Optional = true, Source = InjectSources.Local)]
	private readonly List<ILateTickable> _lateTickables;

	[Inject(Optional = true, Source = InjectSources.Local)]
	private readonly List<ValuePair<Type, int>> _priorities;

	[Inject(Optional = true, Id = "Fixed", Source = InjectSources.Local)]
	private readonly List<ValuePair<Type, int>> _fixedPriorities;

	[Inject(Optional = true, Id = "Late", Source = InjectSources.Local)]
	private readonly List<ValuePair<Type, int>> _latePriorities;

	private readonly TickablesTaskUpdater _updater = new TickablesTaskUpdater();

	private readonly FixedTickablesTaskUpdater _fixedUpdater = new FixedTickablesTaskUpdater();

	private readonly LateTickablesTaskUpdater _lateUpdater = new LateTickablesTaskUpdater();

	private bool _isPaused;

	public IEnumerable<ITickable> Tickables => _tickables;

	public bool IsPaused
	{
		get
		{
			return _isPaused;
		}
		set
		{
			_isPaused = value;
		}
	}

	[Inject]
	public TickableManager()
	{
	}

	[Inject]
	public void Initialize()
	{
		InitTickables();
		InitFixedTickables();
		InitLateTickables();
	}

	private void InitFixedTickables()
	{
		foreach (Type item in _fixedPriorities.Select((ValuePair<Type, int> x) => x.First))
		{
			Assert.That(TypeExtensions.DerivesFrom<IFixedTickable>(item), "Expected type '{0}' to drive from IFixedTickable while checking priorities in TickableHandler", item);
		}
		foreach (IFixedTickable tickable in _fixedTickables)
		{
			List<int> list = (from x in _fixedPriorities
				where TypeExtensions.DerivesFromOrEqual(tickable.GetType(), x.First)
				select x.Second).ToList();
			int priority = ((!LinqExtensions.IsEmpty(list)) ? list.Distinct().Single() : 0);
			_fixedUpdater.AddTask(tickable, priority);
		}
	}

	private void InitTickables()
	{
		foreach (Type item in _priorities.Select((ValuePair<Type, int> x) => x.First))
		{
			Assert.That(TypeExtensions.DerivesFrom<ITickable>(item), "Expected type '{0}' to drive from ITickable while checking priorities in TickableHandler", item);
		}
		foreach (ITickable tickable in _tickables)
		{
			List<int> list = (from x in _priorities
				where TypeExtensions.DerivesFromOrEqual(tickable.GetType(), x.First)
				select x.Second).ToList();
			int priority = ((!LinqExtensions.IsEmpty(list)) ? list.Distinct().Single() : 0);
			_updater.AddTask(tickable, priority);
		}
	}

	private void InitLateTickables()
	{
		foreach (Type item in _latePriorities.Select((ValuePair<Type, int> x) => x.First))
		{
			Assert.That(TypeExtensions.DerivesFrom<ILateTickable>(item), "Expected type '{0}' to drive from ILateTickable while checking priorities in TickableHandler", item);
		}
		foreach (ILateTickable tickable in _lateTickables)
		{
			List<int> list = (from x in _latePriorities
				where TypeExtensions.DerivesFromOrEqual(tickable.GetType(), x.First)
				select x.Second).ToList();
			int priority = ((!LinqExtensions.IsEmpty(list)) ? list.Distinct().Single() : 0);
			_lateUpdater.AddTask(tickable, priority);
		}
	}

	public void Add(ITickable tickable, int priority)
	{
		_updater.AddTask(tickable, priority);
	}

	public void Add(ITickable tickable)
	{
		Add(tickable, 0);
	}

	public void AddLate(ILateTickable tickable, int priority)
	{
		_lateUpdater.AddTask(tickable, priority);
	}

	public void AddLate(ILateTickable tickable)
	{
		AddLate(tickable, 0);
	}

	public void AddFixed(IFixedTickable tickable, int priority)
	{
		_fixedUpdater.AddTask(tickable, priority);
	}

	public void AddFixed(IFixedTickable tickable)
	{
		_fixedUpdater.AddTask(tickable, 0);
	}

	public void Remove(ITickable tickable)
	{
		_updater.RemoveTask(tickable);
	}

	public void RemoveLate(ILateTickable tickable)
	{
		_lateUpdater.RemoveTask(tickable);
	}

	public void RemoveFixed(IFixedTickable tickable)
	{
		_fixedUpdater.RemoveTask(tickable);
	}

	public void Update()
	{
		if (!IsPaused)
		{
			_updater.OnFrameStart();
			_updater.UpdateAll();
		}
	}

	public void FixedUpdate()
	{
		if (!IsPaused)
		{
			_fixedUpdater.OnFrameStart();
			_fixedUpdater.UpdateAll();
		}
	}

	public void LateUpdate()
	{
		if (!IsPaused)
		{
			_lateUpdater.OnFrameStart();
			_lateUpdater.UpdateAll();
		}
	}

	private static object __zenCreate(object[] P_0)
	{
		return new TickableManager();
	}

	private static void __zenFieldSetter0(object P_0, object P_1)
	{
		((TickableManager)P_0)._tickables = (List<ITickable>)P_1;
	}

	private static void __zenFieldSetter1(object P_0, object P_1)
	{
		((TickableManager)P_0)._fixedTickables = (List<IFixedTickable>)P_1;
	}

	private static void __zenFieldSetter2(object P_0, object P_1)
	{
		((TickableManager)P_0)._lateTickables = (List<ILateTickable>)P_1;
	}

	private static void __zenFieldSetter3(object P_0, object P_1)
	{
		((TickableManager)P_0)._priorities = (List<ValuePair<Type, int>>)P_1;
	}

	private static void __zenFieldSetter4(object P_0, object P_1)
	{
		((TickableManager)P_0)._fixedPriorities = (List<ValuePair<Type, int>>)P_1;
	}

	private static void __zenFieldSetter5(object P_0, object P_1)
	{
		((TickableManager)P_0)._latePriorities = (List<ValuePair<Type, int>>)P_1;
	}

	private static void __zenInjectMethod0(object P_0, object[] P_1)
	{
		((TickableManager)P_0).Initialize();
	}

	[Zenject.Internal.Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(TickableManager), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[1]
		{
			new InjectTypeInfo.InjectMethodInfo(__zenInjectMethod0, new InjectableInfo[0], "Initialize")
		}, new InjectTypeInfo.InjectMemberInfo[6]
		{
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter0, new InjectableInfo(optional: true, null, "_tickables", typeof(List<ITickable>), null, InjectSources.Local)),
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter1, new InjectableInfo(optional: true, null, "_fixedTickables", typeof(List<IFixedTickable>), null, InjectSources.Local)),
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter2, new InjectableInfo(optional: true, null, "_lateTickables", typeof(List<ILateTickable>), null, InjectSources.Local)),
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter3, new InjectableInfo(optional: true, null, "_priorities", typeof(List<ValuePair<Type, int>>), null, InjectSources.Local)),
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter4, new InjectableInfo(optional: true, "Fixed", "_fixedPriorities", typeof(List<ValuePair<Type, int>>), null, InjectSources.Local)),
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter5, new InjectableInfo(optional: true, "Late", "_latePriorities", typeof(List<ValuePair<Type, int>>), null, InjectSources.Local))
		});
	}
}
