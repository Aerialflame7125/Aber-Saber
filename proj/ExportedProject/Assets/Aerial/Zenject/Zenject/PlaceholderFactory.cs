using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Zenject.Internal;

namespace Zenject;

public class PlaceholderFactory<TValue> : PlaceholderFactoryBase<TValue>, IFactory<TValue>, IFactory
{
	protected sealed override IEnumerable<Type> ParamTypes
	{
		get
		{
			yield break;
		}
	}

	[NotNull]
	public virtual TValue Create()
	{
		return CreateInternal(new List<TypeValuePair>());
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PlaceholderFactory<TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PlaceholderFactory<TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class PlaceholderFactory<TParam1, TValue> : PlaceholderFactoryBase<TValue>, IFactory<TParam1, TValue>, IFactory
{
	protected sealed override IEnumerable<Type> ParamTypes
	{
		get
		{
			yield return typeof(TParam1);
		}
	}

	[NotNull]
	public virtual TValue Create(TParam1 param)
	{
		return CreateInternal(new List<TypeValuePair> { InjectUtil.CreateTypePair(param) });
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PlaceholderFactory<TParam1, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PlaceholderFactory<TParam1, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class PlaceholderFactory<TParam1, TParam2, TValue> : PlaceholderFactoryBase<TValue>, IFactory<TParam1, TParam2, TValue>, IFactory
{
	protected sealed override IEnumerable<Type> ParamTypes
	{
		get
		{
			yield return typeof(TParam1);
			yield return typeof(TParam2);
		}
	}

	[NotNull]
	public virtual TValue Create(TParam1 param1, TParam2 param2)
	{
		return CreateInternal(new List<TypeValuePair>
		{
			InjectUtil.CreateTypePair(param1),
			InjectUtil.CreateTypePair(param2)
		});
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PlaceholderFactory<TParam1, TParam2, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PlaceholderFactory<TParam1, TParam2, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class PlaceholderFactory<TParam1, TParam2, TParam3, TValue> : PlaceholderFactoryBase<TValue>, IFactory<TParam1, TParam2, TParam3, TValue>, IFactory
{
	protected sealed override IEnumerable<Type> ParamTypes
	{
		get
		{
			yield return typeof(TParam1);
			yield return typeof(TParam2);
			yield return typeof(TParam3);
		}
	}

	[NotNull]
	public virtual TValue Create(TParam1 param1, TParam2 param2, TParam3 param3)
	{
		return CreateInternal(new List<TypeValuePair>
		{
			InjectUtil.CreateTypePair(param1),
			InjectUtil.CreateTypePair(param2),
			InjectUtil.CreateTypePair(param3)
		});
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PlaceholderFactory<TParam1, TParam2, TParam3, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PlaceholderFactory<TParam1, TParam2, TParam3, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TValue> : PlaceholderFactoryBase<TValue>, IFactory<TParam1, TParam2, TParam3, TParam4, TValue>, IFactory
{
	protected sealed override IEnumerable<Type> ParamTypes
	{
		get
		{
			yield return typeof(TParam1);
			yield return typeof(TParam2);
			yield return typeof(TParam3);
			yield return typeof(TParam4);
		}
	}

	[NotNull]
	public virtual TValue Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
	{
		return CreateInternal(new List<TypeValuePair>
		{
			InjectUtil.CreateTypePair(param1),
			InjectUtil.CreateTypePair(param2),
			InjectUtil.CreateTypePair(param3),
			InjectUtil.CreateTypePair(param4)
		});
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TValue> : PlaceholderFactoryBase<TValue>, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>, IFactory
{
	protected sealed override IEnumerable<Type> ParamTypes
	{
		get
		{
			yield return typeof(TParam1);
			yield return typeof(TParam2);
			yield return typeof(TParam3);
			yield return typeof(TParam4);
			yield return typeof(TParam5);
		}
	}

	[NotNull]
	public virtual TValue Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
	{
		return CreateInternal(new List<TypeValuePair>
		{
			InjectUtil.CreateTypePair(param1),
			InjectUtil.CreateTypePair(param2),
			InjectUtil.CreateTypePair(param3),
			InjectUtil.CreateTypePair(param4),
			InjectUtil.CreateTypePair(param5)
		});
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue> : PlaceholderFactoryBase<TValue>, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>, IFactory
{
	protected sealed override IEnumerable<Type> ParamTypes
	{
		get
		{
			yield return typeof(TParam1);
			yield return typeof(TParam2);
			yield return typeof(TParam3);
			yield return typeof(TParam4);
			yield return typeof(TParam5);
			yield return typeof(TParam6);
		}
	}

	[NotNull]
	public virtual TValue Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
	{
		return CreateInternal(new List<TypeValuePair>
		{
			InjectUtil.CreateTypePair(param1),
			InjectUtil.CreateTypePair(param2),
			InjectUtil.CreateTypePair(param3),
			InjectUtil.CreateTypePair(param4),
			InjectUtil.CreateTypePair(param5),
			InjectUtil.CreateTypePair(param6)
		});
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
public class PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TValue> : PlaceholderFactoryBase<TValue>, IFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TValue>, IFactory
{
	protected sealed override IEnumerable<Type> ParamTypes
	{
		get
		{
			yield return typeof(TParam1);
			yield return typeof(TParam2);
			yield return typeof(TParam3);
			yield return typeof(TParam4);
			yield return typeof(TParam5);
			yield return typeof(TParam6);
			yield return typeof(TParam7);
			yield return typeof(TParam8);
			yield return typeof(TParam9);
			yield return typeof(TParam10);
		}
	}

	public virtual TValue Create(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6, TParam7 param7, TParam8 param8, TParam9 param9, TParam10 param10)
	{
		return CreateInternal(new List<TypeValuePair>
		{
			InjectUtil.CreateTypePair(param1),
			InjectUtil.CreateTypePair(param2),
			InjectUtil.CreateTypePair(param3),
			InjectUtil.CreateTypePair(param4),
			InjectUtil.CreateTypePair(param5),
			InjectUtil.CreateTypePair(param6),
			InjectUtil.CreateTypePair(param7),
			InjectUtil.CreateTypePair(param8),
			InjectUtil.CreateTypePair(param9),
			InjectUtil.CreateTypePair(param10)
		});
	}

	private static object __zenCreate(object[] P_0)
	{
		return new PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TValue>();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PlaceholderFactory<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TValue>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
