using UnityEngine;

public class AVProLiveCameraPixelBuffer
{
	public Texture2D _texture;

	public int _innerWidth;

	public int _innerHeight;

	public int _width;

	public int _height;

	public TextureFormat _format;

	private int _deviceIndex;

	private int _bufferIndex;

	public AVProLiveCameraPixelBuffer(int deviceIndex, int bufferIndex)
	{
		_deviceIndex = deviceIndex;
		_bufferIndex = bufferIndex;
	}

	public bool Build(int width, int height, TextureFormat format = TextureFormat.RGBA32)
	{
		_width = width;
		_height = height;
		_format = format;
		if (CreateTexture())
		{
			AVProLiveCameraPlugin.SetTexturePointer(_deviceIndex, _bufferIndex, _texture.GetNativeTexturePtr());
			return true;
		}
		return false;
	}

	public void Close()
	{
		if (_texture != null)
		{
			Object.Destroy(_texture);
			_texture = null;
		}
	}

	public bool RequiresTextureCrop()
	{
		bool result = false;
		if (_texture != null)
		{
			result = _width != _texture.width || _height != _texture.height;
		}
		return result;
	}

	private bool CreateTexture()
	{
		int num = _width;
		int num2 = _height;
		_innerWidth = num;
		_innerHeight = num2;
		if (SystemInfo.npotSupport == NPOTSupport.None && (!Mathf.IsPowerOfTwo(_width) || !Mathf.IsPowerOfTwo(_height)))
		{
			num = Mathf.NextPowerOfTwo(num);
			num2 = Mathf.NextPowerOfTwo(num2);
		}
		if (_texture != null && (_texture.width != num || _texture.height != num2 || _texture.format != _format))
		{
			Object.Destroy(_texture);
			_texture = null;
		}
		if (_texture == null)
		{
			_texture = new Texture2D(num, num2, _format, false, true);
			_texture.wrapMode = TextureWrapMode.Clamp;
			_texture.filterMode = FilterMode.Point;
			_texture.name = "AVProLiveCamera-BufferTexture";
			_texture.Apply(false, true);
		}
		return _texture != null;
	}
}
