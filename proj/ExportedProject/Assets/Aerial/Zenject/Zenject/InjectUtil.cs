using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using ModestTree;

namespace Zenject;

[DebuggerStepThrough]
public static class InjectUtil
{
	public static List<TypeValuePair> CreateArgList(IEnumerable<object> args)
	{
		Assert.That(!LinqExtensions.ContainsItem(args, null), "Cannot include null values when creating a zenject argument list because zenject has no way of deducing the type from a null value.  If you want to allow null, use the Explicit form.");
		return args.Select((object x) => new TypeValuePair(x.GetType(), x)).ToList();
	}

	public static TypeValuePair CreateTypePair<T>(T param)
	{
		return new TypeValuePair((param != null) ? param.GetType() : typeof(T), param);
	}

	public static List<TypeValuePair> CreateArgListExplicit<T>(T param)
	{
		List<TypeValuePair> list = new List<TypeValuePair>();
		list.Add(CreateTypePair(param));
		return list;
	}

	public static List<TypeValuePair> CreateArgListExplicit<TParam1, TParam2>(TParam1 param1, TParam2 param2)
	{
		List<TypeValuePair> list = new List<TypeValuePair>();
		list.Add(CreateTypePair(param1));
		list.Add(CreateTypePair(param2));
		return list;
	}

	public static List<TypeValuePair> CreateArgListExplicit<TParam1, TParam2, TParam3>(TParam1 param1, TParam2 param2, TParam3 param3)
	{
		List<TypeValuePair> list = new List<TypeValuePair>();
		list.Add(CreateTypePair(param1));
		list.Add(CreateTypePair(param2));
		list.Add(CreateTypePair(param3));
		return list;
	}

	public static List<TypeValuePair> CreateArgListExplicit<TParam1, TParam2, TParam3, TParam4>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
	{
		List<TypeValuePair> list = new List<TypeValuePair>();
		list.Add(CreateTypePair(param1));
		list.Add(CreateTypePair(param2));
		list.Add(CreateTypePair(param3));
		list.Add(CreateTypePair(param4));
		return list;
	}

	public static List<TypeValuePair> CreateArgListExplicit<TParam1, TParam2, TParam3, TParam4, TParam5>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5)
	{
		List<TypeValuePair> list = new List<TypeValuePair>();
		list.Add(CreateTypePair(param1));
		list.Add(CreateTypePair(param2));
		list.Add(CreateTypePair(param3));
		list.Add(CreateTypePair(param4));
		list.Add(CreateTypePair(param5));
		return list;
	}

	public static List<TypeValuePair> CreateArgListExplicit<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4, TParam5 param5, TParam6 param6)
	{
		List<TypeValuePair> list = new List<TypeValuePair>();
		list.Add(CreateTypePair(param1));
		list.Add(CreateTypePair(param2));
		list.Add(CreateTypePair(param3));
		list.Add(CreateTypePair(param4));
		list.Add(CreateTypePair(param5));
		list.Add(CreateTypePair(param6));
		return list;
	}

	public static bool PopValueWithType(List<TypeValuePair> extraArgMap, Type injectedFieldType, out object value)
	{
		for (int i = 0; i < extraArgMap.Count; i++)
		{
			TypeValuePair typeValuePair = extraArgMap[i];
			if (TypeExtensions.DerivesFromOrEqual(typeValuePair.Type, injectedFieldType))
			{
				value = typeValuePair.Value;
				extraArgMap.RemoveAt(i);
				return true;
			}
		}
		value = TypeExtensions.GetDefaultValue(injectedFieldType);
		return false;
	}
}
