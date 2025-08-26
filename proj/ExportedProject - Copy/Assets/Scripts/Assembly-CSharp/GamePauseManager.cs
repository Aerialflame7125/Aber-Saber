using UnityEngine;

public class GamePauseManager : MonoBehaviour
{
	[SerializeField]
	[Provider(typeof(PlayerController))]
	private ObjectProvider _playerControllerProvider;

	[SerializeField]
	private GameEnergyCounter _gameEnergyCounter;

	[SerializeField]
	private SongController _songController;

	[Space]
	[SerializeField]
	[EventSender]
	private GameEvent _gameDidPauseEvent;

	[SerializeField]
	[EventSender]
	private GameEvent _gameDidResumeEvent;

	private PlayerController _playerController;

	private bool _pause;

	public bool pause
	{
		get
		{
			return _pause;
		}
		set
		{
			if (_pause != value)
			{
				if (value)
				{
					PauseGame();
				}
				else
				{
					ResumeGame();
				}
				_pause = value;
			}
		}
	}

	public void PauseGame()
	{
		if (!_pause)
		{
			_playerController.disableSabers = true;
			_gameEnergyCounter.pause = true;
			_songController.PauseSong();
			_gameDidPauseEvent.Raise();
		}
	}

	public void ResumeGame()
	{
		if (_pause)
		{
			_playerController.disableSabers = false;
			_gameEnergyCounter.pause = false;
			_songController.ResumeSong();
			_gameDidResumeEvent.Raise();
		}
	}

	private void Start()
	{
		_playerController = _playerControllerProvider.GetProvidedObject<PlayerController>();
	}
}
