using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class WaveformImage : MonoBehaviour
{
	[SerializeField]
	private BeatmapEditorScrollView _beatmapEditorScrollView;

	[SerializeField]
	private Image _image;

	private ScrollRect _scrollRect;

	private Texture2D _texture;

	private Color32[] _textureData;

	private float[] _sampleData;

	private float _samplingFrequency;

	private float _timeOffset;

	public void SetDataFromAudioClip(AudioClip audioClip)
	{
		if (audioClip == null)
		{
			if (_texture != null)
			{
				Object.Destroy(_texture);
			}
			_texture = null;
			_sampleData = null;
			_textureData = null;
			_image.materialForRendering.SetTexture("_MainTex2", null);
		}
		else
		{
			_sampleData = GetSampleData(audioClip);
			_samplingFrequency = (float)(_sampleData.Length / 2) / audioClip.length;
			DrawAtSongTime(_beatmapEditorScrollView.scrollPositionSongTime);
		}
	}

	public void ChangeParams(float timeOffset)
	{
		_timeOffset = timeOffset;
		DrawAtSongTime(_beatmapEditorScrollView.scrollPositionSongTime);
	}

	protected virtual void Awake()
	{
		_scrollRect = _beatmapEditorScrollView.scrollRect;
	}

	protected virtual void Start()
	{
		_scrollRect.onValueChanged.AddListener(ScrollViewDidScroll);
	}

	protected virtual void OnDestroy()
	{
		_scrollRect.onValueChanged.RemoveListener(ScrollViewDidScroll);
	}

	private void ScrollViewDidScroll(Vector2 normalizedPos)
	{
		DrawAtSongTime(_beatmapEditorScrollView.scrollPositionSongTime);
	}

	public void DrawAtSongTime(float songTime)
	{
		if (_sampleData == null)
		{
			return;
		}
		Rect rect = _image.rectTransform.rect;
		int num = (int)(RectTransformUtility.PixelAdjustRect(_image.rectTransform, _image.canvas.rootCanvas).height * _image.canvas.scaleFactor + 0.5f);
		if (_texture == null || _texture.width != num)
		{
			_textureData = new Color32[num];
			_texture = new Texture2D(_textureData.Length, 1, TextureFormat.RGBA32, mipmap: false, linear: true);
			_texture.wrapMode = TextureWrapMode.Clamp;
			_texture.filterMode = FilterMode.Point;
		}
		int num2 = (int)((songTime - _beatmapEditorScrollView.playHeadSongTimeOffset + _timeOffset) * _samplingFrequency) * 2;
		int num3 = (int)(_beatmapEditorScrollView.visibleAreaTimeDuration * _samplingFrequency);
		byte b = 0;
		byte b2 = 0;
		byte b3 = byte.MaxValue;
		byte b4 = byte.MaxValue;
		float num4 = 0f;
		int i = 0;
		bool flag = false;
		for (int j = 0; j < num3; j++)
		{
			num4 += (float)_textureData.Length / (float)num3;
			if (num4 > 1f)
			{
				if (flag)
				{
					ref Color32 reference = ref _textureData[i];
					reference = new Color32(b, b2, b3, b4);
				}
				else
				{
					ref Color32 reference2 = ref _textureData[i];
					reference2 = new Color32(127, 127, 127, 127);
				}
				num4 -= 1f;
				b = 0;
				b2 = 0;
				b3 = byte.MaxValue;
				b4 = byte.MaxValue;
				flag = false;
				i++;
				if (i >= _textureData.Length)
				{
					break;
				}
			}
			if (num2 < 0)
			{
				num2 += 2;
				continue;
			}
			flag = true;
			byte b5 = (byte)((_sampleData[num2] + 1f) / 2f * 255f);
			if (b5 > b)
			{
				b = b5;
			}
			if (b5 < b3)
			{
				b3 = b5;
			}
			num2++;
			b5 = (byte)((_sampleData[num2] + 1f) / 2f * 255f);
			if (b5 > b2)
			{
				b2 = b5;
			}
			if (b5 < b4)
			{
				b4 = b5;
			}
			num2++;
			if (num2 >= _sampleData.Length)
			{
				break;
			}
		}
		for (; i < _textureData.Length; i++)
		{
			ref Color32 reference3 = ref _textureData[i];
			reference3 = new Color32(127, 127, 127, 127);
		}
		_texture.SetPixels32(_textureData);
		_texture.Apply();
		_image.materialForRendering.SetTexture("_MainTex2", _texture);
	}

	private float[] GetSampleData(AudioClip audioClip)
	{
		float[] array = null;
		if (audioClip == null)
		{
			return null;
		}
		array = new float[audioClip.samples * 2];
		float[] array2 = new float[audioClip.samples * audioClip.channels];
		audioClip.GetData(array2, 0);
		if (audioClip.channels > 1)
		{
			for (int i = 0; i < audioClip.samples; i++)
			{
				array[i * 2] = array2[i * audioClip.channels];
				array[i * 2 + 1] = array2[i * audioClip.channels + 1];
			}
		}
		else
		{
			for (int j = 0; j < audioClip.samples; j++)
			{
				array[j * 2] = (array[j * 2 + 1] = array2[j]);
			}
		}
		return array;
	}
}
