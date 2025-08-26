using System;
using UnityEngine;

public class MixedRealitySettings : ScriptableObject
{
	private class Config
	{
		public float x;

		public float y;

		public float z;

		public float rx;

		public float ry;

		public float rz;

		public float near = 0.05f;

		public float far = 300f;

		public float fov = 65f;

		public float additionalRotationOffset;

		public bool trackedObjectEnabled;

		public int trackedObjectID;

		public float trackedObjectSmooth;

		public float fovAdd;

		public int mixedRealityType = 1;

		public bool enableMixedReality;

		public int webCamCompositorFramesDelay;

		public string webCamName = string.Empty;

		public int webCamModeIdx = -1;

		public bool webCamFlipY = true;

		public float keyingColorR;

		public float keyingColorG;

		public float keyingColorB;

		public float keyingThreshold;

		public float keyingSmoothness;

		public float wallPosX;

		public float wallPosZ;

		public float wallRotY;

		public float wallSizeX;

		public float wallSizeY;

		public float wallSizeZ;

		public int pipPosition = 4;

		public float pipRelativeSize = 0.3f;
	}

	public enum MixedRealityType
	{
		None,
		WebCam,
		Quadrants,
		FarCameraOnly
	}

	public enum PIPPosition
	{
		None,
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight
	}

	private Config _config;

	private const string kFileName = "HMExternalCamera.cfg";

	public Color keyingRGBColor
	{
		get
		{
			return new Color(_config.keyingColorR, _config.keyingColorG, _config.keyingColorB);
		}
		set
		{
			_config.keyingColorR = value.r;
			_config.keyingColorG = value.g;
			_config.keyingColorB = value.b;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public float keyingThreshold
	{
		get
		{
			return _config.keyingThreshold;
		}
		set
		{
			_config.keyingThreshold = value;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public float keyingSmoothness
	{
		get
		{
			return _config.keyingSmoothness;
		}
		set
		{
			_config.keyingSmoothness = value;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public string webCamName
	{
		get
		{
			return _config.webCamName;
		}
		set
		{
			if (!(_config.webCamName == value))
			{
				_config.webCamName = value;
				if (this.mixedRealityCameraDeviceSettingsDidChangeEvent != null)
				{
					this.mixedRealityCameraDeviceSettingsDidChangeEvent();
				}
			}
		}
	}

	public int webCamModeIdx
	{
		get
		{
			return _config.webCamModeIdx;
		}
		set
		{
			if (_config.webCamModeIdx != value)
			{
				_config.webCamModeIdx = value;
				if (this.mixedRealityCameraDeviceSettingsDidChangeEvent != null)
				{
					this.mixedRealityCameraDeviceSettingsDidChangeEvent();
				}
			}
		}
	}

	public bool webCamFlipY
	{
		get
		{
			return _config.webCamFlipY;
		}
		set
		{
			_config.webCamFlipY = value;
			if (this.mixedRealityCameraDeviceSettingsDidChangeEvent != null)
			{
				this.mixedRealityCameraDeviceSettingsDidChangeEvent();
			}
		}
	}

	public float cameraFOV
	{
		get
		{
			return _config.fov;
		}
		set
		{
			_config.fov = value;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public Vector3 cameraRotationOffset
	{
		get
		{
			return new Vector3(_config.rx, _config.ry, _config.rz);
		}
		set
		{
			_config.rx = value.x;
			_config.ry = value.y;
			_config.rz = value.z;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public float additionalRotationOffset
	{
		get
		{
			return _config.additionalRotationOffset;
		}
		set
		{
			_config.additionalRotationOffset = value;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public Vector3 cameraPositionOffset
	{
		get
		{
			return new Vector3(_config.x, _config.y, _config.z);
		}
		set
		{
			_config.x = value.x;
			_config.y = value.y;
			_config.z = value.z;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public float cameraFOVOffset
	{
		get
		{
			return _config.fovAdd;
		}
		set
		{
			_config.fovAdd = value;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public Vector3 wallPosition
	{
		get
		{
			return new Vector3(_config.wallPosX, 0f, _config.wallPosZ);
		}
		set
		{
			_config.wallPosX = value.x;
			_config.wallPosZ = value.z;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public float wallRotationY
	{
		get
		{
			return _config.wallRotY;
		}
		set
		{
			_config.wallRotY = value;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public Vector3 wallSize
	{
		get
		{
			return new Vector3(_config.wallSizeX, _config.wallSizeY, _config.wallSizeZ);
		}
		set
		{
			_config.wallSizeX = value.x;
			_config.wallSizeY = value.y;
			_config.wallSizeZ = value.z;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public float pipRelativeSize
	{
		get
		{
			return _config.pipRelativeSize;
		}
		set
		{
			_config.pipRelativeSize = value;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public PIPPosition pipPosition
	{
		get
		{
			if (Enum.IsDefined(typeof(PIPPosition), _config.pipPosition))
			{
				return (PIPPosition)_config.pipPosition;
			}
			return PIPPosition.BottomRight;
		}
		set
		{
			if (_config.pipPosition != (int)value)
			{
				_config.pipPosition = (int)value;
				if (this.mixedRealityBasicSettingsDidChangeEvent != null)
				{
					this.mixedRealityBasicSettingsDidChangeEvent();
				}
			}
		}
	}

	public MixedRealityType mixedRealityType
	{
		get
		{
			if (Enum.IsDefined(typeof(MixedRealityType), _config.mixedRealityType))
			{
				return (MixedRealityType)_config.mixedRealityType;
			}
			return MixedRealityType.FarCameraOnly;
		}
		set
		{
			if (_config.mixedRealityType != (int)value)
			{
				_config.mixedRealityType = (int)value;
				if (this.mixedRealityBasicSettingsDidChangeEvent != null)
				{
					this.mixedRealityBasicSettingsDidChangeEvent();
				}
			}
		}
	}

	public int cameraTrackedObjectID
	{
		get
		{
			return _config.trackedObjectID;
		}
		set
		{
			_config.trackedObjectID = value;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public bool cameraTrackedObjectEnabled
	{
		get
		{
			return _config.trackedObjectEnabled;
		}
		set
		{
			_config.trackedObjectEnabled = value;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public float cameraTrackedObjectSmooth
	{
		get
		{
			return _config.trackedObjectSmooth;
		}
		set
		{
			_config.trackedObjectSmooth = value;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public int webCamCompositorFramesDelay
	{
		get
		{
			return Mathf.Max(_config.webCamCompositorFramesDelay, 0);
		}
		set
		{
			_config.webCamCompositorFramesDelay = value;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public bool enableMixedReality
	{
		get
		{
			return _config.enableMixedReality;
		}
		set
		{
			_config.enableMixedReality = value;
			if (this.mixedRealityBasicSettingsDidChangeEvent != null)
			{
				this.mixedRealityBasicSettingsDidChangeEvent();
			}
		}
	}

	public event Action mixedRealityBasicSettingsDidChangeEvent;

	public event Action mixedRealityCameraDeviceSettingsDidChangeEvent;

	private void OnEnable()
	{
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
		Load();
	}

	public void Load()
	{
		_config = new Config();
		ConfigSerializer.LoadConfig(_config, "HMExternalCamera.cfg");
	}

	public void Save()
	{
		ConfigSerializer.SaveConfig(_config, "HMExternalCamera.cfg");
	}
}
