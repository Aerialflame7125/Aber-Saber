using UnityEngine;

public class EnableDepthTextureMode : MonoBehaviour
{
	[SerializeField]
	private DepthTextureMode _depthTextureMode;

	private void Awake()
	{
		GetComponent<Camera>().depthTextureMode = _depthTextureMode;
	}
}
