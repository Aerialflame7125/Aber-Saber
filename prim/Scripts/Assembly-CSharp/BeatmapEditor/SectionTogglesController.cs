using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class SectionTogglesController : MonoBehaviour
{
	[SerializeField]
	private EditorCopyPasteFilterSO _copyPasteFilter;

	[SerializeField]
	private Toggle _eventsColumnToggle;

	[SerializeField]
	private Toggle _baseNotesColumnToggle;

	[SerializeField]
	private Toggle _upperNotesColumnToggle;

	[SerializeField]
	private Toggle _topNotesColumnToggle;

	[SerializeField]
	private Toggle _obstaclesColumnToggle;

	private ToggleBinder _toggleBinder;

	private void Awake()
	{
		_toggleBinder = new ToggleBinder(new List<Tuple<Toggle, Action<bool>>>
		{
			{
				_eventsColumnToggle,
				(Action<bool>)delegate(bool on)
				{
					_copyPasteFilter.copyEvents = on;
				}
			},
			{
				_baseNotesColumnToggle,
				(Action<bool>)delegate(bool on)
				{
					_copyPasteFilter.copyBaseNotes = on;
				}
			},
			{
				_upperNotesColumnToggle,
				(Action<bool>)delegate(bool on)
				{
					_copyPasteFilter.copyUpperNotes = on;
				}
			},
			{
				_topNotesColumnToggle,
				(Action<bool>)delegate(bool on)
				{
					_copyPasteFilter.copyTopNotes = on;
				}
			},
			{
				_obstaclesColumnToggle,
				(Action<bool>)delegate(bool on)
				{
					_copyPasteFilter.copyObstacles = on;
				}
			}
		});
		_eventsColumnToggle.isOn = _copyPasteFilter.copyEvents;
		_baseNotesColumnToggle.isOn = _copyPasteFilter.copyBaseNotes;
		_upperNotesColumnToggle.isOn = _copyPasteFilter.copyUpperNotes;
		_topNotesColumnToggle.isOn = _copyPasteFilter.copyTopNotes;
		_obstaclesColumnToggle.isOn = _copyPasteFilter.copyObstacles;
	}

	private void OnDestroy()
	{
		if (_toggleBinder != null)
		{
			_toggleBinder.ClearBindings();
		}
	}
}
