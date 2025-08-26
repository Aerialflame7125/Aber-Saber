using System.IO;
using UnityEngine;

public class MainSettingsModel : ScriptableObject
{
	public enum WindowMode
	{
		Windowed,
		Fullscreen
	}

	private class Config
	{
		public string version = "1.2.0";

		public float vrResolutionScale = 1f;

		public int windowResolutionWidth = 1280;

		public int windowResolutionHeight = 720;

		public WindowMode windowMode = WindowMode.Fullscreen;

		public int antiAliasingLevel = 2;

		public float volume = 1f;

		public float playerHeight = 1.8f;

		public bool controllersRumbleEnabled = true;

		public float roomCenterX;

		public float roomCenterY;

		public float roomCenterZ;

		public float roomRotation;

		public int mirrorGraphicsSettings = 2;

		public int mainEffectGraphicsSettings = 1;

		public int bloomGraphicsSettings = 1;

		public int smokeGraphicsSettings = 1;

		public int enableAlphaFeatures;

		public bool swapColors;

		public int pauseButtonPressDurationLevel = 1;

		public string lastImportedFile = "./..";
	}

	public const float kDefaultRoomCenterX = 0f;

	public const float kDefaultRoomCenterY = 0f;

	public const float kDefaultRoomCenterZ = 0f;

	public const float kDefaultPlayerHeight = 1.8f;

	public const string kLastImportedFile = "./..";

	public const float kPauseButtonPressDurationMultiplier = 0.75f;

	private Config _config;

	private const string kFileName = "settings.cfg";

	private const string kTempFileName = "settings.cfg.tmp";

	private const string kBackupFileName = "settings.cfg.bak";

	private const string kCurrentVersion = "1.2.0";

	private bool _playingForTheFirstTimeChecked;

	private Config config
	{
		get
		{
			if (_config == null)
			{
				Load();
			}
			return _config;
		}
	}

	public float vrResolutionScale
	{
		get
		{
			return config.vrResolutionScale;
		}
		set
		{
			config.vrResolutionScale = value;
		}
	}

	public Vector2Int windowResolution
	{
		get
		{
			return new Vector2Int(config.windowResolutionWidth, config.windowResolutionHeight);
		}
		set
		{
			config.windowResolutionWidth = value.x;
			config.windowResolutionHeight = value.y;
		}
	}

	public float playerHeight
	{
		get
		{
			return config.playerHeight;
		}
		set
		{
			config.playerHeight = value;
		}
	}

	public WindowMode windowMode
	{
		get
		{
			return config.windowMode;
		}
		set
		{
			config.windowMode = value;
		}
	}

	public int antiAliasingLevel
	{
		get
		{
			return config.antiAliasingLevel;
		}
		set
		{
			config.antiAliasingLevel = value;
		}
	}

	public float volume
	{
		get
		{
			return config.volume;
		}
		set
		{
			config.volume = value;
		}
	}

	public bool controllersRumbleEnabled
	{
		get
		{
			return config.controllersRumbleEnabled;
		}
		set
		{
			config.controllersRumbleEnabled = value;
		}
	}

	public Vector3 roomCenter
	{
		get
		{
			return new Vector3(config.roomCenterX, config.roomCenterY, config.roomCenterZ);
		}
		set
		{
			config.roomCenterX = value.x;
			config.roomCenterY = value.y;
			config.roomCenterZ = value.z;
		}
	}

	public float roomRotation
	{
		get
		{
			return config.roomRotation;
		}
		set
		{
			config.roomRotation = value;
		}
	}

	public int mirrorGraphicsSettings
	{
		get
		{
			return config.mirrorGraphicsSettings;
		}
		set
		{
			config.mirrorGraphicsSettings = value;
		}
	}

	public int mainEffectGraphicsSettings
	{
		get
		{
			return config.mainEffectGraphicsSettings;
		}
		set
		{
			config.mainEffectGraphicsSettings = value;
		}
	}

	public int bloomGraphicsSettings
	{
		get
		{
			return config.bloomGraphicsSettings;
		}
		set
		{
			config.bloomGraphicsSettings = value;
		}
	}

	public int smokeGraphicsSettings
	{
		get
		{
			return config.smokeGraphicsSettings;
		}
		set
		{
			config.smokeGraphicsSettings = value;
		}
	}

	public bool enableAlphaFeatures
	{
		get
		{
			return config.enableAlphaFeatures == 1;
		}
		set
		{
			config.enableAlphaFeatures = (value ? 1 : 0);
		}
	}

	public bool swapColors
	{
		get
		{
			return config.swapColors;
		}
		set
		{
			config.swapColors = value;
		}
	}

	public int pauseButtonPressDurationLevel
	{
		get
		{
			return config.pauseButtonPressDurationLevel;
		}
		set
		{
			config.pauseButtonPressDurationLevel = value;
		}
	}

	public float pauseButtonPressDuration => (float)config.pauseButtonPressDurationLevel * 0.75f;

	public string lastImportedFile
	{
		get
		{
			return config.lastImportedFile;
		}
		set
		{
			config.lastImportedFile = value;
		}
	}

	public bool playingForTheFirstTime { get; private set; }

	public void Save()
	{
		string filePath = Application.persistentDataPath + "/settings.cfg";
		string tempFilePath = Application.persistentDataPath + "/settings.cfg.tmp";
		string backupFilePath = Application.persistentDataPath + "/settings.cfg.bak";
		FileHelpers.SaveToJSONFile(config, filePath, tempFilePath, backupFilePath);
	}

	private void Load()
	{
		string filePath = Application.persistentDataPath + "/settings.cfg";
		string backupFilePath = Application.persistentDataPath + "/settings.cfg.bak";
		_config = FileHelpers.LoadFromJSONFile<Config>(filePath, backupFilePath);
		if (_config == null)
		{
			_config = new Config();
		}
		else if (_config.version != "1.2.0")
		{
			_config.version = "1.2.0";
		}
	}

	public void LoadIfNeeded()
	{
		if (config == null)
		{
			Load();
		}
	}

	private void OnEnable()
	{
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
		if (!_playingForTheFirstTimeChecked)
		{
			_playingForTheFirstTimeChecked = true;
			string path = Application.persistentDataPath + "/settings.cfg";
			playingForTheFirstTime = !File.Exists(path);
			if (playingForTheFirstTime)
			{
				Debug.Log("playing for the first time");
			}
		}
	}

	private void OnDisable()
	{
		Save();
	}
}
