using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ModestTree;
using UnityEngine;

namespace Zenject.Internal;

public static class ReflectionTypeAnalyzer
{
	private static readonly HashSet<Type> _injectAttributeTypes;

	static ReflectionTypeAnalyzer()
	{
		_injectAttributeTypes = new HashSet<Type>();
		_injectAttributeTypes.Add(typeof(InjectAttributeBase));
	}

	public static void AddCustomInjectAttribute<T>() where T : Attribute
	{
		AddCustomInjectAttribute(typeof(T));
	}

	public static void AddCustomInjectAttribute(Type type)
	{
		Assert.That(TypeExtensions.DerivesFrom<Attribute>(type));
		_injectAttributeTypes.Add(type);
	}

	public static ReflectionTypeInfo GetReflectionInfo(Type type)
	{
		Assert.That(!TypeExtensions.IsEnum(type), "Tried to analyze enum type '{0}'.  This is not supported", type);
		Assert.That(!type.IsArray, "Tried to analyze array type '{0}'.  This is not supported", type);
		Type type2 = TypeExtensions.BaseType(type);
		if (type2 == typeof(object))
		{
			type2 = null;
		}
		return new ReflectionTypeInfo(type, type2, GetConstructorInfo(type), GetMethodInfos(type), GetFieldInfos(type), GetPropertyInfos(type));
	}

	private static List<ReflectionTypeInfo.InjectPropertyInfo> GetPropertyInfos(Type type)
	{
		return (from x in TypeExtensions.DeclaredInstanceProperties(type)
			where _injectAttributeTypes.Any((Type a) => TypeExtensions.HasAttribute(x, a))
			select new ReflectionTypeInfo.InjectPropertyInfo(x, GetInjectableInfoForMember(type, x))).ToList();
	}

	private static List<ReflectionTypeInfo.InjectFieldInfo> GetFieldInfos(Type type)
	{
		return (from x in TypeExtensions.DeclaredInstanceFields(type)
			where _injectAttributeTypes.Any((Type a) => TypeExtensions.HasAttribute(x, a))
			select new ReflectionTypeInfo.InjectFieldInfo(x, GetInjectableInfoForMember(type, x))).ToList();
	}

	private static List<ReflectionTypeInfo.InjectMethodInfo> GetMethodInfos(Type type)
	{
		List<ReflectionTypeInfo.InjectMethodInfo> list = new List<ReflectionTypeInfo.InjectMethodInfo>();
		List<MethodInfo> list2 = (from x in TypeExtensions.DeclaredInstanceMethods(type)
			where _injectAttributeTypes.Any((Type a) => x.GetCustomAttributes(a, inherit: false).Any())
			select x).ToList();
		for (int num = 0; num < list2.Count; num++)
		{
			MethodInfo methodInfo = list2[num];
			InjectAttributeBase injectAttributeBase = TypeExtensions.AllAttributes<InjectAttributeBase>(methodInfo).SingleOrDefault();
			if (injectAttributeBase != null)
			{
				Assert.That(!injectAttributeBase.Optional && injectAttributeBase.Id == null && injectAttributeBase.Source == InjectSources.Any, "Parameters of InjectAttribute do not apply to constructors and methodInfos");
			}
			List<ReflectionTypeInfo.InjectParameterInfo> parameters = (from x in methodInfo.GetParameters()
				select CreateInjectableInfoForParam(type, x)).ToList();
			list.Add(new ReflectionTypeInfo.InjectMethodInfo(methodInfo, parameters));
		}
		return list;
	}

	private static ReflectionTypeInfo.InjectConstructorInfo GetConstructorInfo(Type type)
	{
		List<ReflectionTypeInfo.InjectParameterInfo> list = new List<ReflectionTypeInfo.InjectParameterInfo>();
		ConstructorInfo constructorInfo = TryGetInjectConstructor(type);
		if (constructorInfo != null)
		{
			list.AddRange(from x in constructorInfo.GetParameters()
				select CreateInjectableInfoForParam(type, x));
		}
		return new ReflectionTypeInfo.InjectConstructorInfo(constructorInfo, list);
	}

	private static ReflectionTypeInfo.InjectParameterInfo CreateInjectableInfoForParam(Type parentType, ParameterInfo paramInfo)
	{
		List<InjectAttributeBase> list = TypeExtensions.AllAttributes<InjectAttributeBase>(paramInfo).ToList();
		Assert.That(list.Count <= 1, "Found multiple 'Inject' attributes on type parameter '{0}' of type '{1}'.  Parameter should only have one", paramInfo.Name, parentType);
		InjectAttributeBase injectAttributeBase = list.SingleOrDefault();
		object identifier = null;
		bool flag = false;
		InjectSources sourceType = InjectSources.Any;
		if (injectAttributeBase != null)
		{
			identifier = injectAttributeBase.Id;
			flag = injectAttributeBase.Optional;
			sourceType = injectAttributeBase.Source;
		}
		bool flag2 = (paramInfo.Attributes & ParameterAttributes.HasDefault) == ParameterAttributes.HasDefault;
		return new ReflectionTypeInfo.InjectParameterInfo(paramInfo, new InjectableInfo(flag2 || flag, identifier, paramInfo.Name, paramInfo.ParameterType, (!flag2) ? null : paramInfo.DefaultValue, sourceType));
	}

	private static InjectableInfo GetInjectableInfoForMember(Type parentType, MemberInfo memInfo)
	{
		List<InjectAttributeBase> list = TypeExtensions.AllAttributes<InjectAttributeBase>(memInfo).ToList();
		Assert.That(list.Count <= 1, "Found multiple 'Inject' attributes on type field '{0}' of type '{1}'.  Field should only container one Inject attribute", memInfo.Name, parentType);
		InjectAttributeBase injectAttributeBase = list.SingleOrDefault();
		object identifier = null;
		bool optional = false;
		InjectSources sourceType = InjectSources.Any;
		if (injectAttributeBase != null)
		{
			identifier = injectAttributeBase.Id;
			optional = injectAttributeBase.Optional;
			sourceType = injectAttributeBase.Source;
		}
		Type memberType = ((!(memInfo is FieldInfo)) ? ((PropertyInfo)memInfo).PropertyType : ((FieldInfo)memInfo).FieldType);
		return new InjectableInfo(optional, identifier, memInfo.Name, memberType, null, sourceType);
	}

	private static ConstructorInfo TryGetInjectConstructor(Type type)
	{
		if (TypeExtensions.DerivesFromOrEqual<Component>(type))
		{
			return null;
		}
		if (TypeExtensions.IsAbstract(type))
		{
			return null;
		}
		ConstructorInfo[] array = TypeExtensions.Constructors(type);
		if (LinqExtensions.IsEmpty(array))
		{
			return null;
		}
		if (LinqExtensions.HasMoreThan(array, 1))
		{
			ConstructorInfo constructorInfo = array.Where((ConstructorInfo c) => _injectAttributeTypes.Any((Type a) => TypeExtensions.HasAttribute(c, a))).SingleOrDefault();
			if (constructorInfo != null)
			{
				return constructorInfo;
			}
			ConstructorInfo constructorInfo2 = LinqExtensions.OnlyOrDefault(array.Where((ConstructorInfo x) => x.IsPublic));
			if (constructorInfo2 != null)
			{
				return constructorInfo2;
			}
			return array.OrderBy((ConstructorInfo x) => x.GetParameters().Count()).First();
		}
		return array[0];
	}
}
