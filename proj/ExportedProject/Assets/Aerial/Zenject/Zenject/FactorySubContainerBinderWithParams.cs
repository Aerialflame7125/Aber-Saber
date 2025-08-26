using System;
using ModestTree;
using UnityEngine;

namespace Zenject;

[NoReflectionBaking]
public class FactorySubContainerBinderWithParams<TContract> : FactorySubContainerBinderBase<TContract>
{
	public FactorySubContainerBinderWithParams(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
		: base(bindContainer, bindInfo, factoryBindInfo, subIdentifier)
	{
	}

	[Obsolete("ByNewPrefab has been renamed to ByNewContextPrefab to avoid confusion with ByNewPrefabInstaller and ByNewPrefabMethod")]
	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefab(Type installerType, UnityEngine.Object prefab)
	{
		return ByNewContextPrefab(installerType, prefab);
	}

	[Obsolete("ByNewPrefab has been renamed to ByNewContextPrefab to avoid confusion with ByNewPrefabInstaller and ByNewPrefabMethod")]
	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefab<TInstaller>(UnityEngine.Object prefab) where TInstaller : IInstaller
	{
		return ByNewContextPrefab<TInstaller>(prefab);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewContextPrefab<TInstaller>(UnityEngine.Object prefab) where TInstaller : IInstaller
	{
		return ByNewContextPrefab(typeof(TInstaller), prefab);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewContextPrefab(Type installerType, UnityEngine.Object prefab)
	{
		Zenject.BindingUtil.AssertIsValidPrefab(prefab);
		Assert.That(TypeExtensions.DerivesFrom<MonoInstaller>(installerType), "Invalid installer type given during bind command.  Expected type '{0}' to derive from 'MonoInstaller'", installerType);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabWithParams(installerType, container, new PrefabProvider(prefab), gameObjectInfo), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResource<TInstaller>(string resourcePath) where TInstaller : IInstaller
	{
		return ByNewPrefabResource(typeof(TInstaller), resourcePath);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResource(Type installerType, string resourcePath)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		base.ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(base.ContractType, base.SubIdentifier, new SubContainerCreatorByNewPrefabWithParams(installerType, container, new PrefabProviderResource(resourcePath), gameObjectInfo), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(base.BindInfo, gameObjectInfo);
	}
}
