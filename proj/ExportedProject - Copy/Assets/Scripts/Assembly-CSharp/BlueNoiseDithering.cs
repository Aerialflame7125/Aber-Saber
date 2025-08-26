using UnityEngine;

[CreateAssetMenu(fileName = "BlueNoiseDithering", menuName = "BS/Rendering/BlueNoiseDithering")]
public class BlueNoiseDithering : ScriptableObject
{
	[SerializeField]
	private Texture2D _noiseTexture;

	private int _noiseParamsID;

	private int _globalNoiseTextureID;

	private void OnEnable()
	{
		_noiseParamsID = Shader.PropertyToID("_GlobalBlueNoiseParams");
		_globalNoiseTextureID = Shader.PropertyToID("_GlobalBlueNoiseTex");
	}

	public void SetBlueNoiseShaderParams(int cameraPixelWidth, int cameraPixelHeight)
	{
		Shader.SetGlobalVector(_noiseParamsID, new Vector4((float)cameraPixelWidth / (float)_noiseTexture.width, (float)cameraPixelHeight / (float)_noiseTexture.height, 0f, 0f));
		Shader.SetGlobalTexture(_globalNoiseTextureID, _noiseTexture);
	}
}
