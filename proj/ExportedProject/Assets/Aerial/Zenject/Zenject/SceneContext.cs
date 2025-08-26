using System;
using System.Collections.Generic;
using System.Linq;
using ModestTree;
using ModestTree.Util;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject.Internal;

namespace Zenject;

public class SceneContext : RunnableContext
{
	public static Action<DiContainer> ExtraBindingsInstallMethod;

	public static Action<DiContainer> ExtraBindingsLateInstallMethod;

	public static IEnumerable<DiContainer> ParentContainers;

	[SerializeField]
	[FormerlySerializedAs("ParentNewObjectsUnderRoot")]
	[Tooltip("When true, objects that are created at runtime will be parented to the SceneContext")]
	private bool _parentNewObjectsUnderRoot;

	[Tooltip("Optional contract names for this SceneContext, allowing contexts in subsequently loaded scenes to depend on it and be parented to it, and also for previously loaded decorators to be included")]
	[SerializeField]
	private List<string> _contractNames = new List<string>();

	[SerializeField]
	[Tooltip("Optional contract names of SceneContexts in previously loaded scenes that this context depends on and to which it should be parented")]
	private List<string> _parentContractNames = new List<string>();

	private DiContainer _container;

	private readonly List<SceneDecoratorContext> _decoratorContexts = new List<SceneDecoratorContext>();

	private bool _hasInstalled;

	private bool _hasResolved;

	public override DiContainer Container => _container;

	public bool HasResolved => _hasResolved;

	public bool HasInstalled => _hasInstalled;

	public bool IsValidating => false;

	public IEnumerable<string> ContractNames
	{
		get
		{
			return _contractNames;
		}
		set
		{
			_contractNames.Clear();
			_contractNames.AddRange(value);
		}
	}

	public IEnumerable<string> ParentContractNames
	{
		get
		{
			List<string> list = new List<string>();
			list.AddRange(_parentContractNames);
			return list;
		}
		set
		{
			_parentContractNames = value.ToList();
		}
	}

	public bool ParentNewObjectsUnderRoot
	{
		get
		{
			return _parentNewObjectsUnderRoot;
		}
		set
		{
			_parentNewObjectsUnderRoot = value;
		}
	}

	public event Action PreInstall;

	public event Action PostInstall;

	public event Action PreResolve;

	public event Action PostResolve;

	public void Awake()
	{
		Initialize();
	}

	protected override void RunInternal()
	{
		ProjectContext.Instance.EnsureIsInitialized();
		Install();
		Resolve();
	}

	public override IEnumerable<GameObject> GetRootGameObjects()
	{
		return ZenUtilInternal.GetRootGameObjects(base.gameObject.scene);
	}

	private IEnumerable<DiContainer> GetParentContainers()
	{
		IEnumerable<string> parentContractNames = ParentContractNames;
		if (LinqExtensions.IsEmpty(parentContractNames))
		{
			if (ParentContainers != null)
			{
				IEnumerable<DiContainer> parentContainers = ParentContainers;
				ParentContainers = null;
				return parentContainers;
			}
			return new DiContainer[1] { ProjectContext.Instance.Container };
		}
		Assert.IsNull(ParentContainers, "Scene cannot have both a parent scene context name set and also an explicit parent container given");
		List<DiContainer> list = (from x in LinqExtensions.Except(UnityUtil.AllLoadedScenes, base.gameObject.scene).SelectMany((Scene scene) => scene.GetRootGameObjects()).SelectMany((GameObject root) => root.GetComponentsInChildren<SceneContext>())
			where x.ContractNames.Where((string x) => parentContractNames.Contains(x)).Any()
			select x.Container).ToList();
		if (!list.Any())
		{
			throw Assert.CreateException("SceneContext on object {0} of scene {1} requires at least one of contracts '{2}', but none of the loaded SceneContexts implements that contract.", base.gameObject.name, base.gameObject.scene.name, MiscExtensions.Join(parentContractNames, ", "));
		}
		return list;
	}

	private List<SceneDecoratorContext> LookupDecoratorContexts()
	{
		if (LinqExtensions.IsEmpty(_contractNames))
		{
			return new List<SceneDecoratorContext>();
		}
		return (from decoratorContext in LinqExtensions.Except(UnityUtil.AllLoadedScenes, base.gameObject.scene).SelectMany((Scene scene) => scene.GetRootGameObjects()).SelectMany((GameObject root) => root.GetComponentsInChildren<SceneDecoratorContext>())
			where _contractNames.Contains(decoratorContext.DecoratedContractName)
			select decoratorContext).ToList();
	}

	public void Install()
	{
		Assert.That(!IsValidating);
		Assert.That(!_hasInstalled);
		_hasInstalled = true;
		Assert.IsNull(_container);
		IEnumerable<DiContainer> parents = GetParentContainers();
		Assert.That(!LinqExtensions.IsEmpty(parents));
		Assert.That(parents.All((DiContainer x) => x.IsValidating == parents.First().IsValidating));
		_container = new DiContainer(parents, parents.First().IsValidating);
		if (this.PreInstall != null)
		{
			this.PreInstall();
		}
		Assert.That(LinqExtensions.IsEmpty(_decoratorContexts));
		_decoratorContexts.AddRange(LookupDecoratorContexts());
		if (_parentNewObjectsUnderRoot)
		{
			_container.DefaultParent = base.transform;
		}
		else
		{
			_container.DefaultParent = null;
		}
		List<MonoBehaviour> list = new List<MonoBehaviour>();
		GetInjectableMonoBehaviours(list);
		foreach (MonoBehaviour item in list)
		{
			_container.QueueForInject(item);
		}
		foreach (SceneDecoratorContext decoratorContext in _decoratorContexts)
		{
			decoratorContext.Initialize(_container);
		}
		_container.IsInstalling = true;
		try
		{
			InstallBindings(list);
		}
		finally
		{
			_container.IsInstalling = false;
		}
		if (this.PostInstall != null)
		{
			this.PostInstall();
		}
	}

	public void Resolve()
	{
		if (this.PreResolve != null)
		{
			this.PreResolve();
		}
		Assert.That(_hasInstalled);
		Assert.That(!_hasResolved);
		_hasResolved = true;
		_container.ResolveRoots();
		if (this.PostResolve != null)
		{
			this.PostResolve();
		}
	}

	private void InstallBindings(List<MonoBehaviour> injectableMonoBehaviours)
	{
		_container.Bind(typeof(Context), typeof(SceneContext)).To<SceneContext>().FromInstance(this);
		_container.BindInterfacesTo<SceneContextRegistryAdderAndRemover>().AsSingle();
		_container.BindExecutionOrder<SceneContextRegistryAdderAndRemover>(-1);
		foreach (SceneDecoratorContext decoratorContext in _decoratorContexts)
		{
			decoratorContext.InstallDecoratorSceneBindings();
		}
		InstallSceneBindings(injectableMonoBehaviours);
		_container.Bind(typeof(SceneKernel), typeof(MonoKernel)).To<SceneKernel>().FromNewComponentOn(base.gameObject)
			.AsSingle()
			.NonLazy();
		_container.Bind<ZenjectSceneLoader>().AsSingle();
		if (ExtraBindingsInstallMethod != null)
		{
			ExtraBindingsInstallMethod(_container);
			ExtraBindingsInstallMethod = null;
		}
		foreach (SceneDecoratorContext decoratorContext2 in _decoratorContexts)
		{
			decoratorContext2.InstallDecoratorInstallers();
		}
		InstallInstallers();
		foreach (SceneDecoratorContext decoratorContext3 in _decoratorContexts)
		{
			decoratorContext3.InstallLateDecoratorInstallers();
		}
		if (ExtraBindingsLateInstallMethod != null)
		{
			ExtraBindingsLateInstallMethod(_container);
			ExtraBindingsLateInstallMethod = null;
		}
	}

	protected override void GetInjectableMonoBehaviours(List<MonoBehaviour> monoBehaviours)
	{
		Scene scene = base.gameObject.scene;
		ZenUtilInternal.AddStateMachineBehaviourAutoInjectersInScene(scene);
		ZenUtilInternal.GetInjectableMonoBehavioursInScene(scene, monoBehaviours);
	}

	public static SceneContext Create()
	{
		return RunnableContext.CreateComponent<SceneContext>(new GameObject("SceneContext"));
	}

	[Zenject.Internal.Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(SceneContext), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
