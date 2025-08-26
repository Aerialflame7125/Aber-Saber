using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;
using Zenject.Internal;

namespace Zenject;

public class DisposableManager : IDisposable
{
	private struct DisposableInfo
	{
		public IDisposable Disposable;

		public int Priority;

		public DisposableInfo(IDisposable disposable, int priority)
		{
			Disposable = disposable;
			Priority = priority;
		}
	}

	private class LateDisposableInfo
	{
		public ILateDisposable LateDisposable;

		public int Priority;

		public LateDisposableInfo(ILateDisposable lateDisposable, int priority)
		{
			LateDisposable = lateDisposable;
			Priority = priority;
		}

		private static object __zenCreate(object[] P_0)
		{
			return new LateDisposableInfo((ILateDisposable)P_0[0], (int)P_0[1]);
		}

		[Zenject.Internal.Preserve]
		private static InjectTypeInfo __zenCreateInjectTypeInfo()
		{
			return new InjectTypeInfo(typeof(LateDisposableInfo), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[2]
			{
				new InjectableInfo(optional: false, null, "lateDisposable", typeof(ILateDisposable), null, InjectSources.Any),
				new InjectableInfo(optional: false, null, "priority", typeof(int), null, InjectSources.Any)
			}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
		}
	}

	private readonly List<DisposableInfo> _disposables = new List<DisposableInfo>();

	private readonly List<LateDisposableInfo> _lateDisposables = new List<LateDisposableInfo>();

	private bool _disposed;

	private bool _lateDisposed;

	[Inject]
	public DisposableManager([Inject(Optional = true, Source = InjectSources.Local)] List<IDisposable> disposables, [Inject(Optional = true, Source = InjectSources.Local)] List<ValuePair<Type, int>> priorities, [Inject(Optional = true, Source = InjectSources.Local)] List<ILateDisposable> lateDisposables, [Inject(Id = "Late", Optional = true, Source = InjectSources.Local)] List<ValuePair<Type, int>> latePriorities)
	{
		foreach (IDisposable disposable in disposables)
		{
			int? num = priorities.Where((ValuePair<Type, int> x) => TypeExtensions.DerivesFromOrEqual(disposable.GetType(), x.First)).Select((Func<ValuePair<Type, int>, int?>)((ValuePair<Type, int> x) => x.Second)).SingleOrDefault();
			int priority = (num.HasValue ? num.Value : 0);
			_disposables.Add(new DisposableInfo(disposable, priority));
		}
		foreach (ILateDisposable lateDisposable in lateDisposables)
		{
			int? num2 = latePriorities.Where((ValuePair<Type, int> x) => TypeExtensions.DerivesFromOrEqual(lateDisposable.GetType(), x.First)).Select((Func<ValuePair<Type, int>, int?>)((ValuePair<Type, int> x) => x.Second)).SingleOrDefault();
			int priority2 = (num2.HasValue ? num2.Value : 0);
			_lateDisposables.Add(new LateDisposableInfo(lateDisposable, priority2));
		}
	}

	public void Add(IDisposable disposable)
	{
		Add(disposable, 0);
	}

	public void Add(IDisposable disposable, int priority)
	{
		_disposables.Add(new DisposableInfo(disposable, priority));
	}

	public void AddLate(ILateDisposable disposable)
	{
		AddLate(disposable, 0);
	}

	public void AddLate(ILateDisposable disposable, int priority)
	{
		_lateDisposables.Add(new LateDisposableInfo(disposable, priority));
	}

	public void Remove(IDisposable disposable)
	{
		MiscExtensions.RemoveWithConfirm(_disposables, _disposables.Where((DisposableInfo x) => object.ReferenceEquals(x.Disposable, disposable)).Single());
	}

	public void LateDispose()
	{
		Assert.That(!_lateDisposed, "Tried to late dispose DisposableManager twice!");
		_lateDisposed = true;
		List<LateDisposableInfo> list = _lateDisposables.OrderBy((LateDisposableInfo x) => x.Priority).Reverse().ToList();
		foreach (LateDisposableInfo item in list)
		{
			try
			{
				item.LateDisposable.LateDispose();
			}
			catch (Exception innerException)
			{
				throw Assert.CreateException(innerException, "Error occurred while late disposing ILateDisposable with type '{0}'", item.LateDisposable.GetType());
			}
		}
	}

	public void Dispose()
	{
		Assert.That(!_disposed, "Tried to dispose DisposableManager twice!");
		_disposed = true;
		List<DisposableInfo> list = _disposables.OrderBy((DisposableInfo x) => x.Priority).Reverse().ToList();
		foreach (DisposableInfo item in list)
		{
			try
			{
				item.Disposable.Dispose();
			}
			catch (Exception innerException)
			{
				throw Assert.CreateException(innerException, "Error occurred while disposing IDisposable with type '{0}'", item.Disposable.GetType());
			}
		}
	}

	private static object __zenCreate(object[] P_0)
	{
		return new DisposableManager((List<IDisposable>)P_0[0], (List<ValuePair<Type, int>>)P_0[1], (List<ILateDisposable>)P_0[2], (List<ValuePair<Type, int>>)P_0[3]);
	}

	[Zenject.Internal.Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(DisposableManager), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[4]
		{
			new InjectableInfo(optional: true, null, "disposables", typeof(List<IDisposable>), null, InjectSources.Local),
			new InjectableInfo(optional: true, null, "priorities", typeof(List<ValuePair<Type, int>>), null, InjectSources.Local),
			new InjectableInfo(optional: true, null, "lateDisposables", typeof(List<ILateDisposable>), null, InjectSources.Local),
			new InjectableInfo(optional: true, "Late", "latePriorities", typeof(List<ValuePair<Type, int>>), null, InjectSources.Local)
		}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
