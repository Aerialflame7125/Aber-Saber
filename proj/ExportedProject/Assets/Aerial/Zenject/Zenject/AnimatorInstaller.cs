using UnityEngine;
using Zenject.Internal;

namespace Zenject;

public class AnimatorInstaller : Installer<Animator, AnimatorInstaller>
{
	private readonly Animator _animator;

	public AnimatorInstaller(Animator animator)
	{
		_animator = animator;
	}

	public override void InstallBindings()
	{
		base.Container.Bind<AnimatorIkHandlerManager>().FromNewComponentOn(_animator.gameObject);
		base.Container.Bind<AnimatorIkHandlerManager>().FromNewComponentOn(_animator.gameObject);
	}

	private static object __zenCreate(object[] P_0)
	{
		return new AnimatorInstaller((Animator)P_0[0]);
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(AnimatorInstaller), new InjectTypeInfo.InjectConstructorInfo(__zenCreate, new InjectableInfo[1]
		{
			new InjectableInfo(optional: false, null, "animator", typeof(Animator), null, InjectSources.Any)
		}), new InjectTypeInfo.InjectMethodInfo[0], new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
