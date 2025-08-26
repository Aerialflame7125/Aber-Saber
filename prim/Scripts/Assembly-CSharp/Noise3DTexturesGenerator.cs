using System;
using UnityEngine;

public class Noise3DTexturesGenerator : MonoBehaviour
{
	[Serializable]
	public struct TextureParams
	{
		public int width;

		public int height;

		public int depth;

		[Tooltip("Scale should be equal to repeat if you need seamless repeating")]
		public float scale;

		[Tooltip("Repeat should be equal to scale if you need seamless repeating")]
		public int repeat;

		public float contrast;
	}

	[Serializable]
	public struct MaterialTextureParamsCouple
	{
		public TextureParams textureParams;

		public string globalPropertyName;

		public MaterialPropertyNameCouple[] materialPropertyNameCouples;
	}

	[Serializable]
	public struct MaterialPropertyNameCouple
	{
		public string texturePropertyName;

		public Material material;
	}

	public MaterialTextureParamsCouple[] _data;

	private void Awake()
	{
		for (int i = 0; i < _data.Length; i++)
		{
			MaterialTextureParamsCouple materialTextureParamsCouple = _data[i];
			Color32[] pixels = CreateNoisePixels(materialTextureParamsCouple.textureParams.width, materialTextureParamsCouple.textureParams.height, materialTextureParamsCouple.textureParams.depth, materialTextureParamsCouple.textureParams.scale, materialTextureParamsCouple.textureParams.repeat, materialTextureParamsCouple.textureParams.contrast);
			Texture3D texture3D = new Texture3D(materialTextureParamsCouple.textureParams.width, materialTextureParamsCouple.textureParams.height, materialTextureParamsCouple.textureParams.depth, TextureFormat.Alpha8, mipmap: false);
			texture3D.SetPixels32(pixels);
			texture3D.Apply();
			if (materialTextureParamsCouple.globalPropertyName != null && materialTextureParamsCouple.globalPropertyName != string.Empty)
			{
				Shader.SetGlobalTexture(materialTextureParamsCouple.globalPropertyName, texture3D);
			}
			for (int j = 0; j < materialTextureParamsCouple.materialPropertyNameCouples.Length; j++)
			{
				MaterialPropertyNameCouple materialPropertyNameCouple = materialTextureParamsCouple.materialPropertyNameCouples[j];
				materialPropertyNameCouple.material.SetTexture(materialPropertyNameCouple.texturePropertyName, texture3D);
			}
		}
	}

	private Color32[] CreateNoisePixels(int width, int height, int depth, float scale, int repeat, float contrast)
	{
		Color32[] array = new Color32[width * height * depth];
		for (int i = 0; i < depth; i++)
		{
			for (int j = 0; j < height; j++)
			{
				for (int k = 0; k < width; k++)
				{
					float num = PerlinNoise.Perlin3D(scale * (float)k / (float)width, scale * (float)j / (float)height, scale * (float)i / (float)depth, repeat);
					num = (num - 0.5f) * contrast + 0.5f;
					num = Mathf.Clamp01(num);
					byte b = (byte)Mathf.RoundToInt(num * 255f);
					array[k + j * height + i * height * depth].r = b;
					array[k + j * height + i * height * depth].g = b;
					array[k + j * height + i * height * depth].b = b;
					array[k + j * height + i * height * depth].a = b;
				}
			}
		}
		return array;
	}
}
