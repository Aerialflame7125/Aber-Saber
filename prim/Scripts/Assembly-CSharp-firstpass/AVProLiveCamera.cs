using System;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("AVPro Live Camera/Live Camera")]
public class AVProLiveCamera : MonoBehaviour
{
	public enum SelectDeviceBy
	{
		Default,
		Name,
		Index
	}

	public enum SelectModeBy
	{
		Default,
		Resolution,
		Index
	}

	protected AVProLiveCameraDevice _device;

	protected AVProLiveCameraDeviceMode _mode;

	protected int _videoInput = -1;

	public SelectDeviceBy _deviceSelection;

	public List<string> _desiredDeviceNames = new List<string>(4);

	public int _desiredDeviceIndex;

	public SelectModeBy _modeSelection;

	public List<Vector2> _desiredResolutions = new List<Vector2>(2);

	public int _desiredModeIndex = -1;

	public bool _maintainAspectRatio;

	public bool _highestFrameRate = true;

	public SelectDeviceBy _videoInputSelection;

	public List<AVProLiveCameraPlugin.VideoInput> _desiredVideoInputs = new List<AVProLiveCameraPlugin.VideoInput>(4);

	public int _desiredVideoInputIndex;

	public bool _playOnStart = true;

	public bool _deinterlace;

	public bool _flipX;

	public bool _flipY;

	public bool _updateHotSwap;

	public bool _updateFrameRates;

	public bool _updateSettings;

	private int _lastFrameCount;

	public AVProLiveCameraDevice Device => _device;

	public Texture OutputTexture
	{
		get
		{
			if (_device != null)
			{
				return _device.OutputTexture;
			}
			return null;
		}
	}

	private void Reset()
	{
		_videoInput = -1;
		_mode = null;
		_device = null;
		_flipX = (_flipY = false);
		_deviceSelection = SelectDeviceBy.Default;
		_modeSelection = SelectModeBy.Default;
		_videoInputSelection = SelectDeviceBy.Default;
		_desiredDeviceNames = new List<string>(4);
		_desiredResolutions = new List<Vector2>(2);
		_desiredVideoInputs = new List<AVProLiveCameraPlugin.VideoInput>(4);
		_desiredDeviceNames.Add("Logitech HD Pro Webcam C920");
		_desiredDeviceNames.Add("Decklink Video Capture");
		_desiredDeviceNames.Add("Logitech Webcam Pro 9000");
		_desiredResolutions.Add(new Vector2(1920f, 1080f));
		_desiredResolutions.Add(new Vector2(1280f, 720f));
		_desiredResolutions.Add(new Vector2(640f, 360f));
		_desiredResolutions.Add(new Vector2(640f, 480f));
		_desiredVideoInputs.Add(AVProLiveCameraPlugin.VideoInput.Video_Serial_Digital);
		_desiredVideoInputs.Add(AVProLiveCameraPlugin.VideoInput.Video_SVideo);
		_desiredVideoInputIndex = 0;
		_maintainAspectRatio = false;
		_highestFrameRate = true;
		_desiredModeIndex = -1;
		_desiredDeviceIndex = 0;
	}

	public void Start()
	{
		if (null == UnityEngine.Object.FindObjectOfType(typeof(AVProLiveCameraManager)))
		{
			throw new Exception("You need to add AVProLiveCameraManager component to your scene or change the script execution ordering of AVProLiveCameraManager.");
		}
		SelectDeviceAndMode();
		if (_playOnStart)
		{
			Begin();
		}
	}

	public void Begin()
	{
		SelectDeviceAndMode();
		if (_device != null)
		{
			_device.Deinterlace = _deinterlace;
			_device.FlipX = _flipX;
			_device.FlipY = _flipY;
			if (!_device.Start(_mode, _videoInput))
			{
				Debug.LogWarning("[AVPro Live Camera] Device failed to start.");
				_device.Close();
				_device = null;
				_mode = null;
				_videoInput = -1;
			}
		}
	}

	private void Update()
	{
		if (_device != null)
		{
			if (_flipX != _device.FlipX)
			{
				_device.FlipX = _flipX;
			}
			if (_flipY != _device.FlipY)
			{
				_device.FlipY = _flipY;
			}
			_device.UpdateHotSwap = _updateHotSwap;
			_device.UpdateFrameRates = _updateFrameRates;
			_device.UpdateSettings = _updateSettings;
		}
	}

	private void OnRenderObject()
	{
		if (_lastFrameCount != Time.frameCount)
		{
			_lastFrameCount = Time.frameCount;
			if (_device != null)
			{
				_device.Update(force: false);
			}
		}
	}

	public void OnDestroy()
	{
		if (_device != null)
		{
			_device.Close();
		}
		_device = null;
	}

	public void SelectDeviceAndMode()
	{
		_device = null;
		_mode = null;
		_videoInput = -1;
		_device = SelectDevice();
		if (_device != null)
		{
			_mode = SelectMode();
			_videoInput = SelectVideoInputIndex();
		}
		else
		{
			Debug.LogWarning("[AVProLiveCamera] Could not find the device.");
		}
	}

	private AVProLiveCameraDeviceMode SelectMode()
	{
		AVProLiveCameraDeviceMode aVProLiveCameraDeviceMode = null;
		switch (_modeSelection)
		{
		default:
			aVProLiveCameraDeviceMode = null;
			break;
		case SelectModeBy.Resolution:
			if (_desiredResolutions.Count > 0)
			{
				aVProLiveCameraDeviceMode = GetClosestMode(_device, _desiredResolutions, _maintainAspectRatio, _highestFrameRate);
				if (aVProLiveCameraDeviceMode == null)
				{
					Debug.LogWarning("[AVProLiveCamera] Could not find desired mode, using default mode.");
				}
			}
			break;
		case SelectModeBy.Index:
			if (_desiredModeIndex >= 0)
			{
				aVProLiveCameraDeviceMode = _device.GetMode(_desiredModeIndex);
				if (aVProLiveCameraDeviceMode == null)
				{
					Debug.LogWarning("[AVProLiveCamera] Could not find desired mode, using default mode.");
				}
			}
			break;
		}
		return aVProLiveCameraDeviceMode;
	}

	private AVProLiveCameraDevice SelectDevice()
	{
		AVProLiveCameraDevice result = null;
		switch (_deviceSelection)
		{
		default:
			result = AVProLiveCameraManager.Instance.GetDevice(0);
			break;
		case SelectDeviceBy.Name:
			if (_desiredDeviceNames.Count > 0)
			{
				result = GetFirstDevice(_desiredDeviceNames);
			}
			break;
		case SelectDeviceBy.Index:
			if (_desiredDeviceIndex >= 0)
			{
				result = AVProLiveCameraManager.Instance.GetDevice(_desiredDeviceIndex);
			}
			break;
		}
		return result;
	}

	private int SelectVideoInputIndex()
	{
		int num = -1;
		switch (_videoInputSelection)
		{
		default:
			num = -1;
			break;
		case SelectDeviceBy.Name:
			if (_desiredVideoInputs.Count <= 0 || _device.NumVideoInputs <= 0)
			{
				break;
			}
			foreach (AVProLiveCameraPlugin.VideoInput desiredVideoInput in _desiredVideoInputs)
			{
				for (int i = 0; i < _device.NumVideoInputs; i++)
				{
					if (desiredVideoInput.ToString().Replace("_", " ") == _device.GetVideoInputName(i))
					{
						num = i;
						break;
					}
				}
				if (num >= 0)
				{
					break;
				}
			}
			break;
		case SelectDeviceBy.Index:
			if (_desiredVideoInputIndex >= 0)
			{
				num = _desiredVideoInputIndex;
			}
			break;
		}
		return num;
	}

	private void OnEnable()
	{
		if (_device != null)
		{
			_device.IsActive = true;
		}
	}

	private void OnDisable()
	{
		if (_device != null)
		{
			_device.IsActive = false;
		}
	}

	private static AVProLiveCameraDeviceMode GetClosestMode(AVProLiveCameraDevice device, List<Vector2> resolutions, bool maintainApectRatio, bool highestFrameRate)
	{
		AVProLiveCameraDeviceMode aVProLiveCameraDeviceMode = null;
		for (int i = 0; i < resolutions.Count; i++)
		{
			aVProLiveCameraDeviceMode = device.GetClosestMode(Mathf.FloorToInt(resolutions[i].x), Mathf.FloorToInt(resolutions[i].y), maintainApectRatio, highestFrameRate);
			if (aVProLiveCameraDeviceMode != null)
			{
				break;
			}
		}
		return aVProLiveCameraDeviceMode;
	}

	private static AVProLiveCameraDevice GetFirstDevice(List<string> names)
	{
		AVProLiveCameraDevice aVProLiveCameraDevice = null;
		for (int i = 0; i < names.Count; i++)
		{
			aVProLiveCameraDevice = AVProLiveCameraManager.Instance.GetDevice(names[i]);
			if (aVProLiveCameraDevice != null)
			{
				break;
			}
		}
		return aVProLiveCameraDevice;
	}
}
