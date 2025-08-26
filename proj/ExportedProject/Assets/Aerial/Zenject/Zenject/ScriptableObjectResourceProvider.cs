using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject;

[NoReflectionBaking]
public class ScriptableObjectResourceProvider : IProvider
{
	private readonly DiContainer _container;

	private readonly Type _resourceType;

	private readonly string _resourcePath;

	private readonly List<TypeValuePair> _extraArguments;

	private readonly bool _createNew;

	private readonly object _concreteIdentifier;

	private readonly Action<InjectContext, object> _instantiateCallback;

	public bool IsCached => false;

	public bool TypeVariesBasedOnMemberType => false;

	public ScriptableObjectResourceProvider(string resourcePath, Type resourceType, DiContainer container, IEnumerable<TypeValuePair> extraArguments, bool createNew, object concreteIdentifier, Action<InjectContext, object> instantiateCallback)
	{
		_container = container;
		Assert.DerivesFromOrEqual<ScriptableObject>(resourceType);
		_extraArguments = extraArguments.ToList();
		_resourceType = resourceType;
		_resourcePath = resourcePath;
		_createNew = createNew;
		_concreteIdentifier = concreteIdentifier;
		_instantiateCallback = instantiateCallback;
	}

	public Type GetInstanceType(InjectContext context)
	{
		return _resourceType;
	}

	public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsNotNull(context);
		if (_createNew)
		{
			UnityEngine.Object[] array = Resources.LoadAll(_resourcePath, _resourceType);
			for (int i = 0; i < array.Length; i++)
			{
				buffer.Add(UnityEngine.Object.Instantiate(array[i]));
			}
		}
		else
		{
			MiscExtensions.AllocFreeAddRange(buffer, Resources.LoadAll(_resourcePath, _resourceType));
		}
		Assert.That(buffer.Count > 0, "Could not find resource at path '{0}' with type '{1}'", _resourcePath, _resourceType);
		injectAction = delegate
		{
			for (int j = 0; j < buffer.Count; j++)
			{
				object obj = buffer[j];
				List<TypeValuePair> list = ZenPools.SpawnList<TypeValuePair>();
				MiscExtensions.AllocFreeAddRange(list, _extraArguments);
				MiscExtensions.AllocFreeAddRange(list, args);
				_container.InjectExplicit(obj, _resourceType, list, context, _concreteIdentifier);
				ZenPools.DespawnList(list);
				if (_instantiateCallback != null)
				{
					_instantiateCallback(context, obj);
				}
			}
		};
	}
}
