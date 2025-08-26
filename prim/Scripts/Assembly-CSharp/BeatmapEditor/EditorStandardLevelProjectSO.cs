using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace BeatmapEditor;

public class EditorStandardLevelProjectSO : ScriptableObject
{
	[SerializeField]
	private EditorBeatmapSO _editorBeatmap;

	[SerializeField]
	private EditorAudioSO _editorAudio;

	[SerializeField]
	private EditorLevelCoverImageSO _coverImage;

	[SerializeField]
	private EditorStandardLevelInfoSO _levelInfo;

	[SerializeField]
	private EditorSongParamsSO _songParams;

	[Space]
	[SerializeField]
	private ActiveDifficultySO _activeDifficulty;

	private EditorBeatsDataSet _beatsDataSet = new EditorBeatsDataSet();

	private string _openedProjectDirectoryPath;

	public bool beatmapIsInitialized => _editorBeatmap.hasBeatsData;

	public bool canSaveProject => _editorAudio.isAudioLoaded;

	public EditorStandardLevelInfoSO levelInfo => _levelInfo;

	public string openedProjectDirectoryPath => _openedProjectDirectoryPath;

	private void OnEnable()
	{
		base.hideFlags |= HideFlags.DontUnloadUnusedAsset;
		_activeDifficulty.didChangeEvent += HandleActiveDifficultyDidChange;
	}

	private void OnDisable()
	{
		if (_activeDifficulty != null)
		{
			_activeDifficulty.didChangeEvent -= HandleActiveDifficultyDidChange;
		}
	}

	public void InitNewProject()
	{
		_editorBeatmap.InitWithEmptyData(1);
		_editorAudio.Clear();
		_coverImage.Clear();
		_levelInfo.SetDefaults();
		_songParams.SetDefaults();
		_beatsDataSet.Clear();
		_activeDifficulty.difficulty = LevelDifficulty.Expert;
		_openedProjectDirectoryPath = null;
	}

	public IEnumerator SaveProjectCoroutine(string projectDirectoryPath, Action<bool> finishCallback)
	{
		_beatsDataSet[_activeDifficulty.difficulty] = _editorBeatmap.beatsData;
		List<BeatmapSaveData> beatmapSaveDataList = new List<BeatmapSaveData>();
		List<StandardLevelSaveData.DifficultyBeatmap> difficultyBeatmaps = new List<StandardLevelSaveData.DifficultyBeatmap>();
		IEnumerator enumerator = Enum.GetValues(typeof(LevelDifficulty)).GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				LevelDifficulty levelDifficulty = (LevelDifficulty)enumerator.Current;
				string text = levelDifficulty.Name();
				string text2 = text + ".dat";
				string path = Path.Combine(projectDirectoryPath, text2);
				EditorBeatsData editorBeatsData = _beatsDataSet[levelDifficulty];
				if (editorBeatsData == null)
				{
					if (File.Exists(path))
					{
						File.Delete(path);
					}
					continue;
				}
				BeatmapSaveData beatmapSaveData = editorBeatsData.ConvertToBeatsSaveData(_songParams.beatsPerMinute, clipToTime: true, _editorAudio.songDuration);
				if (beatmapSaveData == null)
				{
					if (File.Exists(path))
					{
						File.Delete(path);
					}
				}
				else
				{
					StandardLevelSaveData.DifficultyBeatmap item = new StandardLevelSaveData.DifficultyBeatmap(text, levelDifficulty.DefaultRating(), text2, 0);
					difficultyBeatmaps.Add(item);
					beatmapSaveDataList.Add(beatmapSaveData);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			IDisposable disposable2 = (disposable = enumerator as IDisposable);
			if (disposable != null)
			{
				disposable2.Dispose();
			}
		}
		StandardLevelSaveData levelSaveData = new StandardLevelSaveData(_levelInfo.songName, _levelInfo.songSubName, _levelInfo.songAuthorName, _levelInfo.levelAuthorName, _songParams.beatsPerMinute, _songParams.songTimeOffset, _songParams.shuffleStrength, _songParams.shufflePeriod, _editorAudio.songDuration * 0.5f, Mathf.Min(10f, _editorAudio.songDuration * 0.5f), _editorAudio.audioFileName, _coverImage.imageFileName, "DefaultEnvironment", difficultyBeatmaps.ToArray());
		yield return null;
		if (!Directory.Exists(projectDirectoryPath))
		{
			Directory.CreateDirectory(projectDirectoryPath);
		}
		yield return null;
		string[] audioFilePaths = FileHelpers.GetFilePaths(projectDirectoryPath, new HashSet<string> { "wav", "mp3", "ogg" });
		string[] array = audioFilePaths;
		foreach (string text3 in array)
		{
			if (text3 != _editorAudio.audioFilePath)
			{
				File.Delete(text3);
			}
		}
		yield return null;
		string[] imageFilePaths = FileHelpers.GetFilePaths(projectDirectoryPath, new HashSet<string> { "png" });
		string[] array2 = imageFilePaths;
		foreach (string text4 in array2)
		{
			if (text4 != _coverImage.imageFilePath)
			{
				File.Delete(text4);
			}
		}
		yield return null;
		string infoJSONFilePath = Path.Combine(projectDirectoryPath, CustomLevelsModelSO.kStandardLevelInfoFileName);
		FileHelpers.SaveToJSONFile(levelSaveData, infoJSONFilePath, infoJSONFilePath + ".tmp", null);
		yield return null;
		for (int k = 0; k < difficultyBeatmaps.Count; k++)
		{
			string text5 = Path.Combine(projectDirectoryPath, difficultyBeatmaps[k].beatmapFilename);
			FileHelpers.SaveToJSONFile(beatmapSaveDataList[k], text5, text5 + ".tmp", null);
		}
		yield return null;
		string audioDestFilePath = Path.Combine(projectDirectoryPath, _editorAudio.audioFileName);
		if (_editorAudio.audioFilePath != audioDestFilePath)
		{
			File.Copy(_editorAudio.audioFilePath, audioDestFilePath, overwrite: true);
		}
		yield return null;
		if ((bool)_coverImage.texture)
		{
			string text6 = Path.Combine(projectDirectoryPath, _coverImage.imageFileName);
			if (_coverImage.imageFilePath != text6)
			{
				File.WriteAllBytes(text6, _coverImage.texture.EncodeToPNG());
			}
		}
		_openedProjectDirectoryPath = projectDirectoryPath;
		finishCallback(obj: true);
	}

	public IEnumerator OpenProjectCoroutine(string projectDirectoryPath, Action<string> finishCallback)
	{
		string infoJSONFilePath = Path.Combine(projectDirectoryPath, CustomLevelsModelSO.kStandardLevelInfoFileName);
		StandardLevelSaveData levelSaveData = FileHelpers.LoadFromJSONFile<StandardLevelSaveData>(infoJSONFilePath);
		if (levelSaveData == null || !levelSaveData.hasAllData)
		{
			finishCallback("Info.dat could not be loaded.");
			yield break;
		}
		yield return null;
		_levelInfo.SetValues(levelSaveData.songName, levelSaveData.songSubName, levelSaveData.songAuthorName, levelSaveData.levelAuthorName);
		_songParams.SetValues(levelSaveData.beatsPerMinute, levelSaveData.songTimeOffset, levelSaveData.shuffle, levelSaveData.shufflePeriod);
		_beatsDataSet.Clear();
		StandardLevelSaveData.DifficultyBeatmap[] difficultyBeatmaps = levelSaveData.difficultyBeatmaps;
		foreach (StandardLevelSaveData.DifficultyBeatmap difficultyBeatmap in difficultyBeatmaps)
		{
			string filePath = Path.Combine(projectDirectoryPath, difficultyBeatmap.beatmapFilename);
			BeatmapSaveData beatmapSaveData = FileHelpers.LoadFromJSONFile<BeatmapSaveData>(filePath);
			if (beatmapSaveData == null)
			{
				finishCallback("Difficulty beatmap could not be loaded.");
				yield break;
			}
			if (!difficultyBeatmap.difficulty.LevelDifficultyFromName(out var levelDifficulty))
			{
				finishCallback("Unknown difficulty name in beatmap found.");
				yield break;
			}
			_beatsDataSet[levelDifficulty] = beatmapSaveData.ConvertToEditorBeatsData();
		}
		yield return null;
		SetActiveBeatmapDataFromBeatmapDataSet(_activeDifficulty.difficulty);
		string coverImageFilePath = Path.Combine(projectDirectoryPath, levelSaveData.coverImageFilename);
		yield return _coverImage.LoadImageCoroutine(coverImageFilePath, delegate(EditorLevelCoverImageSO.LoadingResult loadingResult)
		{
			if (loadingResult != 0)
			{
				_coverImage.Clear();
			}
		});
		yield return null;
		string songFilePath = Path.Combine(projectDirectoryPath, levelSaveData.songFilename);
		yield return _editorAudio.LoadAudioCoroutine(songFilePath, delegate
		{
			if (!_editorAudio.isAudioLoaded)
			{
				finishCallback("Song file could not be loaded.");
			}
		});
		if (_editorAudio.isAudioLoaded)
		{
			_openedProjectDirectoryPath = projectDirectoryPath;
			finishCallback(null);
		}
	}

	public IEnumerator ExportProjectCoroutine(string exportFilePath, Action<bool> finishCallback)
	{
		string exportDirectoryPath = Path.GetDirectoryName(exportFilePath);
		string tempDirectoryPath;
		do
		{
			tempDirectoryPath = Path.Combine(exportDirectoryPath, Path.GetRandomFileName());
		}
		while (Directory.Exists(tempDirectoryPath));
		try
		{
			Directory.CreateDirectory(tempDirectoryPath);
		}
		catch
		{
			finishCallback(obj: false);
			yield break;
		}
		Action cleanupAction = delegate
		{
			if (Directory.Exists(tempDirectoryPath))
			{
				try
				{
					DirectoryInfo directoryInfo = new DirectoryInfo(tempDirectoryPath);
					directoryInfo.Delete(recursive: true);
				}
				catch
				{
				}
			}
		};
		yield return SaveProjectCoroutine(tempDirectoryPath, delegate(bool success)
		{
			if (!success)
			{
				cleanupAction();
				finishCallback(obj: false);
			}
		});
		bool createZipFinished = false;
		FileCompressionHelper.CreateZipFromDirectoryAsync(tempDirectoryPath, exportFilePath, delegate(bool success)
		{
			cleanupAction();
			finishCallback(success);
			createZipFinished = true;
		});
		yield return new WaitUntil(() => createZipFinished);
	}

	public IEnumerator ImportAudioCoroutine(string filePath, Action finishCallback)
	{
		bool loadingFinished = false;
		_editorAudio.LoadAudio(filePath, delegate
		{
			if (finishCallback != null)
			{
				finishCallback();
			}
			loadingFinished = true;
		});
		yield return new WaitUntil(() => loadingFinished);
	}

	private void HandleActiveDifficultyDidChange(LevelDifficulty prevDifficulty, LevelDifficulty currentDifficulty)
	{
		_beatsDataSet[prevDifficulty] = _editorBeatmap.beatsData;
		SetActiveBeatmapDataFromBeatmapDataSet(currentDifficulty);
	}

	private void SetActiveBeatmapDataFromBeatmapDataSet(LevelDifficulty difficulty)
	{
		if (_beatsDataSet[difficulty] != null)
		{
			_editorBeatmap.LoadData(_beatsDataSet[difficulty]);
			return;
		}
		float num = ((!_editorAudio.isAudioLoaded) ? 0f : _editorAudio.songDuration);
		int numberOfBeats = Mathf.CeilToInt(num * (float)_songParams.beatsPerMinute / 60f) + 1;
		_editorBeatmap.InitWithEmptyData(numberOfBeats);
	}
}
