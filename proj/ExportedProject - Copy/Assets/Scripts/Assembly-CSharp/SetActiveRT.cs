using UnityEngine;

public class SetActiveRT : MonoBehaviour
{
	[SerializeField]
	private GetActiveRT _getActiveRT;

	private void OnPreRender()
	{
		Graphics.SetRenderTarget(_getActiveRT.ColorBuffer, _getActiveRT.DepthBuffer);
	}
}
