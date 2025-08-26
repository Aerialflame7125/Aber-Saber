using UnityEngine;

[CreateAssetMenu(fileName = "BloomPrePassParams", menuName = "BS/Rendering/BloomPrePassParams")]
public class BloomPrePassParams : PersistentScriptableObject
{
	public KawaseBlurRenderer.KernelSize bloom1KernelSize = KawaseBlurRenderer.KernelSize.Kernel135;

	public float bloom1Boost;

	[Space]
	public KawaseBlurRenderer.KernelSize bloom2KernelSize = KawaseBlurRenderer.KernelSize.Kernel23;

	public float bloom2Boost;

	public float bloom2Alpha = 0.5f;

	[Space]
	public int downsample;

	public int textureWidth = 512;

	public int textureHeight = 384;

	public float textureToScreenRatio = 1f;

	[Space]
	public float linesWidth = 0.05f;

	public float linesFogDensity = 0.01f;

	public float lineIntensityMultiplier = 0.1f;

	public float fullIntensityOffset = 0.1f;

	public float transformHDRToLDRParam = 0.2f;

	[Space]
	public Texture2D linesTexture;

	public void InitFromPreset(BloomPrePassGraphicsSettingsPresets.Preset preset)
	{
		bloom1KernelSize = preset.bloom1KernelSize;
		bloom1Boost = preset.bloom1Boost;
		bloom2KernelSize = preset.bloom2KernelSize;
		bloom2Boost = preset.bloom2Boost;
		bloom2Alpha = preset.bloom2Alpha;
		downsample = preset.downsample;
		textureWidth = preset.textureWidth;
		textureHeight = preset.textureHeight;
	}
}
