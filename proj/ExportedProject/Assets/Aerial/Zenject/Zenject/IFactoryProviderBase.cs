using System;
using System.Collections.Generic;
using Zenject.Internal;

namespace Zenject;

public abstract class IFactoryProviderBase<TContract> : IProvider
{
	public bool IsCached => false;

	protected Guid FactoryId { get; private set; }

	protected DiContainer Container { get; private set; }

	public bool TypeVariesBasedOnMemberType => false;

	public IFactoryProviderBase(DiContainer container, Guid factoryId)
	{
		Container = container;
		FactoryId = factoryId;
	}

	public Type GetInstanceType(InjectContext context)
	{
		return typeof(TContract);
	}

	public abstract void GetAllInstancesWithInjectSplit(InjectContext context, List<TypeValuePair> args, out Action injectAction, List<object> buffer);

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(IFactoryProviderBase<TContract>), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
