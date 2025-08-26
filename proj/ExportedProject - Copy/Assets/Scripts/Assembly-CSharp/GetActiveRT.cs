using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GetActiveRT : MonoBehaviour
{
	private RenderBuffer _colorBuffer;

	private RenderBuffer _depthBuffer;

	public RenderBuffer ColorBuffer
	{
		get
		{
			return _colorBuffer;
		}
	}

	public RenderBuffer DepthBuffer
	{
		get
		{
			return _depthBuffer;
		}
	}

	private void OnPreRender()
	{
		_colorBuffer = Graphics.activeColorBuffer;
		_depthBuffer = Graphics.activeDepthBuffer;
	}
}
