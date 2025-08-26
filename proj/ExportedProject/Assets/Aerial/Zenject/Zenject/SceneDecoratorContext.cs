using System;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Zenject.Internal;

namespace Zenject;

public class SceneDecoratorContext : Context
{
	[SerializeField]
	private List<MonoInstaller> _lateInstallers = new List<MonoInstaller>();

	[SerializeField]
	private List<MonoInstaller> _lateInstallerPrefabs = new List<MonoInstaller>();

	[SerializeField]
	private List<ScriptableObjectInstaller> _lateScriptableObjectInstallers = new List<ScriptableObjectInstaller>();

	[SerializeField]
	[FormerlySerializedAs("SceneName")]
	private string _decoratedContractName;

	private DiContainer _container;

	private readonly List<MonoBehaviour> _injectableMonoBehaviours = new List<MonoBehaviour>();

	public IEnumerable<MonoInstaller> LateInstallers
	{
		get
		{
			return _lateInstallers;
		}
		set
		{
			_lateInstallers.Clear();
			_lateInstallers.AddRange(value);
		}
	}

	public IEnumerable<MonoInstaller> LateInstallerPrefabs
	{
		get
		{
			return _lateInstallerPrefabs;
		}
		set
		{
			_lateInstallerPrefabs.Clear();
			_lateInstallerPrefabs.AddRange(value);
		}
	}

	public IEnumerable<ScriptableObjectInstaller> LateScriptableObjectInstallers
	{
		get
		{
			return _lateScriptableObjectInstallers;
		}
		set
		{
			_lateScriptableObjectInstallers.Clear();
			_lateScriptableObjectInstallers.AddRange(value);
		}
	}

	public string DecoratedContractName => _decoratedContractName;

	public override DiContainer Container
	{
		get
		{
			Assert.IsNotNull(_container);
			return _container;
		}
	}

	public override IEnumerable<GameObject> GetRootGameObjects()
	{
		throw Assert.CreateException();
	}

	public void Initialize(DiContainer container)
	{
		Assert.IsNull(_container);
		Assert.That(LinqExtensions.IsEmpty(_injectableMonoBehaviours));
		_container = container;
		GetInjectableMonoBehaviours(_injectableMonoBehaviours);
		foreach (MonoBehaviour injectableMonoBehaviour in _injectableMonoBehaviours)
		{
			container.QueueForInject(injectableMonoBehaviour);
		}
	}

	public void InstallDecoratorSceneBindings()
	{
		_container.Bind<SceneDecoratorContext>().FromInstance(this);
		InstallSceneBindings(_injectableMonoBehaviours);
	}

	public void InstallDecoratorInstallers()
	{
		InstallInstallers();
	}

	protected override void GetInjectableMonoBehaviours(List<MonoBehaviour> monoBehaviours)
	{
		Scene scene = base.gameObject.scene;
		ZenUtilInternal.AddStateMachineBehaviourAutoInjectersInScene(scene);
		ZenUtilInternal.GetInjectableMonoBehavioursInScene(scene, monoBehaviours);
	}

	public void InstallLateDecoratorInstallers()
	{
		InstallInstallers(new List<InstallerBase>(), new List<Type>(), _lateScriptableObjectInstallers, _lateInstallers, _lateInstallerPrefabs);
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(SceneDecoratorContext), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
