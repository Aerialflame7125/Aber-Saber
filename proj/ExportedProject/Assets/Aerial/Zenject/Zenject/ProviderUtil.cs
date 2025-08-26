using System;
using ModestTree;

namespace Zenject;

public static class ProviderUtil
{
	public static Type GetTypeToInstantiate(Type contractType, Type concreteType)
	{
		if (TypeExtensions.IsOpenGenericType(concreteType))
		{
			return concreteType.MakeGenericType(contractType.GetGenericArguments());
		}
		Assert.DerivesFromOrEqual(concreteType, contractType);
		return concreteType;
	}
}
