using UnityEngine;
using Zenject.Internal;

namespace Zenject;

public class GuiRenderer : MonoBehaviour
{
	private GuiRenderableManager _renderableManager;

	[Inject]
	private void Construct(GuiRenderableManager renderableManager)
	{
		_renderableManager = renderableManager;
	}

	public void OnGUI()
	{
		_renderableManager.OnGui();
	}

	private static void __zenInjectMethod0(object P_0, object[] P_1)
	{
		((GuiRenderer)P_0).Construct((GuiRenderableManager)P_1[0]);
	}

	[Preserve]
	private static InjectTypeInfo __zenCreateInjectTypeInfo()
	{
		return new InjectTypeInfo(typeof(GuiRenderer), new InjectTypeInfo.InjectConstructorInfo(null, new InjectableInfo[0]), new InjectTypeInfo.InjectMethodInfo[1]
		{
			new InjectTypeInfo.InjectMethodInfo(__zenInjectMethod0, new InjectableInfo[1]
			{
				new InjectableInfo(optional: false, null, "renderableManager", typeof(GuiRenderableManager), null, InjectSources.Any)
			}, "Construct")
		}, new InjectTypeInfo.InjectMemberInfo[0]);
	}
}
