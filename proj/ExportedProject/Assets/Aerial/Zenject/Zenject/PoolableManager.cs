using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;
using Zenject.Internal;

namespace Zenject;

public class PoolableManager
{
	private struct PoolableInfo
	{
		public IPoolable Poolable;

		public int Priority;

		public PoolableInfo(IPoolable poolable, int priority)
		{
			Poolable = poolable;
			Priority = priority;
		}
	}

	private readonly List<IPoolable> _poolables;

	private bool _isSpawned;

	public PoolableManager([InjectLocal] List<IPoolable> poolables, [Inject(Optional = true, Source = InjectSources.Local)] List<ValuePair<Type, int>> priorities)
	{
		PoolableManager poolableManager = this;
		_poolables = (from x in poolables
			select poolableManager.CreatePoolableInfo(x, priorities) into x
			orderby x.Priority
			select x.Poolable).ToList();
	}

	private PoolableInfo CreatePoolableInfo(IPoolable poolable, List<ValuePair<Type, int>> priorities)
	{
		int? num = priorities.Where((ValuePair<Type, int> x) => TypeExtensions.DerivesFromOrEqual(poolable.GetType(), x.First)).Select((Func<ValuePair<Type, int>, int?>)((ValuePair<Type, int> x) => x.Second)).SingleOrDefault();
		int priority = (num.HasValue ? num.Value : 0);
		return new PoolableInfo(poolable, priority);
	}

	public void TriggerOnSpawned()
	{
		Assert.That(!_isSpawned);
		_isSpawned = true;
		for (int i = 0; i < _poolables.Count; i++)
		{
			_poolables[i].OnSpawned();
		}
	}

	public void TriggerOnDespawned()
	{
		Assert.That(_isSpawned);
		_isSpawned = false;
		for (int num = _poolables.Count - 1; num >= 0; num--)
		{
			_poolables[num].OnDespawned();
		}
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PoolableManager((List<IPoolable>)P_0[0], (List<ValuePair<Type, int>>)P_0[1]);
	}

	[Zenject.Internal.Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PoolableManager), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[2]
		{
			new InjectableInfo(optional: false, null, "poolables", typeof(List<IPoolable>), null, InjectSources.Local),
			new InjectableInfo(optional: true, null, "priorities", typeof(List<ValuePair<Type, int>>), null, InjectSources.Local)
		}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
