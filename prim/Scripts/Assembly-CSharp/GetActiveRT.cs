using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GetActiveRT : MonoBehaviour
{
	private RenderBuffer _colorBuffer;

	private RenderBuffer _depthBuffer;

	public RenderBuffer ColorBuffer => _colorBuffer;

	public RenderBuffer DepthBuffer => _depthBuffer;

	private void OnPreRender()
	{
		_colorBuffer = Graphics.activeColorBuffer;
		_depthBuffer = Graphics.activeDepthBuffer;
	}
}
