using System;
using UnityEngine;

namespace BeatmapEditor;

public class EditorBeatmapSO : ScriptableObject
{
	private EditorBeatsData _beatsData;

	private UndoRedoBuffer<EditorBeatsData> _undoRedoBuffer = new UndoRedoBuffer<EditorBeatsData>(40);

	public int beatsPerBar => (_beatsData == null) ? 4 : _beatsData.beatsPerBar;

	public int beatsDataLength => (_beatsData != null) ? _beatsData.length : 0;

	public bool hasBeatsData => _beatsData != null;

	public EditorBeatsData beatsData => _beatsData;

	public event Action didChangeAllDataEvent;

	public BeatData BeatData(int beatIndex)
	{
		return _beatsData[beatIndex];
	}

	private void OnEnable()
	{
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
	}

	public void Undo()
	{
		EditorBeatsData editorBeatsData = _undoRedoBuffer.Undo();
		if (editorBeatsData != null)
		{
			_beatsData = editorBeatsData;
			if (this.didChangeAllDataEvent != null)
			{
				this.didChangeAllDataEvent();
			}
		}
	}

	public void Redo()
	{
		EditorBeatsData editorBeatsData = _undoRedoBuffer.Redo();
		if (editorBeatsData != null)
		{
			_beatsData = editorBeatsData;
			if (this.didChangeAllDataEvent != null)
			{
				this.didChangeAllDataEvent();
			}
		}
	}

	public void InitWithEmptyData(int numberOfBeats)
	{
		_beatsData = new EditorBeatsData(numberOfBeats, 4);
		_undoRedoBuffer.Clear();
		_undoRedoBuffer.Add(_beatsData.Clone());
		if (this.didChangeAllDataEvent != null)
		{
			this.didChangeAllDataEvent();
		}
	}

	public void LoadData(EditorBeatsData beatsData)
	{
		_beatsData = beatsData;
		_undoRedoBuffer.Clear();
		_undoRedoBuffer.Add(_beatsData.Clone());
		if (this.didChangeAllDataEvent != null)
		{
			this.didChangeAllDataEvent();
		}
	}

	public void AddNote(int beatIndex, NoteLineLayer lineLayer, int lineIndex, EditorNoteData noteData)
	{
		if (beatIndex >= _beatsData.length)
		{
			_beatsData.Resize(beatIndex + 1);
		}
		switch (lineLayer)
		{
		case NoteLineLayer.Base:
			_beatsData[beatIndex].baseNotesData[lineIndex] = noteData;
			break;
		case NoteLineLayer.Upper:
			_beatsData[beatIndex].upperNotesData[lineIndex] = noteData;
			break;
		case NoteLineLayer.Top:
			_beatsData[beatIndex].topNotesData[lineIndex] = noteData;
			break;
		}
		_undoRedoBuffer.Add(_beatsData.Clone());
	}

	public void AddEvent(int beatIndex, int cellIndex, EditorEventData eventData)
	{
		if (beatIndex >= _beatsData.length)
		{
			_beatsData.Resize(beatIndex + 1);
		}
		EditorEventData[] eventsData = _beatsData[beatIndex].eventsData;
		eventsData[cellIndex] = eventData;
		_beatsData.FillPreviousValidEvent(beatIndex + 1, cellIndex, untilNextValid: true);
		_undoRedoBuffer.Add(_beatsData.Clone());
	}

	public void AddObstacle(int beatIndex, int lineIndex, int obstacleLength, ObstacleType obstacleType)
	{
		if (beatIndex + obstacleLength >= _beatsData.length)
		{
			_beatsData.Resize(beatIndex + obstacleLength + 1);
		}
		EditorObstacleData[] obstaclesData = _beatsData[beatIndex].obstaclesData;
		for (int i = 0; i < obstacleLength; i++)
		{
			obstaclesData = _beatsData[beatIndex + i].obstaclesData;
			if (obstacleType == ObstacleType.Top)
			{
				for (int j = 0; j < 4; j++)
				{
					obstaclesData[j] = new EditorObstacleData(obstacleType);
				}
			}
			else
			{
				obstaclesData[lineIndex] = new EditorObstacleData(obstacleType);
			}
		}
		_undoRedoBuffer.Add(_beatsData.Clone());
	}

	public void EraseNote(int beatIndex, NoteLineLayer lineLayer, int lineIndex)
	{
		if (beatIndex < _beatsData.length && beatIndex >= 0 && lineIndex >= 0 && lineIndex <= 3)
		{
			switch (lineLayer)
			{
			case NoteLineLayer.Base:
				_beatsData[beatIndex].baseNotesData[lineIndex] = null;
				break;
			case NoteLineLayer.Upper:
				_beatsData[beatIndex].upperNotesData[lineIndex] = null;
				break;
			case NoteLineLayer.Top:
				_beatsData[beatIndex].topNotesData[lineIndex] = null;
				break;
			}
			_undoRedoBuffer.Add(_beatsData.Clone());
		}
	}

	public void EraseObstacle(int beatIndex, int lineIndex)
	{
		if (beatIndex >= _beatsData.length)
		{
			return;
		}
		EditorObstacleData[] obstaclesData = _beatsData[beatIndex].obstaclesData;
		if (obstaclesData[lineIndex] != null)
		{
			EditorObstacleData editorObstacleData = obstaclesData[lineIndex];
			obstaclesData[lineIndex] = null;
			int num = beatIndex;
			while (beatIndex < _beatsData.length - 1)
			{
				beatIndex++;
				obstaclesData = _beatsData[beatIndex].obstaclesData;
				if (obstaclesData == null || obstaclesData[lineIndex] == null || obstaclesData[lineIndex].type != editorObstacleData.type)
				{
					break;
				}
				obstaclesData[lineIndex] = null;
			}
			beatIndex = num;
			while (beatIndex > 0)
			{
				beatIndex--;
				obstaclesData = _beatsData[beatIndex].obstaclesData;
				if (obstaclesData == null || obstaclesData[lineIndex] == null || obstaclesData[lineIndex].type != editorObstacleData.type)
				{
					break;
				}
				obstaclesData[lineIndex] = null;
			}
		}
		_undoRedoBuffer.Add(_beatsData.Clone());
	}

	public void EraseEvent(int beatIndex, int eventIndex)
	{
		if (beatIndex < _beatsData.length)
		{
			EditorEventData editorEventData = _beatsData[beatIndex].eventsData[eventIndex];
			if (editorEventData != null && !editorEventData.isPreviousValidValue)
			{
				_beatsData[beatIndex].eventsData[eventIndex] = null;
				_beatsData.FillPreviousValidEvent(beatIndex, eventIndex, untilNextValid: true);
			}
			_undoRedoBuffer.Add(_beatsData.Clone());
		}
	}

	public void PasteBeatSegments(int segmentStartBeatIndex, EditorNoteData[] _copyPasteSegmentBaseNotesData, EditorNoteData[] _copyPasteSegmentUpperNotesData, EditorNoteData[] _copyPasteSegmentTopNotesData, EditorObstacleData[] _copyPasteSegmentObstaclesData, EditorEventData[] _copyPasteSegmentEventsData)
	{
		if (_copyPasteSegmentBaseNotesData != null)
		{
			int num = _copyPasteSegmentBaseNotesData.Length / 4;
			for (int i = 0; i < num; i++)
			{
				int num2 = segmentStartBeatIndex + i;
				if (num2 >= _beatsData.length)
				{
					_beatsData.Resize(num2 + num);
				}
				for (int j = 0; j < 4; j++)
				{
					_beatsData[num2].baseNotesData[j] = _copyPasteSegmentBaseNotesData[i * 4 + j];
				}
			}
		}
		if (_copyPasteSegmentUpperNotesData != null)
		{
			int num3 = _copyPasteSegmentUpperNotesData.Length / 4;
			for (int k = 0; k < num3; k++)
			{
				int num4 = segmentStartBeatIndex + k;
				if (num4 >= _beatsData.length)
				{
					_beatsData.Resize(num4 + num3);
				}
				for (int l = 0; l < 4; l++)
				{
					_beatsData[num4].upperNotesData[l] = _copyPasteSegmentUpperNotesData[k * 4 + l];
				}
			}
		}
		if (_copyPasteSegmentTopNotesData != null)
		{
			int num5 = _copyPasteSegmentTopNotesData.Length / 4;
			for (int m = 0; m < num5; m++)
			{
				int num6 = segmentStartBeatIndex + m;
				if (num6 >= _beatsData.length)
				{
					_beatsData.Resize(num6 + num5);
				}
				for (int n = 0; n < 4; n++)
				{
					_beatsData[num6].topNotesData[n] = _copyPasteSegmentTopNotesData[m * 4 + n];
				}
			}
		}
		if (_copyPasteSegmentObstaclesData != null)
		{
			int num7 = _copyPasteSegmentObstaclesData.Length / 4;
			for (int num8 = 0; num8 < num7; num8++)
			{
				int num9 = segmentStartBeatIndex + num8;
				if (num9 >= _beatsData.length)
				{
					_beatsData.Resize(num9 + num7);
				}
				for (int num10 = 0; num10 < 4; num10++)
				{
					_beatsData[num9].obstaclesData[num10] = _copyPasteSegmentObstaclesData[num8 * 4 + num10];
				}
			}
		}
		if (_copyPasteSegmentEventsData != null)
		{
			int num11 = _copyPasteSegmentEventsData.Length / 16;
			for (int num12 = 0; num12 < num11; num12++)
			{
				int num13 = segmentStartBeatIndex + num12;
				if (num13 >= _beatsData.length)
				{
					_beatsData.Resize(num13 + num11);
				}
				for (int num14 = 0; num14 < 16; num14++)
				{
					_beatsData[num13].eventsData[num14] = _copyPasteSegmentEventsData[num12 * 16 + num14];
				}
			}
			for (int num15 = 0; num15 < 16; num15++)
			{
				_beatsData.FillPreviousValidEvent(segmentStartBeatIndex, num15, untilNextValid: true);
				_beatsData.FillPreviousValidEvent(segmentStartBeatIndex + num11, num15, untilNextValid: true);
			}
		}
		if (this.didChangeAllDataEvent != null)
		{
			this.didChangeAllDataEvent();
		}
		_undoRedoBuffer.Add(_beatsData.Clone());
	}

	public void Stretch2x()
	{
		Stretch2xInternal(useCallback: true);
	}

	public void Squish2x()
	{
		Squish2xInternal(useCallback: true);
	}

	public bool CanSquish2x(out int problematicBeatIndex)
	{
		return _beatsData.CanSquish2x(out problematicBeatIndex);
	}

	private void Stretch2xInternal(bool useCallback)
	{
		_beatsData.Stretch2x();
		if (useCallback && this.didChangeAllDataEvent != null)
		{
			this.didChangeAllDataEvent();
		}
		_undoRedoBuffer.Add(_beatsData.Clone());
	}

	private void Squish2xInternal(bool useCallback)
	{
		_beatsData.Squish2x();
		if (useCallback && this.didChangeAllDataEvent != null)
		{
			this.didChangeAllDataEvent();
		}
		_undoRedoBuffer.Add(_beatsData.Clone());
	}
}
