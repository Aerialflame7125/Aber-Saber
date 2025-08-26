using System.Collections.Generic;
using UnityEngine;
using Zenject.Internal;

namespace Zenject;

public class AnimatorMoveHandlerManager : MonoBehaviour
{
	private List<IAnimatorMoveHandler> _handlers;

	[Inject]
	public void Construct([Inject(Source = InjectSources.Local)] List<IAnimatorMoveHandler> handlers)
	{
		_handlers = handlers;
	}

	public void OnAnimatorMove()
	{
		foreach (IAnimatorMoveHandler handler in _handlers)
		{
			handler.OnAnimatorMove();
		}
	}

	private static void __zenInjectMethod0(object P_0, object[] P_1)
	{
		((AnimatorMoveHandlerManager)P_0).Construct((List<IAnimatorMoveHandler>)P_1[0]);
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(AnimatorMoveHandlerManager), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[1]
		{
			new InjectTypeInfo.InjectMethodInfo(__zenInjectMethod0, new InjectableInfo[1]
			{
				new InjectableInfo(optional: false, null, "handlers", typeof(List<IAnimatorMoveHandler>), null, InjectSources.Local)
			}, "Construct")
		}, new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
