using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class OpenLevelPanelController : MonoBehaviour
{
	[SerializeField]
	private OpenLevelTableController _openLevelTableController;

	[SerializeField]
	private AlertPanelController _alertPanelController;

	[SerializeField]
	private Button _closeButton;

	private bool _isShown;

	private ButtonBinder _buttonBinder;

	private string[] _levelsNames;

	private string[] _levelsDirectories;

	private void OnDestroy()
	{
		if (_buttonBinder != null)
		{
			_buttonBinder.ClearBindings();
		}
	}

	public IEnumerator ShowCoroutine(Action<string> finishCallback)
	{
		Show(finishCallback);
		yield return new WaitUntil(() => !_isShown);
	}

	public void Show(Action<string> finishCallback)
	{
		_isShown = true;
		GetLevels();
		base.gameObject.SetActive(value: true);
		if (_buttonBinder != null)
		{
			_buttonBinder.ClearBindings();
		}
		_buttonBinder = new ButtonBinder(new List<Tuple<Button, Action>> { 
		{
			_closeButton,
			(Action)delegate
			{
				if (finishCallback != null)
				{
					finishCallback(null);
				}
			}
		} });
		_openLevelTableController.Init(_levelsNames, delegate(int row)
		{
			if (finishCallback != null)
			{
				finishCallback(_levelsDirectories[row]);
			}
		}, delegate(int row)
		{
			_alertPanelController.Show("Delete Level", "Do you really want to delete this level?", "Cancel", delegate
			{
				_alertPanelController.Hide();
			}, "Delete", delegate
			{
				_alertPanelController.Hide();
				DeleteLevel(_levelsDirectories[row]);
				GetLevels();
				_openLevelTableController.SetContent(_levelsNames);
			});
		});
	}

	private void GetLevels()
	{
		_levelsNames = null;
		_levelsDirectories = null;
		try
		{
			_levelsDirectories = Directory.GetDirectories(CustomLevelsModelSO.customLevelsDirectoryPath);
			List<string> list = new List<string>();
			string[] levelsDirectories = _levelsDirectories;
			foreach (string path in levelsDirectories)
			{
				list.Add(Path.GetFileName(path));
			}
			_levelsNames = list.ToArray();
		}
		catch
		{
		}
	}

	private void DeleteLevel(string levelDirectoryPath)
	{
		try
		{
			Directory.Delete(levelDirectoryPath, recursive: true);
		}
		catch
		{
		}
	}

	public void Hide()
	{
		base.gameObject.SetActive(value: false);
		_isShown = false;
	}
}
