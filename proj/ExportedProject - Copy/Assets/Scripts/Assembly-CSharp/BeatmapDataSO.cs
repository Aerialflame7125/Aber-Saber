using UnityEngine;

public class BeatmapDataSO : ScriptableObject
{
	[HideInInspector]
	[SerializeField]
	private string _jsonData;

	private BeatmapData _beatmapData;

	private float _beatsPerMinute;

	private float _shuffle;

	private float _shufflePeriod;

	private bool _hasRequiredDataForLoad;

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
