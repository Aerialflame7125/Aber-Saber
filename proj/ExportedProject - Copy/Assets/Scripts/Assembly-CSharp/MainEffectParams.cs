using UnityEngine;

[CreateAssetMenu(fileName = "MainEffectParams", menuName = "BS/Rendering/MainEffectParams")]
public class MainEffectParams : PersistentScriptableObject
{
	[Range(0f, 3f)]
	public float baseColorBoost = 0.5f;

	public float baseColorBoostThreshold = 0.1f;

	[Space]
	public int bloomIterations = 4;

	public float bloomAlphaWeightScale = 1f;

	[Range(0f, 5f)]
	public float bloomIntensity = 1f;

	[Space]
	public int textureWidth = 512;

	public int textureHeight = 384;

	public bool depthTextureEnabled;

	public void InitFromPreset(MainEffectGraphicsSettingsPresets.Preset preset)
	{
		baseColorBoost = preset.baseColorBoost;
		baseColorBoostThreshold = preset.baseColorBoostThreshold;
		bloomIterations = preset.bloomIterations;
		bloomAlphaWeightScale = preset.bloomAlphaWeightScale;
		bloomIntensity = preset.bloomIntensity;
		textureWidth = preset.textureWidth;
		textureHeight = preset.textureHeight;
	}
}
