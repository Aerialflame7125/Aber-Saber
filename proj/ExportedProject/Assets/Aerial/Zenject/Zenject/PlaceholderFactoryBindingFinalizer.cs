using System.Linq;
using ModestTree;

namespace Zenject;

[NoReflectionBaking]
public class PlaceholderFactoryBindingFinalizer<TContract> : ProviderBindingFinalizer
{
	private readonly FactoryBindInfo _factoryBindInfo;

	public PlaceholderFactoryBindingFinalizer(BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
		: base(bindInfo)
	{
		Assert.That(TypeExtensions.DerivesFrom<IPlaceholderFactory>(factoryBindInfo.FactoryType));
		_factoryBindInfo = factoryBindInfo;
	}

	protected override void OnFinalizeBinding(DiContainer container)
	{
		IProvider param = _factoryBindInfo.ProviderFunc(container);
		TransientProvider transientProvider = new TransientProvider(_factoryBindInfo.FactoryType, container, _factoryBindInfo.Arguments.Concat(InjectUtil.CreateArgListExplicit(param, new InjectContext(container, typeof(TContract)))).ToList(), base.BindInfo.ContextInfo, base.BindInfo.ConcreteIdentifier, null);
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
