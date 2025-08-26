using UnityEngine;

public class ApplySettingsOnBeatmapObjectSpawnController : MonoBehaviour
{
	[SerializeField]
	private MainSettingsModel _mainSettingsModel;

	[SerializeField]
	private BeatmapObjectSpawnController _beatmapObjectSpawnController;

	private void Awake()
	{
		_beatmapObjectSpawnController._globalYJumpOffset = (_mainSettingsModel.playerHeight - 1.8f) * 0.5f;
	}
}
