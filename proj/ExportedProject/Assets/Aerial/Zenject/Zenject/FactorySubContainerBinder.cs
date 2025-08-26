using System;
using UnityEngine;

namespace Zenject;

[NoReflectionBaking]
public class FactorySubContainerBinder<TContract> : FactorySubContainerBinderBase<TContract>
{
	public FactorySubContainerBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
		: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
	{
	}

	public DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(Action<DiContainer> installerMethod)
	{
		SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByMethod(container, subcontainerBindInfo, installerMethod), resolveAll: false);
		return new DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder(subcontainerBindInfo, base.BindInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, Action<DiContainer> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidPrefab(prefab);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, Action<DiContainer> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}

	[Obsolete("ByNewPrefab has been renamed to ByNewContextPrefab to avoid confusion with ByNewPrefabInstaller and ByNewPrefabMethod")]
	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefab(UnityEngine.Object prefab)
	{
		return ByNewContextPrefab(prefab);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewContextPrefab(UnityEngine.Object prefab)
	{
		Zenject.BindingUtil.AssertIsValidPrefab(prefab);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefab(container, new PrefabProvider(prefab), gameObjectInfo), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResource(string resourcePath)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefab(container, new PrefabProviderResource(resourcePath), gameObjectInfo), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}
}
[NoReflectionBaking]
public class FactorySubContainerBinder<TParam1, TContract> : FactorySubContainerBinderWithParams<TContract>
{
	public FactorySubContainerBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
		: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
	{
	}

	public DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(Action<DiContainer, TParam1> installerMethod)
	{
		SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByMethod<TParam1>(container, subcontainerBindInfo, installerMethod), resolveAll: false);
		return new DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder(subcontainerBindInfo, base.BindInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, Action<DiContainer, TParam1> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidPrefab(prefab);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1>(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, Action<DiContainer, TParam1> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1>(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}
}
[NoReflectionBaking]
public class FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10, TContract> : FactorySubContainerBinderWithParams<TContract>
{
	public FactorySubContainerBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
		: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
	{
	}

	public DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10> installerMethod)
	{
		SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10>(container, subcontainerBindInfo, installerMethod), resolveAll: false);
		return new DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder(subcontainerBindInfo, base.BindInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidPrefab(prefab);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10>(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TParam7, TParam8, TParam9, TParam10>(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}
}
[NoReflectionBaking]
public class FactorySubContainerBinder<TParam1, TParam2, TContract> : FactorySubContainerBinderWithParams<TContract>
{
	public FactorySubContainerBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
		: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
	{
	}

	public DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(Action<DiContainer, TParam1, TParam2> installerMethod)
	{
		SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByMethod<TParam1, TParam2>(container, subcontainerBindInfo, installerMethod), resolveAll: false);
		return new DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder(subcontainerBindInfo, base.BindInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, Action<DiContainer, TParam1, TParam2> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidPrefab(prefab);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2>(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, Action<DiContainer, TParam1, TParam2> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2>(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}
}
[NoReflectionBaking]
public class FactorySubContainerBinder<TParam1, TParam2, TParam3, TContract> : FactorySubContainerBinderWithParams<TContract>
{
	public FactorySubContainerBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
		: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
	{
	}

	public DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(Action<DiContainer, TParam1, TParam2, TParam3> installerMethod)
	{
		SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByMethod<TParam1, TParam2, TParam3>(container, subcontainerBindInfo, installerMethod), resolveAll: false);
		return new DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder(subcontainerBindInfo, base.BindInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, Action<DiContainer, TParam1, TParam2, TParam3> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidPrefab(prefab);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3>(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, Action<DiContainer, TParam1, TParam2, TParam3> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3>(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}
}
[NoReflectionBaking]
public class FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TContract> : FactorySubContainerBinderWithParams<TContract>
{
	public FactorySubContainerBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
		: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
	{
	}

	public DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(Action<DiContainer, TParam1, TParam2, TParam3, TParam4> installerMethod)
	{
		SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByMethod<TParam1, TParam2, TParam3, TParam4>(container, subcontainerBindInfo, installerMethod), resolveAll: false);
		return new DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder(subcontainerBindInfo, base.BindInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, Action<DiContainer, TParam1, TParam2, TParam3, TParam4> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidPrefab(prefab);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4>(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, Action<DiContainer, TParam1, TParam2, TParam3, TParam4> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4>(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}
}
[NoReflectionBaking]
public class FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TContract> : FactorySubContainerBinderWithParams<TContract>
{
	public FactorySubContainerBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
		: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
	{
	}

	public DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5> installerMethod)
	{
		SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByMethod<TParam1, TParam2, TParam3, TParam4, TParam5>(container, subcontainerBindInfo, installerMethod), resolveAll: false);
		return new DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder(subcontainerBindInfo, base.BindInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidPrefab(prefab);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4, TParam5>(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4, TParam5>(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}
}
[NoReflectionBaking]
public class FactorySubContainerBinder<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TContract> : FactorySubContainerBinderWithParams<TContract>
{
	public FactorySubContainerBinder(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
		: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
	{
	}

	public DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> installerMethod)
	{
		SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(container, subcontainerBindInfo, installerMethod), resolveAll: false);
		return new DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder(subcontainerBindInfo, base.BindInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidPrefab(prefab);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, Action<DiContainer, TParam1, TParam2, TParam3, TParam4, TParam5, TParam6> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6>(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}
}
