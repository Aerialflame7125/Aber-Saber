using UnityEngine;

public class SimpleTemporalFiltering
{
	private RenderTexture[] _temporalFilteringTextures;

	private int _prevTemporalFilteringTextureIdx = -1;

	private Material _temporalFilteringMaterial;

	private int _bufferTexID;

	public SimpleTemporalFiltering()
	{
		Shader shader = Shader.Find("Hidden/TemporalFiltering");
		_temporalFilteringMaterial = new Material(shader);
		_bufferTexID = Shader.PropertyToID("_BufferTex");
	}

	public RenderTexture FilterTexture(RenderTexture src)
	{
		CreateRenderTexturesIfNeeded(src.width, src.height);
		RenderTexture renderTexture = null;
		if (_prevTemporalFilteringTextureIdx >= 0)
		{
			_temporalFilteringMaterial.SetTexture(_bufferTexID, _temporalFilteringTextures[_prevTemporalFilteringTextureIdx]);
			int num = (_prevTemporalFilteringTextureIdx + 1) % 2;
			renderTexture = _temporalFilteringTextures[num];
			Graphics.Blit(src, renderTexture, _temporalFilteringMaterial);
		}
		else
		{
			renderTexture = _temporalFilteringTextures[0];
			Graphics.Blit(src, renderTexture);
		}
		_prevTemporalFilteringTextureIdx = (_prevTemporalFilteringTextureIdx + 1) % 2;
		return renderTexture;
	}

	private void CreateRenderTexturesIfNeeded(int width, int height)
	{
		if (_temporalFilteringTextures == null)
		{
			_temporalFilteringTextures = new RenderTexture[2];
		}
		for (int i = 0; i < 2; i++)
		{
			if (_temporalFilteringTextures[i] == null || _temporalFilteringTextures[i].width != width || _temporalFilteringTextures[i].height != height)
			{
				if (_temporalFilteringTextures[i] != null)
				{
					_temporalFilteringTextures[i].Release();
				}
				_temporalFilteringTextures[i] = new RenderTexture(width, height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Linear);
				_temporalFilteringTextures[i].wrapMode = TextureWrapMode.Mirror;
				_temporalFilteringTextures[i].Create();
			}
		}
	}
}
