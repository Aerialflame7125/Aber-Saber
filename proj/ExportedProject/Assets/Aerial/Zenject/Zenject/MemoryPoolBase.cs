using System;
using System.Collections.Generic;
using ModestTree;
using Zenject.Internal;

namespace Zenject;

[ZenjectAllowDuringValidation]
public class MemoryPoolBase<TContract> : IValidatable, IMemoryPool, IDisposable
{
	private Stack<TContract> _inactiveItems;

	private IFactory<TContract> _factory;

	private MemoryPoolSettings _settings;

	private DiContainer _container;

	private int _activeCount;

	protected DiContainer Container => _container;

	public IEnumerable<TContract> InactiveItems => _inactiveItems;

	public int NumTotal => NumInactive + NumActive;

	public int NumInactive => _inactiveItems.Count;

	public int NumActive => _activeCount;

	public Type ItemType => typeof(TContract);

	[Inject]
	private void Construct(IFactory<TContract> factory, DiContainer container, [InjectOptional] MemoryPoolSettings settings)
	{
		_settings = settings ?? MemoryPoolSettings.Default;
		_factory = factory;
		_container = container;
		_inactiveItems = new Stack<TContract>(_settings.InitialSize);
		if (!container.IsValidating)
		{
			for (int i = 0; i < _settings.InitialSize; i++)
			{
				_inactiveItems.Push(AllocNew());
			}
		}
	}

	public void Dispose()
	{
	}

	void IMemoryPool.Despawn(object item)
	{
		Despawn((TContract)item);
	}

	public void Despawn(TContract item)
	{
		Assert.That(!_inactiveItems.Contains(item), "Tried to return an item to pool {0} twice", GetType());
		_activeCount--;
		_inactiveItems.Push(item);
		OnDespawned(item);
		if (_inactiveItems.Count > _settings.MaxSize)
		{
			Resize(_settings.MaxSize);
		}
	}

	private TContract AllocNew()
	{
		try
		{
			TContract val = _factory.Create();
			if (!_container.IsValidating)
			{
				Assert.IsNotNull(val, "Factory '{0}' returned null value when creating via {1}!", _factory.GetType(), GetType());
				OnCreated(val);
			}
			return val;
		}
		catch (Exception innerException)
		{
			throw new ZenjectException(MiscExtensions.Fmt("Error during construction of type '{0}' via {1}.Create method!", typeof(TContract), GetType()), innerException);
		}
	}

	void IValidatable.Validate()
	{
		try
		{
			_factory.Create();
		}
		catch (Exception innerException)
		{
			throw new ZenjectException(MiscExtensions.Fmt("Validation for factory '{0}' failed", GetType()), innerException);
		}
	}

	public void Clear()
	{
		Resize(0);
	}

	public void ShrinkBy(int numToRemove)
	{
		Resize(_inactiveItems.Count - numToRemove);
	}

	public void ExpandBy(int numToAdd)
	{
		Resize(_inactiveItems.Count + numToAdd);
	}

	protected TContract GetInternal()
	{
		if (_inactiveItems.Count == 0)
		{
			ExpandPool();
			Assert.That(!LinqExtensions.IsEmpty(_inactiveItems));
		}
		TContract val = _inactiveItems.Pop();
		_activeCount++;
		OnSpawned(val);
		return val;
	}

	public void Resize(int desiredPoolSize)
	{
		if (_inactiveItems.Count != desiredPoolSize)
		{
			if (_settings.ExpandMethod == PoolExpandMethods.Disabled)
			{
				throw new PoolExceededFixedSizeException(MiscExtensions.Fmt("Pool factory '{0}' attempted resize but pool set to fixed size of '{1}'!", GetType(), _inactiveItems.Count));
			}
			Assert.That(desiredPoolSize >= 0, "Attempted to resize the pool to a negative amount");
			while (_inactiveItems.Count > desiredPoolSize)
			{
				OnDestroyed(_inactiveItems.Pop());
			}
			while (desiredPoolSize > _inactiveItems.Count)
			{
				_inactiveItems.Push(AllocNew());
			}
			Assert.IsEqual(_inactiveItems.Count, desiredPoolSize);
		}
	}

	private void ExpandPool()
	{
		switch (_settings.ExpandMethod)
		{
		case PoolExpandMethods.Disabled:
			throw new PoolExceededFixedSizeException(MiscExtensions.Fmt("Pool factory '{0}' exceeded its fixed size of '{1}'!", GetType(), _inactiveItems.Count));
		case PoolExpandMethods.OneAtATime:
			ExpandBy(1);
			break;
		case PoolExpandMethods.Double:
			if (NumTotal == 0)
			{
				ExpandBy(1);
			}
			else
			{
				ExpandBy(NumTotal);
			}
			break;
		default:
			throw Assert.CreateException();
		}
	}

	protected virtual void OnDespawned(TContract item)
	{
	}

	protected virtual void OnSpawned(TContract item)
	{
	}

	protected virtual void OnCreated(TContract item)
	{
	}

	protected virtual void OnDestroyed(TContract item)
	{
	}

	private static object __zenCreate(object[] P_0)
	{
		return new MemoryPoolBase<TContract>();
	}

	private static void __zenInjectMethod0(object P_0, object[] P_1)
	{
		((MemoryPoolBase<TContract>)P_0).Construct((IFactory<TContract>)P_1[0], (DiContainer)P_1[1], (MemoryPoolSettings)P_1[2]);
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(MemoryPoolBase<TContract>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[1]
		{
			new InjectTypeInfo.InjectMethodInfo(__zenInjectMethod0, new InjectableInfo[3]
			{
				new InjectableInfo(optional: false, null, "factory", typeof(IFactory<TContract>), null, InjectSources.Any),
				new InjectableInfo(optional: false, null, "container", typeof(DiContainer), null, InjectSources.Any),
				new InjectableInfo(optional: true, null, "settings", typeof(MemoryPoolSettings), null, InjectSources.Any)
			}, "Construct")
		}, new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
