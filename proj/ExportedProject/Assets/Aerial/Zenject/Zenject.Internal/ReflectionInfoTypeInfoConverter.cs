using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ModestTree;
using UnityEngine;

namespace Zenject.Internal;

public static class ReflectionInfoTypeInfoConverter
{
	public static InjectTypeInfo.InjectMethodInfo ConvertMethod(ReflectionTypeInfo.InjectMethodInfo injectMethod)
	{
		MethodInfo methodInfo = injectMethod.MethodInfo;
		ZenInjectMethod zenInjectMethod = TryCreateActionForMethod(methodInfo);
		if (zenInjectMethod == null)
		{
			zenInjectMethod = delegate(object obj, object[] args)
			{
				methodInfo.Invoke(obj, args);
			};
		}
		return new InjectTypeInfo.InjectMethodInfo(zenInjectMethod, injectMethod.Parameters.Select((ReflectionTypeInfo.InjectParameterInfo x) => x.InjectableInfo).ToArray(), methodInfo.Name);
	}

	public static InjectTypeInfo.InjectConstructorInfo ConvertConstructor(ReflectionTypeInfo.InjectConstructorInfo injectConstructor, Type type)
	{
		return new InjectTypeInfo.InjectConstructorInfo(TryCreateFactoryMethod(type, injectConstructor), injectConstructor.Parameters.Select((ReflectionTypeInfo.InjectParameterInfo x) => x.InjectableInfo).ToArray());
	}

	public static InjectTypeInfo.InjectMemberInfo ConvertField(Type parentType, ReflectionTypeInfo.InjectFieldInfo injectField)
	{
		return new InjectTypeInfo.InjectMemberInfo(GetSetter(parentType, injectField.FieldInfo), injectField.InjectableInfo);
	}

	public static InjectTypeInfo.InjectMemberInfo ConvertProperty(Type parentType, ReflectionTypeInfo.InjectPropertyInfo injectProperty)
	{
		return new InjectTypeInfo.InjectMemberInfo(GetSetter(parentType, injectProperty.PropertyInfo), injectProperty.InjectableInfo);
	}

	private static ZenFactoryMethod TryCreateFactoryMethod(Type type, ReflectionTypeInfo.InjectConstructorInfo reflectionInfo)
	{
		if (TypeExtensions.DerivesFromOrEqual<Component>(type))
		{
			return null;
		}
		if (TypeExtensions.IsAbstract(type))
		{
			Assert.That(LinqExtensions.IsEmpty(reflectionInfo.Parameters));
			return null;
		}
		ConstructorInfo constructorInfo = reflectionInfo.ConstructorInfo;
		ZenFactoryMethod zenFactoryMethod = TryCreateFactoryMethodCompiledLambdaExpression(type, constructorInfo);
		if (zenFactoryMethod == null)
		{
			zenFactoryMethod = ((!(constructorInfo == null)) ? new ZenFactoryMethod(constructorInfo.Invoke) : ((ZenFactoryMethod)delegate(object[] args)
			{
				Assert.That(args.Length == 0);
				return Activator.CreateInstance(type, new object[0]);
			}));
		}
		return zenFactoryMethod;
	}

	private static ZenFactoryMethod TryCreateFactoryMethodCompiledLambdaExpression(Type type, ConstructorInfo constructor)
	{
		if (type.ContainsGenericParameters)
		{
			return null;
		}
		ParameterExpression parameterExpression = Expression.Parameter(typeof(object[]));
		if (constructor == null)
		{
			return Expression.Lambda<ZenFactoryMethod>(Expression.Convert(Expression.New(type), typeof(object)), new ParameterExpression[1] { parameterExpression }).Compile();
		}
		ParameterInfo[] parameters = constructor.GetParameters();
		Expression[] array = new Expression[parameters.Length];
		for (int i = 0; i != parameters.Length; i++)
		{
			array[i] = Expression.Convert(Expression.ArrayIndex(parameterExpression, Expression.Constant(i)), parameters[i].ParameterType);
		}
		return Expression.Lambda<ZenFactoryMethod>(Expression.Convert(Expression.New(constructor, array), typeof(object)), new ParameterExpression[1] { parameterExpression }).Compile();
	}

	private static ZenInjectMethod TryCreateActionForMethod(MethodInfo methodInfo)
	{
		if (methodInfo.DeclaringType.ContainsGenericParameters)
		{
			return null;
		}
		ParameterInfo[] parameters = methodInfo.GetParameters();
		if (parameters.Any((ParameterInfo x) => x.ParameterType.ContainsGenericParameters))
		{
			return null;
		}
		Expression[] array = new Expression[parameters.Length];
		ParameterExpression parameterExpression = Expression.Parameter(typeof(object[]));
		ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object));
		for (int num = 0; num != parameters.Length; num++)
		{
			array[num] = Expression.Convert(Expression.ArrayIndex(parameterExpression, Expression.Constant(num)), parameters[num].ParameterType);
		}
		return Expression.Lambda<ZenInjectMethod>(Expression.Call(Expression.Convert(parameterExpression2, methodInfo.DeclaringType), methodInfo, array), new ParameterExpression[2] { parameterExpression2, parameterExpression }).Compile();
	}

	private static IEnumerable<FieldInfo> GetAllFields(Type t, BindingFlags flags)
	{
		if (t == null)
		{
			return Enumerable.Empty<FieldInfo>();
		}
		return t.GetFields(flags).Concat(GetAllFields(t.BaseType, flags)).Distinct();
	}

	private static ZenMemberSetterMethod GetOnlyPropertySetter(Type parentType, string propertyName)
	{
		Assert.That(parentType != null);
		Assert.That(!string.IsNullOrEmpty(propertyName));
		List<FieldInfo> source = GetAllFields(parentType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.FlattenHierarchy).ToList();
		List<FieldInfo> writeableFields = source.Where((FieldInfo f) => f.Name == string.Format("<" + propertyName + ">k__BackingField", propertyName)).ToList();
		if (!writeableFields.Any())
		{
			throw new ZenjectException(string.Format("Can't find backing field for get only property {0} on {1}.\r\n{2}", propertyName, parentType.FullName, string.Join(";", source.Select((FieldInfo f) => f.Name).ToArray())));
		}
		return delegate(object injectable, object value)
		{
			writeableFields.ForEach(delegate(FieldInfo f)
			{
				f.SetValue(injectable, value);
			});
		};
	}

	private static ZenMemberSetterMethod GetSetter(Type parentType, MemberInfo memInfo)
	{
		ZenMemberSetterMethod zenMemberSetterMethod = TryGetSetterAsCompiledExpression(parentType, memInfo);
		if (zenMemberSetterMethod != null)
		{
			return zenMemberSetterMethod;
		}
		FieldInfo fieldInfo = memInfo as FieldInfo;
		PropertyInfo propInfo = memInfo as PropertyInfo;
		if (fieldInfo != null)
		{
			return delegate(object injectable, object value)
			{
				fieldInfo.SetValue(injectable, value);
			};
		}
		Assert.IsNotNull(propInfo);
		if (propInfo.CanWrite)
		{
			return delegate(object injectable, object value)
			{
				propInfo.SetValue(injectable, value, null);
			};
		}
		return GetOnlyPropertySetter(parentType, propInfo.Name);
	}

	private static ZenMemberSetterMethod TryGetSetterAsCompiledExpression(Type parentType, MemberInfo memInfo)
	{
		if (parentType.ContainsGenericParameters)
		{
			return null;
		}
		FieldInfo fieldInfo = memInfo as FieldInfo;
		PropertyInfo propertyInfo = memInfo as PropertyInfo;
		if (!TypeExtensions.IsValueType(parentType) && (fieldInfo == null || !fieldInfo.IsInitOnly))
		{
			Type type = ((!(fieldInfo != null)) ? propertyInfo.PropertyType : fieldInfo.FieldType);
			ParameterExpression parameterExpression = Expression.Parameter(typeof(object));
			ParameterExpression parameterExpression2 = Expression.Parameter(typeof(object));
			return Expression.Lambda<ZenMemberSetterMethod>(Expression.Assign(Expression.MakeMemberAccess(Expression.Convert(parameterExpression, parentType), memInfo), Expression.Convert(parameterExpression2, type)), new ParameterExpression[2] { parameterExpression, parameterExpression2 }).Compile();
		}
		return null;
	}
}
