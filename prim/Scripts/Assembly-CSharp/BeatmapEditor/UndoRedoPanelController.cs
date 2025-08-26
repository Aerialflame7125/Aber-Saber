using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class UndoRedoPanelController : MonoBehaviour
{
	[SerializeField]
	private EditorBeatmapSO _editorBeatmap;

	[Space]
	[SerializeField]
	private Button _undoButton;

	[SerializeField]
	private Button _redoButton;

	private ButtonBinder _buttonBinder;

	private void Awake()
	{
		_buttonBinder = new ButtonBinder(new List<Tuple<Button, Action>>
		{
			{
				_undoButton,
				(Action)delegate
				{
					_editorBeatmap.Undo();
				}
			},
			{
				_redoButton,
				(Action)delegate
				{
					_editorBeatmap.Redo();
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
}
