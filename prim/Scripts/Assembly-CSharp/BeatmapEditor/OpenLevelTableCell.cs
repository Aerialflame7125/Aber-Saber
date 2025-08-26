using System;
using System.Collections.Generic;
using HMUI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class OpenLevelTableCell : TableCell
{
	[SerializeField]
	private TextMeshProUGUI _text;

	[SerializeField]
	private UnityEngine.UI.Image _bgImage;

	[SerializeField]
	private UnityEngine.UI.Image _highlightImage;

	[SerializeField]
	private Button _deleteButton;

	private ButtonBinder _buttonBinder;

	public string text
	{
		get
		{
			return _text.text;
		}
		set
		{
			_text.text = value;
		}
	}

	public event Action<int> deleteButtonPressedEvent;

	protected override void Start()
	{
		base.Start();
		_buttonBinder = new ButtonBinder(new List<Tuple<Button, Action>> { 
		{
			_deleteButton,
			(Action)delegate
			{
				if (this.deleteButtonPressedEvent != null)
				{
					this.deleteButtonPressedEvent(base.row);
				}
			}
		} });
	}

	protected override void OnDestroy()
	{
		if (_buttonBinder != null)
		{
			_buttonBinder.ClearBindings();
		}
		base.OnDestroy();
	}

	protected override void SelectionDidChange(TransitionType transitionType)
	{
		if (base.selected)
		{
			_highlightImage.enabled = false;
			_bgImage.enabled = true;
			_text.color = Color.black;
		}
		else
		{
			_bgImage.enabled = false;
			_text.color = Color.white;
		}
	}

	protected override void HighlightDidChange(TransitionType transitionType)
	{
		if (base.selected)
		{
			_highlightImage.enabled = false;
			return;
		}
		_highlightImage.enabled = base.highlighted;
		_deleteButton.gameObject.SetActive(base.highlighted);
	}
}
