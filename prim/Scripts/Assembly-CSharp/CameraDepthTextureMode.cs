using UnityEngine;

public class CameraDepthTextureMode : MonoBehaviour
{
	[SerializeField]
	private DepthTextureMode _depthTextureMode;

	private void Awake()
	{
		GetComponent<Camera>().depthTextureMode = _depthTextureMode;
	}
}
