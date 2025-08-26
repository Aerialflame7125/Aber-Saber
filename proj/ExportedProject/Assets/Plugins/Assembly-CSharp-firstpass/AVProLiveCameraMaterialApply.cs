using UnityEngine;

[AddComponentMenu("AVPro Live Camera/Material Apply")]
public class AVProLiveCameraMaterialApply : MonoBehaviour
{
	public Material _material;

	public AVProLiveCamera _liveCamera;

	private void Start()
	{
		if (_liveCamera != null && _liveCamera.OutputTexture != null)
		{
			ApplyMapping(_liveCamera.OutputTexture);
		}
	}

	private void Update()
	{
		if (_liveCamera != null && _liveCamera.OutputTexture != null)
		{
			ApplyMapping(_liveCamera.OutputTexture);
		}
	}

	private void ApplyMapping(Texture texture)
	{
		if (_material != null)
		{
			_material.mainTexture = texture;
		}
	}

	public void OnDisable()
	{
		ApplyMapping(null);
	}
}
