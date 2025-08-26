using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject;

public class PrefabFactory<T> : IFactory<Object, T>, IFactory
{
	[Inject]
	private readonly DiContainer _container;

	public DiContainer Container => _container;

	public virtual T Create(Object prefab)
	{
		Assert.That(prefab != null, "Null prefab given to factory create method when instantiating object with type '{0}'.", typeof(T));
		return _container.InstantiatePrefabForComponent<T>(prefab);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PrefabFactory<T>();
	}

	private static void __zenFieldSetter0(object P_0, object P_1)
	{
		((PrefabFactory<T>)P_0)._container = (DiContainer)P_1;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PrefabFactory<T>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[1]
		{
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter0, new InjectableInfo(optional: false, null, "_container", typeof(DiContainer), null, InjectSources.Any))
		});
	}
}
public class PrefabFactory<P1, T> : IFactory<Object, P1, T>, IFactory
{
	[Inject]
	private readonly DiContainer _container;

	public DiContainer Container => _container;

	public virtual T Create(Object prefab, P1 param)
	{
		Assert.That(prefab != null, "Null prefab given to factory create method when instantiating object with type '{0}'.", typeof(T));
		return (T)_container.InstantiatePrefabForComponentExplicit(typeof(T), prefab, InjectUtil.CreateArgListExplicit(param));
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PrefabFactory<P1, T>();
	}

	private static void __zenFieldSetter0(object P_0, object P_1)
	{
		((PrefabFactory<P1, T>)P_0)._container = (DiContainer)P_1;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PrefabFactory<P1, T>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[1]
		{
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter0, new InjectableInfo(optional: false, null, "_container", typeof(DiContainer), null, InjectSources.Any))
		});
	}
}
public class PrefabFactory<P1, P2, T> : IFactory<Object, P1, P2, T>, IFactory
{
	[Inject]
	private readonly DiContainer _container;

	public DiContainer Container => _container;

	public virtual T Create(Object prefab, P1 param, P2 param2)
	{
		Assert.That(prefab != null, "Null prefab given to factory create method when instantiating object with type '{0}'.", typeof(T));
		return (T)_container.InstantiatePrefabForComponentExplicit(typeof(T), prefab, InjectUtil.CreateArgListExplicit(param, param2));
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PrefabFactory<P1, P2, T>();
	}

	private static void __zenFieldSetter0(object P_0, object P_1)
	{
		((PrefabFactory<P1, P2, T>)P_0)._container = (DiContainer)P_1;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PrefabFactory<P1, P2, T>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[1]
		{
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter0, new InjectableInfo(optional: false, null, "_container", typeof(DiContainer), null, InjectSources.Any))
		});
	}
}
public class PrefabFactory<P1, P2, P3, T> : IFactory<Object, P1, P2, P3, T>, IFactory
{
	[Inject]
	private readonly DiContainer _container;

	public DiContainer Container => _container;

	public virtual T Create(Object prefab, P1 param, P2 param2, P3 param3)
	{
		Assert.That(prefab != null, "Null prefab given to factory create method when instantiating object with type '{0}'.", typeof(T));
		return (T)_container.InstantiatePrefabForComponentExplicit(typeof(T), prefab, InjectUtil.CreateArgListExplicit(param, param2, param3));
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PrefabFactory<P1, P2, P3, T>();
	}

	private static void __zenFieldSetter0(object P_0, object P_1)
	{
		((PrefabFactory<P1, P2, P3, T>)P_0)._container = (DiContainer)P_1;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PrefabFactory<P1, P2, P3, T>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[1]
		{
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter0, new InjectableInfo(optional: false, null, "_container", typeof(DiContainer), null, InjectSources.Any))
		});
	}
}
public class PrefabFactory<P1, P2, P3, P4, T> : IFactory<Object, P1, P2, P3, P4, T>, IFactory
{
	[Inject]
	private readonly DiContainer _container;

	public DiContainer Container => _container;

	public virtual T Create(Object prefab, P1 param, P2 param2, P3 param3, P4 param4)
	{
		Assert.That(prefab != null, "Null prefab given to factory create method when instantiating object with type '{0}'.", typeof(T));
		return (T)_container.InstantiatePrefabForComponentExplicit(typeof(T), prefab, InjectUtil.CreateArgListExplicit(param, param2, param3, param4));
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PrefabFactory<P1, P2, P3, P4, T>();
	}

	private static void __zenFieldSetter0(object P_0, object P_1)
	{
		((PrefabFactory<P1, P2, P3, P4, T>)P_0)._container = (DiContainer)P_1;
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PrefabFactory<P1, P2, P3, P4, T>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[1]
		{
			new InjectTypeInfo.InjectMemberInfo(__zenFieldSetter0, new InjectableInfo(optional: false, null, "_container", typeof(DiContainer), null, InjectSources.Any))
		});
	}
}
