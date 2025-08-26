using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using Zenject.Internal;

namespace Zenject;

public abstract class PlaceholderFactoryBase<TValue> : IPlaceholderFactory, IValidatable
{
	private IProvider _provider;

	private InjectContext _injectContext;

	protected abstract IEnumerable<Type> ParamTypes { get; }

	[Inject]
	private void Construct(IProvider provider, InjectContext injectContext)
	{
		Assert.IsNotNull(provider);
		Assert.IsNotNull(injectContext);
		_provider = provider;
		_injectContext = injectContext;
	}

	protected TValue CreateInternal(List<TypeValuePair> extraArgs)
	{
		try
		{
			object instance = IProviderExtensions.GetInstance(_provider, _injectContext, extraArgs);
			if (_injectContext.Container.IsValidating && instance is ValidationMarker)
			{
				return default(TValue);
			}
			Assert.That(instance == null || TypeExtensions.DerivesFromOrEqual<TValue>(instance.GetType()));
			return (TValue)instance;
		}
		catch (Exception innerException)
		{
			throw new ZenjectException(MiscExtensions.Fmt("Error during construction of type '{0}' via {1}.Create method!", typeof(TValue), GetType()), innerException);
		}
	}

	public virtual void Validate()
	{
		IProviderExtensions.GetInstance(_provider, _injectContext, ValidationUtil.CreateDefaultArgs(ParamTypes.ToArray()));
	}

	private static void __zenInjectMethod0(object P_0, object[] P_1)
	{
		((PlaceholderFactoryBase<TValue>)P_0).Construct((IProvider)P_1[0], (InjectContext)P_1[1]);
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(PlaceholderFactoryBase<TValue>), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[1]
		{
			new InjectTypeInfo.InjectMethodInfo(__zenInjectMethod0, new InjectableInfo[2]
			{
				new InjectableInfo(optional: false, null, "provider", typeof(IProvider), null, InjectSources.Any),
				new InjectableInfo(optional: false, null, "injectContext", typeof(InjectContext), null, InjectSources.Any)
			}, "Construct")
		}, new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
