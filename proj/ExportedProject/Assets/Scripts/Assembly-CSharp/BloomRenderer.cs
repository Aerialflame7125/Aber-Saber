using UnityEngine;

public class BloomRenderer : ScriptableObject
{
	[SerializeField]
	private Shader _shader;

	private int _baseTexID;

	private int _sampleScaleID;

	private Material _material;

	private RenderTexture[] _blurBuffer1 = new RenderTexture[16];

	private RenderTexture[] _blurBuffer2 = new RenderTexture[16];

	private const int kMaxIterations = 16;

	private const int kDownsamplePassNum = 0;

	private const int kUpsamplePassNum = 1;

	private void OnEnable()
	{
		_baseTexID = Shader.PropertyToID("_BaseTex");
		_sampleScaleID = Shader.PropertyToID("_SampleScale");
		_material = new Material(_shader);
		_material.hideFlags = HideFlags.HideAndDontSave;
	}

	private void OnDisable()
	{
		EssentialHelpers.SafeDestroy(_material);
	}

	public void RenderBloom(RenderTexture src, RenderTexture dest, float radius)
	{
		RenderTextureDescriptor descriptor = src.descriptor;
		descriptor.depthBufferBits = 0;
		int width = descriptor.width;
		int height = descriptor.height;
		float num = Mathf.Log(height, 2f) + radius - 8f;
		int num2 = (int)num;
		int num3 = Mathf.Clamp(num2, 1, 16);
		_material.SetFloat(_sampleScaleID, 0.5f + num - (float)num2);
		RenderTexture source = src;
		for (int i = 0; i < num3; i++)
		{
			descriptor.width /= 2;
			descriptor.height /= 2;
			_blurBuffer1[i] = RenderTexture.GetTemporary(descriptor);
			Graphics.Blit(source, _blurBuffer1[i], _material, 0);
			source = _blurBuffer1[i];
		}
		for (int num4 = num3 - 2; num4 >= 0; num4--)
		{
			RenderTexture renderTexture = _blurBuffer1[num4];
			_material.SetTexture(_baseTexID, renderTexture);
			if (num4 > 0)
			{
				descriptor.width = renderTexture.width;
				descriptor.height = renderTexture.height;
				_blurBuffer2[num4] = RenderTexture.GetTemporary(descriptor);
				Graphics.Blit(source, _blurBuffer2[num4], _material, 1);
			}
			else
			{
				Graphics.Blit(source, dest, _material, 1);
			}
			source = _blurBuffer2[num4];
		}
		for (int j = 0; j < 16; j++)
		{
			if (_blurBuffer1[j] != null)
			{
				RenderTexture.ReleaseTemporary(_blurBuffer1[j]);
			}
			if (_blurBuffer2[j] != null)
			{
				RenderTexture.ReleaseTemporary(_blurBuffer2[j]);
			}
			_blurBuffer1[j] = null;
			_blurBuffer2[j] = null;
		}
	}
}
