using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using UnityEngine.Networking;
using System.Collections;

/*
#if UNITY_EDITOR
using UnityEditor;
#endif

// --- Data parsed from info.dat ---
[Serializable]
public class SongInfo
{
    public string _songName;
    public string _songAuthorName;
    public string _levelAuthorName;
    public float _beatsPerMinute;
    public string _songFilename;
    public string _coverImageFilename;
}

// --- Runtime Data Container ---
[Serializable]
public class CustomLevel
{
    public string levelID;
    public string songName;
    public string songAuthorName;
    public string levelAuthorName;
    public float bpm;
    public string songPath;
    public string coverImagePath;
}

public class CustomLevelLoader : MonoBehaviour
{
    public List<CustomLevel> loadedLevels = new List<CustomLevel>();

    public void Start()
    {
        LoadAllCustomLevels();

#if UNITY_EDITOR
        // OPTIONAL: Bake an asset collection in the Editor
        SaveAsAssetCollection();
#endif
    }

    public void LoadAllCustomLevels()
    {
        string basePath = Path.Combine(Application.streamingAssetsPath, "CustomLevels");

        if (!Directory.Exists(basePath))
        {
            Debug.LogWarning("CustomLevels folder not found. Creating...");
            Directory.CreateDirectory(basePath);
            return;
        }

        string[] folders = Directory.GetDirectories(basePath);

        foreach (string folder in folders)
        {
            string infoPath = Path.Combine(folder, "info.dat");
            if (!File.Exists(infoPath))
                continue;

            try
            {
                string json = File.ReadAllText(infoPath);
                var song = JsonConvert.DeserializeObject<SongInfo>(json);

                CustomLevel level = new CustomLevel
                {
                    levelID = Path.GetFileName(folder),
                    songName = song._songName,
                    songAuthorName = song._songAuthorName,
                    levelAuthorName = song._levelAuthorName,
                    bpm = song._beatsPerMinute,
                    songPath = Path.Combine(folder, song._songFilename),
                    coverImagePath = Path.Combine(folder, song._coverImageFilename)
                };

                loadedLevels.Add(level);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to load level from {infoPath}: {ex.Message}");
            }
        }

        Debug.Log($"Loaded {loadedLevels.Count} custom levels.");
    }

    public IEnumerator LoadSongAudio(string path, Action<AudioClip> onLoaded)
    {
        string uri = "file://" + path;
        UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(uri, AudioType.OGGVORBIS);
        yield return www.SendWebRequest();

        if (!www.isNetworkError && !www.isHttpError)
        {
            AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
            onLoaded?.Invoke(clip);
        }
        else
        {
            Debug.LogError("Failed to load audio: " + www.error);
        }
    }

    public IEnumerator LoadCoverImage(string path, Action<Texture2D> onLoaded)
    {
        string uri = "file://" + path;
        UnityWebRequest www = UnityWebRequestTexture.GetTexture(uri);
        yield return www.SendWebRequest();

        if (www.isNetworkError && !www.isHttpError)
        {
            Texture2D tex = DownloadHandlerTexture.GetContent(www);
            onLoaded?.Invoke(tex);
        }
        else
        {
            Debug.LogError("Failed to load image: " + www.error);
        }
    }

#if UNITY_EDITOR
    // Bake into .asset for Editor use
    public void SaveAsAssetCollection()
    {
        string savePath = "Assets/CustomLevelsCollection.asset";

        CustomLevelsCollection asset = AssetDatabase.LoadAssetAtPath<CustomLevelsCollection>(savePath);
        if (asset == null)
        {
            asset = ScriptableObject.CreateInstance<CustomLevelsCollection>();
            AssetDatabase.CreateAsset(asset, savePath);
        }

        asset.levels.Clear();
        asset.levels.AddRange(loadedLevels);

        EditorUtility.SetDirty(asset);
        AssetDatabase.SaveAssets();

        Debug.LogError("Saved CustomLevelsCollection.asset with " + loadedLevels.Count + " levels.");
    }
#endif
}
*/