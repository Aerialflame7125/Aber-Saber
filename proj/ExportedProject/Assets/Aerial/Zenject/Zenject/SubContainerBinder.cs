using System;
using ModestTree;
using UnityEngine;

namespace Zenject;

[NoReflectionBaking]
public class SubContainerBinder
{
	private readonly BindInfo _bindInfo;

	private readonly BindStatement _bindStatement;

	private readonly object _subIdentifier;

	private readonly bool _resolveAll;

	protected IBindingFinalizer SubFinalizer
	{
		set
		{
			_bindStatement.SetFinalizer(value);
		}
	}

	public SubContainerBinder(BindInfo bindInfo, BindStatement bindStatement, object subIdentifier, bool resolveAll)
	{
		_bindInfo = bindInfo;
		_bindStatement = bindStatement;
		_subIdentifier = subIdentifier;
		_resolveAll = resolveAll;
		bindStatement.SetFinalizer(null);
	}

	public ScopeConcreteIdArgConditionCopyNonLazyBinder ByInstance(DiContainer subContainer)
	{
		SubFinalizer = new SubContainerBindingFinalizer(_bindInfo, _subIdentifier, _resolveAll, (DiContainer _) => new SubContainerCreatorByInstance(subContainer));
		return new ScopeConcreteIdArgConditionCopyNonLazyBinder(_bindInfo);
	}

	public WithKernelDefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder ByInstaller<TInstaller>() where TInstaller : InstallerBase
	{
		return ByInstaller(typeof(TInstaller));
	}

	public WithKernelDefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder ByInstaller(Type installerType)
	{
		Assert.That(TypeExtensions.DerivesFrom<InstallerBase>(installerType), "Invalid installer type given during bind command.  Expected type '{0}' to derive from 'Installer<>'", installerType);
		SubContainerCreatorBindInfo subContainerBindInfo = new SubContainerCreatorBindInfo();
		SubFinalizer = new SubContainerBindingFinalizer(_bindInfo, _subIdentifier, _resolveAll, (DiContainer container) => new SubContainerCreatorByInstaller(container, subContainerBindInfo, installerType));
		return new WithKernelDefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder(subContainerBindInfo, _bindInfo);
	}

	public WithKernelDefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder ByMethod(Action<DiContainer> installerMethod)
	{
		SubContainerCreatorBindInfo subContainerBindInfo = new SubContainerCreatorBindInfo();
		SubFinalizer = new SubContainerBindingFinalizer(_bindInfo, _subIdentifier, _resolveAll, (DiContainer container) => new SubContainerCreatorByMethod(container, subContainerBindInfo, installerMethod));
		return new WithKernelDefaultParentScopeConcreteIdArgConditionCopyNonLazyBinder(subContainerBindInfo, _bindInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabMethod(UnityEngine.Object prefab, Action<DiContainer> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidPrefab(prefab);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		SubFinalizer = new SubContainerPrefabBindingFinalizer(_bindInfo, _subIdentifier, _resolveAll, (DiContainer container) => new SubContainerCreatorByNewPrefabMethod(container, new PrefabProvider(prefab), gameObjectInfo, installerMethod));
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(_bindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabInstaller<TInstaller>(UnityEngine.Object prefab) where TInstaller : InstallerBase
	{
		return ByNewPrefabInstaller(prefab, typeof(TInstaller));
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabInstaller(UnityEngine.Object prefab, Type installerType)
	{
		Assert.That(TypeExtensions.DerivesFrom<InstallerBase>(installerType), "Invalid installer type given during bind command.  Expected type '{0}' to derive from 'Installer<>'", installerType);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		SubFinalizer = new SubContainerPrefabBindingFinalizer(_bindInfo, _subIdentifier, _resolveAll, (DiContainer container) => new SubContainerCreatorByNewPrefabInstaller(container, new PrefabProvider(prefab), gameObjectInfo, installerType, _bindInfo.Arguments));
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(_bindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResourceMethod(string resourcePath, Action<DiContainer> installerMethod)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		SubFinalizer = new SubContainerPrefabBindingFinalizer(_bindInfo, _subIdentifier, _resolveAll, (DiContainer container) => new SubContainerCreatorByNewPrefabMethod(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerMethod));
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(_bindInfo, gameObjectInfo);
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
		SubFinalizer = new SubContainerPrefabBindingFinalizer(_bindInfo, _subIdentifier, _resolveAll, (DiContainer container) => new SubContainerCreatorByNewPrefabInstaller(container, new PrefabProviderResource(resourcePath), gameObjectInfo, installerType, _bindInfo.Arguments));
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(_bindInfo, gameObjectInfo);
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
		SubFinalizer = new SubContainerPrefabBindingFinalizer(_bindInfo, _subIdentifier, _resolveAll, (DiContainer container) => new SubContainerCreatorByNewPrefab(container, new PrefabProvider(prefab), gameObjectInfo));
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(_bindInfo, gameObjectInfo);
	}

	public NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder ByNewPrefabResource(string resourcePath)
	{
		Zenject.BindingUtil.AssertIsValidResourcePath(resourcePath);
		GameObjectCreationParameters gameObjectInfo = new GameObjectCreationParameters();
		SubFinalizer = new SubContainerPrefabBindingFinalizer(_bindInfo, _subIdentifier, _resolveAll, (DiContainer container) => new SubContainerCreatorByNewPrefab(container, new PrefabProviderResource(resourcePath), gameObjectInfo));
		return new NameTransformScopeConcreteIdArgConditionCopyNonLazyBinder(_bindInfo, gameObjectInfo);
	}
}
