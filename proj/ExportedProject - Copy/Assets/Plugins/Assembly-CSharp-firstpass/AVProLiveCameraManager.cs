using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("AVPro Live Camera/Manager (required)")]
public class AVProLiveCameraManager : MonoBehaviour
{
	private static AVProLiveCameraManager _instance;

	public bool _supportHotSwapping;

	public bool _supportInternalFormatConversion = true;

	public Shader _shaderBGRA32;

	public Shader _shaderMONO8;

	public Shader _shaderYUY2;

	public Shader _shaderUYVY;

	public Shader _shaderYVYU;

	public Shader _shaderHDYC;

	public Shader _shaderI420;

	public Shader _shaderYV12;

	public Shader _shaderDeinterlace;

	private bool _isInitialised;

	private List<AVProLiveCameraDevice> _devices;

	public static AVProLiveCameraManager Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = (AVProLiveCameraManager)UnityEngine.Object.FindObjectOfType(typeof(AVProLiveCameraManager));
				if (_instance == null)
				{
					Debug.LogError("[AVProLiveCamera] AVProLiveCameraManager component required");
					return null;
				}
				if (!_instance._isInitialised)
				{
					_instance.Init();
				}
			}
			return _instance;
		}
	}

	public int NumDevices
	{
		get
		{
			if (_devices != null)
			{
				return _devices.Count;
			}
			return 0;
		}
	}

	private void Start()
	{
		if (!_isInitialised)
		{
			_instance = this;
			Init();
		}
	}

	private void OnDestroy()
	{
		Deinit();
	}

	protected bool Init()
	{
		try
		{
			if (!AVProLiveCameraPlugin.Init(_supportInternalFormatConversion))
			{
				Debug.LogError("[AVProLiveCamera] failed to initialise.");
				base.enabled = false;
				Deinit();
				return false;
			}
			Debug.Log("[AVProLiveCamera] version " + AVProLiveCameraPlugin.GetPluginVersionString() + " initialised");
		}
		catch (DllNotFoundException ex)
		{
			Debug.Log("[AVProLiveCamera] Unity couldn't find the DLL, did you move the 'Plugins' folder to the root of your project?");
			throw ex;
		}
		GetConversionMethod();
		EnumDevices();
		_isInitialised = true;
		return _isInitialised;
	}

	private void GetConversionMethod()
	{
		bool flag = false;
		if (SystemInfo.graphicsDeviceVersion.StartsWith("Direct3D 11"))
		{
			flag = true;
		}
		if (flag)
		{
			Shader.DisableKeyword("SWAP_RED_BLUE_OFF");
			Shader.EnableKeyword("SWAP_RED_BLUE_ON");
		}
		else
		{
			Shader.DisableKeyword("SWAP_RED_BLUE_ON");
			Shader.EnableKeyword("SWAP_RED_BLUE_OFF");
		}
		Shader.DisableKeyword("AVPRO_GAMMACORRECTION");
		Shader.EnableKeyword("AVPRO_GAMMACORRECTION_OFF");
		if (QualitySettings.activeColorSpace == ColorSpace.Linear)
		{
			Shader.DisableKeyword("AVPRO_GAMMACORRECTION_OFF");
			Shader.EnableKeyword("AVPRO_GAMMACORRECTION");
		}
	}

	private void Update()
	{
		if (_supportHotSwapping && AVProLiveCameraPlugin.UpdateDevicesConnected())
		{
			AddNewDevices();
		}
		GL.IssuePluginEvent(AVProLiveCameraPlugin.GetRenderEventFunc(), 262275072);
	}

	private void AddNewDevices()
	{
		bool flag = false;
		int numDevices = AVProLiveCameraPlugin.GetNumDevices();
		for (int i = 0; i < numDevices; i++)
		{
			string text;
			if (!AVProLiveCameraPlugin.GetDeviceGUID(i, out text))
			{
				continue;
			}
			AVProLiveCameraDevice aVProLiveCameraDevice = FindDeviceWithGUID(text);
			string text2;
			if (aVProLiveCameraDevice == null && AVProLiveCameraPlugin.GetDeviceName(i, out text2))
			{
				int numModes = AVProLiveCameraPlugin.GetNumModes(i);
				if (numModes > 0)
				{
					aVProLiveCameraDevice = new AVProLiveCameraDevice(text2.ToString(), text.ToString(), i);
					_devices.Add(aVProLiveCameraDevice);
					flag = true;
				}
			}
		}
		if (flag)
		{
			SendMessage("NewDeviceAdded", null, SendMessageOptions.DontRequireReceiver);
		}
	}

	private AVProLiveCameraDevice FindDeviceWithGUID(string guid)
	{
		AVProLiveCameraDevice result = null;
		foreach (AVProLiveCameraDevice device in _devices)
		{
			if (device.GUID == guid)
			{
				result = device;
				break;
			}
		}
		return result;
	}

	private void EnumDevices()
	{
		ClearDevices();
		_devices = new List<AVProLiveCameraDevice>(8);
		int numDevices = AVProLiveCameraPlugin.GetNumDevices();
		for (int i = 0; i < numDevices; i++)
		{
			string text;
			string text2;
			if (AVProLiveCameraPlugin.GetDeviceName(i, out text) && AVProLiveCameraPlugin.GetDeviceGUID(i, out text2))
			{
				int numModes = AVProLiveCameraPlugin.GetNumModes(i);
				if (numModes > 0)
				{
					AVProLiveCameraDevice item = new AVProLiveCameraDevice(text.ToString(), text2.ToString(), i);
					_devices.Add(item);
				}
			}
		}
	}

	private void ClearDevices()
	{
		if (_devices != null)
		{
			for (int i = 0; i < _devices.Count; i++)
			{
				_devices[i].Close();
				_devices[i].Dispose();
			}
			_devices.Clear();
			_devices = null;
		}
	}

	public void Deinit()
	{
		ClearDevices();
		_instance = null;
		_isInitialised = false;
		AVProLiveCameraPlugin.Deinit();
	}

	public Shader GetDeinterlaceShader()
	{
		return _shaderDeinterlace;
	}

	public Shader GetPixelConversionShader(AVProLiveCameraPlugin.VideoFrameFormat format)
	{
		Shader result = null;
		switch (format)
		{
		case AVProLiveCameraPlugin.VideoFrameFormat.YUV_422_YUY2:
			result = _shaderYUY2;
			break;
		case AVProLiveCameraPlugin.VideoFrameFormat.YUV_422_UYVY:
			result = _shaderUYVY;
			break;
		case AVProLiveCameraPlugin.VideoFrameFormat.YUV_422_YVYU:
			result = _shaderYVYU;
			break;
		case AVProLiveCameraPlugin.VideoFrameFormat.YUV_422_HDYC:
			result = _shaderHDYC;
			break;
		case AVProLiveCameraPlugin.VideoFrameFormat.RAW_BGRA32:
			result = _shaderBGRA32;
			break;
		case AVProLiveCameraPlugin.VideoFrameFormat.RAW_MONO8:
			result = _shaderMONO8;
			break;
		case AVProLiveCameraPlugin.VideoFrameFormat.YUV_420_PLANAR_I420:
			result = _shaderI420;
			break;
		case AVProLiveCameraPlugin.VideoFrameFormat.YUV_420_PLANAR_YV12:
			result = _shaderYV12;
			break;
		default:
			Debug.LogError("[AVProLiveCamera] Unknown video format '" + format);
			break;
		}
		return result;
	}

	public AVProLiveCameraDevice GetDevice(int index)
	{
		AVProLiveCameraDevice result = null;
		if (index >= 0 && index < _devices.Count)
		{
			result = _devices[index];
		}
		return result;
	}

	public AVProLiveCameraDevice GetDevice(string name)
	{
		AVProLiveCameraDevice result = null;
		int numDevices = NumDevices;
		for (int i = 0; i < numDevices; i++)
		{
			AVProLiveCameraDevice device = GetDevice(i);
			if (device.Name == name)
			{
				result = device;
				break;
			}
		}
		return result;
	}
}
