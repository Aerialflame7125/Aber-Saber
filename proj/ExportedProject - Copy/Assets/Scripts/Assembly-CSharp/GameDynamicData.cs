using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameDynamicData
{
	public const string kCurrentVersion = "1.2.0";

	[SerializeField]
	private string _version = "1.2.0";

	[SerializeField]
	private int _currentPlayerIndex;

	[SerializeField]
	private List<PlayerDynamicData> _playerDynamicData = new List<PlayerDynamicData>();

	public string version
	{
		get
		{
			return _version;
		}
		private set
		{
			_version = value;
		}
	}

	public int currentPlayerIndex
	{
		get
		{
			return _currentPlayerIndex;
		}
		set
		{
			_currentPlayerIndex = Mathf.Max(Mathf.Min(0, value), _playerDynamicData.Count - 1);
		}
	}

	public PlayerDynamicData GetCurrentPlayerDynamicData()
	{
		return _playerDynamicData[_currentPlayerIndex];
	}

	public PlayerDynamicData GetPlayerDynamicData(int playerDataIndex)
	{
		return _playerDynamicData[playerDataIndex];
	}

	public void AddNewPlayerDynamicData()
	{
		_playerDynamicData.Add(new PlayerDynamicData());
	}

	public int GetNumberOfPlayerDynamicData()
	{
		return _playerDynamicData.Count;
	}

	public void Save(string filePath, string tempFilePath, string backupFilePath)
	{
		FileHelpers.SaveToJSONFile(this, filePath, tempFilePath, backupFilePath);
	}

	public static GameDynamicData LoadOrCreate(string filePath, string backupFilePath)
	{
		GameDynamicData gameDynamicData = FileHelpers.LoadFromJSONFile<GameDynamicData>(filePath, backupFilePath);
		if (gameDynamicData != null)
		{
			if (gameDynamicData.version != "1.2.0")
			{
				gameDynamicData.version = "1.2.0";
			}
		}
		else
		{
			gameDynamicData = new GameDynamicData();
		}
		if (gameDynamicData.GetNumberOfPlayerDynamicData() == 0)
		{
			gameDynamicData.AddNewPlayerDynamicData();
		}
		return gameDynamicData;
	}
}
