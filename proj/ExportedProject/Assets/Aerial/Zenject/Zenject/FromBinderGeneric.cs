using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using UnityEngine;

namespace Zenject;

[NoReflectionBaking]
public class FromBinderGeneric<TContract> : FromBinder
{
	public FromBinderGeneric(DiContainer bindContainer, BindInfo bindInfo, BindStatement bindStatement)
		: base(bindContainer, bindInfo, bindStatement)
	{
		Zenject.BindingUtil.AssertIsDerivedFromTypes(typeof(TContract), base.BindInfo.ContractTypes);
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromFactory<TFactory>() where TFactory : IFactory<TContract>
	{
		return FromIFactory(delegate(ConcreteBinderGeneric<IFactory<TContract>> x)
		{
			x.To<TFactory>().AsCached();
		});
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromIFactory(Action<ConcreteBinderGeneric<IFactory<TContract>>> factoryBindGenerator)
	{
		return FromIFactoryBase(factoryBindGenerator);
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromMethod(Func<TContract> method)
	{
		return FromMethodBase((InjectContext ctx) => method());
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromMethod(Func<InjectContext, TContract> method)
	{
		return FromMethodBase(method);
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromMethodMultiple(Func<InjectContext, IEnumerable<TContract>> method)
	{
		return FromMethodMultipleBase(method);
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveGetter<TObj>(Func<TObj, TContract> method)
	{
		return FromResolveGetter(null, method);
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveGetter<TObj>(object identifier, Func<TObj, TContract> method)
	{
		return FromResolveGetter(identifier, method, InjectSources.Any);
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveGetter<TObj>(object identifier, Func<TObj, TContract> method, InjectSources source)
	{
		return FromResolveGetterBase(identifier, method, source, matchMultiple: false);
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveAllGetter<TObj>(Func<TObj, TContract> method)
	{
		return FromResolveAllGetter(null, method);
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveAllGetter<TObj>(object identifier, Func<TObj, TContract> method)
	{
		return FromResolveAllGetter(identifier, method, InjectSources.Any);
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromResolveAllGetter<TObj>(object identifier, Func<TObj, TContract> method, InjectSources source)
	{
		return FromResolveGetterBase(identifier, method, source, matchMultiple: true);
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromInstance(TContract instance)
	{
		return FromInstanceBase(instance);
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentInChildren(bool includeInactive = true)
	{
		Zenject.BindingUtil.AssertIsInterfaceOrComponent(base.AllParentTypes);
		return FromMethodMultiple(delegate(InjectContext ctx)
		{
			Assert.That(TypeExtensions.DerivesFromOrEqual<MonoBehaviour>(ctx.ObjectType), "Cannot use FromComponentInChildren to inject data into non monobehaviours!");
			Assert.IsNotNull(ctx.ObjectInstance);
			TContract componentInChildren = ((MonoBehaviour)ctx.ObjectInstance).GetComponentInChildren<TContract>(includeInactive);
			if (componentInChildren != null)
			{
				return new TContract[1] { componentInChildren };
			}
			Assert.That(ctx.Optional, "Could not find component '{0}' through FromComponentInChildren binding", typeof(TContract));
			return Enumerable.Empty<TContract>();
		});
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentsInChildren(Func<TContract, bool> predicate, bool includeInactive = true)
	{
		return FromComponentsInChildren(excludeSelf: false, predicate, includeInactive);
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentsInChildren(bool excludeSelf = false, Func<TContract, bool> predicate = null, bool includeInactive = true)
	{
		Zenject.BindingUtil.AssertIsInterfaceOrComponent(base.AllParentTypes);
		return FromMethodMultiple(delegate(InjectContext ctx)
		{
			Assert.That(TypeExtensions.DerivesFromOrEqual<MonoBehaviour>(ctx.ObjectType), "Cannot use FromComponentsInChildren to inject data into non monobehaviours!");
			Assert.IsNotNull(ctx.ObjectInstance);
			IEnumerable<TContract> enumerable = from x in ((MonoBehaviour)ctx.ObjectInstance).GetComponentsInChildren<TContract>(includeInactive)
				where !object.ReferenceEquals(x, ctx.ObjectInstance)
				select x;
			if (excludeSelf)
			{
				enumerable = enumerable.Where((TContract x) => (x as Component).gameObject != (ctx.ObjectInstance as Component).gameObject);
			}
			if (predicate != null)
			{
				enumerable = enumerable.Where(predicate);
			}
			return enumerable;
		});
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentInParents(bool excludeSelf = false, bool includeInactive = true)
	{
		Zenject.BindingUtil.AssertIsInterfaceOrComponent(base.AllParentTypes);
		return FromMethodMultiple(delegate(InjectContext ctx)
		{
			Assert.That(TypeExtensions.DerivesFromOrEqual<MonoBehaviour>(ctx.ObjectType), "Cannot use FromComponentInParents to inject data into non monobehaviours!");
			Assert.IsNotNull(ctx.ObjectInstance);
			IEnumerable<TContract> source = from x in ((MonoBehaviour)ctx.ObjectInstance).GetComponentsInParent<TContract>(includeInactive)
				where !object.ReferenceEquals(x, ctx.ObjectInstance)
				select x;
			if (excludeSelf)
			{
				source = source.Where((TContract x) => (x as Component).gameObject != (ctx.ObjectInstance as Component).gameObject);
			}
			TContract val = source.FirstOrDefault();
			if (val != null)
			{
				return new TContract[1] { val };
			}
			Assert.That(ctx.Optional, "Could not find component '{0}' through FromComponentInParents binding", typeof(TContract));
			return Enumerable.Empty<TContract>();
		});
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentsInParents(bool excludeSelf = false, bool includeInactive = true)
	{
		Zenject.BindingUtil.AssertIsInterfaceOrComponent(base.AllParentTypes);
		return FromMethodMultiple(delegate(InjectContext ctx)
		{
			Assert.That(TypeExtensions.DerivesFromOrEqual<MonoBehaviour>(ctx.ObjectType), "Cannot use FromComponentInParents to inject data into non monobehaviours!");
			Assert.IsNotNull(ctx.ObjectInstance);
			IEnumerable<TContract> enumerable = from x in ((MonoBehaviour)ctx.ObjectInstance).GetComponentsInParent<TContract>(includeInactive)
				where !object.ReferenceEquals(x, ctx.ObjectInstance)
				select x;
			if (excludeSelf)
			{
				enumerable = enumerable.Where((TContract x) => (x as Component).gameObject != (ctx.ObjectInstance as Component).gameObject);
			}
			return enumerable;
		});
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentSibling()
	{
		Zenject.BindingUtil.AssertIsInterfaceOrComponent(base.AllParentTypes);
		return FromMethodMultiple(delegate(InjectContext ctx)
		{
			Assert.That(TypeExtensions.DerivesFromOrEqual<MonoBehaviour>(ctx.ObjectType), "Cannot use FromComponentSibling to inject data into non monobehaviours!");
			Assert.IsNotNull(ctx.ObjectInstance);
			TContract component = ((MonoBehaviour)ctx.ObjectInstance).GetComponent<TContract>();
			if (component != null)
			{
				return new TContract[1] { component };
			}
			Assert.That(ctx.Optional, "Could not find component '{0}' through FromComponentSibling binding", typeof(TContract));
			return Enumerable.Empty<TContract>();
		});
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentsSibling()
	{
		Zenject.BindingUtil.AssertIsInterfaceOrComponent(base.AllParentTypes);
		return FromMethodMultiple(delegate(InjectContext ctx)
		{
			Assert.That(TypeExtensions.DerivesFromOrEqual<MonoBehaviour>(ctx.ObjectType), "Cannot use FromComponentSibling to inject data into non monobehaviours!");
			Assert.IsNotNull(ctx.ObjectInstance);
			return from x in ((MonoBehaviour)ctx.ObjectInstance).GetComponents<TContract>()
				where !object.ReferenceEquals(x, ctx.ObjectInstance)
				select x;
		});
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentInHierarchy(bool includeInactive = true)
	{
		Zenject.BindingUtil.AssertIsInterfaceOrComponent(base.AllParentTypes);
		return FromMethodMultiple(delegate(InjectContext ctx)
		{
			TContract val = (from x in base.BindContainer.Resolve<Context>().GetRootGameObjects()
				select x.GetComponentInChildren<TContract>(includeInactive) into x
				where x != null
				select x).FirstOrDefault();
			if (val != null)
			{
				return new TContract[1] { val };
			}
			Assert.That(ctx.Optional, "Could not find component '{0}' through FromComponentInHierarchy binding", typeof(TContract));
			return Enumerable.Empty<TContract>();
		});
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentsInHierarchy(Func<TContract, bool> predicate = null, bool includeInactive = true)
	{
		Zenject.BindingUtil.AssertIsInterfaceOrComponent(base.AllParentTypes);
		return FromMethodMultiple(delegate(InjectContext ctx)
		{
			IEnumerable<TContract> enumerable = from x in base.BindContainer.Resolve<Context>().GetRootGameObjects().SelectMany((GameObject x) => x.GetComponentsInChildren<TContract>(includeInactive))
				where !object.ReferenceEquals(x, ctx.ObjectInstance)
				select x;
			if (predicate != null)
			{
				enumerable = enumerable.Where(predicate);
			}
			return enumerable;
		});
	}
}
