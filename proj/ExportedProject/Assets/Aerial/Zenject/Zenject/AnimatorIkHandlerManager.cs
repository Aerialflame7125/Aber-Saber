using System.Collections.Generic;
using UnityEngine;
using Zenject.Internal;

namespace Zenject;

public class AnimatorIkHandlerManager : MonoBehaviour
{
	private List<IAnimatorIkHandler> _handlers;

	[Inject]
	public void Construct([Inject(Source = InjectSources.Local)] List<IAnimatorIkHandler> handlers)
	{
		_handlers = handlers;
	}

	public void OnAnimatorIk()
	{
		foreach (IAnimatorIkHandler handler in _handlers)
		{
			handler.OnAnimatorIk();
		}
	}

	private static void __zenInjectMethod0(object P_0, object[] P_1)
	{
		((AnimatorIkHandlerManager)P_0).Construct((List<IAnimatorIkHandler>)P_1[0]);
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(AnimatorIkHandlerManager), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[1]
		{
			new InjectTypeInfo.InjectMethodInfo(__zenInjectMethod0, new InjectableInfo[1]
			{
				new InjectableInfo(optional: false, null, "handlers", typeof(List<IAnimatorIkHandler>), null, InjectSources.Local)
			}, "Construct")
		}, new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
