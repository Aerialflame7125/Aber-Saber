using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CustomLevelStandardLevelLoader : ScriptableObject
{
	[SerializeField]
	private CustomLevelAudioClipLoader _customLevelAudioClipLoader;

	[SerializeField]
	private SimpleTextureLoaderSO _customLevelCoverImageLoader;

	private Dictionary<CustomLevelInfoWrapper, StandardLevelSO> cache = new Dictionary<CustomLevelInfoWrapper, StandardLevelSO>();

	public void LoadStandardLevel(CustomLevelInfoWrapper customLevelInfoWrapper, Action<StandardLevelSO> callback)
	{
		if (cache.ContainsKey(customLevelInfoWrapper))
		{
			callback(cache[customLevelInfoWrapper]);
		}
		else
		{
			SharedCoroutineStarter.instance.StartCoroutine(LoadStandardLevelCoroutine(customLevelInfoWrapper, callback));
		}
	}

	private IEnumerator LoadStandardLevelCoroutine(CustomLevelInfoWrapper customLevelInfoWrapper, Action<StandardLevelSO> callback)
	{
		StandardLevelSO standardLevelSO = ScriptableObject.CreateInstance<StandardLevelSO>();
		string levelID = customLevelInfoWrapper.levelID;
		string songName = customLevelInfoWrapper.customLevelInfo._songName;
		string songSubName = customLevelInfoWrapper.customLevelInfo._songSubName;
		string authorName = customLevelInfoWrapper.customLevelInfo._songAuthorName;
		float beatsPerMinute = customLevelInfoWrapper.customLevelInfo._beatsPerMinute;
		float previewStartTime = customLevelInfoWrapper.customLevelInfo._previewStartTime;
		float previewDuration = customLevelInfoWrapper.customLevelInfo._previewDuration;
		AudioClip audioClip = null;
		float audioDuration = 0f;
		Action<AudioClip> loadAudioClip = delegate(AudioClip loadedAudioClip)
		{
			audioClip = loadedAudioClip;
			audioDuration = audioClip.length;
		};
		yield return _customLevelAudioClipLoader.LoadAudioClipFromPathCoroutine(customLevelInfoWrapper.audioPath, loadAudioClip, true);
		Sprite coverImage = null;
		SceneInfo environmentSceneInfo = LoadSceneInfo(customLevelInfoWrapper.customLevelInfo._environmentName);
		StandardLevelSO.DifficultyBeatmap[] difficultyLevels = null;
		BeatmapDataSO[] beatmapsData = new BeatmapDataSO[customLevelInfoWrapper.customLevelInfo._difficultyBeatmaps.Length];
		for (int i = 0; i < beatmapsData.Length; i++)
		{
			beatmapsData[i] = ScriptableObject.CreateInstance<BeatmapDataSO>();
		}
		Action loadDifficultyLevels = delegate
		{
			difficultyLevels = LoadDifficultyLevels(customLevelInfoWrapper.customLevelInfo._difficultyBeatmaps, customLevelInfoWrapper.path, beatmapsData);
		};
		Action endCallback = delegate
		{
			standardLevelSO.InitFull(levelID, songName, songSubName, authorName, audioClip, audioDuration, beatsPerMinute, 0f, 0f, 1f, previewStartTime, previewDuration, coverImage, difficultyLevels, environmentSceneInfo);
			if (callback != null)
			{
				cache[customLevelInfoWrapper] = standardLevelSO;
				callback(standardLevelSO);
			}
		};
		HMTask task = new HMTask(loadDifficultyLevels, endCallback);
		task.Run();
	}

	private SceneInfo LoadSceneInfo(string environmentName)
	{
		return Resources.Load<SceneInfo>("SceneInfo/" + environmentName + "SceneInfo");
	}

	private StandardLevelSO.DifficultyBeatmap[] LoadDifficultyLevels(CustomLevelInfo.DifficultyLevelInfo[] difficultyLevelsInfo, string basePath, BeatmapDataSO[] beatmapsData)
	{
		StandardLevelSO.DifficultyBeatmap[] array = new StandardLevelSO.DifficultyBeatmap[difficultyLevelsInfo.Length];
		for (int i = 0; i < difficultyLevelsInfo.Length; i++)
		{
			array[i] = Load(difficultyLevelsInfo[i], basePath, beatmapsData[i]);
		}
		return array;
	}

	private StandardLevelSO.DifficultyBeatmap Load(CustomLevelInfo.DifficultyLevelInfo difficultyLevelsInfo, string basePath, BeatmapDataSO beatmapData)
	{
		LevelDifficulty levelDifficulty;
		if (!difficultyLevelsInfo._difficulty.LevelDifficultyFromName(out levelDifficulty))
		{
			return null;
		}
		int difficultyRank = difficultyLevelsInfo._difficultyRank;
		float noteJumpMovementSpeed = difficultyLevelsInfo._noteJumpMovementSpeed;
		string jsonData = File.ReadAllText(Path.Combine(basePath, difficultyLevelsInfo._beatmapFilename));
		beatmapData.SetJsonData(jsonData);
		return new StandardLevelSO.DifficultyBeatmap(null, levelDifficulty, difficultyRank, noteJumpMovementSpeed, beatmapData);
	}
}
