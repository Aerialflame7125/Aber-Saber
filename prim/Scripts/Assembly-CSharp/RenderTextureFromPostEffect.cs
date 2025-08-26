using UnityEngine;

public class RenderTextureFromPostEffect : MonoBehaviour
{
	public RenderTexture _targetTexture;

	private Camera _camera;

	public RenderTexture targetTexture => _targetTexture;

	private void Awake()
	{
		_camera = GetComponent<Camera>();
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dst)
	{
		if (_targetTexture != null && (_targetTexture.width != _camera.pixelWidth * 2 || _targetTexture.height != _camera.pixelHeight))
		{
			Object.Destroy(_targetTexture);
			_targetTexture = null;
		}
		if (_targetTexture == null)
		{
			_targetTexture = new RenderTexture(_camera.pixelWidth * 2, _camera.pixelHeight, 24);
			_targetTexture.hideFlags = HideFlags.DontSave;
		}
		Graphics.Blit(src, _targetTexture);
		Graphics.Blit(src, dst);
	}
}
