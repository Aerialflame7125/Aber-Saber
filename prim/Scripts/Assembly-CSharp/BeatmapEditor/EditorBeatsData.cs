using UnityEngine;

namespace BeatmapEditor;

public class EditorBeatsData
{
	private BeatData[] _beats;

	private int _beatsPerBar = 4;

	public BeatData this[int index] => _beats[index];

	public int length => (_beats != null) ? _beats.Length : 0;

	public int beatsPerBar => _beatsPerBar;

	public EditorBeatsData(int lengthInBeats, int beatsPerBar)
	{
		_beats = new BeatData[lengthInBeats];
		for (int i = 0; i < _beats.Length; i++)
		{
			_beats[i] = new BeatData();
		}
		_beatsPerBar = beatsPerBar;
	}

	public EditorBeatsData(EditorBeatsData other)
	{
		if (other._beats == null)
		{
			return;
		}
		_beats = new BeatData[other._beats.Length];
		for (int i = 0; i < _beats.Length; i++)
		{
			if (other._beats[i] != null)
			{
				_beats[i] = new BeatData(other._beats[i]);
			}
		}
		_beatsPerBar = other._beatsPerBar;
	}

	public EditorBeatsData Clone()
	{
		return new EditorBeatsData(this);
	}

	public void Stretch2x()
	{
		_beatsPerBar *= 2;
		BeatData[] array = new BeatData[_beats.Length * 2];
		for (int i = 0; i < _beats.Length; i++)
		{
			array[i * 2] = _beats[i];
			BeatData beatData = (array[i * 2 + 1] = new BeatData());
			for (int j = 0; j < beatData.obstaclesData.Length; j++)
			{
				beatData.obstaclesData[j] = _beats[i].obstaclesData[j];
			}
		}
		_beats = array;
		int num = _beats[0].eventsData.Length;
		for (int k = 0; k < num; k++)
		{
			FillPreviousValidEvent(0, k, untilNextValid: false);
		}
	}

	public void Squish2x()
	{
		if (_beats.Length >= 2)
		{
			_beatsPerBar /= 2;
			BeatData[] array = new BeatData[Mathf.CeilToInt((float)_beats.Length / 2f)];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = _beats[i * 2];
			}
			_beats = array;
			int num = _beats[0].eventsData.Length;
			for (int j = 0; j < num; j++)
			{
				FillPreviousValidEvent(0, j, untilNextValid: false);
			}
		}
	}

	public bool CanSquish2x(out int problematicBeatIndex)
	{
		problematicBeatIndex = -1;
		if (_beats.Length < 2)
		{
			return true;
		}
		for (int i = 1; i < _beats.Length; i += 2)
		{
			problematicBeatIndex = i;
			BeatData beatData = _beats[i];
			for (int j = 0; j < beatData.baseNotesData.Length; j++)
			{
				if (beatData.baseNotesData[j] != null)
				{
					return false;
				}
			}
			for (int k = 0; k < beatData.upperNotesData.Length; k++)
			{
				if (beatData.upperNotesData[k] != null)
				{
					return false;
				}
			}
			for (int l = 0; l < beatData.topNotesData.Length; l++)
			{
				if (beatData.topNotesData[l] != null)
				{
					return false;
				}
			}
			for (int m = 0; m < beatData.eventsData.Length; m++)
			{
				if (beatData.eventsData[m] != null && !beatData.eventsData[m].isPreviousValidValue)
				{
					return false;
				}
			}
		}
		return true;
	}

	public void Resize(int newLengthInBeats)
	{
		if (newLengthInBeats == _beats.Length)
		{
			return;
		}
		BeatData[] array = new BeatData[newLengthInBeats];
		int num = _beats.Length;
		int num2 = Mathf.Min(num, newLengthInBeats);
		for (int i = 0; i < num2; i++)
		{
			array[i] = _beats[i];
		}
		for (int j = num2; j < array.Length; j++)
		{
			array[j] = new BeatData();
		}
		_beats = array;
		if (_beats.Length > 0)
		{
			int num3 = _beats[0].eventsData.Length;
			for (int k = 0; k < num3; k++)
			{
				FillPreviousValidEvent(num, k, untilNextValid: false);
			}
		}
	}

	public void Clear()
	{
		for (int i = 0; i < _beats.Length; i++)
		{
			_beats[i] = new BeatData();
		}
	}

	public void FillPreviousValidEvent(int startBeatIndex, int eventIndex, bool untilNextValid)
	{
		if (startBeatIndex >= _beats.Length)
		{
			return;
		}
		EditorEventData editorEventData = null;
		if (startBeatIndex > 1)
		{
			editorEventData = _beats[startBeatIndex - 1].eventsData[eventIndex];
		}
		for (int i = startBeatIndex; i < _beats.Length; i++)
		{
			EditorEventData[] eventsData = _beats[i].eventsData;
			if (eventsData[eventIndex] != null && !eventsData[eventIndex].isPreviousValidValue)
			{
				if (untilNextValid)
				{
					break;
				}
				editorEventData = eventsData[eventIndex];
			}
			else if (editorEventData != null)
			{
				eventsData[eventIndex] = new EditorEventData(editorEventData.value, isPreviousValidValue: true);
			}
			else
			{
				eventsData[eventIndex] = null;
			}
		}
	}
}
