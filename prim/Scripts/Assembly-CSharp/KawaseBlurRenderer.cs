using System;
using UnityEngine;
using UnityEngine.Rendering;

public class KawaseBlurRenderer : ScriptableObject
{
	public enum KernelSize
	{
		Kernel7,
		Kernel15,
		Kernel23,
		Kernel35,
		Kernel63,
		Kernel127,
		Kernel135,
		Kernel143
	}

	private class BloomKernel
	{
		public KernelSize kernelSize;

		public int sharedPart;
	}

	[SerializeField]
	private Shader _kawaseBlurShader;

	[SerializeField]
	private Shader _additiveShader;

	private int _offsetID;

	private int _boostID;

	private int _zOffsetID;

	private int _zScaleID;

	private int _weightScaleID;

	private Material _kawaseBlurMaterial;

	private Material _additiveMaterial;

	private Material _commandBuffersMaterial;

	private int[][] _kernels;

	private BloomKernel[] _bloomKernels;

	private const int kMaxBloomIterations = 16;

	private const int kBlurAndAddPassNum = 3;

	private const int kBlurPassNum = 2;

	private const int kAlphaAndDepthWeightsPassNum = 1;

	private const int kAlphaWeightsPassNum = 0;

	public int[] GetBlurKernel(KernelSize kernelSize)
	{
		if (_kernels[(int)kernelSize] != null)
		{
			return _kernels[(int)kernelSize];
		}
		int[] array = kernelSize switch
		{
			KernelSize.Kernel7 => new int[2], 
			KernelSize.Kernel15 => new int[3] { 0, 1, 1 }, 
			KernelSize.Kernel23 => new int[4] { 0, 1, 1, 2 }, 
			KernelSize.Kernel35 => new int[5] { 0, 1, 2, 2, 3 }, 
			KernelSize.Kernel63 => new int[7] { 0, 1, 2, 3, 4, 4, 5 }, 
			KernelSize.Kernel127 => new int[10] { 0, 1, 2, 3, 4, 5, 7, 8, 9, 10 }, 
			KernelSize.Kernel135 => new int[11]
			{
				0, 1, 2, 3, 4, 5, 7, 8, 9, 10,
				11
			}, 
			KernelSize.Kernel143 => new int[12]
			{
				0, 1, 2, 3, 4, 5, 7, 8, 9, 10,
				11, 12
			}, 
			_ => new int[2], 
		};
		_kernels[(int)kernelSize] = array;
		return array;
	}

	private void OnEnable()
	{
		_offsetID = Shader.PropertyToID("_Offset");
		_boostID = Shader.PropertyToID("_Boost");
		_zOffsetID = Shader.PropertyToID("_ZOffset");
		_zScaleID = Shader.PropertyToID("_ZScale");
		_weightScaleID = Shader.PropertyToID("_WeightScale");
		_kernels = new int[Enum.GetNames(typeof(KernelSize)).Length][];
		_bloomKernels = new BloomKernel[5]
		{
			new BloomKernel
			{
				kernelSize = KernelSize.Kernel7,
				sharedPart = 1
			},
			new BloomKernel
			{
				kernelSize = KernelSize.Kernel15,
				sharedPart = 2
			},
			new BloomKernel
			{
				kernelSize = KernelSize.Kernel35,
				sharedPart = 3
			},
			new BloomKernel
			{
				kernelSize = KernelSize.Kernel63,
				sharedPart = 5
			},
			new BloomKernel
			{
				kernelSize = KernelSize.Kernel127,
				sharedPart = 10
			}
		};
		_kawaseBlurMaterial = new Material(_kawaseBlurShader);
		_kawaseBlurMaterial.hideFlags = HideFlags.HideAndDontSave;
		_commandBuffersMaterial = new Material(_kawaseBlurShader);
		_commandBuffersMaterial.hideFlags = HideFlags.HideAndDontSave;
		_additiveMaterial = new Material(_additiveShader);
		_additiveMaterial.hideFlags = HideFlags.HideAndDontSave;
	}

	private void OnDisable()
	{
		EssentialHelpers.SafeDestroy(_kawaseBlurMaterial);
		EssentialHelpers.SafeDestroy(_commandBuffersMaterial);
		EssentialHelpers.SafeDestroy(_additiveMaterial);
	}

	public void Bloom(RenderTexture src, RenderTexture dest, int iterations, float boost)
	{
		iterations = Mathf.Clamp(iterations, 1, Mathf.Min(_bloomKernels.Length, 16));
		RenderTextureDescriptor descriptor = src.descriptor;
		descriptor.depthBufferBits = 0;
		RenderTexture renderTexture = src;
		int num = 0;
		for (int i = 0; i < iterations; i++)
		{
			BloomKernel bloomKernel = _bloomKernels[i];
			int[] blurKernel = GetBlurKernel(bloomKernel.kernelSize);
			RenderTexture temporary = RenderTexture.GetTemporary(descriptor);
			Blur(renderTexture, temporary, blurKernel, boost, 0, num, bloomKernel.sharedPart - num, additivelyBlendToDest: false);
			num = bloomKernel.sharedPart;
			Blur(temporary, dest, blurKernel, boost, 0, bloomKernel.sharedPart, blurKernel.Length - bloomKernel.sharedPart, i > 0);
			if (i > 0)
			{
				RenderTexture.ReleaseTemporary(renderTexture);
			}
			renderTexture = temporary;
		}
		RenderTexture.ReleaseTemporary(renderTexture);
	}

	public void DoubleBlur(RenderTexture src, RenderTexture dest0, KernelSize kernelSize0, float boost0, RenderTexture dest1, KernelSize kernelSize1, float boost1, int downsample)
	{
		int[] blurKernel = GetBlurKernel(kernelSize0);
		int[] blurKernel2 = GetBlurKernel(kernelSize1);
		int i;
		for (i = 0; i < blurKernel.Length && i < blurKernel2.Length && blurKernel[i] == blurKernel2[i]; i++)
		{
		}
		int width = src.width >> downsample;
		int height = src.height >> downsample;
		RenderTextureDescriptor descriptor = src.descriptor;
		descriptor.depthBufferBits = 0;
		descriptor.width = width;
		descriptor.height = height;
		RenderTexture temporary = RenderTexture.GetTemporary(descriptor);
		Blur(src, temporary, blurKernel, 0f, downsample, 0, i, additivelyBlendToDest: false);
		Blur(temporary, dest0, blurKernel, boost0, 0, i, blurKernel.Length - i, additivelyBlendToDest: false);
		Blur(temporary, dest1, blurKernel2, boost1, 0, i, blurKernel2.Length - i, additivelyBlendToDest: false);
		RenderTexture.ReleaseTemporary(temporary);
	}

	public void Blur(RenderTexture src, RenderTexture dest, KernelSize kernelSize, float boost, int downsample)
	{
		int[] blurKernel = GetBlurKernel(kernelSize);
		Blur(src, dest, blurKernel, boost, downsample, 0, blurKernel.Length, additivelyBlendToDest: false);
	}

	private void Blur(RenderTexture src, RenderTexture dest, int[] kernel, float boost, int downsample, int startIdx, int length, bool additivelyBlendToDest)
	{
		if (length == 0)
		{
			Graphics.Blit(src, dest);
			return;
		}
		int num = src.width >> downsample;
		int num2 = src.height >> downsample;
		RenderTextureDescriptor descriptor = src.descriptor;
		descriptor.depthBufferBits = 0;
		descriptor.width = num;
		descriptor.height = num2;
		RenderTexture renderTexture = src;
		Vector2 vector = new Vector2(1f / (float)num, 1f / (float)num2);
		int num3 = startIdx + length;
		for (int i = startIdx; i < num3; i++)
		{
			RenderTexture renderTexture2 = null;
			int pass = 2;
			if (i == num3 - 1)
			{
				renderTexture2 = dest;
				if (additivelyBlendToDest)
				{
					pass = 3;
				}
			}
			else
			{
				renderTexture2 = RenderTexture.GetTemporary(descriptor);
			}
			float num4 = (float)kernel[i] + 0.5f;
			_kawaseBlurMaterial.SetVector(_offsetID, new Vector4(num4 * vector.x, num4 * vector.y, (0f - num4) * vector.x, num4 * vector.y));
			_kawaseBlurMaterial.SetFloat(_boostID, boost);
			Graphics.Blit(renderTexture, renderTexture2, _kawaseBlurMaterial, pass);
			if (renderTexture != src)
			{
				RenderTexture.ReleaseTemporary(renderTexture);
			}
			renderTexture = renderTexture2;
		}
	}

	public void AlphaWeights(RenderTexture src, RenderTexture dest, float weightScale)
	{
		float num = 0.5f;
		Vector2 vector = new Vector2(1f / (float)src.width, 1f / (float)src.height);
		_kawaseBlurMaterial.SetFloat(_weightScaleID, weightScale);
		_kawaseBlurMaterial.SetVector(_offsetID, new Vector4(num * vector.x, num * vector.y, (0f - num) * vector.x, num * vector.y));
		Graphics.Blit(src, dest, _kawaseBlurMaterial, 0);
	}

	public void AlphaAndDepthWeights(RenderTexture src, RenderTexture dest, float weightScale, float zOffset, float zScale)
	{
		float num = 0.5f;
		Vector2 vector = new Vector2(1f / (float)src.width, 1f / (float)src.height);
		_kawaseBlurMaterial.SetFloat(_weightScaleID, weightScale);
		_kawaseBlurMaterial.SetFloat(_zOffsetID, zOffset);
		_kawaseBlurMaterial.SetFloat(_zScaleID, zScale);
		_kawaseBlurMaterial.SetVector(_offsetID, new Vector4(num * vector.x, num * vector.y, (0f - num) * vector.x, num * vector.y));
		Graphics.Blit(src, dest, _kawaseBlurMaterial, 1);
	}

	public CommandBuffer CreateBlurCommandBuffer(string globalTextureName, KernelSize kernelSize, float boost, int downsample)
	{
		int[] blurKernel = GetBlurKernel(kernelSize);
		CommandBuffer commandBuffer = new CommandBuffer();
		Camera current = Camera.current;
		int num = ((!current.stereoEnabled || current.stereoTargetEye != StereoTargetEyeMask.Both) ? current.pixelWidth : (current.pixelWidth * 2));
		int pixelHeight = current.pixelHeight;
		int num2 = num >> downsample;
		int num3 = pixelHeight >> downsample;
		int num4 = Shader.PropertyToID("_ScreenCopyTexture");
		commandBuffer.GetTemporaryRT(num4, num2, num3, 0, FilterMode.Bilinear);
		commandBuffer.Blit(BuiltinRenderTextureType.CurrentActive, num4);
		int num5 = Shader.PropertyToID("_TempTexture0");
		int num6 = Shader.PropertyToID("_TempTexture1");
		commandBuffer.GetTemporaryRT(num5, num2, num3, 0, FilterMode.Bilinear);
		commandBuffer.GetTemporaryRT(num6, num2, num3, 0, FilterMode.Bilinear);
		int num7 = num5;
		int num8 = num6;
		if (downsample > 0)
		{
			commandBuffer.Blit(num4, num8);
		}
		Vector2 vector = new Vector2(1f / (float)num2, 1f / (float)num3);
		for (int i = 0; i < blurKernel.Length; i++)
		{
			float num9 = (float)blurKernel[i] + 0.5f;
			commandBuffer.SetGlobalVector(_offsetID, new Vector4(num9 * vector.x, num9 * vector.y, (0f - num9) * vector.x, num9 * vector.y));
			commandBuffer.SetGlobalFloat(_boostID, boost);
			if (i == 0 && downsample == 0)
			{
				commandBuffer.Blit(num4, num7, _commandBuffersMaterial, 2);
			}
			else
			{
				commandBuffer.Blit(num8, num7, _commandBuffersMaterial, 2);
			}
			int num10 = num7;
			num7 = num8;
			num8 = num10;
		}
		commandBuffer.SetGlobalTexture(globalTextureName, num7);
		commandBuffer.ReleaseTemporaryRT(num4);
		commandBuffer.ReleaseTemporaryRT(num8);
		commandBuffer.ReleaseTemporaryRT(num7);
		return commandBuffer;
	}
}
