using UnityEngine;

public class BeatmapDataSO : ScriptableObject
{
	[HideInInspector]
	[SerializeField]
	public string _jsonData;

	public BeatmapData _beatmapData;

	public float _beatsPerMinute;

	public float _shuffle;

	public float _shufflePeriod;

	public bool _hasRequiredDataForLoad;

	public BeatmapData beatmapData
	{
		get
		{
			if (_beatmapData == null)
			{
				Load();
			}
			return _beatmapData;
		}
		set
		{
			_beatmapData = value;
		}
	}

	public void SetJsonData(string jsonData)
	{
		_jsonData = jsonData;
	}

	public void SetRequiredDataForLoad(float beatsPerMinute, float shuffle, float shufflePeriod)
	{
		_beatsPerMinute = beatsPerMinute;
		_shuffle = shuffle;
		_shufflePeriod = shufflePeriod;
		_hasRequiredDataForLoad = true;
	}

	public void Load()
	{
		if (_hasRequiredDataForLoad)
		{
			_beatmapData = BeatmapDataLoader.GetBeatmapDataFromJson(_jsonData, _beatsPerMinute, _shuffle, _shufflePeriod);
		}
	}
}
