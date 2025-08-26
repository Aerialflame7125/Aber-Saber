using UnityEngine;

public class GameSongController : SongController
{
	[SerializeField]
	private AudioTimeSyncController _audioTimeSyncController;

	[SerializeField]
	private AudioPitchGainEffect _failAudioPitchGainEffect;

	[SerializeField]
	private BeatmapObjectCallbackController _beatmapObjectCallbackController;

	private bool _songDidFinish;

	public float songLength
	{
		get
		{
			return _audioTimeSyncController.songLength;
		}
	}

	private void LateUpdate()
	{
		if (!_songDidFinish && _audioTimeSyncController.songTime >= _audioTimeSyncController.songLength)
		{
			_songDidFinish = true;
			SendSongDidFinishEvent();
		}
	}

	public override void StartSong()
	{
		_songDidFinish = false;
		_audioTimeSyncController.StartSong();
	}

	public override void StopSong()
	{
		_audioTimeSyncController.StopSong();
	}

	public override void PauseSong()
	{
		_audioTimeSyncController.Pause();
	}

	public override void ResumeSong()
	{
		_audioTimeSyncController.Resume();
	}

	public void FailStopSong()
	{
		for (int i = 0; i < 16; i++)
		{
			BeatmapEventData beatmapEventData = new BeatmapEventData(0f, (BeatmapEventType)i, -1);
			_beatmapObjectCallbackController.SendBeatmapEventDidTriggerEvent(beatmapEventData);
		}
		_beatmapObjectCallbackController.enabled = false;
		_audioTimeSyncController.forcedAudioSync = true;
		_failAudioPitchGainEffect.StartEffect(1f, delegate
		{
			_audioTimeSyncController.StopSong();
		});
	}
}
