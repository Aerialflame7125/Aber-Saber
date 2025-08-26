using System;
using System.Collections.Generic;
using ModestTree;

namespace Zenject.Internal;

[NoReflectionBaking]
public class DecoratorProvider<TContract> : IDecoratorProvider
{
	private readonly Dictionary<IProvider, List<object>> _cachedInstances = new Dictionary<IProvider, List<object>>();

	private readonly DiContainer _container;

	private readonly List<Guid> _factoryBindIds = new List<Guid>();

	private List<IFactory<TContract, TContract>> _decoratorFactories;

	public DecoratorProvider(DiContainer container)
	{
		_container = container;
	}

	public void AddFactoryId(Guid factoryBindId)
	{
		_factoryBindIds.Add(factoryBindId);
	}

	private void LazyInitializeDecoratorFactories()
	{
		if (_decoratorFactories == null)
		{
			_decoratorFactories = new List<IFactory<TContract, TContract>>();
			for (int i = 0; i < _factoryBindIds.Count; i++)
			{
				Guid guid = _factoryBindIds[i];
				IFactory<TContract, TContract> item = _container.ResolveId<IFactory<TContract, TContract>>(guid);
				_decoratorFactories.Add(item);
			}
		}
	}

	public void GetAllInstances(IProvider provider, InjectContext context, List<object> buffer)
	{
		if (provider.IsCached)
		{
			if (!_cachedInstances.TryGetValue(provider, out var value))
			{
				value = new List<object>();
				WrapProviderInstances(provider, context, value);
				_cachedInstances.Add(provider, value);
			}
			MiscExtensions.AllocFreeAddRange(buffer, value);
		}
		else
		{
			WrapProviderInstances(provider, context, buffer);
		}
	}

	private void WrapProviderInstances(IProvider provider, InjectContext context, List<object> buffer)
	{
		LazyInitializeDecoratorFactories();
		IProviderExtensions.GetAllInstances(provider, context, buffer);
		for (int i = 0; i < buffer.Count; i++)
		{
			buffer[i] = DecorateInstance(buffer[i], context);
		}
	}

	private object DecorateInstance(object instance, InjectContext context)
	{
		for (int i = 0; i < _decoratorFactories.Count; i++)
		{
			instance = _decoratorFactories[i].Create((!context.Container.IsValidating) ? ((TContract)instance) : default(TContract));
		}
		return instance;
	}
}
