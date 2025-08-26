using UnityEngine;

namespace BeatmapEditor;

public class EditorStandardLevelInfoSO : ScriptableObject
{
	[SerializeField]
	private ObservableStringSO _songName;

	[SerializeField]
	private ObservableStringSO _songSubName;

	[SerializeField]
	private ObservableStringSO _songAuthorName;

	[SerializeField]
	private ObservableStringSO _levelAuthorName;

	public string songName => _songName;

	public string songSubName => _songSubName;

	public string songAuthorName => _songAuthorName;

	public string levelAuthorName => _levelAuthorName;

	public void SetDefaults()
	{
		_songName.value = "No Name";
		_songSubName.value = "No Name";
		_songAuthorName.value = "No Name";
		_levelAuthorName.value = "No Name";
	}

	public void SetValues(string songName, string songSubName, string songAuthorName, string levelAuthorName)
	{
		_songName.value = songName;
		_songSubName.value = songSubName;
		_songAuthorName.value = songAuthorName;
		_levelAuthorName.value = levelAuthorName;
	}
}
