using ModestTree;
using UnityEngine;
using Zenject.Internal;

namespace Zenject;

public class ZenjectStateMachineBehaviourAutoInjecter : MonoBehaviour
{
	private DiContainer _container;

	private Animator _animator;

	[Inject]
	public void Construct(DiContainer container)
	{
		_container = container;
		_animator = GetComponent<Animator>();
		Assert.IsNotNull(_animator);
	}

	public void Start()
	{
		if (!(_animator != null))
		{
			return;
		}
		StateMachineBehaviour[] behaviours = _animator.GetBehaviours<StateMachineBehaviour>();
		if (behaviours != null)
		{
			StateMachineBehaviour[] array = behaviours;
			foreach (StateMachineBehaviour injectable in array)
			{
				_container.Inject(injectable);
			}
		}
	}

	private static void __zenInjectMethod0(object P_0, object[] P_1)
	{
		((ZenjectStateMachineBehaviourAutoInjecter)P_0).Construct((DiContainer)P_1[0]);
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(ZenjectStateMachineBehaviourAutoInjecter), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[1]
		{
			new InjectTypeInfo.InjectMethodInfo(__zenInjectMethod0, new InjectableInfo[1]
			{
				new InjectableInfo(optional: false, null, "container", typeof(DiContainer), null, InjectSources.Any)
			}, "Construct")
		}, new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
