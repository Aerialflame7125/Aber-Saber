using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using SFB;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class StandardLevelProjectController : MonoBehaviour
{
	[SerializeField]
	private EditorStandardLevelProjectSO _editorStandardLevelProject;

	[Space]
	[SerializeField]
	private Button _testBeatmapButton;

	[SerializeField]
	private Button _newButton;

	[SerializeField]
	private Button _openButton;

	[SerializeField]
	private Button _saveButton;

	[SerializeField]
	private Button _saveAsButton;

	[SerializeField]
	private Button _importButton;

	[SerializeField]
	private Button _exportButton;

	[SerializeField]
	private Button _importAudioButton;

	[Space]
	[SerializeField]
	private PlaybackController _playbackController;

	[SerializeField]
	private BeatmapTestStarter _beatmapTestStarter;

	[Space]
	[SerializeField]
	private OpenLevelPanelController _openLevelPanelController;

	[SerializeField]
	private SaveAsPanelController _saveAsPanelController;

	[Space]
	[SerializeField]
	private LoadingIndicator _loadingIndicator;

	[SerializeField]
	private AlertPanelController _alertPanelController;

	[SerializeField]
	private EditorPopUpInfoPanelController _popUpInfoPanelController;

	private ButtonBinder _buttonBinder;

	private void Start()
	{
		if (!_editorStandardLevelProject.beatmapIsInitialized)
		{
			_editorStandardLevelProject.InitNewProject();
		}
		_playbackController.ResumeSavedPosition();
		_buttonBinder = new ButtonBinder(new List<Tuple<Button, Action>>
		{
			{
				_testBeatmapButton,
				(Action)HandleTestBeatmapButtonPressed
			},
			{
				_newButton,
				(Action)HandleNewButtonPressed
			},
			{
				_openButton,
				(Action)delegate
				{
					StartCoroutine(OpenLevelCoroutine());
				}
			},
			{
				_saveButton,
				(Action)HandleSaveButtonPressed
			},
			{
				_saveAsButton,
				(Action)delegate
				{
					StartCoroutine(SaveAsLevelCoroutine());
				}
			},
			{
				_importButton,
				(Action)HandleImportButtonPressed
			},
			{
				_exportButton,
				(Action)delegate
				{
					StartCoroutine(ExportLevelCoroutine());
				}
			},
			{
				_importAudioButton,
				(Action)delegate
				{
					StartCoroutine(ImportAudioCoroutine());
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

	private void HandleNewButtonPressed()
	{
		_alertPanelController.Show("New Project", "Do you really want to create new project. All unsaved data will be lost", "Cancel", delegate
		{
			_alertPanelController.Hide();
		}, "Create New", delegate
		{
			_alertPanelController.Hide();
			_editorStandardLevelProject.InitNewProject();
		});
	}

	private void HandleTestBeatmapButtonPressed()
	{
		switch (_beatmapTestStarter.TestBeatmap())
		{
		case BeatmapTestStarter.TestStartResult.NoAudio:
			_popUpInfoPanelController.ShowInfo("Can not start test, because there is no audio loaded.", EditorPopUpInfoPanelController.InfoType.Warning);
			break;
		case BeatmapTestStarter.TestStartResult.NoBeatsData:
			_popUpInfoPanelController.ShowInfo("Can not start test, because there are no beat data.", EditorPopUpInfoPanelController.InfoType.Warning);
			break;
		}
	}

	private IEnumerator OpenLevelCoroutine()
	{
		string newLevelDirectoryPath = null;
		yield return _openLevelPanelController.ShowCoroutine(delegate(string levelDirectoryPath)
		{
			newLevelDirectoryPath = levelDirectoryPath;
			_openLevelPanelController.Hide();
		});
		if (string.IsNullOrEmpty(newLevelDirectoryPath))
		{
			yield break;
		}
		_loadingIndicator.ShowLoading();
		yield return _editorStandardLevelProject.OpenProjectCoroutine(newLevelDirectoryPath, delegate(string errorString)
		{
			if (!string.IsNullOrEmpty(errorString))
			{
				_popUpInfoPanelController.ShowInfo("Level loading failed - " + errorString, EditorPopUpInfoPanelController.InfoType.Warning);
				_editorStandardLevelProject.InitNewProject();
			}
		});
		_loadingIndicator.HideLoading();
	}

	private void HandleSaveButtonPressed()
	{
		if (string.IsNullOrEmpty(_editorStandardLevelProject.openedProjectDirectoryPath))
		{
			StartCoroutine(SaveAsLevelCoroutine());
		}
		else
		{
			StartCoroutine(SaveLevelCoroutine(_editorStandardLevelProject.openedProjectDirectoryPath));
		}
	}

	private IEnumerator SaveAsLevelCoroutine()
	{
		if (!_editorStandardLevelProject.canSaveProject)
		{
			_alertPanelController.Show("Warning", "This level can not be saved, because it does not contain any audio data. Use IMPORT AUDIO button in files panel to add audio.", "OK", delegate
			{
				_alertPanelController.Hide();
			});
			yield break;
		}
		string defaultLevelName = CustomLevelsModelSO.GetDefaultNameForCustomLevel(_editorStandardLevelProject.levelInfo.songName, _editorStandardLevelProject.levelInfo.songAuthorName, _editorStandardLevelProject.levelInfo.levelAuthorName);
		string newLevelName = null;
		yield return _saveAsPanelController.ShowCoroutine(defaultLevelName, delegate(string levelName)
		{
			newLevelName = levelName;
			_saveAsPanelController.Hide();
		});
		if (string.IsNullOrEmpty(newLevelName))
		{
			yield break;
		}
		string levelDirectoryPath = Path.Combine(CustomLevelsModelSO.customLevelsDirectoryPath, newLevelName);
		if (Directory.Exists(levelDirectoryPath))
		{
			bool shouldOverwrite = false;
			yield return _alertPanelController.ShowCoroutine("Warning", "Level with specified name already exist, do you want to overwrite it?", "Cancel", delegate
			{
				_alertPanelController.Hide();
			}, "Overwrite", delegate
			{
				_alertPanelController.Hide();
				shouldOverwrite = true;
			});
			if (!shouldOverwrite)
			{
				yield break;
			}
		}
		yield return SaveLevelCoroutine(levelDirectoryPath);
	}

	private IEnumerator SaveLevelCoroutine(string levelDirectoryPath)
	{
		_loadingIndicator.ShowLoading();
		yield return _editorStandardLevelProject.SaveProjectCoroutine(levelDirectoryPath, delegate(bool success)
		{
			if (success)
			{
				_popUpInfoPanelController.ShowInfo("Level was saved.", EditorPopUpInfoPanelController.InfoType.Info);
			}
			else
			{
				_popUpInfoPanelController.ShowInfo("Error occured while saving the level.", EditorPopUpInfoPanelController.InfoType.Warning);
			}
		});
		_loadingIndicator.HideLoading();
	}

	private void HandleImportButtonPressed()
	{
	}

	private IEnumerator ExportLevelCoroutine()
	{
		if (!_editorStandardLevelProject.canSaveProject)
		{
			_alertPanelController.Show("Warning", "This level can not be exported, because it does not contain any audio data. Use IMPORT AUDIO button in files panel to add audio.", "OK", delegate
			{
				_alertPanelController.Hide();
			});
			yield break;
		}
		_loadingIndicator.ShowLoading();
		yield return null;
		string defaultName = CustomLevelsModelSO.GetDefaultNameForCustomLevel(_editorStandardLevelProject.levelInfo.songName, _editorStandardLevelProject.levelInfo.songAuthorName, _editorStandardLevelProject.levelInfo.levelAuthorName);
		string filePath = NativeFileDialogs.SaveFileDialog("Export Level", defaultName, "bsl", null);
		yield return null;
		if (string.IsNullOrEmpty(filePath))
		{
			_loadingIndicator.HideLoading();
			yield break;
		}
		if (File.Exists(filePath))
		{
			File.Delete(filePath);
		}
		yield return _editorStandardLevelProject.ExportProjectCoroutine(filePath, delegate(bool success)
		{
			if (success)
			{
				_popUpInfoPanelController.ShowInfo("Level was successfully exported.", EditorPopUpInfoPanelController.InfoType.Info);
			}
			else
			{
				_popUpInfoPanelController.ShowInfo("Export failed.", EditorPopUpInfoPanelController.InfoType.Warning);
			}
		});
		_loadingIndicator.HideLoading();
	}

	private IEnumerator ImportAudioCoroutine()
	{
		_loadingIndicator.ShowLoading();
		yield return null;
		string filePath = NativeFileDialogs.OpenFileDialog("Import Audio", new ExtensionFilter[1]
		{
			new ExtensionFilter("Audio Files", "wav", "ogg", "mp3")
		}, null);
		if (filePath != null && filePath != string.Empty)
		{
			yield return _editorStandardLevelProject.ImportAudioCoroutine(filePath, delegate
			{
				_loadingIndicator.HideLoading();
			});
		}
		_loadingIndicator.HideLoading();
	}
}
