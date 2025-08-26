using UnityEngine;
using UnityEngine.Rendering;

[ExecuteInEditMode]
public class DirectionalESMLight : MonoBehaviour
{
	[Range(1f, 10f)]
	[SerializeField]
	private float _blurSize = 1f;

	private RenderTexture _esmShadowTexture;

	private CommandBuffer _copyBuffer;

	private Material _esmBlitMaterial;

	private void OnEnable()
	{
		_esmShadowTexture = new RenderTexture(2048, 2048, 0, RenderTextureFormat.RFloat);
		_copyBuffer = new CommandBuffer();
		_esmBlitMaterial = new Material(Shader.Find("Hidden/ESMBlit"));
		Light component = GetComponent<Light>();
		component.AddCommandBuffer(LightEvent.AfterShadowMap, _copyBuffer);
		GraphicsSettings.SetShaderMode(BuiltinShaderType.ScreenSpaceShadows, BuiltinShaderMode.UseCustom);
		GraphicsSettings.SetCustomShader(BuiltinShaderType.ScreenSpaceShadows, Shader.Find("Hidden/ScreenSpaceShadowsESM"));
	}

	private void Update()
	{
		_copyBuffer.Clear();
		_copyBuffer.SetShadowSamplingMode(BuiltinRenderTextureType.CurrentActive, ShadowSamplingMode.RawDepth);
		_copyBuffer.SetGlobalTexture("_ShadowMapMain", BuiltinRenderTextureType.CurrentActive);
		_copyBuffer.Blit(BuiltinRenderTextureType.CurrentActive, _esmShadowTexture, _esmBlitMaterial, 0);
		int num = Shader.PropertyToID("_ShadowMapTemp0");
		int num2 = Shader.PropertyToID("_ShadowMapTemp1");
		_copyBuffer.GetTemporaryRT(num, _esmShadowTexture.width / 2, _esmShadowTexture.height / 2, 0, FilterMode.Bilinear, RenderTextureFormat.RFloat);
		_copyBuffer.SetGlobalVector("_Parameter", new Vector4(_blurSize, 0f - _blurSize, 0f, 0f));
		_copyBuffer.Blit(_esmShadowTexture, num, _esmBlitMaterial, 1);
		_copyBuffer.Blit(num, num2, _esmBlitMaterial, 2);
		_copyBuffer.ReleaseTemporaryRT(num);
		_copyBuffer.Blit(num2, _esmShadowTexture, _esmBlitMaterial, 3);
		_copyBuffer.ReleaseTemporaryRT(num2);
		_copyBuffer.SetGlobalTexture("_ShadowMapESM", _esmShadowTexture);
	}

	private void OnDisable()
	{
		Light component = GetComponent<Light>();
		component.RemoveCommandBuffer(LightEvent.AfterShadowMap, _copyBuffer);
		GraphicsSettings.SetCustomShader(BuiltinShaderType.ScreenSpaceShadows, null);
		GraphicsSettings.SetShaderMode(BuiltinShaderType.ScreenSpaceShadows, BuiltinShaderMode.UseBuiltin);
		Object.DestroyImmediate(_esmShadowTexture);
	}
}
