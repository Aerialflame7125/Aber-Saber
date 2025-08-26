using UnityEngine;

[AddComponentMenu("AVPro Live Camera/Mesh Apply")]
public class AVProLiveCameraMeshApply : MonoBehaviour
{
	public MeshRenderer _mesh;

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
		if (_mesh != null)
		{
			Material[] materials = _mesh.materials;
			foreach (Material material in materials)
			{
				material.mainTexture = texture;
			}
		}
	}

	public void OnDisable()
	{
		ApplyMapping(null);
	}
}
