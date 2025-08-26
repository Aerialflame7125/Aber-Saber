using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class CopyAndPasteController : MonoBehaviour
{
	[SerializeField]
	private EditorBeatmapSO _editorBeatmap;

	[SerializeField]
	private IPlayheadBeatIndexContainer _playheadBeatIndex;

	[SerializeField]
	private EditorCopyPasteFilterSO _copyPasteFilter;

	[SerializeField]
	private Button _copy1BarButton;

	[SerializeField]
	private Button _copy2BarsButton;

	[SerializeField]
	private Button _copy4BarsButton;

	[SerializeField]
	private Button _copy8BarsButton;

	[SerializeField]
	private Button _copyAllBarsButton;

	[SerializeField]
	private Button _pasteButton;

	private EditorNoteData[] _copyPasteSegmentBaseNotesData;

	private EditorNoteData[] _copyPasteSegmentUpperNotesData;

	private EditorNoteData[] _copyPasteSegmentTopNotesData;

	private EditorObstacleData[] _copyPasteSegmentObstaclesData;

	private EditorEventData[] _copyPasteSegmentEventsData;

	private ButtonBinder _buttonBinder;

	public event Action<int> dataCopiedEvent;

	public event Action dataPastedEvent;

	private void Awake()
	{
		_buttonBinder = new ButtonBinder(new List<Tuple<Button, Action>>
		{
			{
				_copy1BarButton,
				(Action)delegate
				{
					CopyBeatSegment(1);
				}
			},
			{
				_copy2BarsButton,
				(Action)delegate
				{
					CopyBeatSegment(2);
				}
			},
			{
				_copy4BarsButton,
				(Action)delegate
				{
					CopyBeatSegment(4);
				}
			},
			{
				_copy8BarsButton,
				(Action)delegate
				{
					CopyBeatSegment(8);
				}
			},
			{
				_copyAllBarsButton,
				(Action)delegate
				{
					CopyAllBars();
				}
			},
			{
				_pasteButton,
				(Action)delegate
				{
					PasteBeatSement();
				}
			}
		});
	}

	private void OnDestroy()
	{
		if (_buttonBinder != null)
		{
			_buttonBinder.ClearBindings();
		}
	}

	private void CopyBeatSegment(int startBeatIndex, int numberOfBeats)
	{
		bool copyBaseNotes = _copyPasteFilter.copyBaseNotes;
		bool copyUpperNotes = _copyPasteFilter.copyUpperNotes;
		bool copyTopNotes = _copyPasteFilter.copyTopNotes;
		bool copyObstacles = _copyPasteFilter.copyObstacles;
		bool copyEvents = _copyPasteFilter.copyEvents;
		if (!copyBaseNotes && !copyUpperNotes && !copyTopNotes && !copyObstacles && !copyEvents)
		{
			if (this.dataCopiedEvent != null)
			{
				this.dataCopiedEvent(0);
			}
			return;
		}
		if (copyBaseNotes)
		{
			_copyPasteSegmentBaseNotesData = new EditorNoteData[numberOfBeats * 4];
		}
		else
		{
			_copyPasteSegmentBaseNotesData = null;
		}
		if (copyUpperNotes)
		{
			_copyPasteSegmentUpperNotesData = new EditorNoteData[numberOfBeats * 4];
		}
		else
		{
			_copyPasteSegmentUpperNotesData = null;
		}
		if (copyTopNotes)
		{
			_copyPasteSegmentTopNotesData = new EditorNoteData[numberOfBeats * 4];
		}
		else
		{
			_copyPasteSegmentTopNotesData = null;
		}
		if (copyObstacles)
		{
			_copyPasteSegmentObstaclesData = new EditorObstacleData[numberOfBeats * 4];
		}
		else
		{
			_copyPasteSegmentObstaclesData = null;
		}
		if (copyEvents)
		{
			_copyPasteSegmentEventsData = new EditorEventData[numberOfBeats * 16];
		}
		else
		{
			_copyPasteSegmentEventsData = null;
		}
		for (int i = 0; i < numberOfBeats; i++)
		{
			int num = startBeatIndex + i;
			if (num >= _editorBeatmap.beatsDataLength)
			{
				continue;
			}
			if (copyBaseNotes)
			{
				for (int j = 0; j < 4; j++)
				{
					_copyPasteSegmentBaseNotesData[i * 4 + j] = _editorBeatmap.BeatData(num).baseNotesData[j];
				}
			}
			if (copyUpperNotes)
			{
				for (int k = 0; k < 4; k++)
				{
					_copyPasteSegmentUpperNotesData[i * 4 + k] = _editorBeatmap.BeatData(num).upperNotesData[k];
				}
			}
			if (copyTopNotes)
			{
				for (int l = 0; l < 4; l++)
				{
					_copyPasteSegmentTopNotesData[i * 4 + l] = _editorBeatmap.BeatData(num).topNotesData[l];
				}
			}
			if (copyObstacles)
			{
				for (int m = 0; m < 4; m++)
				{
					_copyPasteSegmentObstaclesData[i * 4 + m] = _editorBeatmap.BeatData(num).obstaclesData[m];
				}
			}
			if (copyEvents)
			{
				for (int n = 0; n < 16; n++)
				{
					_copyPasteSegmentEventsData[i * 16 + n] = _editorBeatmap.BeatData(num).eventsData[n];
				}
			}
		}
		if (this.dataCopiedEvent != null)
		{
			this.dataCopiedEvent(numberOfBeats / _editorBeatmap.beatsPerBar);
		}
	}

	private void CopyBeatSegment(int numberOfBars)
	{
		CopyBeatSegment(_playheadBeatIndex.Result.playheadBeatIndex, numberOfBars * _editorBeatmap.beatsPerBar);
	}

	private void CopyAllBars()
	{
		CopyBeatSegment(0, _editorBeatmap.beatsDataLength);
	}

	private void PasteBeatSement()
	{
		_editorBeatmap.PasteBeatSegments(_playheadBeatIndex.Result.playheadBeatIndex, _copyPasteSegmentBaseNotesData, _copyPasteSegmentUpperNotesData, _copyPasteSegmentTopNotesData, _copyPasteSegmentObstaclesData, _copyPasteSegmentEventsData);
		if (this.dataPastedEvent != null)
		{
			this.dataPastedEvent();
		}
	}
}
