using System;
using System.Collections.Generic;
using UnityEngine;

public class AVProLiveCameraDevice : IDisposable
{
	public enum SettingsEnum
	{
		Brightness = 0,
		Contrast = 1,
		Hue = 2,
		Saturation = 3,
		Sharpness = 4,
		Gamma = 5,
		ColorEnable = 6,
		WhiteBalance = 7,
		BacklightCompensation = 8,
		Gain = 9,
		DigitalMultiplier = 10,
		DigitalMultiplierLimit = 11,
		WhiteBalanceComponent = 12,
		PowerlineFrequency = 13,
		Pan = 1000,
		Tilt = 1001,
		Roll = 1002,
		Zoom = 1003,
		Exposure = 1004,
		Iris = 1005,
		Focus = 1006,
		ScanMode = 1007,
		Privacy = 1008,
		PanTilt = 1009,
		PanRelative = 1010,
		TiltRelative = 1011,
		RollRelative = 1012,
		ZoomRelative = 1013,
		ExposureRelative = 1014,
		IrisRelative = 1015,
		FocusRelative = 1016,
		PanTiltRelative = 1017,
		FocalLength = 1018,
		AutoExposurePriority = 1019,
		Logitech_Version = 2000,
		Logitech_DigitalPan = 2001,
		Logitech_DigitalTilt = 2002,
		Logitech_DigitalZoom = 2003,
		Logitech_DigitalPanTiltZoom = 2004,
		Logitech_ExposureTime = 2005,
		Logitech_FaceTracking = 2006,
		Logitech_LED = 2007,
		Logitech_FindFace = 2008
	}

	private const int MaxVideoResolution = 16384;

	private int _deviceIndex;

	private List<AVProLiveCameraDeviceMode> _modes;

	private List<AVProLiveCameraSettingBase> _settings;

	private Dictionary<int, AVProLiveCameraSettingBase> _settingsByType;

	private List<string> _videoInputs;

	private AVProLiveCameraFormatConverter _formatConverter;

	private int _width;

	private int _height;

	private float _frameRate;

	private long _frameDurationHNS;

	private string _format;

	private string _deviceFormat;

	private int _frameCount;

	private float _startFrameTime;

	private int _lastModeIndex = -1;

	private int _lastVideoInputIndex = -1;

	private bool _isActive;

	private bool _isTopDown;

	private bool _flipX;

	private bool _flipY;

	public bool IsActive
	{
		get
		{
			return _isActive;
		}
		set
		{
			_isActive = value;
			if (_deviceIndex >= 0)
			{
				AVProLiveCameraPlugin.SetActive(_deviceIndex, _isActive);
			}
		}
	}

	public int DeviceIndex
	{
		get
		{
			return _deviceIndex;
		}
	}

	public string Name { get; private set; }

	public string GUID { get; private set; }

	public int NumModes
	{
		get
		{
			return _modes.Count;
		}
	}

	public int NumSettings
	{
		get
		{
			return _settings.Count;
		}
	}

	public int NumVideoInputs
	{
		get
		{
			return _videoInputs.Count;
		}
	}

	public Texture OutputTexture
	{
		get
		{
			if (_formatConverter != null && _formatConverter.ValidPicture)
			{
				return _formatConverter.OutputTexture;
			}
			return null;
		}
	}

	public int CurrentWidth
	{
		get
		{
			return _width;
		}
	}

	public int CurrentHeight
	{
		get
		{
			return _height;
		}
	}

	public string CurrentFormat
	{
		get
		{
			return _format;
		}
	}

	public string CurrentDeviceFormat
	{
		get
		{
			return _deviceFormat;
		}
	}

	public float CurrentFrameRate
	{
		get
		{
			return _frameRate;
		}
	}

	public long CurrentFrameDurationHNS
	{
		get
		{
			return _frameDurationHNS;
		}
	}

	public bool IsRunning { get; private set; }

	public bool IsPaused { get; private set; }

	public bool IsPicture { get; private set; }

	public float CaptureFPS { get; private set; }

	public float DisplayFPS { get; private set; }

	public float CaptureFramesDropped { get; private set; }

	public int FramesTotal { get; private set; }

	public bool Deinterlace { get; set; }

	public bool IsConnected { get; private set; }

	public bool FlipX
	{
		get
		{
			return _flipX;
		}
		set
		{
			_flipX = value;
			if (_formatConverter != null)
			{
				_formatConverter.FlipX = _flipX;
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
			_flipY = value;
			if (_formatConverter != null)
			{
				_formatConverter.FlipY = _isTopDown != _flipY;
			}
		}
	}

	public bool UpdateHotSwap { get; set; }

	public bool UpdateFrameRates { get; set; }

	public bool UpdateSettings { get; set; }

	public AVProLiveCameraDevice(string name, string guid, int index)
	{
		IsRunning = false;
		IsPaused = true;
		IsPicture = false;
		IsConnected = true;
		UpdateHotSwap = false;
		UpdateFrameRates = false;
		UpdateSettings = false;
		Name = name;
		GUID = guid;
		_deviceIndex = index;
		_modes = new List<AVProLiveCameraDeviceMode>(64);
		_videoInputs = new List<string>(8);
		_settings = new List<AVProLiveCameraSettingBase>(16);
		_settingsByType = new Dictionary<int, AVProLiveCameraSettingBase>(16);
		_formatConverter = new AVProLiveCameraFormatConverter(_deviceIndex);
		EnumModes();
		EnumVideoInputs();
		EnumVideoSettings();
	}

	public void Dispose()
	{
		if (_formatConverter != null)
		{
			_formatConverter.Dispose();
			_formatConverter = null;
		}
	}

	public bool CanShowConfigWindow()
	{
		return AVProLiveCameraPlugin.HasDeviceConfigWindow(_deviceIndex);
	}

	public bool ShowConfigWindow()
	{
		return AVProLiveCameraPlugin.ShowDeviceConfigWindow(_deviceIndex);
	}

	public bool Start(AVProLiveCameraDeviceMode mode, int videoInputIndex = -1)
	{
		int num = -1;
		if (mode != null)
		{
			num = mode.InternalIndex;
		}
		if (AVProLiveCameraPlugin.StartDevice(_deviceIndex, num, videoInputIndex))
		{
			int num2 = -1;
			if (mode != null)
			{
				for (int i = 0; i < _modes.Count; i++)
				{
					if (_modes[i] == mode)
					{
						num2 = i;
						break;
					}
				}
			}
			Debug.Log("[AVProLiveCamera] Started device using mode index " + num2 + " (internal index " + num + ")");
			int width = AVProLiveCameraPlugin.GetWidth(_deviceIndex);
			int height = AVProLiveCameraPlugin.GetHeight(_deviceIndex);
			AVProLiveCameraPlugin.VideoFrameFormat format = (AVProLiveCameraPlugin.VideoFrameFormat)AVProLiveCameraPlugin.GetFormat(_deviceIndex);
			_width = width;
			_height = height;
			_format = format.ToString();
			_deviceFormat = AVProLiveCameraPlugin.GetDeviceFormat(_deviceIndex);
			_frameRate = AVProLiveCameraPlugin.GetFrameRate(_deviceIndex);
			_frameDurationHNS = AVProLiveCameraPlugin.GetFrameDurationHNS(_deviceIndex);
			if (width <= 0 || width > 16384 || height <= 0 || height > 16384)
			{
				Debug.LogWarning("[AVProLiveCamera] invalid width or height");
				Close();
				return false;
			}
			_isTopDown = AVProLiveCameraPlugin.IsFrameTopDown(_deviceIndex);
			if (!_formatConverter.Build(width, height, format, _flipX, _isTopDown != _flipY, Deinterlace))
			{
				Debug.LogWarning("[AVProLiveCamera] unable to convert camera format");
				Close();
				return false;
			}
			IsActive = true;
			IsRunning = false;
			IsPicture = false;
			IsPaused = true;
			_lastModeIndex = num2;
			_lastVideoInputIndex = videoInputIndex;
			Play();
			return IsRunning;
		}
		Debug.LogWarning("[AVProLiveCamera] unable to start camera");
		Close();
		return false;
	}

	public bool Start(int modeIndex = -1, int videoInputIndex = -1)
	{
		AVProLiveCameraDeviceMode mode = null;
		if (modeIndex >= 0)
		{
			if (modeIndex < _modes.Count)
			{
				mode = _modes[modeIndex];
			}
			else
			{
				Debug.LogError("[AVProLiveCamera] Mode index out of range, using default resolution");
			}
		}
		return Start(mode, videoInputIndex);
	}

	public void SetVideoInputByIndex(int index)
	{
		AVProLiveCameraPlugin.SetVideoInputByIndex(_deviceIndex, index);
	}

	public void Play()
	{
		if (IsActive && (IsPaused || !IsRunning))
		{
			if (AVProLiveCameraPlugin.Play(_deviceIndex))
			{
				ResetDisplayFPS();
				IsPaused = false;
				IsRunning = true;
			}
			else
			{
				Debug.LogWarning("[AVProLiveCamera] failed to play camera");
			}
		}
	}

	public void Pause()
	{
		if (IsActive && IsRunning)
		{
			AVProLiveCameraPlugin.Pause(_deviceIndex);
			IsPaused = true;
		}
	}

	public void Stop()
	{
		if (IsActive && IsRunning)
		{
			AVProLiveCameraPlugin.Stop(_deviceIndex);
			IsRunning = false;
			IsPaused = true;
		}
	}

	private void Update_HotSwap()
	{
		bool flag = AVProLiveCameraPlugin.IsDeviceConnected(_deviceIndex);
		if (IsConnected == flag)
		{
			return;
		}
		if (!flag)
		{
			Debug.Log("[AVProLiveCamera] device #" + _deviceIndex + " '" + Name + "' disconnected");
			Pause();
		}
		else
		{
			Debug.Log("[AVProLiveCamera] device #" + _deviceIndex + " '" + Name + "' reconnected");
			if (IsRunning)
			{
				Start(_lastModeIndex, _lastVideoInputIndex);
			}
		}
		IsConnected = flag;
	}

	private void Update_FrameRates()
	{
		CaptureFPS = AVProLiveCameraPlugin.GetCaptureFrameRate(_deviceIndex);
		CaptureFramesDropped = AVProLiveCameraPlugin.GetCaptureFramesDropped(_deviceIndex);
	}

	public void Update(bool force)
	{
		if (UpdateHotSwap)
		{
			Update_HotSwap();
		}
		if (IsRunning)
		{
			if (_formatConverter != null && _formatConverter.Update())
			{
				UpdateDisplayFPS();
			}
			if (UpdateFrameRates)
			{
				Update_FrameRates();
			}
			if (UpdateSettings)
			{
				Update_Settings();
			}
		}
	}

	public void Update_Settings()
	{
		for (int i = 0; i < _settings.Count; i++)
		{
			_settings[i].Update();
		}
	}

	protected void ResetDisplayFPS()
	{
		_frameCount = 0;
		FramesTotal = 0;
		DisplayFPS = 0f;
		_startFrameTime = 0f;
	}

	public void UpdateDisplayFPS()
	{
		_frameCount++;
		FramesTotal++;
		float realtimeSinceStartup = Time.realtimeSinceStartup;
		float num = realtimeSinceStartup - _startFrameTime;
		if (num >= 1f)
		{
			DisplayFPS = (float)_frameCount / num;
			_frameCount = 0;
			_startFrameTime = realtimeSinceStartup;
		}
	}

	public void Close()
	{
		ResetDisplayFPS();
		_width = (_height = 0);
		_lastVideoInputIndex = (_lastModeIndex = -1);
		_frameDurationHNS = 0L;
		_frameRate = 0f;
		_format = (_deviceFormat = string.Empty);
		IsRunning = false;
		IsPaused = true;
		AVProLiveCameraPlugin.StopDevice(_deviceIndex);
	}

	public string GetVideoInputName(int index)
	{
		string result = string.Empty;
		if (index >= 0 && index < _videoInputs.Count)
		{
			result = _videoInputs[index];
		}
		return result;
	}

	public AVProLiveCameraDeviceMode GetMode(int index)
	{
		AVProLiveCameraDeviceMode result = null;
		if (index >= 0 && index < _modes.Count)
		{
			result = _modes[index];
		}
		return result;
	}

	public AVProLiveCameraDeviceMode GetHighestResolutionMode(float minimumFrameRate)
	{
		AVProLiveCameraDeviceMode result = null;
		float num = 0f;
		for (int i = 0; i < NumModes; i++)
		{
			AVProLiveCameraDeviceMode mode = GetMode(i);
			if (mode.FPS >= minimumFrameRate && (float)(mode.Width * mode.Height) > num)
			{
				result = mode;
				num = mode.Width * mode.Height;
			}
		}
		return result;
	}

	public AVProLiveCameraDeviceMode GetClosestMode(int width, int height, bool maintainAspectRatio, bool highestFrameRate)
	{
		AVProLiveCameraDeviceMode aVProLiveCameraDeviceMode = null;
		if (width <= 0 || height <= 0)
		{
			return aVProLiveCameraDeviceMode;
		}
		List<AVProLiveCameraDeviceMode> list = new List<AVProLiveCameraDeviceMode>();
		for (int i = 0; i < NumModes; i++)
		{
			AVProLiveCameraDeviceMode mode = GetMode(i);
			if (mode.Width == width && mode.Height == height)
			{
				list.Add(mode);
			}
		}
		if (list.Count == 0)
		{
			float b = (float)width * (float)height;
			int num = width * height;
			float num2 = float.MaxValue;
			for (int j = 0; j < NumModes; j++)
			{
				AVProLiveCameraDeviceMode mode2 = GetMode(j);
				float a = (float)mode2.Width / (float)mode2.Height;
				bool flag = true;
				if (maintainAspectRatio && !Mathf.Approximately(a, b))
				{
					flag = false;
				}
				if (flag)
				{
					int num3 = mode2.Width * mode2.Height;
					int num4 = Mathf.Abs(num - num3);
					if ((float)num4 < num2)
					{
						aVProLiveCameraDeviceMode = mode2;
						num2 = num4;
					}
				}
			}
			if (aVProLiveCameraDeviceMode != null)
			{
				for (int k = 0; k < NumModes; k++)
				{
					AVProLiveCameraDeviceMode mode3 = GetMode(k);
					if (mode3.Width == aVProLiveCameraDeviceMode.Width && mode3.Height == aVProLiveCameraDeviceMode.Height)
					{
						list.Add(mode3);
					}
				}
				aVProLiveCameraDeviceMode = null;
			}
		}
		if (list.Count > 0)
		{
			if (list.Count == 1)
			{
				aVProLiveCameraDeviceMode = list[0];
			}
			else if (highestFrameRate)
			{
				float num5 = 0f;
				for (int l = 0; l < list.Count; l++)
				{
					AVProLiveCameraDeviceMode aVProLiveCameraDeviceMode2 = list[l];
					if (aVProLiveCameraDeviceMode2.FPS > num5)
					{
						num5 = aVProLiveCameraDeviceMode2.FPS;
						aVProLiveCameraDeviceMode = aVProLiveCameraDeviceMode2;
					}
				}
			}
			else
			{
				string[] array = new string[9] { "YUV_UYVY_HDYC", "YUV_UYVY", "YUV_YVYU", "YUV_YUY2", "ARGB32", "RGB32", "RGB24", "MJPG", "UNKNOWN" };
				int num6 = 100;
				for (int m = 0; m < list.Count; m++)
				{
					int num7 = Array.IndexOf(array, list[m].Format);
					if (num7 >= 0 && num7 < num6)
					{
						aVProLiveCameraDeviceMode = list[m];
						num6 = num7;
					}
				}
			}
		}
		if (aVProLiveCameraDeviceMode != null)
		{
		}
		return aVProLiveCameraDeviceMode;
	}

	public AVProLiveCameraSettingBase GetVideoSettingByType(SettingsEnum type)
	{
		AVProLiveCameraSettingBase value = null;
		if (!_settingsByType.TryGetValue((int)type, out value))
		{
			value = null;
		}
		return value;
	}

	public AVProLiveCameraSettingBase GetVideoSettingByIndex(int index)
	{
		AVProLiveCameraSettingBase result = null;
		if (index >= 0 && index < _settings.Count)
		{
			result = _settings[index];
		}
		return result;
	}

	private void SortModes()
	{
		_modes.Sort(delegate(AVProLiveCameraDeviceMode x, AVProLiveCameraDeviceMode y)
		{
			int num = 0;
			if (x.Width * x.Height < y.Width * y.Height)
			{
				num = -1;
			}
			else if (y.Width * y.Height < x.Width * x.Height)
			{
				num = 1;
			}
			if (num == 0)
			{
				if (x.FPS < y.FPS)
				{
					num = -1;
				}
				else if (y.FPS < x.FPS)
				{
					num = 1;
				}
			}
			if (num == 0)
			{
				num = x.Format.CompareTo(y.Format);
			}
			return num;
		});
	}

	private void EnumModes()
	{
		int numModes = AVProLiveCameraPlugin.GetNumModes(_deviceIndex);
		for (int i = 0; i < numModes; i++)
		{
			int width;
			int height;
			float fps;
			string format;
			if (AVProLiveCameraPlugin.GetModeInfo(_deviceIndex, i, out width, out height, out fps, out format))
			{
				AVProLiveCameraDeviceMode item = new AVProLiveCameraDeviceMode(this, i, width, height, fps, format.ToString());
				_modes.Add(item);
			}
		}
		SortModes();
	}

	private void EnumVideoInputs()
	{
		int numVideoInputs = AVProLiveCameraPlugin.GetNumVideoInputs(_deviceIndex);
		for (int i = 0; i < numVideoInputs; i++)
		{
			string name;
			if (AVProLiveCameraPlugin.GetVideoInputName(_deviceIndex, i, out name))
			{
				_videoInputs.Add(name);
			}
		}
	}

	private void EnumVideoSettings()
	{
		int numDeviceVideoSettings = AVProLiveCameraPlugin.GetNumDeviceVideoSettings(_deviceIndex);
		for (int i = 0; i < numDeviceVideoSettings; i++)
		{
			int settingType;
			int dataType;
			string name;
			bool canAutomatic;
			if (!AVProLiveCameraPlugin.GetDeviceVideoSettingInfo(_deviceIndex, i, out settingType, out dataType, out name, out canAutomatic))
			{
				continue;
			}
			AVProLiveCameraSettingBase aVProLiveCameraSettingBase = null;
			switch (dataType)
			{
			case 0:
			{
				bool defaultValue2;
				bool currentValue2;
				bool isAutomatic2;
				if (AVProLiveCameraPlugin.GetDeviceVideoSettingBoolean(_deviceIndex, i, out defaultValue2, out currentValue2, out isAutomatic2))
				{
					aVProLiveCameraSettingBase = new AVProLiveCameraSettingBoolean(_deviceIndex, i, settingType, name, canAutomatic, isAutomatic2, defaultValue2, currentValue2);
				}
				break;
			}
			case 1:
			{
				float defaultValue;
				float currentValue;
				float minValue;
				float maxValue;
				bool isAutomatic;
				if (AVProLiveCameraPlugin.GetDeviceVideoSettingFloat(_deviceIndex, i, out defaultValue, out currentValue, out minValue, out maxValue, out isAutomatic))
				{
					aVProLiveCameraSettingBase = new AVProLiveCameraSettingFloat(_deviceIndex, i, settingType, name, canAutomatic, isAutomatic, defaultValue, currentValue, minValue, maxValue);
				}
				break;
			}
			}
			if (aVProLiveCameraSettingBase != null)
			{
				_settings.Add(aVProLiveCameraSettingBase);
				_settingsByType.Add(settingType, aVProLiveCameraSettingBase);
			}
		}
	}
}
