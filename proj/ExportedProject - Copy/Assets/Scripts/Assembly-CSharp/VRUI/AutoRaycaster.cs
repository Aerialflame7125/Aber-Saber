using UnityEngine;
using UnityEngine.UI;
using VRUIControls;

namespace VRUI
{
	public class AutoRaycaster : MonoBehaviour
	{
		private void Awake()
		{
			if (AutoInputModule.useVRUIControls)
			{
				base.gameObject.AddComponent<VRGraphicRaycaster>();
			}
			else
			{
				base.gameObject.AddComponent<GraphicRaycaster>();
			}
		}
	}
}
