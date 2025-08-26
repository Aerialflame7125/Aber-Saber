using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using Zenject.Internal;

namespace Zenject;

[NoReflectionBaking]
public class TransientProvider : IProvider
{
	private readonly DiContainer _container;

	private readonly Type _concreteType;

	private readonly List<TypeValuePair> _extraArguments;

	private readonly object _concreteIdentifier;

	private readonly Action<InjectContext, object> _instantiateCallback;

	public bool IsCached => false;

	public bool TypeVariesBasedOnMemberType => TypeExtensions.IsOpenGenericType(_concreteType);

	public TransientProvider(Type concreteType, DiContainer container, IEnumerable<TypeValuePair> extraArguments, string bindingContext, object concreteIdentifier, Action<InjectContext, object> instantiateCallback)
	{
		Assert.That(!TypeExtensions.IsAbstract(concreteType), "Expected non-abstract type for given binding but instead found type '{0}'{1}", concreteType, (bindingContext != null) ? MiscExtensions.Fmt(" when binding '{0}'", bindingContext) : string.Empty);
		_container = container;
		_concreteType = concreteType;
		_extraArguments = extraArguments.ToList();
		_concreteIdentifier = concreteIdentifier;
		_instantiateCallback = instantiateCallback;
	}

	public Type GetInstanceType(InjectContext context)
	{
		if (!TypeExtensions.DerivesFromOrEqual(_concreteType, context.MemberType))
		{
			return null;
		}
		return GetTypeToCreate(context.MemberType);
	}

	public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsNotNull(context);
		Type instanceType = GetTypeToCreate(context.MemberType);
		List<TypeValuePair> extraArgs = ZenPools.SpawnList<TypeValuePair>();
		MiscExtensions.AllocFreeAddRange(extraArgs, _extraArguments);
		MiscExtensions.AllocFreeAddRange(extraArgs, args);
		object instance = _container.InstantiateExplicit(instanceType, autoInject: false, extraArgs, context, _concreteIdentifier);
		injectAction = delegate
		{
			_container.InjectExplicit(instance, instanceType, extraArgs, context, _concreteIdentifier);
			Assert.That(extraArgs.Count == 0);
			ZenPools.DespawnList(extraArgs);
			if (_instantiateCallback != null)
			{
				_instantiateCallback(context, instance);
			}
		};
		buffer.Add(instance);
	}

	private Type GetTypeToCreate(Type contractType)
	{
		return ProviderUtil.GetTypeToInstantiate(contractType, _concreteType);
	}
}
