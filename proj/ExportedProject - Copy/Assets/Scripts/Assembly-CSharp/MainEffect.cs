using System;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[ImageEffectAllowedInSceneView]
[ExecuteInEditMode]
public class MainEffect : MonoBehaviour
{
	[SerializeField]
	private MainEffectParams _params;

	[SerializeField]
	private FloatVariable _fadeInOutVariable;

	[SerializeField]
	private KawaseBlurRenderer _kawaseBlurRenderer;

	[Space]
	[SerializeField]
	private Shader _mainEffectShader;

	[NonSerialized]
	public bool grabFinalImage;

	private int _bloomTexID;

	private int _bloomIntensityID;

	private int _baseColorBoostID;

	private int _baseColorBoostThresholdID;

	private int _fadeAmountID;

	private Material _material;

	private RenderTexture _finalImageRenderTexture;

	private const string kMainShaderName = "Hidden/MainEffect";

	public RenderTexture finalImageRenderTexture
	{
		get
		{
			return _finalImageRenderTexture;
		}
	}

	private void Awake()
	{
		SetShaderPropertiesIfNeeded();
		Camera component = GetComponent<Camera>();
		if ((bool)_params && _params.depthTextureEnabled)
		{
			component.depthTextureMode = DepthTextureMode.Depth;
		}
		else
		{
			component.depthTextureMode = DepthTextureMode.None;
		}
	}

	private void OnDestroy()
	{
		EssentialHelpers.SafeDestroy(_material);
	}

	private void Start()
	{
		_bloomTexID = Shader.PropertyToID("_BloomTex");
		_baseColorBoostID = Shader.PropertyToID("_BaseColorBoost");
		_baseColorBoostThresholdID = Shader.PropertyToID("_BaseColorBoostThreshold");
		_bloomIntensityID = Shader.PropertyToID("_BloomIntensity");
		_fadeAmountID = Shader.PropertyToID("_FadeAmount");
	}

	private void OnValidate()
	{
		SetShaderPropertiesIfNeeded();
	}

	private void OnRenderImage(RenderTexture src, RenderTexture dest)
	{
		if (grabFinalImage && _finalImageRenderTexture == null)
		{
			RenderTextureDescriptor descriptor = src.descriptor;
			descriptor.depthBufferBits = 0;
			_finalImageRenderTexture = new RenderTexture(descriptor);
		}
		if (_material == null)
		{
			_material = new Material(_mainEffectShader);
		}
		int textureWidth = _params.textureWidth;
		int textureHeight = _params.textureHeight;
		RenderTextureDescriptor desc;
		if (dest != null)
		{
			desc = dest.descriptor;
			desc.width = textureWidth;
			desc.height = textureHeight;
		}
		else
		{
			desc = new RenderTextureDescriptor(textureWidth, textureHeight, RenderTextureFormat.ARGB32, 0);
		}
		RenderTexture temporary = RenderTexture.GetTemporary(desc);
		_kawaseBlurRenderer.AlphaWeights(src, temporary, _params.bloomAlphaWeightScale);
		RenderTexture temporary2 = RenderTexture.GetTemporary(desc);
		_kawaseBlurRenderer.Bloom(temporary, temporary2, _params.bloomIterations, 0f);
		RenderTexture.ReleaseTemporary(temporary);
		_material.SetFloat(_baseColorBoostID, _params.baseColorBoost);
		_material.SetFloat(_baseColorBoostThresholdID, _params.baseColorBoostThreshold);
		_material.SetFloat(_bloomIntensityID, _params.bloomIntensity);
		_material.SetFloat(_fadeAmountID, _fadeInOutVariable.value);
		_material.SetTexture(_bloomTexID, temporary2);
		if (grabFinalImage)
		{
			Graphics.Blit(src, _finalImageRenderTexture, _material, 0);
			Graphics.Blit(_finalImageRenderTexture, dest);
		}
		else
		{
			Graphics.Blit(src, dest, _material, 0);
		}
		RenderTexture.ReleaseTemporary(temporary2);
	}

	public void ApplyImageEffect(RenderTexture src, RenderTexture dest)
	{
		bool flag = grabFinalImage;
		grabFinalImage = false;
		OnRenderImage(src, dest);
		grabFinalImage = flag;
	}

	private void SetShaderPropertiesIfNeeded()
	{
	}
}
