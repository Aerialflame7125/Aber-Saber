using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject;

public class ZenAutoInjecter : MonoBehaviour
{
	public enum ContainerSources
	{
		SceneContext,
		ProjectContext,
		SearchHierarchy
	}

	[SerializeField]
	private ContainerSources _containerSource = ContainerSources.SearchHierarchy;

	private bool _hasInjected;

	public ContainerSources ContainerSource
	{
		get
		{
			return _containerSource;
		}
		set
		{
			_containerSource = value;
		}
	}

	[Inject]
	public void Construct()
	{
		if (!_hasInjected)
		{
			throw Assert.CreateException("ZenAutoInjecter was injected!  Do not use ZenAutoInjecter for objects that are instantiated through zenject or which exist in the initial scene hierarchy");
		}
	}

	public void Awake()
	{
		_hasInjected = true;
		LookupContainer().InjectGameObject(base.gameObject);
	}

	private DiContainer LookupContainer()
	{
		if (_containerSource == ContainerSources.ProjectContext)
		{
			return ProjectContext.Instance.Container;
		}
		if (_containerSource == ContainerSources.SceneContext)
		{
			return GetContainerForCurrentScene();
		}
		Assert.IsEqual(_containerSource, ContainerSources.SearchHierarchy);
		Context componentInParent = base.transform.GetComponentInParent<Context>();
		if (componentInParent != null)
		{
			return componentInParent.Container;
		}
		return GetContainerForCurrentScene();
	}

	private DiContainer GetContainerForCurrentScene()
	{
		return ProjectContext.Instance.Container.Resolve<SceneContextRegistry>().GetContainerForScene(base.gameObject.scene);
	}

	private static void __zenInjectMethod0(object P_0, object[] P_1)
	{
		((ZenAutoInjecter)P_0).Construct();
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(ZenAutoInjecter), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[1]
		{
			new InjectTypeInfo.InjectMethodInfo(__zenInjectMethod0, new InjectableInfo[0], "Construct")
		}, new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
