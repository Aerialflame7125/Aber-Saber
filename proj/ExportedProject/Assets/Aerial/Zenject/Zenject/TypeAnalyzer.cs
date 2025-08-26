using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ModestTree;
using Zenject.Internal;

namespace Zenject;

public static class TypeAnalyzer
{
	private static Dictionary<Type, InjectTypeInfo> _typeInfo = new Dictionary<Type, InjectTypeInfo>();

	public const string ReflectionBakingGetInjectInfoMethodName = "__zenCreateInjectTypeInfo";

	public const string ReflectionBakingFactoryMethodName = "__zenCreate";

	public const string ReflectionBakingInjectMethodPrefix = "__zenInjectMethod";

	public const string ReflectionBakingFieldSetterPrefix = "__zenFieldSetter";

	public const string ReflectionBakingPropertySetterPrefix = "__zenPropertySetter";

	public static ReflectionBakingCoverageModes ReflectionBakingCoverageMode { get; set; }

	public static bool ShouldAllowDuringValidation<T>()
	{
		return ShouldAllowDuringValidation(typeof(T));
	}

	public static bool ShouldAllowDuringValidation(Type type)
	{
		return false;
	}

	public static bool HasInfo<T>()
	{
		return HasInfo(typeof(T));
	}

	public static bool HasInfo(Type type)
	{
		return TryGetInfo(type) != null;
	}

	public static InjectTypeInfo GetInfo<T>()
	{
		return GetInfo(typeof(T));
	}

	public static InjectTypeInfo GetInfo(Type type)
	{
		InjectTypeInfo injectTypeInfo = TryGetInfo(type);
		Assert.IsNotNull(injectTypeInfo, "Unable to get type info for type '{0}'", type);
		return injectTypeInfo;
	}

	public static InjectTypeInfo TryGetInfo<T>()
	{
		return TryGetInfo(typeof(T));
	}

	public static InjectTypeInfo TryGetInfo(Type type)
	{
		if (_typeInfo.TryGetValue(type, out var value))
		{
			return value;
		}
		value = GetInfoInternal(type);
		if (value != null)
		{
			Assert.IsEqual(value.Type, type);
			Assert.IsNull(value.BaseTypeInfo);
			Type type2 = TypeExtensions.BaseType(type);
			if (type2 != null && ShouldAnalyzeType(type2))
			{
				value.BaseTypeInfo = TryGetInfo(type2);
			}
		}
		_typeInfo.Add(type, value);
		return value;
	}

	private static InjectTypeInfo GetInfoInternal(Type type)
	{
		if (!ShouldAnalyzeType(type))
		{
			return null;
		}
		MethodInfo method = type.GetMethod("__zenCreateInjectTypeInfo", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		if (method != null)
		{
			ZenTypeInfoGetter zenTypeInfoGetter = (ZenTypeInfoGetter)Delegate.CreateDelegate(typeof(ZenTypeInfoGetter), method);
			return zenTypeInfoGetter();
		}
		if (ReflectionBakingCoverageMode == ReflectionBakingCoverageModes.NoCheckAssumeFullCoverage)
		{
			return null;
		}
		if (ReflectionBakingCoverageMode == ReflectionBakingCoverageModes.FallbackToDirectReflectionWithWarning)
		{
			Log.Warn("No reflection baking information found for type '{0}' - using more costly direct reflection instead", type);
		}
		return CreateTypeInfoFromReflection(type);
	}

	public static bool ShouldAnalyzeType(Type type)
	{
		if (type == null || TypeExtensions.IsEnum(type) || type.IsArray || TypeExtensions.IsInterface(type) || TypeExtensions.ContainsGenericParameters(type) || IsStaticType(type) || type == typeof(object))
		{
			return false;
		}
		return ShouldAnalyzeNamespace(type.Namespace);
	}

	private static bool IsStaticType(Type type)
	{
		return TypeExtensions.IsAbstract(type) && TypeExtensions.IsSealed(type);
	}

	public static bool ShouldAnalyzeNamespace(string ns)
	{
		if (ns == null)
		{
			return true;
		}
		return ns != "System" && !ns.StartsWith("System.") && ns != "UnityEngine" && !ns.StartsWith("UnityEngine.") && ns != "UnityEditor" && !ns.StartsWith("UnityEditor.") && ns != "UnityStandardAssets" && !ns.StartsWith("UnityStandardAssets.");
	}

	private static InjectTypeInfo CreateTypeInfoFromReflection(Type type)
	{
		ReflectionTypeInfo reflectionInfo = ReflectionTypeAnalyzer.GetReflectionInfo(type);
		InjectTypeInfo.InjectConstructorInfo injectConstructor = ReflectionInfoTypeInfoConverter.ConvertConstructor(reflectionInfo.InjectConstructor, type);
		InjectTypeInfo.InjectMethodInfo[] injectMethods = reflectionInfo.InjectMethods.Select(ReflectionInfoTypeInfoConverter.ConvertMethod).ToArray();
		InjectTypeInfo.InjectMemberInfo[] injectMembers = reflectionInfo.InjectFields.Select((ReflectionTypeInfo.InjectFieldInfo x) => ReflectionInfoTypeInfoConverter.ConvertField(type, x)).Concat(reflectionInfo.InjectProperties.Select((ReflectionTypeInfo.InjectPropertyInfo x) => ReflectionInfoTypeInfoConverter.ConvertProperty(type, x))).ToArray();
		return new InjectTypeInfo(type, injectConstructor, injectMethods, injectMembers);
	}
}
