using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;
using Zenject.Internal;

namespace Zenject;

public class InitializableManager
{
	private class InitializableInfo
	{
		public IInitializable Initializable;

		public int Priority;

		public InitializableInfo(IInitializable initializable, int priority)
		{
			Initializable = initializable;
			Priority = priority;
		}

		private static object __zenCreate(object[] P_0)
		{
			return new InitializableInfo((IInitializable)P_0[0], (int)P_0[1]);
		}

		[Zenject.Internal.Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(InitializableInfo), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[2]
			{
				new InjectableInfo(optional: false, null, "initializable", typeof(IInitializable), null, InjectSources.Any),
				new InjectableInfo(optional: false, null, "priority", typeof(int), null, InjectSources.Any)
			}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
		}
	}

	private List<InitializableInfo> _initializables;

	private bool _hasInitialized;

	[Inject]
	public InitializableManager([Inject(Optional = true, Source = InjectSources.Local)] List<IInitializable> initializables, [Inject(Optional = true, Source = InjectSources.Local)] List<ValuePair<Type, int>> priorities)
	{
		_initializables = new List<InitializableInfo>();
		for (int i = 0; i < initializables.Count; i++)
		{
			IInitializable initializable = initializables[i];
			List<int> list = (from x in priorities
				where TypeExtensions.DerivesFromOrEqual(initializable.GetType(), x.First)
				select x.Second).ToList();
			int priority = ((!LinqExtensions.IsEmpty(list)) ? list.Distinct().Single() : 0);
			_initializables.Add(new InitializableInfo(initializable, priority));
		}
	}

	public void Add(IInitializable initializable)
	{
		Add(initializable, 0);
	}

	public void Add(IInitializable initializable, int priority)
	{
		Assert.That(!_hasInitialized);
		_initializables.Add(new InitializableInfo(initializable, priority));
	}

	public void Initialize()
	{
		Assert.That(!_hasInitialized);
		_hasInitialized = true;
		_initializables = _initializables.OrderBy((InitializableInfo x) => x.Priority).ToList();
		foreach (InitializableInfo initializable in _initializables)
		{
			try
			{
				initializable.Initializable.Initialize();
			}
			catch (Exception innerException)
			{
				throw Assert.CreateException(innerException, "Error occurred while initializing IInitializable with type '{0}'", initializable.Initializable.GetType());
			}
		}
	}

	private static object __zenCreate(object[] P_0)
	{
		return new InitializableManager((List<IInitializable>)P_0[0], (List<ValuePair<Type, int>>)P_0[1]);
	}

	[Zenject.Internal.Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(InitializableManager), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[2]
		{
			new InjectableInfo(optional: true, null, "initializables", typeof(List<IInitializable>), null, InjectSources.Local),
			new InjectableInfo(optional: true, null, "priorities", typeof(List<ValuePair<Type, int>>), null, InjectSources.Local)
		}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
