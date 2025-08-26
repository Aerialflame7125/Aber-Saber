using System;
using System.Collections.Generic;
using UnityEngine;

public class BeatmapObjectCallbackController : MonoBehaviour
{
	public class BeatmapObjectCallbackData
	{
		public BeatmapObjectCallback callback;

		public float aheadTime;

		public int[] nextObjectIndexInLine;

		public int id;

		private static int _nextId;

		public BeatmapObjectCallbackData(BeatmapObjectCallback callback, float aheadTime, int numberOfLines)
		{
			id = _nextId++;
			this.callback = callback;
			this.aheadTime = aheadTime;
			nextObjectIndexInLine = new int[numberOfLines];
			for (int i = 0; i < nextObjectIndexInLine.Length; i++)
			{
				nextObjectIndexInLine[i] = 0;
			}
		}
	}

	public delegate void BeatmapObjectCallback(BeatmapObjectData noteData);

	[SerializeField]
	[Provider(typeof(BeatmapDataModel))]
	private ObjectProvider _beatmapDataModelProvider;

	[SerializeField]
	private FloatVariable _songTime;

	private List<BeatmapObjectCallbackData> _beatmapObjectCallbackData = new List<BeatmapObjectCallbackData>();

	private BeatmapDataModel _beatmapDataModel;

	private int _nextEventIndex;

	private float _startSongTime;

	public float startSongTime => _startSongTime;

	public event Action<BeatmapEventData> beatmapEventDidTriggerEvent;

	public void Init(float startSongTime)
	{
		_startSongTime = startSongTime;
	}

	private void Start()
	{
		GetBeatmapDataModelFromProvider();
	}

	private void OnDestroy()
	{
		if (_beatmapDataModel != null)
		{
			_beatmapDataModel.beatmapDataDidChangeEvent -= HandleBeatmapDataModelDidChangeBeatmapData;
		}
	}

	private void LateUpdate()
	{
		BeatmapData beatmapData = _beatmapDataModel.beatmapData;
		if (beatmapData == null)
		{
			return;
		}
		for (int i = 0; i < _beatmapObjectCallbackData.Count; i++)
		{
			BeatmapObjectCallbackData beatmapObjectCallbackData = _beatmapObjectCallbackData[i];
			for (int j = 0; j < beatmapData.beatmapLinesData.Length; j++)
			{
				while (beatmapObjectCallbackData.nextObjectIndexInLine[j] < beatmapData.beatmapLinesData[j].beatmapObjectsData.Length)
				{
					BeatmapObjectData beatmapObjectData = beatmapData.beatmapLinesData[j].beatmapObjectsData[beatmapObjectCallbackData.nextObjectIndexInLine[j]];
					if (beatmapObjectData.time - beatmapObjectCallbackData.aheadTime < _songTime.value)
					{
						if (beatmapObjectData.time >= startSongTime)
						{
							beatmapObjectCallbackData.callback(beatmapObjectData);
						}
						beatmapObjectCallbackData.nextObjectIndexInLine[j]++;
						continue;
					}
					break;
				}
			}
		}
		while (_nextEventIndex < beatmapData.beatmapEventData.Length)
		{
			BeatmapEventData beatmapEventData = beatmapData.beatmapEventData[_nextEventIndex];
			if (beatmapEventData.time < _songTime.value)
			{
				SendBeatmapEventDidTriggerEvent(beatmapEventData);
				_nextEventIndex++;
				continue;
			}
			break;
		}
	}

	public int AddBeatmapObjectCallback(BeatmapObjectCallback callback, float aheadTime)
	{
		GetBeatmapDataModelFromProvider();
		int numberOfLines = 4;
		if (_beatmapDataModel.beatmapData != null)
		{
			numberOfLines = _beatmapDataModel.beatmapData.beatmapLinesData.Length;
		}
		BeatmapObjectCallbackData beatmapObjectCallbackData = new BeatmapObjectCallbackData(callback, aheadTime, numberOfLines);
		_beatmapObjectCallbackData.Add(beatmapObjectCallbackData);
		return beatmapObjectCallbackData.id;
	}

	public void RemoveBeatmapObjectCallback(int callbackId)
	{
		if (_beatmapObjectCallbackData == null)
		{
			return;
		}
		for (int i = 0; i < _beatmapObjectCallbackData.Count; i++)
		{
			if (_beatmapObjectCallbackData[i].id == callbackId)
			{
				_beatmapObjectCallbackData.RemoveAt(i);
				break;
			}
		}
	}

	public void SendBeatmapEventDidTriggerEvent(BeatmapEventData beatmapEventData)
	{
		if (this.beatmapEventDidTriggerEvent != null)
		{
			this.beatmapEventDidTriggerEvent(beatmapEventData);
		}
	}

	private void GetBeatmapDataModelFromProvider()
	{
		if (_beatmapDataModel == null)
		{
			_beatmapDataModel = _beatmapDataModelProvider.GetProvidedObject<BeatmapDataModel>();
			_beatmapDataModel.beatmapDataDidChangeEvent += HandleBeatmapDataModelDidChangeBeatmapData;
		}
	}

	private void HandleBeatmapDataModelDidChangeBeatmapData()
	{
		int num = 0;
		if (_beatmapDataModel.beatmapData != null)
		{
			num = _beatmapDataModel.beatmapData.beatmapLinesData.Length;
		}
		foreach (BeatmapObjectCallbackData beatmapObjectCallbackDatum in _beatmapObjectCallbackData)
		{
			if (beatmapObjectCallbackDatum.nextObjectIndexInLine.Length < num)
			{
				beatmapObjectCallbackDatum.nextObjectIndexInLine = new int[num];
			}
			for (int i = 0; i < beatmapObjectCallbackDatum.nextObjectIndexInLine.Length; i++)
			{
				beatmapObjectCallbackDatum.nextObjectIndexInLine[i] = 0;
			}
		}
		_nextEventIndex = 0;
	}
}
