using UnityEngine;

public class GameDataModel : PersistentSingleton<GameDataModel>
{
	private const string kGameDynamicDataFileName = "GameDynamicData.dat";

	private const string kGameDynamicDataTempFileName = "GameDynamicData.dat.tmp";

	private const string kGameDynamicDataBackupFileName = "GameDynamicData.dat.bak";

	private GameDynamicData _gameDynamicData;

	public GameDynamicData gameDynamicData
	{
		get
		{
			if (_gameDynamicData == null)
			{
				LoadOrCreateGameDynamicData();
			}
			return _gameDynamicData;
		}
	}

	private void Awake()
	{
		Analytics.GameStart();
		LoadOrCreateGameDynamicData();
	}

	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus)
		{
			SaveGameDynamicData();
		}
	}

	private void OnApplicationQuit()
	{
		Analytics.GameOver();
		SaveGameDynamicData();
	}

	private void SaveGameDynamicData()
	{
		string filePath = Application.persistentDataPath + "/GameDynamicData.dat";
		string tempFilePath = Application.persistentDataPath + "/GameDynamicData.dat.tmp";
		string backupFilePath = Application.persistentDataPath + "/GameDynamicData.dat.bak";
		_gameDynamicData.Save(filePath, tempFilePath, backupFilePath);
	}

	private void LoadOrCreateGameDynamicData()
	{
		string filePath = Application.persistentDataPath + "/GameDynamicData.dat";
		string backupFilePath = Application.persistentDataPath + "/GameDynamicData.dat.bak";
		_gameDynamicData = GameDynamicData.LoadOrCreate(filePath, backupFilePath);
	}
}
