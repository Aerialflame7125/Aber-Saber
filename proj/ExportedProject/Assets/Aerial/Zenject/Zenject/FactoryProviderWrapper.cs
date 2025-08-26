using ModestTree;
using Zenject.Internal;

namespace Zenject;

public class FactoryProviderWrapper<TContract> : IFactory<TContract>, IFactory
{
	private readonly IProvider _provider;

	private readonly InjectContext _injectContext;

	public FactoryProviderWrapper(IProvider provider, InjectContext injectContext)
	{
		Assert.That(TypeExtensions.DerivesFromOrEqual<TContract>(injectContext.MemberType));
		_provider = provider;
		_injectContext = injectContext;
	}

	public TContract Create()
	{
		object instance = IProviderExtensions.GetInstance(_provider, _injectContext);
		if (_injectContext.Container.IsValidating)
		{
			return default(TContract);
		}
		Assert.That(instance == null || TypeExtensions.DerivesFromOrEqual(instance.GetType(), _injectContext.MemberType));
		return (TContract)instance;
	}

	private static object __zenCreate(object[] P_0)
	{
		return new FactoryProviderWrapper<TContract>((IProvider)P_0[0], (InjectContext)P_0[1]);
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(FactoryProviderWrapper<TContract>), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[2]
		{
			new InjectableInfo(optional: false, null, "provider", typeof(IProvider), null, InjectSources.Any),
			new InjectableInfo(optional: false, null, "injectContext", typeof(InjectContext), null, InjectSources.Any)
		}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
