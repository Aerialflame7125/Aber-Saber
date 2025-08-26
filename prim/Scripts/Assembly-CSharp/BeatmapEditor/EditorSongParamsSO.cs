using UnityEngine;

namespace BeatmapEditor;

public class EditorSongParamsSO : ScriptableObject
{
	[SerializeField]
	private ObservableFloatSO _beatsPerMinute;

	[SerializeField]
	private ObservableFloatSO _songTimeOffset;

	[SerializeField]
	private ObservableFloatSO _shuffleStrength;

	[SerializeField]
	private ObservableFloatSO _shufflePeriod;

	public ObservableFloatSO beatsPerMinute => _beatsPerMinute;

	public ObservableFloatSO songTimeOffset => _songTimeOffset;

	public ObservableFloatSO shuffleStrength => _shuffleStrength;

	public ObservableFloatSO shufflePeriod => _shufflePeriod;

	public void SetDefaults()
	{
		_beatsPerMinute.value = 120f;
		_songTimeOffset.value = 0f;
		_shuffleStrength.value = 0f;
		_shufflePeriod.value = 0.5f;
	}

	public void SetValues(float beatsPerMinute, float songTimeOffset, float shuffleStrength, float shufflePeriod)
	{
		_beatsPerMinute.value = beatsPerMinute;
		_songTimeOffset.value = songTimeOffset;
		_shuffleStrength.value = shuffleStrength;
		_shufflePeriod.value = shufflePeriod;
	}
}
