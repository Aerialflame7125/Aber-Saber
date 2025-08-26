using UnityEngine;

public class OnRenderImageTest : MonoBehaviour
{
	[SerializeField]
	private RenderTexture _rt;

	private Material _stereoCopyMaterial;

	private void Start()
	{
		_rt = new RenderTexture(512, 512, 0);
		_rt.hideFlags = HideFlags.DontSave;
		_stereoCopyMaterial = new Material(Shader.Find("Hidden/CopyStereo"));
	}

	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, _rt, _stereoCopyMaterial);
		Graphics.Blit(_rt, destination);
	}
}
