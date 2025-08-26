using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject;

[NoReflectionBaking]
public class CachedProvider : IProvider
{
	private readonly IProvider _creator;

	private List<object> _instances;

	private bool _isCreatingInstance;

	public bool IsCached => true;

	public bool TypeVariesBasedOnMemberType
	{
		get
		{
			throw Assert.CreateException();
		}
	}

	public int NumInstances => (_instances != null) ? _instances.Count : 0;

	public CachedProvider(IProvider creator)
	{
		_creator = creator;
	}

	public void ClearCache()
	{
		_instances = null;
	}

	public Type GetInstanceType(InjectContext context)
	{
		return _creator.GetInstanceType(context);
	}

	public void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer)
	{
		Assert.IsNotNull(context);
		if (_instances != null)
		{
			injectAction = null;
			MiscExtensions.AllocFreeAddRange(buffer, _instances);
			return;
		}
		if (_isCreatingInstance)
		{
			Type instanceType = _creator.GetInstanceType(context);
			throw Assert.CreateException("Found circular dependency when creating type '{0}'. Object graph:\n {1}{2}\n", instanceType, context.GetObjectGraphString(), instanceType);
		}
		_isCreatingInstance = true;
		List<object> list = new List<object>();
		_creator.GetAllInstancesWithInjectSplit(context, args, out injectAction, list);
		Assert.IsNotNull(list);
		_instances = list;
		_isCreatingInstance = false;
		MiscExtensions.AllocFreeAddRange(buffer, list);
	}
}
