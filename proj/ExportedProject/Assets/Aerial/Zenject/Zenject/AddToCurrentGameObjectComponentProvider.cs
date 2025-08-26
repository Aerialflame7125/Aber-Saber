using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject;

[NoReflectionBaking]
public class AddToCurrentGameObjectComponentProvider : IProvider
{
	private readonly Type _componentType;

	private readonly DiContainer _container;

	private readonly List<TypeValuePair> _extraArguments;

	private readonly object _concreteIdentifier;

	private readonly Action<InjectContext, object> _instantiateCallback;

	public bool IsCached => false;

	public bool TypeVariesBasedOnMemberType => false;

	protected DiContainer Container => _container;

	protected Type ComponentType => _componentType;

	public AddToCurrentGameObjectComponentProvider(DiContainer container, Type componentType, IEnumerable<TypeValuePair> extraArguments, object concreteIdentifier, Action<InjectContext, object> instantiateCallback)
	{
		Assert.That(TypeExtensions.DerivesFrom<Component>(componentType));
		_extraArguments = extraArguments.ToList();
		_componentType = componentType;
		_container = container;
		_concreteIdentifier = concreteIdentifier;
		_instantiateCallback = instantiateCallback;
	}

	public Type GetInstanceType(InjectContext context)
	{
		return _componentType;
	}

	public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsNotNull(context);
		Assert.That(TypeExtensions.DerivesFrom<Component>(context.ObjectType), "Object '{0}' can only be injected into MonoBehaviour's since it was bound with 'FromNewComponentSibling'. Attempted to inject into non-MonoBehaviour '{1}'", context.MemberType, context.ObjectType);
		object instance;
		if (!_container.IsValidating || TypeAnalyzer.ShouldAllowDuringValidation(_componentType))
		{
			GameObject gameObject = ((Component)context.ObjectInstance).gameObject;
			instance = gameObject.GetComponent(_componentType);
			if (instance != null)
			{
				injectAction = null;
				buffer.Add(instance);
				return;
			}
			instance = gameObject.AddComponent(_componentType);
		}
		else
		{
			instance = new ValidationMarker(_componentType);
		}
		injectAction = delegate
		{
			List<TypeValuePair> list = ZenPools.SpawnList<TypeValuePair>();
			MiscExtensions.AllocFreeAddRange(list, _extraArguments);
			MiscExtensions.AllocFreeAddRange(list, args);
			_container.InjectExplicit(instance, _componentType, list, context, _concreteIdentifier);
			Assert.That(LinqExtensions.IsEmpty(list));
			ZenPools.DespawnList(list);
			if (_instantiateCallback != null)
			{
				_instantiateCallback(context, instance);
			}
		};
		buffer.Add(instance);
	}
}
