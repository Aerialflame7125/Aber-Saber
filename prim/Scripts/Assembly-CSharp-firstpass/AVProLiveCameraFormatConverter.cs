using System;
using System.Collections.Generic;
using UnityEngine;

public class AVProLiveCameraFormatConverter : IDisposable
{
	private int _deviceIndex;

	private RenderTexture _finalTexture;

	private Material _conversionMaterial;

	private Material _deinterlaceMaterial;

	private int _width;

	private int _height;

	private bool _flipX;

	private bool _flipY;

	private bool _deinterlace;

	private AVProLiveCameraPlugin.VideoFrameFormat _format;

	private List<AVProLiveCameraPixelBuffer> _buffers;

	private Vector4[] _uvs;

	private bool _requiresTextureCrop;

	private int _lastFrameUpdated;

	public Texture OutputTexture => _finalTexture;

	public bool FlipX
	{
		get
		{
			return _flipX;
		}
		set
		{
			if (_flipX != value)
			{
				SetFlip(value, _flipY);
			}
		}
	}

	public bool FlipY
	{
		get
		{
			return _flipY;
		}
		set
		{
			if (_flipY != value)
			{
				SetFlip(_flipX, value);
			}
		}
	}

	public bool ValidPicture { get; private set; }

	public AVProLiveCameraFormatConverter(int deviceIndex)
	{
		ValidPicture = false;
		_deviceIndex = deviceIndex;
		_buffers = new List<AVProLiveCameraPixelBuffer>(4);
	}

	public void Reset()
	{
		ValidPicture = false;
		_lastFrameUpdated = 0;
	}

	public bool Build(int width, int height, AVProLiveCameraPlugin.VideoFrameFormat format, bool flipX, bool flipY, bool deinterlace = false)
	{
		Reset();
		_width = width;
		_height = height;
		_deinterlace = deinterlace;
		_format = format;
		_flipX = flipX;
		_flipY = flipY;
		if (CreateMaterials())
		{
			CreateBuffers();
			CreateRenderTexture();
			switch (_format)
			{
			case AVProLiveCameraPlugin.VideoFrameFormat.RAW_BGRA32:
				_conversionMaterial.SetTexture("_MainTex", _buffers[0]._texture);
				break;
			case AVProLiveCameraPlugin.VideoFrameFormat.RAW_MONO8:
				_conversionMaterial.SetFloat("_TextureWidth", _finalTexture.width);
				_conversionMaterial.SetTexture("_MainTex", _buffers[0]._texture);
				break;
			case AVProLiveCameraPlugin.VideoFrameFormat.YUV_422_YUY2:
			case AVProLiveCameraPlugin.VideoFrameFormat.YUV_422_UYVY:
			case AVProLiveCameraPlugin.VideoFrameFormat.YUV_422_YVYU:
			case AVProLiveCameraPlugin.VideoFrameFormat.YUV_422_HDYC:
				_conversionMaterial.SetFloat("_TextureWidth", _finalTexture.width);
				_conversionMaterial.SetTexture("_MainTex", _buffers[0]._texture);
				break;
			case AVProLiveCameraPlugin.VideoFrameFormat.YUV_420_PLANAR_YV12:
			case AVProLiveCameraPlugin.VideoFrameFormat.YUV_420_PLANAR_I420:
				_conversionMaterial.SetFloat("_TextureWidth", _finalTexture.width);
				_conversionMaterial.SetTexture("_MainTex", _buffers[0]._texture);
				_conversionMaterial.SetTexture("_MainU", _buffers[1]._texture);
				_conversionMaterial.SetTexture("_MainV", _buffers[2]._texture);
				break;
			}
			SetFlip(_flipX, _flipY);
			return true;
		}
		Debug.LogWarning("[AVPro LiveCamera] couldn't create conversion materials");
		return false;
	}

	public bool Update()
	{
		bool flag = false;
		flag = UpdateTextures();
		if (flag)
		{
			if (!_deinterlaceMaterial)
			{
				if (DoFormatConversion(_finalTexture))
				{
					ValidPicture = true;
				}
			}
			else
			{
				RenderTexture temporary = RenderTexture.GetTemporary(_width, _height, 0, _finalTexture.format);
				if (DoFormatConversion(temporary))
				{
					DoDeinterlace(temporary, _finalTexture);
					ValidPicture = true;
				}
				RenderTexture.ReleaseTemporary(temporary);
			}
		}
		return ValidPicture && flag;
	}

	private bool UpdateTextures()
	{
		bool result = false;
		int lastFrameUploaded = AVProLiveCameraPlugin.GetLastFrameUploaded(_deviceIndex);
		if (_lastFrameUpdated != lastFrameUploaded)
		{
			_lastFrameUpdated = lastFrameUploaded;
			result = true;
		}
		return result;
	}

	public void Dispose()
	{
		ValidPicture = false;
		if (_conversionMaterial != null)
		{
			_conversionMaterial.mainTexture = null;
			UnityEngine.Object.Destroy(_conversionMaterial);
			_conversionMaterial = null;
		}
		if (_deinterlaceMaterial != null)
		{
			_deinterlaceMaterial.mainTexture = null;
			UnityEngine.Object.Destroy(_deinterlaceMaterial);
			_deinterlaceMaterial = null;
		}
		if (_finalTexture != null)
		{
			RenderTexture.ReleaseTemporary(_finalTexture);
			_finalTexture = null;
		}
		foreach (AVProLiveCameraPixelBuffer buffer in _buffers)
		{
			buffer.Close();
		}
		_buffers.Clear();
	}

	private void CreateBuffers()
	{
		foreach (AVProLiveCameraPixelBuffer buffer in _buffers)
		{
			buffer.Close();
		}
		_buffers.Clear();
		_requiresTextureCrop = false;
		switch (_format)
		{
		default:
			return;
		case AVProLiveCameraPlugin.VideoFrameFormat.RAW_BGRA32:
			if (_buffers.Count < 1)
			{
				_buffers.Add(new AVProLiveCameraPixelBuffer(_deviceIndex, _buffers.Count));
			}
			_buffers[0].Build(_width, _height);
			_requiresTextureCrop = _buffers[0].RequiresTextureCrop();
			return;
		case AVProLiveCameraPlugin.VideoFrameFormat.RAW_MONO8:
			if (_buffers.Count < 1)
			{
				_buffers.Add(new AVProLiveCameraPixelBuffer(_deviceIndex, _buffers.Count));
			}
			_buffers[0].Build(_width / 4, _height);
			_requiresTextureCrop = _buffers[0].RequiresTextureCrop();
			return;
		case AVProLiveCameraPlugin.VideoFrameFormat.YUV_422_YUY2:
		case AVProLiveCameraPlugin.VideoFrameFormat.YUV_422_UYVY:
		case AVProLiveCameraPlugin.VideoFrameFormat.YUV_422_YVYU:
		case AVProLiveCameraPlugin.VideoFrameFormat.YUV_422_HDYC:
			if (_buffers.Count < 1)
			{
				_buffers.Add(new AVProLiveCameraPixelBuffer(_deviceIndex, _buffers.Count));
			}
			_buffers[0].Build(_width / 2, _height);
			_requiresTextureCrop = _buffers[0].RequiresTextureCrop();
			return;
		case AVProLiveCameraPlugin.VideoFrameFormat.YUV_420_PLANAR_YV12:
		case AVProLiveCameraPlugin.VideoFrameFormat.YUV_420_PLANAR_I420:
			break;
		case AVProLiveCameraPlugin.VideoFrameFormat.RAW_RGB24:
			return;
		}
		while (_buffers.Count < 3)
		{
			_buffers.Add(new AVProLiveCameraPixelBuffer(_deviceIndex, _buffers.Count));
		}
		_buffers[0].Build(_width / 4, _height);
		_buffers[1].Build(_width / 8, _height / 2);
		_buffers[2].Build(_width / 8, _height / 2);
		_requiresTextureCrop = true;
	}

	private bool CreateMaterials()
	{
		Shader pixelConversionShader = AVProLiveCameraManager.Instance.GetPixelConversionShader(_format);
		if ((bool)pixelConversionShader)
		{
			if (_conversionMaterial != null && _conversionMaterial.shader != pixelConversionShader)
			{
				UnityEngine.Object.Destroy(_conversionMaterial);
				_conversionMaterial = null;
			}
			if (_conversionMaterial == null)
			{
				_conversionMaterial = new Material(pixelConversionShader);
				_conversionMaterial.name = "AVProLiveCamera-Material";
				_conversionMaterial.SetVector("_MainTex_ST2", new Vector4(1f, 1f, 0f, 0f));
			}
		}
		if (_deinterlace)
		{
			pixelConversionShader = AVProLiveCameraManager.Instance.GetDeinterlaceShader();
			if ((bool)pixelConversionShader)
			{
				if (_deinterlaceMaterial != null && _deinterlaceMaterial.shader != pixelConversionShader)
				{
					UnityEngine.Object.Destroy(_deinterlaceMaterial);
					_deinterlaceMaterial = null;
				}
				if (_deinterlaceMaterial == null)
				{
					_deinterlaceMaterial = new Material(pixelConversionShader);
					_deinterlaceMaterial.name = "AVProLiveCamera-DeinterlaceMaterial";
				}
			}
		}
		return _conversionMaterial != null && (!_deinterlace || _deinterlaceMaterial != null);
	}

	private void CreateRenderTexture()
	{
		if (_finalTexture != null && (_finalTexture.width != _width || _finalTexture.height != _height))
		{
			RenderTexture.ReleaseTemporary(_finalTexture);
			_finalTexture = null;
		}
		if (_finalTexture == null)
		{
			ValidPicture = false;
			_finalTexture = RenderTexture.GetTemporary(_width, _height, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.sRGB);
			_finalTexture.wrapMode = TextureWrapMode.Clamp;
			_finalTexture.filterMode = FilterMode.Bilinear;
			_finalTexture.name = "AVProLiveCamera-FinalTexture";
			_finalTexture.Create();
		}
	}

	private bool DoFormatConversion(RenderTexture target)
	{
		if (_buffers == null || _buffers.Count == 0)
		{
			return false;
		}
		target.DiscardContents();
		if (!_requiresTextureCrop)
		{
			RenderTexture active = RenderTexture.active;
			Graphics.Blit(_buffers[0]._texture, target, _conversionMaterial, 0);
			RenderTexture.active = active;
		}
		else
		{
			RenderTexture active2 = RenderTexture.active;
			RenderTexture.active = target;
			_conversionMaterial.SetPass(0);
			GL.PushMatrix();
			GL.LoadOrtho();
			DrawQuad();
			GL.PopMatrix();
			RenderTexture.active = active2;
		}
		return true;
	}

	private void DoDeinterlace(RenderTexture source, RenderTexture target)
	{
		target.DiscardContents();
		RenderTexture active = RenderTexture.active;
		Graphics.Blit(source, target, _deinterlaceMaterial);
		RenderTexture.active = active;
	}

	private void SetFlip(bool flipX, bool flipY)
	{
		_flipX = flipX;
		_flipY = flipY;
		if (_requiresTextureCrop)
		{
			if (_buffers != null)
			{
				BuildUVs(_flipX, _flipY);
			}
		}
		else if (_conversionMaterial != null)
		{
			Vector2 mainTextureScale = new Vector2(1f, 1f);
			Vector2 mainTextureOffset = new Vector2(0f, 0f);
			if (_flipX)
			{
				mainTextureScale = new Vector2(-1f, mainTextureScale.y);
				mainTextureOffset = new Vector2(1f, mainTextureOffset.y);
			}
			if (_flipY)
			{
				mainTextureScale = new Vector2(mainTextureScale.x, -1f);
				mainTextureOffset = new Vector2(mainTextureOffset.x, 1f);
			}
			_conversionMaterial.mainTextureScale = mainTextureScale;
			_conversionMaterial.mainTextureOffset = mainTextureOffset;
			_conversionMaterial.SetVector("_MainTex_ST2", new Vector4(mainTextureScale.x, mainTextureScale.y, mainTextureOffset.x, mainTextureOffset.y));
			if (_flipX)
			{
				_conversionMaterial.DisableKeyword("HORIZONTAL_FLIP_OFF");
				_conversionMaterial.EnableKeyword("HORIZONTAL_FLIP_ON");
			}
			else
			{
				_conversionMaterial.DisableKeyword("HORIZONTAL_FLIP_ON");
				_conversionMaterial.EnableKeyword("HORIZONTAL_FLIP_OFF");
			}
		}
	}

	private void BuildUVs(bool invertX, bool invertY)
	{
		_uvs = new Vector4[_buffers.Count];
		for (int i = 0; i < _buffers.Count; i++)
		{
			float num;
			float num2;
			if (invertX)
			{
				num = 1f;
				num2 = 0f;
			}
			else
			{
				num = 0f;
				num2 = 1f;
			}
			float num3;
			float num4;
			if (invertY)
			{
				num3 = 1f;
				num4 = 0f;
			}
			else
			{
				num3 = 0f;
				num4 = 1f;
			}
			if (_buffers[i]._innerWidth != _buffers[i]._texture.width)
			{
				float num5 = (float)_buffers[i]._innerWidth / (float)_buffers[i]._texture.width;
				num *= num5;
				num2 *= num5;
			}
			if (_buffers[i]._innerHeight != _buffers[i]._texture.height)
			{
				float num6 = (float)_buffers[i]._innerHeight / (float)_buffers[i]._texture.height;
				num3 *= num6;
				num4 *= num6;
			}
			ref Vector4 reference = ref _uvs[i];
			reference = new Vector4(num, num3, num2, num4);
		}
	}

	private void DrawQuad()
	{
		GL.Begin(7);
		for (int i = 0; i < _buffers.Count; i++)
		{
			GL.MultiTexCoord2(i, _uvs[i].x, _uvs[i].y);
		}
		GL.Vertex3(0f, 0f, 0.1f);
		for (int j = 0; j < _buffers.Count; j++)
		{
			GL.MultiTexCoord2(j, _uvs[j].z, _uvs[j].y);
		}
		GL.Vertex3(1f, 0f, 0.1f);
		for (int k = 0; k < _buffers.Count; k++)
		{
			GL.MultiTexCoord2(k, _uvs[k].z, _uvs[k].w);
		}
		GL.Vertex3(1f, 1f, 0.1f);
		for (int l = 0; l < _buffers.Count; l++)
		{
			GL.MultiTexCoord2(l, _uvs[l].x, _uvs[l].w);
		}
		GL.Vertex3(0f, 1f, 0.1f);
		GL.End();
	}
}
