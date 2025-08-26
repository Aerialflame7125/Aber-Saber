using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class ScreenshotRecorder : MonoBehaviour
{
	public enum RecordingType
	{
		Sequence,
		Stereo360Sequence,
		Mono360Sequence,
		F10ForScreenshot,
		Interval,
		ScreenshotOnPause
	}

	[SerializeField]
	private string _folder = "ScreenshotFolder";

	[SerializeField]
	[NullAllowed]
	private Camera _camera;

	[SerializeField]
	private int _frameRate = 60;

	[SerializeField]
	private int _interval = 20;

	[SerializeField]
	private RecordingType _recordingType = RecordingType.F10ForScreenshot;

	[SerializeField]
	private int _superSize = 1;

	[SerializeField]
	private bool _pauseWithPButton = true;

	private int _counter;

	private float _originalTimeScale;

	private bool _paused;

	private int _frameNum;

	private RenderTexture _cubemapLeftEye;

	private RenderTexture _cubemapRighEye;

	private RenderTexture _equirectTexture;

	private void OnEnable()
	{
		if (_recordingType == RecordingType.Sequence || _recordingType == RecordingType.Stereo360Sequence || _recordingType == RecordingType.Mono360Sequence)
		{
			Time.captureFramerate = _frameRate;
		}
		Directory.CreateDirectory(_folder);
		_counter = _interval;
		_cubemapLeftEye = new RenderTexture(1024, 1024, 24);
		_cubemapLeftEye.dimension = TextureDimension.Cube;
		_cubemapRighEye = new RenderTexture(1024, 1024, 24);
		_cubemapRighEye.dimension = TextureDimension.Cube;
		_equirectTexture = new RenderTexture(1920, 2160, 0, RenderTextureFormat.ARGB32, RenderTextureReadWrite.Default);
	}

	private void OnDisable()
	{
		_cubemapLeftEye.Release();
		_cubemapRighEye.Release();
		_equirectTexture.Release();
		Object.Destroy(_cubemapLeftEye);
		Object.Destroy(_cubemapRighEye);
		Object.Destroy(_equirectTexture);
	}

	private void Update()
	{
		if (_recordingType == RecordingType.Sequence)
		{
			SaveScreenshot();
		}
		else if (_recordingType != RecordingType.Stereo360Sequence && _recordingType != RecordingType.Mono360Sequence)
		{
			if (_recordingType == RecordingType.Interval && _counter == 0)
			{
				SaveScreenshot();
				_counter = _interval;
			}
			else if (_recordingType == RecordingType.F10ForScreenshot && Input.GetKeyDown(KeyCode.F10))
			{
				SaveScreenshot();
			}
		}
		if (_counter > 0)
		{
			_counter--;
		}
		if (_pauseWithPButton && Input.GetKeyDown(KeyCode.P))
		{
			_paused = !_paused;
			if (_paused)
			{
				_originalTimeScale = Time.timeScale;
				Time.timeScale = 0f;
			}
			else
			{
				Time.timeScale = _originalTimeScale;
			}
		}
	}

	private void OnApplicationFocus(bool hasFocus)
	{
		if (_recordingType == RecordingType.ScreenshotOnPause && hasFocus)
		{
			SaveScreenshot();
		}
	}

	private void SaveScreenshot()
	{
		string text = $"{_folder}/{_frameNum:D04} shot.png";
		ScreenCapture.CaptureScreenshot(text, _superSize);
		Debug.Log("Screenshot saved to \"" + text + "\"");
		_frameNum++;
	}

	private void SaveTextureScreenshot(Texture2D tex)
	{
		string text = $"{_folder}/{_frameNum:D04} shot.png";
		byte[] bytes = tex.EncodeToPNG();
		File.WriteAllBytes(text, bytes);
		Debug.Log("Screenshot saved to \"" + text + "\"");
		_frameNum++;
	}

	private Texture2D ConvertRenderTexture(RenderTexture renderTexture)
	{
		Texture2D texture2D = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.RGB24, mipmap: false);
		RenderTexture.active = renderTexture;
		texture2D.ReadPixels(new Rect(0f, 0f, renderTexture.width, renderTexture.height), 0, 0);
		texture2D.Apply();
		return texture2D;
	}
}
