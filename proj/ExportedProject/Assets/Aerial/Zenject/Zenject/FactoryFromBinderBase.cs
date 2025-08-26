using System;
using System.Collections.Generic;
using UnityEngine;

namespace Zenject;

[NoReflectionBaking]
public class FactoryFromBinderBase : ScopeConcreteIdArgConditionCopyNonLazyBinder
{
	internal DiContainer BindContainer { get; private set; }

	protected FactoryBindInfo FactoryBindInfo { get; private set; }

	internal Func<DiContainer, IProvider> ProviderFunc
	{
		get
		{
			return FactoryBindInfo.ProviderFunc;
		}
		set
		{
			FactoryBindInfo.ProviderFunc = value;
		}
	}

	protected Type ContractType { get; private set; }

	public IEnumerable<Type> AllParentTypes
	{
		get
		{
			yield return ContractType;
			foreach (Type toType in base.BindInfo.ToTypes)
			{
				yield return toType;
			}
		}
	}

	public FactoryFromBinderBase(DiContainer bindContainer, Type contractType, BindInfo bindInfo, FactoryBindInfo factoryBindInfo)
		: base(bindInfo)
	{
		FactoryBindInfo = factoryBindInfo;
		BindContainer = bindContainer;
		ContractType = contractType;
		factoryBindInfo.ProviderFunc = (DiContainer container) => new TransientProvider(ContractType, container, base.BindInfo.Arguments, base.BindInfo.ContextInfo, base.BindInfo.ConcreteIdentifier, base.BindInfo.InstantiatedCallback);
	}

	public ConditionCopyNonLazyBinder FromNew()
	{
		Zenject.BindingUtil.AssertIsNotComponent(ContractType);
		Zenject.BindingUtil.AssertIsNotAbstract(ContractType);
		return this;
	}

	public ConditionCopyNonLazyBinder FromResolve()
	{
		return FromResolve(null);
	}

	public ConditionCopyNonLazyBinder FromInstance(object instance)
	{
		Zenject.BindingUtil.AssertInstanceDerivesFromOrEqual(instance, AllParentTypes);
		ProviderFunc = (DiContainer container) => new InstanceProvider(ContractType, instance, container);
		return this;
	}

	public ConditionCopyNonLazyBinder FromResolve(object subIdentifier)
	{
		ProviderFunc = (DiContainer container) => new ResolveProvider(ContractType, container, subIdentifier, isOptional: false, InjectSources.Any, matchAll: false);
		return this;
	}

	internal ConcreteBinderGeneric<T> CreateIFactoryBinder<T>(out Guid factoryId)
	{
		factoryId = Guid.NewGuid();
		return BindContainer.BindNoFlush<T>().WithId(factoryId);
	}

	public ConditionCopyNonLazyBinder FromComponentOn(GameObject gameObject)
	{
		Zenject.BindingUtil.AssertIsValidGameObject(gameObject);
		Zenject.BindingUtil.AssertIsComponent(ContractType);
		Zenject.BindingUtil.AssertIsNotAbstract(ContractType);
		ProviderFunc = (DiContainer container) => new GetFromGameObjectComponentProvider(ContractType, gameObject, matchSingle: true);
		return this;
	}

	public ConditionCopyNonLazyBinder FromComponentOn(Func<InjectContext, GameObject> gameObjectGetter)
	{
		Zenject.BindingUtil.AssertIsComponent(ContractType);
		Zenject.BindingUtil.AssertIsNotAbstract(ContractType);
		ProviderFunc = (DiContainer container) => new GetFromGameObjectGetterComponentProvider(ContractType, gameObjectGetter, matchSingle: true);
		return this;
	}

	public ConditionCopyNonLazyBinder FromComponentOnRoot()
	{
		return FromComponentOn((InjectContext ctx) => BindContainer.Resolve<Context>().gameObject);
	}

	public ConditionCopyNonLazyBinder FromNewComponentOn(GameObject gameObject)
	{
		Zenject.BindingUtil.AssertIsValidGameObject(gameObject);
		Zenject.BindingUtil.AssertIsComponent(ContractType);
		Zenject.BindingUtil.AssertIsNotAbstract(ContractType);
		ProviderFunc = (DiContainer container) => new AddToExistingGameObjectComponentProvider(gameObject, container, ContractType, new List<TypeValuePair>(), base.BindInfo.ConcreteIdentifier, base.BindInfo.InstantiatedCallback);
		return this;
	}

	public ConditionCopyNonLazyBinder FromNewComponentOn(Func<InjectContext, GameObject> gameObjectGetter)
	{
		Zenject.BindingUtil.AssertIsComponent(ContractType);
		Zenject.BindingUtil.AssertIsNotAbstract(ContractType);
		ProviderFunc = (DiContainer container) => new AddToExistingGameObjectComponentProviderGetter(gameObjectGetter, container, ContractType, new List<TypeValuePair>(), base.BindInfo.ConcreteIdentifier, base.BindInfo.InstantiatedCallback);
		return this;
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder FromNewComponentOnNewGameObject()
	{
		Zenject.BindingUtil.AssertIsComponent(ContractType);
		Zenject.BindingUtil.AssertIsNotAbstract(ContractType);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		ProviderFunc = (DiContainer container) => new AddToNewGameObjectComponentProvider(container, ContractType, new List<TypeValuePair>(), gameObjectInfo, base.BindInfo.ConcreteIdentifier, base.BindInfo.InstantiatedCallback);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder FromNewComponentOnNewPrefab(UnityEngine.Object prefab)
	{
		Zenject.BindingUtil.AssertIsValidPrefab(prefab);
		Zenject.BindingUtil.AssertIsComponent(ContractType);
		Zenject.BindingUtil.AssertIsNotAbstract(ContractType);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		ProviderFunc = (DiContainer container) => new InstantiateOnPrefabComponentProvider(ContractType, new PrefabInstantiator(container, gameObjectInfo, ContractType, new List<TypeValuePair>(), new PrefabProvider(prefab), base.BindInfo.InstantiatedCallback));
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentInNewPrefab(UnityEngine.Object prefab)
	{
		Zenject.BindingUtil.AssertIsValidPrefab(prefab);
		Zenject.BindingUtil.AssertIsInterfaceOrComponent(ContractType);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		ProviderFunc = (DiContainer container) => new GetFromPrefabComponentProvider(ContractType, new PrefabInstantiator(container, gameObjectInfo, ContractType, new List<TypeValuePair>(), new PrefabProvider(prefab), base.BindInfo.InstantiatedCallback), matchSingle: true);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder FromComponentInNewPrefabResource(string resourcePath)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		Zenject.BindingUtil.AssertIsInterfaceOrComponent(ContractType);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		ProviderFunc = (DiContainer container) => new GetFromPrefabComponentProvider(ContractType, new PrefabInstantiator(container, gameObjectInfo, ContractType, new List<TypeValuePair>(), new PrefabProviderResource(resourcePath), base.BindInfo.InstantiatedCallback), matchSingle: true);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder FromNewComponentOnNewPrefabResource(string resourcePath)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		Zenject.BindingUtil.AssertIsComponent(ContractType);
		Zenject.BindingUtil.AssertIsNotAbstract(ContractType);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		ProviderFunc = (DiContainer container) => new InstantiateOnPrefabComponentProvider(ContractType, new PrefabInstantiator(container, gameObjectInfo, ContractType, new List<TypeValuePair>(), new PrefabProviderResource(resourcePath), base.BindInfo.InstantiatedCallback));
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}

	public ConditionCopyNonLazyBinder FromNewScriptableObjectResource(string resourcePath)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		Zenject.BindingUtil.AssertIsInterfaceOrScriptableObject(ContractType);
		ProviderFunc = (DiContainer container) => new ScriptableObjectResourceProvider(resourcePath, ContractType, container, new List<TypeValuePair>(), createNew: true, null, base.BindInfo.InstantiatedCallback);
		return this;
	}

	public ConditionCopyNonLazyBinder FromScriptableObjectResource(string resourcePath)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		Zenject.BindingUtil.AssertIsInterfaceOrScriptableObject(ContractType);
		ProviderFunc = (DiContainer container) => new ScriptableObjectResourceProvider(resourcePath, ContractType, container, new List<TypeValuePair>(), createNew: false, null, base.BindInfo.InstantiatedCallback);
		return this;
	}

	public ConditionCopyNonLazyBinder FromResource(string resourcePath)
	{
		Zenject.BindingUtil.AssertDerivesFromUnityObject(ContractType);
		ProviderFunc = (DiContainer container) => new ResourceProvider(resourcePath, ContractType, matchSingle: true);
		return this;
	}
}
