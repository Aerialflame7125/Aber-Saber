using System;
using ModestTree;
using UnityEngine;

namespace Zenject;

[NoReflectionBaking]
public class FactorySubContainerBinderBase<TContract>
{
	protected DiContainer BindContainer { get; private set; }

	protected FactoryBindInfo FactoryBindInfo { get; private set; }

	protected Func<DiContainer, IProvider> ProviderFunc
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

	protected BindInfo BindInfo { get; private set; }

	protected object SubIdentifier { get; private set; }

	protected Type ContractType => typeof(TContract);

	public FactorySubContainerBinderBase(DiContainer bindContainer, BindInfo bindInfo, FactoryBindInfo factoryBindInfo, object subIdentifier)
	{
		FactoryBindInfo = factoryBindInfo;
		SubIdentifier = subIdentifier;
		BindInfo = bindInfo;
		BindContainer = bindContainer;
		factoryBindInfo.ProviderFunc = null;
	}

	public DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder ByInstaller<TInstaller>() where TInstaller : InstallerBase
	{
		return ByInstaller(typeof(TInstaller));
	}

	public DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder ByInstaller(Type installerType)
	{
		Assert.That(TypeExtensions.DerivesFrom<InstallerBase>(installerType), "Invalid installer type given during bind command.  Expected type '{0}' to derive from 'Installer<>'", installerType);
		SubContainerCreatorBindInfo subcontainerBindInfo = new SubContainerCreatorBindInfo();
		ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(ContractType, SubIdentifier, new SubContainerCreatorByInstaller(container, subcontainerBindInfo, installerType, BindInfo.Arguments), resolveAll: false);
		return new DefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder(subcontainerBindInfo, BindInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabInstaller<TInstaller>(UnityEngine.Object prefab) where TInstaller : InstallerBase
	{
		return ByNewPrefabInstaller(prefab, typeof(TInstaller));
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabInstaller(UnityEngine.Object prefab, Type installerType)
	{
		Assert.That(TypeExtensions.DerivesFrom<InstallerBase>(installerType), "Invalid installer type given during bind command.  Expected type '{0}' to derive from 'Installer<>'", installerType);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(ContractType, SubIdentifier, new SubContainerCreatorByNewPrefabInstaller(container, new PrefabProvider(prefab), gameObjectInfo, installerType, BindInfo.Arguments), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(BindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceInstaller<TInstaller>(string resourcePath) where TInstaller : InstallerBase
	{
		return ByNewPrefabResourceInstaller(resourcePath, typeof(TInstaller));
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceInstaller(string resourcePath, Type installerType)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		Assert.That(TypeExtensions.DerivesFrom<InstallerBase>(installerType), "Invalid installer type given during bind command.  Expected type '{0}' to derive from 'Installer<>'", installerType);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		ProviderFunc = (DiContainer container) => new SubContainerDependencyProvider(ContractType, SubIdentifier, new SubContainerCreatorByNewPrefabInstaller(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerType, BindInfo.Arguments), resolveAll: false);
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(BindInfo, gameObjectInfo);
	}
}
