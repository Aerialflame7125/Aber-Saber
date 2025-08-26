using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class NoteTypeAndDirectionPanelController : MonoBehaviour
{
	[SerializeField]
	private EditorSelectedNoteTypeSO _selectedNoteType;

	[SerializeField]
	private EditorSelectedNoteCutDirectionSO _selectedNoteCutDirection;

	[Space]
	[SerializeField]
	private Toggle _directionToggleUp;

	[SerializeField]
	private Toggle _directionToggleUpLeft;

	[SerializeField]
	private Toggle _directionToggleUpRight;

	[SerializeField]
	private Toggle _directionToggleLeft;

	[SerializeField]
	private Toggle _directionToggleRight;

	[SerializeField]
	private Toggle _directionToggleDown;

	[SerializeField]
	private Toggle _directionToggleDownLeft;

	[SerializeField]
	private Toggle _directionToggleDownRight;

	[SerializeField]
	private Toggle _typeToggleNoteA;

	[SerializeField]
	private Toggle _typeToggleNoteB;

	[SerializeField]
	private Toggle _typeToggleBombNote;

	private ToggleBinder _directionalToggleBinder = new ToggleBinder();

	private ToggleBinder _typeToggleBinder = new ToggleBinder();

	private void Awake()
	{
		RefreshNoteCutDirectionToggles();
		RefreshNoteTypeToggles();
		_typeToggleBinder.AddBindings(new List<Tuple<Toggle, Action<bool>>>
		{
			{
				_typeToggleNoteA,
				(Action<bool>)delegate(bool on)
				{
					if (on)
					{
						_selectedNoteType.value = NoteType.NoteA;
					}
				}
			},
			{
				_typeToggleNoteB,
				(Action<bool>)delegate(bool on)
				{
					if (on)
					{
						_selectedNoteType.value = NoteType.NoteB;
					}
				}
			},
			{
				_typeToggleBombNote,
				(Action<bool>)delegate(bool on)
				{
					if (on)
					{
						_selectedNoteType.value = NoteType.Bomb;
					}
				}
			}
		});
		_directionalToggleBinder.AddBindings(new List<Tuple<Toggle, Action<bool>>>
		{
			{
				_directionToggleUp,
				(Action<bool>)delegate(bool on)
				{
					if (on)
					{
						_selectedNoteCutDirection.value = NoteCutDirection.Up;
					}
				}
			},
			{
				_directionToggleUpLeft,
				(Action<bool>)delegate(bool on)
				{
					if (on)
					{
						_selectedNoteCutDirection.value = NoteCutDirection.UpLeft;
					}
				}
			},
			{
				_directionToggleUpRight,
				(Action<bool>)delegate(bool on)
				{
					if (on)
					{
						_selectedNoteCutDirection.value = NoteCutDirection.UpRight;
					}
				}
			},
			{
				_directionToggleLeft,
				(Action<bool>)delegate(bool on)
				{
					if (on)
					{
						_selectedNoteCutDirection.value = NoteCutDirection.Left;
					}
				}
			},
			{
				_directionToggleRight,
				(Action<bool>)delegate(bool on)
				{
					if (on)
					{
						_selectedNoteCutDirection.value = NoteCutDirection.Right;
					}
				}
			},
			{
				_directionToggleDown,
				(Action<bool>)delegate(bool on)
				{
					if (on)
					{
						_selectedNoteCutDirection.value = NoteCutDirection.Down;
					}
				}
			},
			{
				_directionToggleDownLeft,
				(Action<bool>)delegate(bool on)
				{
					if (on)
					{
						_selectedNoteCutDirection.value = NoteCutDirection.DownLeft;
					}
				}
			},
			{
				_directionToggleDownRight,
				(Action<bool>)delegate(bool on)
				{
					if (on)
					{
						_selectedNoteCutDirection.value = NoteCutDirection.DownRight;
					}
				}
			}
		});
		_selectedNoteType.didChangeEvent += RefreshNoteTypeToggles;
		_selectedNoteCutDirection.didChangeEvent += RefreshNoteCutDirectionToggles;
	}

	private void OnDestroy()
	{
		if (_directionalToggleBinder != null)
		{
			_directionalToggleBinder.ClearBindings();
		}
		if (_typeToggleBinder != null)
		{
			_typeToggleBinder.ClearBindings();
		}
		_selectedNoteType.didChangeEvent -= RefreshNoteTypeToggles;
		_selectedNoteCutDirection.didChangeEvent -= RefreshNoteCutDirectionToggles;
	}

	private void RefreshNoteCutDirectionToggles()
	{
		_directionalToggleBinder.Disable();
		_directionToggleUp.isOn = _selectedNoteCutDirection.value == NoteCutDirection.Up;
		_directionToggleUpLeft.isOn = _selectedNoteCutDirection.value == NoteCutDirection.UpLeft;
		_directionToggleUpRight.isOn = _selectedNoteCutDirection.value == NoteCutDirection.UpRight;
		_directionToggleLeft.isOn = _selectedNoteCutDirection.value == NoteCutDirection.Left;
		_directionToggleRight.isOn = _selectedNoteCutDirection.value == NoteCutDirection.Right;
		_directionToggleDown.isOn = _selectedNoteCutDirection.value == NoteCutDirection.Down;
		_directionToggleDownLeft.isOn = _selectedNoteCutDirection.value == NoteCutDirection.DownLeft;
		_directionToggleDownRight.isOn = _selectedNoteCutDirection.value == NoteCutDirection.DownRight;
		_directionalToggleBinder.Enable();
	}

	private void RefreshNoteTypeToggles()
	{
		_typeToggleBinder.Disable();
		_typeToggleNoteA.isOn = _selectedNoteType.value == NoteType.NoteA;
		_typeToggleNoteB.isOn = _selectedNoteType.value == NoteType.NoteB;
		_typeToggleBombNote.isOn = _selectedNoteType.value == NoteType.Bomb;
		_typeToggleBinder.Enable();
	}
}
