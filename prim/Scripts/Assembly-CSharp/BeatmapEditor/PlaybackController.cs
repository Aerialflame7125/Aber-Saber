using System;
using UnityEngine;
using UnityEngine.UI;

namespace BeatmapEditor;

public class PlaybackController : MonoBehaviour
{
	[SerializeField]
	private ObservableFloatSO _songTimeOffset;

	[Space]
	[SerializeField]
	private BeatmapEditorSongController _beatmapEditorSongController;

	[SerializeField]
	private BeatmapEditorScrollView _beatmapEditorScrollView;

	[SerializeField]
	private Image _playButtonImage;

	[SerializeField]
	private Image _pauseButtonImage;

	public float songTime => _beatmapEditorScrollView.scrollPositionSongTime;

	private void Awake()
	{
		BeatmapEditorScrollView beatmapEditorScrollView = _beatmapEditorScrollView;
		beatmapEditorScrollView.scrollDragDidBeginEvent = (Action)Delegate.Combine(beatmapEditorScrollView.scrollDragDidBeginEvent, new Action(HandleScrollDidBeginDrag));
	}

	private void OnDestroy()
	{
		if (_beatmapEditorScrollView != null)
		{
			BeatmapEditorScrollView beatmapEditorScrollView = _beatmapEditorScrollView;
			beatmapEditorScrollView.scrollDragDidBeginEvent = (Action)Delegate.Remove(beatmapEditorScrollView.scrollDragDidBeginEvent, new Action(HandleScrollDidBeginDrag));
		}
	}

	private void Update()
	{
		if (_beatmapEditorSongController.isPlaying)
		{
			SyncSrollPositionWithSongControllerSongTime();
		}
		if (Input.GetKeyDown(KeyCode.Space) && !EventSystemHelper.IsInputFieldSelected())
		{
			PlayOrPauseSong();
		}
	}

	private void HandleScrollDidBeginDrag()
	{
		PauseSong();
	}

	private void SyncSrollPositionWithSongControllerSongTime()
	{
		float num = _beatmapEditorSongController.songTime;
		_beatmapEditorScrollView.SetPositionToSongTime(num);
	}

	private void RefreshUI()
	{
		bool isPlaying = _beatmapEditorSongController.isPlaying;
		_playButtonImage.enabled = !isPlaying;
		_pauseButtonImage.enabled = isPlaying;
	}

	public void PauseSong()
	{
		if (_beatmapEditorSongController.isPlaying)
		{
			_beatmapEditorSongController.PauseSong();
			_beatmapEditorScrollView.SnapPositionToBeat();
		}
	}

	public void PlayOrPauseSong()
	{
		if (_beatmapEditorSongController.isPlaying)
		{
			PauseSong();
		}
		else
		{
			_beatmapEditorSongController.PlaySong(_beatmapEditorScrollView.scrollPositionSongTime + (float)_songTimeOffset);
		}
		RefreshUI();
	}

	public void StopSong()
	{
		_beatmapEditorSongController.StopSong();
		RefreshUI();
		SyncSrollPositionWithSongControllerSongTime();
	}

	public void RewindSong()
	{
		_beatmapEditorSongController.RewindSong();
		RefreshUI();
		SyncSrollPositionWithSongControllerSongTime();
	}

	public void ResumeSavedPosition()
	{
		_beatmapEditorScrollView.MoveToSavedBarPosition();
	}

	public void PlayOrPauseButtonPressed()
	{
		PlayOrPauseSong();
	}

	public void StopButtonPressed()
	{
		StopSong();
	}

	public void RewindButtonPressed()
	{
		RewindSong();
	}
}
