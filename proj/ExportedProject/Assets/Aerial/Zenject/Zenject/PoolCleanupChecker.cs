using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using Zenject.Internal;

namespace Zenject;

public class PoolCleanupChecker : ILateDisposable
{
	private readonly List<IMemoryPool> _poolFactories;

	private readonly List<Type> _ignoredPools;

	public PoolCleanupChecker([Inject(Optional = true, Source = InjectSources.Local)] List<IMemoryPool> poolFactories, [Inject(Source = InjectSources.Local)] List<Type> ignoredPools)
	{
		_poolFactories = poolFactories;
		_ignoredPools = ignoredPools;
		Assert.That(ignoredPools.All(TypeExtensions.DerivesFrom<IMemoryPool>));
	}

	public void LateDispose()
	{
		foreach (IMemoryPool poolFactory in _poolFactories)
		{
			if (!_ignoredPools.Contains(poolFactory.GetType()))
			{
				Assert.IsEqual(poolFactory.NumActive, 0, MiscExtensions.Fmt("Found active objects in pool '{0}' during dispose.  Did you forget to despawn an object of type '{1}'?", poolFactory.GetType(), poolFactory.ItemType));
			}
		}
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PoolCleanupChecker((List<IMemoryPool>)P_0[0], (List<Type>)P_0[1]);
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PoolCleanupChecker), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[2]
		{
			new InjectableInfo(optional: true, null, "poolFactories", typeof(List<IMemoryPool>), null, InjectSources.Local),
			new InjectableInfo(optional: false, null, "ignoredPools", typeof(List<Type>), null, InjectSources.Local)
		}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
