using UnityEngine;

[RequireComponent(typeof(Camera))]
[ImageEffectAllowedInSceneView]
[ExecuteInEditMode]
public class KawaseBlurPostProcess : MonoBehaviour
{
	[SerializeField]
	private KawaseBlurRenderer _kawaseBlurRenderer;

	[SerializeField]
	private KawaseBlurRenderer.KernelSize _kernelSize = KawaseBlurRenderer.KernelSize.Kernel23;

	[SerializeField]
	private int _downsample;

	public void Init(KawaseBlurRenderer.KernelSize kernelSize, int downsample)
	{
		_kernelSize = kernelSize;
		_downsample = downsample;
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		_kawaseBlurRenderer.Blur(src, dest, _kernelSize, 0f, _downsample);
	}
}
