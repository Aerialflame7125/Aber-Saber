using System.Linq;
using ModestTree;

namespace Zenject;

[NoReflectionBaking]
public class MemoryPoolBindingFinalizer<TContract> : ProviderBindingFinalizer
{
	private readonly MemoryPoolBindInfo _poolBindInfo;

	private readonly FactoryBindInfo _factoryBindInfo;

	public MemoryPoolBindingFinalizer(BindInfo bindInfo, FactoryBindInfo factoryBindInfo, MemoryPoolBindInfo poolBindInfo)
		: base(bindInfo)
	{
		Assert.That(TypeExtensions.DerivesFrom<IMemoryPool>(factoryBindInfo.FactoryType));
		_factoryBindInfo = factoryBindInfo;
		_poolBindInfo = poolBindInfo;
	}

	protected override void OnFinalizeBinding(DiContainer container)
	{
		FactoryProviderWrapper<TContract> param = new FactoryProviderWrapper<TContract>(_factoryBindInfo.ProviderFunc(container), new InjectContext(container, typeof(TContract)));
		MemoryPoolSettings param2 = new MemoryPoolSettings(_poolBindInfo.InitialSize, _poolBindInfo.MaxSize, _poolBindInfo.ExpandMethod);
		TransientProvider transientProvider = new TransientProvider(_factoryBindInfo.FactoryType, container, _factoryBindInfo.Arguments.Concat(InjectUtil.CreateArgListExplicit(param, param2)).ToList(), base.BindInfo.ContextInfo, base.BindInfo.ConcreteIdentifier, null);
		IProvider provider;
		if (base.BindInfo.Scope == ScopeTypes.Unset || base.BindInfo.Scope == ScopeTypes.Singleton)
		{
			provider = Zenject.BindingUtil.CreateCachedProvider(transientProvider);
		}
		else
		{
			Assert.IsEqual(base.BindInfo.Scope, ScopeTypes.Transient);
			provider = transientProvider;
		}
		RegisterProviderForAllContracts(container, provider);
	}
}
