using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.XR; // Not used in this snippet, but kept for context
using System;
using System.IO;
using System.Linq;
using Newtonsoft.Json; // Used for some internal parsing if needed, but JsonUtility for main objects
using Newtonsoft.Json.Linq; // Kept for context with SimpleJSON/V3ToV2
using SimpleJSON; // Your V3ToV2 dependency
using System.Threading.Tasks; // For background threading

namespace SongLoader
{
    public class MainSongLoader : MonoBehaviour
    {
        [SerializeField]
        private StandardLevelCollectionSO _NoArrowsCollection;

        [SerializeField]
        private TextMeshProUGUI _LoadingText;

        [SerializeField]
        private HMUI.Image _LoadingBar;

        [SerializeField]
        private GameObject _loadingTextObject;

        [SerializeField]
        private GameObject _loadingBarObject;

        [SerializeField]
        private SceneInfo _niceEnvironmentSceneInfo;

        [SerializeField]
        private BL.Leaderboards.GetLeaderboard getLeaderboard;

        private static StandardLevelCollectionSO _persistentCustomCollection = null;

        // --- Threading utility ---
        // A queue for actions to be executed on the main Unity thread.
        private static readonly Queue<Action> _mainThreadActions = new Queue<Action>();
        // A lock object to ensure thread-safe access to the queue.
        private static readonly object _mainThreadLock = new object();

        // Call this from Update to process queued actions.
        void Update()
        {
            // Process actions from background threads on the main thread
            lock (_mainThreadLock)
            {
                while (_mainThreadActions.Count > 0)
                {
                    _mainThreadActions.Dequeue().Invoke();
                }
            }
        }

        // Helper to enqueue actions to be run on the main thread.
        private static void EnqueueMainThreadAction(Action action)
        {
            lock (_mainThreadLock)
            {
                _mainThreadActions.Enqueue(action);
            }
        }
        // --- End Threading utility ---

        [System.Serializable]
        public class DifficultyBeatmap
        {
            public string _difficulty;
            public int _difficultyRank;
            public string _beatmapFilename;
            public float _noteJumpMovementSpeed;
            public float _noteJumpStartBeatOffset;
            // Add _customData if you need it
        }

        [System.Serializable]
        public class DifficultyBeatmapSet
        {
            public string _beatmapCharacteristicName;
            public List<DifficultyBeatmap> _difficultyBeatmaps;
        }

        [System.Serializable]
        public class Beatmap
        {
            public string version; // This is used by your ProcessBeatmapJson
            // Add other root-level properties of your Beatmap.dat if JsonUtility is used directly on it
            // For example, if you need to read _events, _notes, etc. at this level
        }

        [System.Serializable]
        public class SongInfo
        {
            public string _version;
            public string _songName;
            public string _songSubName;
            public string _songAuthorName;
            public string _levelAuthorName;
            public string _beatsPerMinute;
            public string _previewStartTime;
            public string _previewDuration;
            public string _songFilename;
            public string _coverImageFilename;
            public float _shuffle;
            public float _shufflePeriod;
            public float _songTimeOffset;
            public List<DifficultyBeatmapSet> _difficultyBeatmapSets;
        }

        // A class to hold all raw data loaded from a song directory before creating Unity objects
        private class RawSongData
        {
            public string DirectoryPath;
            public SongInfo Info;
            public string mapCode;
            public byte[] CoverImageData; // Raw bytes for the cover image
            public Dictionary<string, string> BeatmapJsons; // Key: filename, Value: json string
            public string SongAudioPath; // Path to the audio file, Audio loading is still a Coroutine
        }

        public StandardLevelCollectionSO MergeWithNoArrowsCollection(List<StandardLevelSO> customLevels)
        {
            List<StandardLevelSO> mergedLevels = new List<StandardLevelSO>();
            if (_NoArrowsCollection != null && _NoArrowsCollection.levels != null)
                mergedLevels.AddRange(_NoArrowsCollection.levels);

            mergedLevels.AddRange(customLevels);

            StandardLevelCollectionSO mergedCollection = ScriptableObject.CreateInstance<StandardLevelCollectionSO>();
            mergedCollection.levels = mergedLevels.ToArray();

            Debug.Log($"Merged collection contains {mergedCollection.levels.Length} levels.");
            return mergedCollection;
        }

        public void ApplyMergedCollectionToGameplayModes(List<StandardLevelSO> customLevels, bool forceReload = false)
        {
            _loadingBarObject.SetActive(true);
            _loadingTextObject.SetActive(true);

            if (_persistentCustomCollection == null || forceReload)
            {
                _persistentCustomCollection = MergeWithNoArrowsCollection(customLevels);
                Debug.Log("Created new persistent custom collection.");
            }
            else
            {
                Debug.Log("Using existing persistent custom collection.");
            }

            var collectionsForModes = Resources.FindObjectsOfTypeAll<LevelCollectionsForGameplayModes>().FirstOrDefault();
            if (collectionsForModes == null)
            {
                Debug.LogError("LevelCollectionsForGameplayModes not found!");
                return;
            }

            var mergedForNoArrows = new LevelCollectionsForGameplayModes.LevelCollectionForGameplayMode();
            mergedForNoArrows._gameplayMode = GameplayMode.SoloNoArrows;
            mergedForNoArrows._levelCollection = _persistentCustomCollection;

            var existing = collectionsForModes._collections.ToList();
            int idx = existing.FindIndex(x => x._gameplayMode == GameplayMode.SoloNoArrows);
            if (idx >= 0)
                existing[idx] = mergedForNoArrows;
            else
                existing.Add(mergedForNoArrows);

            collectionsForModes._collections = existing.ToArray();

            Debug.Log("Applied persistent collection to LevelCollectionsForGameplayModes.");
        }

        public void StartLoadingAndMerge(bool forceReload = false)
        {
            StartCoroutine(LoadCustomSongsWithProgressCoroutine((customLevels) =>
            {
                ApplyMergedCollectionToGameplayModes(customLevels, forceReload);
            }));
        }

        public IEnumerator LoadCustomSongsWithProgressCoroutine(Action<List<StandardLevelSO>> onLoaded)
        {
            IEnumerator DelayedAction(float delay)
            {
                yield return new WaitForSeconds(delay);
                _loadingBarObject.SetActive(false);
                _loadingTextObject.SetActive(false);
            }

            List<StandardLevelSO> customLevels = new List<StandardLevelSO>();
            string customLevelsPath;
#if UNITY_ANDROID && !UNITY_EDITOR
            customLevelsPath = Path.Combine(Application.persistentDataPath, "CustomLevels");
#else
            customLevelsPath = Path.Combine(Application.dataPath, "CustomLevels");
#endif

            if (!Directory.Exists(customLevelsPath))
            {
                _loadingBarObject.SetActive(true);
                _loadingTextObject.SetActive(true);
                _LoadingBar.fillAmount = 1f;
                Debug.LogError($"Custom levels path does not exist: {customLevelsPath}");
                Directory.CreateDirectory(customLevelsPath);
                _LoadingText.text = "No custom levels found!";
                StartCoroutine(DelayedAction(3f)); // Shortened delay for no levels
                yield break;
            }

            string[] subdirectories = Directory.GetDirectories(customLevelsPath);
            int totalSongs = subdirectories.Length;
            int songsProcessed = 0;

            _loadingBarObject.SetActive(true);
            _loadingTextObject.SetActive(true);
            _LoadingBar.fillAmount = 0f;
            _LoadingText.text = "Preparing to load songs...";

            List<RawSongData> rawSongDataList = new List<RawSongData>();
            List<Task<RawSongData>> backgroundTasks = new List<Task<RawSongData>>();

            // Phase 1: Start background tasks for reading and parsing JSON/image bytes
            foreach (string subdir in subdirectories)
            {
                string infoPath = Path.Combine(subdir, "Info.dat");
                if (!File.Exists(infoPath))
                {
                    Debug.LogWarning($"Info.dat not found in {subdir}. Skipping this directory.");
                    totalSongs--; // Adjust total count if a directory is skipped
                    continue;
                }

                // Start a background task for each song to read file data and parse JSON
                backgroundTasks.Add(Task.Run(() => ReadAndParseSongDataInBackground(subdir, infoPath)));
            }

            // Yield and wait for each background task to complete, updating progress
            while (backgroundTasks.Any())
            {
                // Wait for any task to complete
                Task<RawSongData> completedTask = Task.WhenAny(backgroundTasks).Result;
                backgroundTasks.Remove(completedTask);

                RawSongData rawData = null;
                try
                {
                    rawData = completedTask.Result;
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error in background song data processing: {e.Message}");
                    // Don't add this song to the list, but increment processed count
                }

                if (rawData != null)
                {
                    rawSongDataList.Add(rawData);
                }

                songsProcessed++;
                _LoadingText.text = $"Loading songs... ({songsProcessed}/{totalSongs})";
                _LoadingBar.fillAmount = (float)songsProcessed / totalSongs;
                yield return null; // Yield to update UI
            }

            Debug.Log($"Finished background data loading for {rawSongDataList.Count} songs. Now compiling on main thread.");
            _LoadingText.text = $"Compiling songs...";
            _LoadingBar.fillAmount = 0f; // Reset for compilation phase

            // Phase 2: Create Unity objects on the main thread
            int compiledSongs = 0;
            foreach (RawSongData rawData in rawSongDataList)
            {
                yield return ProcessRawSongDataOnMainThread(rawData, (level) =>
                {
                    if (level != null)
                    {
                        customLevels.Add(level);
                    }
                    compiledSongs++;
                    _LoadingText.text = $"Compiling songs... ({compiledSongs}/{rawSongDataList.Count})";
                    _LoadingBar.fillAmount = (float)compiledSongs / rawSongDataList.Count;
                });
                yield return null; // Yield again to ensure UI updates after each song compilation
            }

            Debug.Log("All custom songs loaded and compiled!");
            _LoadingText.text = $"Loaded {customLevels.Count} custom songs!";
            onLoaded?.Invoke(customLevels);
            _LoadingBar.fillAmount = 1f;
            StartCoroutine(DelayedAction(3f)); // Shortened delay for successful load
        }


        // This method runs entirely on a background thread.
        private async Task<RawSongData> ReadAndParseSongDataInBackground(string directoryPath, string infoPath)
        {
            RawSongData rawData = new RawSongData
            {
                DirectoryPath = directoryPath,
                BeatmapJsons = new Dictionary<string, string>()
            };

            // Read Info.dat
            string json = await Task.Run(() => File.ReadAllText(infoPath));
            rawData.Info = JsonUtility.FromJson<SongInfo>(json);

            // Validate Info.dat (e.g., check for Standard characteristic)
            bool hasStandard = false;
            if (rawData.Info._difficultyBeatmapSets != null)
            {
                foreach (var set in rawData.Info._difficultyBeatmapSets)
                {
                    if (set._beatmapCharacteristicName == "Standard")
                    {
                        hasStandard = true;
                        break;
                    }
                }
            }
            if (!hasStandard)
            {
                throw new InvalidOperationException($"No Standard characteristic found in {rawData.Info._songName}.");
            }

            // Read cover image bytes
            string coverImagePath = Path.Combine(directoryPath, rawData.Info._coverImageFilename);
            if (File.Exists(coverImagePath))
            {
                rawData.CoverImageData = await Task.Run(() => File.ReadAllBytes(coverImagePath));
            }

            // Store song audio path (actual loading will be a Coroutine)
            rawData.SongAudioPath = Path.Combine(directoryPath, rawData.Info._songFilename);

            string beatmapFolderName = new DirectoryInfo(directoryPath).Name;
            int delimindex = beatmapFolderName.IndexOf("(");
            if (delimindex > 0)
            {
                rawData.mapCode = beatmapFolderName.Substring(0, delimindex - 1);
            }

            // Read and process beatmap data files in background
            foreach (var set in rawData.Info._difficultyBeatmapSets)
            {
                if (set._beatmapCharacteristicName != "Standard") continue;

                foreach (var diff in set._difficultyBeatmaps)
                {
                    string beatmapPath = Path.Combine(directoryPath, diff._beatmapFilename);
                    if (File.Exists(beatmapPath))
                    {
                        string beatmapDataJson = await Task.Run(() => File.ReadAllText(beatmapPath));
                        // ProcessBeatmapJson (V3 to V2 conversion) should also run in the background
                        string processedBeatmapJson = ProcessBeatmapJson(beatmapDataJson, rawData.Info._songName);
                        rawData.BeatmapJsons[diff._beatmapFilename] = processedBeatmapJson;
                        
                    }
                    else
                    {
                        Debug.LogWarning($"Beatmap file not found: {beatmapPath} for song {rawData.Info._songName}. Skipping this difficulty.");
                    }
                }
            }
            return rawData;
        }

        // This method runs on the main thread and creates Unity objects.
        private IEnumerator ProcessRawSongDataOnMainThread(RawSongData rawData, Action<StandardLevelSO> onComplete)
        {
            // Create StandardLevelSO
            StandardLevelSO level = ScriptableObject.CreateInstance<StandardLevelSO>();

            // Map general song info
            level._levelID = rawData.mapCode;
            level._songName = rawData.Info._songName;
            Debug.Log($"Map code for song {level._songName}: {level._levelID}");
            level._songSubName = rawData.Info._songSubName;
            level._songAuthorName = rawData.Info._songAuthorName;
            level._beatsPerMinute = float.TryParse(rawData.Info._beatsPerMinute, out float bpm) ? bpm : 120f;
            level._previewStartTime = float.TryParse(rawData.Info._previewStartTime, out float pst) ? pst : 0f;
            level._previewDuration = float.TryParse(rawData.Info._previewDuration, out float pd) ? pd : 10f;

            // Load cover image on main thread
            if (rawData.CoverImageData != null)
            {
                level._coverImage = LoadCoverImageFromBytes(rawData.CoverImageData);
            }
            else
            {
                Debug.LogWarning($"No cover image data for {rawData.Info._songName}.");
            }

            level._songTimeOffset = rawData.Info._songTimeOffset;
            level._shuffle = rawData.Info._shuffle;
            level._shufflePeriod = rawData.Info._shufflePeriod;
            level._environmentSceneInfo = _niceEnvironmentSceneInfo;

            // Load audio clip (already a Coroutine, which runs on the main thread)
            yield return LoadAudioClipCoroutine(rawData.SongAudioPath, clip =>
            {
                level._audioClip = clip;
            });

            // Convert difficulties and create BeatmapDataSOs
            List<StandardLevelSO.DifficultyBeatmap> diffs = new List<StandardLevelSO.DifficultyBeatmap>();
            foreach (var set in rawData.Info._difficultyBeatmapSets)
            {
                if (set._beatmapCharacteristicName != "Standard") continue;

                foreach (var diff in set._difficultyBeatmaps)
                {
                    if (rawData.BeatmapJsons.TryGetValue(diff._beatmapFilename, out string beatmapDataJson))
                    {
                        LevelDifficulty levelDiff = LevelDifficulty.Easy;
                        Enum.TryParse(diff._difficulty, out levelDiff);

                        BeatmapDataSO beatmapData = ScriptableObject.CreateInstance<BeatmapDataSO>();
                        beatmapData.SetJsonData(beatmapDataJson);
                        beatmapData.SetRequiredDataForLoad(level._beatsPerMinute, level._shuffle, level._shufflePeriod);
                        beatmapData.Load(); // This Load() method might also do some processing, ensure it's performant

                        var db = new StandardLevelSO.DifficultyBeatmap(
                            level, levelDiff, diff._difficultyRank, diff._noteJumpMovementSpeed, beatmapData
                        );
                        diffs.Add(db);
                    }
                }
            }
            level._difficultyBeatmaps = diffs.ToArray();
            level.InitData(); // Initialize after all beatmaps are set

            onComplete?.Invoke(level);
        }

        // Original helper for loading cover image, modified to take bytes
        public static Sprite LoadCoverImageFromBytes(byte[] fileData)
        {
            if (fileData == null || fileData.Length == 0)
                return null;

            Texture2D tex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            // LoadImage *must* be on the main thread
            if (!tex.LoadImage(fileData))
                return null;

            return Sprite.Create(
                tex,
                new Rect(0, 0, tex.width, tex.height),
                new Vector2(0.5f, 0.5f)
            );
        }

        public static IEnumerator LoadAudioClipCoroutine(string songPath, Action<AudioClip> onLoaded)
        {
            string pathToUse = songPath;
            if (!File.Exists(pathToUse))
            {
                string altExt = Path.ChangeExtension(songPath, Path.GetExtension(songPath).ToLower() == ".egg" ? ".ogg" : ".egg");
                if (File.Exists(altExt))
                    pathToUse = altExt;
            }

            string url = "file:///" + pathToUse.Replace("\\", "/");
            using (var www = UnityEngine.Networking.UnityWebRequestMultimedia.GetAudioClip(url, AudioType.OGGVORBIS))
            {
                yield return www.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
                if (www.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
#else
                if (www.isNetworkError || www.isHttpError)
#endif
                {
                    Debug.LogError("Failed to load audio clip: " + www.error + " (" + url + ")");
                    onLoaded?.Invoke(null);
                }
                else
                {
                    AudioClip clip = UnityEngine.Networking.DownloadHandlerAudioClip.GetContent(www);
                    onLoaded?.Invoke(clip);
                }
            }
        }

        private string ProcessBeatmapJson(string beatmapJson, string songName = "Unknown Song")
        {
            // This method will now be called from a background thread.
            // Ensure SimpleJSON and your V3ToV2 code is thread-safe.
            // If SimpleJSON itself has static state or relies on main thread Unity APIs,
            // this could be an issue. Generally, pure data parsing should be fine.

            var node = JSON.Parse(beatmapJson);
            string version = node["version"];
            bool isV3 = !string.IsNullOrEmpty(version) && version.StartsWith("3");

            if (isV3)
            {
                Debug.Log($"Found V3 syntax for song {songName}. Converting using CM..."); // Log might show on main thread when task finishes
                // Ensure V3ToV2.ConvertFullBeatmap does NOT use Unity APIs
                var v2Node = V3ToV2.ConvertFullBeatmap(node.AsObject);
                Debug.Log("Converted V3 beatmap to V2.");
                return v2Node.ToString();
            }
            else
            {
                Debug.Log($"No V3 syntax detected for song {songName}. No conversion needed.");
                return beatmapJson;
            }
        }
    }
}